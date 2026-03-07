using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleExpressionWalkerTest
    {
        #region Validate Tests

        [Fact]
        public void Validate_WithNullValidation_ThrowsArgumentNullException()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("validation", () =>
                RuleExpressionWalker.Validate(null, expression, false));
        }

        [Fact]
        public void Validate_WithThisReferenceExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(TestClass), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithPrimitiveExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithFieldReferenceExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithPropertyReferenceExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithBinaryOperatorExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithMethodInvokeExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithIndexerExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeArrayIndexerExpression(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntArray"
                ),
                new CodePrimitiveExpression(0)
            );

            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithArrayIndexerExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeArrayIndexerExpression(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntArray"
                ),
                new CodePrimitiveExpression(0)
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithCastExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeCastExpression(
                typeof(object),
                new CodePrimitiveExpression(42)
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(object), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithTypeReferenceExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeTypeReferenceExpression(typeof(string));
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Validate_WithObjectCreateExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeObjectCreateExpression(
                typeof(TestClass)
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(TestClass), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithArrayCreateExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(5)
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int[]), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithDirectionExpression_ReturnsValidInfo()
        {
            // Arrange
            var expression = new CodeDirectionExpression(
                FieldDirection.Ref,
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                )
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.NotNull(result);//System.Int32&
            Assert.Equal(typeof(int).MakeByRefType(), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithUnsupportedExpression_AddsValidationError()
        {
            // Arrange
            var expression = new CodeArgumentReferenceExpression("test");
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, expression, false);

            // Assert
            Assert.Null(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithCustomIRuleExpression_ReturnsValidInfo()
        {
            // Arrange
            var customExpression = new CustomRuleExpression();
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, customExpression, false);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Validate_WithWrittenTrue_RevalidatesExpression()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var validation = CreateMockValidation(typeof(TestClass));

            // First validate as read
            RuleExpressionWalker.Validate(validation, expression, false);

            // Act - Now validate as written
            var result = RuleExpressionWalker.Validate(validation, expression, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithNullAnalysis_ThrowsArgumentNullException()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("analysis", () =>
                RuleExpressionWalker.AnalyzeUsage(null, expression, true, false, null));
        }

        [Fact]
        public void AnalyzeUsage_WithThisReferenceExpression_CompletesSuccessfully()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var analysis = CreateMockAnalysis(typeof(TestClass));

            // Act & Assert - Should not throw
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);
        }

        [Fact]
        public void AnalyzeUsage_WithFieldReferenceExpression_CompletesSuccessfully()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var analysis = CreateMockAnalysis(typeof(TestClass));

            // Act & Assert - Should not throw
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);
        }

        [Fact]
        public void AnalyzeUsage_WithPropertyReferenceExpression_CompletesSuccessfully()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var codeStatement = new CodeAssignStatement(expression, new CodePrimitiveExpression(444));
            var analysis = CreateMockAnalysis(typeof(TestClass), codeStatement);

            // Act & Assert - Should not throw
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);
        }

        [Fact]
        public void AnalyzeUsage_WithMethodInvokeExpression_CompletesSuccessfully()
        {
            // Arrange
            var expression = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var codeStatement = new CodeExpressionStatement(expression);
            var analysis = CreateMockAnalysis(typeof(TestClass), codeStatement);

            // Act & Assert - Should not throw
            RuleExpressionWalker.AnalyzeUsage(analysis, expression, true, false, null);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithNullExecution_ThrowsArgumentNullException()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("execution", () =>
                RuleExpressionWalker.Evaluate(null, expression));
        }

        [Fact]
        public void Evaluate_WithThisReferenceExpression_ReturnsThis()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var testInstance = new TestClass();
            var execution = CreateMockExecution(testInstance);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Same(testInstance, result.Value);
        }

        [Fact]
        public void Evaluate_WithPrimitiveExpression_ReturnsValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var execution = CreateMockExecution(new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_WithFieldReferenceExpression_ReturnsFieldValue()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var codeStatement = new CodeAssignStatement(expression, new CodePrimitiveExpression(99));
            var testInstance = new TestClass { intField = 99 };
            var execution = CreateMockExecution(testInstance, codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(99, result.Value);
        }

        [Fact]
        public void Evaluate_WithPropertyReferenceExpression_ReturnsPropertyValue()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var codeStatement = new CodeAssignStatement(expression, new CodePrimitiveExpression(123));
            var testInstance = new TestClass { IntProperty = 123 };
            var execution = CreateMockExecution(testInstance, codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123, result.Value);
        }

        [Fact]
        public void Evaluate_WithBinaryOperatorExpression_ReturnsResult()
        {
            // Arrange
            var expression = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );
            var property = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var codeStatement = new CodeAssignStatement(property, expression);
            var execution = CreateMockExecution(typeof(TestClass), codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(15, result.Value);
        }

        [Fact]
        public void Evaluate_WithMethodInvokeExpression_ReturnsResult()
        {
            // Arrange
            var expression = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue"
            );
            var codeStatement = new CodeExpressionStatement(expression);
            var testInstance = new TestClass();
            var execution = CreateMockExecution(testInstance, codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_WithCastExpression_ReturnsResult()
        {
            // Arrange
            var property = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "ObjectProperty"
            );
            var expression = new CodeCastExpression(
                typeof(object),
                new CodePrimitiveExpression(42)
            );
            var codeStatement = new CodeAssignStatement(property, expression);
            var execution = CreateMockExecution(typeof(TestClass), codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_WithObjectCreateExpression_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeObjectCreateExpression(typeof(TestClass));
            var property = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "ObjectProperty"
            );
            var codeStatement = new CodeAssignStatement(property, expression);
            var execution = CreateMockExecution(typeof(TestClass), codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestClass>(result.Value);
        }

        [Fact]
        public void Evaluate_WithArrayCreateExpression_ReturnsNewArray()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(5)
            );
            var property = new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntArray"
                );
            var codeStatement = new CodeAssignStatement(property, expression);
            var execution = CreateMockExecution(typeof(TestClass), codeStatement);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int[]>(result.Value);
            Assert.Equal(5, ((int[])result.Value).Length);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithThisReferenceExpression_AppendsThis()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            Assert.Contains("this", sb.ToString());
        }

        [Fact]
        public void Decompile_WithPrimitiveExpression_AppendsValue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            Assert.Contains("42", sb.ToString());
        }

        [Fact]
        public void Decompile_WithFieldReferenceExpression_AppendsFieldAccess()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("intField", result);
        }

        [Fact]
        public void Decompile_WithPropertyReferenceExpression_AppendsPropertyAccess()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("IntProperty", result);
        }

        [Fact]
        public void Decompile_WithBinaryOperatorExpression_AppendsOperation()
        {
            // Arrange
            var expression = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("5", result);
            Assert.Contains("10", result);
        }

        [Fact]
        public void Decompile_WithMethodInvokeExpression_AppendsMethodCall()
        {
            // Arrange
            var expression = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var sb = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(sb, expression, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("TestMethod", result);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithBothNull_ReturnsTrue()
        {
            // Act
            var result = RuleExpressionWalker.Match(null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithFirstNull_ReturnsFalse()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act
            var result = RuleExpressionWalker.Match(null, expression);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSecondNull_ReturnsFalse()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act
            var result = RuleExpressionWalker.Match(expression, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentTypes_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeThisReferenceExpression();
            var expression2 = new CodePrimitiveExpression(42);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameThisReference_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeThisReferenceExpression();
            var expression2 = new CodeThisReferenceExpression();

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithSamePrimitiveValue_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(42);
            var expression2 = new CodePrimitiveExpression(42);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentPrimitiveValue_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePrimitiveExpression(42);
            var expression2 = new CodePrimitiveExpression(99);

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameFieldReference_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var expression2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentFieldReference_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var expression2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "stringField"
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSamePropertyReference_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var expression2 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithSameBinaryOperator_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );
            var expression2 = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithSameMethodInvoke_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var expression2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );

            // Act
            var result = RuleExpressionWalker.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithNull_ReturnsNull()
        {
            // Act
            var result = RuleExpressionWalker.Clone(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Clone_WithThisReference_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeThisReferenceExpression();

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeThisReferenceExpression>(result);
            Assert.NotSame(expression, result);
        }

        [Fact]
        public void Clone_WithPrimitiveExpression_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodePrimitiveExpression>(result);
            Assert.NotSame(expression, result);
            Assert.Equal(42, ((CodePrimitiveExpression)result).Value);
        }

        [Fact]
        public void Clone_WithFieldReference_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeFieldReferenceExpression>(result);
            Assert.NotSame(expression, result);
            var clonedField = (CodeFieldReferenceExpression)result;
            Assert.Equal("intField", clonedField.FieldName);
        }

        [Fact]
        public void Clone_WithPropertyReference_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodePropertyReferenceExpression>(result);
            Assert.NotSame(expression, result);
            var clonedProperty = (CodePropertyReferenceExpression)result;
            Assert.Equal("IntProperty", clonedProperty.PropertyName);
        }

        [Fact]
        public void Clone_WithBinaryOperator_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(10)
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeBinaryOperatorExpression>(result);
            Assert.NotSame(expression, result);
            var clonedBinary = (CodeBinaryOperatorExpression)result;
            Assert.Equal(CodeBinaryOperatorType.Add, clonedBinary.Operator);
        }

        [Fact]
        public void Clone_WithMethodInvoke_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeMethodInvokeExpression>(result);
            Assert.NotSame(expression, result);
            var clonedMethod = (CodeMethodInvokeExpression)result;
            Assert.Equal("TestMethod", clonedMethod.Method.MethodName);
        }

        [Fact]
        public void Clone_WithCastExpression_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeCastExpression(
                typeof(object),
                new CodePrimitiveExpression(42)
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeCastExpression>(result);
            Assert.NotSame(expression, result);
        }

        [Fact]
        public void Clone_WithObjectCreate_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeObjectCreateExpression(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeObjectCreateExpression>(result);
            Assert.NotSame(expression, result);
        }

        [Fact]
        public void Clone_WithArrayCreate_ReturnsNewInstance()
        {
            // Arrange
            var expression = new CodeArrayCreateExpression(
                typeof(int),
                new CodePrimitiveExpression(5)
            );

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeArrayCreateExpression>(result);
            Assert.NotSame(expression, result);
        }

        [Fact]
        public void Clone_PreservesUserData()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);
            expression.UserData["TestKey"] = "TestValue";

            // Act
            var result = RuleExpressionWalker.Clone(expression);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.UserData.Contains("TestKey"));
            Assert.Equal("TestValue", result.UserData["TestKey"]);
        }

        #endregion

        #region Helper Methods

        private static RuleValidation CreateMockValidation(Type type)
        {
            return new RuleValidation(type);
        }

        private static RuleExecution CreateMockExecution(object instance)
        {
            var validation = CreateMockValidation(instance.GetType());
            return new RuleExecution(validation, instance);
        }

        private static RuleAnalysis CreateMockAnalysis(Type type)
        {
            var validation = CreateMockValidation(type);
            return new RuleAnalysis(validation, true);
        }

        private static RuleAnalysis CreateMockAnalysis(Type type, CodeStatement statement)
        {
            var validation = CreateMockValidation(type, statement);
            return new RuleAnalysis(validation, true);
        }

        private static RuleValidation CreateMockValidation(Type type, CodeStatement statement)
        {
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = new CodePrimitiveExpression(1),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(1)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(statement));
            ruleSet.Rules.Add(rule);
            var validation = CreateMockValidation(type);
            ruleSet.Validate(validation);
            return validation;
        }

        private static RuleExecution CreateMockExecution(Type type, CodeStatement statement)
        {
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = new CodePrimitiveExpression(1),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(1)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(statement));
            ruleSet.Rules.Add(rule);
            var validation = CreateMockValidation(type);
            ruleSet.Validate(validation);

            var instance = Activator.CreateInstance(type);
            return new RuleExecution(validation, instance);
        }

        private static RuleExecution CreateMockExecution(object instance, CodeStatement statement)
        {
            Type type = instance.GetType();
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = new CodePrimitiveExpression(1),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(1)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(statement));
            ruleSet.Rules.Add(rule);
            var validation = CreateMockValidation(type);
            ruleSet.Validate(validation);

            return new RuleExecution(validation, instance);
        }

        #endregion

        #region Test Helper Classes

        public class TestClass
        {
            public int intField;
            public string? stringField;

            public int IntProperty { get; set; }
            public object? ObjectProperty { get; set; }
            public string? StringProperty { get; set; }
            public int[] IntArray { get; set; } = new int[10];

#pragma warning disable CA1822 // Mark members as static
            public void TestMethod() { }

            public int GetValue() => 42;
#pragma warning restore CA1822 // Mark members as static
        }

        private class CustomRuleExpression : CodeExpression, IRuleExpression
        {
            public void AnalyzeUsage(RuleAnalysis analysis, bool isRead, bool isWritten, RulePathQualifier qualifier)
            {
                // No-op for testing
            }

            CodeExpression IRuleExpression.Clone()
            {
                return new CustomRuleExpression();
            }

            public void Decompile(StringBuilder decompilation, CodeExpression parentExpression)
            {
                decompilation.Append("CustomExpression");
            }

            public IRuleExpressionResult Evaluate(RuleExecution execution)
            {
                return new RuleLiteralResult(42);
            }

            public bool Match(CodeExpression expression)
            {
                return expression is CustomRuleExpression;
            }

            public RuleExpressionInfo Validate(RuleValidation validation, bool isWritten)
            {
                return new RuleExpressionInfo(typeof(int));
            }
        }

        #endregion
    }
}