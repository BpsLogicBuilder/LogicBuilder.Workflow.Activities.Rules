using System;
using System.CodeDom;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class EnumOperationMethodInfoTest
    {
        #region Test Enums
        public enum PositionType
        {
            None = 0,
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4,
            Fifth = 5
        }

        public enum TestEnumLong : long
        {
            None = 0L,
            First = 1L,
            Second = 2L,
            Third = 3L
        }

        public enum TestEnumByte : byte
        {
            None = 0,
            First = 1,
            Second = 2
        }
        #endregion

        #region Constructor Tests - Add Operation
        [Fact]
        public void Constructor_AddEnumAndInt_CreatesCorrectMethodInfo()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType), methodInfo.ReturnType);
            ParameterInfo[] parameters = methodInfo.GetParameters();
            Assert.Equal(2, parameters.Length);
        }

        [Fact]
        public void Constructor_AddIntAndEnum_CreatesCorrectMethodInfo()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(int), CodeBinaryOperatorType.Add, typeof(PositionType), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_AddEnumAndEnum_CreatesUnderlyingTypeReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(PositionType), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(int), methodInfo.ReturnType); // Returns underlying type when adding two enums
        }

        [Fact]
        public void Constructor_AddNullableEnumAndInt_CreatesNullableReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Add, typeof(int), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType?), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_AddEnumAndNullableInt_CreatesNullableReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int?), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType?), methodInfo.ReturnType);
        }
        #endregion

        #region Constructor Tests - Subtract Operation
        [Fact]
        public void Constructor_SubtractEnumAndEnum_CreatesUnderlyingTypeReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(PositionType), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(int), methodInfo.ReturnType); // E - E returns underlying type
        }

        [Fact]
        public void Constructor_SubtractEnumAndInt_CreatesEnumReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(int), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType), methodInfo.ReturnType); // E - U returns E
        }

        [Fact]
        public void Constructor_SubtractEnumAndZero_CreatesUnderlyingTypeReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(PositionType), true);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(int), methodInfo.ReturnType); // E - 0 (as E) returns underlying type
        }

        [Fact]
        public void Constructor_SubtractZeroAndEnum_CreatesUnderlyingTypeReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(int), CodeBinaryOperatorType.Subtract, typeof(PositionType), true);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(int), methodInfo.ReturnType); // 0 - E returns underlying type
        }

        [Fact]
        public void Constructor_SubtractNullableEnums_CreatesNullableReturn()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Subtract, typeof(PositionType?), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(int?), methodInfo.ReturnType);
        }
        #endregion

        #region Constructor Tests - Comparison Operations
        [Fact]
        public void Constructor_ValueEquality_ReturnsBoolType()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.ValueEquality, typeof(PositionType), false);

            // Assert
            Assert.Equal(typeof(bool), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_LessThan_ReturnsBoolType()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThan, typeof(PositionType), false);

            // Assert
            Assert.Equal(typeof(bool), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_LessThanOrEqual_ReturnsBoolType()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThanOrEqual, typeof(PositionType), false);

            // Assert
            Assert.Equal(typeof(bool), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_GreaterThan_ReturnsBoolType()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThan, typeof(PositionType), false);

            // Assert
            Assert.Equal(typeof(bool), methodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_GreaterThanOrEqual_ReturnsBoolType()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThanOrEqual, typeof(PositionType), false);

            // Assert
            Assert.Equal(typeof(bool), methodInfo.ReturnType);
        }
        #endregion

        #region Invoke Tests - Add Operation
        [Fact]
        public void Invoke_AddEnumAndInt_ReturnsCorrectResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);
            object[] parameters = [PositionType.First, 2];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal(PositionType.Third, result);
        }

        [Fact]
        public void Invoke_AddIntAndEnum_ReturnsCorrectResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(int), CodeBinaryOperatorType.Add, typeof(PositionType), false);
            object[] parameters = [2, PositionType.First];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal(PositionType.Third, result);
        }

        [Fact]
        public void Invoke_AddEnumAndEnum_ReturnsUnderlyingType()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(PositionType), false);
            object[] parameters = [PositionType.First, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Invoke_AddWithNullFirstParameter_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Add, typeof(int), false);
            object?[] parameters = [null, 2];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_AddWithNullSecondParameter_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int?), false);
            object?[] parameters = [PositionType.First, null];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_AddNullableEnumAndInt_ReturnsNullableResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Add, typeof(int), false);
            object[] parameters = [new PositionType?(PositionType.Second), new int?(1)];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);//compiler returns the non-nullable enum type when Axtivator.CreateInstance is used to create a nullable type with a value.
            Assert.Equal(PositionType.Third, (PositionType)result);
        }
        #endregion

        #region Invoke Tests - Subtract Operation
        [Fact]
        public void Invoke_SubtractEnumAndEnum_ReturnsUnderlyingType()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(PositionType), false);
            object[] parameters = [PositionType.Fifth, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Invoke_SubtractEnumAndInt_ReturnsEnum()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(int), false);
            object[] parameters = [PositionType.Fourth, 2];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal(PositionType.Second, result);
        }

        [Fact]
        public void Invoke_SubtractWithNullFirstParameter_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Subtract, typeof(PositionType), false);
            object?[] parameters = [null, PositionType.First];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_SubtractWithNullSecondParameter_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(PositionType?), false);
            object?[] parameters = [PositionType.Third, null];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_SubtractNullableEnums_ReturnsNullableResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Subtract, typeof(PositionType?), false);
            object[] parameters = [PositionType.Fourth, PositionType.First];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);//compiler returns the non-nullable enum type when Axtivator.CreateInstance is used to create a nullable type with a value.
            Assert.Equal(3, (int)result);
        }
        #endregion

        #region Invoke Tests - Comparison Operations
        [Fact]
        public void Invoke_ValueEquality_SameValues_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.ValueEquality, typeof(PositionType), false);
            object[] parameters = [PositionType.Second, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_ValueEquality_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.ValueEquality, typeof(PositionType), false);
            object[] parameters = [PositionType.First, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_ValueEquality_BothNull_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.ValueEquality, typeof(PositionType?), false);
            object?[] parameters = [null, null];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_ValueEquality_OneNull_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.ValueEquality, typeof(PositionType?), false);
            object?[] parameters = [PositionType.First, null];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_LessThan_FirstLess_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThan, typeof(PositionType), false);
            object[] parameters = [PositionType.First, PositionType.Third];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_LessThan_FirstGreater_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThan, typeof(PositionType), false);
            object[] parameters = [PositionType.Fourth, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_LessThanOrEqual_FirstLess_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThanOrEqual, typeof(PositionType), false);
            object[] parameters = [PositionType.Second, PositionType.Fourth];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_LessThanOrEqual_Equal_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThanOrEqual, typeof(PositionType), false);
            object[] parameters = [PositionType.Third, PositionType.Third];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThan_FirstGreater_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThan, typeof(PositionType), false);
            object[] parameters = [PositionType.Fifth, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThan_FirstLess_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThan, typeof(PositionType), false);
            object[] parameters = [PositionType.First, PositionType.Fourth];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqual_FirstGreater_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThanOrEqual, typeof(PositionType), false);
            object[] parameters = [PositionType.Fourth, PositionType.First];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqual_Equal_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThanOrEqual, typeof(PositionType), false);
            object[] parameters = [PositionType.Second, PositionType.Second];
            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }
        #endregion

        #region Invoke Tests - Unsupported Operations
        [Fact]
        public void Invoke_UnsupportedOperation_ThrowsException()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Multiply, typeof(PositionType), false);
            object[] parameters = [PositionType.First, PositionType.Second];

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture));
            Assert.Contains("not supported", exception.Message);
        }
        #endregion

        #region Property Tests
        [Fact]
        public void GetBaseDefinition_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var baseDefinition = methodInfo.GetBaseDefinition();

            // Assert
            Assert.Null(baseDefinition);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsNull()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var attributes = methodInfo.ReturnTypeCustomAttributes;

            // Assert
            Assert.Null(attributes);
        }

        [Fact]
        public void Attributes_ReturnsStatic()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var attributes = methodInfo.Attributes;

            // Assert
            Assert.Equal(MethodAttributes.Static, attributes);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsRuntime()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var flags = methodInfo.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(MethodImplAttributes.Runtime, flags);
        }

        [Fact]
        public void GetParameters_ReturnsTwoParameters()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var parameters = methodInfo.GetParameters();

            // Assert
            Assert.NotNull(parameters);
            Assert.Equal(2, parameters.Length);
            Assert.Equal(typeof(PositionType), parameters[0].ParameterType);
            Assert.Equal(typeof(int), parameters[1].ParameterType);
        }

        [Fact]
        public void MethodHandle_ReturnsEmptyHandle()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var handle = methodInfo.MethodHandle;

            // Assert
            Assert.Equal(new RuntimeMethodHandle(), handle);
        }

        [Fact]
        public void DeclaringType_ReturnsEnumType()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var declaringType = methodInfo.DeclaringType;

            // Assert
            Assert.Equal(typeof(Enum), declaringType);
        }

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsEmptyArray()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var attributes = methodInfo.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.NotNull(attributes);
            Assert.Empty(attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsEmptyArray()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var attributes = methodInfo.GetCustomAttributes(false);

            // Assert
            Assert.NotNull(attributes);
            Assert.Empty(attributes);
        }

        [Fact]
        public void IsDefined_ReturnsTrue()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var isDefined = methodInfo.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.True(isDefined);
        }

        [Fact]
        public void Name_ReturnsOpEnum()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var name = methodInfo.Name;

            // Assert
            Assert.Equal("op_Enum", name);
        }

        [Fact]
        public void ReflectedType_ReturnsReturnType()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var reflectedType = methodInfo.ReflectedType;

            // Assert
            Assert.Equal(methodInfo.ReturnType, reflectedType);
        }

        [Fact]
        public void ReturnType_AddOperation_ReturnsCorrectType()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);

            // Act
            var returnType = methodInfo.ReturnType;

            // Assert
            Assert.Equal(typeof(PositionType), returnType);
        }

        [Fact]
        public void ReturnType_ComparisonOperation_ReturnsBool()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.ValueEquality, typeof(PositionType), false);

            // Act
            var returnType = methodInfo.ReturnType;

            // Assert
            Assert.Equal(typeof(bool), returnType);
        }
        #endregion

        #region Different Enum Types Tests
        [Fact]
        public void Invoke_LongEnumAddition_ReturnsCorrectResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(TestEnumLong), CodeBinaryOperatorType.Add, typeof(long), false);
            object[] parameters = [TestEnumLong.First, 1L];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestEnumLong>(result);
            Assert.Equal(TestEnumLong.Second, result);
        }

        [Fact]
        public void Invoke_ByteEnumComparison_ReturnsCorrectResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(TestEnumByte), CodeBinaryOperatorType.LessThan, typeof(TestEnumByte), false);
            object[] parameters = [TestEnumByte.First, TestEnumByte.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void Invoke_AddEnumZero_ReturnsCorrectResult()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);
            object[] parameters = [PositionType.Third, 0];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal(PositionType.Third, result);
        }

        [Fact]
        public void Invoke_SubtractToZero_ReturnsNone()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(int), false);
            object[] parameters = [PositionType.Second, 2];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal(PositionType.None, result);
        }

        [Fact]
        public void Invoke_SubtractSameEnum_ReturnsZero()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(PositionType), false);
            object[] parameters = [PositionType.Third, PositionType.Third];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Constructor_WithNullableTypes_HandlesCorrectly()
        {
            // Arrange & Act
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType?), CodeBinaryOperatorType.Add, typeof(int?), false);

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal(typeof(PositionType?), methodInfo.ReturnType);
            ParameterInfo[] parameters = methodInfo.GetParameters();
            Assert.Equal(2, parameters.Length);
            Assert.Equal(typeof(PositionType?), parameters[0].ParameterType);
            Assert.Equal(typeof(int?), parameters[1].ParameterType);
        }

        [Fact]
        public void Invoke_CompareLessThanWithEqual_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThan, typeof(PositionType), false);
            object[] parameters = [PositionType.Second, PositionType.Second];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_CompareGreaterThanWithEqual_ReturnsFalse()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThan, typeof(PositionType), false);
            object[] parameters = [PositionType.Fourth, PositionType.Fourth];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Multiple Operation Type Coverage
        [Fact]
        public void Constructor_AllComparisonOperations_CreateCorrectMethodInfos()
        {
            // Arrange & Act
            var equality = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.ValueEquality, typeof(PositionType), false);
            var lessThan = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThan, typeof(PositionType), false);
            var lessOrEqual = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.LessThanOrEqual, typeof(PositionType), false);
            var greaterThan = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThan, typeof(PositionType), false);
            var greaterOrEqual = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.GreaterThanOrEqual, typeof(PositionType), false);

            // Assert
            Assert.All([equality, lessThan, lessOrEqual, greaterThan, greaterOrEqual],
                m => Assert.Equal(typeof(bool), m.ReturnType));
        }

        [Fact]
        public void Invoke_AddWithLargerValue_HandlesOverflow()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Add, typeof(int), false);
            object[] parameters = [PositionType.Fifth, 100];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal((PositionType)105, result);
        }

        [Fact]
        public void Invoke_SubtractResultingInNegative_HandlesCorrectly()
        {
            // Arrange
            var methodInfo = new EnumOperationMethodInfo(typeof(PositionType), CodeBinaryOperatorType.Subtract, typeof(int), false);
            object[] parameters = [PositionType.First, 5];

            // Act
            object result = methodInfo.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PositionType>(result);
            Assert.Equal((PositionType)(-4), result);
        }
        #endregion
    }
}
