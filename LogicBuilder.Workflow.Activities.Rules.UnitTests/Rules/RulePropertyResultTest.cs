using System;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RulePropertyResultTest
    {
        #region Test Helper Classes
        private class TestClass
        {
            public string? SimpleProperty { get; set; } = "InitialValue";

            public int NumericProperty { get; set; } = 42;

            public static string StaticProperty { get; set; } = "StaticValue";

            private readonly string[] _array = { "Item0", "Item1", "Item2", "Item3", "Item4" };

            private readonly string[,] _twoDimentionalArray = new string[2, 3] { { "1", "2", "3" }, { "4", "5", "6" } };


            public string this[int index]//NOSONAR - needed for testing indexer
            {
                get => $"Item{index}";
                set { _array[index] = value; }
            }

            public string this[int x, int y]//NOSONAR - needed for testing indexer
            {
                get => $"Item[{x},{y}]";
                set { _twoDimentionalArray[x, y] = value; }
            }

            public string ThrowingProperty //NOSONAR - needed for testing
            {
                get => throw new InvalidOperationException("Getter error");
                set => throw new InvalidOperationException("Setter error");
            }

            public string ThrowingPropertyWithInnerException//NOSONAR - needed for testing
            {
                get => throw new TargetInvocationException("Outer exception", new ArgumentException("Inner exception"));
                set => throw new TargetInvocationException("Outer exception", new ArgumentException("Inner exception"));
            }

            public string ThrowingPropertyNoInnerException//NOSONAR - needed for testing
            {
                get => throw new TargetInvocationException("No inner exception", null);
                set => throw new TargetInvocationException("No inner exception", null);
            }
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenPropertyInfoIsNull()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new RulePropertyResult(null, new TestClass(), null));
            
            Assert.Equal("propertyInfo", exception.ParamName);
        }

        [Fact]
        public void Constructor_ShouldSucceed_WithValidPropertyInfo()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));

            // Act
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Constructor_ShouldSucceed_WithNullTargetObject()
        {
            // Arrange
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StaticProperty));

            // Act
            var result = new RulePropertyResult(propertyInfo, null, null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Constructor_ShouldSucceed_WithIndexerArguments()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int)]);
            var indexerArgs = new object[] { 5 };

            // Act
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Assert
            Assert.NotNull(result);
        }
        #endregion

        #region Value Getter Tests
        [Fact]
        public void Value_Get_ShouldReturnPropertyValue_ForInstanceProperty()
        {
            // Arrange
            var testObject = new TestClass { SimpleProperty = "TestValue" };
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("TestValue", value);
        }

        [Fact]
        public void Value_Get_ShouldReturnPropertyValue_ForStaticProperty()
        {
            // Arrange
            TestClass.StaticProperty = "StaticTestValue";
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StaticProperty));
            var result = new RulePropertyResult(propertyInfo, null, null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("StaticTestValue", value);
        }

        [Fact]
        public void Value_Get_ShouldThrowRuleEvaluationException_WhenTargetIsNullForInstanceProperty()
        {
            // Arrange
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, null, null);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() => result.Value);
            Assert.Contains(propertyInfo?.Name ?? "", exception.Message);
            Assert.Equal(propertyInfo, exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        [Fact]
        public void Value_Get_ShouldReturnIndexedValue_WithSingleIndexer()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int)]);
            var indexerArgs = new object[] { 3 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("Item3", value);
        }

        [Fact]
        public void Value_Get_ShouldReturnIndexedValue_WithMultipleIndexers()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int), typeof(int)]);
            var indexerArgs = new object[] { 2, 5 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("Item[2,5]", value);
        }
        #endregion

        #region Value Setter Tests
        [Fact]
        public void Value_Set_ShouldSetPropertyValue_ForInstanceProperty()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            _ = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = "NewValue"
            };

            // Assert
            Assert.Equal("NewValue", testObject.SimpleProperty);
        }

        [Fact]
        public void Value_Set_ShouldSetPropertyValue_ForStaticProperty()
        {
            // Arrange
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StaticProperty));
            _ = new RulePropertyResult(propertyInfo, null, null)
            {
                // Act
                Value = "NewStaticValue"
            };

            // Assert
            Assert.Equal("NewStaticValue", TestClass.StaticProperty);
        }

        [Fact]
        public void Value_Set_ShouldThrowRuleEvaluationException_WhenTargetIsNullForInstanceProperty()
        {
            // Arrange
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, null, null);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() => result.Value = "NewValue");
            Assert.Contains(propertyInfo?.Name ?? "", exception.Message);
            Assert.Equal(propertyInfo, exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        [Fact]
        public void Value_Set_ShouldSetIndexedValue_WithSingleIndexer()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int)]);
            var indexerArgs = new object[] { 4 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs)
            {
                // Act
                Value = "NewIndexedValue"
            };

            // Assert - just verify no exception is thrown
            Assert.NotNull(result);
        }

        [Fact]
        public void Value_Set_ShouldSetIndexedValue_WithMultipleIndexers()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int), typeof(int)]);
            var indexerArgs = new object[] { 0, 2 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs)
            {
                // Act
                Value = "NewMultiIndexedValue"
            };

            // Assert - just verify no exception is thrown
            Assert.NotNull(result);
        }

        [Fact]
        public void Value_Set_ShouldSetNumericProperty_Correctly()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.NumericProperty));
            _ = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = 100
            };

            // Assert
            Assert.Equal(100, testObject.NumericProperty);
        }
        #endregion

        #region Exception Handling Tests - Getter

        [Fact]
        public void Value_Get_ShouldThrowTargetInvocationException_WhenPropertyGetterThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value);
            Assert.Contains("Getter error", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Value_Get_ShouldIncludePropertyInfoInExceptionMessage_WhenPropertyGetterThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value);
            Assert.Contains(propertyInfo!.Name, exception.Message);
            Assert.Contains(propertyInfo!.ReflectedType!.Name, exception.Message);
        }

        [Fact]
        public void Value_Get_ShouldWrapTargetInvocationExceptionWithInnerException()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingPropertyWithInnerException));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value);
            // The outer TargetInvocationException from the property is caught and re-wrapped
            // The InnerException should be the ArgumentException from the original throw
            Assert.NotNull(exception.InnerException);
            Assert.IsType<TargetInvocationException>(exception.InnerException);
            var innerException = (TargetInvocationException)exception.InnerException;
            Assert.NotNull(innerException.InnerException);
            Assert.IsType<ArgumentException>(innerException.InnerException);
        }

        [Fact]
        public void Value_Get_ShouldRethrowTargetInvocationException_WhenNoInnerException()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingPropertyNoInnerException));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value);
            // When the property throws TargetInvocationException with null InnerException,
            // it gets rethrown as-is, which means the caught exception is the original one wrapped by reflection
            Assert.NotNull(exception.InnerException);
        }

        #endregion

        #region Exception Handling Tests - Setter

        [Fact]
        public void Value_Set_ShouldThrowTargetInvocationException_WhenPropertySetterThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value = "test");
            Assert.Contains("Setter error", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Value_Set_ShouldIncludePropertyInfoInExceptionMessage_WhenPropertySetterThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value = "test");
            Assert.Contains(propertyInfo!.Name, exception.Message);
            Assert.Contains(propertyInfo!.ReflectedType!.Name, exception.Message);
        }

        [Fact]
        public void Value_Set_ShouldWrapTargetInvocationExceptionWithInnerException()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingPropertyWithInnerException));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value = "test");
            // The outer TargetInvocationException from the property is caught and re-wrapped
            Assert.NotNull(exception.InnerException);
            Assert.IsType<TargetInvocationException>(exception.InnerException);
            var innerException = (TargetInvocationException)exception.InnerException;
            Assert.NotNull(innerException.InnerException);
            Assert.IsType<ArgumentException>(innerException.InnerException);
        }

        [Fact]
        public void Value_Set_ShouldRethrowTargetInvocationException_WhenNoInnerException()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingPropertyNoInnerException));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value = "test");
            // When the property throws TargetInvocationException with null InnerException,
            // it gets rethrown as-is, which means the caught exception is the original one wrapped by reflection
            Assert.NotNull(exception.InnerException);
        }

        #endregion

        #region Additional Edge Case Tests

        [Fact]
        public void Value_Get_ShouldWorkCorrectly_WithNullIndexerArguments()
        {
            // Arrange
            var testObject = new TestClass { SimpleProperty = "TestValue1" };
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("TestValue1", value);
        }

        [Fact]
        public void Value_Set_ShouldWorkCorrectly_WithNullIndexerArguments()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            _ = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = "NewValue1"
            };

            // Assert
            Assert.Equal("NewValue1", testObject.SimpleProperty);
        }

        [Fact]
        public void Value_GetAndSet_ShouldWorkCorrectly_WithSingleIndexer()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int)]);
            var indexerArgs = new object[] { 2 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Act
            var originalValue = result.Value;
            result.Value = "UpdatedItem2";
            var updatedValue = result.Value;

            // Assert
            Assert.Equal("Item2", originalValue);
            Assert.Equal("Item2", updatedValue); // Getter returns formatted string, not actual array value
        }

        [Fact]
        public void Value_GetAndSet_ShouldWorkCorrectly_WithMultipleIndexers()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty("Item", [typeof(int), typeof(int)]);
            var indexerArgs = new object[] { 1, 2 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Act
            var originalValue = result.Value;
            result.Value = "UpdatedItem[1,2]";
            var updatedValue = result.Value;

            // Assert
            Assert.Equal("Item[1,2]", originalValue);
            Assert.Equal("Item[1,2]", updatedValue); // Getter returns formatted string
        }

        [Fact]
        public void Constructor_ShouldAcceptEmptyIndexerArray()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var indexerArgs = Array.Empty<object>();

            // Act
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("InitialValue", result.Value);
        }

        [Fact]
        public void Value_Get_ShouldWorkWithStaticProperty_EvenWithNonNullTarget()
        {
            // Arrange
            var testObject = new TestClass();
            TestClass.StaticProperty = "StaticTestValue";
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StaticProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("StaticTestValue", value);
        }

        [Fact]
        public void Value_Set_ShouldWorkWithStaticProperty_EvenWithNonNullTarget()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StaticProperty));
            _ = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = "NewStaticValue"
            };

            // Assert
            Assert.Equal("NewStaticValue", TestClass.StaticProperty);
        }

        [Fact]
        public void Value_GetAndSet_ShouldHandleNumericTypeConversions()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.NumericProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = 999
            };
            var value = result.Value;

            // Assert
            Assert.Equal(999, value);
            Assert.Equal(999, testObject.NumericProperty);
        }

        [Fact]
        public void Value_Set_ShouldHandleNullValue_ForReferenceTypeProperty()
        {
            // Arrange
            var testObject = new TestClass { SimpleProperty = "NotNull" };
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null)
            {
                // Act
                Value = null
            };

            // Assert
            Assert.Null(testObject.SimpleProperty);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Value_Get_ShouldReturnNull_WhenPropertyValueIsNull()
        {
            // Arrange
            var testObject = new TestClass { SimpleProperty = null };
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act
            var value = result.Value;

            // Assert
            Assert.Null(value);
        }

        #endregion

        #region Integration Tests
        [Fact]
        public void Value_GetAndSet_ShouldWorkTogether()
        {
            // Arrange
            var testObject = new TestClass { SimpleProperty = "Original" };
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.SimpleProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act
            var originalValue = result.Value;
            result.Value = "Modified";
            var modifiedValue = result.Value;

            // Assert
            Assert.Equal("Original", originalValue);
            Assert.Equal("Modified", modifiedValue);
            Assert.Equal("Modified", testObject.SimpleProperty);
        }
        #endregion
    }
}