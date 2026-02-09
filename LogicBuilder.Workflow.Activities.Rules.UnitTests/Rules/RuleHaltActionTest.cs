using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleHaltActionTest
    {
        #region Constructor and Basic Tests

        [Fact]
        public void Constructor_CreatesValidInstance()
        {
            // Arrange & Act
            var haltAction = new RuleHaltAction();

            // Assert
            Assert.NotNull(haltAction);
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithValidValidator_ReturnsTrue()
        {
            // Arrange
            var haltAction = new RuleHaltAction();
            var validation = new RuleValidation(typeof(TestEntity), null);

            // Act
            bool result = haltAction.Validate(validation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithNullValidator_ReturnsTrue()
        {
            // Arrange
            var haltAction = new RuleHaltAction();

            // Act
            bool result = haltAction.Validate(null);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Execute Tests

        [Fact]
        public void Execute_SetsHaltedToTrue()
        {
            // Arrange
            var haltAction = new RuleHaltAction();
            var testEntity = new TestEntity();
            var validation = new RuleValidation(typeof(TestEntity), null);
            var execution = new RuleExecution(validation, testEntity);

            // Act
            haltAction.Execute(execution);

            // Assert
            Assert.True(execution.Halted);
        }

        [Fact]
        public void Execute_WithNullContext_ThrowsArgumentNullException()
        {
            // Arrange
            var haltAction = new RuleHaltAction();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => haltAction.Execute(null));
            Assert.Equal("context", exception.ParamName);
        }

        [Fact]
        public void Execute_MultipleInvocations_KeepsHaltedTrue()
        {
            // Arrange
            var haltAction = new RuleHaltAction();
            var testEntity = new TestEntity();
            var validation = new RuleValidation(typeof(TestEntity), null);
            var execution = new RuleExecution(validation, testEntity);

            // Act
            haltAction.Execute(execution);
            haltAction.Execute(execution);

            // Assert
            Assert.True(execution.Halted);
        }

        #endregion

        #region GetSideEffects Tests

        [Fact]
        public void GetSideEffects_ReturnsNull()
        {
            // Arrange
            var haltAction = new RuleHaltAction();
            var validation = new RuleValidation(typeof(TestEntity), null);

            // Act
            var sideEffects = haltAction.GetSideEffects(validation);

            // Assert
            Assert.Null(sideEffects);
        }

        [Fact]
        public void GetSideEffects_WithNullValidation_ReturnsNull()
        {
            // Arrange
            var haltAction = new RuleHaltAction();

            // Act
            var sideEffects = haltAction.GetSideEffects(null);

            // Assert
            Assert.Null(sideEffects);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            // Arrange
            var originalAction = new RuleHaltAction();

            // Act
            var clonedAction = originalAction.Clone();

            // Assert
            Assert.NotNull(clonedAction);
            Assert.IsType<RuleHaltAction>(clonedAction);
            Assert.NotSame(originalAction, clonedAction);
        }

        [Fact]
        public void Clone_CreatesIndependentInstance()
        {
            // Arrange
            var originalAction = new RuleHaltAction();
            var testEntity = new TestEntity();
            var validation = new RuleValidation(typeof(TestEntity), null);
            var execution = new RuleExecution(validation, testEntity);

            // Act
            var clonedAction = (RuleHaltAction)originalAction.Clone();
            clonedAction.Execute(execution);

            // Assert
            Assert.True(execution.Halted);
            Assert.IsType<RuleHaltAction>(clonedAction);
        }

        #endregion

        #region ToString Tests

        [Fact]
        public void ToString_ReturnsHalt()
        {
            // Arrange
            var haltAction = new RuleHaltAction();

            // Act
            string result = haltAction.ToString();

            // Assert
            Assert.Equal("Halt", result);
        }

        #endregion

        #region Equals Tests

        [Fact]
        public void Equals_WithAnotherRuleHaltAction_ReturnsTrue()
        {
            // Arrange
            var action1 = new RuleHaltAction();
            var action2 = new RuleHaltAction();

            // Act
            bool result = action1.Equals(action2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithSameInstance_ReturnsTrue()
        {
            // Arrange
            var action = new RuleHaltAction();

            // Act
            bool result = action.Equals(action);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            var action = new RuleHaltAction();

            // Act
            bool result = action.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            var action = new RuleHaltAction();
            var otherObject = new object();

            // Act
            bool result = action.Equals(otherObject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentRuleActionType_ReturnsFalse()
        {
            // Arrange
            var haltAction = new RuleHaltAction();
            var updateAction = new RuleUpdateAction("SomeProperty");

            // Act
            bool result = haltAction.Equals(updateAction);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_ReturnsSameValueForMultipleCalls()
        {
            // Arrange
            var action = new RuleHaltAction();

            // Act
            int hash1 = action.GetHashCode();
            int hash2 = action.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact(Skip = "TODO: Use ToString.GetHashCode() for hashcode.")]
        public void GetHashCode_ForTwoInstances_ReturnsDifferentValues()
        {
            // Arrange
            var action1 = new RuleHaltAction();
            var action2 = new RuleHaltAction();

            // Act
            int hash1 = action1.GetHashCode();
            int hash2 = action2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        #endregion

        #region Test Helper Classes

        private class TestEntity
        {
            public string? Name { get; set; }
            public int Value { get; set; }
            public bool Flag { get; set; }
        }

        #endregion
    }
}