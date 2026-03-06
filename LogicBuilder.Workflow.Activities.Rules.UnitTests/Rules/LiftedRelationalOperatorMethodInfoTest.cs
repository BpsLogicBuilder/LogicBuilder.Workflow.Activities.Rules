using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LiftedRelationalOperatorMethodInfoTest
    {
        #region Helper Classes and Methods
        public static class TestOperators
        {
            public static bool LessThan(int a, int b) => a < b;
            public static bool LessThanOrEqual(long a, long b) => a <= b;
            public static bool GreaterThan(decimal a, decimal b) => a > b;
            public static bool GreaterThanOrEqual(double a, double b) => a >= b;
            public static bool LessThanFloat(float a, float b) => a < b;
            public static bool GreaterThanShort(short a, short b) => a > b;
            public static bool LessThanUInt(uint a, uint b) => a < b;
        }

        private static MethodInfo GetTestMethod(string methodName)
        {
            return typeof(TestOperators).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)!;
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_WithValidMethod_CreatesLiftedRelationalOperator()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Assert
            Assert.NotNull(liftedMethod);
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WrapsParametersAsNullable()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
            Assert.Equal(typeof(int?), parameters[0].ParameterType);
            Assert.Equal(typeof(int?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithLessThanOrEqualMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(long?), parameters[0].ParameterType);
            Assert.Equal(typeof(long?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithGreaterThanMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(decimal?), parameters[0].ParameterType);
            Assert.Equal(typeof(decimal?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithGreaterThanOrEqualMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(double?), parameters[0].ParameterType);
            Assert.Equal(typeof(double?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithFloatMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo lessThanFloatMethod = GetTestMethod("LessThanFloat");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanFloatMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            ParameterInfo[] parameters = liftedMethod.GetParameters();
            Assert.Equal(typeof(float?), parameters[0].ParameterType);
            Assert.Equal(typeof(float?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_ReturnTypeIsAlwaysBool()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Assert
            Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            Assert.False(liftedMethod.ReturnType.IsGenericType); // Not nullable bool
        }
        #endregion

        #region Invoke Tests - Null Parameters
        [Fact]
        public void Invoke_BothParametersNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [null, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result); // Relational operators return false for null
        }

        [Fact]
        public void Invoke_FirstParameterNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [null, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_SecondParameterNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [5, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_FirstNullWithGreaterThan_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object?[] parameters = [null, 10m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_SecondNullWithLessThanOrEqual_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
            object?[] parameters = [5L, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Invoke Tests - Valid Relational Comparisons
        [Fact]
        public void Invoke_LessThanWithSmallerValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [3, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_LessThanWithLargerValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [10, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_LessThanWithEqualValues_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [5, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_LessThanOrEqualWithSmallerValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
            object[] parameters = [3L, 5L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_LessThanOrEqualWithEqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
            object[] parameters = [5L, 5L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_LessThanOrEqualWithLargerValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
            object[] parameters = [10L, 5L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanWithLargerValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [10m, 5m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanWithSmallerValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [3m, 5m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanWithEqualValues_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [5m, 5m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqualWithLargerValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);
            object[] parameters = [10.0, 5.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqualWithEqualValues_ReturnsTrue()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);
            object[] parameters = [5.0, 5.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqualWithSmallerValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);
            object[] parameters = [3.0, 5.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Invoke Tests - Zero and Negative Values
        [Fact]
        public void Invoke_WithZeroAndPositive_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [0, 1];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithNegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [-10, -5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithNegativeAndPositive_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [-5, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }
        #endregion

        #region Invoke Tests - Edge Values
        [Fact]
        public void Invoke_WithMaxValue_HandlesCorrectly()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [int.MaxValue - 1, int.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithMinValue_HandlesCorrectly()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [decimal.MinValue + 1, decimal.MinValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithVeryLargeDecimalValues_HandlesCorrectly()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [decimal.MaxValue, decimal.MaxValue - 1];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithDoubleInfinity_HandlesCorrectly()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [double.PositiveInfinity, double.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_WithDoubleNaN_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);
            object[] parameters = [double.NaN, 5.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            // NaN comparisons always return false
            Assert.False((bool)result);
        }
        #endregion

        #region Inherited Property Tests
        [Fact]
        public void GetBaseDefinition_ReturnsActualMethodBaseDefinition()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            MethodInfo baseDefinition = liftedMethod.GetBaseDefinition();

            // Assert
            Assert.Equal(lessThanMethod.GetBaseDefinition(), baseDefinition);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsActualMethodReturnTypeCustomAttributes()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            var customAttributes = liftedMethod.ReturnTypeCustomAttributes;

            // Assert
            Assert.Equal(lessThanMethod.ReturnTypeCustomAttributes, customAttributes);
        }

        [Fact]
        public void Attributes_ReturnsActualMethodAttributesWithoutStatic()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            MethodAttributes attributes = liftedMethod.Attributes;

            // Assert
            Assert.Equal(lessThanMethod.Attributes & ~MethodAttributes.Static, attributes);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsActualMethodImplementationFlags()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            var flags = liftedMethod.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(lessThanMethod.GetMethodImplementationFlags(), flags);
        }

        [Fact]
        public void MethodHandle_ReturnsActualMethodHandle()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            RuntimeMethodHandle handle = liftedMethod.MethodHandle;

            // Assert
            Assert.Equal(lessThanMethod.MethodHandle, handle);
        }

        [Fact]
        public void DeclaringType_ReturnsActualMethodDeclaringType()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            Type declaringType = liftedMethod.DeclaringType;

            // Assert
            Assert.Equal(typeof(TestOperators), declaringType);
        }

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(lessThanMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false), attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(false);

            // Assert
            Assert.Equal(lessThanMethod.GetCustomAttributes(false).Length, attributes.Length);
        }

        [Fact]
        public void IsDefined_ReturnsActualMethodIsDefined()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            bool isDefined = liftedMethod.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(lessThanMethod.IsDefined(typeof(ObsoleteAttribute), false), isDefined);
        }

        [Fact]
        public void Name_ReturnsActualMethodName()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            string name = liftedMethod.Name;

            // Assert
            Assert.Equal("LessThan", name);
        }

        [Fact]
        public void ReflectedType_ReturnsActualMethodReflectedType()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            Type reflectedType = liftedMethod.ReflectedType;

            // Assert
            Assert.Equal(lessThanMethod.ReflectedType, reflectedType);
        }
        #endregion

        #region Equals and GetHashCode Tests
        [Fact]
        public void Equals_WithSameMethod_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod1 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            var liftedMethod2 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentMethods_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod1 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            var liftedMethod2 = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            bool areEqual = liftedMethod.Equals(null);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
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
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod1 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            var liftedMethod2 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

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
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod1 = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            var liftedMethod2 = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);

            // Act
            int hash1 = liftedMethod1.GetHashCode();
            int hash2 = liftedMethod2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
        #endregion

        #region Additional Type Tests
        [Fact]
        public void Constructor_WithFloatParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo lessThanFloatMethod = GetTestMethod("LessThanFloat");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanFloatMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(float?), parameters[0].ParameterType);
            Assert.Equal(typeof(float?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithShortParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo greaterThanShortMethod = GetTestMethod("GreaterThanShort");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanShortMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(short?), parameters[0].ParameterType);
            Assert.Equal(typeof(short?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithUIntParameters_CreatesCorrectParameterTypes()
        {
            // Arrange
            MethodInfo lessThanUIntMethod = GetTestMethod("LessThanUInt");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanUIntMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(typeof(uint?), parameters[0].ParameterType);
            Assert.Equal(typeof(uint?), parameters[1].ParameterType);
        }

        [Fact]
        public void Invoke_FloatLessThan_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo lessThanFloatMethod = GetTestMethod("LessThanFloat");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanFloatMethod);
            object[] parameters = [1.5f, 2.5f];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_ShortGreaterThan_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo greaterThanShortMethod = GetTestMethod("GreaterThanShort");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanShortMethod);
            object[] parameters = [(short)10, (short)5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_UIntLessThan_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo lessThanUIntMethod = GetTestMethod("LessThanUInt");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanUIntMethod);
            object[] parameters = [5u, 10u];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }
        #endregion

        #region Null Handling Verification Tests
        [Fact]
        public void Invoke_NullCheckOccursBeforeMethodInvocation()
        {
            // Arrange
            // This test verifies that null checks happen before actual method invocation
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [null, 100];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_MultipleInvocationsWithNulls_ConsistentResults()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [null, null];

            // Act
            object result1 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);
            object result2 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(result1, result2);
            Assert.False((bool)result1);
        }

        [Fact]
        public void Invoke_MultipleInvocationsWithValidValues_ConsistentResults()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [10m, 5m];

            // Act
            object result1 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);
            object result2 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(result1, result2);
            Assert.True((bool)result1);
        }
        #endregion

        #region Parameter Structure Tests
        [Fact]
        public void GetParameters_ReturnsExpectedParameterCount()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
        }

        [Fact]
        public void ReturnType_IsAlwaysBoolNotNullable()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);

            // Act
            Type returnType = liftedMethod.ReturnType;

            // Assert
            Assert.Equal(typeof(bool), returnType);
            Assert.False(returnType.IsGenericType); // Not Nullable<bool>
        }

        [Fact]
        public void Constructor_ParametersAreNullableOfOriginalType()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");

            // Act
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
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

        #region Comparison Semantics Tests
        [Fact]
        public void Invoke_LessThanWithZeroAndZero_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [0, 0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanWithNegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [-3m, -5m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result); // -3 > -5
        }

        [Fact]
        public void Invoke_LessThanOrEqualWithZero_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanOrEqualMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanOrEqualMethod);
            object[] parameters = [0L, 0L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_GreaterThanOrEqualWithNegativeAndZero_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);
            object[] parameters = [-1.0, 0.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }
        #endregion

        #region Null Handling Difference from Equality Tests
        [Fact]
        public void Invoke_BothNullReturnsFalse_UnlikeEqualityOperator()
        {
            // Arrange
            // This test verifies the key difference between relational and equality operators:
            // null == null returns true (equality)
            // null < null returns false (relational)
            MethodInfo lessThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object?[] parameters = [null, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result); // Key difference: relational operators return false for null
        }

        [Fact]
        public void Invoke_AnyNullParameter_AlwaysReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanOrEqualMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanOrEqualMethod);

            // Act & Assert - First null
            object?[] parameters1 = [null, 100.0];
            object result1 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters1, CultureInfo.InvariantCulture);
            Assert.False((bool)result1);

            // Act & Assert - Second null
            object?[] parameters2 = [100.0, null];
            object result2 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters2, CultureInfo.InvariantCulture);
            Assert.False((bool)result2);

            // Act & Assert - Both null
            object?[] parameters3 = [null, null];
            object result3 = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters3, CultureInfo.InvariantCulture);
            Assert.False((bool)result3);
        }
        #endregion

        #region Special Floating Point Tests
        [Fact]
        public void Invoke_PositiveInfinityGreaterThanMaxValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [double.PositiveInfinity, double.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result);
        }

        [Fact]
        public void Invoke_NegativeInfinityLessThanMinValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("GreaterThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [double.MinValue, double.NegativeInfinity];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result); // MinValue >= NegativeInfinity
        }

        [Fact]
        public void Invoke_WithFloatNaN_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanFloatMethod = GetTestMethod("LessThanFloat");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanFloatMethod);
            object[] parameters = [float.NaN, 5.0f];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            // NaN comparisons always return false
            Assert.False((bool)result);
        }
        #endregion

        #region Cross-Operator Consistency Tests
        [Fact]
        public void Invoke_AllRelationalOperators_HandleNullConsistently()
        {
            // Arrange
            var methods = new[]
            {
                GetTestMethod("LessThan"),
                GetTestMethod("LessThanOrEqual"),
                GetTestMethod("GreaterThan"),
                GetTestMethod("GreaterThanOrEqual")
            };

            // Act & Assert
            foreach (var liftedMethod in methods.Select(m => new LiftedRelationalOperatorMethodInfo(m)))
            {
                object?[] parameters = [null, null];
                object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);
                Assert.False((bool)result, $"{liftedMethod.Name} should return false for null parameters");
            }
        }

        [Fact]
        public void Constructor_AllRelationalOperators_HaveBoolReturnType()
        {
            // Arrange
            var methods = new[]
            {
                GetTestMethod("LessThan"),
                GetTestMethod("LessThanOrEqual"),
                GetTestMethod("GreaterThan"),
                GetTestMethod("GreaterThanOrEqual")
            };

            // Act & Assert
            foreach (var liftedMethod in methods.Select(m => new LiftedRelationalOperatorMethodInfo(m)))
            {
                Assert.Equal(typeof(bool), liftedMethod.ReturnType);
            }
        }
        #endregion

        #region Boundary Value Tests
        [Fact]
        public void Invoke_DecimalMaxValueLessThanMaxValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("GreaterThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [decimal.MaxValue, decimal.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_IntMinValueLessThanMinValue_ReturnsFalse()
        {
            // Arrange
            MethodInfo lessThanMethod = GetTestMethod("LessThan");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(lessThanMethod);
            object[] parameters = [int.MinValue, int.MinValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.False((bool)result);
        }

        [Fact]
        public void Invoke_LongMaxGreaterThanMinValue_ReturnsTrue()
        {
            // Arrange
            MethodInfo greaterThanMethod = GetTestMethod("LessThanOrEqual");
            var liftedMethod = new LiftedRelationalOperatorMethodInfo(greaterThanMethod);
            object[] parameters = [long.MinValue, long.MaxValue];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            Assert.True((bool)result); // MinValue <= MaxValue
        }
        #endregion
    }
}
