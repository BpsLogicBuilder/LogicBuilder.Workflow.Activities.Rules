using System;
using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleExpressionConditionTest
    {
        #region Constructor Tests

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            // Act
            var condition = new RuleExpressionCondition();

            // Assert
            Assert.NotNull(condition);
            Assert.Null(condition.Name);
            Assert.Null(condition.Expression);
        }

        [Fact]
        public void Constructor_WithConditionName_SetsName()
        {
            // Arrange
            var conditionName = "TestCondition";

            // Act
            var condition = new RuleExpressionCondition(conditionName);

            // Assert
            Assert.NotNull(condition);
            Assert.Equal(conditionName, condition.Name);
            Assert.Null(condition.Expression);
        }

        [Fact]
        public void Constructor_WithNullConditionName_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleExpressionCondition((string)null!));
        }

        [Fact]
        public void Constructor_WithConditionNameAndExpression_SetsBoth()
        {
            // Arrange
            var conditionName = "TestCondition";
            var expression = new CodePrimitiveExpression(true);

            // Act
            var condition = new RuleExpressionCondition(conditionName, expression);

            // Assert
            Assert.NotNull(condition);
            Assert.Equal(conditionName, condition.Name);
            Assert.Equal(expression, condition.Expression);
        }

        [Fact]
        public void Constructor_WithExpression_SetsExpression()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);

            // Act
            var condition = new RuleExpressionCondition(expression);

            // Assert
            Assert.NotNull(condition);
            Assert.Null(condition.Name);
            Assert.Equal(expression, condition.Expression);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Name_CanBeSet()
        {
            // Arrange
            var condition = new RuleExpressionCondition();
            var name = "TestName";

            // Act
            condition.Name = name;

            // Assert
            Assert.Equal(name, condition.Name);
        }

        [Fact]
        public void Name_CanBeChanged()
        {
            // Arrange
            var condition = new RuleExpressionCondition("InitialName");

            // Act
            condition.Name = "NewName";

            // Assert
            Assert.Equal("NewName", condition.Name);
        }

        [Fact]
        public void Name_ThrowsException_AfterRuntimeInitialized()
        {
            // Arrange
            var condition = new RuleExpressionCondition("TestName");
            condition.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => condition.Name = "NewName");
        }

        [Fact]
        public void Expression_CanBeSet()
        {
            // Arrange
            var condition = new RuleExpressionCondition();
            var expression = new CodePrimitiveExpression(true);

            // Act
            condition.Expression = expression;

            // Assert
            Assert.Equal(expression, condition.Expression);
        }

        [Fact]
        public void Expression_CanBeChanged()
        {
            // Arrange
            var initialExpression = new CodePrimitiveExpression(true);
            var condition = new RuleExpressionCondition(initialExpression);
            var newExpression = new CodePrimitiveExpression(false);

            // Act
            condition.Expression = newExpression;

            // Assert
            Assert.Equal(newExpression, condition.Expression);
        }

        [Fact]
        public void Expression_ThrowsException_AfterRuntimeInitialized()
        {
            // Arrange
            var condition = new RuleExpressionCondition(new CodePrimitiveExpression(true));
            condition.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => condition.Expression = new CodePrimitiveExpression(false));
        }

        #endregion

        #region OnRuntimeInitialized Tests

        [Fact]
        public void OnRuntimeInitialized_CanBeCalledMultipleTimes()
        {
            // Arrange
            var condition = new RuleExpressionCondition("TestCondition");

            // Act
            condition.OnRuntimeInitialized();
            condition.OnRuntimeInitialized();
            condition.OnRuntimeInitialized();

            // Assert - No exception should be thrown
            Assert.NotNull(condition);
        }

        [Fact]
        public void OnRuntimeInitialized_PreventsNameChange()
        {
            // Arrange
            var condition = new RuleExpressionCondition("TestCondition");

            // Act
            condition.OnRuntimeInitialized();

            // Assert
            Assert.Throws<InvalidOperationException>(() => condition.Name = "NewName");
        }

        [Fact]
        public void OnRuntimeInitialized_PreventsExpressionChange()
        {
            // Arrange
            var condition = new RuleExpressionCondition(new CodePrimitiveExpression(true));

            // Act
            condition.OnRuntimeInitialized();

            // Assert
            Assert.Throws<InvalidOperationException>(() => condition.Expression = new CodePrimitiveExpression(false));
        }

        #endregion

        #region Equals Tests

        [Fact]
        public void Equals_WithSameNameAndExpression_ReturnsTrue()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);
            var condition1 = new RuleExpressionCondition("Test", expression);
            var condition2 = new RuleExpressionCondition("Test", expression);

            // Act
            var result = condition1.Equals(condition2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentNames_ReturnsFalse()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(true);
            var condition1 = new RuleExpressionCondition("Test1", expression);
            var condition2 = new RuleExpressionCondition("Test2", expression);

            // Act
            var result = condition1.Equals(condition2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithBothNullExpressions_ReturnsTrue()
        {
            // Arrange
            var condition1 = new RuleExpressionCondition("Test");
            var condition2 = new RuleExpressionCondition("Test");

            // Act
            var result = condition1.Equals(condition2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithOneNullExpression_ReturnsFalse()
        {
            // Arrange
            var condition1 = new RuleExpressionCondition("Test");
            var condition2 = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var result = condition1.Equals(condition2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithNullObject_ReturnsFalse()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");

            // Act
            var result = condition.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");
            var obj = new object();

            // Act
            var result = condition.Equals(obj);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithSameInstance_ReturnsTrue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var result = condition.Equals(condition);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_ReturnsValue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");

            // Act
            var hashCode = condition.GetHashCode();

            // Assert
            Assert.NotEqual(0, hashCode);
        }

        [Fact]
        public void GetHashCode_ConsistentForSameInstance()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");

            // Act
            var hashCode1 = condition.GetHashCode();
            var hashCode2 = condition.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        #endregion

        #region ToString Tests

        [Fact]
        public void ToString_WithNullExpression_ReturnsEmptyString()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");

            // Act
            var result = condition.ToString();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ToString_WithPrimitiveExpression_ReturnsDecompiledString()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var result = condition.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.Contains("true", result.ToLower());
        }

        [Fact]
        public void ToString_WithComplexExpression_ReturnsDecompiledString()
        {
            // Arrange
            var left = new CodePrimitiveExpression(5);
            var right = new CodePrimitiveExpression(10);
            var binaryOp = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
            var condition = new RuleExpressionCondition("Test", binaryOp);

            // Act
            var result = condition.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithNullValidation_ThrowsArgumentNullException()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => condition.Validate(null));
        }

        [Fact]
        public void Validate_WithNullExpression_ReturnsFalse()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = condition.Validate(validation);

            // Assert
            Assert.False(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void Validate_WithValidExpression_ReturnsTrue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var result = condition.Validate(validation);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithNullExpression_ReturnsTrue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");
            var execution = new RuleExecution(new RuleValidation(typeof(TestClass), null), new TestClass());

            // Act
            var result = condition.Evaluate(execution);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Evaluate_WithTrueExpression_ReturnsTrue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));
            var execution = new RuleExecution(new RuleValidation(typeof(TestClass), null), new TestClass());

            // Act
            var result = condition.Evaluate(execution);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Evaluate_WithFalseExpression_ReturnsFalse()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(false));
            var execution = new RuleExecution(new RuleValidation(typeof(TestClass), null), new TestClass());

            // Act
            var result = condition.Evaluate(execution);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region GetDependencies Tests

        [Fact]
        public void GetDependencies_WithNullExpression_ReturnsEmptyCollection()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test");
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var dependencies = condition.GetDependencies(validation);

            // Assert
            Assert.NotNull(dependencies);
            Assert.Empty(dependencies);
        }

        [Fact]
        public void GetDependencies_WithSimpleExpression_ReturnsEmptyCollection()
        {
            // Arrange
            var condition = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));
            var validation = new RuleValidation(typeof(TestClass), null);

            // Act
            var dependencies = condition.GetDependencies(validation);

            // Assert
            Assert.NotNull(dependencies);
            Assert.Empty(dependencies);
        }

        [Fact]
        public void GetDependencies_WithPropertyReference_ReturnsDependencies()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var propertyRef = new CodePropertyReferenceExpression(thisRef, "Value");
            var condition = new RuleExpressionCondition("Test", propertyRef);
            CodeAssignStatement setTextAction = new(propertyRef, new CodePrimitiveExpression("SomeText"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setTextAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            // Act
            var dependencies = condition.GetDependencies(validation);

            // Assert
            Assert.NotNull(dependencies);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            // Arrange
            var original = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
        }

        [Fact]
        public void Clone_CopiesName()
        {
            // Arrange
            var original = new RuleExpressionCondition("TestCondition", new CodePrimitiveExpression(true));

            // Act
            var clone = (RuleExpressionCondition)original.Clone();

            // Assert
            Assert.Equal(original.Name, clone.Name);
        }

        [Fact]
        public void Clone_CopiesExpression()
        {
            // Arrange
            var original = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var clone = (RuleExpressionCondition)original.Clone();

            // Assert
            Assert.NotNull(clone.Expression);
            Assert.NotSame(original.Expression, clone.Expression);
        }

        [Fact]
        public void Clone_ResetsRuntimeInitialized()
        {
            // Arrange
            var original = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));
            original.OnRuntimeInitialized();

            // Act
            var clone = (RuleExpressionCondition)original.Clone();

            // Assert - Should be able to modify name without exception
            clone.Name = "NewName";
            Assert.Equal("NewName", clone.Name);
        }

        [Fact]
        public void Clone_WithNullExpression_ClonesSuccessfully()
        {
            // Arrange
            var original = new RuleExpressionCondition("Test");

            // Act
            var clone = (RuleExpressionCondition)original.Clone();

            // Assert
            Assert.NotNull(clone);
            Assert.Null(clone.Expression);
            Assert.Equal(original.Name, clone.Name);
        }

        [Fact]
        public void Clone_CreatesIndependentCopy()
        {
            // Arrange
            var original = new RuleExpressionCondition("Test", new CodePrimitiveExpression(true));

            // Act
            var clone = (RuleExpressionCondition)original.Clone();
            clone.Name = "Modified";

            // Assert
            Assert.Equal("Test", original.Name);
            Assert.Equal("Modified", clone.Name);
        }

        #endregion

        #region Test Helper Class

        private class TestClass
        {
            public int Value { get; set; } = 42;
            public bool Flag { get; set; } = true;
            public string Text { get; set; } = "test";
        }

        #endregion
    }
}