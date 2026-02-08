using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DirectionExpressionTest
    {
        #region Validate Tests

        [Fact]
        public void Validate_InDirection_ReturnsValidExpressionInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_OutDirection_ReturnsValidExpressionInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ExpressionType.IsByRef || result.ExpressionType == typeof(int));
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_RefDirection_ReturnsValidExpressionInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ExpressionType.IsByRef || result.ExpressionType == typeof(int));
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_IsWritten_AddsValidationError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("Cannot write to an expression of this type.", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_NullExpression_AddsValidationError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(FieldDirection.In, null);

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("direction", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_TypeReferenceExpression_AddsValidationError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeTypeReferenceExpression(typeof(int)));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_RefDirection_WithPropertyReference_ReturnsValidInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "IntProperty"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_InDirection_ReturnsCorrectValue()
        {
            // Arrange
            var testObj = new TestClass { IntField = 42 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObj);
            
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, directionExpr);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_OutDirection_ReturnsCorrectResult()
        {
            // Arrange
            var testObj = new TestClass { IntField = 10 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObj);
            
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, directionExpr);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Evaluate_RefDirection_ReturnsCorrectValue()
        {
            // Arrange
            var testObj = new TestClass { IntField = 100 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObj);
            
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, directionExpr);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_InDirection_NoPrefix()
        {
            // Arrange
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, directionExpr, null);

            // Assert
            var result = sb.ToString();
            Assert.DoesNotContain("out ", result);
            Assert.DoesNotContain("ref ", result);
            Assert.Contains("IntField", result);
        }

        [Fact]
        public void Decompile_OutDirection_HasOutPrefix()
        {
            // Arrange
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, directionExpr, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("out ", result);
            Assert.Contains("IntField", result);
        }

        [Fact]
        public void Decompile_RefDirection_HasRefPrefix()
        {
            // Arrange
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, directionExpr, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("ref ", result);
            Assert.Contains("IntField", result);
        }

        [Fact]
        public void Decompile_WithPropertyReference_DecompilesCorrectly()
        {
            // Arrange
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "IntProperty"));
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, directionExpr, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("ref ", result);
            Assert.Contains("IntProperty", result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesIdenticalCopy()
        {
            // Arrange
            var original = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeDirectionExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(original.Direction, cloned.Direction);
            Assert.NotSame(original, cloned);
            Assert.NotSame(original.Expression, cloned.Expression);
        }

        [Fact]
        public void Clone_InDirection_PreservesDirection()
        {
            // Arrange
            var original = new CodeDirectionExpression(
                FieldDirection.In,
                new CodePrimitiveExpression(42));

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeDirectionExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(FieldDirection.In, cloned.Direction);
        }

        [Fact]
        public void Clone_OutDirection_PreservesDirection()
        {
            // Arrange
            var original = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var cloned = RuleExpressionWalker.Clone(original) as CodeDirectionExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(FieldDirection.Out, cloned.Direction);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_SameDirectionAndExpression_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var expr2 = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_DifferentDirection_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var expr2 = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_DifferentExpression_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            var expr2 = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "StringField"));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_BothInDirectionWithSameField_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeDirectionExpression(
                FieldDirection.In,
                new CodePrimitiveExpression(100));
            var expr2 = new CodeDirectionExpression(
                FieldDirection.In,
                new CodePrimitiveExpression(100));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_InDirection_AnalyzesWithWildcard()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.In,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);
            var analysis = new RuleAnalysis(validation, true);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, directionExpr, true, false, null);

            // Assert - verifies no exception is thrown
            Assert.NotNull(analysis);
        }

        [Fact]
        public void AnalyzeUsage_OutDirection_AnalyzesAsWrite()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Out,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);
            var analysis = new RuleAnalysis(validation, false);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, directionExpr, false, true, null);

            // Assert - verifies no exception is thrown
            Assert.NotNull(analysis);
        }

        [Fact]
        public void AnalyzeUsage_RefDirection_AnalyzesForBothReadAndWrite()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var directionExpr = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IntField"));
            
            RuleExpressionWalker.Validate(validation, directionExpr, false);
            var analysis = new RuleAnalysis(validation, true);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, directionExpr, true, true, null);

            // Assert - verifies no exception is thrown
            Assert.NotNull(analysis);
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int IntField;
            
            public int IntProperty { get; set; }
            public string? StringProperty { get; set; }
        }

        #endregion
    }
}