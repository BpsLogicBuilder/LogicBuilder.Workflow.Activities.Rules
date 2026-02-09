using System;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ShortLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            short value = 100;

            // Act
            ShortLiteral literal = new(value);

            // Assert
            Assert.Equal(value, literal.Value);
            Assert.Equal(typeof(short), literal.m_type);
        }

        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange
            short value = -100;

            // Act
            ShortLiteral literal = new(value);

            // Assert
            Assert.Equal(value, literal.Value);
        }

        [Fact]
        public void Constructor_WithMinValue_SetsValueCorrectly()
        {
            // Arrange
            short value = short.MinValue;

            // Act
            ShortLiteral literal = new(value);

            // Assert
            Assert.Equal(value, literal.Value);
        }

        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange
            short value = short.MaxValue;

            // Act
            ShortLiteral literal = new(value);

            // Assert
            Assert.Equal(value, literal.Value);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange
            short value = 0;

            // Act
            ShortLiteral literal = new(value);

            // Assert
            Assert.Equal(value, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameLiteral_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDifferentLiteral_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(200);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            sbyte value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            byte value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithChar_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(65);
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
            ShortLiteral literal = new(100);
            short value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithUShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            ushort value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            int value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithUInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            uint value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithLong_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            long value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithULong_WhenPositive_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            ulong value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithULong_WhenNegative_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal = new(-100);
            ulong value = 100;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithFloat_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            float value = 100.0f;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDouble_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            double value = 100.0;

            // Act
            bool result = literal.Equal(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDecimal_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            decimal value = 100m;

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
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(200);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(200);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            sbyte value = 127;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            byte value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithChar_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(64);
            char value = 'A'; // 65

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            short value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            ushort value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            int value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            uint value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithLong_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            long value = 200;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            float value = 200.0f;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            double value = 200.0;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            decimal value = 200m;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithNegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            ShortLiteral literal = new(-100);
            short value = -50;

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
            ShortLiteral literal1 = new(200);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithLargerLiteral_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(200);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            sbyte value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            byte value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithChar_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            char value = 'A'; // 65

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            short value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            ushort value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            int value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            uint value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithLong_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            long value = 100;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            float value = 100.0f;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            double value = 100.0;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            decimal value = 100m;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithNegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            ShortLiteral literal = new(-50);
            short value = -100;

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
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(200);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(200);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.LessThanOrEqual(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_WithSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            sbyte value = 127;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            sbyte value = 100;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            byte value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithChar_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(65);
            char value = 'A'; // 65

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            short value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            ushort value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            int value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithUInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            uint value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithLong_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            long value = 200;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithFloat_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            float value = 200.0f;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithDouble_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            double value = 200.0;

            // Act
            bool result = literal.LessThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            decimal value = 200m;

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
            ShortLiteral literal1 = new(200);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLargerLiteral_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(100);
            ShortLiteral literal2 = new(200);

            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            sbyte value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            sbyte value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithByte_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            byte value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithChar_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(65);
            char value = 'A'; // 65

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            short value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUShort_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            ushort value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            int value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithUInt_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            uint value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            long value = 100;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithFloat_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            float value = 100.0f;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDouble_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            double value = 100.0;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(200);
            decimal value = 100m;

            // Act
            bool result = literal.GreaterThanOrEqual(value);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region Edge Cases Tests
        [Fact]
        public void Equal_WithMinValue_ReturnsCorrectResult()
        {
            // Arrange
            ShortLiteral literal1 = new(short.MinValue);
            ShortLiteral literal2 = new(short.MinValue);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithMaxValue_ReturnsCorrectResult()
        {
            // Arrange
            ShortLiteral literal1 = new(short.MaxValue);
            ShortLiteral literal2 = new(short.MaxValue);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_WithMinAndMaxValues_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal1 = new(short.MinValue);
            ShortLiteral literal2 = new(short.MaxValue);

            // Act
            bool result = literal1.LessThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_WithMaxAndMinValues_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal1 = new(short.MaxValue);
            ShortLiteral literal2 = new(short.MinValue);

            // Act
            bool result = literal1.GreaterThan(literal2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithNegativeAndPositive_ReturnsFalse()
        {
            // Arrange
            ShortLiteral literal1 = new(-100);
            ShortLiteral literal2 = new(100);

            // Act
            bool result = literal1.Equal(literal2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_NegativeComparedToZero_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(-100);
            short value = 0;

            // Act
            bool result = literal.LessThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_PositiveComparedToZero_ReturnsTrue()
        {
            // Arrange
            ShortLiteral literal = new(100);
            short value = 0;

            // Act
            bool result = literal.GreaterThan(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Value_ReturnsBoxedShort()
        {
            // Arrange
            short value = 100;
            ShortLiteral literal = new(value);

            // Act
            object boxedValue = literal.Value;

            // Assert
            Assert.IsType<short>(boxedValue);
            Assert.Equal(value, boxedValue);
        }
        #endregion
    }
}