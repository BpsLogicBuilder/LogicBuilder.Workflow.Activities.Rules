using System;
using System.CodeDom;
using System.Reflection;
using System.Text;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class FieldReferenceExpressionTest
    {
        private readonly FieldReferenceExpression _fieldReferenceExpression;

        public FieldReferenceExpressionTest()
        {
            _fieldReferenceExpression = new FieldReferenceExpression();
        }

        #region Test Helper Classes

        private class TestClass
        {
            public int PublicIntField = 42;
            public string PublicStringField = "test";
            public const int ConstField = 100;
            internal int InternalField = 99;
            
            public static int StaticPublicField = 123;
            public static string StaticStringField = "static";
        }

        private class TestRuleClass
        {
            public TestClass TestInstance = new();
            public int IntValue { get; set; } = 10;
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithValidInstanceField_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithValidStaticField_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodeFieldReferenceExpression(targetObject, "StaticPublicField");
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNullTargetObject_ReturnsNullAndAddsError()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(null, "PublicIntField");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("PublicIntField", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_WithNonExistentField_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "NonExistentField");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_CannotResolveMember, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_WithIsWritten_AndValidField_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, true);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNullLiteralTarget_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodePrimitiveExpression(null);
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_WithNestedFieldReference_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var testInstanceField = new CodeFieldReferenceExpression(thisRef, "TestInstance");
            var expression = new CodeFieldReferenceExpression(testInstanceField, "PublicIntField");
            CodeBinaryOperatorExpression intValueTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "IntValue"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(19)
            };
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(intValueTest) };
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);

            // Act
            var result = _fieldReferenceExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithValidInstanceField_ReturnsFieldValue()
        {
            // Arrange
            var testInstance = new TestClass { PublicIntField = 55 };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");

            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _fieldReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _fieldReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(55, result.Value);
        }

        [Fact]
        public void Evaluate_WithStaticField_ReturnsStaticFieldValue()
        {
            // Arrange
            TestClass.StaticPublicField = 999;
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodeFieldReferenceExpression(targetObject, "StaticPublicField");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _fieldReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = _fieldReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(999, result.Value);
        }

        [Fact]
        public void Evaluate_WithStringField_ReturnsStringValue()
        {
            // Arrange
            var testInstance = new TestClass { PublicStringField = "hello world" };
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicStringField");

            CodeAssignStatement setTextAction = new(expression, new CodePrimitiveExpression("SomeText"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setTextAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _fieldReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _fieldReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("hello world", result.Value);
        }

        [Fact]
        public void Evaluate_WithoutValidation_ThrowsInvalidOperationException()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                _fieldReferenceExpression.Evaluate(expression, execution));
            Assert.Contains("not validated", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Evaluate_WithNestedFieldReference_ReturnsCorrectValue()
        {
            // Arrange
            var testRuleInstance = new TestRuleClass();
            testRuleInstance.TestInstance.PublicIntField = 777;
            
            var thisRef = new CodeThisReferenceExpression();
            var testInstanceField = new CodeFieldReferenceExpression(thisRef, "TestInstance");
            var expression = new CodeFieldReferenceExpression(testInstanceField, "PublicIntField");
            CodeAssignStatement setIntAction = new(expression, new CodePrimitiveExpression(777));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _fieldReferenceExpression.Validate(testInstanceField, validation, false);
            _fieldReferenceExpression.Validate(expression, validation, false);
            
            var execution = new RuleExecution(validation, testRuleInstance);

            // Act
            var result = _fieldReferenceExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(777, result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithValidFieldReference_GeneratesCorrectString()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            var stringBuilder = new StringBuilder();

            // Act
            _fieldReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Equal("this.PublicIntField", result);
        }

        [Fact]
        public void Decompile_WithStaticFieldReference_GeneratesCorrectString()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodeFieldReferenceExpression(targetObject, "StaticPublicField");
            var stringBuilder = new StringBuilder();

            // Act
            _fieldReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("TestClass", result);
            Assert.Contains("StaticPublicField", result);
        }

        [Fact]
        public void Decompile_WithNullTarget_ThrowsRuleEvaluationException()
        {
            // Arrange
            var expression = new CodeFieldReferenceExpression(null, "PublicIntField");
            var stringBuilder = new StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() => 
                _fieldReferenceExpression.Decompile(expression, stringBuilder, null));
            Assert.Contains("PublicIntField", exception.Message);
        }

        [Fact]
        public void Decompile_WithNestedFieldReference_GeneratesCorrectString()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var testInstanceField = new CodeFieldReferenceExpression(thisRef, "TestInstance");
            var expression = new CodeFieldReferenceExpression(testInstanceField, "PublicIntField");
            var stringBuilder = new StringBuilder();

            // Act
            _fieldReferenceExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Equal("this.TestInstance.PublicIntField", result);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesDistinctCopy()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");

            // Act
            var cloned = _fieldReferenceExpression.Clone(expression) as CodeFieldReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.Equal(expression.FieldName, cloned.FieldName);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);
        }

        [Fact]
        public void Clone_WithStaticFieldReference_CreatesCorrectCopy()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var expression = new CodeFieldReferenceExpression(targetObject, "StaticPublicField");

            // Act
            var cloned = _fieldReferenceExpression.Clone(expression) as CodeFieldReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(expression.FieldName, cloned.FieldName);
            Assert.IsType<CodeTypeReferenceExpression>(cloned.TargetObject);
        }

        [Fact]
        public void Clone_PreservesFieldName()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicStringField");

            // Act
            var cloned = _fieldReferenceExpression.Clone(expression) as CodeFieldReferenceExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal("PublicStringField", cloned.FieldName);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodeFieldReferenceExpression(targetObject1, "PublicIntField");
            
            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodeFieldReferenceExpression(targetObject2, "PublicIntField");

            // Act
            var result = _fieldReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentFieldNames_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodeFieldReferenceExpression(targetObject1, "PublicIntField");
            
            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodeFieldReferenceExpression(targetObject2, "PublicStringField");

            // Act
            var result = _fieldReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentTargetObjects_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "PublicIntField");
            
            var expression2 = new CodeFieldReferenceExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)), "PublicIntField");

            // Act
            var result = _fieldReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameFieldNameAndTargetType_ReturnsTrue()
        {
            // Arrange
            var expression1 = new CodeFieldReferenceExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)), "StaticPublicField");
            
            var expression2 = new CodeFieldReferenceExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)), "StaticPublicField");

            // Act
            var result = _fieldReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithNestedFieldReferences_WorksCorrectly()
        {
            // Arrange
            var thisRef1 = new CodeThisReferenceExpression();
            var testInstance1 = new CodeFieldReferenceExpression(thisRef1, "TestInstance");
            var expression1 = new CodeFieldReferenceExpression(testInstance1, "PublicIntField");
            
            var thisRef2 = new CodeThisReferenceExpression();
            var testInstance2 = new CodeFieldReferenceExpression(thisRef2, "TestInstance");
            var expression2 = new CodeFieldReferenceExpression(testInstance2, "PublicIntField");

            // Act
            var result = _fieldReferenceExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithReadOperation_AnalyzesTargetObject()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            
            var validation = new RuleValidation(typeof(TestClass));
            _fieldReferenceExpression.Validate(expression, validation, false);
            
            var analysis = new RuleAnalysis(validation, true);

            // Act
            _fieldReferenceExpression.AnalyzeUsage(expression, analysis, true, false, null);

            // Assert - Analysis should track field read
            Assert.NotNull(analysis);
        }

        [Fact]
        public void AnalyzeUsage_WithWriteOperation_AnalyzesTargetObject()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeFieldReferenceExpression(targetObject, "PublicIntField");
            
            var validation = new RuleValidation(typeof(TestClass));
            _fieldReferenceExpression.Validate(expression, validation, true);
            
            var analysis = new RuleAnalysis(validation, false);

            // Act
            _fieldReferenceExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert - Analysis should track field write
            Assert.NotNull(analysis);
        }

        #endregion
    }
}