using System;
using System.Collections.Generic;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleStateTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidRule_StoresRule()
        {
            // Arrange
            var rule = new Rule("TestRule");

            // Act
            var ruleState = new RuleState(rule);

            // Assert
            Assert.Same(rule, ruleState.Rule);
        }

        [Fact]
        public void Constructor_InitializesCollectionsAsNull()
        {
            // Arrange
            var rule = new Rule("TestRule");

            // Act
            var ruleState = new RuleState(rule);

            // Assert
            Assert.Null(ruleState.ThenActionsActiveRules);
            Assert.Null(ruleState.ElseActionsActiveRules);
        }

        #endregion

        #region ThenActionsActiveRules Property Tests

        [Fact]
        public void ThenActionsActiveRules_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var ruleState = new RuleState(rule);
            var activeRules = new List<int> { 1, 2, 3 };

            // Act
            ruleState.ThenActionsActiveRules = activeRules;

            // Assert
            Assert.Same(activeRules, ruleState.ThenActionsActiveRules);
        }

        [Fact]
        public void ThenActionsActiveRules_SetToNull_ReturnsNull()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var ruleState = new RuleState(rule)
            {
                ThenActionsActiveRules = [1, 2, 3]
            };

            // Act
            ruleState.ThenActionsActiveRules = null;

            // Assert
            Assert.Null(ruleState.ThenActionsActiveRules);
        }

        #endregion

        #region ElseActionsActiveRules Property Tests

        [Fact]
        public void ElseActionsActiveRules_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var ruleState = new RuleState(rule);
            var activeRules = new List<int> { 4, 5, 6 };

            // Act
            ruleState.ElseActionsActiveRules = activeRules;

            // Assert
            Assert.Same(activeRules, ruleState.ElseActionsActiveRules);
        }

        [Fact]
        public void ElseActionsActiveRules_SetToNull_ReturnsNull()
        {
            // Arrange
            var rule = new Rule("TestRule");
            var ruleState = new RuleState(rule)
            {
                ElseActionsActiveRules = [4, 5, 6]
            };

            // Act
            ruleState.ElseActionsActiveRules = null;

            // Assert
            Assert.Null(ruleState.ElseActionsActiveRules);
        }

        #endregion

        #region CompareTo Tests

        [Fact]
        public void CompareTo_WithHigherPriority_ReturnsNegative()
        {
            // Arrange
            var rule1 = new Rule("Rule1") { Priority = 10 };
            var rule2 = new Rule("Rule2") { Priority = 5 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result < 0, "RuleState with higher priority should compare as less (for descending sort)");
        }

        [Fact]
        public void CompareTo_WithLowerPriority_ReturnsPositive()
        {
            // Arrange
            var rule1 = new Rule("Rule1") { Priority = 5 };
            var rule2 = new Rule("Rule2") { Priority = 10 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result > 0, "RuleState with lower priority should compare as greater (for descending sort)");
        }

        [Fact]
        public void CompareTo_WithSamePriority_ComparesNameAscending()
        {
            // Arrange
            var rule1 = new Rule("RuleA") { Priority = 10 };
            var rule2 = new Rule("RuleB") { Priority = 10 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result < 0, "When priorities are equal, names should be compared in ascending order (A > B)");
        }

        [Fact]
        public void CompareTo_WithSamePriorityReversedNames_ReturnsCorrectOrder()
        {
            // Arrange
            var rule1 = new Rule("RuleZ") { Priority = 10 };
            var rule2 = new Rule("RuleA") { Priority = 10 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result > 0, "When priorities are equal, names should be compared in ascending order (Z < A)");
        }

        [Fact]
        public void CompareTo_WithSamePriorityAndSameName_ReturnsZero()
        {
            // Arrange
            var rule1 = new Rule("SameRule") { Priority = 10 };
            var rule2 = new Rule("SameRule") { Priority = 10 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CompareTo_Sorting_PrioritizesByPriorityThenName()
        {
            // Arrange
            var ruleStates = new List<RuleState>
            {
                new(new Rule("RuleC") { Priority = 5 }),
                new(new Rule("RuleA") { Priority = 10 }),
                new(new Rule("RuleB") { Priority = 10 }),
                new(new Rule("RuleD") { Priority = 5 }),
                new(new Rule("RuleE") { Priority = 15 })
            };

            // Act
            ruleStates.Sort();

            // Assert
            Assert.Equal("RuleE", ruleStates[0].Rule.Name); // Priority 15
            Assert.Equal("RuleA", ruleStates[1].Rule.Name); // Priority 10, name A
            Assert.Equal("RuleB", ruleStates[2].Rule.Name); // Priority 10, name B
            Assert.Equal("RuleC", ruleStates[3].Rule.Name); // Priority 5, name C
            Assert.Equal("RuleD", ruleStates[4].Rule.Name); // Priority 5, name D
        }

        [Fact]
        public void CompareTo_WithNegativePriorities_WorksCorrectly()
        {
            // Arrange
            var rule1 = new Rule("Rule1") { Priority = -5 };
            var rule2 = new Rule("Rule2") { Priority = 5 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result > 0, "RuleState with negative priority should compare as greater than positive priority");
        }

        [Fact]
        public void CompareTo_WithZeroPriority_WorksCorrectly()
        {
            // Arrange
            var rule1 = new Rule("Rule1") { Priority = 0 };
            var rule2 = new Rule("Rule2") { Priority = 10 };
            var ruleState1 = new RuleState(rule1);
            var ruleState2 = new RuleState(rule2);

            // Act
            int result = ((IComparable)ruleState1).CompareTo(ruleState2);

            // Assert
            Assert.True(result > 0, "RuleState with zero priority should compare as greater than positive priority");
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void RuleState_CompleteWorkflow_AllPropertiesWork()
        {
            // Arrange
            var rule = new Rule("IntegrationRule") { Priority = 42 };
            var ruleState = new RuleState(rule);
            var thenRules = new List<int> { 1, 2, 3 };
            var elseRules = new List<int> { 4, 5, 6 };

            // Act
            ruleState.ThenActionsActiveRules = thenRules;
            ruleState.ElseActionsActiveRules = elseRules;

            // Assert
            Assert.Same(rule, ruleState.Rule);
            Assert.Same(thenRules, ruleState.ThenActionsActiveRules);
            Assert.Same(elseRules, ruleState.ElseActionsActiveRules);
            Assert.Equal(42, ruleState.Rule.Priority);
            Assert.Equal("IntegrationRule", ruleState.Rule.Name);
        }

        #endregion
    }
}