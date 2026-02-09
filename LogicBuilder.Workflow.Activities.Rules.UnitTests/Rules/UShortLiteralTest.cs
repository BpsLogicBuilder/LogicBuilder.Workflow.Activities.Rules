namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class UShortLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            ushort expectedValue = 42;

            // Act
            UShortLiteral literal = new(expectedValue);

            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Constructor_SetsTypeCorrectly()
        {
            // Arrange & Act
            UShortLiteral literal = new(100);

            // Assert
            Assert.Equal(typeof(ushort), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsBoxedUShort()
        {
            // Arrange
            ushort expectedValue = 65535;
            UShortLiteral literal = new(expectedValue);

            // Act
            object value = literal.Value;

            // Assert
            Assert.IsType<ushort>(value);
            Assert.Equal(expectedValue, value);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData(100, 100, true)]
        [InlineData(100, 200, false)]
        [InlineData(0, 0, true)]
        [InlineData(65535, 65535, true)]
        [InlineData(65535, 65534, false)]
        public void Equal_WithUShort_ReturnsCorrectResult(ushort leftValue, ushort rightValue, bool expected)
        {
            // Arrange
            UShortLiteral left = new(leftValue);
            UShortLiteral right = new(rightValue);

            // Act
            bool result = left.Equal(right);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (sbyte)100, true)]
        [InlineData(100, (sbyte)50, false)]
        [InlineData(100, (sbyte)-1, false)]
        public void Equal_WithSByte_ReturnsCorrectResult(ushort leftValue, sbyte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (byte)100, true)]
        [InlineData(100, (byte)50, false)]
        [InlineData(255, (byte)255, true)]
        public void Equal_WithByte_ReturnsCorrectResult(ushort leftValue, byte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 'A', true)]
        [InlineData(66, 'A', false)]
        [InlineData(97, 'a', true)]
        public void Equal_WithChar_ReturnsCorrectResult(ushort leftValue, char rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (short)100, true)]
        [InlineData(100, (short)50, false)]
        [InlineData(100, (short)-1, false)]
        [InlineData(32767, (short)32767, true)]
        public void Equal_WithShort_ReturnsCorrectResult(ushort leftValue, short rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100, true)]
        [InlineData(100, 200, false)]
        [InlineData(65535, 65535, true)]
        public void Equal_WithInt_ReturnsCorrectResult(ushort leftValue, int rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100u, true)]
        [InlineData(100, 200u, false)]
        [InlineData(65535, 65535u, true)]
        public void Equal_WithUInt_ReturnsCorrectResult(ushort leftValue, uint rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100L, true)]
        [InlineData(100, 200L, false)]
        [InlineData(65535, 65535L, true)]
        public void Equal_WithLong_ReturnsCorrectResult(ushort leftValue, long rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100uL, true)]
        [InlineData(100, 200uL, false)]
        [InlineData(65535, 65535uL, true)]
        public void Equal_WithULong_ReturnsCorrectResult(ushort leftValue, ulong rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100.0f, true)]
        [InlineData(100, 100.5f, false)]
        [InlineData(65535, 65535.0f, true)]
        public void Equal_WithFloat_ReturnsCorrectResult(ushort leftValue, float rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 100.0, true)]
        [InlineData(100, 100.5, false)]
        [InlineData(65535, 65535.0, true)]
        public void Equal_WithDouble_ReturnsCorrectResult(ushort leftValue, double rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.Equal(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithDecimal_ReturnsCorrectResult()
        {
            // Arrange
            UShortLiteral literal = new(100);

            // Act & Assert
            Assert.True(literal.Equal(100m));
            Assert.False(literal.Equal(100.5m));
            Assert.False(literal.Equal(200m));
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData(100, 200, true)]
        [InlineData(200, 100, false)]
        [InlineData(100, 100, false)]
        [InlineData(0, 65535, true)]
        public void LessThan_WithUShort_ReturnsCorrectResult(ushort leftValue, ushort rightValue, bool expected)
        {
            // Arrange
            UShortLiteral left = new(leftValue);
            UShortLiteral right = new(rightValue);

            // Act
            bool result = left.LessThan(right);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (sbyte)100, true)]
        [InlineData(100, (sbyte)50, false)]
        [InlineData(100, (sbyte)100, false)]
        public void LessThan_WithSByte_ReturnsCorrectResult(ushort leftValue, sbyte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (byte)100, true)]
        [InlineData(100, (byte)50, false)]
        [InlineData(100, (byte)100, false)]
        public void LessThan_WithByte_ReturnsCorrectResult(ushort leftValue, byte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 'z', true)]
        [InlineData(200, 'A', false)]
        [InlineData(65, 'A', false)]
        public void LessThan_WithChar_ReturnsCorrectResult(ushort leftValue, char rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (short)100, true)]
        [InlineData(100, (short)50, false)]
        [InlineData(100, (short)100, false)]
        public void LessThan_WithShort_ReturnsCorrectResult(ushort leftValue, short rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100, true)]
        [InlineData(100, 50, false)]
        [InlineData(100, 100, false)]
        public void LessThan_WithInt_ReturnsCorrectResult(ushort leftValue, int rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100u, true)]
        [InlineData(100, 50u, false)]
        [InlineData(100, 100u, false)]
        public void LessThan_WithUInt_ReturnsCorrectResult(ushort leftValue, uint rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100L, true)]
        [InlineData(100, 50L, false)]
        [InlineData(100, 100L, false)]
        public void LessThan_WithLong_ReturnsCorrectResult(ushort leftValue, long rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100uL, true)]
        [InlineData(100, 50uL, false)]
        [InlineData(100, 100uL, false)]
        public void LessThan_WithULong_ReturnsCorrectResult(ushort leftValue, ulong rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100.0f, true)]
        [InlineData(100, 50.0f, false)]
        [InlineData(100, 100.0f, false)]
        public void LessThan_WithFloat_ReturnsCorrectResult(ushort leftValue, float rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100.0, true)]
        [InlineData(100, 50.0, false)]
        [InlineData(100, 100.0, false)]
        public void LessThan_WithDouble_ReturnsCorrectResult(ushort leftValue, double rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsCorrectResult()
        {
            // Arrange
            UShortLiteral literal = new(100);

            // Act & Assert
            Assert.True(literal.LessThan(200m));
            Assert.False(literal.LessThan(50m));
            Assert.False(literal.LessThan(100m));
        }
        #endregion

        #region GreaterThan Tests
        [Theory]
        [InlineData(200, 100, true)]
        [InlineData(100, 200, false)]
        [InlineData(100, 100, false)]
        [InlineData(65535, 0, true)]
        public void GreaterThan_WithUShort_ReturnsCorrectResult(ushort leftValue, ushort rightValue, bool expected)
        {
            // Arrange
            UShortLiteral left = new(leftValue);
            UShortLiteral right = new(rightValue);

            // Act
            bool result = left.GreaterThan(right);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (sbyte)50, true)]
        [InlineData(50, (sbyte)100, false)]
        [InlineData(100, (sbyte)100, false)]
        public void GreaterThan_WithSByte_ReturnsCorrectResult(ushort leftValue, sbyte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (byte)50, true)]
        [InlineData(50, (byte)100, false)]
        [InlineData(100, (byte)100, false)]
        public void GreaterThan_WithByte_ReturnsCorrectResult(ushort leftValue, byte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(200, 'A', true)]
        [InlineData(50, 'z', false)]
        [InlineData(65, 'A', false)]
        public void GreaterThan_WithChar_ReturnsCorrectResult(ushort leftValue, char rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (short)50, true)]
        [InlineData(50, (short)100, false)]
        [InlineData(100, (short)100, false)]
        public void GreaterThan_WithShort_ReturnsCorrectResult(ushort leftValue, short rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50, true)]
        [InlineData(50, 100, false)]
        [InlineData(100, 100, false)]
        public void GreaterThan_WithInt_ReturnsCorrectResult(ushort leftValue, int rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50u, true)]
        [InlineData(50, 100u, false)]
        [InlineData(100, 100u, false)]
        public void GreaterThan_WithUInt_ReturnsCorrectResult(ushort leftValue, uint rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50L, true)]
        [InlineData(50, 100L, false)]
        [InlineData(100, 100L, false)]
        public void GreaterThan_WithLong_ReturnsCorrectResult(ushort leftValue, long rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50uL, true)]
        [InlineData(50, 100uL, false)]
        [InlineData(100, 100uL, false)]
        public void GreaterThan_WithULong_ReturnsCorrectResult(ushort leftValue, ulong rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50.0f, true)]
        [InlineData(50, 100.0f, false)]
        [InlineData(100, 100.0f, false)]
        public void GreaterThan_WithFloat_ReturnsCorrectResult(ushort leftValue, float rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50.0, true)]
        [InlineData(50, 100.0, false)]
        [InlineData(100, 100.0, false)]
        public void GreaterThan_WithDouble_ReturnsCorrectResult(ushort leftValue, double rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThan(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsCorrectResult()
        {
            // Arrange
            UShortLiteral literal = new(100);

            // Act & Assert
            Assert.True(literal.GreaterThan(50m));
            Assert.False(literal.GreaterThan(200m));
            Assert.False(literal.GreaterThan(100m));
        }
        #endregion

        #region LessThanOrEqual Tests
        [Theory]
        [InlineData(100, 200, true)]
        [InlineData(200, 100, false)]
        [InlineData(100, 100, true)]
        [InlineData(0, 65535, true)]
        public void LessThanOrEqual_WithUShort_ReturnsCorrectResult(ushort leftValue, ushort rightValue, bool expected)
        {
            // Arrange
            UShortLiteral left = new(leftValue);
            UShortLiteral right = new(rightValue);

            // Act
            bool result = left.LessThanOrEqual(right);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (sbyte)100, true)]
        [InlineData(100, (sbyte)50, false)]
        [InlineData(100, (sbyte)100, true)]
        public void LessThanOrEqual_WithSByte_ReturnsCorrectResult(ushort leftValue, sbyte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (byte)100, true)]
        [InlineData(100, (byte)50, false)]
        [InlineData(100, (byte)100, true)]
        public void LessThanOrEqual_WithByte_ReturnsCorrectResult(ushort leftValue, byte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 'z', true)]
        [InlineData(200, 'A', false)]
        [InlineData(65, 'A', true)]
        public void LessThanOrEqual_WithChar_ReturnsCorrectResult(ushort leftValue, char rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, (short)100, true)]
        [InlineData(100, (short)50, false)]
        [InlineData(100, (short)100, true)]
        public void LessThanOrEqual_WithShort_ReturnsCorrectResult(ushort leftValue, short rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100, true)]
        [InlineData(100, 50, false)]
        [InlineData(100, 100, true)]
        public void LessThanOrEqual_WithInt_ReturnsCorrectResult(ushort leftValue, int rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100u, true)]
        [InlineData(100, 50u, false)]
        [InlineData(100, 100u, true)]
        public void LessThanOrEqual_WithUInt_ReturnsCorrectResult(ushort leftValue, uint rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100L, true)]
        [InlineData(100, 50L, false)]
        [InlineData(100, 100L, true)]
        public void LessThanOrEqual_WithLong_ReturnsCorrectResult(ushort leftValue, long rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100uL, true)]
        [InlineData(100, 50uL, false)]
        [InlineData(100, 100uL, true)]
        public void LessThanOrEqual_WithULong_ReturnsCorrectResult(ushort leftValue, ulong rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100.0f, true)]
        [InlineData(100, 50.0f, false)]
        [InlineData(100, 100.0f, true)]
        public void LessThanOrEqual_WithFloat_ReturnsCorrectResult(ushort leftValue, float rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50, 100.0, true)]
        [InlineData(100, 50.0, false)]
        [InlineData(100, 100.0, true)]
        public void LessThanOrEqual_WithDouble_ReturnsCorrectResult(ushort leftValue, double rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.LessThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsCorrectResult()
        {
            // Arrange
            UShortLiteral literal = new(100);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200m));
            Assert.False(literal.LessThanOrEqual(50m));
            Assert.True(literal.LessThanOrEqual(100m));
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Theory]
        [InlineData(200, 100, true)]
        [InlineData(100, 200, false)]
        [InlineData(100, 100, true)]
        [InlineData(65535, 0, true)]
        public void GreaterThanOrEqual_WithUShort_ReturnsCorrectResult(ushort leftValue, ushort rightValue, bool expected)
        {
            // Arrange
            UShortLiteral left = new(leftValue);
            UShortLiteral right = new(rightValue);

            // Act
            bool result = left.GreaterThanOrEqual(right);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (sbyte)50, true)]
        [InlineData(50, (sbyte)100, false)]
        [InlineData(100, (sbyte)100, true)]
        public void GreaterThanOrEqual_WithSByte_ReturnsCorrectResult(ushort leftValue, sbyte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (byte)50, true)]
        [InlineData(50, (byte)100, false)]
        [InlineData(100, (byte)100, true)]
        public void GreaterThanOrEqual_WithByte_ReturnsCorrectResult(ushort leftValue, byte rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(200, 'A', true)]
        [InlineData(50, 'z', false)]
        [InlineData(65, 'A', true)]
        public void GreaterThanOrEqual_WithChar_ReturnsCorrectResult(ushort leftValue, char rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, (short)50, true)]
        [InlineData(50, (short)100, false)]
        [InlineData(100, (short)100, true)]
        public void GreaterThanOrEqual_WithShort_ReturnsCorrectResult(ushort leftValue, short rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50, true)]
        [InlineData(50, 100, false)]
        [InlineData(100, 100, true)]
        public void GreaterThanOrEqual_WithInt_ReturnsCorrectResult(ushort leftValue, int rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50u, true)]
        [InlineData(50, 100u, false)]
        [InlineData(100, 100u, true)]
        public void GreaterThanOrEqual_WithUInt_ReturnsCorrectResult(ushort leftValue, uint rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50L, true)]
        [InlineData(50, 100L, false)]
        [InlineData(100, 100L, true)]
        public void GreaterThanOrEqual_WithLong_ReturnsCorrectResult(ushort leftValue, long rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50uL, true)]
        [InlineData(50, 100uL, false)]
        [InlineData(100, 100uL, true)]
        public void GreaterThanOrEqual_WithULong_ReturnsCorrectResult(ushort leftValue, ulong rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50.0f, true)]
        [InlineData(50, 100.0f, false)]
        [InlineData(100, 100.0f, true)]
        public void GreaterThanOrEqual_WithFloat_ReturnsCorrectResult(ushort leftValue, float rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100, 50.0, true)]
        [InlineData(50, 100.0, false)]
        [InlineData(100, 100.0, true)]
        public void GreaterThanOrEqual_WithDouble_ReturnsCorrectResult(ushort leftValue, double rightValue, bool expected)
        {
            // Arrange
            UShortLiteral literal = new(leftValue);

            // Act
            bool result = literal.GreaterThanOrEqual(rightValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsCorrectResult()
        {
            // Arrange
            UShortLiteral literal = new(100);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50m));
            Assert.False(literal.GreaterThanOrEqual(200m));
            Assert.True(literal.GreaterThanOrEqual(100m));
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void UShortLiteral_WithMinValue_WorksCorrectly()
        {
            // Arrange
            UShortLiteral literal = new(ushort.MinValue);

            // Act & Assert
            Assert.Equal(0, (ushort)literal.Value);
            Assert.True(literal.Equal((ushort)0));
            Assert.True(literal.LessThan((ushort)1));
            Assert.False(literal.GreaterThan((ushort)0));
        }

        [Fact]
        public void UShortLiteral_WithMaxValue_WorksCorrectly()
        {
            // Arrange
            UShortLiteral literal = new(ushort.MaxValue);

            // Act & Assert
            Assert.Equal(65535, (ushort)literal.Value);
            Assert.True(literal.Equal((ushort)65535));
            Assert.True(literal.GreaterThan((ushort)65534));
            Assert.False(literal.LessThan((ushort)65535));
        }
        #endregion
    }
}