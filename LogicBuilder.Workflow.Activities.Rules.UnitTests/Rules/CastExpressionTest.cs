using System;
using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class CastExpressionTest
    {
        #region Validate Tests

        [Fact]
        public void Validate_WrittenToExpression_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, true);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Contains(validation.Errors, e => e.ErrorNumber == Common.ErrorNumbers.Error_InvalidAssignTarget);
        }

        [Fact]
        public void Validate_NullExpression_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), null);

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_NullTargetType_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression
            {
                Expression = new CodePrimitiveExpression(5),
                TargetType = null
            };

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_ValidIntToLongCast_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(long), new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(long), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_ValidDoubleToIntCast_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_CastNullToReferenceType_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(string), new CodePrimitiveExpression(null));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(string), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_CastNullToValueType_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(null));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_CastNullToNullableType_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int?), new CodePrimitiveExpression(null));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int?), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_IncompatibleCast_ReturnsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(DateTime), new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_CastBetweenNumericTypes_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(byte), new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(byte), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_CastCharToInt_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression('A'));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_CastIntToChar_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(char), new CodePrimitiveExpression(65));

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(char), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_DownCastInheritanceHierarchy_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var thisRef = new CodeThisReferenceExpression();
            var castExpr = new CodeCastExpression(typeof(object), thisRef);

            // Act
            var result = RuleExpressionWalker.Validate(validation, castExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(object), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_NotValidated_ThrowsException()
        {
            // Arrange
            var execution = new RuleExecution(new RuleValidation(typeof(TestClass), null), new TestClass());
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5));

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                RuleExpressionWalker.Evaluate(execution, castExpr));
            Assert.Contains("not validated", exception.Message);
        }

        [Fact]
        public void Evaluate_IntToLong_ReturnsLong()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(long), new CodePrimitiveExpression(5));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(5L, result.Value);
            Assert.IsType<long>(result.Value);
        }

        [Fact]
        public void Evaluate_DoubleToInt_ReturnsInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.7));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(6, result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public void Evaluate_NullToReferenceType_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(string), new CodePrimitiveExpression(null));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public void Evaluate_NullToNullableType_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int?), new CodePrimitiveExpression(null));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public void Evaluate_CharToInt_ReturnsInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression('A'));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(65, result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public void Evaluate_IntToChar_ReturnsChar()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(char), new CodePrimitiveExpression(65));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal('A', result.Value);
            Assert.IsType<char>(result.Value);
        }

        [Fact]
        public void Evaluate_ByteToInt_ReturnsInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression((byte)255));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(255, result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public void Evaluate_LongToInt_ReturnsInt()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(100L));
            RuleExpressionWalker.Validate(validation, castExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, castExpr);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(100, result.Value);
            Assert.IsType<int>(result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_SimpleCast_ReturnsCorrectString()
        {
            // Arrange
            var castExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, castExpr, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Equal("(int)5.5", result);
        }

        [Fact]
        public void Decompile_CastWithParentheses_ReturnsCorrectString()
        {
            // Arrange
            var innerExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(3));
            var castExpr = new CodeCastExpression(typeof(long), innerExpr);
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, castExpr, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("(long)", result);
        }

        [Fact]
        public void Decompile_NullExpression_ThrowsException()
        {
            // Arrange
            var castExpr = new CodeCastExpression(typeof(int), null);
            var stringBuilder = new System.Text.StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                RuleExpressionWalker.Decompile(stringBuilder, castExpr, null));
        }

        [Fact]
        public void Decompile_GenericType_ReturnsCorrectString()
        {
            // Arrange
            var castExpr = new CodeCastExpression(typeof(int?), new CodePrimitiveExpression(5));
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, castExpr, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("System.Nullable<int>", result);
        }

        [Fact]
        public void Decompile_NestedCast_ReturnsCorrectString()
        {
            // Arrange
            var innerCast = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));
            var outerCast = new CodeCastExpression(typeof(long), innerCast);
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, outerCast, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("(long)", result);
            Assert.Contains("(int)", result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_SimpleCast_ReturnsEqualExpression()
        {
            // Arrange
            var original = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeCastExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.Equal(original.TargetType.BaseType, cloned.TargetType.BaseType);
        }

        [Fact]
        public void Clone_CastWithComplexExpression_ReturnsEqualExpression()
        {
            // Arrange
            var innerExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(3));
            var original = new CodeCastExpression(typeof(long), innerExpr);

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeCastExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.NotSame(original.Expression, cloned.Expression);
            Assert.Equal(original.TargetType.BaseType, cloned.TargetType.BaseType);
        }

        [Fact]
        public void Clone_PreservesTargetType_Success()
        {
            // Arrange
            var original = new CodeCastExpression(typeof(decimal), new CodePrimitiveExpression(5));

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeCastExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal("System.Decimal", cloned.TargetType.BaseType);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_SameCastExpressions_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));
            var expr2 = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_DifferentTargetTypes_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));
            var expr2 = new CodeCastExpression(typeof(long), new CodePrimitiveExpression(5.5));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_DifferentExpressions_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(5.5));
            var expr2 = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(6.5));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_ComplexExpressionsSame_ReturnsTrue()
        {
            // Arrange
            var innerExpr1 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(3));
            var expr1 = new CodeCastExpression(typeof(long), innerExpr1);

            var innerExpr2 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(3));
            var expr2 = new CodeCastExpression(typeof(long), innerExpr2);

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_ComplexExpressionsDifferent_ReturnsFalse()
        {
            // Arrange
            var innerExpr1 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(3));
            var expr1 = new CodeCastExpression(typeof(long), innerExpr1);

            var innerExpr2 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Subtract,
                new CodePrimitiveExpression(3));
            var expr2 = new CodeCastExpression(typeof(long), innerExpr2);

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_SimpleCast_AnalyzesChildExpression()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass), null);
            var fieldRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntValue");
            var castExpr = new CodeCastExpression(typeof(long), fieldRef);
            
            RuleExpressionWalker.Validate(validation, castExpr, false);
            
            var analysis = new RuleAnalysis(validation, true);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, castExpr, true, false, null);

            // Assert
            Assert.True(analysis.ForWrites);
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int IntValue { get; set; }
            public double DoubleValue { get; set; }
            public string? StringValue { get; set; }
            public long LongValue { get; set; }
        }

        #endregion
    }
}