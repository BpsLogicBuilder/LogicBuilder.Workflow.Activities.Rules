using LogicBuilder.Workflow.Activities.Common;
using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ArrayIndexerExpressionTest
    {
        private readonly ArrayIndexerExpression _arrayIndexerExpression;

        public ArrayIndexerExpressionTest()
        {
            _arrayIndexerExpression = new ArrayIndexerExpression();
        }

        #region Test Helper Classes

        private class TestClass
        {
            public int[]? SingleDimArray = [1, 2, 3, 4, 5];
            public string[] StringArray = ["a", "b", "c"];
            public int[,] TwoDimArray = new int[3, 4];
            public int[,,] ThreeDimArray = new int[2, 3, 4];
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithValidSingleDimensionalArray_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithValidMultiDimensionalArray_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr1 = new CodePrimitiveExpression(0);
            var indexExpr2 = new CodePrimitiveExpression(1);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr1, indexExpr2);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNullTargetObject_ReturnsNullAndAddsError()
        {
            // Arrange
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(null, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithStaticTarget_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNoIndices_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var expression = new CodeArrayIndexerExpression(targetObject);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNullLiteralTarget_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodePrimitiveExpression(null);
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNonArrayType_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_CannotIndexType, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithWrongArrayRank_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr = new CodePrimitiveExpression(0); // Only 1 index for 2D array
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ArrayIndexBadRank, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithDirectionExpression_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodeDirectionExpression(FieldDirection.Out, new CodePrimitiveExpression(0));
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_IndexerArgCannotBeRefOrOut, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithInvalidIndexType_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression("invalid"); // String instead of int
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_ArrayIndexBadType, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithValidByteIndex_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodeCastExpression(typeof(byte), new CodePrimitiveExpression(0));
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithValidLongIndex_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodeCastExpression(typeof(long), new CodePrimitiveExpression(0));
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithThreeDimensionalArray_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "ThreeDimArray");
            var indexExpr1 = new CodePrimitiveExpression(0);
            var indexExpr2 = new CodePrimitiveExpression(1);
            var indexExpr3 = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr1, indexExpr2, indexExpr3);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WhenIsWritten_ReturnsValidExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _arrayIndexerExpression.Validate(expression, validation, true);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithValidSingleDimensionalArray_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));
            _arrayIndexerExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _arrayIndexerExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Value);
        }

        [Fact]
        public void Evaluate_WithValidMultiDimensionalArray_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            testInstance.TwoDimArray[1, 2] = 42;
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr1 = new CodePrimitiveExpression(1);
            var indexExpr2 = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr1, indexExpr2);
            var validation = new RuleValidation(typeof(TestClass));
            _arrayIndexerExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _arrayIndexerExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_WithNullTarget_ThrowsRuleEvaluationException()
        {
            // Arrange
            var testInstance = new TestClass
            {
                SingleDimArray = null
            };
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));
            _arrayIndexerExpression.Validate(expression, validation, false);
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                _arrayIndexerExpression.Evaluate(expression, execution));
            Assert.Contains("null", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Evaluate_WithoutValidation_ThrowsInvalidOperationException()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _arrayIndexerExpression.Evaluate(expression, execution));
            Assert.Contains("not validated", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Evaluate_CanSetArrayElement_SetsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));
            _arrayIndexerExpression.Validate(expression, validation, true);
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _arrayIndexerExpression.Evaluate(expression, execution);
            result.Value = 999;

            // Assert
            Assert.Equal(999, testInstance.SingleDimArray![2]);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithValidExpression_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act
            _arrayIndexerExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Equal("this.SingleDimArray[2]", result);
        }

        [Fact]
        public void Decompile_WithMultipleIndices_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr1 = new CodePrimitiveExpression(1);
            var indexExpr2 = new CodePrimitiveExpression(3);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr1, indexExpr2);
            var stringBuilder = new StringBuilder();

            // Act
            _arrayIndexerExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Equal("this.TwoDimArray[1, 3]", result);
        }

        [Fact]
        public void Decompile_WithNullTargetObject_ThrowsRuleEvaluationException()
        {
            // Arrange
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(null, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                _arrayIndexerExpression.Decompile(expression, stringBuilder, null));
            Assert.Contains("null", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Decompile_WithNoIndices_ThrowsRuleEvaluationException()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var expression = new CodeArrayIndexerExpression
            {
                TargetObject = targetObject
            };
            var stringBuilder = new StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                _arrayIndexerExpression.Decompile(expression, stringBuilder, null));
            Assert.Contains("index", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesIdenticalExpression()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);

            // Act
            var cloned = _arrayIndexerExpression.Clone(expression) as CodeArrayIndexerExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned?.TargetObject);
            Assert.Equal(expression.Indices.Count, cloned?.Indices.Count);
            
            var originalTarget = expression.TargetObject as CodeFieldReferenceExpression ?? throw new InvalidOperationException();
            var clonedTarget = cloned?.TargetObject as CodeFieldReferenceExpression ?? throw new InvalidOperationException();
            Assert.Equal(originalTarget.FieldName, clonedTarget.FieldName);
        }

        [Fact]
        public void Clone_WithMultipleIndices_CreatesIdenticalExpression()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr1 = new CodePrimitiveExpression(1);
            var indexExpr2 = new CodePrimitiveExpression(3);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr1, indexExpr2);

            // Act
            var cloned = _arrayIndexerExpression.Clone(expression) as CodeArrayIndexerExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(2, cloned?.Indices.Count);
            Assert.NotSame(expression.Indices[0], cloned?.Indices[0]);
            Assert.NotSame(expression.Indices[1], cloned?.Indices[1]);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr1 = new CodePrimitiveExpression(2);
            var expression1 = new CodeArrayIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr2 = new CodePrimitiveExpression(2);
            var expression2 = new CodeArrayIndexerExpression(targetObject2, indexExpr2);

            // Act
            var result = _arrayIndexerExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentTargets_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr1 = new CodePrimitiveExpression(2);
            var expression1 = new CodeArrayIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "StringArray");
            var indexExpr2 = new CodePrimitiveExpression(2);
            var expression2 = new CodeArrayIndexerExpression(targetObject2, indexExpr2);

            // Act
            var result = _arrayIndexerExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentIndices_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr1 = new CodePrimitiveExpression(2);
            var expression1 = new CodeArrayIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr2 = new CodePrimitiveExpression(3);
            var expression2 = new CodeArrayIndexerExpression(targetObject2, indexExpr2);

            // Act
            var result = _arrayIndexerExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentIndexCounts_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr1 = new CodePrimitiveExpression(2);
            var expression1 = new CodeArrayIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr2a = new CodePrimitiveExpression(1);
            var indexExpr2b = new CodePrimitiveExpression(2);
            var expression2 = new CodeArrayIndexerExpression(targetObject2, indexExpr2a, indexExpr2b);

            // Act
            var result = _arrayIndexerExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithMultipleIdenticalIndices_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr1a = new CodePrimitiveExpression(1);
            var indexExpr1b = new CodePrimitiveExpression(3);
            var expression1 = new CodeArrayIndexerExpression(targetObject1, indexExpr1a, indexExpr1b);

            var targetObject2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "TwoDimArray");
            var indexExpr2a = new CodePrimitiveExpression(1);
            var indexExpr2b = new CodePrimitiveExpression(3);
            var expression2 = new CodeArrayIndexerExpression(targetObject2, indexExpr2a, indexExpr2b);

            // Act
            var result = _arrayIndexerExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_AnalyzesTargetObjectWithQualifier()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeArrayIndexerExpression(targetObject, indexExpr);

            var validation = new RuleValidation(typeof(TestClass));
            _arrayIndexerExpression.Validate(expression, validation, true);
            var analysis = new RuleAnalysis(validation, true);

            // Act
            _arrayIndexerExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            Assert.NotEmpty(analysis.GetSymbols());
        }

        [Fact]
        public void AnalyzeUsage_AnalyzesIndexArguments()
        {
            // Arrange
            var targetObject = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "SingleDimArray");
            var indexField = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "PublicIntField");
            var expression = new CodeArrayIndexerExpression(targetObject, indexField);
            
            var validation = new RuleValidation(typeof(TestClassWithIndex));
            _arrayIndexerExpression.Validate(expression, validation, true);
            var analysis = new RuleAnalysis(validation, true);

            // Act
            _arrayIndexerExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        private class TestClassWithIndex
        {
            public int[] SingleDimArray = [1, 2, 3, 4, 5];
            public int PublicIntField = 2;
        }

        #endregion
    }
}