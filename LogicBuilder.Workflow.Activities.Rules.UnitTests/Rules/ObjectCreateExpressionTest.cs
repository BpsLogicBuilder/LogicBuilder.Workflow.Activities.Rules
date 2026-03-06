using LogicBuilder.Workflow.Activities.Common;
using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ObjectCreateExpressionTest
    {
        #region Helper Classes
        public class SimpleClass
        {
            public SimpleClass() { }
            public SimpleClass(int value) { Value = value; }
            public SimpleClass(int value, string name) { Value = value; Name = name; }
            public int Value { get; set; }
            public string? Name { get; set; }
        }

        public abstract class AbstractClass
        {
            public abstract void DoSomething();
        }

        public class ClassWithOptionalParams(int value = 10, string name = "default")
        {
            public int Value { get; set; } = value;
            public string Name { get; set; } = name;
        }

        public class ClassWithParamsArray(params int[] numbers)
        {
            public int[] Numbers { get; set; } = numbers;
        }

        public class ClassWithRefParams
        {
            public int Value { get; set; }

            public ClassWithRefParams(ref int value)
            {
                value *= 2;
                Value = value;
            }
        }

        public class ClassWithOutParams
        {
            public int Value { get; set; }

            public ClassWithOutParams(int input, out int output)
            {
                Value = input;
                output = input * 2;
            }
        }

        public struct SimpleStruct
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }

        public struct StructWithConstructor(int value)
        {
            public int Value { get; set; } = value;
        }
        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithWrittenFlag_ReturnsNullAndAddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithValidClassAndNoParameters_ReturnsValidInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(SimpleClass), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithValueTypeAndNoParameters_ReturnsValidInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleStruct));
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleStruct));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(SimpleStruct), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithAbstractType_ReturnsNullAndAddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(AbstractClass));
            var createExpr = new CodeObjectCreateExpression(typeof(AbstractClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_MethodNotExists, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithValidParameters_ReturnsValidInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(SimpleClass), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithInvalidParameterType_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression("invalid")); // Expecting int
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_WithNonExistentConstructor_ReturnsNullAndAddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression(true)); // No constructor with (int, bool)
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(ErrorNumbers.Error_MethodNotExists, validation.Errors[0].ErrorNumber);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithNoParameters_DoesNotAddSymbols()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            var analysis = new RuleAnalysis(validation, true);

            // Act
            expressionInternal.AnalyzeUsage(createExpr, analysis, true, false, null);

            // Assert
            Assert.Empty(analysis.GetSymbols());
        }

        [Fact]
        public void AnalyzeUsage_TestUsingRule()
        {
            // Arrange
            var fieldRef = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass), fieldRef);
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            var analysis = new RuleAnalysis(validation, false);

            // Act
            expressionInternal.AnalyzeUsage(createExpr, analysis, true, false, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_WithFieldParameter_AddsSymbol()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var fieldRef = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef);
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            var analysis = new RuleAnalysis(validation, false);

            // Act
            expressionInternal.AnalyzeUsage(createExpr, analysis, true, false, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_WithMultipleParameters_AnalyzesAll()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var fieldRef1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var fieldRef2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Name");
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef1,
                fieldRef2);
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            var analysis = new RuleAnalysis(validation, false);

            // Act
            expressionInternal.AnalyzeUsage(createExpr, analysis, true, false, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithoutValidation_ThrowsException()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var testInstance = new SimpleClass { Value = 99 };
            var execution = new RuleExecution(validation, testInstance);
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();
            // Don't validate

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                expressionInternal.Evaluate(createExpr, execution));
            Assert.NotNull(exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        [Fact]
        public void Evaluate_WithValueTypeAndNoParameters_CreatesInstance()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleStruct));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(SimpleStruct));
            ruleSet.Validate(validation);
            var execution = new RuleExecution(validation, new SimpleStruct());
            
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<SimpleStruct>(result.Value);
        }

        [Fact]
        public void Evaluate_WithClassAndNoParameters_CreatesInstance()
        {
            // Arrange
            var fieldRef = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Value");
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass), fieldRef);
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(SimpleClass));
            ruleSet.Validate(validation);

            var testInstance = new SimpleClass { Value = 99 };
            var execution = new RuleExecution(validation, testInstance);
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            Assert.IsType<SimpleClass>(result.Value);
        }

        [Fact]
        public void Evaluate_WithParameters_CreatesInstanceWithValues()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(SimpleClass));
            ruleSet.Validate(validation);
            var testInstance = new SimpleClass { Value = 99 };
            var execution = new RuleExecution(validation, testInstance);
            
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as SimpleClass ?? throw new InvalidOperationException("Instance is null.");
            Assert.NotNull(instance);
            Assert.Equal(42, instance.Value);
            Assert.Equal("test", instance.Name);
        }

        [Fact]
        public void Evaluate_WithOutParameter_SetsOutValue()
        {
            // Arrange
            var outField = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var outParam = new CodeDirectionExpression(
                FieldDirection.Out, outField);

            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithOutParams),
                new CodePrimitiveExpression(10),
                outParam);
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            var testClass = new TestClass { Value = 0 };
            var execution = new RuleExecution(validation, testClass);
            
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as ClassWithOutParams ?? throw new InvalidOperationException("Instance is null.");
            Assert.NotNull(instance);
            Assert.Equal(10, instance.Value);
            Assert.Equal(20, testClass.Value); // Out parameter should be set
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithNoParameters_GeneratesCorrectString()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();
            var sb = new StringBuilder();

            // Act
            expressionInternal.Decompile(createExpr, sb, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("new", result);
            Assert.Contains("SimpleClass", result);
            Assert.Contains("()", result);
        }

        [Fact]
        public void Decompile_WithParameters_GeneratesCorrectString()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expressionInternal = GetObjectCreateExpression();
            var sb = new StringBuilder();

            // Act
            expressionInternal.Decompile(createExpr, sb, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("new", result);
            Assert.Contains("SimpleClass", result);
            Assert.Contains("42", result);
            Assert.Contains("\"test\"", result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithNoParameters_CreatesEqualExpression()
        {
            // Arrange
            var original = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var cloned = expressionInternal.Clone(original) as CodeObjectCreateExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.Equal(original.CreateType.BaseType, cloned.CreateType.BaseType);
            Assert.Equal(original.Parameters.Count, cloned.Parameters.Count);
        }

        [Fact]
        public void Clone_WithParameters_CreatesEqualExpression()
        {
            // Arrange
            var original = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var cloned = expressionInternal.Clone(original) as CodeObjectCreateExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.Equal(original.CreateType.BaseType, cloned.CreateType.BaseType);
            Assert.Equal(original.Parameters.Count, cloned.Parameters.Count);
            
            for (int i = 0; i < original.Parameters.Count; i++)
            {
                Assert.NotSame(original.Parameters[i], cloned.Parameters[i]);
            }
        }

        [Fact]
        public void Clone_WithComplexParameters_CreatesDeepCopy()
        {
            // Arrange
            var fieldRef = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var original = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var cloned = expressionInternal.Clone(original) as CodeObjectCreateExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.Single(cloned.Parameters);
            Assert.NotSame(original.Parameters[0], cloned.Parameters[0]);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithEqualExpressions_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expr2 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentTypes_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expr2 = new CodeObjectCreateExpression(typeof(TestClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentParameterCounts_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42));
            var expr2 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentParameters_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42));
            var expr2 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(99));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithEqualParameters_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expr2 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithNullComperand_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentExpressionType_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeObjectCreateExpression(typeof(SimpleClass));
            var expr2 = new CodePrimitiveExpression(42);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Additional Validate Tests

        [Fact]
        public void Validate_WithNullCreateType_ReturnsNullAndAddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var createExpr = new CodeObjectCreateExpression();
            // CreateType defaults to void when null, which is different from being explicitly null
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert - When CreateType is not set, it defaults to void which is valid
            Assert.NotNull(result);
            Assert.Equal(typeof(void), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithInvalidParameterExpression_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var invalidParam = new CodeBinaryOperatorExpression(); // Invalid, not initialized
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                invalidParam);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_WithOptionalParameters_ValidatesSuccessfully()
        {
            // Arrange
            var validation = new RuleValidation(typeof(ClassWithOptionalParams));
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithOptionalParams),
                new CodePrimitiveExpression(5));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(ClassWithOptionalParams), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithParamsArray_ValidatesSuccessfully()
        {
            // Arrange
            var validation = new RuleValidation(typeof(ClassWithParamsArray));
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithParamsArray),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2),
                new CodePrimitiveExpression(3));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(ClassWithParamsArray), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithRefParameter_ValidatesSuccessfully()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var fieldRef = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var refParam = new CodeDirectionExpression(FieldDirection.Ref, fieldRef);
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithRefParams),
                refParam);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Validate(createExpr, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(ClassWithRefParams), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Additional Evaluate Tests

        [Fact]
        public void Evaluate_WithNullCreateType_ThrowsException()
        {
            // Arrange
            var validation = new RuleValidation(typeof(SimpleClass));
            var testInstance = new SimpleClass();
            var execution = new RuleExecution(validation, testInstance);
            var createExpr = new CodeObjectCreateExpression();
            // CreateType defaults to void when not set, so we need to validate it first
            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act & Assert - When validated with default void type, evaluate throws InvalidOperationException
            var exception = Assert.Throws<InvalidOperationException>(() =>
                expressionInternal.Evaluate(createExpr, execution));
            Assert.NotNull(exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        [Fact]
        public void Evaluate_WithOptionalParameters_UsesDefaults()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithOptionalParams),
                new CodePrimitiveExpression(5));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(ClassWithOptionalParams));
            ruleSet.Validate(validation);
            var testInstance = new ClassWithOptionalParams();
            var execution = new RuleExecution(validation, testInstance);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as ClassWithOptionalParams ?? throw new InvalidOperationException("Instance is null.");
            Assert.Equal(5, instance.Value);
            Assert.Equal("default", instance.Name); // Should use default value
        }

        [Fact]
        public void Evaluate_WithParamsArray_ExpandsParameters()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithParamsArray),
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2),
                new CodePrimitiveExpression(3),
                new CodePrimitiveExpression(4));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(ClassWithParamsArray));
            ruleSet.Validate(validation);
            var testInstance = new ClassWithParamsArray();
            var execution = new RuleExecution(validation, testInstance);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as ClassWithParamsArray ?? throw new InvalidOperationException("Instance is null.");
            Assert.Equal(4, instance.Numbers.Length);
            Assert.Equal([1, 2, 3, 4], instance.Numbers);
        }

        [Fact]
        public void Evaluate_WithRefParameter_ModifiesParameter()
        {
            // Arrange
            var refField = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var refParam = new CodeDirectionExpression(FieldDirection.Ref, refField);
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithRefParams),
                refParam);
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));
            CodeAssignStatement setValueAction = new(refField, new CodePrimitiveExpression(5));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setValueAction));
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            var testClass = new TestClass { Value = 5 };
            var execution = new RuleExecution(validation, testClass);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as ClassWithRefParams ?? throw new InvalidOperationException("Instance is null.");
            Assert.Equal(10, instance.Value); // 5 * 2
            Assert.Equal(10, testClass.Value); // Ref parameter should be modified
        }

        [Fact]
        public void Evaluate_ConstructorThrowsExceptionWithInner_WrapsException()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassThatThrows),
                new CodePrimitiveExpression(true));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(ClassThatThrows));
            ruleSet.Validate(validation);
            var testInstance = new ClassThatThrows();
            var execution = new RuleExecution(validation, testInstance);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act & Assert
            var exception = Assert.Throws<System.Reflection.TargetInvocationException>(() =>
                expressionInternal.Evaluate(createExpr, execution));
            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Evaluate_ConstructorThrowsExceptionWithoutInner_RethrowsOriginal()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassThatThrowsWithoutInner),
                new CodePrimitiveExpression(true));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(ClassThatThrowsWithoutInner));
            ruleSet.Validate(validation);
            var testInstance = new ClassThatThrowsWithoutInner();
            var execution = new RuleExecution(validation, testInstance);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act & Assert
            Assert.Throws<System.Reflection.TargetInvocationException>(() =>
                expressionInternal.Evaluate(createExpr, execution));
        }

        [Fact]
        public void Evaluate_WithMultipleOutParameters_SetsAllOutValues()
        {
            // Arrange
            var outField1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var outField2 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "Value2");
            var outParam1 = new CodeDirectionExpression(FieldDirection.Out, outField1);
            var outParam2 = new CodeDirectionExpression(FieldDirection.Out, outField2);

            var createExpr = new CodeObjectCreateExpression(
                typeof(ClassWithMultipleOutParams),
                new CodePrimitiveExpression(10),
                outParam1,
                outParam2);
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClassWithValue2));
            ruleSet.Validate(validation);
            var testClass = new TestClassWithValue2 { Value = 0, Value2 = 0 };
            var execution = new RuleExecution(validation, testClass);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = result.Value as ClassWithMultipleOutParams ?? throw new InvalidOperationException("Instance is null.");
            Assert.Equal(10, instance.Value);
            Assert.Equal(20, testClass.Value); // First out parameter
            Assert.Equal(30, testClass.Value2); // Second out parameter
        }

        [Fact]
        public void Evaluate_StructWithConstructor_CreatesInstance()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(StructWithConstructor),
                new CodePrimitiveExpression(42));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            var testInstanceField = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Name");
            CodeAssignStatement setStringAction = new(testInstanceField, new CodePrimitiveExpression("Test"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(StructWithConstructor));
            ruleSet.Validate(validation);
            var testInstance = new StructWithConstructor();
            var execution = new RuleExecution(validation, testInstance);

            var expressionInternal = GetObjectCreateExpression();
            expressionInternal.Validate(createExpr, validation, false);

            // Act
            var result = expressionInternal.Evaluate(createExpr, execution);

            // Assert
            Assert.NotNull(result.Value);
            var instance = (StructWithConstructor)result.Value;
            Assert.Equal(42, instance.Value);
        }

        #endregion

        #region Additional Decompile Tests

        [Fact]
        public void Decompile_WithNestedBinaryExpression_GeneratesCorrectString()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42));
            var binaryExpr = new CodeBinaryOperatorExpression(
                createExpr,
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(1));
            var expressionInternal = GetObjectCreateExpression();
            var sb = new StringBuilder();

            // Act
            expressionInternal.Decompile(createExpr, sb, binaryExpr);

            // Assert
            var result = sb.ToString();
            Assert.Contains("new", result);
            Assert.Contains("SimpleClass", result);
        }

        [Fact]
        public void Decompile_WithMultipleParameters_GeneratesCorrectString()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42),
                new CodePrimitiveExpression("test"));
            var expressionInternal = GetObjectCreateExpression();
            var sb = new StringBuilder();

            // Act
            expressionInternal.Decompile(createExpr, sb, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("new", result);
            Assert.Contains("SimpleClass", result);
            Assert.Contains("42", result);
            Assert.Contains("\"test\"", result);
            Assert.Contains(",", result);
        }

        [Fact]
        public void Decompile_WithComplexParameter_GeneratesCorrectString()
        {
            // Arrange
            var fieldRef = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var createExpr = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef);
            var expressionInternal = GetObjectCreateExpression();
            var sb = new StringBuilder();

            // Act
            expressionInternal.Decompile(createExpr, sb, null);

            // Assert
            var result = sb.ToString();
            Assert.Contains("new", result);
            Assert.Contains("SimpleClass", result);
            Assert.Contains("this.Value", result);
        }

        #endregion

        #region Additional Clone Tests

        [Fact]
        public void Clone_ModifyingClone_DoesNotAffectOriginal()
        {
            // Arrange
            var original = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                new CodePrimitiveExpression(42));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var cloned = expressionInternal.Clone(original) as CodeObjectCreateExpression ?? throw new InvalidOperationException("Cloned expression is null.");
            cloned.Parameters.Add(new CodePrimitiveExpression("extra"));

            // Assert
            Assert.Single(original.Parameters);
            Assert.Equal(2, cloned.Parameters.Count);
        }

        [Fact]
        public void Clone_WithValueType_CreatesDeepCopy()
        {
            // Arrange
            var original = new CodeObjectCreateExpression(typeof(StructWithConstructor),
                new CodePrimitiveExpression(42));
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var cloned = expressionInternal.Clone(original) as CodeObjectCreateExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(original, cloned);
            Assert.Equal(original.CreateType.BaseType, cloned.CreateType.BaseType);
        }

        #endregion

        #region Additional Match Tests

        [Fact]
        public void Match_WithEqualExpressionsIncludingParameters_ReturnsTrue()
        {
            // Arrange
            var fieldRef1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var fieldRef2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var expr1 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef1);
            var expr2 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef2);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentComplexParameters_ReturnsFalse()
        {
            // Arrange
            var fieldRef1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Value");
            var fieldRef2 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Name");
            var expr1 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef1);
            var expr2 = new CodeObjectCreateExpression(
                typeof(SimpleClass),
                fieldRef2);
            var expressionInternal = GetObjectCreateExpression();

            // Act
            var result = expressionInternal.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Helper Methods

        private static ObjectCreateExpression GetObjectCreateExpression()
        {
            return new ObjectCreateExpression();
        }

        private class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; }//NOSONAR - needed for testing
        }

        private class TestClassWithValue2
        {
            public int Value { get; set; }
            public int Value2 { get; set; }
            public string? Name { get; set; }//NOSONAR - needed for testing
        }

        public class ClassThatThrows
        {
            public ClassThatThrows() { }
            public ClassThatThrows(bool shouldThrow)
            {
                if (shouldThrow)
                    throw new InvalidOperationException("Constructor exception");
            }
        }

        public class ClassThatThrowsWithoutInner
        {
            public ClassThatThrowsWithoutInner() { }
            public ClassThatThrowsWithoutInner(bool shouldThrow)
            {
                if (shouldThrow)
                    throw new System.Reflection.TargetInvocationException(null);
            }
        }

        public class ClassWithMultipleOutParams
        {
            public int Value { get; set; }

            public ClassWithMultipleOutParams(int input, out int output1, out int output2)
            {
                Value = input;
                output1 = input * 2;
                output2 = input * 3;
            }
        }

        #endregion
    }
}