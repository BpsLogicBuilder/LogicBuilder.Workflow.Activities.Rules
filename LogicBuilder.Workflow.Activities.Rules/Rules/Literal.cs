// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

#define CODE_ANALYSIS
using LogicBuilder.Workflow.Activities.Common;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region Literal Class
    /// <summary>
    /// Represents a typed literal value and is the base class for all type specific literal classes
    /// </summary>
    public abstract class Literal
    {
        #region Properties
        /// <summary>
        /// A delegate for literal factory methods
        /// </summary>
        private delegate Literal LiteralMaker(object literalValue);

        /// <summary>
        /// Collection of literal factory methods indexed by type
        /// </summary>
        private static readonly Dictionary<Type, LiteralMaker> types = CreateMakersDictionary();

        /// <summary>
        /// The type of the literal
        /// </summary>
        protected internal Type m_type;

        /// <summary>
        /// Get the boxed literal
        /// </summary>
        internal abstract object Value { get; }

        /// <summary>
        /// Group types by similar characteristics so we can check if comparisons succeed
        /// </summary>
        [Flags()]
        enum NumberTypes
        {
            SignedNumbers = 0x01,
            UnsignedNumbers = 0x02,
            ULong = 0x04,
            Float = 0x08,
            Decimal = 0x10,
            String = 0x20,
            Bool = 0x40
        };

        /// <summary>
        /// Collection of TypeFlags for the supported value types indexed by type
        /// </summary>
        private static readonly Dictionary<Type, NumberTypes> supportedTypes = CreateTypesDictionary();

        private static Dictionary<Type, LiteralMaker> CreateMakersDictionary()
        {
            // Create the literal class factory delegates
            Dictionary<Type, LiteralMaker> dictionary = new(32)
            {
                { typeof(byte), new LiteralMaker(Literal.MakeByte) },
                { typeof(sbyte), new LiteralMaker(Literal.MakeSByte) },
                { typeof(short), new LiteralMaker(Literal.MakeShort) },
                { typeof(int), new LiteralMaker(Literal.MakeInt) },
                { typeof(long), new LiteralMaker(Literal.MakeLong) },
                { typeof(ushort), new LiteralMaker(Literal.MakeUShort) },
                { typeof(uint), new LiteralMaker(Literal.MakeUInt) },
                { typeof(ulong), new LiteralMaker(Literal.MakeULong) },
                { typeof(float), new LiteralMaker(Literal.MakeFloat) },
                { typeof(double), new LiteralMaker(Literal.MakeDouble) },
                { typeof(char), new LiteralMaker(Literal.MakeChar) },
                { typeof(string), new LiteralMaker(Literal.MakeString) },
                { typeof(decimal), new LiteralMaker(Literal.MakeDecimal) },
                { typeof(bool), new LiteralMaker(Literal.MakeBool) },
                { typeof(byte?), new LiteralMaker(Literal.MakeByte) },
                { typeof(sbyte?), new LiteralMaker(Literal.MakeSByte) },
                { typeof(short?), new LiteralMaker(Literal.MakeShort) },
                { typeof(int?), new LiteralMaker(Literal.MakeInt) },
                { typeof(long?), new LiteralMaker(Literal.MakeLong) },
                { typeof(ushort?), new LiteralMaker(Literal.MakeUShort) },
                { typeof(uint?), new LiteralMaker(Literal.MakeUInt) },
                { typeof(ulong?), new LiteralMaker(Literal.MakeULong) },
                { typeof(float?), new LiteralMaker(Literal.MakeFloat) },
                { typeof(double?), new LiteralMaker(Literal.MakeDouble) },
                { typeof(char?), new LiteralMaker(Literal.MakeChar) },
                { typeof(decimal?), new LiteralMaker(Literal.MakeDecimal) },
                { typeof(bool?), new LiteralMaker(Literal.MakeBool) }
            };
            return dictionary;
        }

        private static Dictionary<Type, NumberTypes> CreateTypesDictionary()
        {
            // Create the literal class factory delegates
            Dictionary<Type, NumberTypes> dictionary = new(32)
            {
                { typeof(byte), NumberTypes.UnsignedNumbers },
                { typeof(byte?), NumberTypes.UnsignedNumbers },
                { typeof(sbyte), NumberTypes.SignedNumbers },
                { typeof(sbyte?), NumberTypes.SignedNumbers },
                { typeof(short), NumberTypes.SignedNumbers },
                { typeof(short?), NumberTypes.SignedNumbers },
                { typeof(int), NumberTypes.SignedNumbers },
                { typeof(int?), NumberTypes.SignedNumbers },
                { typeof(long), NumberTypes.SignedNumbers },
                { typeof(long?), NumberTypes.SignedNumbers },
                { typeof(ushort), NumberTypes.UnsignedNumbers },
                { typeof(ushort?), NumberTypes.UnsignedNumbers },
                { typeof(uint), NumberTypes.UnsignedNumbers },
                { typeof(uint?), NumberTypes.UnsignedNumbers },
                { typeof(ulong), NumberTypes.ULong },
                { typeof(ulong?), NumberTypes.ULong },
                { typeof(float), NumberTypes.Float },
                { typeof(float?), NumberTypes.Float },
                { typeof(double), NumberTypes.Float },
                { typeof(double?), NumberTypes.Float },
                { typeof(char), NumberTypes.UnsignedNumbers },
                { typeof(char?), NumberTypes.UnsignedNumbers },
                { typeof(string), NumberTypes.String },
                { typeof(decimal), NumberTypes.Decimal },
                { typeof(decimal?), NumberTypes.Decimal },
                { typeof(bool), NumberTypes.Bool },
                { typeof(bool?), NumberTypes.Bool }
            };
            return dictionary;
        }
        #endregion

        #region Factory Methods
        internal static Literal MakeLiteral(Type literalType, object literalValue)
        {
            if (literalValue == null)
                return new NullLiteral(literalType);

            if (types.TryGetValue(literalType, out LiteralMaker f))
            {
                try
                {
                    return f(literalValue);
                }
                catch (InvalidCastException e)
                {
                    throw new RuleEvaluationIncompatibleTypesException(
                        string.Format(CultureInfo.CurrentCulture,
                            Messages.InvalidCast,
                            RuleDecompiler.DecompileType(literalValue.GetType()),
                            RuleDecompiler.DecompileType(literalType)),
                        literalType,
                        CodeBinaryOperatorType.Assign,
                        literalValue.GetType(),
                        e);
                }
            }
            return null;
        }

        /// <summary>
        /// Factory function for a boolean type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeBool(object literalValue)
        {
            return new BoolLiteral((bool)literalValue);
        }

        /// <summary>
        /// Factory function for a byte type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeByte(object literalValue)
        {
            return new ByteLiteral((byte)literalValue);
        }

        /// <summary>
        /// Factory function for a sbyte type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeSByte(object literalValue)
        {
            return new SByteLiteral((sbyte)literalValue);
        }

        /// <summary>
        /// Factory function for a char type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeChar(object literalValue)
        {
            return new CharLiteral((char)literalValue);
        }

        /// <summary>
        /// Factory function for a decimal type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeDecimal(object literalValue)
        {
            return new DecimalLiteral((decimal)literalValue);
        }

        /// <summary>
        /// Factory function for an Int16 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeShort(object literalValue)
        {
            return new ShortLiteral((short)literalValue);
        }

        /// <summary>
        /// Factory function for an Int32 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeInt(object literalValue)
        {
            return new IntLiteral((int)literalValue);
        }

        /// <summary>
        /// Factory function for an Int64 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeLong(object literalValue)
        {
            return new LongLiteral((long)literalValue);
        }

        /// <summary>
        /// Factory function for an UInt16 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeUShort(object literalValue)
        {
            return new UShortLiteral((ushort)literalValue);
        }

        /// <summary>
        /// Factory function for an UInt32 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeUInt(object literalValue)
        {
            return new UIntLiteral((uint)literalValue);
        }

        /// <summary>
        /// Factory function for an UInt64 type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeULong(object literalValue)
        {
            return new ULongLiteral((ulong)literalValue);
        }

        /// <summary>
        /// Factory function for a float type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeFloat(object literalValue)
        {
            return new FloatLiteral((float)literalValue);
        }

        /// <summary>
        /// Factory function for a double type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeDouble(object literalValue)
        {
            return new DoubleLiteral((double)literalValue);
        }

        /// <summary>
        /// Factory function for a string type
        /// </summary>
        /// <param name="literalValue"></param>
        /// <param name="readOnly"></param>
        private static Literal MakeString(object literalValue)
        {
            return new StringLiteral((string)literalValue);
        }
        #endregion

        #region Default Operators
        internal static class DefaultOperators
        {
            public static int Addition(int x, int y) { return x + y; }
            public static uint Addition(uint x, uint y) { return x + y; }
            public static long Addition(long x, long y) { return x + y; }
            public static ulong Addition(ulong x, ulong y) { return x + y; }
            public static float Addition(float x, float y) { return x + y; }
            public static double Addition(double x, double y) { return x + y; }
            public static decimal Addition(decimal x, decimal y) { return x + y; }
            public static string Addition(string x, string y) { return x + y; }
            public static string Addition(string x, object y) { return x + y; }
            public static string Addition(object x, string y) { return x + y; }

            public static int Subtraction(int x, int y) { return x - y; }
            public static uint Subtraction(uint x, uint y) { return x - y; }
            public static long Subtraction(long x, long y) { return x - y; }
            public static ulong Subtraction(ulong x, ulong y) { return x - y; }
            public static float Subtraction(float x, float y) { return x - y; }
            public static double Subtraction(double x, double y) { return x - y; }
            public static decimal Subtraction(decimal x, decimal y) { return x - y; }

            public static int Multiply(int x, int y) { return x * y; }
            public static uint Multiply(uint x, uint y) { return x * y; }
            public static long Multiply(long x, long y) { return x * y; }
            public static ulong Multiply(ulong x, ulong y) { return x * y; }
            public static float Multiply(float x, float y) { return x * y; }
            public static double Multiply(double x, double y) { return x * y; }
            public static decimal Multiply(decimal x, decimal y) { return x * y; }

            public static int Division(int x, int y) { return x / y; }
            public static uint Division(uint x, uint y) { return x / y; }
            public static long Division(long x, long y) { return x / y; }
            public static ulong Division(ulong x, ulong y) { return x / y; }
            public static float Division(float x, float y) { return x / y; }
            public static double Division(double x, double y) { return x / y; }
            public static decimal Division(decimal x, decimal y) { return x / y; }

            public static int Modulus(int x, int y) { return x % y; }
            public static uint Modulus(uint x, uint y) { return x % y; }
            public static long Modulus(long x, long y) { return x % y; }
            public static ulong Modulus(ulong x, ulong y) { return x % y; }
            public static float Modulus(float x, float y) { return x % y; }
            public static double Modulus(double x, double y) { return x % y; }
            public static decimal Modulus(decimal x, decimal y) { return x % y; }

            public static int BitwiseAnd(int x, int y) { return x & y; }
            public static uint BitwiseAnd(uint x, uint y) { return x & y; }
            public static long BitwiseAnd(long x, long y) { return x & y; }
            public static ulong BitwiseAnd(ulong x, ulong y) { return x & y; }
            public static bool BitwiseAnd(bool x, bool y) { return x && y; }

            public static int BitwiseOr(int x, int y) { return x | y; }
            public static uint BitwiseOr(uint x, uint y) { return x | y; }
            public static long BitwiseOr(long x, long y) { return x | y; }
            public static ulong BitwiseOr(ulong x, ulong y) { return x | y; }
            public static bool BitwiseOr(bool x, bool y) { return x || y; }

            public static bool Equality(int x, int y) { return x == y; }
            public static bool Equality(uint x, uint y) { return x == y; }
            public static bool Equality(long x, long y) { return x == y; }
            public static bool Equality(ulong x, ulong y) { return x == y; }
            public static bool Equality(float x, float y) { return Math.Abs(x - y) < float.Epsilon; }
            public static bool Equality(double x, double y) { return Math.Abs(x - y) < double.Epsilon; }
            public static bool Equality(decimal x, decimal y) { return x == y; }
            public static bool Equality(bool x, bool y) { return x == y; }
            public static bool Equality(string x, string y) { return x == y; }
            // mark object == object since it has special rules
            public static bool ObjectEquals(object x, object y) { return x == y; }

            public static bool GreaterThan(int x, int y) { return x > y; }
            public static bool GreaterThan(uint x, uint y) { return x > y; }
            public static bool GreaterThan(long x, long y) { return x > y; }
            public static bool GreaterThan(ulong x, ulong y) { return x > y; }
            public static bool GreaterThan(float x, float y) { return x > y; }
            public static bool GreaterThan(double x, double y) { return x > y; }
            public static bool GreaterThan(decimal x, decimal y) { return x > y; }

            public static bool GreaterThanOrEqual(int x, int y) { return x >= y; }
            public static bool GreaterThanOrEqual(uint x, uint y) { return x >= y; }
            public static bool GreaterThanOrEqual(long x, long y) { return x >= y; }
            public static bool GreaterThanOrEqual(ulong x, ulong y) { return x >= y; }
            public static bool GreaterThanOrEqual(float x, float y) { return x >= y; }
            public static bool GreaterThanOrEqual(double x, double y) { return x >= y; }
            public static bool GreaterThanOrEqual(decimal x, decimal y) { return x >= y; }

            public static bool LessThan(int x, int y) { return x < y; }
            public static bool LessThan(uint x, uint y) { return x < y; }
            public static bool LessThan(long x, long y) { return x < y; }
            public static bool LessThan(ulong x, ulong y) { return x < y; }
            public static bool LessThan(float x, float y) { return x < y; }
            public static bool LessThan(double x, double y) { return x < y; }
            public static bool LessThan(decimal x, decimal y) { return x < y; }

            public static bool LessThanOrEqual(int x, int y) { return x <= y; }
            public static bool LessThanOrEqual(uint x, uint y) { return x <= y; }
            public static bool LessThanOrEqual(long x, long y) { return x <= y; }
            public static bool LessThanOrEqual(ulong x, ulong y) { return x <= y; }
            public static bool LessThanOrEqual(float x, float y) { return x <= y; }
            public static bool LessThanOrEqual(double x, double y) { return x <= y; }
            public static bool LessThanOrEqual(decimal x, decimal y) { return x <= y; }
        }
        #endregion

        #region Type Checking Methods

        internal static RuleBinaryExpressionInfo AllowedComparison(
            Type lhs,
            CodeExpression lhsExpression,
            Type rhs,
            CodeExpression rhsExpression,
            CodeBinaryOperatorType comparison,
            RuleValidation validator,
            out ValidationError error)
        {
            // note that null values come in as a NullLiteral type

            // are the types supported?
            if ((supportedTypes.TryGetValue(lhs, out NumberTypes lhsFlags)) && (supportedTypes.TryGetValue(rhs, out NumberTypes rhsFlags)))
            {
                // both sides supported
                if (lhsFlags == rhsFlags)
                {
                    // both sides the same type, so it's allowed
                    // only allow equality on booleans
                    if ((lhsFlags == NumberTypes.Bool) && (comparison != CodeBinaryOperatorType.ValueEquality))
                    {
                        string message = string.Format(CultureInfo.CurrentCulture, Messages.RelationalOpBadTypes, comparison,
                            RuleDecompiler.DecompileType(lhs),
                            RuleDecompiler.DecompileType(rhs));
                        error = new ValidationError(message, ErrorNumbers.Error_OperandTypesIncompatible);
                        return null;
                    }
                    error = null;
                    return new RuleBinaryExpressionInfo(lhs, rhs, typeof(bool));
                }

                // if not the same, only certain combinations allowed
                switch (lhsFlags | rhsFlags)
                {
                    case NumberTypes.Decimal | NumberTypes.SignedNumbers:
                    case NumberTypes.Decimal | NumberTypes.UnsignedNumbers:
                    case NumberTypes.Decimal | NumberTypes.ULong:
                    case NumberTypes.Float | NumberTypes.SignedNumbers:
                    case NumberTypes.Float | NumberTypes.UnsignedNumbers:
                    case NumberTypes.Float | NumberTypes.ULong:
                    case NumberTypes.ULong | NumberTypes.UnsignedNumbers:
                    case NumberTypes.SignedNumbers | NumberTypes.UnsignedNumbers:
                        error = null;
                        return new RuleBinaryExpressionInfo(lhs, rhs, typeof(bool));
                }
                string message2 = string.Format(CultureInfo.CurrentCulture, Messages.RelationalOpBadTypes, comparison,
                    (lhs == typeof(NullLiteral)) ? Messages.NullValue : RuleDecompiler.DecompileType(lhs),
                    (rhs == typeof(NullLiteral)) ? Messages.NullValue : RuleDecompiler.DecompileType(rhs));
                error = new ValidationError(message2, ErrorNumbers.Error_OperandTypesIncompatible);
                return null;
            }

            // see if they override the operator
            MethodInfo operatorOverride = MapOperatorToMethod(comparison, lhs, lhsExpression, rhs, rhsExpression, validator, out error);
            if (operatorOverride != null)
                return new RuleBinaryExpressionInfo(lhs, rhs, operatorOverride);

            // unable to evaluate, so return false
            return null;
        }

        internal enum OperatorGrouping
        {
            None = -1,
            Arithmetic,
            Equality,
            Relational
        }

        [ExcludeFromCodeCoverage]
        private readonly struct OperandTypeDetails(bool lhsNullable, bool rhsNullable, Type lhsType0, Type rhsType0)
        {
            public readonly bool LhsNullable { get; } = lhsNullable;
            public readonly bool RhsNullable { get; } = rhsNullable;
            public readonly Type LhsType0 { get; } = lhsType0;
            public readonly Type RhsType0 { get; } = rhsType0;
        }

        internal static readonly MethodInfo ObjectEquality = typeof(DefaultOperators).GetMethod(nameof(DefaultOperators.ObjectEquals));

        internal static MethodInfo MapOperatorToMethod(
            CodeBinaryOperatorType op,
            Type lhs,
            CodeExpression lhsExpression,
            Type rhs,
            CodeExpression rhsExpression,
            RuleValidation validator,
            out ValidationError error)
        {
            // determine what the method name should be

            string message = null;
            string methodName = GetOperatorName(op, ref message, out error);
            if (methodName == null)
                return null;
            OperatorGrouping group = GetOperatorGrouping(op, ref message, out error);
            if (group == OperatorGrouping.None)
                return null;

            // NOTE: types maybe NullLiteral, which signifies the constant "null"
            List<MethodInfo> candidates = [];
            bool lhsNullable = ConditionHelper.IsNullableValueType(lhs);
            bool rhsNullable = ConditionHelper.IsNullableValueType(rhs);
            Type lhsType0 = GetUnderlyingTypeIfNullableValueType(lhs, lhsNullable);
            Type rhsType0 = GetUnderlyingTypeIfNullableValueType(rhs, rhsNullable);

            // special cases for enums
            if (lhsType0.IsEnum)
            {
                MethodInfo methodInfoFronLhsEnum = GetMethodInfoFronLhsEnum(op, new OperandTypeDetails(lhsNullable, rhsNullable, lhsType0, rhsType0), lhs, rhs, rhsExpression, ref error);
                if (methodInfoFronLhsEnum != null)
                    return methodInfoFronLhsEnum;
            }
            else if (rhsType0.IsEnum)
            {
                MethodInfo methodInfoFromRhsEnum = GetMethodInfoFromRhsEnum(op, new OperandTypeDetails(lhsNullable, rhsNullable, lhsType0, rhsType0), lhs, lhsExpression, rhs, ref error);
                if (methodInfoFromRhsEnum != null)
                    return methodInfoFromRhsEnum;
            }

            // enum specific operations already handled, see if one side (or both) define operators
            AddOperatorOverloads(lhsType0, methodName, lhs, rhs, candidates);
            AddOperatorOverloads(rhsType0, methodName, lhs, rhs, candidates);

            if (CanBeLiftedToNull(lhs, rhs, lhsNullable, rhsNullable))
            {
                // need to add in lifted methods
                AddLiftedOperators(lhsType0, methodName, group, lhsType0, rhsType0, candidates);
                AddLiftedOperators(rhsType0, methodName, group, lhsType0, rhsType0, candidates);
            }

            if (candidates.Count == 0)
            {
                // no overrides, so get the default list
                methodName = methodName.Substring(3);       // strip off the op_
                SetCandidatesFromImplicitConversion(lhs, rhs, methodName, candidates);

                SetCandidatesFromAssignableReferenceTypes(lhs, rhs, methodName, candidates);
                if (CanBeLiftedToNull(lhs, rhs, lhsNullable, rhsNullable))
                    SetCandidatesFromLeftingMethodsToNull(methodName, group, candidates, lhsType0, rhsType0);
            }
            if (candidates.Count == 1)
            {
                // only 1, so it is it
                error = null;
                return candidates[0];
            }
            else if (candidates.Count == 0)
            {
                SetNoCandidatesFoundError(op, lhs, rhs, out error, out message, group);
                return null;
            }
            else
            {
                return GetBestFitFromCandidates(op, lhs, rhs, validator, out error, ref message, candidates);
            }

            static void SetCandidatesFromImplicitConversion(Type lhs, Type rhs, string methodName, List<MethodInfo> candidates)
            {
                foreach (MethodInfo mi
                                    in typeof(DefaultOperators).GetMethods()
                                    .Where(m => m.Name == methodName))
                {
                    ParameterInfo[] parameters = mi.GetParameters();
                    Type parm1 = parameters[0].ParameterType;
                    Type parm2 = parameters[1].ParameterType;
                    if (RuleValidation.ImplicitConversion(lhs, parm1) &&
                        RuleValidation.ImplicitConversion(rhs, parm2))
                    {
                        candidates.Add(mi);
                    }
                }
            }

            static void SetCandidatesFromAssignableReferenceTypes(Type lhs, Type rhs, string methodName, List<MethodInfo> candidates)
            {
                // if no candidates and ==, can we use object == object?
                if ((candidates.Count == 0)
                    && ("Equality" == methodName)
                    && (!lhs.IsValueType)
                    && (!rhs.IsValueType)
                    && ((lhs == typeof(NullLiteral)) || (rhs == typeof(NullLiteral)) ||
                        (lhs.IsAssignableFrom(rhs)) || (rhs.IsAssignableFrom(lhs))))
                {
                    // C# 7.9.6
                    // references must be compatible
                    // no boxing
                    // value types can't be compared
                    // they are not classes, so references need to be compatible
                    // also check for null (which is NullLiteral type) -- null is compatible with any object type
                    candidates.Add(ObjectEquality);
                }
            }

            static void SetCandidatesFromLeftingMethodsToNull(string methodName, OperatorGrouping group, List<MethodInfo> candidates, Type lhsType0, Type rhsType0)
            {
                // if no candidates and nullable, add lifted operators
                if (candidates.Count == 0)
                {
                    foreach (MethodInfo mi
                        in typeof(DefaultOperators).GetMethods()
                        .Where(m => m.Name == methodName))
                    {
                        ParameterInfo[] parameters = mi.GetParameters();
                        MethodInfo liftedMethod = EvaluateLiftedMethod(mi, parameters, group, lhsType0, rhsType0);
                        if (liftedMethod != null)
                            candidates.Add(liftedMethod);
                    }
                }
            }

            static bool CanBeLiftedToNull(Type lhs, Type rhs, bool lhsNullable, bool rhsNullable)
            {
                return lhsNullable || rhsNullable || (lhs == typeof(NullLiteral)) || (rhs == typeof(NullLiteral));
            }

            static OperatorGrouping GetOperatorGrouping(CodeBinaryOperatorType op, ref string message, out ValidationError error)
            {
                error = null;
                switch (op)
                {
                    case CodeBinaryOperatorType.ValueEquality:
                        return OperatorGrouping.Equality;
                    case CodeBinaryOperatorType.GreaterThan:
                    case CodeBinaryOperatorType.GreaterThanOrEqual:
                    case CodeBinaryOperatorType.LessThan:
                    case CodeBinaryOperatorType.LessThanOrEqual:
                        return OperatorGrouping.Relational;
                    case CodeBinaryOperatorType.Add:
                    case CodeBinaryOperatorType.Subtract:
                    case CodeBinaryOperatorType.Multiply:
                    case CodeBinaryOperatorType.Divide:
                    case CodeBinaryOperatorType.Modulus:
                    case CodeBinaryOperatorType.BitwiseAnd:
                    case CodeBinaryOperatorType.BitwiseOr:
                        return OperatorGrouping.Arithmetic;
                    default:
                        Debug.Assert(false, "Operator " + op + " not implemented");
                        message = string.Format(CultureInfo.CurrentCulture, Messages.BinaryOpNotSupported, op);
                        error = new ValidationError(message, ErrorNumbers.Error_CodeExpressionNotHandled);
                        return OperatorGrouping.None;
                }
            }

            static string GetOperatorName(CodeBinaryOperatorType op, ref string message, out ValidationError error)
            {
                error = null;
                switch (op)
                {
                    case CodeBinaryOperatorType.ValueEquality:
                        return "op_Equality";
                    case CodeBinaryOperatorType.GreaterThan:
                        return "op_GreaterThan";
                    case CodeBinaryOperatorType.GreaterThanOrEqual:
                        return "op_GreaterThanOrEqual";
                    case CodeBinaryOperatorType.LessThan:
                        return "op_LessThan";
                    case CodeBinaryOperatorType.LessThanOrEqual:
                        return "op_LessThanOrEqual";
                    case CodeBinaryOperatorType.Add:
                        return "op_Addition";
                    case CodeBinaryOperatorType.Subtract:
                        return "op_Subtraction";
                    case CodeBinaryOperatorType.Multiply:
                        return "op_Multiply";
                    case CodeBinaryOperatorType.Divide:
                        return "op_Division";
                    case CodeBinaryOperatorType.Modulus:
                        return "op_Modulus";
                    case CodeBinaryOperatorType.BitwiseAnd:
                        return "op_BitwiseAnd";
                    case CodeBinaryOperatorType.BitwiseOr:
                        return "op_BitwiseOr";
                    default:
                        Debug.Assert(false, "Operator " + op + " not implemented");
                        message = string.Format(CultureInfo.CurrentCulture, Messages.BinaryOpNotSupported, op);
                        error = new ValidationError(message, ErrorNumbers.Error_CodeExpressionNotHandled);
                        return null;
                }
            }

            static MethodInfo GetBestFitFromCandidates(CodeBinaryOperatorType op, Type lhs, Type rhs, RuleValidation validator, out ValidationError error, ref string message, List<MethodInfo> candidates)
            {
                // more than 1, so pick the best one
                MethodInfo bestFit = validator.FindBestCandidate(null, candidates, lhs, rhs);
                if (bestFit != null)
                {
                    error = null;
                    return bestFit;
                }
                // must be ambiguous. Since there are at least 2 choices, show only the first 2
                message = string.Format(CultureInfo.CurrentCulture,
                    Messages.AmbiguousOperator,
                    op,
                    RuleDecompiler.DecompileMethod(candidates[0]),
                    RuleDecompiler.DecompileMethod(candidates[1]));
                error = new ValidationError(message, ErrorNumbers.Error_OperandTypesIncompatible);
                return null;
            }

            static void SetNoCandidatesFoundError(CodeBinaryOperatorType op, Type lhs, Type rhs, out ValidationError error, out string message, OperatorGrouping group)
            {
                // nothing matched
                message = string.Format(CultureInfo.CurrentCulture,
                    (group == OperatorGrouping.Arithmetic) ? Messages.ArithOpBadTypes : Messages.RelationalOpBadTypes,
                    op,
                    (lhs == typeof(NullLiteral)) ? Messages.NullValue : RuleDecompiler.DecompileType(lhs),
                    (rhs == typeof(NullLiteral)) ? Messages.NullValue : RuleDecompiler.DecompileType(rhs));
                error = new ValidationError(message, ErrorNumbers.Error_OperandTypesIncompatible);
            }

            static MethodInfo GetMethodInfoFromRhsEnum(CodeBinaryOperatorType op, OperandTypeDetails operandTypeDetails, Type lhs, CodeExpression lhsExpression, Type rhs, ref ValidationError error)
            {
                // lhs != enum, so only 2 cases (U = underlying type of E):
                //    E = U + E
                //    E = U - E
                // comparisons are E == E, etc., so if the lhs is not an enum, too bad
                // although we need to check for 0 == E
                bool rhsNullable = operandTypeDetails.RhsNullable;
                Type lhsType0 = operandTypeDetails.LhsType0;
                Type rhsType0 = operandTypeDetails.RhsType0;
                Type underlyingType;
                switch (op)
                {
                    case CodeBinaryOperatorType.Add:
                        underlyingType = EnumHelper.GetUnderlyingType(rhsType0);
                        if ((underlyingType != null) &&
                            (RuleValidation.TypesAreAssignable(lhsType0, underlyingType, lhsExpression, out error)))
                        {
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                        break;

                    case CodeBinaryOperatorType.Subtract:
                        underlyingType = EnumHelper.GetUnderlyingType(rhsType0);
                        MethodInfo substractMethodInfo = GetSubtractMethodInfo(op, lhs, lhsExpression, rhs, ref error, lhsType0, underlyingType);
                        if (substractMethodInfo != null)
                            return substractMethodInfo;

                        break;

                    case CodeBinaryOperatorType.ValueEquality:
                    case CodeBinaryOperatorType.LessThan:
                    case CodeBinaryOperatorType.LessThanOrEqual:
                    case CodeBinaryOperatorType.GreaterThan:
                    case CodeBinaryOperatorType.GreaterThanOrEqual:
                        if (rhsNullable && (lhs == typeof(NullLiteral)))
                        {
                            // handle null op enum?
                            // treat the lhs as the same nullable enum type
                            error = null;
                            return new EnumOperationMethodInfo(rhs, op, rhs, false);
                        }
                        else if (DecimalIntegerLiteralZero(lhs, lhsExpression as CodePrimitiveExpression))
                        {
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, true);
                        }
                        break;
                }

                // can't do it, sorry
                // but check if there is a user-defined operator that works
                return null;

                static MethodInfo GetSubtractMethodInfo(CodeBinaryOperatorType op, Type lhs, CodeExpression lhsExpression, Type rhs, ref ValidationError error, Type lhsType0, Type underlyingType)
                {
                    if (underlyingType != null)
                    {
                        CodePrimitiveExpression primitive = lhsExpression as CodePrimitiveExpression;
                        if (DecimalIntegerLiteralZero(lhs, primitive))
                        {
                            // 0 - E, can convert 0 to E
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, true);
                        }
                        else if (RuleValidation.TypesAreAssignable(lhsType0, underlyingType, lhsExpression, out error))
                        {
                            // expression not passed to TypesAreAssignable, so not looking for constants (since 0 is all we care about)
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                    }

                    return null;
                }
            }

            static MethodInfo GetMethodInfoFronLhsEnum(CodeBinaryOperatorType op, OperandTypeDetails operandTypeDetails, Type lhs, Type rhs, CodeExpression rhsExpression, ref ValidationError error)
            {
                // only 3 cases (U = underlying type of E):
                //    E = E + U
                //    U = E - E
                //    E = E - U
                // plus the standard comparisons (E == E, E > E, etc.)
                // need to also allow E == 0
                bool lhsNullable = operandTypeDetails.LhsNullable;
                Type lhsType0 = operandTypeDetails.LhsType0;
                Type rhsType0 = operandTypeDetails.RhsType0;
                Type underlyingType;
                switch (op)
                {
                    case CodeBinaryOperatorType.Add:
                        underlyingType = EnumHelper.GetUnderlyingType(lhsType0);
                        if ((underlyingType != null) &&
                            (RuleValidation.TypesAreAssignable(rhsType0, underlyingType, rhsExpression, out error)))
                        {
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                        break;
                    case CodeBinaryOperatorType.Subtract:
                        underlyingType = EnumHelper.GetUnderlyingType(lhsType0);
                        MethodInfo substractMethodInfo = GetSubstractMethodInfo(op, lhs, rhs, rhsExpression, ref error, operandTypeDetails, underlyingType);
                        if (substractMethodInfo != null)
                            return substractMethodInfo;
                        break;
                    case CodeBinaryOperatorType.ValueEquality:
                    case CodeBinaryOperatorType.LessThan:
                    case CodeBinaryOperatorType.LessThanOrEqual:
                    case CodeBinaryOperatorType.GreaterThan:
                    case CodeBinaryOperatorType.GreaterThanOrEqual:
                        if (lhsType0 == rhsType0)
                        {
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                        else if (lhsNullable && (rhs == typeof(NullLiteral)))
                        {
                            // handle enum? op null
                            // treat the rhs as the same nullable enum
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, lhs, false);
                        }
                        else if (DecimalIntegerLiteralZero(rhs, rhsExpression as CodePrimitiveExpression))
                        {
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, true);
                        }
                        break;
                }
                // can't do it, sorry
                // but check if there is a user-defined operator that works
                return null;

                static MethodInfo GetSubstractMethodInfo(CodeBinaryOperatorType op, Type lhs, Type rhs, CodeExpression rhsExpression, ref ValidationError error, OperandTypeDetails operandTypeDetails, Type underlyingType)
                {
                    Type lhsType0 = operandTypeDetails.LhsType0;
                    Type rhsType0 = operandTypeDetails.RhsType0;
                    if (underlyingType != null)
                    {
                        if (lhsType0 == rhsType0)
                        {
                            // E - E
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                        else if (DecimalIntegerLiteralZero(rhs, rhsExpression as CodePrimitiveExpression))
                        {
                            // E - 0, can convert 0 to E
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, true);
                        }
                        else if (RuleValidation.TypesAreAssignable(rhsType0, underlyingType, rhsExpression, out error))
                        {
                            // expression not passed to TypesAreAssignable, so not looking for constants (since 0 is all we care about)
                            error = null;
                            return new EnumOperationMethodInfo(lhs, op, rhs, false);
                        }
                    }

                    return null;
                }
            }

            static Type GetUnderlyingTypeIfNullableValueType(Type lhs, bool lhsNullable)
            {
                return (lhsNullable) ? Nullable.GetUnderlyingType(lhs) : lhs;
            }
        }

        private static bool DecimalIntegerLiteralZero(Type type, CodePrimitiveExpression expression)
        {
            if (expression != null)
            {
                if (type == typeof(int))
                    return expression.Value.Equals(0);
                else if (type == typeof(uint))
                    return expression.Value.Equals(0U);
                else if (type == typeof(long))
                    return expression.Value.Equals(0L);
                else if (type == typeof(ulong))
                    return expression.Value.Equals(0UL);
            }
            return false;
        }

        private static void AddOperatorOverloads(Type type, string methodName, Type arg1, Type arg2, List<MethodInfo> candidates)
        {
            // append the list of methods that match the name specified
            int numAdded = 0;
            MethodInfo[] possible = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (MethodInfo mi in possible)
            {
                ParameterInfo[] parameters = mi.GetParameters();
                if ((mi.Name == methodName)
                    && (parameters.Length == 2)
                    && EvaluateMethod(parameters, arg1, arg2))
                {
                    ++numAdded;
                    if (!candidates.Contains(mi))
                        candidates.Add(mi);
                }
            }
            if ((numAdded > 0) || (type == typeof(object)))
                return;

            // no matches, check direct base class (if there is one)
            type = type.BaseType;
            if (type == null)
                return;

            possible = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (MethodInfo mi in possible)
            {
                ParameterInfo[] parameters = mi.GetParameters();
                if ((mi.Name == methodName)
                    && (parameters.Length == 2)
                    && EvaluateMethod(parameters, arg1, arg2)
                    && !candidates.Contains(mi))
                {
                    candidates.Add(mi);
                }
            }
        }

        private static void AddLiftedOperators(Type type, string methodName, OperatorGrouping group, Type arg1, Type arg2, List<MethodInfo> candidates)
        {
            // append the list of lifted methods that match the name specified
            int numAdded = 0;
            MethodInfo[] possible = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (MethodInfo mi in possible)
            {
                ParameterInfo[] parameters = mi.GetParameters();
                if ((mi.Name == methodName) && (parameters.Length == 2))
                {
                    MethodInfo liftedMethod = EvaluateLiftedMethod(mi, parameters, group, arg1, arg2);
                    if (liftedMethod != null)
                    {
                        ++numAdded;
                        if (!candidates.Contains(liftedMethod))
                            candidates.Add(liftedMethod);
                    }
                }
            }
            if ((numAdded > 0) || (type == typeof(object)))
                return;

            // no matches, check direct base class (if there is one)
            type = type.BaseType;
            if (type == null)
                return;

            CheckBaseClassForCandidates(type, methodName, group, arg1, arg2, candidates);

            static void CheckBaseClassForCandidates(Type type, string methodName, OperatorGrouping group, Type arg1, Type arg2, List<MethodInfo> candidates)
            {
                MethodInfo[] possible = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
                foreach (MethodInfo mi in possible)
                {
                    ParameterInfo[] parameters = mi.GetParameters();
                    if ((mi.Name == methodName) && (parameters.Length == 2))
                    {
                        MethodInfo liftedMethod = EvaluateLiftedMethod(mi, parameters, group, arg1, arg2);
                        if ((liftedMethod != null) && !candidates.Contains(liftedMethod))
                            candidates.Add(liftedMethod);
                    }
                }
            }
        }

        private static bool EvaluateMethod(ParameterInfo[] parameters, Type arg1, Type arg2)
        {
            Type parm1 = parameters[0].ParameterType;
            Type parm2 = parameters[1].ParameterType;
            return (RuleValidation.ImplicitConversion(arg1, parm1) &&
                    RuleValidation.ImplicitConversion(arg2, parm2));
        }

        private static MethodInfo EvaluateLiftedMethod(MethodInfo mi, ParameterInfo[] parameters, OperatorGrouping group, Type arg1, Type arg2)
        {
            Type parm1 = parameters[0].ParameterType;
            Type parm2 = parameters[1].ParameterType;
            if (!ConditionHelper.IsNonNullableValueType(parm1) || !ConditionHelper.IsNonNullableValueType(parm2))
                return null;

            // lift the parameters for testing conversions, if possible
            parm1 = typeof(Nullable<>).MakeGenericType(parm1);
            parm2 = typeof(Nullable<>).MakeGenericType(parm2);
            switch (group)
            {
                case OperatorGrouping.Equality:     // for == !=
                    if (mi.ReturnType == typeof(bool) &&
                        RuleValidation.ImplicitConversion(arg1, parm1) &&
                        RuleValidation.ImplicitConversion(arg2, parm2))
                    {
                        return new LiftedEqualityOperatorMethodInfo(mi);
                    }
                    break;
                case OperatorGrouping.Relational:       // for < > <= >=
                    if (mi.ReturnType == typeof(bool) &&
                        RuleValidation.ImplicitConversion(arg1, parm1) &&
                        RuleValidation.ImplicitConversion(arg2, parm2))
                    {
                        return new LiftedRelationalOperatorMethodInfo(mi);
                    }
                    break;
                case OperatorGrouping.Arithmetic:       // for + - * / % & ^
                    if (ConditionHelper.IsNonNullableValueType(mi.ReturnType) &&
                        RuleValidation.ImplicitConversion(arg1, parm1) &&
                        RuleValidation.ImplicitConversion(arg2, parm2))
                    {
                        return new LiftedArithmeticOperatorMethodInfo(mi);
                    }
                    break;
            }
            return null;
        }
        #endregion

        #region Value Type Dispatch Methods

        /// <summary>
        /// Relational equal operator
        /// Value-equality if we can do it, otherwise reference-equality
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal abstract bool Equal(Literal rhs);
        internal virtual bool Equal(byte literalValue)
        {
            return false;
        }
        internal virtual bool Equal(sbyte literalValue)
        {
            return false;
        }
        internal virtual bool Equal(short literalValue)
        {
            return false;
        }
        internal virtual bool Equal(int literalValue)
        {
            return false;
        }
        internal virtual bool Equal(long literalValue)
        {
            return false;
        }
        internal virtual bool Equal(ushort literalValue)
        {
            return false;
        }
        internal virtual bool Equal(uint literalValue)
        {
            return false;
        }
        internal virtual bool Equal(ulong literalValue)
        {
            return false;
        }
        internal virtual bool Equal(float literalValue)
        {
            return false;
        }
        internal virtual bool Equal(double literalValue)
        {
            return false;
        }
        internal virtual bool Equal(char literalValue)
        {
            return false;
        }
        internal virtual bool Equal(string literalValue)
        {
            return false;
        }
        internal virtual bool Equal(decimal literalValue)
        {
            return false;
        }
        internal virtual bool Equal(bool literalValue)
        {
            return false;
        }

        /// <summary>
        /// Relational less than operator
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal abstract bool LessThan(Literal rhs);
        internal virtual bool LessThan()
        {
            return false;
        }
        internal virtual bool LessThan(byte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(char literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(sbyte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(short literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(int literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(long literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(ushort literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(uint literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(ulong literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(float literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(double literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(string literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(decimal literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }
        internal virtual bool LessThan(bool literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThan, m_type);
        }

        /// <summary>
        /// Relational greater than operator
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal abstract bool GreaterThan(Literal rhs);
        internal virtual bool GreaterThan()
        {
            return false;
        }
        internal virtual bool GreaterThan(byte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(char literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(sbyte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(short literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(int literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(long literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(ushort literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(uint literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(ulong literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(float literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(double literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(string literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(decimal literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }
        internal virtual bool GreaterThan(bool literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThan, m_type);
        }

        /// <summary>
        /// Relational less than or equal to operator
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal abstract bool LessThanOrEqual(Literal rhs);
        internal virtual bool LessThanOrEqual()
        {
            return false;
        }
        internal virtual bool LessThanOrEqual(byte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(char literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(sbyte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(short literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(int literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(long literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(ushort literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(uint literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(ulong literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(float literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(double literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(string literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(decimal literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }
        internal virtual bool LessThanOrEqual(bool literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.LessThanOrEqual, m_type);
        }

        /// <summary>
        /// Relational greater than or equal to operator
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal abstract bool GreaterThanOrEqual(Literal rhs);
        internal virtual bool GreaterThanOrEqual()
        {
            return false;
        }
        internal virtual bool GreaterThanOrEqual(byte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(char literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(sbyte literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(short literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(int literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(long literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(ushort literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(uint literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(ulong literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(float literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(double literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(string literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(decimal literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        internal virtual bool GreaterThanOrEqual(bool literalValue)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Messages.IncompatibleComparisonTypes, literalValue.GetType(), m_type);
            throw new RuleEvaluationIncompatibleTypesException(message, literalValue.GetType(), CodeBinaryOperatorType.GreaterThanOrEqual, m_type);
        }
        #endregion
    }
    #endregion

    #region Null Literal Class
    /// <summary>
    /// Represents an null literal
    /// </summary>
    public class NullLiteral : Literal
    {
        // NOTE (from MSDN page on IComparable.CompareTo Method): 
        // By definition, any object compares greater than a null reference
        // But C# (24.3.1) doesn't -- if either side is null, result is false

        public NullLiteral(Type type)
        {
            m_type = type;
        }

        internal override object Value
        {
            get { return null; }
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Value == null;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan();
        }
        internal override bool LessThan(byte literalValue)
        {
            return false;
        }
        internal override bool LessThan(char literalValue)
        {
            return false;
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return false;
        }
        internal override bool LessThan(short literalValue)
        {
            return false;
        }
        internal override bool LessThan(int literalValue)
        {
            return false;
        }
        internal override bool LessThan(long literalValue)
        {
            return false;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return false;
        }
        internal override bool LessThan(uint literalValue)
        {
            return false;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return false;
        }
        internal override bool LessThan(float literalValue)
        {
            return false;
        }
        internal override bool LessThan(double literalValue)
        {
            return false;
        }
        internal override bool LessThan(string literalValue)
        {
            // for strings, maintain compatibility with v1
            return true;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return false;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan();
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(string literalValue)
        {
            return false;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return false;
        }

        /// <summary>
        /// Relational less than or equal to operator
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual();
        }
        internal override bool LessThanOrEqual()
        {
            return (m_type == typeof(string));      // null == null for strings only
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return false;
        }
        internal override bool LessThanOrEqual(string literalValue)
        {
            // for strings, maintain compatibility with v1
            return true;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return false;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual();
        }
        internal override bool GreaterThanOrEqual()
        {
            return (m_type == typeof(string));      // null == null for strings only
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(string literalValue)
        {
            return false;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return false;
        }
    }
    #endregion

    #region Boolean Literal Class
    /// <summary>
    /// Represents a boolean literal
    /// </summary>
    internal class BoolLiteral : Literal
    {
        private readonly bool m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal BoolLiteral(bool literalValue)
        {
            m_value = literalValue;
            m_type = typeof(bool);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(bool literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
    }
    #endregion

    #region Byte Literal Class
    /// <summary>
    /// Represents a byte literal
    /// </summary>
    internal class ByteLiteral : Literal
    {
        private readonly byte m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal ByteLiteral(byte literalValue)
        {
            m_value = literalValue;
            m_type = typeof(byte);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }

        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region SByte Literal Class
    /// <summary>
    /// Represents a byte literal
    /// </summary>
    internal class SByteLiteral : Literal
    {
        private readonly sbyte m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal SByteLiteral(sbyte literalValue)
        {
            m_value = literalValue;
            m_type = typeof(sbyte);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value == literalValue);
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;

        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Char Literal Class
    /// <summary>
    /// Represents a byte literal
    /// </summary>
    internal class CharLiteral : Literal
    {
        private readonly char m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal CharLiteral(char literalValue)
        {
            m_value = literalValue;
            m_type = typeof(char);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Decimal Literal Class
    /// <summary>
    /// Represents a decimal literal
    /// </summary>
    internal class DecimalLiteral : Literal
    {
        private readonly decimal m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal DecimalLiteral(decimal literalValue)
        {
            m_value = literalValue;
            m_type = typeof(decimal);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Int16 Literal Class
    /// <summary>
    /// Represents an Int16 literal
    /// </summary>
    internal class ShortLiteral : Literal
    {
        private readonly short m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal ShortLiteral(short literalValue)
        {
            m_value = literalValue;
            m_type = typeof(short);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value == literalValue);
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Int32 Literal Class
    /// <summary>
    /// Represents an Int32 literal
    /// </summary>
    internal class IntLiteral : Literal
    {
        private readonly int m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal IntLiteral(int literalValue)
        {
            m_value = literalValue;
            m_type = typeof(int);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value == literalValue);
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return (m_value < 0) || ((ulong)m_value < literalValue);
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value > literalValue);
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return (m_value < 0) || ((ulong)m_value <= literalValue);
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value >= literalValue);
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Int64 Literal Class
    /// <summary>
    /// Represents an Int64 literal
    /// </summary>
    internal class LongLiteral : Literal
    {
        private readonly long m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal LongLiteral(long literalValue)
        {
            m_value = literalValue;
            m_type = typeof(long);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value == literalValue);
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return (m_value < 0) || ((ulong)m_value < literalValue);
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value > literalValue);
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return (m_value < 0) || ((ulong)m_value <= literalValue);
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return (m_value >= 0) && ((ulong)m_value >= literalValue);
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region UInt16 Literal Class
    /// <summary>
    /// Represents an UInt16 literal
    /// </summary>
    internal class UShortLiteral : Literal
    {
        private readonly ushort m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal UShortLiteral(ushort literalValue)
        {
            m_value = literalValue;
            m_type = typeof(ushort);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region UInt32 Literal Class
    /// <summary>
    /// Represents an UInt32 literal
    /// </summary>
    internal class UIntLiteral : Literal
    {
        private readonly uint m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal UIntLiteral(uint literalValue)
        {
            m_value = literalValue;
            m_type = typeof(uint);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(short literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(int literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(long literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region UInt64 Literal Class
    /// <summary>
    /// Represents an UInt64 literal
    /// </summary>
    internal class ULongLiteral : Literal
    {
        private readonly ulong m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal ULongLiteral(ulong literalValue)
        {
            m_value = literalValue;
            m_type = typeof(ulong);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(byte literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(sbyte literalValue)
        {
            return (literalValue >= 0) && (m_value == (ulong)literalValue);
        }
        internal override bool Equal(short literalValue)
        {
            return (literalValue >= 0) && (m_value == (ulong)literalValue);
        }
        internal override bool Equal(int literalValue)
        {
            return (literalValue >= 0) && (m_value == (ulong)literalValue);
        }
        internal override bool Equal(long literalValue)
        {
            return (literalValue >= 0) && (m_value == (ulong)literalValue);
        }
        internal override bool Equal(char literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ushort literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(uint literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(ulong literalValue)
        {
            return m_value == literalValue;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(decimal literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return (literalValue >= 0) && (m_value < (ulong)literalValue);
        }
        internal override bool LessThan(long literalValue)
        {
            return (literalValue >= 0) && (m_value < (ulong)literalValue);
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(decimal literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return (literalValue < 0) || (m_value > (ulong)literalValue);
        }
        internal override bool GreaterThan(long literalValue)
        {
            return (literalValue < 0) || (m_value > (ulong)literalValue);
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(decimal literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return (literalValue >= 0) && (m_value <= (ulong)literalValue);
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return (literalValue >= 0) && (m_value <= (ulong)literalValue);
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(decimal literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return (literalValue < 0) || (m_value >= (ulong)literalValue);
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return (literalValue < 0) || (m_value >= (ulong)literalValue);
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(decimal literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Double Literal Class
    /// <summary>
    /// Represents a double literal
    /// </summary>
    internal class DoubleLiteral : Literal
    {
        private readonly double m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal DoubleLiteral(double literalValue)
        {
            m_value = literalValue;
            m_type = typeof(double);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(byte literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(char literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(short literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(ushort literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(int literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(uint literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(long literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(ulong literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < double.Epsilon;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region Float Literal Class
    /// <summary>
    /// Represents a float literal
    /// </summary>
    internal class FloatLiteral : Literal
    {
        private readonly float m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal FloatLiteral(float literalValue)
        {
            m_value = literalValue;
            m_type = typeof(float);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(sbyte literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(byte literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(char literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(short literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(ushort literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(int literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(uint literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(long literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(ulong literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(float literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }
        internal override bool Equal(double literalValue)
        {
            return Math.Abs(m_value - literalValue) < float.Epsilon;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(sbyte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(byte literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(char literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(short literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ushort literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(int literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(uint literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(long literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(ulong literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(float literalValue)
        {
            return m_value < literalValue;
        }
        internal override bool LessThan(double literalValue)
        {
            return m_value < literalValue;
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan(sbyte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(byte literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(char literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(short literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ushort literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(int literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(uint literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(long literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(ulong literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(float literalValue)
        {
            return m_value > literalValue;
        }
        internal override bool GreaterThan(double literalValue)
        {
            return m_value > literalValue;
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(sbyte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(byte literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(short literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(char literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ushort literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(int literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(uint literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(long literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(ulong literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(float literalValue)
        {
            return m_value <= literalValue;
        }
        internal override bool LessThanOrEqual(double literalValue)
        {
            return m_value <= literalValue;
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual(sbyte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(byte literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(char literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(short literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ushort literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(int literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(uint literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(long literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(ulong literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(float literalValue)
        {
            return m_value >= literalValue;
        }
        internal override bool GreaterThanOrEqual(double literalValue)
        {
            return m_value >= literalValue;
        }
    }
    #endregion

    #region String Literal Class
    /// <summary>
    /// Represents a string literal
    /// </summary>
    internal class StringLiteral : Literal
    {
        private readonly string m_value;

        internal override object Value
        {
            get { return m_value; }
        }

        internal StringLiteral(string internalValue)
        {
            m_value = internalValue;
            m_type = typeof(string);
        }

        internal override bool Equal(Literal rhs)
        {
            return rhs.Equal(m_value);
        }
        internal override bool Equal(string literalValue)
        {
            return m_value == literalValue;
        }

        internal override bool LessThan(Literal rhs)
        {
            return rhs.GreaterThan(m_value);
        }
        internal override bool LessThan(string literalValue)
        {
            return 0 > string.Compare(m_value, literalValue, false, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal override bool GreaterThan(Literal rhs)
        {
            return rhs.LessThan(m_value);
        }
        internal override bool GreaterThan()
        {
            return true;
        }
        internal override bool GreaterThan(string literalValue)
        {
            return 0 < string.Compare(m_value, literalValue, false, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal override bool LessThanOrEqual(Literal rhs)
        {
            return rhs.GreaterThanOrEqual(m_value);
        }
        internal override bool LessThanOrEqual(string literalValue)
        {
            return 0 >= string.Compare(m_value, literalValue, false, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal override bool GreaterThanOrEqual(Literal rhs)
        {
            return rhs.LessThanOrEqual(m_value);
        }
        internal override bool GreaterThanOrEqual()
        {
            return true;
        }
        internal override bool GreaterThanOrEqual(string literalValue)
        {
            return 0 <= string.Compare(m_value, literalValue, false, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
    #endregion
}
