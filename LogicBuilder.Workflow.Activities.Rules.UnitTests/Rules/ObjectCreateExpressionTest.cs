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
            var instance = result.Value as SimpleClass;
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

        #region Helper Methods

        private static ObjectCreateExpression GetObjectCreateExpression()
        {
            return new ObjectCreateExpression();
            //var type = typeof(RuleExpressionWalker).Assembly.GetType(
            //    "LogicBuilder.Workflow.Activities.Rules.ObjectCreateExpression");
            //return (RuleExpressionInternal)Activator.CreateInstance(type, true);
        }

        private class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; }
        }

        #endregion
    }
}