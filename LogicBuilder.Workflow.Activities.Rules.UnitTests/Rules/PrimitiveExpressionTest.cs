using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class PrimitiveExpressionTest
    {
        private readonly PrimitiveExpression _primitiveExpression;

        public PrimitiveExpressionTest()
        {
            _primitiveExpression = new PrimitiveExpression();
        }

        #region Validate Tests

        [Fact]
        public void Validate_WithIsWrittenTrue_ReturnsNullAndAddsError()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNonNullValue_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithNullValue_ReturnsNullLiteralType()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(null);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(NullLiteral), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithStringValue_ReturnsStringType()
        {
            // Arrange
            var expression = new CodePrimitiveExpression("test");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(string), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithBoolValue_ReturnsBoolType()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithDoubleValue_ReturnsDoubleType()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(3.14);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _primitiveExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(double), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_DoesNotThrowException()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, true);

            // Act & Assert - Should not throw
            _primitiveExpression.AnalyzeUsage(expression, analysis, true, false, null);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithIntValue_ReturnsCorrectValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var validation = new RuleValidation(typeof(TestClass));
            _primitiveExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _primitiveExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_WithStringValue_ReturnsCorrectValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression("test");
            var validation = new RuleValidation(typeof(TestClass));
            _primitiveExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _primitiveExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test", result.Value);
        }

        [Fact]
        public void Evaluate_WithNullValue_ReturnsNull()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(null);
            var validation = new RuleValidation(typeof(TestClass));
            _primitiveExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _primitiveExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Evaluate_WithBoolValue_ReturnsCorrectValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);
            var validation = new RuleValidation(typeof(TestClass));
            _primitiveExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _primitiveExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(true, result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithIntValue_ProducesCorrectString()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var sb = new StringBuilder();

            // Act
            _primitiveExpression.Decompile(expression, sb, null);

            // Assert
            Assert.Equal("42", sb.ToString());
        }

        [Fact]
        public void Decompile_WithStringValue_ProducesCorrectString()
        {
            // Arrange
            var expression = new CodePrimitiveExpression("test");
            var sb = new StringBuilder();

            // Act
            _primitiveExpression.Decompile(expression, sb, null);

            // Assert
            Assert.Equal("\"test\"", sb.ToString());
        }

        [Fact]
        public void Decompile_WithNullValue_ProducesCorrectString()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(null);
            var sb = new StringBuilder();

            // Act
            _primitiveExpression.Decompile(expression, sb, null);

            // Assert
            Assert.Equal("null", sb.ToString());
        }

        [Fact]
        public void Decompile_WithBoolTrueValue_ProducesCorrectString()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);
            var sb = new StringBuilder();

            // Act
            _primitiveExpression.Decompile(expression, sb, null);

            // Assert
            Assert.Equal("True", sb.ToString());
        }

        [Fact]
        public void Decompile_WithBoolFalseValue_ProducesCorrectString()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(false);
            var sb = new StringBuilder();

            // Act
            _primitiveExpression.Decompile(expression, sb, null);

            // Assert
            Assert.Equal("False", sb.ToString());
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithIntValue_CreatesNewInstanceWithSameValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);

            // Act
            var cloned = _primitiveExpression.Clone(expression) as CodePrimitiveExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.Equal(42, cloned.Value);
        }

        [Fact]
        public void Clone_WithStringValue_CreatesNewInstanceWithSameValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression("test");

            // Act
            var cloned = _primitiveExpression.Clone(expression) as CodePrimitiveExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.Equal("test", cloned.Value);
        }

        [Fact]
        public void Clone_WithNullValue_CreatesNewInstanceWithNull()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(null);

            // Act
            var cloned = _primitiveExpression.Clone(expression) as CodePrimitiveExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.Null(cloned.Value);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithEqualIntValues_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(42);
            var expression2 = new CodePrimitiveExpression(42);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentIntValues_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(42);
            var expression2 = new CodePrimitiveExpression(43);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithEqualStringValues_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression("test");
            var expression2 = new CodePrimitiveExpression("test");

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentStringValues_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression("test1");
            var expression2 = new CodePrimitiveExpression("test2");

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithBothNullValues_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(null);
            var expression2 = new CodePrimitiveExpression(null);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithOneNullValue_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(42);
            var expression2 = new CodePrimitiveExpression(null);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithEqualBoolValues_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(true);
            var expression2 = new CodePrimitiveExpression(true);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentBoolValues_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(true);
            var expression2 = new CodePrimitiveExpression(false);

            // Act
            var result = _primitiveExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; } = string.Empty;
            public bool BoolProperty { get; set; }
        }

        #endregion
    }
}