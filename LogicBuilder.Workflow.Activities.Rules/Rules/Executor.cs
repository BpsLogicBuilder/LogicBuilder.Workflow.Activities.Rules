// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

#define CODE_ANALYSIS
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region RuleExpressionResult class hierarchy
    public interface IRuleExpressionResult 
    {
        object Value { get; set; }
    }

    public class RuleLiteralResult(object literal) : IRuleExpressionResult
    {
        private readonly object literal = literal;

        public object Value
        {
            get
            {
                return literal;
            }
            set
            {
                throw new InvalidOperationException(Messages.CannotWriteToExpression);
            }
        }
    }

    internal class RuleFieldResult(object targetObject, FieldInfo fieldInfo) : IRuleExpressionResult
    {
        private readonly object targetObject = targetObject;
        private readonly FieldInfo fieldInfo = fieldInfo ?? throw new ArgumentNullException("fieldInfo");

        public object Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                if (!fieldInfo.IsStatic && targetObject == null)
                {
                    // Accessing a non-static field from null target.
                    string message = string.Format(CultureInfo.CurrentCulture, Messages.TargetEvaluatedNullField, fieldInfo.Name);
                    RuleEvaluationException exception = new(message);
                    exception.Data[RuleUserDataKeys.ErrorObject] = fieldInfo;
                    throw exception;
                }

                fieldInfo.SetValue(targetObject, value);
            }
        }

        private object GetValue()
        {
            if (!fieldInfo.IsStatic && targetObject == null)
            {
                // Accessing a non-static field from null target.
                string message = string.Format(CultureInfo.CurrentCulture, Messages.TargetEvaluatedNullField, fieldInfo.Name);
                RuleEvaluationException exception = new(message);
                exception.Data[RuleUserDataKeys.ErrorObject] = fieldInfo;
                throw exception;
            }

            return fieldInfo.GetValue(targetObject);
        }
    }

    internal class RulePropertyResult(PropertyInfo propertyInfo, object targetObject, object[] indexerArguments) : IRuleExpressionResult
    {
        private readonly PropertyInfo propertyInfo = propertyInfo ?? throw new ArgumentNullException("propertyInfo");
        private readonly object targetObject = targetObject;
        private readonly object[] indexerArguments = indexerArguments;

        public object Value
        {
            get
            {
                return GetValue();
            }

            set
            {
                if (!propertyInfo.GetSetMethod(true).IsStatic && targetObject == null)
                {
                    string message = string.Format(CultureInfo.CurrentCulture, Messages.TargetEvaluatedNullProperty, propertyInfo.Name);
                    RuleEvaluationException exception = new(message);
                    exception.Data[RuleUserDataKeys.ErrorObject] = propertyInfo;
                    throw exception;
                }

                try
                {
                    propertyInfo.SetValue(targetObject, value, indexerArguments);
                }
                catch (TargetInvocationException e)
                {
                    // if there is no inner exception, leave it untouched
                    if (e.InnerException == null)
                        throw;
                    string message = string.Format(CultureInfo.CurrentCulture, Messages.Error_PropertySet,
                        RuleDecompiler.DecompileType(propertyInfo.ReflectedType), propertyInfo.Name, e.InnerException.Message);
                    throw new TargetInvocationException(message, e.InnerException);
                }

            }
        }

        private object GetValue()
        {
            if (!propertyInfo.GetGetMethod(true).IsStatic && targetObject == null)
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.TargetEvaluatedNullProperty, propertyInfo.Name);
                RuleEvaluationException exception = new(message);
                exception.Data[RuleUserDataKeys.ErrorObject] = propertyInfo;
                throw exception;
            }

            try
            {
                return propertyInfo.GetValue(targetObject, indexerArguments);
            }
            catch (TargetInvocationException e)
            {
                // if there is no inner exception, leave it untouched
                if (e.InnerException == null)
                    throw;
                string message = string.Format(CultureInfo.CurrentCulture, Messages.Error_PropertyGet,
                    RuleDecompiler.DecompileType(propertyInfo.ReflectedType), propertyInfo.Name, e.InnerException.Message);
                throw new TargetInvocationException(message, e.InnerException);
            }
        }
    }

    internal class RuleArrayElementResult(Array targetArray, long[] indexerArguments) : IRuleExpressionResult
    {
        private readonly Array targetArray = targetArray ?? throw new ArgumentNullException("targetArray");
        private readonly long[] indexerArguments = indexerArguments ?? throw new ArgumentNullException("indexerArguments");

        public object Value
        {
            get
            {
                return targetArray.GetValue(indexerArguments);
            }

            set
            {
                targetArray.SetValue(value, indexerArguments);
            }
        }
    }
    #endregion

    #region RuleExecution Class
    public class RuleExecution
    {
        private readonly object thisObject;
        private RuleValidation validation;
        private readonly RuleLiteralResult thisLiteralResult;

        public RuleExecution(RuleValidation validation, object thisObject)
        {
            if (validation == null)
                throw new ArgumentNullException("validation");
            if (thisObject == null)
                throw new ArgumentNullException("thisObject");
            if (validation.ThisType != thisObject.GetType())
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Messages.ValidationMismatch,
                        RuleDecompiler.DecompileType(validation.ThisType),
                        RuleDecompiler.DecompileType(thisObject.GetType())));

            this.validation = validation;
            this.thisObject = thisObject;
            this.thisLiteralResult = new RuleLiteralResult(thisObject);
        }

        public object ThisObject
        {
            get { return thisObject; }
        }

        public RuleValidation Validation
        {
            get { return validation; }
            set
            {
                validation = value ?? throw new ArgumentNullException("value");
            }
        }

        public bool Halted { get; set; }

        internal RuleLiteralResult ThisLiteralResult
        {
            get { return this.thisLiteralResult; }
        }
    }
    #endregion

    #region RuleState internal class
    internal class RuleState : IComparable
    {
        internal readonly Rule Rule;

        internal RuleState(Rule rule)
        {
            this.Rule = rule;
        }

        internal ICollection<int> ThenActionsActiveRules { get; set; }

        internal ICollection<int> ElseActionsActiveRules { get; set; }

        int IComparable.CompareTo(object obj)
        {
            RuleState other = obj as RuleState;
            int compare = other?.Rule?.Priority.CompareTo(Rule.Priority) ?? -1;
            //using other to compare.  thisRule.Priority is greater than null (result is 1) - so return -1 for descending order.
            if (compare == 0)
                // if the priorities are the same, compare names (in ascending order)
                compare = -other.Rule?.Name.CompareTo(Rule.Name) ?? 1;
            return compare;
        }

        public override bool Equals(object obj)
        {
            if (obj is not RuleState other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Rule?.Priority == other.Rule?.Priority &&
                   Rule?.Name == other.Rule?.Name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Rule?.Priority.GetHashCode() ?? 0);
                hash = hash * 23 + (Rule?.Name?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public static bool operator ==(RuleState left, RuleState right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(RuleState left, RuleState right)
        {
            return !(left == right);
        }

        public static bool operator <(RuleState left, RuleState right)
        {
            if (left is null)
                return right is not null;

            return ((IComparable)left).CompareTo(right) < 0;
        }

        public static bool operator <=(RuleState left, RuleState right)
        {
            if (left is null)
                return true;

            return ((IComparable)left).CompareTo(right) <= 0;
        }

        public static bool operator >(RuleState left, RuleState right)
        {
            if (left is null)
                return false;

            return ((IComparable)left).CompareTo(right) > 0;
        }

        public static bool operator >=(RuleState left, RuleState right)
        {
            if (left is null)
                return right is null;

            return ((IComparable)left).CompareTo(right) >= 0;
        }
    }
    #endregion

    #region Tracking Argument
    #endregion

    internal class Executor
    {
        #region Rule Set Executor

        internal static IList<RuleState> Preprocess(RuleChainingBehavior behavior, ICollection<Rule> rules, RuleValidation validation, Tracer tracer)
        {
            // start by taking the active rules and make them into a list sorted by priority
            List<RuleState> orderedRules = new(rules.Count);
            foreach (Rule r in rules.Where(r => r.Active))
            {
                orderedRules.Add(new RuleState(r));
            }
            orderedRules.Sort();

            // Analyze the rules to match side-effects with dependencies.
            // Note that the RuleSet needs to have been validated prior to this.
            AnalyzeRules(behavior, orderedRules, validation, tracer);

            // return the sorted list of rules
            return orderedRules;
        }

        internal static void ExecuteRuleSet(IList<RuleState> orderedRules, RuleExecution ruleExecution, Tracer tracer)
        {
            // keep track of rule execution
            long[] executionCount = new long[orderedRules.Count];
            bool[] satisfied = new bool[orderedRules.Count];
            // clear the halted flag
            ruleExecution.Halted = false;

            // loop until we hit the end of the list
            int current = 0;
            while (current < orderedRules.Count)
            {
                RuleState currentRuleState = orderedRules[current];

                // does this rule need to be evaluated?
                if (satisfied[current])
                {
                    ++current;
                    continue;
                }

                // yes, so evaluate it and determine the list of actions needed
                tracer?.StartRule(currentRuleState.Rule.Name);
                satisfied[current] = true;
                bool result = currentRuleState.Rule.Condition.Evaluate(ruleExecution);
                tracer?.RuleResult(currentRuleState.Rule.Name, result);

                ICollection<RuleAction> actions = (result) ?
                    currentRuleState.Rule.thenActions :
                    currentRuleState.Rule.elseActions;
                ICollection<int> activeRules = result ?
                    currentRuleState.ThenActionsActiveRules :
                    currentRuleState.ElseActionsActiveRules;

                // are there any actions to be performed?
                if (actions == null || actions.Count <= 0)
                {
                    ++current;
                    continue;
                }

                ++executionCount[current];
                string ruleName = currentRuleState.Rule.Name;
                tracer?.StartActions(ruleName, result);
                ExecuteActions(ruleExecution, actions);

                // was Halt executed?
                if (ruleExecution.Halted)
                    break;

                // any fields updated?
                if (activeRules == null)
                    continue;

                current = GetCurrentIndex(orderedRules, tracer, executionCount, satisfied, current, activeRules, ruleName);
            }
            // no more rules to execute, so we are done
        }

        private static void ExecuteActions(RuleExecution ruleExecution, ICollection<RuleAction> actions)
        {
            // evaluate the actions
            foreach (RuleAction action in actions)
            {
                action.Execute(ruleExecution);

                // was Halt executed?
                if (ruleExecution.Halted)
                    break;
            }
        }

        private static int GetCurrentIndex(IList<RuleState> orderedRules, Tracer tracer, long[] executionCount, bool[] satisfied, int current, ICollection<int> activeRules, string ruleName)
        {
            foreach (int updatedRuleIndex in activeRules)
            {
                RuleState rs = orderedRules[updatedRuleIndex];
                if (satisfied[updatedRuleIndex]
                    && (executionCount[updatedRuleIndex] == 0 || rs.Rule.ReevaluationBehavior == RuleReevaluationBehavior.Always))
                {
                    // evaluate at least once, or repeatedly if appropriate
                    tracer?.TraceUpdate(ruleName, rs.Rule.Name);
                    satisfied[updatedRuleIndex] = false;
                    if (updatedRuleIndex < current)
                        current = updatedRuleIndex;
                }
            }

            return current;
        }

        class RuleSymbolInfo
        {
            internal ICollection<string> conditionDependencies;
            internal ICollection<string> thenSideEffects;
            internal ICollection<string> elseSideEffects;
        }


        private static void AnalyzeRules(RuleChainingBehavior behavior, List<RuleState> ruleStates, RuleValidation validation, Tracer tracer)
        {
            int i;
            int numRules = ruleStates.Count;

            // if no chaining is required, then nothing to do
            if (behavior == RuleChainingBehavior.None)
                return;

            // Analyze all the rules and collect all the dependencies & side-effects
            RuleSymbolInfo[] ruleSymbols = new RuleSymbolInfo[numRules];
            for (i = 0; i < numRules; ++i)
                ruleSymbols[i] = AnalyzeRule(behavior, ruleStates[i].Rule, validation, tracer);

            for (i = 0; i < numRules; ++i)
            {
                RuleState currentRuleState = ruleStates[i];

                if (ruleSymbols[i].thenSideEffects != null)
                {
                    currentRuleState.ThenActionsActiveRules = AnalyzeSideEffects(ruleSymbols[i].thenSideEffects, ruleSymbols);

                    if ((currentRuleState.ThenActionsActiveRules != null) && (tracer != null))
                        tracer.TraceThenTriggers(currentRuleState.Rule.Name, currentRuleState.ThenActionsActiveRules, ruleStates);
                }

                if (ruleSymbols[i].elseSideEffects != null)
                {
                    currentRuleState.ElseActionsActiveRules = AnalyzeSideEffects(ruleSymbols[i].elseSideEffects, ruleSymbols);

                    if ((currentRuleState.ElseActionsActiveRules != null) && (tracer != null))
                        tracer.TraceElseTriggers(currentRuleState.Rule.Name, currentRuleState.ElseActionsActiveRules, ruleStates);
                }
            }
        }

        private static ICollection<int> AnalyzeSideEffects(ICollection<string> sideEffects, RuleSymbolInfo[] ruleSymbols)
        {
            Dictionary<int, object> affectedRules = [];

            for (int i = 0; i < ruleSymbols.Length; ++i)
            {
                ICollection<string> dependencies = ruleSymbols[i].conditionDependencies;
                if (dependencies == null)
                {
                    continue;
                }

                foreach (string sideEffect in sideEffects)
                {
                    bool match = false;

                    if (sideEffect.EndsWith("*", StringComparison.Ordinal))
                    {
                        match = AnalyzeWildCards(dependencies, sideEffect, match);
                    }
                    else
                    {
                        match = AnalyzeNonWildCards(dependencies, sideEffect, match);
                    }

                    if (match)
                    {
                        affectedRules[i] = null;
                        break;
                    }
                }
            }

            return affectedRules.Keys;
        }

        private static bool AnalyzeWildCards(ICollection<string> dependencies, string sideEffect, bool match)
        {
            foreach (string dependency in dependencies)
            {
                if (dependency.EndsWith("*", StringComparison.Ordinal))
                {
                    // Strip the trailing "/*" from the dependency
                    string stripDependency = dependency.Substring(0, dependency.Length - 2);
                    // Strip the trailing "*" from the side-effect
                    string stripSideEffect = sideEffect.Substring(0, sideEffect.Length - 1);

                    string shortString;
                    string longString;

                    if (stripDependency.Length < stripSideEffect.Length)
                    {
                        shortString = stripDependency;
                        longString = stripSideEffect;
                    }
                    else
                    {
                        shortString = stripSideEffect;
                        longString = stripDependency;
                    }

                    // There's a match if the shorter string is a prefix of the longer string.
                    if (longString.StartsWith(shortString, StringComparison.Ordinal))
                    {
                        match = true;
                        break;
                    }
                }
                else
                {
                    string stripSideEffect = sideEffect.Substring(0, sideEffect.Length - 1);
                    string stripDependency = GetsStripDependency(dependency);

                    if (stripDependency.StartsWith(stripSideEffect, StringComparison.Ordinal))
                    {
                        match = true;
                        break;
                    }
                }
            }

            return match;
        }

        private static string GetsStripDependency(string dependency)
        {
            string stripDependency = dependency;
            if (stripDependency.EndsWith("/", StringComparison.Ordinal))
                stripDependency = stripDependency.Substring(0, stripDependency.Length - 1);
            return stripDependency;
        }

        private static bool AnalyzeNonWildCards(ICollection<string> dependencies, string sideEffect, bool match)
        {
            // The side-effect did not end with a wildcard
            foreach (string dependency in dependencies)
            {
                if (dependency.EndsWith("*", StringComparison.Ordinal))
                {
                    // Strip the trailing "/*"
                    string stripDependency = dependency.Substring(0, dependency.Length - 2);

                    string shortString;
                    string longString;

                    if (stripDependency.Length < sideEffect.Length)
                    {
                        shortString = stripDependency;
                        longString = sideEffect;
                    }
                    else
                    {
                        shortString = sideEffect;
                        longString = stripDependency;
                    }

                    // There's a match if the shorter string is a prefix of the longer string.
                    if (longString.StartsWith(shortString, StringComparison.Ordinal))
                    {
                        match = true;
                        break;
                    }
                }
                else
                {
                    // The side-effect must be a prefix of the dependency (or an exact match).
                    if (dependency.StartsWith(sideEffect, StringComparison.Ordinal))
                    {
                        match = true;
                        break;
                    }
                }
            }

            return match;
        }

        private static RuleSymbolInfo AnalyzeRule(RuleChainingBehavior behavior, Rule rule, RuleValidation validator, Tracer tracer)
        {
            RuleSymbolInfo rsi = new();

            if (rule.Condition != null)
            {
                rsi.conditionDependencies = rule.Condition.GetDependencies(validator);

                if ((rsi.conditionDependencies != null) && (tracer != null))
                    tracer.TraceConditionSymbols(rule.Name, rsi.conditionDependencies);
            }

            if (rule.thenActions != null)
            {
                rsi.thenSideEffects = GetActionSideEffects(behavior, rule.thenActions, validator);

                if ((rsi.thenSideEffects != null) && (tracer != null))
                    tracer.TraceThenSymbols(rule.Name, rsi.thenSideEffects);
            }

            if (rule.elseActions != null)
            {
                rsi.elseSideEffects = GetActionSideEffects(behavior, rule.elseActions, validator);

                if ((rsi.elseSideEffects != null) && (tracer != null))
                    tracer.TraceElseSymbols(rule.Name, rsi.elseSideEffects);
            }

            return rsi;
        }

        private static ICollection<string> GetActionSideEffects(RuleChainingBehavior behavior, IList<RuleAction> actions, RuleValidation validation)
        {
            // Man, I wish there were a Set<T> class...
            Dictionary<string, object> symbols = [];

            foreach (RuleAction action in actions.Where
                        (
                            a => behavior == RuleChainingBehavior.Full
                                || (behavior == RuleChainingBehavior.UpdateOnly && a is RuleUpdateAction)
                        ))
            {
                ICollection<string> sideEffects = action.GetSideEffects(validation);
                if (sideEffects != null)
                {
                    foreach (string symbol in sideEffects)
                        symbols[symbol] = null;
                }
            }

            return symbols.Keys;
        }

        #endregion

        #region Condition Executors
        internal static bool EvaluateBool(CodeExpression expression, RuleExecution context)
        {
            object result = RuleExpressionWalker.Evaluate(context, expression).Value;
            if (result is bool boolResult)
                return boolResult;

            Type expectedType = context.Validation.ExpressionInfo(expression).ExpressionType;
            if (expectedType == null)
            {
                // oops ... not a boolean, so error
                InvalidOperationException exception = new(Messages.ConditionMustBeBoolean);
                exception.Data[RuleUserDataKeys.ErrorObject] = expression;
                throw exception;
            }

            return (bool)AdjustType(expectedType, result, typeof(bool));
        }

        internal static object AdjustType(Type operandType, object operandValue, Type toType)
        {
            // if no conversion required, we are done
            if (operandType == toType)
                return operandValue;

            if (AdjustValueStandard(operandType, operandValue, toType, out object converted))
                return converted;

            // not a standard conversion, see if it's an implicit user defined conversions
            MethodInfo conversion = RuleValidation.FindImplicitConversion(operandType, toType, out ValidationError error);
            if (conversion == null)
            {
                if (error != null)
                    throw new RuleEvaluationException(error.ErrorText);

                throw new RuleEvaluationException(
                    string.Format(CultureInfo.CurrentCulture,
                        Messages.CastIncompatibleTypes,
                        RuleDecompiler.DecompileType(operandType),
                        RuleDecompiler.DecompileType(toType)));
            }

            // now we have a method, need to do the conversion S -> Sx -> Tx -> T
            Type sx = conversion.GetParameters()[0].ParameterType;
            Type tx = conversion.ReturnType;

            if (AdjustValueStandard(operandType, operandValue, sx, out object intermediateResult1))
            {
                // we are happy with the first conversion, so call the user's static method
                object intermediateResult2 = conversion.Invoke(null, [intermediateResult1]);
                if (AdjustValueStandard(tx, intermediateResult2, toType, out object intermediateResult3))
                    return intermediateResult3;
            }
            throw new RuleEvaluationException(
                string.Format(CultureInfo.CurrentCulture,
                    Messages.CastIncompatibleTypes,
                    RuleDecompiler.DecompileType(operandType),
                    RuleDecompiler.DecompileType(toType)));
        }

        internal static object AdjustTypeWithCast(Type operandType, object operandValue, Type toType)
        {
            // if no conversion required, we are done
            if (operandType == toType)
                return operandValue;

            if (AdjustValueStandard(operandType, operandValue, toType, out object converted))
                return converted;

            // handle enumerations (done above?)

            // now it's time for implicit and explicit user defined conversions
            MethodInfo conversion = RuleValidation.FindExplicitConversion(operandType, toType, out ValidationError error);
            if (conversion == null)
            {
                if (error != null)
                    throw new RuleEvaluationException(error.ErrorText);

                throw new RuleEvaluationException(
                    string.Format(CultureInfo.CurrentCulture,
                        Messages.CastIncompatibleTypes,
                        RuleDecompiler.DecompileType(operandType),
                        RuleDecompiler.DecompileType(toType)));
            }

            // now we have a method, need to do the conversion S -> Sx -> Tx -> T
            Type sx = conversion.GetParameters()[0].ParameterType;
            Type tx = conversion.ReturnType;

            if (AdjustValueStandard(operandType, operandValue, sx, out object intermediateResult1))
            {
                // we are happy with the first conversion, so call the user's static method
                object intermediateResult2 = conversion.Invoke(null, [intermediateResult1]);
                if (AdjustValueStandard(tx, intermediateResult2, toType, out object intermediateResult3))
                    return intermediateResult3;
            }
            throw new RuleEvaluationException(
                string.Format(CultureInfo.CurrentCulture,
                    Messages.CastIncompatibleTypes,
                    RuleDecompiler.DecompileType(operandType),
                    RuleDecompiler.DecompileType(toType)));
        }

        private static bool AdjustValueStandard(Type operandType, object operandValue, Type toType, out object converted)
        {
            // assume it's the same for now
            converted = operandValue;

            // check for null
            if (operandValue == null)
            {
                return ConvertNullOperand(operandType, toType, ref converted);
            }

            // check simple cases
            Type currentType = operandValue.GetType();
            if (currentType == toType)
                return true;

            // now the fun begins
            // this should handle most class conversions
            if (toType.IsAssignableFrom(currentType))
                return true;

            // handle the numerics (both implicit and explicit), along with nullable
            // note that if the value was null, it's already handled, so value cannot be nullable
            if ((currentType.IsValueType) && (toType.IsValueType))
            {
                return ConvertValueType(ref operandValue, toType, ref converted, ref currentType);
            }

            // no luck with standard conversions, so no conversion done
            return false;
        }

        private static bool ConvertValueType(ref object operandValue, Type toType, ref object converted, ref Type currentType)
        {
            if (currentType.IsEnum)
            {
                // strip off the enum representation
                currentType = Enum.GetUnderlyingType(currentType);
                ArithmeticLiteral literal = ArithmeticLiteral.MakeLiteral(currentType, operandValue);
                operandValue = literal.Value;
            }

            bool resultNullable = ConditionHelper.IsNullableValueType(toType);
            Type resultType = (resultNullable) ? Nullable.GetUnderlyingType(toType) : toType;

            if (resultType.IsEnum)
            {
                // Enum.ToObject may throw if currentType is not type SByte, 
                // Int16, Int32, Int64, Byte, UInt16, UInt32, or UInt64.
                // So we adjust currentValue to the underlying type (which may throw if out of range)
                Type underlyingType = Enum.GetUnderlyingType(resultType);
                if (AdjustValueStandard(currentType, operandValue, underlyingType, out object adjusted))
                {
                    converted = Enum.ToObject(resultType, adjusted);
                    if (resultNullable)
                        converted = Activator.CreateInstance(toType, converted);
                    return true;
                }
            }
            else if ((resultType.IsPrimitive) || (resultType == typeof(decimal)))
            {
                return ConvertPrimitiveOrDecimalType(operandValue, toType, ref converted, currentType, resultNullable, resultType);
            }

            return false;
        }

        private static bool ConvertPrimitiveOrDecimalType(object operandValue, Type toType, ref object converted, Type currentType, bool resultNullable, Type resultType)
        {
            // resultType must be a primitive to continue (not a struct)
            // (enums and generics handled above)
            if (currentType == typeof(char))
            {
                return ConvertChar(operandValue, toType, out converted, resultNullable, resultType);
            }
            else if (currentType == typeof(float))
            {
                float f = (float)operandValue;
                converted = resultType == typeof(char)
                    ? (char)f
                    : ((IConvertible)f).ToType(resultType, CultureInfo.CurrentCulture);
                if (resultNullable)
                    converted = Activator.CreateInstance(toType, converted);
                return true;
            }
            else if (currentType == typeof(double))
            {
                double d = (double)operandValue;
                converted = resultType == typeof(char)
                    ? (char)d
                    : ((IConvertible)d).ToType(resultType, CultureInfo.CurrentCulture);
                if (resultNullable)
                    converted = Activator.CreateInstance(toType, converted);
                return true;
            }
            else if (currentType == typeof(decimal))
            {
                return ConvertDecimal(operandValue, toType, out converted, resultNullable, resultType);
            }
            else
            {
                return ConvertConvertible(operandValue, toType, ref converted, resultNullable, resultType);
            }
        }

        private static bool ConvertDecimal(object operandValue, Type toType, out object converted, bool resultNullable, Type resultType)
        {
            decimal d = (decimal)operandValue;
            converted = resultType == typeof(char)
                ? (char)d
                : ((IConvertible)d).ToType(resultType, CultureInfo.CurrentCulture);
            if (resultNullable)
                converted = Activator.CreateInstance(toType, converted);
            return true;
        }

        private static bool ConvertConvertible(object operandValue, Type toType, ref object converted, bool resultNullable, Type resultType)
        {
            if (operandValue is IConvertible convert)
            {
                try
                {
                    converted = convert.ToType(resultType, CultureInfo.CurrentCulture);
                    if (resultNullable)
                        converted = Activator.CreateInstance(toType, converted);
                    return true;
                }
                catch (InvalidCastException)
                {
                    // not IConvertable, so can't do it
                    return false;
                }
            }

            return false;
        }

        private static bool ConvertChar(object operandValue, Type toType, out object converted, bool resultNullable, Type resultType)
        {
            char c = (char)operandValue;
            if (resultType == typeof(float))
            {
                converted = (float)c;
            }
            else if (resultType == typeof(double))
            {
                converted = (double)c;
            }
            else if (resultType == typeof(decimal))
            {
                converted = (decimal)c;
            }
            else
            {
                converted = ((IConvertible)c).ToType(resultType, CultureInfo.CurrentCulture);
            }
            if (resultNullable)
                converted = Activator.CreateInstance(toType, converted);
            return true;
        }

        private static bool ConvertNullOperand(Type operandType, Type toType, ref object converted)
        {
            // are we converting to a value type?
            if (toType.IsValueType)
            {
                // is the conversion to nullable?
                if (!ConditionHelper.IsNullableValueType(toType))
                {
                    // value type and null, so no conversion possible
                    string message = string.Format(CultureInfo.CurrentCulture, Messages.CannotCastNullToValueType, RuleDecompiler.DecompileType(toType));
                    throw new InvalidCastException(message);
                }

                // here we have a Nullable<T>
                // however, we may need to call the implicit conversion operator if the types are not compatible
                converted = Activator.CreateInstance(toType);
                return RuleValidation.StandardImplicitConversion(operandType, toType, null, out _);
            }

            // not a value type, so null is valid
            return true;
        }
        #endregion
    }
}
