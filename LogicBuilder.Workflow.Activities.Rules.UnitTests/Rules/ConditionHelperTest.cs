using System;
using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ConditionHelperTest
    {
        #region IsNullableValueType Tests

        [Fact]
        public void IsNullableValueType_WithNullableInt_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(int?);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullableValueType_WithNullableDouble_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(double?);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullableValueType_WithNullableDateTime_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(DateTime?);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullableValueType_WithInt_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullableValueType_WithString_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullableValueType_WithReferenceType_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(object);

            // Act
            bool result = ConditionHelper.IsNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region IsNonNullableValueType Tests

        [Fact]
        public void IsNonNullableValueType_WithInt_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithDouble_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(double);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithBool_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(bool);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithDateTime_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(DateTime);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithNullableInt_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(int?);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithString_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNonNullableValueType_WithReferenceType_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(object);

            // Act
            bool result = ConditionHelper.IsNonNullableValueType(type);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CloneObject Tests

        [Fact]
        public void CloneObject_WithNull_ReturnsNull()
        {
            // Arrange
            object? original = null;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CloneObject_WithValueType_ReturnsSameValue()
        {
            // Arrange
            int original = 42;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithDouble_ReturnsSameValue()
        {
            // Arrange
            double original = 3.14;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithDateTime_ReturnsSameValue()
        {
            // Arrange
            DateTime original = new(2026, 2, 9);

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithNonCloneableObject_ThrowsNotSupportedException()
        {
            // Arrange
            var original = new NonCloneableTestClass { Value = "Test" };

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => ConditionHelper.CloneObject(original));
            Assert.Contains("NonCloneableTestClass", exception.Message);
        }

        #endregion

        #region CloneUserData Tests

        [Fact]
        public void CloneUserData_WithEmptyUserData_DoesNotThrow()
        {
            // Arrange
            var original = new CodeExpression();
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Empty(result.UserData);
        }

        [Fact]
        public void CloneUserData_WithValueTypeData_ClonesSuccessfully()
        {
            // Arrange
            var original = new CodeExpression();
            original.UserData.Add("key1", 42);
            original.UserData.Add("key2", 3.14);
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Equal(2, result.UserData.Count);
            Assert.Equal(42, result.UserData["key1"]);
            Assert.Equal(3.14, result.UserData["key2"]);
        }

        [Fact]
        public void CloneUserData_WithNonCloneableData_ThrowsNotSupportedException()
        {
            // Arrange
            var original = new CodeExpression();
            original.UserData.Add("key", new NonCloneableTestClass { Value = "Test" });
            var result = new CodeExpression();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => ConditionHelper.CloneUserData(original, result));
        }

        #endregion

        #region GetRuleDefinitionsFromManifest Tests

        [Fact]
        public void GetRuleDefinitionsFromManifest_WithNullType_ThrowsArgumentNullException()
        {
            // Arrange
            Type? workflowType = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => ConditionHelper.GetRuleDefinitionsFromManifest(workflowType));
            Assert.Equal("workflowType", exception.ParamName);
        }

        [Fact]
        public void GetRuleDefinitionsFromManifest_WithTypeWithoutRules_ReturnsNull()
        {
            // Arrange
            Type workflowType = typeof(ConditionHelperTest);

            // Act
            var result = ConditionHelper.GetRuleDefinitionsFromManifest(workflowType);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Helper Classes

        private class NonCloneableTestClass
        {
            public string? Value { get; set; }
        }

        #endregion
    }
}