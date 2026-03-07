using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ThisExpressionTest
    {
        private readonly Type thisExpressionType;
        private readonly object thisExpressionInstance;

        public ThisExpressionTest()
        {
            // Get the internal ThisExpression type via reflection
            thisExpressionType = typeof(RuleValidation).Assembly.GetType("LogicBuilder.Workflow.Activities.Rules.ThisExpression")!;
            thisExpressionInstance = Activator.CreateInstance(thisExpressionType, true)!;
        }

        #region Validate Tests

        [Fact]
        public void Validate_WithIsWrittenFalse_ReturnsRuleExpressionInfo()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = new RuleValidation(typeof(TestClass));
            
            // Act
            var result = InvokeValidate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(TestClass), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithIsWrittenTrue_AddsValidationError()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = InvokeValidate(expression, validation, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            
            var error = validation.Errors[0];
            Assert.Contains("Cannot write to an expression of this type.", error.ErrorText);
            Assert.Equal(Common.ErrorNumbers.Error_InvalidAssignTarget, error.ErrorNumber);
            Assert.Same(expression, error.UserData[RuleUserDataKeys.ErrorObject]);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_ForWritesTrue_IsWrittenFalse_ReturnsEarly()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, true);

            // Act
            InvokeAnalyzeUsage(expression, analysis, true, false, null!);

            // Assert
            // No symbols should be added since we return early
            var symbols = GetAnalysisSymbols(analysis);
            Assert.Empty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_ForWritesFalse_IsReadFalse_ReturnsEarly()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);

            // Act
            InvokeAnalyzeUsage(expression, analysis, false, false, null!);

            // Assert
            // No symbols should be added since we return early
            var symbols = GetAnalysisSymbols(analysis);
            Assert.Empty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_WithWildcardNotAtEnd_ThrowsNotSupportedException()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            var qualifier = CreateRulePathQualifier("*", CreateRulePathQualifier("Property2", null!));

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => 
                InvokeAnalyzeUsage(expression, analysis, true, false, qualifier));
            Assert.IsType<NotSupportedException>(exception.InnerException);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_ReturnsThisLiteralResult()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var testObject = new TestClass { Value = 42 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);

            // Act
            var result = InvokeEvaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Same(testObject, result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_AppendsThisKeyword()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var stringBuilder = new StringBuilder();
            var parentExpression = new CodeBinaryOperatorExpression();

            // Act
            InvokeDecompile(expression, stringBuilder, parentExpression);

            // Assert
            Assert.Equal("this", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_WithNullParent_AppendsThisKeyword()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var stringBuilder = new StringBuilder();

            // Act
            InvokeDecompile(expression, stringBuilder, null!);

            // Assert
            Assert.Equal("this", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_WithExistingContent_AppendsThisKeyword()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var stringBuilder = new StringBuilder("prefix.");

            // Act
            InvokeDecompile(expression, stringBuilder, null!);

            // Assert
            Assert.Equal("prefix.this", stringBuilder.ToString());
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_ReturnsNewCodeThisReferenceExpression()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act
            var result = InvokeClone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeThisReferenceExpression>(result);
            Assert.NotSame(expression, result);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithTwoThisExpressions_ReturnsTrue()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var comperand = new CodeThisReferenceExpression();

            // Act
            var result = InvokeMatch(expression, comperand);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithSameExpression_ReturnsTrue()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act
            var result = InvokeMatch(expression, expression);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Helper Methods

        private RuleExpressionInfo InvokeValidate(CodeExpression expression, RuleValidation validation, bool isWritten)
        {
            var method = thisExpressionType.GetMethod("Validate", BindingFlags.Instance | BindingFlags.Public);
            return (RuleExpressionInfo)method!.Invoke(thisExpressionInstance, [expression, validation, isWritten])!;
        }

        private void InvokeAnalyzeUsage(CodeExpression expression, RuleAnalysis analysis, bool isRead, bool isWritten, RulePathQualifier qualifier)
        {
            var method = thisExpressionType.GetMethod("AnalyzeUsage", BindingFlags.Instance | BindingFlags.Public);
            method!.Invoke(thisExpressionInstance, [expression, analysis, isRead, isWritten, qualifier]);
        }

        private IRuleExpressionResult InvokeEvaluate(CodeExpression expression, RuleExecution execution)
        {
            var method = thisExpressionType.GetMethod("Evaluate", BindingFlags.Instance | BindingFlags.Public);
            return (IRuleExpressionResult)method!.Invoke(thisExpressionInstance, [expression, execution])!;
        }

        private void InvokeDecompile(CodeExpression expression, StringBuilder stringBuilder, CodeExpression parentExpression)
        {
            var method = thisExpressionType.GetMethod("Decompile", BindingFlags.Instance | BindingFlags.Public);
            method!.Invoke(thisExpressionInstance, [expression, stringBuilder, parentExpression]);
        }

        private CodeExpression InvokeClone(CodeExpression expression)
        {
            var method = thisExpressionType.GetMethod("Clone", BindingFlags.Instance | BindingFlags.Public);
            return (CodeExpression)method!.Invoke(thisExpressionInstance, [expression])!;
        }

        private bool InvokeMatch(CodeExpression expression, CodeExpression comperand)
        {
            var method = thisExpressionType.GetMethod("Match", BindingFlags.Instance | BindingFlags.Public);
            return (bool)method!.Invoke(thisExpressionInstance, [expression, comperand])!;
        }

        private static RulePathQualifier CreateRulePathQualifier(string name, RulePathQualifier next)
        {
            var constructor = typeof(RulePathQualifier).GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                [typeof(string), typeof(RulePathQualifier)],
                null);
            return (RulePathQualifier)constructor!.Invoke([name, next]);
        }

        private static HashSet<string> GetAnalysisSymbols(RuleAnalysis analysis)
        {
            var field = typeof(RuleAnalysis).GetField("symbols", BindingFlags.Instance | BindingFlags.NonPublic);
            var symbols = field?.GetValue(analysis);
            
            if (symbols is IEnumerable<string> enumerable)
            {
                return [.. enumerable];
            }

            return [];
        }

        #endregion

        #region Test Helper Class

        private class TestClass
        {
            public int Value { get; set; }
            public string Property1 { get; set; } = string.Empty;
            public string Property2 { get; set; } = string.Empty;
        }

        #endregion
    }
}