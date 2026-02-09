namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DecimalLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            decimal expectedValue = 123.45m;

            // Act
            var literal = new DecimalLiteral(expectedValue);

            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Constructor_SetsTypeCorrectly()
        {
            // Arrange
            decimal value = 100.5m;

            // Act
            var literal = new DecimalLiteral(value);

            // Assert
            Assert.Equal(typeof(decimal), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsBoxedDecimal()
        {
            // Arrange
            decimal expectedValue = 999.99m;
            var literal = new DecimalLiteral(expectedValue);

            // Act
            object result = literal.Value;

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(expectedValue, result);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDifferentLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(200.5m);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithSByte_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(10m);
            sbyte value = 10;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithByte_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(255m);
            byte value = 255;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithChar_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(65m);
            char value = 'A';

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithShort_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(1000m);
            short value = 1000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithUShort_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(5000m);
            ushort value = 5000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithInt_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(100000m);
            int value = 100000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithUInt_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(4000000000m);
            uint value = 4000000000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithLong_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(9000000000m);
            long value = 9000000000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithULong_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(10000000000m);
            ulong value = 10000000000;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDecimal_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(123.456m);
            decimal value = 123.456m;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithLargerLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(200.5m);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new DecimalLiteral(200.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithSByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(5m);
            sbyte value = 10;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(100m);
            byte value = 50;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithChar_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(60m);
            char value = 'A'; // 65

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(500m);
            short value = 1000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithUShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(5000m);
            ushort value = 3000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(50000m);
            int value = 100000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithUInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(4000000000m);
            uint value = 3000000000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithLong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(5000000000m);
            long value = 9000000000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithULong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(10000000000m);
            ulong value = 5000000000;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(123.456m);
            decimal value = 200.789m;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithSmallerLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(200.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithLargerLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(200.5m);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithSByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(20m);
            sbyte value = 10;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(50m);
            byte value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithChar_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(70m);
            char value = 'A'; // 65

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(2000m);
            short value = 1000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithUShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(3000m);
            ushort value = 5000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(200000m);
            int value = 100000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithUInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(2000000000m);
            uint value = 3000000000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithLong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(10000000000m);
            long value = 5000000000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithULong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(5000000000m);
            ulong value = 10000000000;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(300.789m);
            decimal value = 200.456m;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_WithLargerLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(200.5m);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new DecimalLiteral(200.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithSByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(10m);
            sbyte value = 10;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(50m);
            byte value = 100;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithChar_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(65m);
            char value = 'A'; // 65

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(1000m);
            short value = 500;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(3000m);
            ushort value = 3000;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(100000m);
            int value = 200000;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(3000000000m);
            uint value = 2000000000;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithLong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(9000000000m);
            long value = 9000000000;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithULong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(5000000000m);
            ulong value = 10000000000;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(123.456m);
            decimal value = 123.456m;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_WithSmallerLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(200.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(100.5m);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLargerLiteral_ReturnsFalse()
        {
            // Arrange
            var literal1 = new DecimalLiteral(100.5m);
            var literal2 = new DecimalLiteral(200.5m);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithSByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(10m);
            sbyte value = 10;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithByte_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(150m);
            byte value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithChar_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(65m);
            char value = 'A'; // 65

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(500m);
            short value = 1000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUShort_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(3000m);
            ushort value = 3000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(200000m);
            int value = 100000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUInt_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(2000000000m);
            uint value = 3000000000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(9000000000m);
            long value = 9000000000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(10000000000m);
            ulong value = 5000000000;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(123.456m);
            decimal value = 123.456m;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void Equal_WithZero_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(0m);
            decimal value = 0m;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithNegativeValue_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(-123.45m);
            decimal value = -123.45m;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithNegativeValues_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(-200m);
            decimal value = -100m;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithNegativeValues_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(-100m);
            decimal value = -200m;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithMaxValue_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(decimal.MaxValue);
            decimal value = decimal.MaxValue;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithMinValue_ReturnsTrue()
        {
            // Arrange
            var literal = new DecimalLiteral(decimal.MinValue);
            decimal value = decimal.MinValue;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithVerySmallDifference_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new DecimalLiteral(1.0000000000000000001m);
            decimal value = 1.0000000000000000002m;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}