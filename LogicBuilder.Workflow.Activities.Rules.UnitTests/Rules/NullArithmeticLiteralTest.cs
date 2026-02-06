namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class NullArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_WithType_SetsTypeCorrectly()
        {
            // Arrange & Act
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Assert
            Assert.Equal(typeof(int?), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var value = literal.Value;

            // Assert
            Assert.Null(value);
        }

        [Fact]
        public void TypeName_ReturnsNullValueMessage()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var typeName = literal.GetType().GetProperty("TypeName", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(literal) as string;

            // Assert
            Assert.Equal(Messages.NullValue, typeName);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.Add(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Add(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.Add(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithChar_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(char?));

            // Act
            var result = literal.Add('A');

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.Add((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.Add(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.Add(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithFloat_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = literal.Add(42.5f);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(double?));

            // Act
            var result = literal.Add(42.5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = literal.Add(42.5m);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithBool_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = literal.Add(true);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithString_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(string));

            // Act
            var result = literal.Add("test");

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.Subtract(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Subtract(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.Subtract(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.Subtract((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.Subtract(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.Subtract(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = literal.Subtract(42.5f);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(double?));

            // Act
            var result = literal.Subtract(42.5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = literal.Subtract(42.5m);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.Multiply(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Multiply(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.Multiply(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.Multiply((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.Multiply(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.Multiply(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = literal.Multiply(42.5f);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(double?));

            // Act
            var result = literal.Multiply(42.5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = literal.Multiply(42.5m);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.Divide(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Divide(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.Divide(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.Divide((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.Divide(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.Divide(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithFloat_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = literal.Divide(42.5f);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithDouble_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(double?));

            // Act
            var result = literal.Divide(42.5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = literal.Divide(42.5m);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.Modulus(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.Modulus(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.Modulus(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.Modulus((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.Modulus(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.Modulus(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = literal.Modulus(42.5f);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(double?));

            // Act
            var result = literal.Modulus(42.5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = literal.Modulus(42.5m);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.BitAnd(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.BitAnd(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.BitAnd(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.BitAnd((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.BitAnd(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.BitAnd(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithBoolTrue_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = literal.BitAnd(true);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithBoolFalse_ReturnsFalse()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = literal.BitAnd(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));
            var intLiteral = new IntArithmeticLiteral(42);

            // Act
            var result = nullLiteral.BitOr(intLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = literal.BitOr(42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithLong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = literal.BitOr(42L);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ushort?));

            // Act
            var result = literal.BitOr((ushort)42);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(uint?));

            // Act
            var result = literal.BitOr(42U);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithULong_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(ulong?));

            // Act
            var result = literal.BitOr(42UL);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithBoolFalse_ReturnsNull()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = literal.BitOr(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithBoolTrue_ReturnsTrue()
        {
            // Arrange
            var literal = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = literal.BitOr(true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(true, result);
        }
        #endregion

        #region Integration Tests with Other Literals
        [Fact]
        public void IntArithmeticLiteral_Add_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var intLiteral = new IntArithmeticLiteral(42);
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = intLiteral.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void LongArithmeticLiteral_Subtract_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(42L);
            var nullLiteral = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = longLiteral.Subtract(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FloatArithmeticLiteral_Multiply_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var floatLiteral = new FloatArithmeticLiteral(42.5f);
            var nullLiteral = new NullArithmeticLiteral(typeof(float?));

            // Act
            var result = floatLiteral.Multiply(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DecimalArithmeticLiteral_Divide_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var decimalLiteral = new DecimalArithmeticLiteral(42.5m);
            var nullLiteral = new NullArithmeticLiteral(typeof(decimal?));

            // Act
            var result = decimalLiteral.Divide(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BooleanArithmeticLiteral_BitAnd_WithNullArithmeticLiteral_WhenFalse_ReturnsFalse()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(false);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitAnd(nullLiteral);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }

        [Fact]
        public void BooleanArithmeticLiteral_BitAnd_WithNullArithmeticLiteral_WhenTrue_ReturnsNull()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitAnd(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BooleanArithmeticLiteral_BitOr_WithNullArithmeticLiteral_WhenTrue_ReturnsTrue()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitOr(nullLiteral);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(true, result);
        }

        [Fact]
        public void BooleanArithmeticLiteral_BitOr_WithNullArithmeticLiteral_WhenFalse_ReturnsNull()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(false);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitOr(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void StringArithmeticLiteral_Add_WithNullArithmeticLiteral_ReturnsOriginalString()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("test");
            var nullLiteral = new NullArithmeticLiteral(typeof(string));

            // Act
            var result = stringLiteral.Add(nullLiteral);

            // Assert
            Assert.Equal(null, result);
        }
        #endregion
    }
}