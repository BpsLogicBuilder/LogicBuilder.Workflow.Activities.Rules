using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class SimpleParameterInfoTest
    {
        #region Helper Classes

        public class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; }
        }

        public struct TestStruct
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        #endregion

        #region Constructor Tests - ParameterInfo Overload

        [Fact]
        public void Constructor_WithParameterInfo_CreatesNullableWrapper()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithIntParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.NotNull(simpleParam);
            Assert.Equal(typeof(int?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithValueTypeParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithDoubleParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(double?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithStructParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithStructParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Type expectedType = typeof(Nullable<>).MakeGenericType(typeof(TestStruct));
            Assert.Equal(expectedType, simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithBoolParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithBoolParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(bool?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithDateTimeParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithDateTimeParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(DateTime?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithDecimalParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithDecimalParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(decimal?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithLongParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithLongParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(long?), simpleParam.ParameterType);
        }

        #endregion

        #region Constructor Tests - Type Overload

        [Fact]
        public void Constructor_WithTypeParameter_StoresTypeDirect()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.NotNull(simpleParam);
            Assert.Equal(typeof(int), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithNullableType_StoresType()
        {
            // Arrange
            Type type = typeof(int?);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(int?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithStringType_StoresType()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(string), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithDoubleType_StoresType()
        {
            // Arrange
            Type type = typeof(double);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(double), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithBoolType_StoresType()
        {
            // Arrange
            Type type = typeof(bool);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(bool), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithStructType_StoresType()
        {
            // Arrange
            Type type = typeof(TestStruct);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(TestStruct), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithDateTimeType_StoresType()
        {
            // Arrange
            Type type = typeof(DateTime);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(DateTime), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithDecimalType_StoresType()
        {
            // Arrange
            Type type = typeof(decimal);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(decimal), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithObjectType_StoresType()
        {
            // Arrange
            Type type = typeof(object);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(object), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithCustomClassType_StoresType()
        {
            // Arrange
            Type type = typeof(TestClass);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(TestClass), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithArrayType_StoresType()
        {
            // Arrange
            Type type = typeof(int[]);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(int[]), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithGenericType_StoresType()
        {
            // Arrange
            Type type = typeof(List<int>);

            // Act
            var simpleParam = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(typeof(List<int>), simpleParam.ParameterType);
        }

        #endregion

        #region ParameterType Property Tests

        [Fact]
        public void ParameterType_MultipleAccess_ReturnsSameType()
        {
            // Arrange
            Type type = typeof(int);
            var simpleParam = new SimpleParameterInfo(type);

            // Act
            Type result1 = simpleParam.ParameterType;
            Type result2 = simpleParam.ParameterType;

            // Assert
            Assert.Equal(result1, result2);
            Assert.Same(result1, result2);
        }

        [Fact]
        public void ParameterType_FromParameterInfo_ReturnsNullableType()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithIntParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];
            var simpleParam = new SimpleParameterInfo(parameter);

            // Act
            Type result = simpleParam.ParameterType;

            // Assert
            Assert.True(result.IsGenericType);
            Assert.Equal(typeof(Nullable<>), result.GetGenericTypeDefinition());
            Assert.Equal(typeof(int), result.GetGenericArguments()[0]);
        }

        [Fact]
        public void ParameterType_FromTypeParameter_ReturnsExactType()
        {
            // Arrange
            Type originalType = typeof(string);
            var simpleParam = new SimpleParameterInfo(originalType);

            // Act
            Type result = simpleParam.ParameterType;

            // Assert
            Assert.Equal(originalType, result);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void Constructor_WithByteParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithByteParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(byte?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithCharParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithCharParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(char?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithShortParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithShortParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(short?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithFloatParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithFloatParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(float?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithUIntParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithUIntParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(uint?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithULongParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithULongParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(typeof(ulong?), simpleParam.ParameterType);
        }

        [Fact]
        public void Constructor_WithEnumParameter_WrapsInNullable()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithEnumParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam = new SimpleParameterInfo(parameter);

            // Assert
            Type expectedType = typeof(Nullable<>).MakeGenericType(typeof(DayOfWeek));
            Assert.Equal(expectedType, simpleParam.ParameterType);
        }

        #endregion

        #region Comparison Tests

        [Fact]
        public void Constructor_TwoInstancesWithSameParameterInfo_HaveSameParameterType()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithIntParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];

            // Act
            var simpleParam1 = new SimpleParameterInfo(parameter);
            var simpleParam2 = new SimpleParameterInfo(parameter);

            // Assert
            Assert.Equal(simpleParam1.ParameterType, simpleParam2.ParameterType);
        }

        [Fact]
        public void Constructor_TwoInstancesWithSameType_HaveSameParameterType()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            var simpleParam1 = new SimpleParameterInfo(type);
            var simpleParam2 = new SimpleParameterInfo(type);

            // Assert
            Assert.Equal(simpleParam1.ParameterType, simpleParam2.ParameterType);
            Assert.Same(simpleParam1.ParameterType, simpleParam2.ParameterType);
        }

        [Fact]
        public void Constructor_ParameterInfoVsType_HaveDifferentBehavior()
        {
            // Arrange
            MethodInfo method = typeof(TestMethods).GetMethod("MethodWithIntParameter", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo parameter = method.GetParameters()[0];
            Type directType = typeof(int);

            // Act
            var fromParameterInfo = new SimpleParameterInfo(parameter);
            var fromType = new SimpleParameterInfo(directType);

            // Assert
            Assert.NotEqual(fromParameterInfo.ParameterType, fromType.ParameterType);
            Assert.Equal(typeof(int?), fromParameterInfo.ParameterType); // Wrapped in Nullable
            Assert.Equal(typeof(int), fromType.ParameterType); // Direct type
        }

        #endregion
    }

    #region Test Helper Methods

    public static class TestMethods
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static void MethodWithIntParameter(int value)
        { 
            //NOSONAR - meeded for testing
        }
        public static void MethodWithDoubleParameter(double value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithStringParameter(string value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithStructParameter(SimpleParameterInfoTest.TestStruct value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithBoolParameter(bool value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithDateTimeParameter(DateTime value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithDecimalParameter(decimal value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithLongParameter(long value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithObjectParameter(object value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithTestClassParameter(SimpleParameterInfoTest.TestClass value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithByteParameter(byte value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithCharParameter(char value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithShortParameter(short value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithFloatParameter(float value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithUIntParameter(uint value)
        {
            //NOSONAR - meeded for testing
        }
        public static void MethodWithULongParameter(ulong value)
        {
            //NOSONAR - meeded for testing
        }

        public static void MethodWithEnumParameter(DayOfWeek value)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            //NOSONAR - meeded for testing
        }
    }

    #endregion
}
