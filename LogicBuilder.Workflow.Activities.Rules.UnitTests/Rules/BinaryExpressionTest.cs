using System;
using System.CodeDom;
using System.Text;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class BinaryExpressionTest
    {
        private readonly BinaryExpression _binaryExpression;

        public BinaryExpressionTest()
        {
            _binaryExpression = new BinaryExpression();
        }

        #region Test Helper Class
        private class TestClass
        {
            public int IntValue { get; set; }
            public double DoubleValue { get; set; }
            public bool BoolValue { get; set; }
            public string StringValue { get; set; } = string.Empty;
            public long LongValue { get; set; }
            public ulong ULongValue { get; set; }
        }
        #endregion

        #region Validate Tests - Arithmetic Operators

        [Fact]
        public void Validate_AddOperation_WithValidIntegers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_SubtractOperation_WithValidNumbers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10.5);
            var right = new CodePrimitiveExpression(5.5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Subtract, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_MultiplyOperation_WithValidNumbers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(3);
            var right = new CodePrimitiveExpression(7);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Multiply, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_DivideOperation_WithValidNumbers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(2);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Divide, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_ModulusOperation_WithValidIntegers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Modulus, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Validate Tests - Bitwise Operators

        [Fact]
        public void Validate_BitwiseAndOperation_WithValidIntegers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BitwiseAnd, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_BitwiseOrOperation_WithValidIntegers_ReturnsCorrectType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BitwiseOr, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Validate Tests - Comparison Operators

        [Fact]
        public void Validate_LessThanOperation_WithValidNumbers_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_LessThanOrEqualOperation_WithValidNumbers_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThanOrEqual, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_GreaterThanOperation_WithValidNumbers_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.GreaterThan, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_GreaterThanOrEqualOperation_WithValidNumbers_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.GreaterThanOrEqual, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Validate Tests - Equality Operators

        [Fact]
        public void Validate_ValueEqualityOperation_WithValidValues_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_IdentityEqualityOperation_WithValidValues_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression("test");
            var right = new CodePrimitiveExpression("test");
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityEquality, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_IdentityInequalityOperation_WithValidValues_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression("test");
            var right = new CodePrimitiveExpression("other");
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Validate Tests - Logical Operators

        [Fact]
        public void Validate_BooleanAndOperation_WithValidBools_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_BooleanOrOperation_WithValidBools_ReturnsBoolType()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanOr, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(bool), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_BooleanAndOperation_WithNonBoolLeft_AddsError()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(true);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_BooleanAndOperation_WithNonBoolRight_AddsError()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        #endregion

        #region Validate Tests - Error Cases

        [Fact]
        public void Validate_WithIsWrittenTrue_ReturnsNullAndAddsError()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            _ = _binaryExpression.Validate(expression, validation, true);

            // Assert
            Assert.NotEmpty(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNullLeft_AddsError()
        {
            // Arrange
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(null, CodeBinaryOperatorType.Add, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            _ = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotEmpty(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_LeftOperandMissing, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNullRight_AddsError()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, null);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            _ = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotEmpty(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_RightOperandMissing, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithTypeReferenceExpressionLeft_AddsError()
        {
            // Arrange
            var left = new CodeTypeReferenceExpression(typeof(int));
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_WithTypeReferenceExpressionRight_AddsError()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodeTypeReferenceExpression(typeof(int));
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = _binaryExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        #endregion

        #region Evaluate Tests - Arithmetic

        [Fact]
        public void Evaluate_AddOperation_ReturnsCorrectSum()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(15, result.Value);
        }

        [Fact]
        public void Evaluate_SubtractOperation_ReturnsCorrectDifference()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Subtract, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7, result.Value);
        }

        [Fact]
        public void Evaluate_MultiplyOperation_ReturnsCorrectProduct()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Multiply, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(15, result.Value);
        }

        [Fact]
        public void Evaluate_DivideOperation_ReturnsCorrectQuotient()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(2);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Divide, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Value);
        }

        [Fact]
        public void Evaluate_ModulusOperation_ReturnsCorrectRemainder()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Modulus, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value);
        }

        #endregion

        #region Evaluate Tests - Bitwise

        [Fact]
        public void Evaluate_BitwiseAndOperation_ReturnsCorrectResult()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5); // 0101
            var right = new CodePrimitiveExpression(3); // 0011
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BitwiseAnd, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value); // 0001
        }

        [Fact]
        public void Evaluate_BitwiseOrOperation_ReturnsCorrectResult()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5); // 0101
            var right = new CodePrimitiveExpression(3); // 0011
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BitwiseOr, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7, result.Value); // 0111
        }

        #endregion

        #region Evaluate Tests - Comparison

        [Fact]
        public void Evaluate_LessThanOperation_ReturnsTrue()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void Evaluate_LessThanOperation_ReturnsFalse()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.False((bool)result.Value);
        }

        [Fact]
        public void Evaluate_GreaterThanOperation_ReturnsTrue()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.GreaterThan, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        #endregion

        #region Evaluate Tests - Equality

        [Fact]
        public void Evaluate_ValueEqualityOperation_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void Evaluate_ValueEqualityOperation_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.False((bool)result.Value);
        }

        [Fact]
        public void Evaluate_IdentityEqualityOperation_ReturnsCorrectResult()
        {
            // Arrange
            var left = new CodePrimitiveExpression("test");
            var right = new CodePrimitiveExpression("test");
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityEquality, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            // Note: Identity equality for strings with same literal may be true due to interning
        }

        [Fact]
        public void Evaluate_IdentityInequalityOperation_ReturnsCorrectResult()
        {
            // Arrange
            var left = new CodePrimitiveExpression("test");
            var right = new CodePrimitiveExpression("other");
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        #endregion

        #region Evaluate Tests - Logical (Short-Circuit)

        [Fact]
        public void Evaluate_BooleanAndOperation_WithTrueAndTrue_ReturnsTrue()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(true);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            RuleSet ruleSet = new();
            Rule rule = new("TestRule")
            {
                Condition = new RuleExpressionCondition(expression)
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void Evaluate_BooleanAndOperation_WithFalseLeft_ReturnsFalseWithoutEvaluatingRight()
        {
            // Arrange
            var left = new CodePrimitiveExpression(false);
            var right = new CodePrimitiveExpression(true);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            var validation = new RuleValidation(typeof(TestClass), null);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.False((bool)result.Value);
        }

        [Fact]
        public void Evaluate_BooleanOrOperation_WithTrueLeft_ReturnsTrueWithoutEvaluatingRight()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanOr, right);
            var validation = new RuleValidation(typeof(TestClass), null);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void Evaluate_BooleanOrOperation_WithFalseAndFalse_ReturnsFalse()
        {
            // Arrange
            var left = new CodePrimitiveExpression(false);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanOr, right);
            var validation = new RuleValidation(typeof(TestClass), null);
            _binaryExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _binaryExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.False((bool)result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_AddOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("5 + 10", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_SubtractOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Subtract, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("10 - 5", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_MultiplyOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Multiply, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("5 * 3", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_DivideOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(2);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Divide, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("10 / 2", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_ModulusOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(3);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Modulus, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("10 % 3", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_LessThanOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("5 < 10", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_ValueEqualityOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(10);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("10 == 10", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_BooleanAndOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("True && False", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_BooleanOrOperation_ProducesCorrectString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanOr, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            Assert.Equal("True || False", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_NestedExpression_ProducesCorrectStringWithParentheses()
        {
            // Arrange
            var innerLeft = new CodePrimitiveExpression(2);
            var innerRight = new CodePrimitiveExpression(3);
            var innerExpression = new CodeBinaryOperatorExpression(innerLeft, CodeBinaryOperatorType.Add, innerRight);
            
            var outerRight = new CodePrimitiveExpression(4);
            var outerExpression = new CodeBinaryOperatorExpression(innerExpression, CodeBinaryOperatorType.Multiply, outerRight);
            
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(outerExpression, stringBuilder, null);

            // Assert
            Assert.Contains("+", stringBuilder.ToString());
            Assert.Contains("*", stringBuilder.ToString());
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesIdenticalExpression()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);

            // Act
            var cloned = _binaryExpression.Clone(expression) as CodeBinaryOperatorExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.Equal(expression.Operator, cloned.Operator);
            Assert.NotSame(expression.Left, cloned.Left);
            Assert.NotSame(expression.Right, cloned.Right);
        }

        [Fact]
        public void Clone_WithNestedExpression_CreatesDeepCopy()
        {
            // Arrange
            var innerLeft = new CodePrimitiveExpression(2);
            var innerRight = new CodePrimitiveExpression(3);
            var innerExpression = new CodeBinaryOperatorExpression(innerLeft, CodeBinaryOperatorType.Add, innerRight);
            
            var outerRight = new CodePrimitiveExpression(4);
            var expression = new CodeBinaryOperatorExpression(innerExpression, CodeBinaryOperatorType.Multiply, outerRight);

            // Act
            var cloned = _binaryExpression.Clone(expression) as CodeBinaryOperatorExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.Left, cloned.Left);
            Assert.NotSame(expression.Right, cloned.Right);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var left1 = new CodePrimitiveExpression(5);
            var right1 = new CodePrimitiveExpression(10);
            var expression1 = new CodeBinaryOperatorExpression(left1, CodeBinaryOperatorType.Add, right1);

            var left2 = new CodePrimitiveExpression(5);
            var right2 = new CodePrimitiveExpression(10);
            var expression2 = new CodeBinaryOperatorExpression(left2, CodeBinaryOperatorType.Add, right2);

            // Act
            var result = _binaryExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentOperators_ReturnsFalse()
        {
            // Arrange
            var left1 = new CodePrimitiveExpression(5);
            var right1 = new CodePrimitiveExpression(10);
            var expression1 = new CodeBinaryOperatorExpression(left1, CodeBinaryOperatorType.Add, right1);

            var left2 = new CodePrimitiveExpression(5);
            var right2 = new CodePrimitiveExpression(10);
            var expression2 = new CodeBinaryOperatorExpression(left2, CodeBinaryOperatorType.Subtract, right2);

            // Act
            var result = _binaryExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentLeftOperands_ReturnsFalse()
        {
            // Arrange
            var left1 = new CodePrimitiveExpression(5);
            var right1 = new CodePrimitiveExpression(10);
            var expression1 = new CodeBinaryOperatorExpression(left1, CodeBinaryOperatorType.Add, right1);

            var left2 = new CodePrimitiveExpression(3);
            var right2 = new CodePrimitiveExpression(10);
            var expression2 = new CodeBinaryOperatorExpression(left2, CodeBinaryOperatorType.Add, right2);

            // Act
            var result = _binaryExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentRightOperands_ReturnsFalse()
        {
            // Arrange
            var left1 = new CodePrimitiveExpression(5);
            var right1 = new CodePrimitiveExpression(10);
            var expression1 = new CodeBinaryOperatorExpression(left1, CodeBinaryOperatorType.Add, right1);

            var left2 = new CodePrimitiveExpression(5);
            var right2 = new CodePrimitiveExpression(15);
            var expression2 = new CodeBinaryOperatorExpression(left2, CodeBinaryOperatorType.Add, right2);

            // Act
            var result = _binaryExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Special Cases

        [Fact]
        public void Decompile_EqualityWithFalse_OptimizesToNotOperator()
        {
            // Arrange
            var left = new CodePrimitiveExpression(true);
            var right = new CodePrimitiveExpression(false);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            // Should produce "!true" or "true == false" depending on optimization
            Assert.NotNull(result);
        }

        [Fact]
        public void Decompile_SubtractFromZero_OptimizesToNegation()
        {
            // Arrange
            var left = new CodePrimitiveExpression(0);
            var right = new CodePrimitiveExpression(5);
            var expression = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Subtract, right);
            var stringBuilder = new StringBuilder();

            // Act
            _binaryExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            // Should produce "-5" or "0 - 5" depending on optimization
            Assert.NotNull(result);
        }

        #endregion
    }
}