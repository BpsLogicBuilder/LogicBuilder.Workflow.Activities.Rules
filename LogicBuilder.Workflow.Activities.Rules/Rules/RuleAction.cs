using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using LogicBuilder.Workflow.Activities.Common;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;

namespace LogicBuilder.Workflow.Activities.Rules
{
    public interface IRuleAction
    {
        bool Validate(RuleValidation validator);
        void Execute(RuleExecution context);
        ICollection<string> GetSideEffects(RuleValidation validation);
        IRuleAction Clone();
    }

    [Serializable]
    public class RuleHaltAction : IRuleAction
    {
        public bool Validate(RuleValidation validator)
        {
            // Trivial... nothing to validate.
            return true;
        }

        public void Execute(RuleExecution context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            context.Halted = true;
        }

        public ICollection<string> GetSideEffects(RuleValidation validation)
        {
            return [];
        }

        public IRuleAction Clone()
        {
            return (IRuleAction)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "Halt";
        }

        public override bool Equals(object obj)
        {
            return (obj is RuleHaltAction);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }


    [Serializable]
    public class RuleUpdateAction : IRuleAction
    {
        public RuleUpdateAction(string path)
        {
            this.Path = path;
        }

        public RuleUpdateAction()
        {
        }

        public string Path { get; set; }

        public bool Validate(RuleValidation validator)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");

            bool success = true;

            if (Path == null)
            {
                ValidationError error = new(Messages.NullUpdate, ErrorNumbers.Error_ParameterNotSet);
                error.UserData[RuleUserDataKeys.ErrorObject] = this;
                validator.AddError(error);
                success = false;
            }

            // now make sure that the path is valid
            string[] parts = Path?.Split('/') ?? [];
            if (parts.Length > 0 && parts[0] == "this")
            {
                Type currentType = validator.ThisType;
                ValidatePathSegments(validator, ref success, parts, ref currentType);
            }
            else
            {
                ValidationError error = new(Messages.UpdateNotThis, ErrorNumbers.Error_InvalidUpdate);
                error.UserData[RuleUserDataKeys.ErrorObject] = this;
                validator.AddError(error);
                success = false;
            }

            return success;
        }

        private void ValidatePathSegments(RuleValidation validator, ref bool success, string[] parts, ref Type currentType)
        {
            for (int i = 1; i < parts.Length; ++i)
            {
                if (parts[i] == "*")
                {
                    success = ValidateWildCards(validator, success, parts, i);
                    break;
                }
                else if (string.IsNullOrEmpty(parts[i]) && i == parts.Length - 1)
                {
                    // It's okay to end with a "/".
                    break;
                }

                while (currentType.IsArray)
                    currentType = currentType.GetElementType();

                BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;
                if (validator.AllowInternalMembers(currentType))
                    bindingFlags |= BindingFlags.NonPublic;//NOSONAR - used when the current type belongs to the same assembly as the root object.
                FieldInfo field = currentType.GetField(parts[i], bindingFlags);
                if (field != null)
                {
                    currentType = field.FieldType;
                    continue;
                }

                PropertyInfo property = currentType.GetProperty(parts[i], bindingFlags);
                if (property != null)
                {
                    currentType = property.PropertyType;
                }
                else
                {
                    string message = string.Format(CultureInfo.CurrentCulture, Messages.UpdateUnknownFieldOrProperty, parts[i]);
                    ValidationError error = new(message, ErrorNumbers.Error_InvalidUpdate);
                    error.UserData[RuleUserDataKeys.ErrorObject] = this;
                    validator.AddError(error);
                    success = false;
                    break;
                }
            }
        }

        private bool ValidateWildCards(RuleValidation validator, bool success, string[] parts, int i)
        {
            if (i < parts.Length - 1)
            {
                // The "*" occurred in the middle of the path, which is a no-no.
                ValidationError error = new(Messages.InvalidWildCardInPathQualifier, ErrorNumbers.Error_InvalidWildCardInPathQualifier);
                error.UserData[RuleUserDataKeys.ErrorObject] = this;
                validator.AddError(error);
                success = false;
            }
            else
            {
                // It occurred at the end, which is okay.
            }

            return success;
        }

        public void Execute(RuleExecution context)
        {
            // This action has no execution behaviour.
        }

        public ICollection<string> GetSideEffects(RuleValidation validation)
        {
            return [this.Path];
        }

        public IRuleAction Clone()
        {
            return (IRuleAction)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "Update(\"" + this.Path + "\")";
        }

        public override bool Equals(object obj)
        {
#pragma warning disable 56506
            return ((obj is RuleUpdateAction other) && (string.Equals(this.Path, other.Path, StringComparison.Ordinal)));
#pragma warning restore 56506
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }

    [Serializable]
    public class RuleStatementAction : IRuleAction
    {
        public RuleStatementAction(CodeStatement codeDomStatement)
        {
            this.CodeDomStatement = codeDomStatement;
        }

        public RuleStatementAction(CodeExpression codeDomExpression)
        {
            this.CodeDomStatement = new CodeExpressionStatement(codeDomExpression);
        }

        public RuleStatementAction()
        {
        }

        public CodeStatement CodeDomStatement { get; set; }

        public bool Validate(RuleValidation validator)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");

            if (CodeDomStatement == null)
            {
                ValidationError error = new(Messages.NullStatement, ErrorNumbers.Error_ParameterNotSet);
                error.UserData[RuleUserDataKeys.ErrorObject] = this;
                validator.AddError(error);
                return false;
            }
            else
            {
                return CodeDomStatementWalker.Validate(validator, CodeDomStatement);
            }
        }

        public void Execute(RuleExecution context)
        {
            if (CodeDomStatement == null)
                throw new InvalidOperationException(Messages.NullStatement);
            CodeDomStatementWalker.Execute(context, CodeDomStatement);
        }

        public ICollection<string> GetSideEffects(RuleValidation validation)
        {
            RuleAnalysis analysis = new(validation, true);
            if (CodeDomStatement != null)
                CodeDomStatementWalker.AnalyzeUsage(analysis, CodeDomStatement);
            return analysis.GetSymbols();
        }

        public IRuleAction Clone()
        {
            RuleStatementAction newAction = (RuleStatementAction)this.MemberwiseClone();
            newAction.CodeDomStatement = CodeDomStatementWalker.Clone(CodeDomStatement);
            return newAction;
        }

        public override string ToString()
        {
            if (CodeDomStatement == null)
                return "";

            StringBuilder decompilation = new();
            CodeDomStatementWalker.Decompile(decompilation, CodeDomStatement);
            return decompilation.ToString();
        }

        public override bool Equals(object obj)
        {
#pragma warning disable 56506
            return ((obj is RuleStatementAction other) && (CodeDomStatementWalker.Match(CodeDomStatement, other.CodeDomStatement)));
#pragma warning restore 56506
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
