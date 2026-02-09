namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class NullLiteralTest
    {
        #region Constructor Tests
        [Fact]
        public void Constructor_SetsTypeCorrectly()
        {
            // Arrange & Act
            var nullLiteral = new NullLiteral(typeof(string));

            // Assert
            Assert.Equal(typeof(string), nullLiteral.m_type);
        }

        [Fact]
        public void Constructor_WithIntType_SetsTypeCorrectly()
        {
            // Arrange & Act
            var nullLiteral = new NullLiteral(typeof(int));

            // Assert
            Assert.Equal(typeof(int), nullLiteral.m_type);
        }

        [Fact]
        public void Constructor_WithNullableIntType_SetsTypeCorrectly()
        {
            // Arrange & Act
            var nullLiteral = new NullLiteral(typeof(int?));

            // Assert
            Assert.Equal(typeof(int?), nullLiteral.m_type);
        }
        #endregion

        #region Value Property Tests
        [Fact]
        public void Value_ReturnsNull()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var value = nullLiteral.Value;

            // Assert
            Assert.Null(value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithNullLiteral_ReturnsTrue()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(string));
            var nullLiteral2 = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral1.Equal(nullLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithNonNullLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));
            var intLiteral = Literal.MakeLiteral(typeof(int), 42);

            // Act
            var result = nullLiteral.Equal(intLiteral);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithStringLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));
            var stringLiteral = Literal.MakeLiteral(typeof(string), "test");

            // Act
            var result = nullLiteral.Equal(stringLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThan((byte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithSByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThan((sbyte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithChar_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(char));

            // Act
            var result = nullLiteral.LessThan('A');

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(short));

            // Act
            var result = nullLiteral.LessThan((short)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThan(10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithLong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(long));

            // Act
            var result = nullLiteral.LessThan(10L);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithUShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ushort));

            // Act
            var result = nullLiteral.LessThan((ushort)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithUInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(uint));

            // Act
            var result = nullLiteral.LessThan(10U);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithULong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ulong));

            // Act
            var result = nullLiteral.LessThan(10UL);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithFloat_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(float));

            // Act
            var result = nullLiteral.LessThan(10.5f);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithDouble_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(double));

            // Act
            var result = nullLiteral.LessThan(10.5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(decimal));

            // Act
            var result = nullLiteral.LessThan(10.5m);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithString_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.LessThan("test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithStringLiteral_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));
            var stringLiteral = Literal.MakeLiteral(typeof(string), "test");

            // Act
            var result = nullLiteral.LessThan(stringLiteral);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithIntLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));
            var intLiteral = Literal.MakeLiteral(typeof(int), 42);

            // Act
            var result = nullLiteral.LessThan(intLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThan((byte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithSByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThan((sbyte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithChar_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(char));

            // Act
            var result = nullLiteral.GreaterThan('A');

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(short));

            // Act
            var result = nullLiteral.GreaterThan((short)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThan(10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithLong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(long));

            // Act
            var result = nullLiteral.GreaterThan(10L);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithUShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ushort));

            // Act
            var result = nullLiteral.GreaterThan((ushort)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithUInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(uint));

            // Act
            var result = nullLiteral.GreaterThan(10U);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithULong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ulong));

            // Act
            var result = nullLiteral.GreaterThan(10UL);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithFloat_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(float));

            // Act
            var result = nullLiteral.GreaterThan(10.5f);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithDouble_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(double));

            // Act
            var result = nullLiteral.GreaterThan(10.5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(decimal));

            // Act
            var result = nullLiteral.GreaterThan(10.5m);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithString_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.GreaterThan("test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));
            var intLiteral = Literal.MakeLiteral(typeof(int), 42);

            // Act
            var result = nullLiteral.GreaterThan(intLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_NoParameter_WithStringType_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.LessThanOrEqual();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_NoParameter_WithIntType_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThanOrEqual();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThanOrEqual((byte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithSByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThanOrEqual((sbyte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithChar_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(char));

            // Act
            var result = nullLiteral.LessThanOrEqual('A');

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(short));

            // Act
            var result = nullLiteral.LessThanOrEqual((short)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.LessThanOrEqual(10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithLong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(long));

            // Act
            var result = nullLiteral.LessThanOrEqual(10L);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ushort));

            // Act
            var result = nullLiteral.LessThanOrEqual((ushort)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(uint));

            // Act
            var result = nullLiteral.LessThanOrEqual(10U);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithULong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ulong));

            // Act
            var result = nullLiteral.LessThanOrEqual(10UL);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithFloat_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(float));

            // Act
            var result = nullLiteral.LessThanOrEqual(10.5f);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithDouble_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(double));

            // Act
            var result = nullLiteral.LessThanOrEqual(10.5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(decimal));

            // Act
            var result = nullLiteral.LessThanOrEqual(10.5m);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithString_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.LessThanOrEqual("test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithStringLiteral_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));
            var stringLiteral = Literal.MakeLiteral(typeof(string), "test");

            // Act
            var result = nullLiteral.LessThanOrEqual(stringLiteral);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithIntLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));
            var intLiteral = Literal.MakeLiteral(typeof(int), 42);

            // Act
            var result = nullLiteral.LessThanOrEqual(intLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_NoParameter_WithStringType_ReturnsTrue()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.GreaterThanOrEqual();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_NoParameter_WithIntType_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThanOrEqual();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThanOrEqual((byte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithSByte_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThanOrEqual((sbyte)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithChar_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(char));

            // Act
            var result = nullLiteral.GreaterThanOrEqual('A');

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(short));

            // Act
            var result = nullLiteral.GreaterThanOrEqual((short)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(long));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10L);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUShort_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ushort));

            // Act
            var result = nullLiteral.GreaterThanOrEqual((ushort)10);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUInt_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(uint));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10U);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(ulong));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10UL);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithFloat_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(float));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10.5f);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDouble_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(double));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10.5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(decimal));

            // Act
            var result = nullLiteral.GreaterThanOrEqual(10.5m);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithString_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral.GreaterThanOrEqual("test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral = new NullLiteral(typeof(int));
            var intLiteral = Literal.MakeLiteral(typeof(int), 42);

            // Act
            var result = nullLiteral.GreaterThanOrEqual(intLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region Cross Comparison Tests
        [Fact]
        public void NullToNull_Equality_ReturnsTrue()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(int));
            var nullLiteral2 = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral1.Equal(nullLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void StringNullLiteral_LessThanOrEqual_StringNullLiteral_ReturnsTrue()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(string));
            var nullLiteral2 = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral1.LessThanOrEqual(nullLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void StringNullLiteral_GreaterThanOrEqual_StringNullLiteral_ReturnsTrue()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(string));
            var nullLiteral2 = new NullLiteral(typeof(string));

            // Act
            var result = nullLiteral1.GreaterThanOrEqual(nullLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IntNullLiteral_LessThanOrEqual_IntNullLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(int));
            var nullLiteral2 = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral1.LessThanOrEqual(nullLiteral2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IntNullLiteral_GreaterThanOrEqual_IntNullLiteral_ReturnsFalse()
        {
            // Arrange
            var nullLiteral1 = new NullLiteral(typeof(int));
            var nullLiteral2 = new NullLiteral(typeof(int));

            // Act
            var result = nullLiteral1.GreaterThanOrEqual(nullLiteral2);

            // Assert
            Assert.False(result);
        }
        #endregion
    }
}