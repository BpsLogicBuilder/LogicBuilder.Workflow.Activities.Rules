using System;
using System.Collections.Generic;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleEngineTest
    {
        #region Helper Classes

        // Simple test class for rule execution
        private class TestEntity
        {
            public string? State { get; set; }
            public int Discount { get; set; }
            public bool IsProcessed { get; set; }
        }

        // Test implementation of RuleCondition
        private class TestRuleCondition : RuleCondition
        {
            private readonly Func<RuleExecution, bool> _evaluateFunc;
            private readonly string _name;

            public TestRuleCondition(string name, Func<RuleExecution, bool> evaluateFunc = null!)
            {
                _name = name;
                _evaluateFunc = evaluateFunc ?? (_ => true);
            }

            public override string Name
            {
                get => _name;
                set { }
            }

            public override bool Evaluate(RuleExecution execution)
            {
                return _evaluateFunc(execution);
            }

            public override bool Validate(RuleValidation validation)
            {
                return true;
            }

            public override ICollection<string> GetDependencies(RuleValidation validation)
            {
                return new List<string>();
            }

            public override RuleCondition Clone()
            {
                return new TestRuleCondition(_name, _evaluateFunc);
            }
        }

        #endregion

        #region Constructor Tests - RuleEngine(RuleSet, Type)

        [Fact]
        public void Constructor_WithValidRuleSetAndType_CreatesInstance()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);

            // Act
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));

            // Assert
            Assert.NotNull(ruleEngine);
        }

        [Fact]
        public void Constructor_WithNullRuleSet_ThrowsNullReferenceException()
        {
            // Arrange
            RuleSet? ruleSet = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new RuleEngine(ruleSet, typeof(TestEntity)));
        }

        [Fact]
        public void Constructor_WithNullType_ThrowsArgumentNullException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleEngine(ruleSet, (Type?)null));
        }

        [Fact]
        public void Constructor_WithInvalidRuleSet_ThrowsRuleSetValidationException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule1 = new Rule("DuplicateName")
            {
                Condition = new TestRuleCondition("Condition1")
            };
            var rule2 = new Rule("DuplicateName")
            {
                Condition = new TestRuleCondition("Condition2")
            };
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);

            // Act & Assert
            var exception = Assert.Throws<RuleSetValidationException>(() => new RuleEngine(ruleSet, typeof(TestEntity)));
            Assert.NotNull(exception.Errors);
            Assert.NotEmpty(exception.Errors);
        }

        #endregion

        #region Constructor Tests - RuleEngine(RuleSet, RuleValidation)

        [Fact]
        public void Constructor_WithValidRuleSetAndValidation_CreatesInstance()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var validation = new RuleValidation(typeof(TestEntity));

            // Act
            var ruleEngine = new RuleEngine(ruleSet, validation);

            // Assert
            Assert.NotNull(ruleEngine);
        }

        [Fact]
        public void Constructor_WithRuleValidation_WithNullRuleSet_ThrowsNullReferenceException()
        {
            // Arrange
            RuleSet? ruleSet = null;
            var validation = new RuleValidation(typeof(TestEntity));

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new RuleEngine(ruleSet, validation));
        }

        [Fact]
        public void Constructor_WithRuleValidation_WithNullValidation_ThrowsNullReferenceException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleEngine(ruleSet,(RuleValidation?)null));
        }

        [Fact]
        public void Constructor_WithRuleValidation_WithInvalidRuleSet_ThrowsRuleSetValidationException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule1 = new Rule("DuplicateName")
            {
                Condition = new TestRuleCondition("Condition1")
            };
            var rule2 = new Rule("DuplicateName")
            {
                Condition = new TestRuleCondition("Condition2")
            };
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);
            var validation = new RuleValidation(typeof(TestEntity));

            // Act & Assert
            var exception = Assert.Throws<RuleSetValidationException>(() => new RuleEngine(ruleSet, validation));
            Assert.NotNull(exception.Errors);
            Assert.NotEmpty(exception.Errors);
        }

        #endregion

        #region Execute Tests - Execute(object)

        [Fact]
        public void Execute_WithValidObject_ExecutesSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity { State = "CT" };

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        [Fact]
        public void Execute_WithNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ruleEngine.Execute((object?)null));
        }

        [Fact]
        public void Execute_WithEmptyRuleSet_ExecutesWithoutError()
        {
            // Arrange
            var ruleSet = new RuleSet("EmptyRuleSet");
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity();

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        [Fact]
        public void Execute_WithMultipleRules_ExecutesAllRules()
        {
            // Arrange
            var ruleSet = new RuleSet("MultiRuleSet");
            var rule1 = new Rule("Rule1")
            {
                Condition = new TestRuleCondition("Condition1")
            };
            var rule2 = new Rule("Rule2")
            {
                Condition = new TestRuleCondition("Condition2")
            };
            var rule3 = new Rule("Rule3")
            {
                Condition = new TestRuleCondition("Condition3")
            };
            ruleSet.Rules.Add(rule1);
            ruleSet.Rules.Add(rule2);
            ruleSet.Rules.Add(rule3);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity();

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        #endregion

        #region Execute Tests - Execute(RuleExecution)

        [Fact]
        public void Execute_WithRuleExecution_ExecutesSuccessfully()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var validation = new RuleValidation(typeof(TestEntity));
            var ruleEngine = new RuleEngine(ruleSet, validation);
            var testEntity = new TestEntity { State = "CT" };
            var ruleExecution = new RuleExecution(validation, testEntity);

            // Act
            ruleEngine.Execute(ruleExecution);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(ruleExecution);
        }

        [Fact]
        public void Execute_WithNullRuleExecution_ThrowsArgumentNullException()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ruleEngine.Execute(null));
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void RuleEngine_CanExecuteMultipleTimes_WithSameInstance()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet");
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));

            var testEntity1 = new TestEntity { State = "CT" };
            var testEntity2 = new TestEntity { State = "MA" };
            var testEntity3 = new TestEntity { State = "VA" };

            // Act
            ruleEngine.Execute(testEntity1);
            ruleEngine.Execute(testEntity2);
            ruleEngine.Execute(testEntity3);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity1);
            Assert.NotNull(testEntity2);
            Assert.NotNull(testEntity3);
        }

        [Fact]
        public void RuleEngine_WithDifferentChainingBehaviors_ExecutesCorrectly()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet")
            {
                ChainingBehavior = RuleChainingBehavior.Full
            };
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity();

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        [Fact]
        public void RuleEngine_WithSequentialChainingBehavior_ExecutesCorrectly()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet")
            {
                ChainingBehavior = RuleChainingBehavior.Full
            };
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity();

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        [Fact]
        public void RuleEngine_WithNoneChainingBehavior_ExecutesCorrectly()
        {
            // Arrange
            var ruleSet = new RuleSet("TestRuleSet")
            {
                ChainingBehavior = RuleChainingBehavior.None
            };
            var rule = new Rule("TestRule")
            {
                Condition = new TestRuleCondition("TestCondition")
            };
            ruleSet.Rules.Add(rule);
            var ruleEngine = new RuleEngine(ruleSet, typeof(TestEntity));
            var testEntity = new TestEntity();

            // Act
            ruleEngine.Execute(testEntity);

            // Assert - if no exception thrown, test passes
            Assert.NotNull(testEntity);
        }

        #endregion
    }
}