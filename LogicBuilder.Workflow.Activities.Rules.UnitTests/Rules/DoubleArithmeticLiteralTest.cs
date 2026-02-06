namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DoubleArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueAndType()
        {
            // Arrange & Act
            var literal = new DoubleArithmeticLiteral(42.5);

            // Assert
            Assert.Equal(42.5, literal.Value);
            Assert.Equal(typeof(double), literal.m_type);
        }

        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DoubleArithmeticLiteral(-100.75);

            // Assert
            Assert.Equal(-100.75, literal.Value);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DoubleArithmeticLiteral(0.0);

            // Assert
            Assert.Equal(0.0, literal.Value);
        }

        [Fact]
        public void Constructor_WithVeryLargeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DoubleArithmeticLiteral(double.MaxValue);

            // Assert
            Assert.Equal(double.MaxValue, literal.Value);
        }

        [Fact]
        public void Constructor_WithVerySmallValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new DoubleArithmeticLiteral(double.MinValue);

            // Assert
            Assert.Equal(double.MinValue, literal.Value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_CallsOtherLiteralAddMethod()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(10.5);
            var literal2 = new IntArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30.5, result);
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsDoubleSum()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(15.25);
            var literal2 = new LongArithmeticLiteral(10L);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(25.25, result);
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ReturnsDoubleSum()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(10.5);
            var literal2 = new FloatArithmeticLiteral(5.25f);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(15.75, result);
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(15.5);

            // Act
            var result = literal.Add(25);

            // Assert
            Assert.Equal(40.5, result);
        }

        [Fact]
        public void Add_WithLong_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(100.25);

            // Act
            var result = literal.Add(200L);

            // Assert
            Assert.Equal(300.25, result);
        }

        [Fact]
        public void Add_WithChar_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Add('A'); // 'A' = 65

            // Assert
            Assert.Equal(75.5, result);
        }

        [Fact]
        public void Add_WithUShort_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(50.75);

            // Act
            var result = literal.Add((ushort)100);

            // Assert
            Assert.Equal(150.75, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.25);

            // Act
            var result = literal.Add(20U);

            // Assert
            Assert.Equal(30.25, result);
        }

        [Fact]
        public void Add_WithULong_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Add(100UL);

            // Assert
            Assert.Equal(110.5, result);
        }

        [Fact]
        public void Add_WithFloat_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Add(20.25f);

            // Assert
            Assert.Equal(30.75, result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Add(20.75);

            // Assert
            Assert.Equal(31.25, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(42.5);

            // Act
            var result = literal.Add("Value: ");

            // Assert
            Assert.Equal("Value: 42.5", result);
        }

        [Fact]
        public void Add_WithNegativeValues_ReturnsCorrectSum()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(-10.5);

            // Act
            var result = literal.Add(-20.25);

            // Assert
            Assert.Equal(-30.75, result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_CallsOtherLiteralSubtractMethod()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(10.5);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5.5, result); // 10.5 - 5 = 5.5
        }

        [Fact]
        public void Subtract_WithLongArithmeticLiteral_ReturnsDoubleDifference()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(20.75);
            var literal2 = new LongArithmeticLiteral(5L);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(15.75, result); // 20.75 - 5 = 15.75
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(15.5);

            // Act
            var result = literal.Subtract(25);

            // Assert
            Assert.Equal(9.5, result); // 25 - 15.5 = 9.5
        }

        [Fact]
        public void Subtract_WithLong_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(100.25);

            // Act
            var result = literal.Subtract(200L);

            // Assert
            Assert.Equal(99.75, result); // 200 - 100.25 = 99.75
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(25.5);

            // Act
            var result = literal.Subtract((ushort)50);

            // Assert
            Assert.Equal(24.5, result); // 50 - 25.5 = 24.5
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.75);

            // Act
            var result = literal.Subtract(30U);

            // Assert
            Assert.Equal(19.25, result); // 30 - 10.75 = 19.25
        }

        [Fact]
        public void Subtract_WithULong_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(50.5);

            // Act
            var result = literal.Subtract(100UL);

            // Assert
            Assert.Equal(49.5, result); // 100 - 50.5 = 49.5
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(15.5);

            // Act
            var result = literal.Subtract(20.25f);

            // Assert
            Assert.Equal(4.75, (double)result, 5); // 20.25 - 15.5 = 4.75
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Subtract(30.75);

            // Assert
            Assert.Equal(20.25, result); // 30.75 - 10.5 = 20.25
        }

        [Fact]
        public void Subtract_WithNegativeValues_ReturnsCorrectDifference()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(-10.5);

            // Act
            var result = literal.Subtract(-5);

            // Assert
            Assert.Equal(5.5, result); // -5 - (-10.5) = 5.5
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_CallsOtherLiteralMultiplyMethod()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(2.5);
            var literal2 = new IntArithmeticLiteral(4);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(10.0, result);
        }

        [Fact]
        public void Multiply_WithLongArithmeticLiteral_ReturnsDoubleProduct()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(3.5);
            var literal2 = new LongArithmeticLiteral(2L);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(7.0, result);
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(5.5);

            // Act
            var result = literal.Multiply(3);

            // Assert
            Assert.Equal(16.5, result);
        }

        [Fact]
        public void Multiply_WithLong_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(4.25);

            // Act
            var result = literal.Multiply(4L);

            // Assert
            Assert.Equal(17.0, result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Multiply((ushort)10);

            // Assert
            Assert.Equal(25.0, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(3.5);

            // Act
            var result = literal.Multiply(5U);

            // Assert
            Assert.Equal(17.5, result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.25);

            // Act
            var result = literal.Multiply(4UL);

            // Assert
            Assert.Equal(9.0, result);
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Multiply(1.5f);

            // Assert
            Assert.Equal(3.75, (double)result, 5);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Multiply(3.2);

            // Assert
            Assert.Equal(8.0, result);
        }

        [Fact]
        public void Multiply_WithZero_ReturnsZero()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(42.5);

            // Act
            var result = literal.Multiply(0);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void Multiply_WithNegativeValues_ReturnsPositiveProduct()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(-5.5);

            // Act
            var result = literal.Multiply(-2);

            // Assert
            Assert.Equal(11.0, result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_CallsOtherLiteralDivideMethod()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(5.0);
            var literal2 = new IntArithmeticLiteral(10);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(0.5, result); // 5.0 / 10.0 = 0.5
        }

        [Fact]
        public void Divide_WithLongArithmeticLiteral_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(4.0);
            var literal2 = new LongArithmeticLiteral(20L);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(0.2, result); // 4.0 / 20 = 0.2
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(4.0);

            // Act
            var result = literal.Divide(20);

            // Assert
            Assert.Equal(5.0, result); // 20 / 4.0 = 5.0
        }

        [Fact]
        public void Divide_WithLong_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Divide(10L);

            // Assert
            Assert.Equal(4.0, result); // 10 / 2.5 = 4.0
        }

        [Fact]
        public void Divide_WithUShort_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(5.0);

            // Act
            var result = literal.Divide((ushort)50);

            // Assert
            Assert.Equal(10.0, result); // 50 / 5.0 = 10.0
        }

        [Fact]
        public void Divide_WithUInt_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(4.0);

            // Act
            var result = literal.Divide(16U);

            // Assert
            Assert.Equal(4.0, result); // 16 / 4.0 = 4.0
        }

        [Fact]
        public void Divide_WithULong_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Divide(10UL);

            // Assert
            Assert.Equal(4.0, result); // 10 / 2.5 = 4.0
        }

        [Fact]
        public void Divide_WithFloat_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.0);

            // Act
            var result = literal.Divide(8.0f);

            // Assert
            Assert.Equal(4.0, (double)result, 5); // 8.0 / 2.0 = 4.0
        }

        [Fact]
        public void Divide_WithDouble_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.5);

            // Act
            var result = literal.Divide(10.0);

            // Assert
            Assert.Equal(4.0, result); // 10.0 / 2.5 = 4.0
        }

        [Fact]
        public void Divide_ResultingInDecimal_ReturnsCorrectValue()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(3.0);

            // Act
            var result = literal.Divide(10);

            // Assert
            Assert.Equal(3.3333333333333335, (double)result, 10); // 10 / 3.0
        }

        [Fact]
        public void Divide_WithNegativeValues_ReturnsNegativeQuotient()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(-4.0);

            // Act
            var result = literal.Divide(20);

            // Assert
            Assert.Equal(-5.0, result); // 20 / -4.0 = -5.0
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_CallsOtherLiteralModulusMethod()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(3.5);
            var literal2 = new IntArithmeticLiteral(10);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(3.5, result); // 3.5 % 10 = 3.5
        }

        [Fact]
        public void Modulus_WithLongArithmeticLiteral_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal1 = new DoubleArithmeticLiteral(4.0);
            var literal2 = new LongArithmeticLiteral(15L);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(4.0, result); // 4.0 % 15 = 4.0
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(3.0);

            // Act
            var result = literal.Modulus(10);

            // Assert
            Assert.Equal(1.0, result); // 10 % 3.0 = 1.0
        }

        [Fact]
        public void Modulus_WithLong_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(4.5);

            // Act
            var result = literal.Modulus(15L);

            // Assert
            Assert.Equal(1.5, result); // 15 % 4.5 = 1.5
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(7.0);

            // Act
            var result = literal.Modulus((ushort)20);

            // Assert
            Assert.Equal(6.0, result); // 20 % 7.0 = 6.0
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(6.0);

            // Act
            var result = literal.Modulus(20U);

            // Assert
            Assert.Equal(2.0, result); // 20 % 6.0 = 2.0
        }

        [Fact]
        public void Modulus_WithULong_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(7.0);

            // Act
            var result = literal.Modulus(25UL);

            // Assert
            Assert.Equal(4.0, result); // 25 % 7.0 = 4.0
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(3.5);

            // Act
            var result = literal.Modulus(10.0f);

            // Assert
            Assert.Equal(3.0, (double)result, 5); // 10.0 % 3.5 = 3.0
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(4.5);

            // Act
            var result = literal.Modulus(20.0);

            // Assert
            Assert.Equal(2.0, result); // 20.0 % 4.5 = 2.0
        }

        [Fact]
        public void Modulus_WithDecimalDivisor_ReturnsCorrectRemainder()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(2.3);

            // Act
            var result = literal.Modulus(7.5);

            // Assert
            Assert.Equal(0.6, (double)result, 1); // 7.5 % 2.3 ≈ 0.6
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void Add_WithPositiveInfinity_ReturnsPositiveInfinity()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(double.PositiveInfinity);

            // Act
            var result = literal.Add(100.0);

            // Assert
            Assert.Equal(double.PositiveInfinity, result);
        }

        [Fact]
        public void Add_WithNegativeInfinity_ReturnsNegativeInfinity()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(double.NegativeInfinity);

            // Act
            var result = literal.Add(100.0);

            // Assert
            Assert.Equal(double.NegativeInfinity, result);
        }

        [Fact]
        public void Add_WithNaN_ReturnsNaN()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(double.NaN);

            // Act
            var result = literal.Add(100.0);

            // Assert
            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void Multiply_ByZero_ReturnsZero()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(0.0);

            // Act
            var result = literal.Multiply(42.5);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void Divide_ByVerySmallNumber_ReturnsLargeNumber()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(0.0001);

            // Act
            var result = literal.Divide(1000.0);

            // Assert
            Assert.Equal(10000000.0, result);
        }
        #endregion

        #region Unsupported Operation Tests
        [Fact]
        public void BitAnd_ThrowsException()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(5));
        }

        [Fact]
        public void BitOr_ThrowsException()
        {
            // Arrange
            var literal = new DoubleArithmeticLiteral(10.5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(5));
        }
        #endregion
    }
}