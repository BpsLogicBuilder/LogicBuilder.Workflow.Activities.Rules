using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ArithmeticLiteralResultTypeTest
    {
        #region Helper Classes

        private class TestClass
        {
            public int IntProperty { get; set; } //NOSONAR - Used for testing property reference expressions
            public string? StringProperty { get; set; } //NOSONAR - Used for testing property reference expressions

        }

        #endregion

        #region ResultType Tests for Bitwise Operations

        [Fact]
        public void ResultType_BitwiseAnd_WithLongAndInt_ReturnsLong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "IntProperty");
            var rightExpr = new CodePrimitiveExpression(5L);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseAnd,
                typeof(int),
                leftExpr,
                typeof(long),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(long), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseOr_WithUIntAndUShort_ReturnsUInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression((ushort)5);
            var rightExpr = new CodePrimitiveExpression(10U);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseOr,
                typeof(ushort),
                leftExpr,
                typeof(uint),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(uint), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseAnd_WithBool_ReturnsBool()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(true);
            var rightExpr = new CodePrimitiveExpression(false);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseAnd,
                typeof(bool),
                leftExpr,
                typeof(bool),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(bool), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseAnd_WithIncompatibleTypes_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(5);
            var rightExpr = new CodePrimitiveExpression(3.14);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseAnd,
                typeof(int),
                leftExpr,
                typeof(double),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        #endregion

        #region ResultType Tests for Arithmetic Operations

        [Fact]
        public void ResultType_Add_WithStringAndAnyType_ReturnsString()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression("test");
            var rightExpr = new CodePrimitiveExpression(42);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Add,
                typeof(string),
                leftExpr,
                typeof(int),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        [Fact]
        public void ResultType_Add_WithDecimalAndInt_ReturnsDecimal()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(10.5m);
            var rightExpr = new CodePrimitiveExpression(5);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Add,
                typeof(decimal),
                leftExpr,
                typeof(int),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(decimal), result.ExpressionType);
        }

        [Fact]
        public void ResultType_Subtract_WithDoubleAndFloat_ReturnsDouble()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(10.5);
            var rightExpr = new CodePrimitiveExpression(5.5f);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Subtract,
                typeof(double),
                leftExpr,
                typeof(float),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(double), result.ExpressionType);
        }

        [Fact]
        public void ResultType_Multiply_WithFloatAndInt_ReturnsFloat()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(2.5f);
            var rightExpr = new CodePrimitiveExpression(3);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Multiply,
                typeof(float),
                leftExpr,
                typeof(int),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(float), result.ExpressionType);
        }

        [Fact]
        public void ResultType_Divide_WithULongAndUInt_ReturnsULong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(100UL);
            var rightExpr = new CodePrimitiveExpression(10U);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Divide,
                typeof(ulong),
                leftExpr,
                typeof(uint),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(ulong), result.ExpressionType);
        }

        [Fact]
        public void ResultType_Modulus_WithLongAndUShort_ReturnsLong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(100L);
            var rightExpr = new CodePrimitiveExpression((ushort)7);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Modulus,
                typeof(long),
                leftExpr,
                typeof(ushort),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(long), result.ExpressionType);
        }

        [Fact]
        public void ResultType_ArithmeticOperation_WithIncompatibleTypes_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(5);
            var rightExpr = new CodePrimitiveExpression(true);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Add,
                typeof(int),
                leftExpr,
                typeof(bool),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        #endregion

        #region Edge Cases for Type Combinations

        [Fact]
        public void ResultType_Add_WithULongAndChar_ReturnsULong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(100UL);
            var rightExpr = new CodePrimitiveExpression('A');

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Add,
                typeof(ulong),
                leftExpr,
                typeof(char),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(ulong), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseAnd_WithULongAndUShort_ReturnsULong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(255UL);
            var rightExpr = new CodePrimitiveExpression((ushort)15);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseAnd,
                typeof(ulong),
                leftExpr,
                typeof(ushort),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(ulong), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseOr_WithIntAndUShort_ReturnsInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(8);
            var rightExpr = new CodePrimitiveExpression((ushort)4);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseOr,
                typeof(int),
                leftExpr,
                typeof(ushort),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        #endregion

        #region Nullable Type Tests

        [Fact]
        public void ResultType_Add_WithNullableInt_ReturnsNullableInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(null);
            var rightExpr = new CodePrimitiveExpression(5);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.Add,
                typeof(int?),
                leftExpr,
                typeof(int),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(int?), result.ExpressionType);
        }

        [Fact]
        public void ResultType_BitwiseAnd_WithNullableBool_ReturnsNullableBool()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var leftExpr = new CodePrimitiveExpression(null);
            var rightExpr = new CodePrimitiveExpression(true);

            // Act
            var result = ArithmeticLiteral.ResultType(
                CodeBinaryOperatorType.BitwiseAnd,
                typeof(bool?),
                leftExpr,
                typeof(bool),
                rightExpr,
                validation,
                out var error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(bool?), result.ExpressionType);
        }

        #endregion
    }
}
