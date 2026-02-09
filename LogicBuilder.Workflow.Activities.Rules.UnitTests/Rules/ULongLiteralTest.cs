namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ULongLiteralTest
    {
        #region Constructor Tests
        [Fact]
        public void Constructor_ShouldInitializeWithULongValue()
        {
            // Arrange & Act
            var literal = new ULongLiteral(42UL);

            // Assert
            Assert.Equal(42UL, literal.Value);
            Assert.Equal(typeof(ulong), literal.m_type);
        }

        [Fact]
        public void Constructor_ShouldHandleZeroValue()
        {
            // Arrange & Act
            var literal = new ULongLiteral(0UL);

            // Assert
            Assert.Equal(0UL, literal.Value);
        }

        [Fact]
        public void Constructor_ShouldHandleMaxValue()
        {
            // Arrange & Act
            var literal = new ULongLiteral(ulong.MaxValue);

            // Assert
            Assert.Equal(ulong.MaxValue, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameLiteral_ShouldReturnTrue()
        {
            // Arrange
            var literal1 = new ULongLiteral(100UL);
            var literal2 = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithDifferentLiteral_ShouldReturnFalse()
        {
            // Arrange
            var literal1 = new ULongLiteral(100UL);
            var literal2 = new ULongLiteral(200UL);

            // Act & Assert
            Assert.False(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.Equal((byte)100));
            Assert.False(literal.Equal((byte)50));
        }

        [Fact]
        public void Equal_WithSByte_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.Equal((sbyte)100));
            Assert.False(literal.Equal((sbyte)50));
        }

        [Fact]
        public void Equal_WithSByte_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.False(literal.Equal((sbyte)-10));
        }

        [Fact]
        public void Equal_WithShort_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.Equal((short)1000));
            Assert.False(literal.Equal((short)500));
        }

        [Fact]
        public void Equal_WithShort_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.False(literal.Equal((short)-1000));
        }

        [Fact]
        public void Equal_WithInt_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(50000UL);

            // Act & Assert
            Assert.True(literal.Equal(50000));
            Assert.False(literal.Equal(25000));
        }

        [Fact]
        public void Equal_WithInt_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(50000UL);

            // Act & Assert
            Assert.False(literal.Equal(-50000));
        }

        [Fact]
        public void Equal_WithLong_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000000UL);

            // Act & Assert
            Assert.True(literal.Equal(1000000L));
            Assert.False(literal.Equal(500000L));
        }

        [Fact]
        public void Equal_WithLong_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(1000000UL);

            // Act & Assert
            Assert.False(literal.Equal(-1000000L));
        }

        [Fact]
        public void Equal_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(65UL);

            // Act & Assert
            Assert.True(literal.Equal('A'));
            Assert.False(literal.Equal('B'));
        }

        [Fact]
        public void Equal_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(5000UL);

            // Act & Assert
            Assert.True(literal.Equal((ushort)5000));
            Assert.False(literal.Equal((ushort)2500));
        }

        [Fact]
        public void Equal_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100000UL);

            // Act & Assert
            Assert.True(literal.Equal(100000U));
            Assert.False(literal.Equal(50000U));
        }

        [Fact]
        public void Equal_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(10000000000UL);

            // Act & Assert
            Assert.True(literal.Equal(10000000000UL));
            Assert.False(literal.Equal(5000000000UL));
        }

        [Fact]
        public void Equal_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.Equal(1000.0f));
            Assert.False(literal.Equal(500.0f));
        }

        [Fact]
        public void Equal_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.Equal(1000.0));
            Assert.False(literal.Equal(500.0));
        }

        [Fact]
        public void Equal_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.Equal(1000m));
            Assert.False(literal.Equal(500m));
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new ULongLiteral(100UL);
            var literal2 = new ULongLiteral(200UL);
            var literal3 = new ULongLiteral(50UL);

            // Act & Assert
            Assert.True(literal1.LessThan(literal2));
            Assert.False(literal1.LessThan(literal3));
            Assert.False(literal1.LessThan(literal1));
        }

        [Fact]
        public void LessThan_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThan((byte)200));
            Assert.False(literal.LessThan((byte)50));
            Assert.False(literal.LessThan((byte)100));
        }

        [Fact]
        public void LessThan_WithInt_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThan(200));
            Assert.False(literal.LessThan(50));
        }

        [Fact]
        public void LessThan_WithInt_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.False(literal.LessThan(-200));
        }

        [Fact]
        public void LessThan_WithLong_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.LessThan(2000L));
            Assert.False(literal.LessThan(500L));
        }

        [Fact]
        public void LessThan_WithLong_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.False(literal.LessThan(-2000L));
        }

        [Fact]
        public void LessThan_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(65UL);

            // Act & Assert
            Assert.True(literal.LessThan('Z'));
            Assert.False(literal.LessThan('A'));
        }

        [Fact]
        public void LessThan_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.LessThan((ushort)2000));
            Assert.False(literal.LessThan((ushort)500));
        }

        [Fact]
        public void LessThan_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(10000UL);

            // Act & Assert
            Assert.True(literal.LessThan(20000U));
            Assert.False(literal.LessThan(5000U));
        }

        [Fact]
        public void LessThan_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100000UL);

            // Act & Assert
            Assert.True(literal.LessThan(200000UL));
            Assert.False(literal.LessThan(50000UL));
        }

        [Fact]
        public void LessThan_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThan(200.0f));
            Assert.False(literal.LessThan(50.0f));
        }

        [Fact]
        public void LessThan_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThan(200.0));
            Assert.False(literal.LessThan(50.0));
        }

        [Fact]
        public void LessThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThan(200m));
            Assert.False(literal.LessThan(50m));
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new ULongLiteral(200UL);
            var literal2 = new ULongLiteral(100UL);
            var literal3 = new ULongLiteral(300UL);

            // Act & Assert
            Assert.True(literal1.GreaterThan(literal2));
            Assert.False(literal1.GreaterThan(literal3));
            Assert.False(literal1.GreaterThan(literal1));
        }

        [Fact]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan((byte)100));
            Assert.False(literal.GreaterThan((byte)250));
            Assert.False(literal.GreaterThan((byte)200));
        }

        [Fact]
        public void GreaterThan_WithInt_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(100));
            Assert.False(literal.GreaterThan(300));
        }

        [Fact]
        public void GreaterThan_WithInt_NegativeValue_ShouldReturnTrue()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(-100));
        }

        [Fact]
        public void GreaterThan_WithLong_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(1000L));
            Assert.False(literal.GreaterThan(3000L));
        }

        [Fact]
        public void GreaterThan_WithLong_NegativeValue_ShouldReturnTrue()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(-1000L));
        }

        [Fact]
        public void GreaterThan_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(90UL);

            // Act & Assert
            Assert.True(literal.GreaterThan('A'));
            Assert.False(literal.GreaterThan('Z'));
        }

        [Fact]
        public void GreaterThan_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThan((ushort)1000));
            Assert.False(literal.GreaterThan((ushort)3000));
        }

        [Fact]
        public void GreaterThan_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(20000UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(10000U));
            Assert.False(literal.GreaterThan(30000U));
        }

        [Fact]
        public void GreaterThan_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200000UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(100000UL));
            Assert.False(literal.GreaterThan(300000UL));
        }

        [Fact]
        public void GreaterThan_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(100.0f));
            Assert.False(literal.GreaterThan(300.0f));
        }

        [Fact]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(100.0));
            Assert.False(literal.GreaterThan(300.0));
        }

        [Fact]
        public void GreaterThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThan(100m));
            Assert.False(literal.GreaterThan(300m));
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new ULongLiteral(100UL);
            var literal2 = new ULongLiteral(200UL);
            var literal3 = new ULongLiteral(50UL);

            // Act & Assert
            Assert.True(literal1.LessThanOrEqual(literal2));
            Assert.True(literal1.LessThanOrEqual(literal1));
            Assert.False(literal1.LessThanOrEqual(literal3));
        }

        [Fact]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((byte)200));
            Assert.True(literal.LessThanOrEqual((byte)100));
            Assert.False(literal.LessThanOrEqual((byte)50));
        }

        [Fact]
        public void LessThanOrEqual_WithInt_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200));
            Assert.True(literal.LessThanOrEqual(100));
            Assert.False(literal.LessThanOrEqual(50));
        }

        [Fact]
        public void LessThanOrEqual_WithInt_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.False(literal.LessThanOrEqual(-200));
        }

        [Fact]
        public void LessThanOrEqual_WithLong_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(2000L));
            Assert.True(literal.LessThanOrEqual(1000L));
            Assert.False(literal.LessThanOrEqual(500L));
        }

        [Fact]
        public void LessThanOrEqual_WithLong_NegativeValue_ShouldReturnFalse()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.False(literal.LessThanOrEqual(-2000L));
        }

        [Fact]
        public void LessThanOrEqual_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(65UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual('Z'));
            Assert.True(literal.LessThanOrEqual('A'));
            Assert.False(literal.LessThanOrEqual('@'));
        }

        [Fact]
        public void LessThanOrEqual_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(1000UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((ushort)2000));
            Assert.True(literal.LessThanOrEqual((ushort)1000));
            Assert.False(literal.LessThanOrEqual((ushort)500));
        }

        [Fact]
        public void LessThanOrEqual_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(10000UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(20000U));
            Assert.True(literal.LessThanOrEqual(10000U));
            Assert.False(literal.LessThanOrEqual(5000U));
        }

        [Fact]
        public void LessThanOrEqual_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100000UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200000UL));
            Assert.True(literal.LessThanOrEqual(100000UL));
            Assert.False(literal.LessThanOrEqual(50000UL));
        }

        [Fact]
        public void LessThanOrEqual_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200.0f));
            Assert.True(literal.LessThanOrEqual(100.0f));
            Assert.False(literal.LessThanOrEqual(50.0f));
        }

        [Fact]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200.0));
            Assert.True(literal.LessThanOrEqual(100.0));
            Assert.False(literal.LessThanOrEqual(50.0));
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(200m));
            Assert.True(literal.LessThanOrEqual(100m));
            Assert.False(literal.LessThanOrEqual(50m));
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new ULongLiteral(200UL);
            var literal2 = new ULongLiteral(100UL);
            var literal3 = new ULongLiteral(300UL);

            // Act & Assert
            Assert.True(literal1.GreaterThanOrEqual(literal2));
            Assert.True(literal1.GreaterThanOrEqual(literal1));
            Assert.False(literal1.GreaterThanOrEqual(literal3));
        }

        [Fact]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((byte)100));
            Assert.True(literal.GreaterThanOrEqual((byte)200));
            Assert.False(literal.GreaterThanOrEqual((byte)250));
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100));
            Assert.True(literal.GreaterThanOrEqual(200));
            Assert.False(literal.GreaterThanOrEqual(300));
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_NegativeValue_ShouldReturnTrue()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(-100));
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_PositiveValue_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(1000L));
            Assert.True(literal.GreaterThanOrEqual(2000L));
            Assert.False(literal.GreaterThanOrEqual(3000L));
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_NegativeValue_ShouldReturnTrue()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(-1000L));
        }

        [Fact]
        public void GreaterThanOrEqual_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(65UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual('@'));
            Assert.True(literal.GreaterThanOrEqual('A'));
            Assert.False(literal.GreaterThanOrEqual('Z'));
        }

        [Fact]
        public void GreaterThanOrEqual_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(2000UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((ushort)1000));
            Assert.True(literal.GreaterThanOrEqual((ushort)2000));
            Assert.False(literal.GreaterThanOrEqual((ushort)3000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(20000UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(10000U));
            Assert.True(literal.GreaterThanOrEqual(20000U));
            Assert.False(literal.GreaterThanOrEqual(30000U));
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200000UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100000UL));
            Assert.True(literal.GreaterThanOrEqual(200000UL));
            Assert.False(literal.GreaterThanOrEqual(300000UL));
        }

        [Fact]
        public void GreaterThanOrEqual_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100.0f));
            Assert.True(literal.GreaterThanOrEqual(200.0f));
            Assert.False(literal.GreaterThanOrEqual(300.0f));
        }

        [Fact]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100.0));
            Assert.True(literal.GreaterThanOrEqual(200.0));
            Assert.False(literal.GreaterThanOrEqual(300.0));
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new ULongLiteral(200UL);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(100m));
            Assert.True(literal.GreaterThanOrEqual(200m));
            Assert.False(literal.GreaterThanOrEqual(300m));
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void Comparison_WithMinValue_ShouldWorkCorrectly()
        {
            // Arrange
            var minLiteral = new ULongLiteral(ulong.MinValue);
            var normalLiteral = new ULongLiteral(100UL);

            // Act & Assert
            Assert.True(minLiteral.LessThan(normalLiteral));
            Assert.False(minLiteral.GreaterThan(normalLiteral));
            Assert.True(minLiteral.LessThanOrEqual(normalLiteral));
            Assert.False(minLiteral.GreaterThanOrEqual(normalLiteral));
        }

        [Fact]
        public void Comparison_WithMaxValue_ShouldWorkCorrectly()
        {
            // Arrange
            var maxLiteral = new ULongLiteral(ulong.MaxValue);
            var normalLiteral = new ULongLiteral(100UL);

            // Act & Assert
            Assert.False(maxLiteral.LessThan(normalLiteral));
            Assert.True(maxLiteral.GreaterThan(normalLiteral));
            Assert.False(maxLiteral.LessThanOrEqual(normalLiteral));
            Assert.True(maxLiteral.GreaterThanOrEqual(normalLiteral));
        }

        [Fact]
        public void Equal_WithMaxULongValue_ShouldWorkCorrectly()
        {
            // Arrange
            var literal = new ULongLiteral(ulong.MaxValue);

            // Act & Assert
            Assert.True(literal.Equal(ulong.MaxValue));
            Assert.False(literal.Equal(ulong.MaxValue - 1));
        }
        #endregion
    }
}