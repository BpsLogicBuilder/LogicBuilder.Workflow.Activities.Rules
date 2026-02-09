namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LongLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongLiteral(123456789L);

            // Assert
            Assert.Equal(123456789L, literal.Value);
            Assert.Equal(typeof(long), literal.m_type);
        }

        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongLiteral(-987654321L);

            // Assert
            Assert.Equal(-987654321L, literal.Value);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongLiteral(0L);

            // Assert
            Assert.Equal(0L, literal.Value);
        }

        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongLiteral(long.MaxValue);

            // Assert
            Assert.Equal(long.MaxValue, literal.Value);
        }

        [Fact]
        public void Constructor_WithMinValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongLiteral(long.MinValue);

            // Assert
            Assert.Equal(long.MinValue, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithDifferentLongLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(200L);

            // Act & Assert
            Assert.False(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50L);

            // Act & Assert
            Assert.True(literal.Equal((sbyte)50));
        }

        [Fact]
        public void Equal_WithByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.Equal((byte)100));
        }

        [Fact]
        public void Equal_WithChar_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(65L);

            // Act & Assert
            Assert.True(literal.Equal('A'));
        }

        [Fact]
        public void Equal_WithShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.Equal((short)1000));
        }

        [Fact]
        public void Equal_WithUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.Equal((ushort)2000));
        }

        [Fact]
        public void Equal_WithInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50000L);

            // Act & Assert
            Assert.True(literal.Equal(50000));
        }

        [Fact]
        public void Equal_WithUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100000L);

            // Act & Assert
            Assert.True(literal.Equal(100000u));
        }

        [Fact]
        public void Equal_WithLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(9876543210L);

            // Act & Assert
            Assert.True(literal.Equal(9876543210L));
        }

        [Fact]
        public void Equal_WithULong_WhenPositive_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(12345L);

            // Act & Assert
            Assert.True(literal.Equal(12345UL));
        }

        [Fact]
        public void Equal_WithULong_WhenNegative_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(-100L);

            // Act & Assert
            Assert.False(literal.Equal(100UL));
        }

        [Fact]
        public void Equal_WithFloat_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.Equal(1000.0f));
        }

        [Fact]
        public void Equal_WithDouble_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.Equal(2000.0));
        }

        [Fact]
        public void Equal_WithDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(3000L);

            // Act & Assert
            Assert.True(literal.Equal(3000m));
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithGreaterLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(200L);

            // Act & Assert
            Assert.True(literal1.LessThan(literal2));
        }

        [Fact]
        public void LessThan_WithSmallerLongLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new LongLiteral(200L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.False(literal1.LessThan(literal2));
        }

        [Fact]
        public void LessThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(10L);

            // Act & Assert
            Assert.True(literal.LessThan((sbyte)20));
        }

        [Fact]
        public void LessThan_WithByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50L);

            // Act & Assert
            Assert.True(literal.LessThan((byte)100));
        }

        [Fact]
        public void LessThan_WithChar_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(60L);

            // Act & Assert
            Assert.True(literal.LessThan('Z')); // Z = 90
        }

        [Fact]
        public void LessThan_WithShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(500L);

            // Act & Assert
            Assert.True(literal.LessThan((short)1000));
        }

        [Fact]
        public void LessThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.LessThan((ushort)2000));
        }

        [Fact]
        public void LessThan_WithInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(10000L);

            // Act & Assert
            Assert.True(literal.LessThan(20000));
        }

        [Fact]
        public void LessThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50000L);

            // Act & Assert
            Assert.True(literal.LessThan(100000u));
        }

        [Fact]
        public void LessThan_WithLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000000L);

            // Act & Assert
            Assert.True(literal.LessThan(2000000L));
        }

        [Fact]
        public void LessThan_WithULong_WhenNegative_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(-100L);

            // Act & Assert
            Assert.True(literal.LessThan(100UL));
        }

        [Fact]
        public void LessThan_WithULong_WhenPositiveAndLess_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.LessThan(200UL));
        }

        [Fact]
        public void LessThan_WithULong_WhenPositiveAndGreater_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.False(literal.LessThan(200UL));
        }

        [Fact]
        public void LessThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.LessThan(200.0f));
        }

        [Fact]
        public void LessThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.LessThan(200.0));
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.LessThan(200m));
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithSmallerLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(200L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal1.GreaterThan(literal2));
        }

        [Fact]
        public void GreaterThan_WithGreaterLongLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(200L);

            // Act & Assert
            Assert.False(literal1.GreaterThan(literal2));
        }

        [Fact]
        public void GreaterThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.GreaterThan((sbyte)50));
        }

        [Fact]
        public void GreaterThan_WithByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(200L);

            // Act & Assert
            Assert.True(literal.GreaterThan((byte)100));
        }

        [Fact]
        public void GreaterThan_WithChar_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.GreaterThan('A')); // A = 65
        }

        [Fact]
        public void GreaterThan_WithShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.GreaterThan((short)1000));
        }

        [Fact]
        public void GreaterThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(3000L);

            // Act & Assert
            Assert.True(literal.GreaterThan((ushort)2000));
        }

        [Fact]
        public void GreaterThan_WithInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50000L);

            // Act & Assert
            Assert.True(literal.GreaterThan(40000));
        }

        [Fact]
        public void GreaterThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(200000L);

            // Act & Assert
            Assert.True(literal.GreaterThan(100000u));
        }

        [Fact]
        public void GreaterThan_WithLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(5000000L);

            // Act & Assert
            Assert.True(literal.GreaterThan(4000000L));
        }

        [Fact]
        public void GreaterThan_WithULong_WhenPositiveAndGreater_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.True(literal.GreaterThan(200UL));
        }

        [Fact]
        public void GreaterThan_WithULong_WhenPositiveAndLess_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.False(literal.GreaterThan(200UL));
        }

        [Fact]
        public void GreaterThan_WithULong_WhenNegative_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(-100L);

            // Act & Assert
            Assert.False(literal.GreaterThan(100UL));
        }

        [Fact]
        public void GreaterThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.True(literal.GreaterThan(200.0f));
        }

        [Fact]
        public void GreaterThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.True(literal.GreaterThan(200.0));
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.True(literal.GreaterThan(200m));
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_WithEqualLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal1.LessThanOrEqual(literal2));
        }

        [Fact]
        public void LessThanOrEqual_WithGreaterLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(200L);

            // Act & Assert
            Assert.True(literal1.LessThanOrEqual(literal2));
        }

        [Fact]
        public void LessThanOrEqual_WithSmallerLongLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new LongLiteral(200L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.False(literal1.LessThanOrEqual(literal2));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((sbyte)50));
        }

        [Fact]
        public void LessThanOrEqual_WithGreaterByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((byte)100));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((short)1000));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualChar_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(65L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual('A'));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((ushort)2000));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(50000));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100000u));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(1000000L));
        }

        [Fact]
        public void LessThanOrEqual_WithULong_WhenNegative_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(-100L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100UL));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualFloat_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(1000.0f));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualDouble_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(2000.0));
        }

        [Fact]
        public void LessThanOrEqual_WithEqualDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(3000L);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(3000m));
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_WithEqualLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal1.GreaterThanOrEqual(literal2));
        }

        [Fact]
        public void GreaterThanOrEqual_WithSmallerLongLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new LongLiteral(200L);
            var literal2 = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal1.GreaterThanOrEqual(literal2));
        }

        [Fact]
        public void GreaterThanOrEqual_WithGreaterLongLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new LongLiteral(100L);
            var literal2 = new LongLiteral(200L);

            // Act & Assert
            Assert.False(literal1.GreaterThanOrEqual(literal2));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((sbyte)50));
        }

        [Fact]
        public void GreaterThanOrEqual_WithSmallerByte_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((byte)50));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((short)1000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualChar_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(65L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual('A'));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((ushort)2000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(50000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100000u));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(1000000L));
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_WhenPositiveAndGreater_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(300L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(200UL));
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_WhenNegative_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(-100L);

            // Act & Assert
            Assert.False(literal.GreaterThanOrEqual(100UL));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualFloat_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(1000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(1000.0f));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualDouble_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(2000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(2000.0));
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(3000L);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(3000m));
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void LessThan_WithMaxValue_ReturnsFalse()
        {
            // Arrange
            var literal = new LongLiteral(long.MaxValue);

            // Act & Assert
            Assert.False(literal.LessThan(long.MaxValue - 1));
        }

        [Fact]
        public void GreaterThan_WithMinValue_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(long.MinValue + 1);

            // Act & Assert
            Assert.True(literal.GreaterThan(long.MinValue));
        }

        [Fact]
        public void Equal_WithNegativeLong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(-1234567890L);

            // Act & Assert
            Assert.True(literal.Equal(-1234567890L));
        }

        [Fact]
        public void LessThan_NegativeWithPositiveULong_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(-1000L);

            // Act & Assert
            Assert.True(literal.LessThan(1000UL));
        }

        [Fact]
        public void GreaterThan_PositiveWithNegative_ReturnsTrue()
        {
            // Arrange
            var literal = new LongLiteral(100L);

            // Act & Assert
            Assert.True(literal.GreaterThan(-100));
        }
        #endregion
    }
}