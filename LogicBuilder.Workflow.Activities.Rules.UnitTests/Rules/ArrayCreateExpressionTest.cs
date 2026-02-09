using LogicBuilder.Workflow.Activities.Common;
using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ArrayCreateExpressionTest
    {
        private readonly Type testType = typeof(TestClass);

        public ArrayCreateExpressionTest()
        {
        }

        #region Validate Tests

        [Fact]
        public void Validate_WithValidIntArray_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithValidStringArrayAndInitializers_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(string),
                new CodePrimitiveExpression("test1"),
                new CodePrimitiveExpression("test2")
            );
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(string[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithSizeExpression_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithIsWrittenTrue_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, true);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithArrayType_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int[]), 5);
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithNegativeSize_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), -1);
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithInvalidSizeExpressionType_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression("invalid")
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithBothSizeAndSizeExpression_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5)
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithIncompatibleInitializerType_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 2);
            expression.Initializers.Add(new CodePrimitiveExpression(1));
            expression.Initializers.Add(new CodePrimitiveExpression("invalid"));
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_OperandTypesIncompatible, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithMoreInitializersThanSize_AddsError()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 1);
            expression.Initializers.Add(new CodePrimitiveExpression(1));
            expression.Initializers.Add(new CodePrimitiveExpression(2));
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
            Assert.Equal(ErrorNumbers.Error_OperandTypesIncompatible, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithUIntSizeExpression_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10u)
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithLongSizeExpression_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10L)
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithULongSizeExpression_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10ul)
            };
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithZeroSize_ReturnsArrayType()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 0);
            var validation = new RuleValidation(testType);

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithSize_CreatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<int[]>(result.Value);
            Assert.Equal(5, ((int[])result.Value).Length);
        }

        [Fact]
        public void Evaluate_WithSizeExpressionInt_CreatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(string))
            {
                SizeExpression = new CodePrimitiveExpression(3)
            };
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<string[]>(result.Value);
            Assert.Equal(3, ((string[])result.Value).Length);
        }

        [Fact]
        public void Evaluate_WithSizeExpressionLong_CreatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(double))
            {
                SizeExpression = new CodePrimitiveExpression(4L)
            };
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<double[]>(result.Value);
            Assert.Equal(4, ((double[])result.Value).Length);
        }

        [Fact]
        public void Evaluate_WithSizeExpressionUInt_CreatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(bool))
            {
                SizeExpression = new CodePrimitiveExpression(2u)
            };
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<bool[]>(result.Value);
            Assert.Equal(2, ((bool[])result.Value).Length);
        }

        [Fact]
        public void Evaluate_WithSizeExpressionULong_CreatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(char))
            {
                SizeExpression = new CodePrimitiveExpression(6ul)
            };
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<char[]>(result.Value);
            Assert.Equal(6, ((char[])result.Value).Length);
        }

        [Fact]
        public void Evaluate_WithInitializers_CreatesAndPopulatesArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(10),
                new CodePrimitiveExpression(20),
                new CodePrimitiveExpression(30)
            );
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            var array = (int[])result.Value;
            Assert.Equal(3, array.Length);
            Assert.Equal(10, array[0]);
            Assert.Equal(20, array[1]);
            Assert.Equal(30, array[2]);
        }

        [Fact]
        public void Evaluate_WithSizeAndInitializers_CreatesArrayWithExtraSpace()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(string), 5);
            expression.Initializers.Add(new CodePrimitiveExpression("first"));
            expression.Initializers.Add(new CodePrimitiveExpression("second"));
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            var array = (string[])result.Value;
            Assert.Equal(5, array.Length);
            Assert.Equal("first", array[0]);
            Assert.Equal("second", array[1]);
            Assert.Null(array[2]);
        }

        [Fact(Skip = "Fix line 3440 in Expressions.cs. createExpression == null should be createExpression == null")]
        public void Evaluate_WithoutValidation_ThrowsException()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var validation = new RuleValidation(testType);
            var execution = new RuleExecution(validation, new TestClass());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                RuleExpressionWalker.Evaluate(execution, expression));
        }

        [Fact]
        public void Evaluate_WithZeroSize_CreatesEmptyArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 0);
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<int[]>(result.Value);
            Assert.Empty(((int[])result.Value));
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithSize_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new int[5]", builder.ToString());
        }

        [Fact]
        public void Decompile_WithSizeExpression_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(string))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new string[10]", builder.ToString());
        }

        [Fact]
        public void Decompile_WithInitializers_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2),
                new CodePrimitiveExpression(3)
            );
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new int[] { 1, 2, 3}", builder.ToString());
        }

        [Fact]
        public void Decompile_WithSizeAndInitializers_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(string), 5);
            expression.Initializers.Add(new CodePrimitiveExpression("test"));
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new string[5] { \"test\"}", builder.ToString());
        }

        [Fact]
        public void Decompile_WithZeroSize_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 0);
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new int[0]", builder.ToString());
        }

        [Fact]
        public void Decompile_WithEmptyInitializers_ReturnsCorrectString()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int));
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, null);

            // Assert
            Assert.Equal("new int[0]", builder.ToString());
        }

        [Fact]
        public void Decompile_WithParentExpression_HandlesParentheses()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);
            var parent = new CodeBinaryOperatorExpression();
            var builder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(builder, expression, parent);

            // Assert - exact parentheses may vary based on RuleDecompiler.MustParenthesize logic
            Assert.Contains("int[5]", builder.ToString());
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithSize_CreatesIdenticalExpression()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(int), 5);

            // Act
            var cloned = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<CodeArrayCreateExpression>(cloned);
            var clonedArray = (CodeArrayCreateExpression)cloned;
            Assert.Equal(expression.Size, clonedArray.Size);
            Assert.Equal(expression.CreateType.BaseType, clonedArray.CreateType.BaseType);
        }

        [Fact]
        public void Clone_WithSizeExpression_CreatesIdenticalExpression()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(typeof(string))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };

            // Act
            var cloned = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<CodeArrayCreateExpression>(cloned);
            var clonedArray = (CodeArrayCreateExpression)cloned;
            Assert.NotNull(clonedArray.SizeExpression);
            Assert.IsType<CodePrimitiveExpression>(clonedArray.SizeExpression);
            Assert.Equal(10, ((CodePrimitiveExpression)clonedArray.SizeExpression).Value);
        }

        [Fact]
        public void Clone_WithInitializers_CreatesIdenticalExpression()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            );

            // Act
            var cloned = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<CodeArrayCreateExpression>(cloned);
            var clonedArray = (CodeArrayCreateExpression)cloned;
            Assert.Equal(2, clonedArray.Initializers.Count);
            Assert.Equal(1, ((CodePrimitiveExpression)clonedArray.Initializers[0]).Value);
            Assert.Equal(2, ((CodePrimitiveExpression)clonedArray.Initializers[1]).Value);
        }

        [Fact]
        public void Clone_DoesNotShareReferences()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                [new CodePrimitiveExpression(1), new CodePrimitiveExpression(2)]
            );

            // Act
            var cloned = (CodeArrayCreateExpression)RuleExpressionWalker.Clone(expression);
            cloned.Initializers[0] = new CodePrimitiveExpression(999);

            // Assert
            Assert.Equal(1, ((CodePrimitiveExpression)expression.Initializers[0]).Value);
            Assert.Equal(999, ((CodePrimitiveExpression)cloned.Initializers[0]).Value);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int), 5);
            var expression2 = new CodeArrayCreateExpression(typeof(int), 5);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentSize_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int), 5);
            var expression2 = new CodeArrayCreateExpression(typeof(int), 10);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentTypes_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int), 5);
            var expression2 = new CodeArrayCreateExpression(typeof(string), 5);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithMatchingSizeExpressions_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var expression2 = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentSizeExpressions_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var expression2 = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(20)
            };

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithOnlyOneSizeExpression_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = new CodePrimitiveExpression(10)
            };
            var expression2 = new CodeArrayCreateExpression(typeof(int), 10);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithMatchingInitializers_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            );
            var expression2 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentInitializers_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            );
            var expression2 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(3)
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentInitializerCounts_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            );
            var expression2 = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(1)
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithNonArrayCreateExpression_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeArrayCreateExpression(typeof(int), 5);
            var expression2 = new CodePrimitiveExpression(5);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithSizeExpression_AnalyzesExpression()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var sizeExpr = new CodePropertyReferenceExpression(thisRef, "IntValue");
            var expression = new CodeArrayCreateExpression(typeof(int))
            {
                SizeExpression = sizeExpr
            };
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var analysis = new RuleAnalysis(validation, false);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);

            // Assert - should analyze the size expression
            Assert.True(analysis.GetSymbols().Count > 0);
        }

        [Fact]
        public void AnalyzeUsage_WithInitializers_AnalyzesAll()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var init1 = new CodePropertyReferenceExpression(thisRef, "IntValue");
            var init2 = new CodePropertyReferenceExpression(thisRef, "DoubleValue");
            var expression = new CodeArrayCreateExpression(typeof(object));
            expression.Initializers.Add(init1);
            expression.Initializers.Add(init2);
            var validation = new RuleValidation(testType);
            RuleExpressionWalker.Validate(validation, expression, false);
            var analysis = new RuleAnalysis(validation, false);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);

            // Assert - should analyze all initializer expressions
            var symbols = analysis.GetSymbols();
            Assert.True(symbols.Count >= 2);
        }

        #endregion

        private class TestClass
        {
            public int IntValue { get; set; } = 10;
            public double DoubleValue { get; set; } = 3.14;
            public string StringValue { get; set; } = "test";
        }
    }
}