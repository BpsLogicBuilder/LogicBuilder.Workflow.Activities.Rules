using System;
using System.CodeDom;
using System.Collections.Generic;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_Default_CreatesEmptyRule()
        {
            // Arrange & Act
            var rule = new Rule();

            // Assert
            Assert.Null(rule.Name);
            Assert.Null(rule.Description);
            Assert.Equal(0, rule.Priority);
            Assert.Equal(RuleReevaluationBehavior.Always, rule.ReevaluationBehavior);
            Assert.True(rule.Active);
            Assert.Null(rule.Condition);
            Assert.NotNull(rule.ThenActions);
            Assert.Empty(rule.ThenActions);
            Assert.NotNull(rule.ElseActions);
            Assert.Empty(rule.ElseActions);
        }

        [Fact]
        public void Constructor_WithName_SetsName()
        {
            // Arrange
            const string ruleName = "TestRule";

            // Act
            var rule = new Rule(ruleName);

            // Assert
            Assert.Equal(ruleName, rule.Name);
            Assert.Null(rule.Condition);
            Assert.NotNull(rule.ThenActions);
            Assert.Empty(rule.ThenActions);
        }

        [Fact]
        public void Constructor_WithNameConditionAndThenActions_SetsProperties()
        {
            // Arrange
            const string ruleName = "TestRule";
            var condition = new RuleExpressionCondition("true");
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            var thenActions = new List<IRuleAction> { new RuleStatementAction(methodInvoke) };

            // Act
            var rule = new Rule(ruleName, condition, thenActions);

            // Assert
            Assert.Equal(ruleName, rule.Name);
            Assert.Same(condition, rule.Condition);
            Assert.Same(thenActions, rule.ThenActions);
            Assert.NotNull(rule.ElseActions);
            Assert.Empty(rule.ElseActions);
        }

        [Fact]
        public void Constructor_WithAllParameters_SetsAllProperties()
        {
            // Arrange
            const string ruleName = "TestRule";
            var condition = new RuleExpressionCondition("true");
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            var thenActions = new List<IRuleAction> { new RuleStatementAction(methodInvoke) };
            var elseActions = new List<IRuleAction> { new RuleStatementAction(methodInvoke) };

            // Act
            var rule = new Rule(ruleName, condition, thenActions, elseActions);

            // Assert
            Assert.Equal(ruleName, rule.Name);
            Assert.Same(condition, rule.Condition);
            Assert.Same(thenActions, rule.ThenActions);
            Assert.Same(elseActions, rule.ElseActions);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Name_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule();
            const string newName = "NewRuleName";

            // Act
            rule.Name = newName;

            // Assert
            Assert.Equal(newName, rule.Name);
        }

        [Fact]
        public void Description_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule();
            const string description = "Test description";

            // Act
            rule.Description = description;

            // Assert
            Assert.Equal(description, rule.Description);
        }

        [Fact]
        public void Priority_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule();
            const int priority = 100;

            // Act
            rule.Priority = priority;

            // Assert
            Assert.Equal(priority, rule.Priority);
        }

        [Fact]
        public void ReevaluationBehavior_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule
            {
                // Act
                ReevaluationBehavior = RuleReevaluationBehavior.Never
            };

            // Assert
            Assert.Equal(RuleReevaluationBehavior.Never, rule.ReevaluationBehavior);
        }

        [Fact]
        public void Active_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule
            {
                // Act
                Active = false
            };

            // Assert
            Assert.False(rule.Active);
        }

        [Fact]
        public void Condition_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule();
            var condition = new RuleExpressionCondition("true");

            // Act
            rule.Condition = condition;

            // Assert
            Assert.Same(condition, rule.Condition);
        }

        [Fact]
        public void ThenActions_Get_ReturnsNonNullList()
        {
            // Arrange
            var rule = new Rule();

            // Act
            var thenActions = rule.ThenActions;

            // Assert
            Assert.NotNull(thenActions);
            Assert.Empty(thenActions);
        }

        [Fact]
        public void ElseActions_Get_ReturnsNonNullList()
        {
            // Arrange
            var rule = new Rule();

            // Act
            var elseActions = rule.ElseActions;

            // Assert
            Assert.NotNull(elseActions);
            Assert.Empty(elseActions);
        }

        #endregion

        #region Runtime Initialization Tests

        [Fact]
        public void Name_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.Name = "NewName");
        }

        [Fact]
        public void Description_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.Description = "New description");
        }

        [Fact]
        public void Priority_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.Priority = 100);
        }

        [Fact]
        public void ReevaluationBehavior_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.ReevaluationBehavior = RuleReevaluationBehavior.Never);
        }

        [Fact]
        public void Active_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.Active = false);
        }

        [Fact]
        public void Condition_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var rule = new Rule("TestRule");
            rule.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => rule.Condition = new RuleExpressionCondition("true"));
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesDeepCopy()
        {
            // Arrange
            var originalRule = new Rule("TestRule")
            {
                Description = "Test description",
                Priority = 50,
                ReevaluationBehavior = RuleReevaluationBehavior.Never,
                Active = false,
                Condition = new RuleExpressionCondition("x > 5")
            };
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            originalRule.ThenActions.Add(new RuleStatementAction(methodInvoke));
            originalRule.ElseActions.Add(new RuleStatementAction(methodInvoke));
            originalRule.OnRuntimeInitialized();

            // Act
            var clonedRule = originalRule.Clone();

            // Assert
            Assert.NotSame(originalRule, clonedRule);
            Assert.Equal(originalRule.Name, clonedRule.Name);
            Assert.Equal(originalRule.Description, clonedRule.Description);
            Assert.Equal(originalRule.Priority, clonedRule.Priority);
            Assert.Equal(originalRule.ReevaluationBehavior, clonedRule.ReevaluationBehavior);
            Assert.Equal(originalRule.Active, clonedRule.Active);
            
            // Verify deep copy of condition
            Assert.NotSame(originalRule.Condition, clonedRule.Condition);
            Assert.Equal(originalRule.Condition, clonedRule.Condition);
            
            // Verify deep copy of actions
            Assert.NotSame(originalRule.ThenActions, clonedRule.ThenActions);
            Assert.Equal(originalRule.ThenActions.Count, clonedRule.ThenActions.Count);
            Assert.NotSame(originalRule.ElseActions, clonedRule.ElseActions);
            Assert.Equal(originalRule.ElseActions.Count, clonedRule.ElseActions.Count);
        }

        [Fact]
        public void Clone_ClonedRuleCanBeModified()
        {
            // Arrange
            var originalRule = new Rule("TestRule");
            originalRule.OnRuntimeInitialized();

            // Act
            var clonedRule = originalRule.Clone();

            // Assert - cloned rule should be modifiable even if original is runtime initialized
            clonedRule.Name = "NewName";
            clonedRule.Priority = 100;
            Assert.Equal("NewName", clonedRule.Name);
            Assert.Equal(100, clonedRule.Priority);
        }

        [Fact]
        public void Clone_WithNullCondition_ClonesSuccessfully()
        {
            // Arrange
            var originalRule = new Rule("TestRule");

            // Act
            var clonedRule = originalRule.Clone();

            // Assert
            Assert.Null(clonedRule.Condition);
        }

        [Fact]
        public void Clone_WithNullActions_ClonesSuccessfully()
        {
            // Arrange
            var originalRule = new Rule("TestRule");
            // Don't access ThenActions or ElseActions properties to keep them null

            // Act
            var clonedRule = originalRule.Clone();

            // Assert
            Assert.NotNull(clonedRule);
            Assert.Equal(originalRule.Name, clonedRule.Name);
        }

        #endregion

        #region Equals Tests

        [Fact]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var obj = new object();

            // Act & Assert
            Assert.False(rule.Equals(obj));
        }

        [Fact]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("Rule1");
            var rule2 = new Rule("Rule2");

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentDescription_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { Description = "Desc1" };
            var rule2 = new Rule("TestRule") { Description = "Desc2" };

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentPriority_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { Priority = 1 };
            var rule2 = new Rule("TestRule") { Priority = 2 };

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentReevaluationBehavior_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { ReevaluationBehavior = RuleReevaluationBehavior.Always };
            var rule2 = new Rule("TestRule") { ReevaluationBehavior = RuleReevaluationBehavior.Never };

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentActive_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { Active = true };
            var rule2 = new Rule("TestRule") { Active = false };

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentCondition_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { Condition = new RuleExpressionCondition("x > 5") };
            var rule2 = new Rule("TestRule") { Condition = new RuleExpressionCondition("x < 5") };

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_OneConditionNull_ReturnsFalse()
        {
            // Arrange
            var rule1 = new Rule("TestRule") { Condition = new RuleExpressionCondition("x > 5") };
            var rule2 = new Rule("TestRule");

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_BothConditionsNull_ReturnsTrue()
        {
            // Arrange
            var rule1 = new Rule("TestRule");
            var rule2 = new Rule("TestRule");

            // Act & Assert
            Assert.True(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentThenActions_ReturnsFalse()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            var methodInvoke2 = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeOtherText")
            );
            var rule1 = new Rule("TestRule");
            rule1.ThenActions.Add(new RuleStatementAction(methodInvoke));
            var rule2 = new Rule("TestRule");
            rule2.ThenActions.Add(new RuleStatementAction(methodInvoke2));

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_DifferentElseActions_ReturnsFalse()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            var methodInvoke2 = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeOtherText")
            );
            var rule1 = new Rule("TestRule");
            rule1.ElseActions.Add(new RuleStatementAction(methodInvoke));
            var rule2 = new Rule("TestRule");
            rule2.ElseActions.Add(new RuleStatementAction(methodInvoke2));

            // Act & Assert
            Assert.False(rule1.Equals(rule2));
        }

        [Fact]
        public void Equals_IdenticalRules_ReturnsTrue()
        {
            // Arrange
            var condition = new RuleExpressionCondition("x > 5");
            var rule1 = new Rule("TestRule")
            {
                Description = "Test",
                Priority = 10,
                ReevaluationBehavior = RuleReevaluationBehavior.Never,
                Active = true,
                Condition = condition
            };
            var methodInvoke = new CodeMethodInvokeExpression
            (
                new CodeTypeReferenceExpression(typeof(System.Diagnostics.Debug)),
                "Writeline",
                new CodePrimitiveExpression("SomeText")
            );
            rule1.ThenActions.Add(new RuleStatementAction(methodInvoke));
            rule1.ElseActions.Add(new RuleStatementAction(methodInvoke));

            var rule2 = new Rule("TestRule")
            {
                Description = "Test",
                Priority = 10,
                ReevaluationBehavior = RuleReevaluationBehavior.Never,
                Active = true,
                Condition = condition
            };

            rule2.ThenActions.Add(new RuleStatementAction(methodInvoke));
            rule2.ElseActions.Add(new RuleStatementAction(methodInvoke));

            // Act & Assert
            Assert.True(rule1.Equals(rule2));
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            // Arrange
            var rule = new Rule("TestRule");

            // Act
            var hash1 = rule.GetHashCode();
            var hash2 = rule.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ReturnsValue()
        {
            // Arrange
            var rule = new Rule("TestRule");

            // Act
            var hashCode = rule.GetHashCode();

            // Assert
            Assert.NotEqual(0, hashCode);
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithoutName_AddsValidationError()
        {
            // Arrange
            var rule = new Rule();
            var validation = new RuleValidation(typeof(object));

            // Act
            rule.Validate(validation);

            // Assert
            Assert.True(validation.Errors.Count > 0);
            Assert.Contains(validation.Errors, e => e.ErrorNumber == Common.ErrorNumbers.Error_InvalidConditionName);
        }

        [Fact]
        public void Validate_WithoutCondition_AddsValidationError()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var validation = new RuleValidation(typeof(object), null);

            // Act
            rule.Validate(validation);

            // Assert
            Assert.True(validation.Errors.Count > 0);
            Assert.Contains(validation.Errors, e => e.ErrorNumber == Common.ErrorNumbers.Error_MissingRuleCondition);
        }

        [Fact]
        public void Validate_ErrorsContainRuleName()
        {
            // Arrange
            const string ruleName = "MyTestRule";
            var rule = new Rule(ruleName);
            var validation = new RuleValidation(typeof(object), null);

            // Act
            rule.Validate(validation);

            // Assert
            Assert.True(validation.Errors.Count > 0);
            Assert.All(validation.Errors, error => Assert.Contains(ruleName, error.ErrorText));
        }

        [Fact]
        public void Validate_PopulatesErrorsByRuleName()
        {
            // Arrange
            const string ruleName = "MyTestRule";
            var rule = new Rule(ruleName);
            var validation = new RuleValidation(typeof(object), null);

            // Act
            rule.Validate(validation);

            // Assert
            Assert.True(validation.ErrorsByRuleName.ContainsKey(ruleName));
            Assert.True(validation.ErrorsByRuleName[ruleName].Count > 0);
        }

        #endregion
    }
}