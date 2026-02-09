namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class FloatLiteralTest
    {
        #region Constructor and Value Tests
        
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = 3.14f;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
            Assert.Equal(typeof(float), literal.m_type);
        }
        
        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = 0.0f;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = -123.45f;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = float.MaxValue;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithMinValue_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = float.MinValue;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithNaN_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = float.NaN;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.True(float.IsNaN((float)literal.Value));
        }
        
        [Fact]
        public void Constructor_WithPositiveInfinity_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = float.PositiveInfinity;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithNegativeInfinity_SetsValueCorrectly()
        {
            // Arrange
            float expectedValue = float.NegativeInfinity;
            
            // Act
            FloatLiteral literal = new(expectedValue);
            
            // Assert
            Assert.Equal(expectedValue, literal.Value);
        }
        
        #endregion
        
        #region Equal Tests
        
        [Fact]
        public void Equal_WithSameLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(42.5f);
            FloatLiteral literal2 = new(42.5f);
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithDifferentLiteral_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal1 = new(42.5f);
            FloatLiteral literal2 = new(43.5f);
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_WithSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(42.0f);
            
            // Act
            bool result = literal.Equal((sbyte)42);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(100.0f);
            
            // Act
            bool result = literal.Equal((byte)100);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithChar_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(65.0f);
            
            // Act
            bool result = literal.Equal('A');
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1000.0f);
            
            // Act
            bool result = literal.Equal((short)1000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithUShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(2000.0f);
            
            // Act
            bool result = literal.Equal((ushort)2000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50000.0f);
            
            // Act
            bool result = literal.Equal(50000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithUInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(60000.0f);
            
            // Act
            bool result = literal.Equal(60000u);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithLong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(100000.0f);
            
            // Act
            bool result = literal.Equal(100000L);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithULong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(200000.0f);
            
            // Act
            bool result = literal.Equal(200000UL);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithFloat_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(3.14f);
            
            // Act
            bool result = literal.Equal(3.14f);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_WithDouble_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal = new(3.14f);
            
            // Act
            bool result = literal.Equal(3.14);//Double has more precision than float, so this should return false

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal = new(3.14f);
            
            // Act
            bool result = literal.Equal(2.71f);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_WithNaN_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal = new(float.NaN);
            
            // Act
            bool result = literal.Equal(float.NaN);
            
            // Assert
            // NaN is not equal to NaN
            Assert.False(result);
        }
        
        #endregion
        
        #region LessThan Tests
        
        [Fact]
        public void LessThan_WithGreaterLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(10.5f);
            FloatLiteral literal2 = new(20.5f);
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal1 = new(20.5f);
            FloatLiteral literal2 = new(10.5f);
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void LessThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(10.5f);
            
            // Act
            bool result = literal.LessThan((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50.0f);
            
            // Act
            bool result = literal.LessThan((byte)100);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithChar_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(64.0f);
            
            // Act
            bool result = literal.LessThan('A'); // 'A' = 65
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(500.0f);
            
            // Act
            bool result = literal.LessThan((short)1000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1000.0f);
            
            // Act
            bool result = literal.LessThan((ushort)2000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(25000.0f);
            
            // Act
            bool result = literal.LessThan(50000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(30000.0f);
            
            // Act
            bool result = literal.LessThan(60000u);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithLong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50000.0f);
            
            // Act
            bool result = literal.LessThan(100000L);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithULong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(100000.0f);
            
            // Act
            bool result = literal.LessThan(200000UL);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1.5f);
            
            // Act
            bool result = literal.LessThan(2.5f);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1.5f);
            
            // Act
            bool result = literal.LessThan(2.5);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_WithNegativeValues_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(-10.5f);
            
            // Act
            bool result = literal.LessThan(-5.0f);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region GreaterThan Tests
        
        [Fact]
        public void GreaterThan_WithSmallerLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(20.5f);
            FloatLiteral literal2 = new(10.5f);
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithGreaterLiteral_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal1 = new(10.5f);
            FloatLiteral literal2 = new(20.5f);
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThan_WithSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50.0f);
            
            // Act
            bool result = literal.GreaterThan((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(150.0f);
            
            // Act
            bool result = literal.GreaterThan((byte)100);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithChar_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(70.0f);
            
            // Act
            bool result = literal.GreaterThan('A'); // 'A' = 65
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(2000.0f);
            
            // Act
            bool result = literal.GreaterThan((short)1000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithUShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(3000.0f);
            
            // Act
            bool result = literal.GreaterThan((ushort)2000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(75000.0f);
            
            // Act
            bool result = literal.GreaterThan(50000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithUInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(80000.0f);
            
            // Act
            bool result = literal.GreaterThan(60000u);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithLong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(150000.0f);
            
            // Act
            bool result = literal.GreaterThan(100000L);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithULong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(250000.0f);
            
            // Act
            bool result = literal.GreaterThan(200000UL);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithFloat_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(5.5f);
            
            // Act
            bool result = literal.GreaterThan(2.5f);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithDouble_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(5.5f);
            
            // Act
            bool result = literal.GreaterThan(2.5);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_WithNegativeValues_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(-5.0f);
            
            // Act
            bool result = literal.GreaterThan(-10.5f);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region LessThanOrEqual Tests
        
        [Fact]
        public void LessThanOrEqual_WithGreaterLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(10.5f);
            FloatLiteral literal2 = new(20.5f);
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(15.5f);
            FloatLiteral literal2 = new(15.5f);
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithSmallerLiteral_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal1 = new(20.5f);
            FloatLiteral literal2 = new(10.5f);
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(10.0f);
            
            // Act
            bool result = literal.LessThanOrEqual((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(20.0f);
            
            // Act
            bool result = literal.LessThanOrEqual((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50.0f);
            
            // Act
            bool result = literal.LessThanOrEqual((byte)100);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithChar_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(65.0f);
            
            // Act
            bool result = literal.LessThanOrEqual('A'); // 'A' = 65
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(500.0f);
            
            // Act
            bool result = literal.LessThanOrEqual((short)1000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithUShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1000.0f);
            
            // Act
            bool result = literal.LessThanOrEqual((ushort)2000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(25000.0f);
            
            // Act
            bool result = literal.LessThanOrEqual(50000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithUInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(30000.0f);
            
            // Act
            bool result = literal.LessThanOrEqual(60000u);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithLong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50000.0f);
            
            // Act
            bool result = literal.LessThanOrEqual(100000L);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithULong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(100000.0f);
            
            // Act
            bool result = literal.LessThanOrEqual(200000UL);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithFloat_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1.5f);
            
            // Act
            bool result = literal.LessThanOrEqual(2.5f);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_WithDouble_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(1.5f);
            
            // Act
            bool result = literal.LessThanOrEqual(2.5);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region GreaterThanOrEqual Tests
        
        [Fact]
        public void GreaterThanOrEqual_WithSmallerLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(20.5f);
            FloatLiteral literal2 = new(10.5f);
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithEqualLiteral_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal1 = new(15.5f);
            FloatLiteral literal2 = new(15.5f);
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithGreaterLiteral_ReturnsFalse()
        {
            // Arrange
            FloatLiteral literal1 = new(10.5f);
            FloatLiteral literal2 = new(20.5f);
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(50.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithEqualSByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(20.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual((sbyte)20);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithByte_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(150.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual((byte)100);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithChar_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(65.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual('A'); // 'A' = 65
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(2000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual((short)1000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithUShort_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(3000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual((ushort)2000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(75000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(50000);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithUInt_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(80000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(60000u);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithLong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(150000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(100000L);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithULong_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(250000.0f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(200000UL);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithFloat_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(5.5f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(2.5f);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_WithDouble_ReturnsTrue()
        {
            // Arrange
            FloatLiteral literal = new(5.5f);
            
            // Act
            bool result = literal.GreaterThanOrEqual(2.5);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region Edge Case Tests
        
        [Fact]
        public void Comparison_WithZero_WorksCorrectly()
        {
            // Arrange
            FloatLiteral zero = new(0.0f);
            FloatLiteral positive = new(1.0f);
            FloatLiteral negative = new(-1.0f);
            
            // Act & Assert
            Assert.True(zero.LessThan(positive));
            Assert.True(zero.GreaterThan(negative));
            Assert.True(zero.Equal(0.0f));
        }
        
        [Fact]
        public void Comparison_WithPositiveInfinity_WorksCorrectly()
        {
            // Arrange
            FloatLiteral infinity = new(float.PositiveInfinity);
            FloatLiteral maxValue = new(float.MaxValue);
            
            // Act & Assert
            Assert.True(infinity.GreaterThan(maxValue));
            Assert.False(infinity.LessThan(maxValue));
        }
        
        [Fact]
        public void Comparison_WithNegativeInfinity_WorksCorrectly()
        {
            // Arrange
            FloatLiteral negInfinity = new(float.NegativeInfinity);
            FloatLiteral minValue = new(float.MinValue);
            
            // Act & Assert
            Assert.True(negInfinity.LessThan(minValue));
            Assert.False(negInfinity.GreaterThan(minValue));
        }
        
        [Fact]
        public void Comparison_WithVerySmallDifference_WorksCorrectly()
        {
            // Arrange
            FloatLiteral literal1 = new(1.0f);
            FloatLiteral literal2 = new(1.0f + float.Epsilon);//Flota lacks the precision to represent this difference

            // Act & Assert
            Assert.False(literal1.LessThan(literal2));
            Assert.True(literal1.Equal(literal2));
        }
        
        #endregion
    }
}