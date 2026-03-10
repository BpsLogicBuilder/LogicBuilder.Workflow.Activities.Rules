using System.CodeDom;
using System.Globalization;
using System.Text;
using LogicBuilder.Workflow.Activities.Common;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;

namespace LogicBuilder.Workflow.Activities.Rules
{
    internal interface IRuleCodeDomStatement
    {
        bool Validate(RuleValidation validation);
        void Execute(RuleExecution execution);
        void AnalyzeUsage(RuleAnalysis analysis);
        void Decompile(StringBuilder decompilation);
        bool Match(CodeStatement expression);
        CodeStatement Clone();
    }

    internal class ExpressionStatement : IRuleCodeDomStatement
    {
        private readonly CodeExpressionStatement exprStatement;

        private ExpressionStatement(CodeExpressionStatement exprStatement)
        {
            this.exprStatement = exprStatement;
        }

        internal static IRuleCodeDomStatement Create(CodeStatement statement)
        {
            return new ExpressionStatement((CodeExpressionStatement)statement);
        }

        public bool Validate(RuleValidation validation)
        {
            bool success = false;

            if (exprStatement.Expression == null)
            {
                ValidationError error = new(Messages.NullInvokeStatementExpression, ErrorNumbers.Error_ParameterNotSet);
                error.UserData[RuleUserDataKeys.ErrorObject] = exprStatement;
                validation.Errors.Add(error);
            }
            else if (exprStatement.Expression is CodeMethodInvokeExpression)
            {
                RuleExpressionInfo exprInfo = RuleExpressionWalker.Validate(validation, exprStatement.Expression, false);
                success = (exprInfo != null);
            }
            else
            {
                ValidationError error = new(Messages.InvokeNotHandled, ErrorNumbers.Error_CodeExpressionNotHandled);
                error.UserData[RuleUserDataKeys.ErrorObject] = exprStatement;
                validation.Errors.Add(error);
            }

            return success;
        }

        public void AnalyzeUsage(RuleAnalysis analysis)
        {
            RuleExpressionWalker.AnalyzeUsage(analysis, exprStatement.Expression, false, false, null);
        }

        public void Execute(RuleExecution execution)
        {
            RuleExpressionWalker.Evaluate(execution, exprStatement.Expression);
        }

        public void Decompile(StringBuilder decompilation)
        {
            if (exprStatement.Expression == null)
            {
                RuleEvaluationException exception = new(Messages.InvokeStatementNull);
                exception.Data[RuleUserDataKeys.ErrorObject] = exprStatement;
                throw exception;
            }

            RuleExpressionWalker.Decompile(decompilation, exprStatement.Expression, null);
        }

        public bool Match(CodeStatement expression)
        {
            return ((expression is CodeExpressionStatement comperandStatement)
                && RuleExpressionWalker.Match(exprStatement.Expression, comperandStatement.Expression));
        }

        public CodeStatement Clone()
        {
            CodeExpressionStatement newStatement = new()
            {
                Expression = RuleExpressionWalker.Clone(exprStatement.Expression)
            };
            return newStatement;
        }
    }

    internal class AssignmentStatement : IRuleCodeDomStatement
    {
        private readonly CodeAssignStatement assignStatement;

        private AssignmentStatement(CodeAssignStatement assignStatement)
        {
            this.assignStatement = assignStatement;
        }

        internal static IRuleCodeDomStatement Create(CodeStatement statement)
        {
            return new AssignmentStatement((CodeAssignStatement)statement);
        }

        public bool Validate(RuleValidation validation)
        {
            bool success = false;
            string message;
            RuleExpressionInfo lhsExprInfo = null;

            if (assignStatement.Left == null)
            {
                ValidationError error = new(Messages.NullAssignLeft, ErrorNumbers.Error_LeftOperandMissing);
                error.UserData[RuleUserDataKeys.ErrorObject] = assignStatement;
                validation.Errors.Add(error);
            }
            else
            {
                lhsExprInfo = validation.ExpressionInfo(assignStatement.Left);
                lhsExprInfo ??= RuleExpressionWalker.Validate(validation, assignStatement.Left, true);
            }

            RuleExpressionInfo rhsExprInfo = null;
            if (assignStatement.Right == null)
            {
                ValidationError error = new(Messages.NullAssignRight, ErrorNumbers.Error_RightOperandMissing);
                error.UserData[RuleUserDataKeys.ErrorObject] = assignStatement;
                validation.Errors.Add(error);
            }
            else
            {
                rhsExprInfo = RuleExpressionWalker.Validate(validation, assignStatement.Right, false);
            }

            if (lhsExprInfo == null || rhsExprInfo == null)
                return success;

            Type expressionType = rhsExprInfo.ExpressionType;
            Type assignmentType = lhsExprInfo.ExpressionType;

            if (assignmentType == typeof(NullLiteral))
            {
                // Can't assign to a null literal.
                ValidationError error = new(Messages.NullAssignLeft, ErrorNumbers.Error_LeftOperandInvalidType);
                error.UserData[RuleUserDataKeys.ErrorObject] = assignStatement;
                validation.Errors.Add(error);
                success = false;
            }
            else if (assignmentType == expressionType)
            {
                // Easy case, they're both the same type.
                success = true;
            }
            else
            {
                // The types aren't the same, but it still might be a legal assignment.
                if (!RuleValidation.TypesAreAssignable(expressionType, assignmentType, assignStatement.Right, out ValidationError error))
                {
                    if (error == null)
                    {
                        message = string.Format(CultureInfo.CurrentCulture, Messages.AssignNotAllowed, RuleDecompiler.DecompileType(expressionType), RuleDecompiler.DecompileType(assignmentType));
                        error = new ValidationError(message, ErrorNumbers.Error_OperandTypesIncompatible);
                    }
                    error.UserData[RuleUserDataKeys.ErrorObject] = assignStatement;
                    validation.Errors.Add(error);
                }
                else
                {
                    success = true;
                }
            }

            return success;
        }

        public void AnalyzeUsage(RuleAnalysis analysis)
        {
            // The left side of the assignment is modified.
            RuleExpressionWalker.AnalyzeUsage(analysis, assignStatement.Left, false, true, null);
            // The right side of the assignment is read.
            RuleExpressionWalker.AnalyzeUsage(analysis, assignStatement.Right, true, false, null);
        }

        public void Execute(RuleExecution execution)
        {
            Type leftType = execution.Validation.ExpressionInfo(assignStatement.Left).ExpressionType;
            Type rightType = execution.Validation.ExpressionInfo(assignStatement.Right).ExpressionType;

            IRuleExpressionResult leftResult = RuleExpressionWalker.Evaluate(execution, assignStatement.Left);
            IRuleExpressionResult rightResult = RuleExpressionWalker.Evaluate(execution, assignStatement.Right);
            leftResult.Value = Executor.AdjustType(rightType, rightResult.Value, leftType);
        }

        public void Decompile(StringBuilder decompilation)
        {
            if (assignStatement.Right == null)
            {
                RuleEvaluationException exception = new(Messages.AssignRightNull);
                exception.Data[RuleUserDataKeys.ErrorObject] = assignStatement;
                throw exception;
            }
            if (assignStatement.Left == null)
            {
                RuleEvaluationException exception = new(Messages.AssignLeftNull);
                exception.Data[RuleUserDataKeys.ErrorObject] = assignStatement;
                throw exception;
            }

            RuleExpressionWalker.Decompile(decompilation, assignStatement.Left, null);
            decompilation.Append(" = ");
            RuleExpressionWalker.Decompile(decompilation, assignStatement.Right, null);
        }

        public bool Match(CodeStatement expression)
        {
            return ((expression is CodeAssignStatement comperandStatement)
                && RuleExpressionWalker.Match(assignStatement.Left, comperandStatement.Left)
                && RuleExpressionWalker.Match(assignStatement.Right, comperandStatement.Right));
        }

        public CodeStatement Clone()
        {
            CodeAssignStatement newStatement = new()
            {
                Left = RuleExpressionWalker.Clone(assignStatement.Left),
                Right = RuleExpressionWalker.Clone(assignStatement.Right)
            };
            return newStatement;
        }
    }
}
