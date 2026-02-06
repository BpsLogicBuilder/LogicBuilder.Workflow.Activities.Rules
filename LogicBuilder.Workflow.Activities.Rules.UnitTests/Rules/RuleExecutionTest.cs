using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleExecutionTest
    {
        #region Helper Classes

        private class TestClass
        {
            public int TestProperty { get; set; } = 42;
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));

            // Act
            RuleExecution ruleExecution = new(validation, testObject);

            // Assert
            Assert.NotNull(ruleExecution);
            Assert.Same(testObject, ruleExecution.ThisObject);
            Assert.Same(validation, ruleExecution.Validation);
            Assert.False(ruleExecution.Halted);
        }

        [Fact]
        public void Constructor_WithNullValidation_ThrowsArgumentNullException()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = null!;

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new RuleExecution(validation, testObject));
            Assert.Equal("validation", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithNullThisObject_ThrowsArgumentNullException()
        {
            // Arrange
            RuleValidation validation = new(typeof(TestClass));
            TestClass testObject = null!;

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new RuleExecution(validation, testObject));
            Assert.Equal("thisObject", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithMismatchedTypes_ThrowsInvalidOperationException()
        {
            // Arrange
            RuleValidation validation = new(typeof(TestClass));
            string testObject = "wrong type";

            // Act & Assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                new RuleExecution(validation, testObject));
            Assert.Contains("TestClass", exception.Message);
            Assert.Contains("The type used for validation (\"LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules.RuleExecutionTest.TestClass\") is not compatible with the type of the object specified", exception.Message);
        }

        #endregion

        #region ThisObject Property Tests

        [Fact]
        public void ThisObject_ReturnsCorrectObject()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act
            object result = ruleExecution.ThisObject;

            // Assert
            Assert.Same(testObject, result);
        }

        [Fact]
        public void ThisObject_ReturnsSameInstanceAfterMultipleCalls()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act
            object result1 = ruleExecution.ThisObject;
            object result2 = ruleExecution.ThisObject;

            // Assert
            Assert.Same(result1, result2);
            Assert.Same(testObject, result1);
        }

        #endregion

        #region Validation Property Tests

        [Fact]
        public void Validation_Getter_ReturnsCorrectValidation()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act
            RuleValidation result = ruleExecution.Validation;

            // Assert
            Assert.Same(validation, result);
        }

        [Fact]
        public void Validation_Setter_SetsNewValidation()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation1 = new(typeof(TestClass));
            RuleValidation validation2 = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation1, testObject)
            {
                // Act
                Validation = validation2
            };

            // Assert
            Assert.Same(validation2, ruleExecution.Validation);
            Assert.NotSame(validation1, ruleExecution.Validation);
        }

        [Fact]
        public void Validation_Setter_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                ruleExecution.Validation = null!);
            Assert.Equal("value", exception.ParamName);
        }

        [Fact]
        public void Validation_Setter_ReplacesValidationSuccessfully()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation1 = new(typeof(TestClass));
            RuleValidation validation2 = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation1, testObject)
            {
                // Act
                Validation = validation2
            };
            RuleValidation result = ruleExecution.Validation;

            // Assert
            Assert.Same(validation2, result);
        }

        #endregion

        #region Halted Property Tests

        [Fact]
        public void Halted_DefaultValue_IsFalse()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act
            bool result = ruleExecution.Halted;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Halted_Setter_SetsToTrue()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject)
            {
                // Act
                Halted = true
            };

            // Assert
            Assert.True(ruleExecution.Halted);
        }

        [Fact]
        public void Halted_Setter_SetsToFalse()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject)
            {
                Halted = true
            };

            // Act
            ruleExecution.Halted = false;

            // Assert
            Assert.False(ruleExecution.Halted);
        }

        [Fact]
        public void Halted_ToggleMultipleTimes_WorksCorrectly()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation, testObject);

            // Act & Assert
            Assert.False(ruleExecution.Halted);

            ruleExecution.Halted = true;
            Assert.True(ruleExecution.Halted);

            ruleExecution.Halted = false;
            Assert.False(ruleExecution.Halted);

            ruleExecution.Halted = true;
            Assert.True(ruleExecution.Halted);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void RuleExecution_WithComplexObject_WorksCorrectly()
        {
            // Arrange
            TestClass testObject = new() { TestProperty = 100 };
            RuleValidation validation = new(typeof(TestClass));

            // Act
            RuleExecution ruleExecution = new(validation, testObject);

            // Assert
            Assert.NotNull(ruleExecution);
            Assert.Same(testObject, ruleExecution.ThisObject);
            Assert.Equal(100, ((TestClass)ruleExecution.ThisObject).TestProperty);
        }

        [Fact]
        public void RuleExecution_StateChanges_AreIndependent()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation = new(typeof(TestClass));
            RuleExecution ruleExecution1 = new(validation, testObject);
            RuleExecution ruleExecution2 = new(validation, testObject);

            // Act
            ruleExecution1.Halted = true;

            // Assert
            Assert.True(ruleExecution1.Halted);
            Assert.False(ruleExecution2.Halted);
        }

        [Fact]
        public void RuleExecution_WithValueType_WorksCorrectly()
        {
            // Arrange
            int testObject = 42;
            RuleValidation validation = new(typeof(int));

            // Act
            RuleExecution ruleExecution = new(validation, testObject);

            // Assert
            Assert.NotNull(ruleExecution);
            Assert.Equal(42, ruleExecution.ThisObject);
        }

        [Fact]
        public void RuleExecution_ValidationCanBeChanged_AfterConstruction()
        {
            // Arrange
            TestClass testObject = new();
            RuleValidation validation1 = new(typeof(TestClass));
            RuleValidation validation2 = new(typeof(TestClass));
            RuleExecution ruleExecution = new(validation1, testObject);

            // Act
            RuleValidation initialValidation = ruleExecution.Validation;
            ruleExecution.Validation = validation2;
            RuleValidation updatedValidation = ruleExecution.Validation;

            // Assert
            Assert.Same(validation1, initialValidation);
            Assert.Same(validation2, updatedValidation);
            Assert.NotSame(initialValidation, updatedValidation);
        }

        #endregion
    }
}