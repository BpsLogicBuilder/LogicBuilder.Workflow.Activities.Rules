namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class UIntLiteralTest
    {
        #region Constructor Tests
        [Fact]
        public void Constructor_ShouldInitializeWithUIntValue()
        {
            // Arrange & Act
            var literal = new UIntLiteral(42U);

            // Assert
            Assert.Equal(42U, literal.Value);
            Assert.Equal(typeof(uint), literal.m_type);
        }

        [Fact]
        public void Constructor_ShouldHandleZeroValue()
        {
            // Arrange & Act
            var literal = new UIntLiteral(0U);

            // Assert
            Assert.Equal(0U, literal.Value);
        }

        [Fact]
        public void Constructor_ShouldHandleMaxValue()
        {
            // Arrange & Act
            var literal = new UIntLiteral(uint.MaxValue);

            // Assert
            Assert.Equal(uint.MaxValue, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameLiteral_ShouldReturnTrue()
        {
            // Arrange
            var literal1 = new UIntLiteral(100U);
            var literal2 = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithDifferentLiteral_ShouldReturnFalse()
        {
            // Arrange
            var literal1 = new UIntLiteral(100U);
            var literal2 = new UIntLiteral(200U);

            // Act & Assert
            Assert.False(literal1.Equal(literal2));
        }

        [Fact]
        public void Equal_WithSByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.Equal((sbyte)100));
            Assert.False(literal.Equal((sbyte)50));
        }

        [Fact]
        public void Equal_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.Equal((byte)100));
            Assert.False(literal.Equal((byte)50));
        }

        [Fact]
        public void Equal_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(65U);

            // Act & Assert
            Assert.True(literal.Equal('A'));
            Assert.False(literal.Equal('B'));
        }

        [Fact]
        public void Equal_WithShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.Equal((short)1000));
            Assert.False(literal.Equal((short)500));
        }

        [Fact]
        public void Equal_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.Equal((ushort)1000));
            Assert.False(literal.Equal((ushort)500));
        }

        [Fact]
        public void Equal_WithInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.Equal(100000));
            Assert.False(literal.Equal(50000));
        }

        [Fact]
        public void Equal_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.Equal(100000U));
            Assert.False(literal.Equal(50000U));
        }

        [Fact]
        public void Equal_WithLong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.Equal(100000L));
            Assert.False(literal.Equal(50000L));
        }

        [Fact]
        public void Equal_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.Equal(100000UL));
            Assert.False(literal.Equal(50000UL));
        }

        [Fact]
        public void Equal_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.Equal(100.0f));
            Assert.False(literal.Equal(50.0f));
        }

        [Fact]
        public void Equal_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.Equal(100.0));
            Assert.False(literal.Equal(50.0));
        }

        [Fact]
        public void Equal_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.Equal(100m));
            Assert.False(literal.Equal(50m));
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new UIntLiteral(100U);
            var literal2 = new UIntLiteral(200U);

            // Act & Assert
            Assert.True(literal1.LessThan(literal2));
            Assert.False(literal2.LessThan(literal1));
        }

        [Fact]
        public void LessThan_WithSByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThan((sbyte)100));
            Assert.False(literal.LessThan((sbyte)25));
        }

        [Fact]
        public void LessThan_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThan((byte)100));
            Assert.False(literal.LessThan((byte)25));
        }

        [Fact]
        public void LessThan_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(65U);

            // Act & Assert
            Assert.True(literal.LessThan('Z'));
            Assert.False(literal.LessThan('A'));
        }

        [Fact]
        public void LessThan_WithShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(500U);

            // Act & Assert
            Assert.True(literal.LessThan((short)1000));
            Assert.False(literal.LessThan((short)250));
        }

        [Fact]
        public void LessThan_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(500U);

            // Act & Assert
            Assert.True(literal.LessThan((ushort)1000));
            Assert.False(literal.LessThan((ushort)250));
        }

        [Fact]
        public void LessThan_WithInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThan(100000));
            Assert.False(literal.LessThan(25000));
        }

        [Fact]
        public void LessThan_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThan(100000U));
            Assert.False(literal.LessThan(25000U));
        }

        [Fact]
        public void LessThan_WithLong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThan(100000L));
            Assert.False(literal.LessThan(25000L));
        }

        [Fact]
        public void LessThan_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThan(100000UL));
            Assert.False(literal.LessThan(25000UL));
        }

        [Fact]
        public void LessThan_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThan(100.0f));
            Assert.False(literal.LessThan(25.0f));
        }

        [Fact]
        public void LessThan_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThan(100.0));
            Assert.False(literal.LessThan(25.0));
        }

        [Fact]
        public void LessThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThan(100m));
            Assert.False(literal.LessThan(25m));
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new UIntLiteral(200U);
            var literal2 = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal1.GreaterThan(literal2));
            Assert.False(literal2.GreaterThan(literal1));
        }

        [Fact]
        public void GreaterThan_WithSByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThan((sbyte)50));
            Assert.False(literal.GreaterThan((sbyte)127));
        }

        [Fact]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThan((byte)50));
            Assert.False(literal.GreaterThan((byte)200));
        }

        [Fact]
        public void GreaterThan_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(90U);

            // Act & Assert
            Assert.True(literal.GreaterThan('A'));
            Assert.False(literal.GreaterThan('Z'));
        }

        [Fact]
        public void GreaterThan_WithShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.GreaterThan((short)500));
            Assert.False(literal.GreaterThan((short)2000));
        }

        [Fact]
        public void GreaterThan_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.GreaterThan((ushort)500));
            Assert.False(literal.GreaterThan((ushort)2000));
        }

        [Fact]
        public void GreaterThan_WithInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50000));
            Assert.False(literal.GreaterThan(200000));
        }

        [Fact]
        public void GreaterThan_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50000U));
            Assert.False(literal.GreaterThan(200000U));
        }

        [Fact]
        public void GreaterThan_WithLong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50000L));
            Assert.False(literal.GreaterThan(200000L));
        }

        [Fact]
        public void GreaterThan_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50000UL));
            Assert.False(literal.GreaterThan(200000UL));
        }

        [Fact]
        public void GreaterThan_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50.0f));
            Assert.False(literal.GreaterThan(200.0f));
        }

        [Fact]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50.0));
            Assert.False(literal.GreaterThan(200.0));
        }

        [Fact]
        public void GreaterThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThan(50m));
            Assert.False(literal.GreaterThan(200m));
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new UIntLiteral(100U);
            var literal2 = new UIntLiteral(200U);
            var literal3 = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal1.LessThanOrEqual(literal2));
            Assert.True(literal1.LessThanOrEqual(literal3));
            Assert.False(literal2.LessThanOrEqual(literal1));
        }

        [Fact]
        public void LessThanOrEqual_WithSByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((sbyte)100));
            Assert.True(literal.LessThanOrEqual((sbyte)50));
            Assert.False(literal.LessThanOrEqual((sbyte)25));
        }

        [Fact]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((byte)100));
            Assert.True(literal.LessThanOrEqual((byte)50));
            Assert.False(literal.LessThanOrEqual((byte)25));
        }

        [Fact]
        public void LessThanOrEqual_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(65U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual('Z'));
            Assert.True(literal.LessThanOrEqual('A'));
            Assert.False(literal.LessThanOrEqual('@'));
        }

        [Fact]
        public void LessThanOrEqual_WithShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(500U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((short)1000));
            Assert.True(literal.LessThanOrEqual((short)500));
            Assert.False(literal.LessThanOrEqual((short)250));
        }

        [Fact]
        public void LessThanOrEqual_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(500U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual((ushort)1000));
            Assert.True(literal.LessThanOrEqual((ushort)500));
            Assert.False(literal.LessThanOrEqual((ushort)250));
        }

        [Fact]
        public void LessThanOrEqual_WithInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100000));
            Assert.True(literal.LessThanOrEqual(50000));
            Assert.False(literal.LessThanOrEqual(25000));
        }

        [Fact]
        public void LessThanOrEqual_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100000U));
            Assert.True(literal.LessThanOrEqual(50000U));
            Assert.False(literal.LessThanOrEqual(25000U));
        }

        [Fact]
        public void LessThanOrEqual_WithLong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100000L));
            Assert.True(literal.LessThanOrEqual(50000L));
            Assert.False(literal.LessThanOrEqual(25000L));
        }

        [Fact]
        public void LessThanOrEqual_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50000U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100000UL));
            Assert.True(literal.LessThanOrEqual(50000UL));
            Assert.False(literal.LessThanOrEqual(25000UL));
        }

        [Fact]
        public void LessThanOrEqual_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100.0f));
            Assert.True(literal.LessThanOrEqual(50.0f));
            Assert.False(literal.LessThanOrEqual(25.0f));
        }

        [Fact]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100.0));
            Assert.True(literal.LessThanOrEqual(50.0));
            Assert.False(literal.LessThanOrEqual(25.0));
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(50U);

            // Act & Assert
            Assert.True(literal.LessThanOrEqual(100m));
            Assert.True(literal.LessThanOrEqual(50m));
            Assert.False(literal.LessThanOrEqual(25m));
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_WithLiteral_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal1 = new UIntLiteral(200U);
            var literal2 = new UIntLiteral(100U);
            var literal3 = new UIntLiteral(200U);

            // Act & Assert
            Assert.True(literal1.GreaterThanOrEqual(literal2));
            Assert.True(literal1.GreaterThanOrEqual(literal3));
            Assert.False(literal2.GreaterThanOrEqual(literal1));
        }

        [Fact]
        public void GreaterThanOrEqual_WithSByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((sbyte)50));
            Assert.True(literal.GreaterThanOrEqual((sbyte)100));
            Assert.False(literal.GreaterThanOrEqual((sbyte)127));
        }

        [Fact]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((byte)50));
            Assert.True(literal.GreaterThanOrEqual((byte)100));
            Assert.False(literal.GreaterThanOrEqual((byte)200));
        }

        [Fact]
        public void GreaterThanOrEqual_WithChar_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(90U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual('A'));
            Assert.True(literal.GreaterThanOrEqual('Z'));
            Assert.False(literal.GreaterThanOrEqual((char)100));
        }

        [Fact]
        public void GreaterThanOrEqual_WithShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((short)500));
            Assert.True(literal.GreaterThanOrEqual((short)1000));
            Assert.False(literal.GreaterThanOrEqual((short)2000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithUShort_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(1000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual((ushort)500));
            Assert.True(literal.GreaterThanOrEqual((ushort)1000));
            Assert.False(literal.GreaterThanOrEqual((ushort)2000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50000));
            Assert.True(literal.GreaterThanOrEqual(100000));
            Assert.False(literal.GreaterThanOrEqual(200000));
        }

        [Fact]
        public void GreaterThanOrEqual_WithUInt_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50000U));
            Assert.True(literal.GreaterThanOrEqual(100000U));
            Assert.False(literal.GreaterThanOrEqual(200000U));
        }

        [Fact]
        public void GreaterThanOrEqual_WithLong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50000L));
            Assert.True(literal.GreaterThanOrEqual(100000L));
            Assert.False(literal.GreaterThanOrEqual(200000L));
        }

        [Fact]
        public void GreaterThanOrEqual_WithULong_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100000U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50000UL));
            Assert.True(literal.GreaterThanOrEqual(100000UL));
            Assert.False(literal.GreaterThanOrEqual(200000UL));
        }

        [Fact]
        public void GreaterThanOrEqual_WithFloat_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50.0f));
            Assert.True(literal.GreaterThanOrEqual(100.0f));
            Assert.False(literal.GreaterThanOrEqual(200.0f));
        }

        [Fact]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50.0));
            Assert.True(literal.GreaterThanOrEqual(100.0));
            Assert.False(literal.GreaterThanOrEqual(200.0));
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            var literal = new UIntLiteral(100U);

            // Act & Assert
            Assert.True(literal.GreaterThanOrEqual(50m));
            Assert.True(literal.GreaterThanOrEqual(100m));
            Assert.False(literal.GreaterThanOrEqual(200m));
        }
        #endregion

        #region Edge Cases Tests
        [Fact]
        public void Comparisons_WithZero_ShouldWorkCorrectly()
        {
            // Arrange
            var literal = new UIntLiteral(0U);

            // Act & Assert
            Assert.True(literal.Equal(0U));
            Assert.False(literal.LessThan(0U));
            Assert.False(literal.GreaterThan(0U));
            Assert.True(literal.LessThanOrEqual(0U));
            Assert.True(literal.GreaterThanOrEqual(0U));
        }

        [Fact]
        public void Comparisons_WithMaxValue_ShouldWorkCorrectly()
        {
            // Arrange
            var literal = new UIntLiteral(uint.MaxValue);

            // Act & Assert
            Assert.True(literal.Equal(uint.MaxValue));
            Assert.False(literal.LessThan(uint.MaxValue));
            Assert.False(literal.GreaterThan(uint.MaxValue));
            Assert.True(literal.LessThanOrEqual(uint.MaxValue));
            Assert.True(literal.GreaterThanOrEqual(uint.MaxValue));
        }

        [Fact]
        public void Equal_WithNullLiteral_ShouldReturnFalse()
        {
            // Arrange
            var literal = new UIntLiteral(100U);
            var nullLiteral = new NullLiteral(typeof(uint?));

            // Act & Assert
            Assert.False(literal.Equal(nullLiteral));
        }
        #endregion
    }
}