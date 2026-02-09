namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class IntLiteralTest
    {
        #region Constructor Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            int expectedValue = 42;

            // Act
            var literal = new IntLiteral(expectedValue);

            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Constructor_SetsTypeCorrectly()
        {
            // Arrange & Act
            var literal = new IntLiteral(100);

            // Assert
            Assert.Equal(typeof(int), literal.m_type);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Constructor_HandlesVariousIntValues(int value)
        {
            // Act
            var literal = new IntLiteral(value);

            // Assert
            Assert.Equal(value, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(42, 42, true)]
        [InlineData(-10, -10, true)]
        [InlineData(100, 200, false)]
        [InlineData(-5, 5, false)]
        public void Equal_WithLiteral_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal1 = new IntLiteral(value1);
            var literal2 = new IntLiteral(value2);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, (sbyte)0, true)]
        [InlineData(42, (sbyte)42, true)]
        [InlineData(-10, (sbyte)-10, true)]
        [InlineData(100, (sbyte)50, false)]
        public void Equal_WithSByte_ReturnsExpectedResult(int intValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, (byte)0, true)]
        [InlineData(42, (byte)42, true)]
        [InlineData(100, (byte)50, false)]
        public void Equal_WithByte_ReturnsExpectedResult(int intValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 'A', true)]
        [InlineData(97, 'a', true)]
        [InlineData(0, '\0', true)]
        [InlineData(65, 'B', false)]
        public void Equal_WithChar_ReturnsExpectedResult(int intValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, (short)0, true)]
        [InlineData(42, (short)42, true)]
        [InlineData(-10, (short)-10, true)]
        [InlineData(100, (short)50, false)]
        public void Equal_WithShort_ReturnsExpectedResult(int intValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, (ushort)0, true)]
        [InlineData(42, (ushort)42, true)]
        [InlineData(100, (ushort)50, false)]
        public void Equal_WithUShort_ReturnsExpectedResult(int intValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(42, 42, true)]
        [InlineData(-10, -10, true)]
        [InlineData(100, 200, false)]
        public void Equal_WithInt_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(value1);

            // Act
            bool result = literal.Equal(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0U, true)]
        [InlineData(42, 42U, true)]
        [InlineData(100, 200U, false)]
        public void Equal_WithUInt_ReturnsExpectedResult(int intValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0L, true)]
        [InlineData(42, 42L, true)]
        [InlineData(-10, -10L, true)]
        [InlineData(100, 200L, false)]
        public void Equal_WithLong_ReturnsExpectedResult(int intValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0UL, true)]
        [InlineData(42, 42UL, true)]
        [InlineData(-1, 100UL, false)]
        [InlineData(100, 200UL, false)]
        public void Equal_WithULong_ReturnsExpectedResult(int intValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithULong_NegativeInt_ReturnsFalse()
        {
            // Arrange
            var literal = new IntLiteral(-1);

            // Act
            bool result = literal.Equal(5UL);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(0, 0.0f, true)]
        [InlineData(42, 42.0f, true)]
        [InlineData(-10, -10.0f, true)]
        [InlineData(100, 200.0f, false)]
        public void Equal_WithFloat_ReturnsExpectedResult(int intValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0.0, true)]
        [InlineData(42, 42.0, true)]
        [InlineData(-10, -10.0, true)]
        [InlineData(100, 200.0, false)]
        public void Equal_WithDouble_ReturnsExpectedResult(int intValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.Equal(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new IntLiteral(42);

            // Act
            bool resultEqual = literal.Equal(42m);
            bool resultNotEqual = literal.Equal(43m);

            // Assert
            Assert.True(resultEqual);
            Assert.False(resultNotEqual);
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 5, false)]
        [InlineData(-10, 0, true)]
        [InlineData(0, -10, false)]
        public void LessThan_WithLiteral_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal1 = new IntLiteral(value1);
            var literal2 = new IntLiteral(value2);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (sbyte)10, true)]
        [InlineData(10, (sbyte)5, false)]
        [InlineData(5, (sbyte)5, false)]
        public void LessThan_WithSByte_ReturnsExpectedResult(int intValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (byte)10, true)]
        [InlineData(10, (byte)5, false)]
        [InlineData(5, (byte)5, false)]
        public void LessThan_WithByte_ReturnsExpectedResult(int intValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 'Z', true)]  // 65 < 90
        [InlineData(90, 'A', false)] // 90 > 65
        [InlineData(65, 'A', false)] // 65 == 65
        public void LessThan_WithChar_ReturnsExpectedResult(int intValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (short)10, true)]
        [InlineData(10, (short)5, false)]
        [InlineData(5, (short)5, false)]
        public void LessThan_WithShort_ReturnsExpectedResult(int intValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (ushort)10, true)]
        [InlineData(10, (ushort)5, false)]
        [InlineData(5, (ushort)5, false)]
        public void LessThan_WithUShort_ReturnsExpectedResult(int intValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 5, false)]
        public void LessThan_WithInt_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(value1);

            // Act
            bool result = literal.LessThan(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10U, true)]
        [InlineData(10, 5U, false)]
        [InlineData(5, 5U, false)]
        public void LessThan_WithUInt_ReturnsExpectedResult(int intValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10L, true)]
        [InlineData(10, 5L, false)]
        [InlineData(5, 5L, false)]
        public void LessThan_WithLong_ReturnsExpectedResult(int intValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, 0UL, true)]
        [InlineData(5, 10UL, true)]
        [InlineData(10, 5UL, false)]
        [InlineData(5, 5UL, false)]
        public void LessThan_WithULong_ReturnsExpectedResult(int intValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0f, true)]
        [InlineData(10, 5.0f, false)]
        [InlineData(5, 5.0f, false)]
        public void LessThan_WithFloat_ReturnsExpectedResult(int intValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0, true)]
        [InlineData(10, 5.0, false)]
        [InlineData(5, 5.0, false)]
        public void LessThan_WithDouble_ReturnsExpectedResult(int intValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new IntLiteral(5);

            // Act
            bool resultLess = literal.LessThan(10m);
            bool resultGreater = literal.LessThan(3m);
            bool resultEqual = literal.LessThan(5m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.False(resultEqual);
        }
        #endregion

        #region GreaterThan Tests
        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(5, 5, false)]
        [InlineData(0, -10, true)]
        [InlineData(-10, 0, false)]
        public void GreaterThan_WithLiteral_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal1 = new IntLiteral(value1);
            var literal2 = new IntLiteral(value2);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (sbyte)5, true)]
        [InlineData(5, (sbyte)10, false)]
        [InlineData(5, (sbyte)5, false)]
        public void GreaterThan_WithSByte_ReturnsExpectedResult(int intValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (byte)5, true)]
        [InlineData(5, (byte)10, false)]
        [InlineData(5, (byte)5, false)]
        public void GreaterThan_WithByte_ReturnsExpectedResult(int intValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(90, 'A', true)]  // 90 > 65
        [InlineData(65, 'Z', false)] // 65 < 90
        [InlineData(65, 'A', false)] // 65 == 65
        public void GreaterThan_WithChar_ReturnsExpectedResult(int intValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (short)5, true)]
        [InlineData(5, (short)10, false)]
        [InlineData(5, (short)5, false)]
        public void GreaterThan_WithShort_ReturnsExpectedResult(int intValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (ushort)5, true)]
        [InlineData(5, (ushort)10, false)]
        [InlineData(5, (ushort)5, false)]
        public void GreaterThan_WithUShort_ReturnsExpectedResult(int intValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(5, 5, false)]
        public void GreaterThan_WithInt_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(value1);

            // Act
            bool result = literal.GreaterThan(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5U, true)]
        [InlineData(5, 10U, false)]
        [InlineData(5, 5U, false)]
        public void GreaterThan_WithUInt_ReturnsExpectedResult(int intValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5L, true)]
        [InlineData(5, 10L, false)]
        [InlineData(5, 5L, false)]
        public void GreaterThan_WithLong_ReturnsExpectedResult(int intValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5UL, true)]
        [InlineData(5, 10UL, false)]
        [InlineData(5, 5UL, false)]
        [InlineData(-1, 5UL, false)]
        public void GreaterThan_WithULong_ReturnsExpectedResult(int intValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0f, true)]
        [InlineData(5, 10.0f, false)]
        [InlineData(5, 5.0f, false)]
        public void GreaterThan_WithFloat_ReturnsExpectedResult(int intValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0, true)]
        [InlineData(5, 10.0, false)]
        [InlineData(5, 5.0, false)]
        public void GreaterThan_WithDouble_ReturnsExpectedResult(int intValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new IntLiteral(10);

            // Act
            bool resultGreater = literal.GreaterThan(5m);
            bool resultLess = literal.GreaterThan(15m);
            bool resultEqual = literal.GreaterThan(10m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.False(resultEqual);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 5, true)]
        [InlineData(-10, 0, true)]
        [InlineData(0, -10, false)]
        public void LessThanOrEqual_WithLiteral_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal1 = new IntLiteral(value1);
            var literal2 = new IntLiteral(value2);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (sbyte)10, true)]
        [InlineData(10, (sbyte)5, false)]
        [InlineData(5, (sbyte)5, true)]
        public void LessThanOrEqual_WithSByte_ReturnsExpectedResult(int intValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (byte)10, true)]
        [InlineData(10, (byte)5, false)]
        [InlineData(5, (byte)5, true)]
        public void LessThanOrEqual_WithByte_ReturnsExpectedResult(int intValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 'Z', true)]  // 65 <= 90
        [InlineData(90, 'A', false)] // 90 > 65
        [InlineData(65, 'A', true)]  // 65 == 65
        public void LessThanOrEqual_WithChar_ReturnsExpectedResult(int intValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (short)10, true)]
        [InlineData(10, (short)5, false)]
        [InlineData(5, (short)5, true)]
        public void LessThanOrEqual_WithShort_ReturnsExpectedResult(int intValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (ushort)10, true)]
        [InlineData(10, (ushort)5, false)]
        [InlineData(5, (ushort)5, true)]
        public void LessThanOrEqual_WithUShort_ReturnsExpectedResult(int intValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 5, true)]
        public void LessThanOrEqual_WithInt_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(value1);

            // Act
            bool result = literal.LessThanOrEqual(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10U, true)]
        [InlineData(10, 5U, false)]
        [InlineData(5, 5U, true)]
        public void LessThanOrEqual_WithUInt_ReturnsExpectedResult(int intValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10L, true)]
        [InlineData(10, 5L, false)]
        [InlineData(5, 5L, true)]
        public void LessThanOrEqual_WithLong_ReturnsExpectedResult(int intValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, 0UL, true)]
        [InlineData(5, 10UL, true)]
        [InlineData(10, 5UL, false)]
        [InlineData(5, 5UL, true)]
        public void LessThanOrEqual_WithULong_ReturnsExpectedResult(int intValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0f, true)]
        [InlineData(10, 5.0f, false)]
        [InlineData(5, 5.0f, true)]
        public void LessThanOrEqual_WithFloat_ReturnsExpectedResult(int intValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0, true)]
        [InlineData(10, 5.0, false)]
        [InlineData(5, 5.0, true)]
        public void LessThanOrEqual_WithDouble_ReturnsExpectedResult(int intValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.LessThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new IntLiteral(5);

            // Act
            bool resultLess = literal.LessThanOrEqual(10m);
            bool resultGreater = literal.LessThanOrEqual(3m);
            bool resultEqual = literal.LessThanOrEqual(5m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.True(resultEqual);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(5, 5, true)]
        [InlineData(0, -10, true)]
        [InlineData(-10, 0, false)]
        public void GreaterThanOrEqual_WithLiteral_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal1 = new IntLiteral(value1);
            var literal2 = new IntLiteral(value2);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (sbyte)5, true)]
        [InlineData(5, (sbyte)10, false)]
        [InlineData(5, (sbyte)5, true)]
        public void GreaterThanOrEqual_WithSByte_ReturnsExpectedResult(int intValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (byte)5, true)]
        [InlineData(5, (byte)10, false)]
        [InlineData(5, (byte)5, true)]
        public void GreaterThanOrEqual_WithByte_ReturnsExpectedResult(int intValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(90, 'A', true)]  // 90 >= 65
        [InlineData(65, 'Z', false)] // 65 < 90
        [InlineData(65, 'A', true)]  // 65 == 65
        public void GreaterThanOrEqual_WithChar_ReturnsExpectedResult(int intValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (short)5, true)]
        [InlineData(5, (short)10, false)]
        [InlineData(5, (short)5, true)]
        public void GreaterThanOrEqual_WithShort_ReturnsExpectedResult(int intValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (ushort)5, true)]
        [InlineData(5, (ushort)10, false)]
        [InlineData(5, (ushort)5, true)]
        public void GreaterThanOrEqual_WithUShort_ReturnsExpectedResult(int intValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(5, 5, true)]
        public void GreaterThanOrEqual_WithInt_ReturnsExpectedResult(int value1, int value2, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(value1);

            // Act
            bool result = literal.GreaterThanOrEqual(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5U, true)]
        [InlineData(5, 10U, false)]
        [InlineData(5, 5U, true)]
        public void GreaterThanOrEqual_WithUInt_ReturnsExpectedResult(int intValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5L, true)]
        [InlineData(5, 10L, false)]
        [InlineData(5, 5L, true)]
        public void GreaterThanOrEqual_WithLong_ReturnsExpectedResult(int intValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5UL, true)]
        [InlineData(5, 10UL, false)]
        [InlineData(5, 5UL, true)]
        [InlineData(-1, 5UL, false)]
        public void GreaterThanOrEqual_WithULong_ReturnsExpectedResult(int intValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0f, true)]
        [InlineData(5, 10.0f, false)]
        [InlineData(5, 5.0f, true)]
        public void GreaterThanOrEqual_WithFloat_ReturnsExpectedResult(int intValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0, true)]
        [InlineData(5, 10.0, false)]
        [InlineData(5, 5.0, true)]
        public void GreaterThanOrEqual_WithDouble_ReturnsExpectedResult(int intValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new IntLiteral(intValue);

            // Act
            bool result = literal.GreaterThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new IntLiteral(10);

            // Act
            bool resultGreater = literal.GreaterThanOrEqual(5m);
            bool resultLess = literal.GreaterThanOrEqual(15m);
            bool resultEqual = literal.GreaterThanOrEqual(10m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.True(resultEqual);
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void EdgeCase_MaxValue_ComparisonsWork()
        {
            // Arrange
            var maxLiteral = new IntLiteral(int.MaxValue);
            var normalLiteral = new IntLiteral(100);

            // Act & Assert
            Assert.True(maxLiteral.GreaterThan(normalLiteral));
            Assert.False(maxLiteral.LessThan(normalLiteral));
            Assert.True(maxLiteral.GreaterThanOrEqual(normalLiteral));
            Assert.False(maxLiteral.LessThanOrEqual(normalLiteral));
            Assert.False(maxLiteral.Equal(normalLiteral));
        }

        [Fact]
        public void EdgeCase_MinValue_ComparisonsWork()
        {
            // Arrange
            var minLiteral = new IntLiteral(int.MinValue);
            var normalLiteral = new IntLiteral(-100);

            // Act & Assert
            Assert.False(minLiteral.GreaterThan(normalLiteral));
            Assert.True(minLiteral.LessThan(normalLiteral));
            Assert.False(minLiteral.GreaterThanOrEqual(normalLiteral));
            Assert.True(minLiteral.LessThanOrEqual(normalLiteral));
            Assert.False(minLiteral.Equal(normalLiteral));
        }

        [Fact]
        public void EdgeCase_NegativeValue_ComparesWithULong()
        {
            // Arrange
            var negativeLiteral = new IntLiteral(-5);

            // Act
            bool equalResult = negativeLiteral.Equal(10UL);
            bool lessThanResult = negativeLiteral.LessThan(10UL);
            bool greaterThanResult = negativeLiteral.GreaterThan(10UL);

            // Assert
            Assert.False(equalResult);
            Assert.True(lessThanResult);
            Assert.False(greaterThanResult);
        }

        [Fact]
        public void EdgeCase_PositiveValue_ComparesWithULong()
        {
            // Arrange
            var positiveLiteral = new IntLiteral(15);

            // Act
            bool equalResult = positiveLiteral.Equal(15UL);
            bool lessThanResult = positiveLiteral.LessThan(10UL);
            bool greaterThanResult = positiveLiteral.GreaterThan(10UL);
            bool greaterThanOrEqualResult = positiveLiteral.GreaterThanOrEqual(15UL);
            bool lessThanOrEqualResult = positiveLiteral.LessThanOrEqual(15UL);

            // Assert
            Assert.True(equalResult);
            Assert.False(lessThanResult);
            Assert.True(greaterThanResult);
            Assert.True(greaterThanOrEqualResult);
            Assert.True(lessThanOrEqualResult);
        }
        #endregion
    }
}