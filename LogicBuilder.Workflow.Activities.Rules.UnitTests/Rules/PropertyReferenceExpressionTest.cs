using System;
using System.CodeDom;
using System.Reflection;
using System.Text;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class PropertyReferenceExpressionTest
    {
        private readonly PropertyReferenceExpression _propertyReferenceExpression;

        public PropertyReferenceExpressionTest()
        {
            _propertyReferenceExpression = new PropertyReferenceExpression();
        }

        #region Test Helper Classes

        private class TestClass
        {
            public int PublicIntProperty { get; set; } = 42;
            public string PublicStringProperty { get; set; } = "test";
            internal int InternalProperty { get; set; } = 99;
            
            public int ReadOnlyProperty { get; } = 100;
            
            private int _writeOnlyValue;
            public int WriteOnlyProperty 
            { 
                set { _writeOnlyValue = value; } 
            }
            
            public static int StaticPublicProperty { get; set; } = 123;
            public static string StaticStringProperty { get; set; } = "static";

            public object? NullableProperty { get; set; }
        }

        private class TestRuleClass
        {
            public TestClass? TestInstance { get; set; }
            public int IntValue { get; set; } = 10;
        }

        private class TestClassWithRuleAttributes
        {
            [RuleRead("SomeField")]
            public int PropertyWithRuleRead { get; set; }

            [RuleWrite("AnotherField")]
            public int PropertyWithRuleWrite { get; set; }
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithValidInstanceProperty_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithValidStaticProperty_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodePropertyReferenceExpression(targetObject, "StaticPublicProperty");
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNullTargetObject_ReturnsNullAndAddsError()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(null, "PublicIntProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("PublicIntProperty", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_WithNonExistentProperty_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "NonExistentProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("NonExistentProperty", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_WithNullLiteralTargetType_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodePrimitiveExpression(null);
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("PublicIntProperty", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_ReadOnlyProperty_WhenWritten_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "ReadOnlyProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_WriteOnlyProperty_WhenRead_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "WriteOnlyProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_InternalProperty_WithAllowInternalMembers_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "InternalProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithStringProperty_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicStringProperty");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNestedProperty_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), 
                "TestInstance");
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithSimpleProperty_AnalyzesTargetObject()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, true);
            
            var analysis = new RuleAnalysis(validation, true);

            // Act
            _propertyReferenceExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            Assert.NotEmpty(analysis.GetSymbols());
        }

        [Fact]
        public void AnalyzeUsage_WhenRead_AnalyzesAsRead()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var analysis = new RuleAnalysis(validation, false);

            // Act
            _propertyReferenceExpression.AnalyzeUsage(expression, analysis, true, false, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
            Assert.Contains(symbols, s => s.Contains("PublicIntProperty"));
        }

        [Fact]
        public void AnalyzeUsage_WhenWritten_AnalyzesAsWritten()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, true);
            
            var analysis = new RuleAnalysis(validation, true);

            // Act
            _propertyReferenceExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
            Assert.Contains(symbols, s => s.Contains("PublicIntProperty"));
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithValidProperty_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass { PublicIntProperty = 99 };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(44));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(99, result.Value);
        }

        [Fact]
        public void Evaluate_WithStringProperty_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass { PublicStringProperty = "hello" };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicStringProperty");
            CodeAssignStatement setStringAction = new(expression, new CodePrimitiveExpression("hello"));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setStringAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("hello", result.Value);
        }

        [Fact]
        public void Evaluate_WithNestedProperty_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestRuleClass 
            { 
                TestInstance = new TestClass { PublicIntProperty = 55 } 
            };
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), 
                "TestInstance");
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(55, result.Value);
        }

        [Fact]
        public void Evaluate_WithNullTarget_ThrowsException()
        {
            // Arrange
            var testInstance = new TestRuleClass { TestInstance = null };
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), 
                "TestInstance");
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() => 
                _propertyReferenceExpression.Evaluate(expression, execution).Value);
        }

        [Fact]
        public void Evaluate_WithoutValidation_ThrowsInvalidOperationException()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                _propertyReferenceExpression.Evaluate(expression, execution));
        }

        [Fact]
        public void Evaluate_PropertyReturnsNull_ReturnsNullValue()
        {
            // Arrange
            var testInstance = new TestClass { NullableProperty = null };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "NullableProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Evaluate_StaticProperty_ReturnsCorrectValue()
        {
            // Arrange
            TestClass.StaticPublicProperty = 777;
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodePropertyReferenceExpression(targetObject, "StaticPublicProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, new TestRuleClass());

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(777, result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithValidExpression_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var stringBuilder = new StringBuilder();

            // Act
            _propertyReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("PublicIntProperty", result);
            Assert.Contains(".", result);
        }

        [Fact]
        public void Decompile_WithNestedExpression_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), 
                "TestInstance");
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            var stringBuilder = new StringBuilder();

            // Act
            _propertyReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("TestInstance", result);
            Assert.Contains("PublicIntProperty", result);
        }

        [Fact]
        public void Decompile_WithNullTargetObject_ThrowsException()
        {
            // Arrange
            var expression = new CodePropertyReferenceExpression(null, "PublicIntProperty");
            var stringBuilder = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() => 
                _propertyReferenceExpression.Decompile(expression, stringBuilder, null));
        }

        [Fact]
        public void Decompile_WithStaticProperty_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodePropertyReferenceExpression(targetObject, "StaticPublicProperty");
            var stringBuilder = new StringBuilder();

            // Act
            _propertyReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("TestClass", result);
            Assert.Contains("StaticPublicProperty", result);
            Assert.Contains(".", result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithValidExpression_ReturnsIdenticalCopy()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");

            // Act
            var cloned = _propertyReferenceExpression.Clone(expression) as CodePropertyReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(expression.PropertyName, cloned.PropertyName);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);
        }

        [Fact]
        public void Clone_WithNestedExpression_ReturnsDeepCopy()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), 
                "TestInstance");
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");

            // Act
            var cloned = _propertyReferenceExpression.Clone(expression) as CodePropertyReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(expression.PropertyName, cloned.PropertyName);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);
            
            var originalTarget = expression.TargetObject as CodePropertyReferenceExpression;
            var clonedTarget = cloned.TargetObject as CodePropertyReferenceExpression;
            Assert.NotNull(originalTarget);
            Assert.NotNull(clonedTarget);
            Assert.Equal(originalTarget.PropertyName, clonedTarget.PropertyName);
            Assert.NotSame(originalTarget, clonedTarget);
        }

        [Fact]
        public void Clone_WithStaticPropertyExpression_ReturnsIdenticalCopy()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodePropertyReferenceExpression(targetObject, "StaticPublicProperty");

            // Act
            var cloned = _propertyReferenceExpression.Clone(expression) as CodePropertyReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(expression.PropertyName, cloned.PropertyName);
            Assert.NotSame(expression, cloned);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodePropertyReferenceExpression(targetObject1, "PublicIntProperty");
            
            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodePropertyReferenceExpression(targetObject2, "PublicIntProperty");

            // Act
            var result = _propertyReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentPropertyNames_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodePropertyReferenceExpression(targetObject1, "PublicIntProperty");
            
            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodePropertyReferenceExpression(targetObject2, "PublicStringProperty");

            // Act
            var result = _propertyReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentTargetObjects_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "PublicIntProperty");
            
            var expression2 = new CodePropertyReferenceExpression(
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TestInstance"),
                "PublicIntProperty");

            // Act
            var result = _propertyReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithNestedIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePropertyReferenceExpression(
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TestInstance"),
                "PublicIntProperty");
            
            var expression2 = new CodePropertyReferenceExpression(
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TestInstance"),
                "PublicIntProperty");

            // Act
            var result = _propertyReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithStaticPropertyExpressions_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodePropertyReferenceExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)), "StaticPublicProperty");
            
            var expression2 = new CodePropertyReferenceExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)), "StaticPublicProperty");

            // Act
            var result = _propertyReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void Validate_WithComplexNestedPath_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(), 
                    "TestInstance"),
                "PublicStringProperty");
            // This creates: this.TestInstance.PublicStringProperty (which returns string)
            // But we're trying to access a property on that string
            var expression = new CodePropertyReferenceExpression(targetObject, "Length");
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _propertyReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Evaluate_CanSetPropertyValue_ReturnsUpdatedValue()
        {
            // Arrange
            var testInstance = new TestClass { PublicIntProperty = 10 };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodePropertyReferenceExpression(targetObject, "PublicIntProperty");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _propertyReferenceExpression.Validate(expression, validation, true);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _propertyReferenceExpression.Evaluate(expression, execution);
            result.Value = 50;

            // Assert
            Assert.Equal(50, testInstance.PublicIntProperty);
        }

        #endregion
    }
}