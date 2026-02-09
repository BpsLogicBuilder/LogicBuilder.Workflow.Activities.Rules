using System.Linq;
using System.Threading.Tasks;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleDefinitionsTest
    {
        #region Helper Classes

        // Test implementation of RuleCondition for testing purposes
        private class TestRuleCondition(string name) : RuleCondition
        {
            private string _name = name;

            public override string Name
            {
                get => _name;
                set => _name = value;
            }

            public override bool Evaluate(RuleExecution execution)
            {
                return true;
            }

            public override bool Validate(RuleValidation validation)
            {
                return true;
            }

            public override System.Collections.Generic.ICollection<string> GetDependencies(RuleValidation validation)
            {
                return [];
            }

            public override RuleCondition Clone()
            {
                return new TestRuleCondition(_name);
            }
        }

        #endregion

        #region Property Tests - Conditions

        [Fact]
        public void Conditions_WhenAccessedFirstTime_ReturnsNewCollection()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var conditions = ruleDefinitions.Conditions;

            // Assert
            Assert.NotNull(conditions);
            Assert.IsType<RuleConditionCollection>(conditions);
        }

        [Fact]
        public void Conditions_WhenAccessedMultipleTimes_ReturnsSameInstance()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var conditions1 = ruleDefinitions.Conditions;
            var conditions2 = ruleDefinitions.Conditions;

            // Assert
            Assert.Same(conditions1, conditions2);
        }

        [Fact]
        public void Conditions_InitiallyEmpty()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var conditions = ruleDefinitions.Conditions;

            // Assert
            Assert.Empty(conditions);
        }

        [Fact]
        public void Conditions_CanAddConditions()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testCondition = new TestRuleCondition("TestCondition");

            // Act
            ruleDefinitions.Conditions.Add(testCondition);

            // Assert
            Assert.Single(ruleDefinitions.Conditions);
            Assert.Contains(testCondition, ruleDefinitions.Conditions);
        }

        #endregion

        #region Property Tests - RuleSets

        [Fact]
        public void RuleSets_WhenAccessedFirstTime_ReturnsNewCollection()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var ruleSets = ruleDefinitions.RuleSets;

            // Assert
            Assert.NotNull(ruleSets);
            Assert.IsType<RuleSetCollection>(ruleSets);
        }

        [Fact]
        public void RuleSets_WhenAccessedMultipleTimes_ReturnsSameInstance()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var ruleSets1 = ruleDefinitions.RuleSets;
            var ruleSets2 = ruleDefinitions.RuleSets;

            // Assert
            Assert.Same(ruleSets1, ruleSets2);
        }

        [Fact]
        public void RuleSets_InitiallyEmpty()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act
            var ruleSets = ruleDefinitions.RuleSets;

            // Assert
            Assert.Empty(ruleSets);
        }

        [Fact]
        public void RuleSets_CanAddRuleSets()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testRuleSet = new RuleSet("TestRuleSet");

            // Act
            ruleDefinitions.RuleSets.Add(testRuleSet);

            // Assert
            Assert.Single(ruleDefinitions.RuleSets);
            Assert.Contains(testRuleSet, ruleDefinitions.RuleSets);
        }

        #endregion

        #region OnRuntimeInitialized Tests

        [Fact]
        public void OnRuntimeInitialized_InitializesConditions()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testCondition = new TestRuleCondition("TestCondition");
            ruleDefinitions.Conditions.Add(testCondition);

            // Act
            ruleDefinitions.OnRuntimeInitialized();

            // Assert
            Assert.True(ruleDefinitions.Conditions.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_InitializesRuleSets()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testRuleSet = new RuleSet("TestRuleSet");
            ruleDefinitions.RuleSets.Add(testRuleSet);

            // Act
            ruleDefinitions.OnRuntimeInitialized();

            // Assert
            Assert.True(ruleDefinitions.RuleSets.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_CalledMultipleTimes_InitializesOnlyOnce()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testCondition = new TestRuleCondition("TestCondition");
            var testRuleSet = new RuleSet("TestRuleSet");
            ruleDefinitions.Conditions.Add(testCondition);
            ruleDefinitions.RuleSets.Add(testRuleSet);

            // Act
            ruleDefinitions.OnRuntimeInitialized();
            ruleDefinitions.OnRuntimeInitialized();
            ruleDefinitions.OnRuntimeInitialized();

            // Assert - No exception should be thrown
            Assert.True(ruleDefinitions.Conditions.RuntimeMode);
            Assert.True(ruleDefinitions.RuleSets.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_WithEmptyCollections_DoesNotThrow()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();

            // Act & Assert - Should not throw
            ruleDefinitions.OnRuntimeInitialized();
        }

        [Fact]
        public void OnRuntimeInitialized_ThreadSafe()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            var testCondition = new TestRuleCondition("TestCondition");
            var testRuleSet = new RuleSet("TestRuleSet");
            ruleDefinitions.Conditions.Add(testCondition);
            ruleDefinitions.RuleSets.Add(testRuleSet);

            // Act - Call from multiple threads
            Parallel.For(0, 10, i =>
            {
                ruleDefinitions.OnRuntimeInitialized();
            });

            // Assert
            Assert.True(ruleDefinitions.Conditions.RuntimeMode);
            Assert.True(ruleDefinitions.RuleSets.RuntimeMode);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            // Arrange
            var original = new RuleDefinitions();

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
        }

        [Fact]
        public void Clone_WithEmptyCollections_CreatesEmptyClone()
        {
            // Arrange
            var original = new RuleDefinitions();

            // Act
            var clone = original.Clone();

            // Assert
            Assert.Empty(clone.Conditions);
            Assert.Empty(clone.RuleSets);
        }

        [Fact]
        public void Clone_WithConditions_ClonesConditions()
        {
            // Arrange
            var original = new RuleDefinitions();
            var condition1 = new TestRuleCondition("Condition1");
            var condition2 = new TestRuleCondition("Condition2");
            original.Conditions.Add(condition1);
            original.Conditions.Add(condition2);

            // Act
            var clone = original.Clone();

            // Assert
            Assert.Equal(2, clone.Conditions.Count);
            Assert.NotSame(original.Conditions, clone.Conditions);
            Assert.Equal("Condition1", clone.Conditions.First().Name);
            Assert.Equal("Condition2", clone.Conditions.Last().Name);
        }

        [Fact]
        public void Clone_WithRuleSets_ClonesRuleSets()
        {
            // Arrange
            var original = new RuleDefinitions();
            var ruleSet1 = new RuleSet("RuleSet1", "Description1");
            var ruleSet2 = new RuleSet("RuleSet2", "Description2");
            original.RuleSets.Add(ruleSet1);
            original.RuleSets.Add(ruleSet2);

            // Act
            var clone = original.Clone();

            // Assert
            Assert.Equal(2, clone.RuleSets.Count);
            Assert.NotSame(original.RuleSets, clone.RuleSets);
            Assert.Equal("RuleSet1", clone.RuleSets.First().Name);
            Assert.Equal("RuleSet2", clone.RuleSets.Last().Name);
            Assert.Equal("Description1", clone.RuleSets.First().Description);
            Assert.Equal("Description2", clone.RuleSets.Last().Description);
        }

        [Fact]
        public void Clone_CreatesDeepCopy_ConditionsAreIndependent()
        {
            // Arrange
            var original = new RuleDefinitions();
            var condition = new TestRuleCondition("OriginalCondition");
            original.Conditions.Add(condition);

            // Act
            var clone = original.Clone();
            clone.Conditions.First().Name = "ModifiedCondition";

            // Assert
            Assert.Equal("OriginalCondition", original.Conditions.First().Name);
            Assert.Equal("ModifiedCondition", clone.Conditions.First().Name);
        }

        [Fact]
        public void Clone_CreatesDeepCopy_RuleSetsAreIndependent()
        {
            // Arrange
            var original = new RuleDefinitions();
            var ruleSet = new RuleSet("OriginalRuleSet", "OriginalDescription");
            original.RuleSets.Add(ruleSet);

            // Act
            var clone = original.Clone();
            clone.RuleSets.First().Description = "ModifiedDescription";

            // Assert
            Assert.Equal("OriginalDescription", original.RuleSets.First().Description);
            Assert.Equal("ModifiedDescription", clone.RuleSets.First().Description);
        }

        [Fact]
        public void Clone_WithBothConditionsAndRuleSets_ClonesAll()
        {
            // Arrange
            var original = new RuleDefinitions();
            original.Conditions.Add(new TestRuleCondition("Condition1"));
            original.Conditions.Add(new TestRuleCondition("Condition2"));
            original.RuleSets.Add(new RuleSet("RuleSet1"));
            original.RuleSets.Add(new RuleSet("RuleSet2"));

            // Act
            var clone = original.Clone();

            // Assert
            Assert.Equal(2, clone.Conditions.Count);
            Assert.Equal(2, clone.RuleSets.Count);
            Assert.NotSame(original.Conditions, clone.Conditions);
            Assert.NotSame(original.RuleSets, clone.RuleSets);
        }

        [Fact]
        public void Clone_DoesNotCopyRuntimeInitializedState()
        {
            // Arrange
            var original = new RuleDefinitions();
            original.Conditions.Add(new TestRuleCondition("TestCondition"));
            original.RuleSets.Add(new RuleSet("TestRuleSet"));
            original.OnRuntimeInitialized();

            // Act
            var clone = original.Clone();

            // Assert
            Assert.False(clone.Conditions.RuntimeMode);
            Assert.False(clone.RuleSets.RuntimeMode);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void RuleDefinitions_FullLifecycle()
        {
            // Arrange
            var ruleDefinitions = new RuleDefinitions();
            
            // Act - Add items
            ruleDefinitions.Conditions.Add(new TestRuleCondition("Condition1"));
            ruleDefinitions.RuleSets.Add(new RuleSet("RuleSet1"));

            // Clone before initialization
            var cloneBeforeInit = ruleDefinitions.Clone();

            // Initialize
            ruleDefinitions.OnRuntimeInitialized();

            // Clone after initialization
            var cloneAfterInit = ruleDefinitions.Clone();

            // Assert
            Assert.Single(ruleDefinitions.Conditions);
            Assert.Single(ruleDefinitions.RuleSets);
            Assert.True(ruleDefinitions.Conditions.RuntimeMode);
            Assert.True(ruleDefinitions.RuleSets.RuntimeMode);

            Assert.Single(cloneBeforeInit.Conditions);
            Assert.Single(cloneBeforeInit.RuleSets);
            Assert.False(cloneBeforeInit.Conditions.RuntimeMode);
            Assert.False(cloneBeforeInit.RuleSets.RuntimeMode);

            Assert.Single(cloneAfterInit.Conditions);
            Assert.Single(cloneAfterInit.RuleSets);
            Assert.False(cloneAfterInit.Conditions.RuntimeMode);
            Assert.False(cloneAfterInit.RuleSets.RuntimeMode);
        }

        #endregion
    }
}