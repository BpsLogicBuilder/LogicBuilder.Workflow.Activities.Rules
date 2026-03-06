using System;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleLiteralResultTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithIntValue_CreatesInstance()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(42);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Constructor_WithStringValue_CreatesInstance()
        {
            // Arrange
            var testString = "test value";

            // Act
            var result = new RuleLiteralResult(testString);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testString, result.Value);
        }

        [Fact]
        public void Constructor_WithNullValue_CreatesInstance()
        {
            // Act
            var result = new RuleLiteralResult(null);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Constructor_WithBooleanValue_CreatesInstance()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(true);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void Constructor_WithDoubleValue_CreatesInstance()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(3.14);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3.14, result.Value);
        }

        [Fact]
        public void Constructor_WithObjectValue_CreatesInstance()
        {
            // Arrange
            var testObject = new { Name = "Test", Value = 123 };

            // Act
            var result = new RuleLiteralResult(testObject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testObject, result.Value);
        }

        #endregion

        #region Value Getter Tests

        [Fact]
        public void ValueGetter_ReturnsCorrectValue_ForInt()
        {
            // Arrange
            var literal = 100;
            var result = new RuleLiteralResult(literal);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(literal, value);
        }

        [Fact]
        public void ValueGetter_ReturnsCorrectValue_ForString()
        {
            // Arrange
            var literal = "hello world";
            var result = new RuleLiteralResult(literal);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(literal, value);
        }

        [Fact]
        public void ValueGetter_ReturnsNull_WhenConstructedWithNull()
        {
            // Arrange
            var result = new RuleLiteralResult(null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Null(value);
        }

        [Fact]
        public void ValueGetter_ReturnsSameReference_ForReferenceType()
        {
            // Arrange
            var testObject = new object();
            var result = new RuleLiteralResult(testObject);

            // Act
            var value = result.Value;

            // Assert
            Assert.Same(testObject, value);
        }

        #endregion

        #region Value Setter Tests

        [Fact]
        public void ValueSetter_ThrowsInvalidOperationException()
        {
            // Arrange
            var result = new RuleLiteralResult(42);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.Value = 100);
        }

        [Fact]
        public void ValueSetter_ThrowsInvalidOperationException_WhenSettingNull()
        {
            // Arrange
            var result = new RuleLiteralResult("test");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.Value = null);
        }

        [Fact]
        public void ValueSetter_ThrowsInvalidOperationException_WhenSettingSameValue()
        {
            // Arrange
            var result = new RuleLiteralResult(42);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.Value = 42);
        }

        [Fact]
        public void ValueSetter_ExceptionMessage_ContainsExpectedText()
        {
            // Arrange
            var result = new RuleLiteralResult(42);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => result.Value = 100);

            // Assert
            Assert.NotNull(exception.Message);
            Assert.NotEmpty(exception.Message);
        }

        #endregion

        #region Type Inheritance Tests

        [Fact]
        public void RuleLiteralResult_InheritsFromRuleExpressionResult()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(42);

            // Assert
            Assert.IsType<IRuleExpressionResult>(result, exactMatch: false);
        }

        [Fact]
        public void RuleLiteralResult_CanBeTreatedAsRuleExpressionResult()
        {
            // Arrange
            IRuleExpressionResult expressionResult = new RuleLiteralResult("test");

            // Act
            var value = expressionResult.Value;

            // Assert
            Assert.Equal("test", value);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void Constructor_WithZero_CreatesInstance()
        {
            // Act
            var result = new RuleLiteralResult(0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void Constructor_WithEmptyString_CreatesInstance()
        {
            // Act
            var result = new RuleLiteralResult(string.Empty);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.Value);
        }

        [Fact]
        public void Constructor_WithMinIntValue_CreatesInstance()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(int.MinValue);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(int.MinValue, result.Value);
        }

        [Fact]
        public void Constructor_WithMaxIntValue_CreatesInstance()
        {
            // Arrange & Act
            var result = new RuleLiteralResult(int.MaxValue);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(int.MaxValue, result.Value);
        }

        #endregion
    }
}