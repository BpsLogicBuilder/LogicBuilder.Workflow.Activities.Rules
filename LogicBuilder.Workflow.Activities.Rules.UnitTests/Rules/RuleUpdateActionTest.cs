using System;
using System.CodeDom;
using System.Collections.Generic;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleUpdateActionTest
    {
        #region Helper Classes
        private class TestClass
        {
#pragma warning disable CS0649
            public int IntField;
#pragma warning restore CS0649
            public string? StringProperty { get; set; }
            public NestedClass? Nested { get; set; }
            public int[]? IntArray { get; set; }
        }

        private class NestedClass
        {
#pragma warning disable CS0649
            public double DoubleField;
#pragma warning restore CS0649
            public string? NestedProperty { get; set; }
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_WithPath_SetsPathProperty()
        {
            // Arrange
            string expectedPath = "this/IntField";

            // Act
            var action = new RuleUpdateAction(expectedPath);

            // Assert
            Assert.Equal(expectedPath, action.Path);
        }

        [Fact]
        public void Constructor_Default_PathIsNull()
        {
            // Act
            var action = new RuleUpdateAction();

            // Assert
            Assert.Null(action.Path);
        }
        #endregion

        #region Property Tests
        [Fact]
        public void Path_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var action = new RuleUpdateAction();
            string expectedPath = "this/StringProperty";

            // Act
            action.Path = expectedPath;

            // Assert
            Assert.Equal(expectedPath, action.Path);
        }
        #endregion

        #region Validate Tests
        [Fact]
        public void Validate_NullValidator_ThrowsArgumentNullException()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => action.Validate(null));
        }

        [Fact]
        public void Validate_NullPath_ReturnsFalseAndAddsError()
        {
            // Arrange
            var action = new RuleUpdateAction();
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.False(result);
            Assert.True(validator.Errors.Count > 0);
        }

        [Fact]
        public void Validate_PathNotStartingWithThis_ReturnsFalseAndAddsError()
        {
            // Arrange
            var action = new RuleUpdateAction("IntField");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.False(result);
            Assert.True(validator.Errors.Count > 0);
        }

        [Fact]
        public void Validate_ValidFieldPath_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void Validate_ValidPropertyPath_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/StringProperty");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void Validate_ValidNestedPath_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/Nested/DoubleField");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void Validate_UnknownFieldOrProperty_ReturnsFalseAndAddsError()
        {
            // Arrange
            var action = new RuleUpdateAction("this/UnknownField");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.False(result);
            Assert.True(validator.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WildcardAtEnd_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField/*");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void Validate_WildcardInMiddle_ReturnsFalseAndAddsError()
        {
            // Arrange
            var action = new RuleUpdateAction("this/*/IntField");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.False(result);
            Assert.True(validator.Errors.Count > 0);
        }

        [Fact]
        public void Validate_PathEndingWithSlash_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField/");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void Validate_ArrayType_ReturnsTrue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntArray");
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            bool result = action.Validate(validator);

            // Assert
            Assert.True(result);
            Assert.Empty(validator.Errors);
        }
        #endregion

        #region Execute Tests
        [Fact]
        public void Execute_DoesNotThrow()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");
            var testObject = new TestClass();
            var execution = new RuleExecution(new RuleValidation(typeof(TestClass)), testObject);

            // Act & Assert - Should not throw
            action.Execute(execution);
        }
        #endregion

        #region GetSideEffects Tests
        [Fact]
        public void GetSideEffects_ReturnsPathAsCollection()
        {
            // Arrange
            string path = "this/IntField";
            var action = new RuleUpdateAction(path);
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            ICollection<string> sideEffects = action.GetSideEffects(validator);

            // Assert
            Assert.NotNull(sideEffects);
            Assert.Single(sideEffects);
            Assert.Contains(path, sideEffects);
        }
        #endregion

        #region Clone Tests
        [Fact]
        public void Clone_CreatesNewInstance()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");

            // Act
            IRuleAction clonedAction = action.Clone();

            // Assert
            Assert.NotNull(clonedAction);
            Assert.IsType<RuleUpdateAction>(clonedAction);
            Assert.NotSame(action, clonedAction);
        }

        [Fact]
        public void Clone_CopiesPath()
        {
            // Arrange
            string path = "this/StringProperty";
            var action = new RuleUpdateAction(path);

            // Act
            var clonedAction = (RuleUpdateAction)action.Clone();

            // Assert
            Assert.Equal(action.Path, clonedAction.Path);
        }
        #endregion

        #region ToString Tests
        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            string path = "this/IntField";
            var action = new RuleUpdateAction(path);

            // Act
            string result = action.ToString();

            // Assert
            Assert.Equal($"Update(\"{path}\")", result);
        }
        #endregion

        #region Equals Tests
        [Fact]
        public void Equals_SamePathValues_ReturnsTrue()
        {
            // Arrange
            string path = "this/IntField";
            var action1 = new RuleUpdateAction(path);
            var action2 = new RuleUpdateAction(path);

            // Act
            bool result = action1.Equals(action2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_DifferentPathValues_ReturnsFalse()
        {
            // Arrange
            var action1 = new RuleUpdateAction("this/IntField");
            var action2 = new RuleUpdateAction("this/StringProperty");

            // Act
            bool result = action1.Equals(action2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");
            var otherObject = new object();

            // Act
            bool result = action.Equals(otherObject);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region GetHashCode Tests
        [Fact]
        public void GetHashCode_ReturnsValue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");

            // Act
            int hashCode = action.GetHashCode();

            // Assert - Just verify it returns a value
            Assert.NotEqual(0, hashCode);
        }

        [Fact]
        public void GetHashCode_SameInstanceReturnsSameValue()
        {
            // Arrange
            var action = new RuleUpdateAction("this/IntField");

            // Act
            int hashCode1 = action.GetHashCode();
            int hashCode2 = action.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }
        #endregion
    }
}