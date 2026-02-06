using System;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RulePropertyResultTest
    {
        #region Test Helper Classes
        private class TestClass
        {
            public string SimpleProperty { get; set; } = "InitialValue";
            
            public int NumericProperty { get; set; } = 42;
            
            public static string StaticProperty { get; set; } = "StaticValue";
            
            public static string ThrowingProperty
            {
                get => throw new InvalidOperationException("Get failed");
                set => throw new InvalidOperationException("Set failed");
            }
            
            public string this[int index]
            {
                get => $"Item{index}";
                set { /* setter for indexer */ }
            }
            
            public string this[int x, int y]
            {
                get => $"Item[{x},{y}]";
                set { /* setter for multi-dimensional indexer */ }
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

        [Fact]
        public void Value_Get_ShouldThrowTargetInvocationException_WithCustomMessage_WhenPropertyThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value);
            Assert.Contains("Get failed", exception.Message);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
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
            var indexerArgs = new object[] { 7 };
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
            var indexerArgs = new object[] { 3, 4 };
            var result = new RulePropertyResult(propertyInfo, testObject, indexerArgs)
            {
                // Act
                Value = "NewMultiIndexedValue"
            };

            // Assert - just verify no exception is thrown
            Assert.NotNull(result);
        }

        [Fact]
        public void Value_Set_ShouldThrowTargetInvocationException_WithCustomMessage_WhenPropertyThrows()
        {
            // Arrange
            var testObject = new TestClass();
            var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.ThrowingProperty));
            var result = new RulePropertyResult(propertyInfo, testObject, null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() => result.Value = "NewValue");
            Assert.Contains("Set failed", exception.Message);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
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