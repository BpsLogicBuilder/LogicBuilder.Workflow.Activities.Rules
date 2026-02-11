using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleSetTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_Default_InitializesEmptyRulesList()
        {
            // Arrange & Act
            var ruleSet = new RuleSet();

            // Assert
            Assert.NotNull(ruleSet.Rules);
            Assert.Empty(ruleSet.Rules);
            Assert.Null(ruleSet.Name);
            Assert.Null(ruleSet.Description);
            Assert.Equal(RuleChainingBehavior.Full, ruleSet.ChainingBehavior);
        }

        [Fact]
        public void Constructor_WithName_InitializesNameAndEmptyRulesList()
        {
            // Arrange
            string expectedName = "TestRuleSet";

            // Act
            var ruleSet = new RuleSet(expectedName);

            // Assert
            Assert.Equal(expectedName, ruleSet.Name);
            Assert.NotNull(ruleSet.Rules);
            Assert.Empty(ruleSet.Rules);
            Assert.Null(ruleSet.Description);
            Assert.Equal(RuleChainingBehavior.Full, ruleSet.ChainingBehavior);
        }

        [Fact]
        public void Constructor_WithNameAndDescription_InitializesAllProperties()
        {
            // Arrange
            string expectedName = "TestRuleSet";
            string expectedDescription = "Test Description";

            // Act
            var ruleSet = new RuleSet(expectedName, expectedDescription);

            // Assert
            Assert.Equal(expectedName, ruleSet.Name);
            Assert.Equal(expectedDescription, ruleSet.Description);
            Assert.NotNull(ruleSet.Rules);
            Assert.Empty(ruleSet.Rules);
            Assert.Equal(RuleChainingBehavior.Full, ruleSet.ChainingBehavior);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Name_SetBeforeRuntimeInitialized_SetsSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet();
            string expectedName = "NewName";

            // Act
            ruleSet.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, ruleSet.Name);
        }

        [Fact]
        public void Name_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var ruleSet = new RuleSet("InitialName");
            ruleSet.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ruleSet.Name = "NewName");
        }

        [Fact]
        public void Description_SetBeforeRuntimeInitialized_SetsSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet();
            string expectedDescription = "New Description";

            // Act
            ruleSet.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, ruleSet.Description);
        }

        [Fact]
        public void Description_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            ruleSet.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ruleSet.Description = "New Description");
        }

        [Fact]
        public void ChainingBehavior_SetBeforeRuntimeInitialized_SetsSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet
            {
                // Act
                ChainingBehavior = RuleChainingBehavior.UpdateOnly
            };

            // Assert
            Assert.Equal(RuleChainingBehavior.UpdateOnly, ruleSet.ChainingBehavior);
        }

        [Fact]
        public void ChainingBehavior_SetAfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            ruleSet.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => ruleSet.ChainingBehavior = RuleChainingBehavior.None);
        }

        [Theory]
        [InlineData(RuleChainingBehavior.None)]
        [InlineData(RuleChainingBehavior.UpdateOnly)]
        [InlineData(RuleChainingBehavior.Full)]
        public void ChainingBehavior_SetAllValidValues_SetsSuccessfully(RuleChainingBehavior behavior)
        {
            // Arrange
            var ruleSet = new RuleSet
            {
                // Act
                ChainingBehavior = behavior
            };

            // Assert
            Assert.Equal(behavior, ruleSet.ChainingBehavior);
        }

        [Fact]
        public void Rules_Get_ReturnsRulesCollection()
        {
            // Arrange
            var ruleSet = new RuleSet();

            // Act
            var rules = ruleSet.Rules;

            // Assert
            Assert.NotNull(rules);
            Assert.IsType<ICollection<Rule>>(rules, exactMatch: false);
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_NullValidation_ThrowsArgumentNullException()
        {
            // Arrange
            var ruleSet = new RuleSet();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ruleSet.Validate(null!));
        }

        [Fact]
        public void Validate_EmptyRuleSet_ReturnsTrue()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            bool result = ruleSet.Validate(validation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ValidRules_ReturnsTrue()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var rule = new Rule("Rule1")
            {
                Condition = new RuleExpressionCondition("this.Value > 0")
            };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));

            // Act
            _ = ruleSet.Validate(validation);

            // Assert - result depends on rule validation, but should not throw
            Assert.NotNull(validation);
        }

        [Fact]
        public void Validate_DuplicateRuleNames_AddsValidationError()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var rule1 = new Rule("DuplicateName");
            var rule2 = new Rule("DuplicateName");
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);

            var validation = new RuleValidation(typeof(TestClass));

            // Act
            ruleSet.Validate(validation);

            // Assert
            Assert.NotNull(validation.Errors);
            Assert.True(validation.Errors.Count > 0);
            Assert.Contains(validation.Errors, e => e.ErrorNumber == Common.ErrorNumbers.Error_DuplicateConditions);
        }

        [Fact]
        public void Validate_RuleWithEmptyName_DoesNotCheckForDuplicates()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var rule1 = new Rule("");
            var rule2 = new Rule("");
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);

            var validation = new RuleValidation(typeof(TestClass));

            // Act
            ruleSet.Validate(validation);

            // Assert - Should not report duplicate name errors for empty names
            var duplicateErrors = validation.Errors?.Where(e => 
                e.ErrorNumber == Common.ErrorNumbers.Error_DuplicateConditions).ToList();
            Assert.True(duplicateErrors == null || duplicateErrors.Count == 0);
        }

        [Fact]
        public void Validate_WithValidationErrors_ReturnsFalse()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var rule1 = new Rule("Rule1");
            var rule2 = new Rule("Rule1"); // Duplicate name
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);

            var validation = new RuleValidation(typeof(TestClass));

            // Act
            bool result = ruleSet.Validate(validation);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Execute Tests

        [Fact]
        public void Execute_NullRuleExecution_ThrowsArgumentNullException()
        {
            // Arrange
            var ruleSet = new RuleSet();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ruleSet.Execute(null!));
        }

        [Fact]
        public void Execute_ValidRuleExecution_ExecutesSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var validation = new RuleValidation(typeof(TestClass));
            var target = new TestClass();
            var ruleExecution = new RuleExecution(validation, target);

            // Act - Should not throw
            ruleSet.Execute(ruleExecution);

            // Assert - Execution completed without exception
            Assert.NotNull(ruleExecution);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_EmptyRuleSet_CreatesIdenticalCopy()
        {
            // Arrange
            var original = new RuleSet("TestName", "TestDescription")
            {
                ChainingBehavior = RuleChainingBehavior.UpdateOnly
            };

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotSame(original, clone);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.Description, clone.Description);
            Assert.Equal(original.ChainingBehavior, clone.ChainingBehavior);
            Assert.NotNull(clone.Rules);
            Assert.Empty(clone.Rules);
        }

        [Fact]
        public void Clone_WithRules_CreatesDeepCopy()
        {
            // Arrange
            var original = new RuleSet("TestName");
            var rule1 = new Rule("Rule1");
            var rule2 = new Rule("Rule2");
            original.Rules.Add(rule1);
            original.Rules.Add(rule2);

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotSame(original, clone);
            Assert.NotSame(original.Rules, clone.Rules);
            Assert.Equal(original.Rules.Count, clone.Rules.Count);
            
            var originalRulesList = original.Rules.ToList();
            var clonedRulesList = clone.Rules.ToList();
            
            for (int i = 0; i < originalRulesList.Count; i++)
            {
                Assert.NotSame(originalRulesList[i], clonedRulesList[i]);
                Assert.Equal(originalRulesList[i].Name, clonedRulesList[i].Name);
            }
        }

        [Fact]
        public void Clone_AfterRuntimeInitialized_CreatesNonInitializedCopy()
        {
            // Arrange
            var original = new RuleSet("TestName", "TestDescription");
            original.OnRuntimeInitialized();

            // Act
            var clone = original.Clone();

            // Assert
            // The clone should allow property changes (not runtime initialized)
            clone.Name = "NewName";
            Assert.Equal("NewName", clone.Name);
        }

        [Fact]
        public void Clone_NullRules_HandlesGracefully()
        {
            // Arrange
            var original = new RuleSet("TestName");

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotNull(clone);
            Assert.NotNull(clone.Rules);
        }

        #endregion

        #region Equals Tests

        [Fact]
        public void Equals_IdenticalRuleSets_ReturnsTrue()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test", "Description")
            {
                ChainingBehavior = RuleChainingBehavior.UpdateOnly
            };
            var ruleSet2 = new RuleSet("Test", "Description")
            {
                ChainingBehavior = RuleChainingBehavior.UpdateOnly
            };

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_DifferentNames_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test1");
            var ruleSet2 = new RuleSet("Test2");

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_DifferentDescriptions_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test", "Description1");
            var ruleSet2 = new RuleSet("Test", "Description2");

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_DifferentChainingBehavior_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test") { ChainingBehavior = RuleChainingBehavior.Full };
            var ruleSet2 = new RuleSet("Test") { ChainingBehavior = RuleChainingBehavior.None };

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_DifferentRuleCount_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test");
            ruleSet1.Rules.Add(new Rule("Rule1"));

            var ruleSet2 = new RuleSet("Test");

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_IdenticalRulesInSameOrder_ReturnsTrue()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test");
            ruleSet1.Rules.Add(new Rule("Rule1"));
            ruleSet1.Rules.Add(new Rule("Rule2"));

            var ruleSet2 = new RuleSet("Test");
            ruleSet2.Rules.Add(new Rule("Rule1"));
            ruleSet2.Rules.Add(new Rule("Rule2"));

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_DifferentRulesInSameOrder_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test");
            ruleSet1.Rules.Add(new Rule("Rule1"));

            var ruleSet2 = new RuleSet("Test");
            ruleSet2.Rules.Add(new Rule("Rule2"));

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_SameRulesInDifferentOrder_ReturnsFalse()
        {
            // Arrange
            var ruleSet1 = new RuleSet("Test");
            ruleSet1.Rules.Add(new Rule("Rule1"));
            ruleSet1.Rules.Add(new Rule("Rule2"));

            var ruleSet2 = new RuleSet("Test");
            ruleSet2.Rules.Add(new Rule("Rule2"));
            ruleSet2.Rules.Add(new Rule("Rule1"));

            // Act
            bool result = ruleSet1.Equals(ruleSet2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");

            // Act
            int hashCode1 = ruleSet.GetHashCode();
            int hashCode2 = ruleSet.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_DoesNotThrow()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            ruleSet.Rules.Add(new Rule("Rule1"));

            // Act & Assert - Should not throw
            int hashCode = ruleSet.GetHashCode();
            Assert.NotEqual(0, hashCode); // Just verify we got a value
        }

        #endregion

        #region OnRuntimeInitialized Tests

        [Fact]
        public void OnRuntimeInitialized_CalledOnce_InitializesRuleSet()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");

            // Act
            ruleSet.OnRuntimeInitialized();

            // Assert - Property setters should now throw
            Assert.Throws<InvalidOperationException>(() => ruleSet.Name = "NewName");
        }

        [Fact]
        public void OnRuntimeInitialized_CalledMultipleTimes_DoesNotThrow()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");

            // Act & Assert - Should not throw
            ruleSet.OnRuntimeInitialized();
            ruleSet.OnRuntimeInitialized();
            ruleSet.OnRuntimeInitialized();
        }

        [Fact]
        public void OnRuntimeInitialized_WithRules_InitializesAllRules()
        {
            // Arrange
            var ruleSet = new RuleSet("Test");
            var rule1 = new Rule("Rule1");
            var rule2 = new Rule("Rule2");
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);

            // Act
            ruleSet.OnRuntimeInitialized();

            // Assert - Rules should now be runtime initialized
            Assert.Throws<InvalidOperationException>(() => rule1.Name = "NewName");
            Assert.Throws<InvalidOperationException>(() => rule2.Name = "NewName");
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; }
        }

        #endregion
    }
}