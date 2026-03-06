using System;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LiftedConversionMethodInfoTest
    {
        #region Helper Structs with Conversion Operators

        public struct IntValue
        {
            public int Value { get; set; }

            public static implicit operator long(IntValue source)
            {
                return source.Value;
            }

            public static explicit operator short(IntValue source)
            {
                return (short)source.Value;
            }
        }

        public struct DecimalValue
        {
            public decimal Value { get; set; }

            public static implicit operator double(DecimalValue source)
            {
                return (double)source.Value;
            }

            public static explicit operator int(DecimalValue source)
            {
                return (int)source.Value;
            }
        }

        public struct ComplexValue
        {
            public int X { get; set; }
            public int Y { get; set; }

            public static implicit operator double(ComplexValue source)
            {
                return source.X + source.Y;
            }

            public static explicit operator int(ComplexValue source)
            {
                return source.X * source.Y;
            }
        }

        public struct ByteValue
        {
            public byte Value { get; set; }

            public static implicit operator int(ByteValue source)
            {
                return source.Value;
            }
        }

        public struct FloatValue
        {
            public float Value { get; set; }

            public static explicit operator decimal(FloatValue source)
            {
                return (decimal)source.Value;
            }
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidConversionMethod_CreatesInstance()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.NotNull(liftedConversion);
            Assert.Equal(conversionMethod.Name, liftedConversion.Name);
        }

        [Fact]
        public void Constructor_WithConversionMethod_SetsReturnTypeToNullable()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Type expectedReturnType = typeof(long?);
            Assert.Equal(expectedReturnType, liftedConversion.ReturnType);
        }

        [Fact]
        public void Constructor_WithConversionMethod_WrapsParameterInNullable()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            ParameterInfo[] parameters = liftedConversion.GetParameters();
            Assert.Single(parameters);
        }

        [Fact]
        public void Constructor_WithExplicitConversion_CreatesLiftedVersion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.NotNull(liftedConversion);
            Assert.Equal(typeof(short?), liftedConversion.ReturnType);
        }

        [Fact]
        public void Constructor_WithStructConversion_CreatesLiftedVersion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ComplexValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ComplexValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.NotNull(liftedConversion);
            Assert.Equal(typeof(double?), liftedConversion.ReturnType);
        }

        [Fact]
        public void Constructor_WithDecimalConversion_CreatesLiftedVersion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.Equal(typeof(decimal?), liftedConversion.ReturnType);
        }

        [Fact]
        public void Constructor_PreservesMethodName()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.Equal("op_Implicit", liftedConversion.Name);
        }

        [Fact]
        public void Constructor_PreservesDeclaringType()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.Equal(typeof(IntValue), liftedConversion.DeclaringType);
        }

        #endregion

        #region Invoke Tests - Null Parameter

        [Fact]
        public void Invoke_WithNullParameter_ReturnsDefaultNullableInstance()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            object[] parameters = [null!];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert - Activator.CreateInstance for nullable returns null
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_WithNullParameter_CreatesDefaultNullableInstance()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            object[] parameters = [null!];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Invoke_WithNullParameterForStructConversion_ReturnsNull()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ComplexValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ComplexValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            object[] parameters = [null!];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Invoke Tests - Non-Null Parameter

        [Fact]
        public void Invoke_WithValidParameter_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new IntValue { Value = 42 };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert - Executor.AdjustType wraps it in nullable
            Assert.NotNull(result);
            Assert.Equal(42L, (long)result);
        }

        [Fact]
        public void Invoke_WithStructParameter_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ComplexValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ComplexValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new ComplexValue { X = 10, Y = 20 };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(30.0, (double)result);
        }

        [Fact]
        public void Invoke_WithIntToDecimalConversion_ReturnsDecimal()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new IntWrapper { Value = 100 };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100m, (decimal)result);
        }

        [Fact]
        public void Invoke_WithLongToDoubleConversion_ReturnsDouble()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(LongWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(LongWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new LongWrapper { Value = 1000L };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1000.0, (double)result);
        }

        [Fact]
        public void Invoke_WithFloatToDecimalConversion_ReturnsDecimal()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(FloatWrapper).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(FloatWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new FloatWrapper { Value = 3.14f };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3.14m, (decimal)result, 2);
        }

        [Fact]
        public void Invoke_WithStringToIntConversion_ReturnsInt()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(StringWrapper).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(StringWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new StringWrapper { Value = "123" };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123, (int)result);
        }

        [Fact]
        public void Invoke_WithBoolToIntConversion_ReturnsInt()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(BoolWrapper).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(BoolWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new BoolWrapper { Value = true };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, (int)result);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void ReturnType_ReturnsNullableWrappedType()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            Type returnType = liftedConversion.ReturnType;

            // Assert
            Assert.True(returnType.IsGenericType);
            Assert.Equal(typeof(Nullable<>), returnType.GetGenericTypeDefinition());
            Assert.Equal(typeof(long), Nullable.GetUnderlyingType(returnType));
        }

        [Fact]
        public void GetParameters_ReturnsSingleModifiedParameter()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            ParameterInfo[] parameters = liftedConversion.GetParameters();

            // Assert
            Assert.Single(parameters);
            Assert.IsType<SimpleParameterInfo>(parameters[0]);
        }

        [Fact]
        public void DeclaringType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            Type declaringType = liftedConversion.DeclaringType;

            // Assert
            Assert.Equal(typeof(IntValue), declaringType);
        }

        [Fact]
        public void ReflectedType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            Type reflectedType = liftedConversion.ReflectedType;

            // Assert
            Assert.Equal(typeof(IntValue), reflectedType);
        }

        [Fact]
        public void Name_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            string name = liftedConversion.Name;

            // Assert
            Assert.Equal("op_Implicit", name);
        }

        [Fact]
        public void Attributes_RemovesStaticFlag()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            MethodAttributes attributes = liftedConversion.Attributes;

            // Assert
            Assert.False(attributes.HasFlag(MethodAttributes.Static));
        }

        [Fact]
        public void MethodHandle_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            RuntimeMethodHandle handle = liftedConversion.MethodHandle;

            // Assert
            Assert.Equal(conversionMethod.MethodHandle, handle);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            var customAttributes = liftedConversion.ReturnTypeCustomAttributes;

            // Assert
            Assert.NotNull(customAttributes);
            Assert.Equal(conversionMethod.ReturnTypeCustomAttributes, customAttributes);
        }

        [Fact]
        public void GetBaseDefinition_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            MethodInfo baseDefinition = liftedConversion.GetBaseDefinition();

            // Assert
            Assert.NotNull(baseDefinition);
            Assert.Equal(conversionMethod.GetBaseDefinition(), baseDefinition);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            MethodImplAttributes implFlags = liftedConversion.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(conversionMethod.GetMethodImplementationFlags(), implFlags);
        }

        #endregion

        #region Custom Attribute Tests

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            object[] attributes = liftedConversion.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.NotNull(attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            object[] attributes = liftedConversion.GetCustomAttributes(false);

            // Assert
            Assert.NotNull(attributes);
        }

        [Fact]
        public void IsDefined_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            bool isDefined = liftedConversion.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.False(isDefined);
        }

        #endregion

        #region Equality and HashCode Tests

        [Fact]
        public void Equals_WithSameMethod_ReturnsTrue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion1 = new LiftedConversionMethodInfo(conversionMethod);
            var liftedConversion2 = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            bool result = liftedConversion1.Equals(liftedConversion2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentMethod_ReturnsFalse()
        {
            // Arrange
            MethodInfo conversionMethod1 = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            MethodInfo conversionMethod2 = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion1 = new LiftedConversionMethodInfo(conversionMethod1);
            var liftedConversion2 = new LiftedConversionMethodInfo(conversionMethod2);

            // Act
            bool result = liftedConversion1.Equals(liftedConversion2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsConsistentValue()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Act
            int hash1 = liftedConversion.GetHashCode();
            int hash2 = liftedConversion.GetHashCode();

            // Assert - Same instance should return same hash
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_WithDifferentMethod_ReturnsDifferentHash()
        {
            // Arrange
            MethodInfo conversionMethod1 = typeof(IntValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            MethodInfo conversionMethod2 = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion1 = new LiftedConversionMethodInfo(conversionMethod1);
            var liftedConversion2 = new LiftedConversionMethodInfo(conversionMethod2);

            // Act
            int hash1 = liftedConversion1.GetHashCode();
            int hash2 = liftedConversion2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void Invoke_WithZeroValue_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new IntWrapper { Value = 0 };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0m, (decimal)result);
        }

        [Fact]
        public void Invoke_WithNegativeValue_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new IntWrapper { Value = -50 };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(-50m, (decimal)result);
        }

        [Fact]
        public void Invoke_WithMaxIntValue_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntWrapper).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntWrapper)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var wrapper = new IntWrapper { Value = int.MaxValue };
            object[] parameters = [wrapper];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((decimal)int.MaxValue, (decimal)result);
        }

        [Fact]
        public void Constructor_WithExplicitStructConversion_CreatesLiftedVersion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ComplexValue).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ComplexValue)], null)!;

            // Act
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);

            // Assert
            Assert.NotNull(liftedConversion);
            Assert.Equal(typeof(int?), liftedConversion.ReturnType);
        }

        [Fact]
        public void Invoke_WithExplicitStructConversion_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ComplexValue).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ComplexValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new ComplexValue { X = 15, Y = 25 };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(375, (int)result);
        }

        [Fact]
        public void Invoke_WithByteConversion_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(ByteValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(ByteValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new ByteValue { Value = 255 };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(255, (int)result);
        }

        [Fact]
        public void Invoke_WithExplicitConversion_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(IntValue).GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(IntValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new IntValue { Value = 1000 };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((short)1000, (short)result);
        }

        [Fact]
        public void Invoke_WithDecimalToDoubleConversion_PerformsConversion()
        {
            // Arrange
            MethodInfo conversionMethod = typeof(DecimalValue).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, [typeof(DecimalValue)], null)!;
            var liftedConversion = new LiftedConversionMethodInfo(conversionMethod);
            var source = new DecimalValue { Value = 123.456m };
            object[] parameters = [source];

            // Act
            object result = liftedConversion.Invoke(null, BindingFlags.Default, null, parameters, CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123.456, (double)result, 3);
        }

        #endregion
    }

    #region Test Helper Conversion Operators

    public struct ConversionOperators
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static implicit operator decimal(ConversionOperators value)
        {
            return 0m;
        }

        public static implicit operator double(ConversionOperators value)
        {
            return 0.0;
        }

        public static explicit operator float(ConversionOperators value)
        {
            return 0f;
        }
#pragma warning restore IDE0060 // Remove unused parameter
    }

    public struct IntWrapper
    {
        public int Value { get; set; }

        public static implicit operator decimal(IntWrapper value)
        {
            return (decimal)value.Value;
        }
    }

    public struct LongWrapper
    {
        public long Value { get; set; }

        public static implicit operator double(LongWrapper value)
        {
            return (double)value.Value;
        }
    }

    public struct FloatWrapper
    {
        public float Value { get; set; }

        public static explicit operator decimal(FloatWrapper value)
        {
            return (decimal)value.Value;
        }
    }

    public struct StringWrapper
    {
        public string Value { get; set; }

        public static explicit operator int(StringWrapper value)
        {
            return int.Parse(value.Value);
        }
    }

    public struct BoolWrapper
    {
        public bool Value { get; set; }

        public static explicit operator int(BoolWrapper value)
        {
            return value.Value ? 1 : 0;
        }
    }

    #endregion
}
