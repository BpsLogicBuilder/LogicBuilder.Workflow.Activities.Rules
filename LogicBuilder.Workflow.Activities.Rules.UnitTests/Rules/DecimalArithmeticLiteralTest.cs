using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DecimalArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueAndType()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(42.5m);

            // Assert
            Assert.Equal(42.5m, literal.Value);
            Assert.Equal(typeof(decimal), literal.m_type);
        }

        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(-100.75m);

            // Assert
            Assert.Equal(-100.75m, literal.Value);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(0.0m);

            // Assert
            Assert.Equal(0.0m, literal.Value);
        }

        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(decimal.MaxValue);

            // Assert
            Assert.Equal(decimal.MaxValue, literal.Value);
        }

        [Fact]
        public void Constructor_WithMinValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(decimal.MinValue);

            // Assert
            Assert.Equal(decimal.MinValue, literal.Value);
        }

        [Fact]
        public void Constructor_WithVerySmallFraction_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DecimalArithmeticLiteral(0.0000000001m);

            // Assert
            Assert.Equal(0.0000000001m, literal.Value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithIntArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new IntArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new LongArithmeticLiteral(20L);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithCharArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new CharArithmeticLiteral('A'); // 'A' = 65

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(75.5m, result);
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UShortArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UIntArithmeticLiteral(20U);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithULongArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new ULongArithmeticLiteral(20UL);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ReturnsDecimalSum()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DecimalArithmeticLiteral(20.25m);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.75m, result);
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsString()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(42.5m);
            var literal2 = new StringArithmeticLiteral("Answer: ");

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal("42.5Answer: ", result);
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(15.5m);

            // Act
            var result = literal.Add(25);

            // Assert
            Assert.Equal(40.5m, result);
        }

        [Fact]
        public void Add_WithLong_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(100.25m);

            // Act
            var result = literal.Add(200L);

            // Assert
            Assert.Equal(300.25m, result);
        }

        [Fact]
        public void Add_WithChar_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Add('A'); // 'A' = 65

            // Assert
            Assert.Equal(75.5m, result);
        }

        [Fact]
        public void Add_WithUShort_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(50.5m);

            // Act
            var result = literal.Add((ushort)100);

            // Assert
            Assert.Equal(150.5m, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Add(20U);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithULong_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Add(100UL);

            // Assert
            Assert.Equal(110.5m, result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Add(20.25m);

            // Assert
            Assert.Equal(30.75m, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(42.5m);

            // Act
            var result = literal.Add("Answer: ");

            // Assert
            Assert.Equal("Answer: 42.5", result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithIntArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert - Note: 10.5 - 5 = 5.5
            Assert.Equal(5.5m, result);
        }

        [Fact]
        public void Subtract_WithLongArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new LongArithmeticLiteral(5L);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5.5m, result);
        }

        [Fact]
        public void Subtract_WithUShortArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UShortArithmeticLiteral(20);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(-9.5m, result);
        }

        [Fact]
        public void Subtract_WithUIntArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UIntArithmeticLiteral(20U);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(-9.5m, result);
        }

        [Fact]
        public void Subtract_WithULongArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new ULongArithmeticLiteral(20UL);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(-9.5m, result);
        }

        [Fact]
        public void Subtract_WithDecimalArithmeticLiteral_ReturnsDecimalDifference()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DecimalArithmeticLiteral(5.25m);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5.25m, result);
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(15.5m);

            // Act - Note: 25 - 15.5 = 9.5 (right - left)
            var result = literal.Subtract(25);

            // Assert
            Assert.Equal(9.5m, result);
        }

        [Fact]
        public void Subtract_WithLong_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(100.25m);

            // Act - Note: 300 - 100.25 = 199.75 (right - left)
            var result = literal.Subtract(300L);

            // Assert
            Assert.Equal(199.75m, result);
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(50.5m);

            // Act - Note: 100 - 50.5 = 49.5 (right - left)
            var result = literal.Subtract((ushort)100);

            // Assert
            Assert.Equal(49.5m, result);
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 30 - 10.5 = 19.5 (right - left)
            var result = literal.Subtract(30U);

            // Assert
            Assert.Equal(19.5m, result);
        }

        [Fact]
        public void Subtract_WithULong_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 100 - 10.5 = 89.5 (right - left)
            var result = literal.Subtract(100UL);

            // Assert
            Assert.Equal(89.5m, result);
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 30.25 - 10.5 = 19.75 (right - left)
            var result = literal.Subtract(30.25m);

            // Assert
            Assert.Equal(19.75m, result);
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithIntArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new IntArithmeticLiteral(2);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(21.0m, result);
        }

        [Fact]
        public void Multiply_WithLongArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new LongArithmeticLiteral(3L);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(31.5m, result);
        }

        [Fact]
        public void Multiply_WithUShortArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UShortArithmeticLiteral(4);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(42.0m, result);
        }

        [Fact]
        public void Multiply_WithUIntArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UIntArithmeticLiteral(5U);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(52.5m, result);
        }

        [Fact]
        public void Multiply_WithULongArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new ULongArithmeticLiteral(6UL);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(63.0m, result);
        }

        [Fact]
        public void Multiply_WithDecimalArithmeticLiteral_ReturnsDecimalProduct()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DecimalArithmeticLiteral(2.5m);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(26.25m, result);
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(15.5m);

            // Act - Note: 25 * 15.5 (right * left)
            var result = literal.Multiply(25);

            // Assert
            Assert.Equal(387.5m, result);
        }

        [Fact]
        public void Multiply_WithLong_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.25m);

            // Act - Note: 20 * 10.25 (right * left)
            var result = literal.Multiply(20L);

            // Assert
            Assert.Equal(205.0m, result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(5.5m);

            // Act - Note: 10 * 5.5 (right * left)
            var result = literal.Multiply((ushort)10);

            // Assert
            Assert.Equal(55.0m, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 20 * 10.5 (right * left)
            var result = literal.Multiply(20U);

            // Assert
            Assert.Equal(210.0m, result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 10 * 10.5 (right * left)
            var result = literal.Multiply(10UL);

            // Assert
            Assert.Equal(105.0m, result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act - Note: 2.5 * 10.5 (right * left)
            var result = literal.Multiply(2.5m);

            // Assert
            Assert.Equal(26.25m, result);
        }

        [Fact]
        public void Multiply_WithZero_ReturnsZero()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(100.5m);

            // Act
            var result = literal.Multiply(0);

            // Assert
            Assert.Equal(0.0m, result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithIntArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new IntArithmeticLiteral(2);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(5.25m, result);
        }

        [Fact]
        public void Divide_WithLongArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new LongArithmeticLiteral(3L);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(3.5m, result);
        }

        [Fact]
        public void Divide_WithUShortArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new UShortArithmeticLiteral(2);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(5.25m, result);
        }

        [Fact]
        public void Divide_WithUIntArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(21.0m);
            var literal2 = new UIntArithmeticLiteral(3U);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(7.0m, result);
        }

        [Fact]
        public void Divide_WithULongArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new ULongArithmeticLiteral(2UL);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(5.25m, result);
        }

        [Fact]
        public void Divide_WithDecimalArithmeticLiteral_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DecimalArithmeticLiteral(2.5m);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(4.2m, result);
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(5.0m);

            // Act - Note: 100 / 5.0 = 20.0 (right / left)
            var result = literal.Divide(100);

            // Assert
            Assert.Equal(20.0m, result);
        }

        [Fact]
        public void Divide_WithLong_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.0m);

            // Act - Note: 100 / 10.0 = 10.0 (right / left)
            var result = literal.Divide(100L);

            // Assert
            Assert.Equal(10.0m, result);
        }

        [Fact]
        public void Divide_WithUShort_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(5.0m);

            // Act - Note: 100 / 5.0 = 20.0 (right / left)
            var result = literal.Divide((ushort)100);

            // Assert
            Assert.Equal(20.0m, result);
        }

        [Fact]
        public void Divide_WithUInt_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.0m);

            // Act - Note: 100 / 10.0 = 10.0 (right / left)
            var result = literal.Divide(100U);

            // Assert
            Assert.Equal(10.0m, result);
        }

        [Fact]
        public void Divide_WithULong_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(5.0m);

            // Act - Note: 100 / 5.0 = 20.0 (right / left)
            var result = literal.Divide(100UL);

            // Assert
            Assert.Equal(20.0m, result);
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(2.5m);

            // Act - Note: 10.0 / 2.5 = 4.0 (right / left)
            var result = literal.Divide(10.0m);

            // Assert
            Assert.Equal(4.0m, result);
        }

        [Fact]
        public void Zero_Divide_By_Returns_Zero()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act & Assert
            var result = literal.Divide(0);

            // Assert
            Assert.Equal(0m, result);
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithIntArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new IntArithmeticLiteral(3);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(1.5m, result);
        }

        [Fact]
        public void Modulus_WithLongArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new LongArithmeticLiteral(3L);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(1.5m, result);
        }

        [Fact]
        public void Modulus_WithUShortArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(17.5m);
            var literal2 = new UShortArithmeticLiteral(5);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(2.5m, result);
        }

        [Fact]
        public void Modulus_WithUIntArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(22.5m);
            var literal2 = new UIntArithmeticLiteral(7U);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(1.5m, result);
        }

        [Fact]
        public void Modulus_WithULongArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(15.5m);
            var literal2 = new ULongArithmeticLiteral(4UL);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(3.5m, result);
        }

        [Fact]
        public void Modulus_WithDecimalArithmeticLiteral_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DecimalArithmeticLiteral(3.0m);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(1.5m, result);
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10 % 3.5 (right % left)
            var result = literal.Modulus(10);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Modulus_WithLong_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10 % 3.5 (right % left)
            var result = literal.Modulus(10L);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10 % 3.5 (right % left)
            var result = literal.Modulus((ushort)10);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10 % 3.5 (right % left)
            var result = literal.Modulus(10U);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Modulus_WithULong_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10 % 3.5 (right % left)
            var result = literal.Modulus(10UL);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(3.5m);

            // Act - Note: 10.0 % 3.5 (right % left)
            var result = literal.Modulus(10.0m);

            // Assert
            Assert.Equal(3.0m, result);
        }

        [Fact]
        public void Zero_Modulus_Value_Returns_Zero()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act
            var result = literal.Modulus(0);

            // Assert
            Assert.Equal(0m, result);
        }
        #endregion

        #region Invalid Operation Tests
        [Fact]
        public void Add_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new BooleanArithmeticLiteral(true);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Add(literal2));
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new FloatArithmeticLiteral(5.5f);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Add(literal2));
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new DoubleArithmeticLiteral(5.5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Add(literal2));
        }

        [Fact]
        public void Subtract_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new BooleanArithmeticLiteral(false);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Subtract(literal2));
        }

        [Fact]
        public void Multiply_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new BooleanArithmeticLiteral(true);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Multiply(literal2));
        }

        [Fact]
        public void Divide_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new BooleanArithmeticLiteral(false);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Divide(literal2));
        }

        [Fact]
        public void Modulus_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal1 = new DecimalArithmeticLiteral(10.5m);
            var literal2 = new BooleanArithmeticLiteral(true);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal1.Modulus(literal2));
        }

        [Fact]
        public void BitAnd_WithInt_ThrowsException()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(5));
        }

        [Fact]
        public void BitOr_WithInt_ThrowsException()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(10.5m);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(5));
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void Add_WithNegativeValues_ReturnsCorrectSum()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(-10.5m);

            // Act
            var result = literal.Add(-5);

            // Assert
            Assert.Equal(-15.5m, result);
        }

        [Fact]
        public void Subtract_WithNegativeValues_ReturnsCorrectDifference()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(-10.5m);

            // Act - Note: -5 - (-10.5) = 5.5 (right - left)
            var result = literal.Subtract(-5);

            // Assert
            Assert.Equal(5.5m, result);
        }

        [Fact]
        public void Multiply_WithNegativeValue_ReturnsNegativeProduct()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(-5.5m);

            // Act - Note: 10 * (-5.5) = -55.0 (right * left)
            var result = literal.Multiply(10);

            // Assert
            Assert.Equal(-55.0m, result);
        }

        [Fact]
        public void Divide_WithNegativeValue_ReturnsNegativeQuotient()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(-5.0m);

            // Act - Note: 100 / (-5.0) = -20.0 (right / left)
            var result = literal.Divide(100);

            // Assert
            Assert.Equal(-20.0m, result);
        }

        [Fact]
        public void Add_WithMaxValue_HandlesCorrectly()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(decimal.MaxValue);

            // Act & Assert
            Assert.Throws<OverflowException>(() => literal.Add(1));
        }

        [Fact]
        public void Subtract_WithMinValue_HandlesCorrectly()
        {
            // Arrange
            var literal = new DecimalArithmeticLiteral(decimal.MinValue);

            // Act & Assert
            Assert.Throws<OverflowException>(() => literal.Subtract(1m));
        }
        #endregion
    }
}