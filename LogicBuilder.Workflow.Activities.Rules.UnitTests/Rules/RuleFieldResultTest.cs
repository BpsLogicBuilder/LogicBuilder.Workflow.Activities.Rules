using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleFieldResultTest
    {
        #region Test Helper Classes

        private class TestClass
        {
            public readonly int InstanceField = 42;
            public readonly string StringField = "test";
            public static int StaticField = 100;
            public static string StaticStringField = "static test";
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidFieldInfo_CreatesInstance()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("InstanceField");

            // Act
            var result = new RuleFieldResult(testObject, fieldInfo);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Constructor_WithNullFieldInfo_ThrowsArgumentNullException()
        {
            // Arrange
            var testObject = new TestClass();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new RuleFieldResult(testObject, null));
            Assert.Equal("fieldInfo", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithNullTargetObject_CreatesInstance()
        {
            // Arrange
            var fieldInfo = typeof(TestClass).GetField("StaticField");

            // Act
            var result = new RuleFieldResult(null, fieldInfo);

            // Assert
            Assert.NotNull(result);
        }

        #endregion

        #region Value Getter Tests - Instance Fields

        [Fact]
        public void ValueGetter_WithInstanceField_ReturnsCorrectValue()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("InstanceField");
            var result = new RuleFieldResult(testObject, fieldInfo);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(42, value);
        }

        [Fact]
        public void ValueGetter_WithStringInstanceField_ReturnsCorrectValue()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("StringField");
            var result = new RuleFieldResult(testObject, fieldInfo);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("test", value);
        }

        [Fact]
        public void ValueGetter_WithNullTargetAndInstanceField_ThrowsRuleEvaluationException()
        {
            // Arrange
            var fieldInfo = typeof(TestClass).GetField("InstanceField");
            var result = new RuleFieldResult(null, fieldInfo);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() => 
            {
                _ = result.Value;
            });
            Assert.Contains(fieldInfo?.Name ?? "", exception.Message);
            Assert.Equal(fieldInfo, exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        #endregion

        #region Value Getter Tests - Static Fields

        [Fact]
        public void ValueGetter_WithStaticField_ReturnsCorrectValue()
        {
            // Arrange
            var fieldInfo = typeof(TestClass).GetField("StaticField");
            var result = new RuleFieldResult(null, fieldInfo);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(100, value);
        }

        [Fact]
        public void ValueGetter_WithStaticStringField_ReturnsCorrectValue()
        {
            // Arrange
            var fieldInfo = typeof(TestClass).GetField("StaticStringField");
            var result = new RuleFieldResult(null, fieldInfo);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal("static test", value);
        }

        [Fact]
        public void ValueGetter_WithStaticFieldAndNonNullTarget_ReturnsCorrectValue()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("StaticField");
            var result = new RuleFieldResult(testObject, fieldInfo);

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(100, value);
        }

        #endregion

        #region Value Setter Tests - Instance Fields

        [Fact]
        public void ValueSetter_WithInstanceField_SetsCorrectValue()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("InstanceField");
            var result = new RuleFieldResult(testObject, fieldInfo)
            {
                // Act
                Value = 999
            };

            // Assert
            Assert.Equal(999, testObject.InstanceField);
            Assert.Equal(999, result.Value);
        }

        [Fact]
        public void ValueSetter_WithStringInstanceField_SetsCorrectValue()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("StringField");
            var result = new RuleFieldResult(testObject, fieldInfo)
            {
                // Act
                Value = "modified"
            };

            // Assert
            Assert.Equal("modified", testObject.StringField);
            Assert.Equal("modified", result.Value);
        }

        [Fact]
        public void ValueSetter_WithNullTargetAndInstanceField_ThrowsRuleEvaluationException()
        {
            // Arrange
            var fieldInfo = typeof(TestClass).GetField("InstanceField");
            var result = new RuleFieldResult(null, fieldInfo);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() => 
            {
                result.Value = 999;
            });
            Assert.Contains(fieldInfo?.Name ?? "", exception.Message);
            Assert.Equal(fieldInfo, exception.Data[RuleUserDataKeys.ErrorObject]);
        }

        #endregion

        #region Value Setter Tests - Static Fields

        [Fact]
        public void ValueSetter_WithStaticField_SetsCorrectValue()
        {
            // Arrange
            var originalValue = TestClass.StaticField;
            var fieldInfo = typeof(TestClass).GetField("StaticField");
            var result = new RuleFieldResult(null, fieldInfo);

            try
            {
                // Act
                result.Value = 555;

                // Assert
                Assert.Equal(555, TestClass.StaticField);
                Assert.Equal(555, result.Value);
            }
            finally
            {
                // Cleanup - restore original value
                TestClass.StaticField = originalValue;
            }
        }

        [Fact]
        public void ValueSetter_WithStaticStringField_SetsCorrectValue()
        {
            // Arrange
            var originalValue = TestClass.StaticStringField;
            var fieldInfo = typeof(TestClass).GetField("StaticStringField");
            var result = new RuleFieldResult(null, fieldInfo);

            try
            {
                // Act
                result.Value = "modified static";

                // Assert
                Assert.Equal("modified static", TestClass.StaticStringField);
                Assert.Equal("modified static", result.Value);
            }
            finally
            {
                // Cleanup - restore original value
                TestClass.StaticStringField = originalValue;
            }
        }

        [Fact]
        public void ValueSetter_WithStaticFieldAndNonNullTarget_SetsCorrectValue()
        {
            // Arrange
            var originalValue = TestClass.StaticField;
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("StaticField");
            var result = new RuleFieldResult(testObject, fieldInfo);

            try
            {
                // Act
                result.Value = 777;

                // Assert
                Assert.Equal(777, TestClass.StaticField);
                Assert.Equal(777, result.Value);
            }
            finally
            {
                // Cleanup - restore original value
                TestClass.StaticField = originalValue;
            }
        }

        #endregion

        #region Multiple Operations Tests

        [Fact]
        public void Value_MultipleGetAndSetOperations_WorksCorrectly()
        {
            // Arrange
            var testObject = new TestClass();
            var fieldInfo = typeof(TestClass).GetField("InstanceField");
            var result = new RuleFieldResult(testObject, fieldInfo);

            // Act & Assert - First get
            Assert.Equal(42, result.Value);

            // Act & Assert - First set
            result.Value = 100;
            Assert.Equal(100, result.Value);
            Assert.Equal(100, testObject.InstanceField);

            // Act & Assert - Second set
            result.Value = 200;
            Assert.Equal(200, result.Value);
            Assert.Equal(200, testObject.InstanceField);
        }

        #endregion
    }
}