using System;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LiftedEqualityOperatorMethodInfoTest
    {
        #region Helper Classes and Methods
        public static class TestOperators
        {
            public static bool Equals(int a, int b) => a == b;
            public static bool NotEquals(long a, long b) => a != b;
            public static bool EqualsDecimal(decimal a, decimal b) => a == b;
            public static bool EqualsDouble(double a, double b) => Math.Abs(a - b) < double.Epsilon;
            public static bool EqualsString(string a, string b) => a == b;
            public static bool EqualsCustom(CustomType a, CustomType b) => a?.Value == b?.Value;
        }

        public class CustomType
        {
            public int Value { get; set; }
        }

        private static MethodInfo GetTestMethod(string methodName)
        {
            return typeof(TestOperators).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)!;
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_WithValidMethod_CreatesLiftedEqualityOperator()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Assert
            Assert.NotNull(liftedMethod);
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WrapsParametersAsNullable()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
            Assert.Equal(typeof(int?), parameters[0].ParameterType);
            Assert.Equal(typeof(int?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithNotEqualsMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithDecimalMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(decimal?), parameters[0].ParameterType);
            Assert.Equal(typeof(decimal?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithDoubleMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo equalsDoubleMethod = GetTestMethod("EqualsDouble");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDoubleMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(double?), parameters[0].ParameterType);
            Assert.Equal(typeof(double?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_ReturnTypeIsAlwaysBool()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            Assert.False(liftedMethod.ReturnType.IsGenericType); // Not nullable bool
        }
        #endregion

        #region Invoke Tests - Null Parameters
        [Fact]
        public void Invoke_BothParametersNull_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [null, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_FirstParameterNullSecondNotNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [null, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_SecondParameterNullFirstNotNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [5, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Invoke Tests - Valid Equality Comparisons
        [Fact]
        public void Invoke_EqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [5, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_DifferentValues_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [5, 10];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_NotEqualsWithEqualValues_ReturnsFalse()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);
            object[] parameters = [5L, 5L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_NotEqualsWithDifferentValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);
            object[] parameters = [5L, 10L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_DecimalEqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            object[] parameters = [3.14m, 3.14m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_DoubleEqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsDoubleMethod = GetTestMethod("EqualsDouble");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDoubleMethod);
            object[] parameters = [2.5, 2.5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_ZeroEqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [0, 0];
            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_NegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [-5, -5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_MaxAndMinValues_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [int.MaxValue, int.MinValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Invoke Tests - Null Handling Edge Cases
        [Fact]
        public void Invoke_BothNullWithNotEqualsOperator_ReturnsTrue()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);
            object?[] parameters = [null, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result); // null == null returns true from first check
        }

        [Fact]
        public void Invoke_FirstNullWithDecimal_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            object?[] parameters = [null, 3.14m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_SecondNullWithDecimal_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            object?[] parameters = [3.14m, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Inherited Property Tests
        [Fact]
        public void GetBaseDefinition_ReturnsActualMethodBaseDefinition()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            MethodInfo baseDefinition = liftedMethod.GetBaseDefinition();

            // Assert
            Assert.Equal(equalsMethod.GetBaseDefinition(), baseDefinition);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsActualMethodReturnTypeCustomAttributes()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            var customAttributes = liftedMethod.ReturnTypeCustomAttributes;

            // Assert
            Assert.Equal(equalsMethod.ReturnTypeCustomAttributes, customAttributes);
        }

        [Fact]
        public void Attributes_ReturnsActualMethodAttributesWithoutStatic()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            MethodAttributes attributes = liftedMethod.Attributes;

            // Assert
            Assert.Equal(equalsMethod.Attributes & ~MethodAttributes.Static, attributes);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsActualMethodImplementationFlags()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            var flags = liftedMethod.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(equalsMethod.GetMethodImplementationFlags(), flags);
        }

        [Fact]
        public void MethodHandle_ReturnsActualMethodHandle()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            RuntimeMethodHandle handle = liftedMethod.MethodHandle;

            // Assert
            Assert.Equal(equalsMethod.MethodHandle, handle);
        }

        [Fact]
        public void DeclaringType_ReturnsActualMethodDeclaringType()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            Type declaringType = liftedMethod.DeclaringType;

            // Assert
            Assert.Equal(typeof(TestOperators), declaringType);
        }

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(equalsMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false), attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(false);

            // Assert
            Assert.Equal(equalsMethod.GetCustomAttributes(false).Length, attributes.Length);
        }

        [Fact]
        public void IsDefined_ReturnsActualMethodIsDefined()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            bool isDefined = liftedMethod.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(equalsMethod.IsDefined(typeof(ObsoleteAttribute), false), isDefined);
        }

        [Fact]
        public void Name_ReturnsActualMethodName()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            string name = liftedMethod.Name;

            // Assert
            Assert.Equal("Equals", name);
        }

        [Fact]
        public void ReflectedType_ReturnsActualMethodReflectedType()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            Type reflectedType = liftedMethod.ReflectedType;

            // Assert
            Assert.Equal(equalsMethod.ReflectedType, reflectedType);
        }
        #endregion

        #region Equals and GetHashCode Tests
        [Fact]
        public void Equals_WithSameMethod_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod1 = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            var liftedMethod2 = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentMethods_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");
            var liftedMethod1 = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            var liftedMethod2 = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            bool areEqual = liftedMethod.Equals(null);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object other = "string";

            // Act
            bool areEqual = liftedMethod.Equals(other);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_WithSameMethod_ReturnsSameHashCode()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod1 = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            var liftedMethod2 = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            int hash1 = liftedMethod1.GetHashCode();
            int hash2 = liftedMethod2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_WithDifferentMethods_ReturnsDifferentHashCodes()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");
            var liftedMethod1 = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            var liftedMethod2 = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);

            // Act
            int hash1 = liftedMethod1.GetHashCode();
            int hash2 = liftedMethod2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void GetParameters_ReturnsExpectedParameterCount()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
        }

        [Fact]
        public void ReturnType_IsAlwaysBoolNotNullable()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);

            // Act
            Type returnType = liftedMethod.ReturnType;

            // Assert
            Assert.Equal(typeof(bool), returnType);
            Assert.False(returnType.IsGenericType); // Not Nullable<bool>
        }

        [Fact]
        public void Invoke_WithMaxValueAndSameValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [int.MaxValue, int.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithMinValueAndSameValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [int.MinValue, int.MinValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Constructor_ParametersAreNullableOfOriginalType()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
            Assert.True(parameters[0].ParameterType.IsGenericType);
            Assert.True(parameters[1].ParameterType.IsGenericType);
            Assert.Equal(typeof(Nullable<>), parameters[0].ParameterType.GetGenericTypeDefinition());
            Assert.Equal(typeof(Nullable<>), parameters[1].ParameterType.GetGenericTypeDefinition());
            Assert.Equal(typeof(long), parameters[0].ParameterType.GetGenericArguments()[0]);
            Assert.Equal(typeof(long), parameters[1].ParameterType.GetGenericArguments()[0]);
        }
        #endregion

        #region Comparison with Different Operators
        [Fact]
        public void Invoke_MultipleInvocationsWithSameValues_ConsistentResults()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [42, 42];

            // Act
            object result1 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);
            object result2 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(result1, result2);
            Assert.True((bool)result1);
        }

        [Fact]
        public void Invoke_NullChecksOccurBeforeMethodInvocation()
        {
            // Arrange
            // This test verifies that null checks happen before actual method invocation
            // If the first parameter is null, we should get the null handling result
            // without calling the underlying method
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object?[] parameters = [null, 100];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Constructor Validation Tests
        [Fact]
        public void Constructor_WithLongParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo notEqualsMethod = GetTestMethod("NotEquals");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(notEqualsMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(long?), parameters[0].ParameterType);
            Assert.Equal(typeof(long?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithDecimalParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(decimal?), parameters[0].ParameterType);
            Assert.Equal(typeof(decimal?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithDoubleParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo equalsDoubleMethod = GetTestMethod("EqualsDouble");

            // Act
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDoubleMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(double?), parameters[0].ParameterType);
            Assert.Equal(typeof(double?), parameters[1].ParameterType);
        }
        #endregion

        #region Special Value Tests
        [Fact]
        public void Invoke_WithVeryLargeDecimalValues_HandlesCorrectly()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            decimal largeValue = decimal.MaxValue;
            object[] parameters = [largeValue, largeValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithVerySmallDecimalValues_HandlesCorrectly()
        {
            // Arrange
            MethodInfo equalsDecimalMethod = GetTestMethod("EqualsDecimal");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDecimalMethod);
            decimal smallValue = decimal.MinValue;
            object[] parameters = [smallValue, smallValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithZeroAndNonZero_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [0, 1];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_WithNegativeAndPositiveValues_ReturnsFalse()
        {
            // Arrange
            MethodInfo equalsMethod = GetTestMethod("Equals");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsMethod);
            object[] parameters = [-5, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_WithDoubleNaN_HandlesCorrectly()
        {
            // Arrange
            MethodInfo equalsDoubleMethod = GetTestMethod("EqualsDouble");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDoubleMethod);
            object[] parameters = [double.NaN, double.NaN];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            // NaN == NaN is false in IEEE 754 standard
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_WithDoubleInfinity_HandlesCorrectly()
        {
            // Arrange
            MethodInfo equalsDoubleMethod = GetTestMethod("EqualsDouble");
            var liftedMethod = new LiftedEqualityOperatorMethodInfo(equalsDoubleMethod);
            object[] parameters = [double.PositiveInfinity, double.PositiveInfinity];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);//indeterminate result due to infinity - infinity being NaN
        }
        #endregion
    }
}
