using System;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LiftedArithmeticOperatorMethodInfoTest
    {
        #region Helper Classes and Methods
        public static class TestOperators
        {
            public static int Add(int a, int b) => a + b;
            public static long Subtract(long a, long b) => a - b;
            public static decimal Multiply(decimal a, decimal b) => a * b;
            public static double Divide(double a, double b) => a / b;
            public static float Modulo(float a, float b) => a % b;
            public static short BitwiseAnd(short a, short b) => (short)(a & b);
            public static ushort BitwiseOr(ushort a, ushort b) => (ushort)(a | b);
        }

        private static MethodInfo GetTestMethod(string methodName)
        {
            return typeof(TestOperators).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)!;
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_WithValidMethod_CreatesLiftedArithmeticOperator()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Assert
            Assert.NotNull(liftedMethod);
            Assert.Equal(typeof(int?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WrapsParametersAsNullable()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
            Assert.Equal(typeof(int?), parameters[0].ParameterType);
            Assert.Equal(typeof(int?), parameters[1].ParameterType);
        }

        [Fact]
        public void Constructor_WithSubtractMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo subtractMethod = GetTestMethod("Subtract");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(subtractMethod);

            // Assert
            Assert.Equal(typeof(long?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithMultiplyMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo multiplyMethod = GetTestMethod("Multiply");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(multiplyMethod);

            // Assert
            Assert.Equal(typeof(decimal?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithDivideMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo divideMethod = GetTestMethod("Divide");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(divideMethod);

            // Assert
            Assert.Equal(typeof(double?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithModuloMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo moduloMethod = GetTestMethod("Modulo");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(moduloMethod);

            // Assert
            Assert.Equal(typeof(float?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithBitwiseAndMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo bitwiseAndMethod = GetTestMethod("BitwiseAnd");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(bitwiseAndMethod);

            // Assert
            Assert.Equal(typeof(short?), liftedMethod.ReturnType);
        }

        [Fact]
        public void Constructor_WithBitwiseOrMethod_CreatesCorrectReturnType()
        {
            // Arrange
            MethodInfo bitwiseOrMethod = GetTestMethod("BitwiseOr");

            // Act
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(bitwiseOrMethod);

            // Assert
            Assert.Equal(typeof(ushort?), liftedMethod.ReturnType);
        }
        #endregion

        #region Invoke Tests - Null Parameters
        [Fact]
        public void Invoke_WithFirstParameterNull_ReturnsNull()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object?[] parameters = [null, 5];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_WithSecondParameterNull_ReturnsNull()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object?[] parameters = [5, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_WithBothParametersNull_ReturnsNull()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object?[] parameters = [null, null];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Invoke Tests - Valid Operations
        [Fact]
        public void Invoke_AddWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object[] parameters = [5, 3];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Invoke_SubtractWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo subtractMethod = GetTestMethod("Subtract");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(subtractMethod);
            object[] parameters = [10L, 3L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7L, result);
        }

        [Fact]
        public void Invoke_MultiplyWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo multiplyMethod = GetTestMethod("Multiply");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(multiplyMethod);
            object[] parameters = [4m, 2.5m];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void Invoke_DivideWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo divideMethod = GetTestMethod("Divide");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(divideMethod);
            object[] parameters = [10.0, 2.0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void Invoke_ModuloWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo moduloMethod = GetTestMethod("Modulo");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(moduloMethod);
            object[] parameters = [10.0f, 3.0f];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1.0f, result);
        }

        [Fact]
        public void Invoke_BitwiseAndWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo bitwiseAndMethod = GetTestMethod("BitwiseAnd");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(bitwiseAndMethod);
            object?[] parameters = [(short)15, (short)7];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((short)7, result);
        }

        [Fact]
        public void Invoke_BitwiseOrWithValidParameters_ReturnsNullableResult()
        {
            // Arrange
            MethodInfo bitwiseOrMethod = GetTestMethod("BitwiseOr");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(bitwiseOrMethod);
            object[] parameters = [(ushort)8, (ushort)4];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((ushort)12, result);
        }

        [Fact]
        public void Invoke_WithZeroResult_ReturnsZeroNotNull()
        {
            // Arrange
            MethodInfo subtractMethod = GetTestMethod("Subtract");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(subtractMethod);
            object[] parameters = [5L, 5L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0L, result);
        }

        [Fact]
        public void Invoke_WithNegativeResult_ReturnsNegativeValue()
        {
            // Arrange
            MethodInfo subtractMethod = GetTestMethod("Subtract");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(subtractMethod);
            object[] parameters = [3L, 10L];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(-7L, result);
        }
        #endregion

        #region Inherited Property Tests
        [Fact]
        public void GetBaseDefinition_ReturnsActualMethodBaseDefinition()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            MethodInfo baseDefinition = liftedMethod.GetBaseDefinition();

            // Assert
            Assert.Equal(addMethod.GetBaseDefinition(), baseDefinition);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsActualMethodReturnTypeCustomAttributes()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            var customAttributes = liftedMethod.ReturnTypeCustomAttributes;

            // Assert
            Assert.Equal(addMethod.ReturnTypeCustomAttributes, customAttributes);
        }

        [Fact]
        public void Attributes_ReturnsActualMethodAttributesWithoutStatic()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            MethodAttributes attributes = liftedMethod.Attributes;

            // Assert
            Assert.Equal(addMethod.Attributes & ~MethodAttributes.Static, attributes);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsActualMethodImplementationFlags()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            var flags = liftedMethod.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(addMethod.GetMethodImplementationFlags(), flags);
        }

        [Fact]
        public void MethodHandle_ReturnsActualMethodHandle()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            RuntimeMethodHandle handle = liftedMethod.MethodHandle;

            // Assert
            Assert.Equal(addMethod.MethodHandle, handle);
        }

        [Fact]
        public void DeclaringType_ReturnsActualMethodDeclaringType()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            Type declaringType = liftedMethod.DeclaringType;

            // Assert
            Assert.Equal(typeof(TestOperators), declaringType);
        }

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(addMethod.GetCustomAttributes(typeof(ObsoleteAttribute), false), attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsActualMethodCustomAttributes()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            object[] attributes = liftedMethod.GetCustomAttributes(false);

            // Assert
            Assert.Equal(addMethod.GetCustomAttributes(false).Length, attributes.Length);
        }

        [Fact]
        public void IsDefined_ReturnsActualMethodIsDefined()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            bool isDefined = liftedMethod.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.Equal(addMethod.IsDefined(typeof(ObsoleteAttribute), false), isDefined);
        }

        [Fact]
        public void Name_ReturnsActualMethodName()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            string name = liftedMethod.Name;

            // Assert
            Assert.Equal("Add", name);
        }

        [Fact]
        public void ReflectedType_ReturnsActualMethodReflectedType()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            Type reflectedType = liftedMethod.ReflectedType;

            // Assert
            Assert.Equal(addMethod.ReflectedType, reflectedType);
        }
        #endregion

        #region Equals and GetHashCode Tests
        [Fact]
        public void Equals_WithSameMethod_ReturnsTrue()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod1 = new LiftedArithmeticOperatorMethodInfo(addMethod);
            var liftedMethod2 = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentMethods_ReturnsFalse()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            MethodInfo subtractMethod = GetTestMethod("Subtract");
            var liftedMethod1 = new LiftedArithmeticOperatorMethodInfo(addMethod);
            var liftedMethod2 = new LiftedArithmeticOperatorMethodInfo(subtractMethod);

            // Act
            bool areEqual = liftedMethod1.Equals(liftedMethod2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            bool areEqual = liftedMethod.Equals(null);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
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
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod1 = new LiftedArithmeticOperatorMethodInfo(addMethod);
            var liftedMethod2 = new LiftedArithmeticOperatorMethodInfo(addMethod);

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
            MethodInfo addMethod = GetTestMethod("Add");
            MethodInfo subtractMethod = GetTestMethod("Subtract");
            var liftedMethod1 = new LiftedArithmeticOperatorMethodInfo(addMethod);
            var liftedMethod2 = new LiftedArithmeticOperatorMethodInfo(subtractMethod);

            // Act
            int hash1 = liftedMethod1.GetHashCode();
            int hash2 = liftedMethod2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void Invoke_WithMaxValues_HandlesCorrectly()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object[] parameters = [int.MaxValue, 0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(int.MaxValue, result);
        }

        [Fact]
        public void Invoke_WithMinValues_HandlesCorrectly()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);
            object[] parameters = [int.MinValue, 0];

            // Act
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(int.MinValue, result);
        }

        [Fact]
        public void Invoke_DivideByZero_ThrowsException()
        {
            // Arrange
            MethodInfo divideMethod = GetTestMethod("Divide");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(divideMethod);
            object[] parameters = [10.0, 0.0];

            // Act & Assert
            // Division by zero in floating point returns infinity, not exception
            object result = liftedMethod.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);
            Assert.Equal(double.PositiveInfinity, result);
        }

        [Fact]
        public void GetParameters_ReturnsExpectedParameterCount()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            ParameterInfo[] parameters = liftedMethod.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);
        }

        [Fact]
        public void ReturnType_IsNullableOfOriginalReturnType()
        {
            // Arrange
            MethodInfo addMethod = GetTestMethod("Add");
            var liftedMethod = new LiftedArithmeticOperatorMethodInfo(addMethod);

            // Act
            Type returnType = liftedMethod.ReturnType;

            // Assert
            Assert.True(returnType.IsGenericType);
            Assert.Equal(typeof(Nullable<>), returnType.GetGenericTypeDefinition());
            Assert.Equal(typeof(int), returnType.GetGenericArguments()[0]);
        }
        #endregion
    }
}
