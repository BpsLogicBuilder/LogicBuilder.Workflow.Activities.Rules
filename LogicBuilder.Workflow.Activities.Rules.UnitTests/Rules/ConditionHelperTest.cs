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
            DateTime original = new(2026, 2, 9, 0, 0, 0, DateTimeKind.Unspecified);

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

        #region Additional CloneObject Tests

        [Fact]
        public void CloneObject_WithCloneableObject_ClonesSuccessfully()
        {
            // Arrange
            var original = new CloneableTestClass { Value = "Original" };

            // Act
            var result = (CloneableTestClass)ConditionHelper.CloneObject(original);

            // Assert
            Assert.NotNull(result);
            Assert.NotSame(original, result);
            Assert.Equal("Original", result.Value);
        }

        [Fact]
        public void CloneObject_WithString_ClonesSuccessfully()
        {
            // Arrange
            string original = "Test String";

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
            Assert.Same(original, result); // Strings are immutable and implement ICloneable
        }

        [Fact]
        public void CloneObject_WithNullableValueType_ReturnsSameValue()
        {
            // Arrange
            int? original = 42;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithBool_ReturnsSameValue()
        {
            // Arrange
            bool original = true;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithDecimal_ReturnsSameValue()
        {
            // Arrange
            decimal original = 123.45m;

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithChar_ReturnsSameValue()
        {
            // Arrange
            char original = 'A';

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        [Fact]
        public void CloneObject_WithStruct_ReturnsSameValue()
        {
            // Arrange
            var original = new TestStruct { X = 10, Y = 20 };

            // Act
            object result = ConditionHelper.CloneObject(original);

            // Assert
            Assert.Equal(original, result);
        }

        #endregion

        #region Additional CloneUserData Tests

        [Fact]
        public void CloneUserData_WithCloneableData_ClonesSuccessfully()
        {
            // Arrange
            var original = new CodeExpression();
            var cloneableObj = new CloneableTestClass { Value = "Test" };
            original.UserData.Add("key", cloneableObj);
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Single(result.UserData);
            var clonedObj = result.UserData["key"] as CloneableTestClass;
            Assert.NotNull(clonedObj);
            Assert.NotSame(cloneableObj, clonedObj);
            Assert.Equal("Test", clonedObj.Value);
        }

        [Fact]
        public void CloneUserData_WithStringData_ClonesSuccessfully()
        {
            // Arrange
            var original = new CodeExpression();
            original.UserData.Add("stringKey", "stringValue");
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Single(result.UserData);
            Assert.Equal("stringValue", result.UserData["stringKey"]);
        }

        [Fact]
        public void CloneUserData_WithMultipleCloneableItems_ClonesAll()
        {
            // Arrange
            var original = new CodeExpression();
            original.UserData.Add("key1", new CloneableTestClass { Value = "Value1" });
            original.UserData.Add("key2", 42);
            original.UserData.Add("key3", "text");
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Equal(3, result.UserData.Count);
            Assert.Equal(42, result.UserData["key2"]);
            Assert.Equal("text", result.UserData["key3"]);
            var clonedObj = (CloneableTestClass)result.UserData["key1"]!;
            Assert.NotNull(clonedObj);
            Assert.Equal("Value1", clonedObj.Value);
        }

        [Fact]
        public void CloneUserData_WithNullValue_ClonesNullValue()
        {
            // Arrange
            var original = new CodeExpression();
            original.UserData.Add("nullKey", null);
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Single(result.UserData);
            Assert.Null(result.UserData["nullKey"]);
        }

        [Fact]
        public void CloneUserData_WithDateTimeData_ClonesSuccessfully()
        {
            // Arrange
            var original = new CodeExpression();
            var dateTime = new DateTime(2026, 2, 9, 0, 0, 0, DateTimeKind.Unspecified);
            original.UserData.Add("date", dateTime);
            var result = new CodeExpression();

            // Act
            ConditionHelper.CloneUserData(original, result);

            // Assert
            Assert.Single(result.UserData);
            Assert.Equal(dateTime, result.UserData["date"]);
        }

        #endregion

        #region Additional GetRuleDefinitionsFromManifest Tests

        [Fact]
        public void GetRuleDefinitionsFromManifest_CalledMultipleTimesForSameType_UsesCachedValue()
        {
            // Arrange
            Type workflowType = typeof(ConditionHelperTest);

            // Act
            var result1 = ConditionHelper.GetRuleDefinitionsFromManifest(workflowType);
            var result2 = ConditionHelper.GetRuleDefinitionsFromManifest(workflowType);

            // Assert
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void GetRuleDefinitionsFromManifest_WithDifferentTypes_ReturnsDifferentResults()
        {
            // Arrange
            Type type1 = typeof(ConditionHelperTest);
            Type type2 = typeof(NonCloneableTestClass);

            // Act
            var result1 = ConditionHelper.GetRuleDefinitionsFromManifest(type1);
            var result2 = ConditionHelper.GetRuleDefinitionsFromManifest(type2);

            // Assert
            // Both should be null since neither has embedded rules, but cache should handle them separately
            Assert.Null(result1);
            Assert.Null(result2);
        }

        #endregion

        #region Helper Classes

        private class NonCloneableTestClass
        {
            public string? Value { get; set; }
        }

        private class CloneableTestClass : ICloneable
        {
            public string? Value { get; set; }

            public object Clone()
            {
                return new CloneableTestClass { Value = this.Value };
            }
        }

        private struct TestStruct
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        #endregion
    }
}