namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class UIntArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueAndType()
        {
            // Arrange & Act
            var literal = new UIntArithmeticLiteral(42);

            // Assert
            Assert.Equal(42U, literal.Value);
            Assert.Equal(typeof(uint), literal.m_type);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new UIntArithmeticLiteral(0);

            // Assert
            Assert.Equal(0U, literal.Value);
        }

        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new UIntArithmeticLiteral(uint.MaxValue);

            // Assert
            Assert.Equal(uint.MaxValue, literal.Value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_CallsOtherLiteralAddMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30L, result);
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(15);

            // Act
            var result = literal.Add(25);

            // Assert
            Assert.Equal(40L, result);
        }

        [Fact]
        public void Add_WithLong_ReturnsLongSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(100);

            // Act
            var result = literal.Add(200L);

            // Assert
            Assert.Equal(300L, result);
        }

        [Fact]
        public void Add_WithChar_ReturnsUIntSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add('A'); // 'A' = 65

            // Assert
            Assert.Equal(75U, result);
        }

        [Fact]
        public void Add_WithUShort_ReturnsUIntSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(50);

            // Act
            var result = literal.Add((ushort)100);

            // Assert
            Assert.Equal(150U, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsUIntSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add(20U);

            // Assert
            Assert.Equal(30U, result);
        }

        [Fact]
        public void Add_WithULong_ReturnsULongSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add(100UL);

            // Assert
            Assert.Equal(110UL, result);
        }

        [Fact]
        public void Add_WithFloat_ReturnsFloatSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add(20.5f);

            // Assert
            Assert.Equal(30.5f, result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add(20.75);

            // Assert
            Assert.Equal(30.75, result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Add(20.5m);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(42);

            // Act
            var result = literal.Add("Answer: ");

            // Assert
            Assert.Equal("Answer: 42", result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_CallsOtherLiteralSubtractMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5L, result); // 10 - 5 = 5
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(15);

            // Act
            var result = literal.Subtract(25);

            // Assert
            Assert.Equal(10L, result); // 25 - 15 = 10
        }

        [Fact]
        public void Subtract_WithLong_ReturnsLongDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(100);

            // Act
            var result = literal.Subtract(300L);

            // Assert
            Assert.Equal(200L, result); // 300 - 100 = 200
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsUIntDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(50);

            // Act
            var result = literal.Subtract((ushort)100);

            // Assert
            Assert.Equal(50U, result); // 100 - 50 = 50
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsUIntDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract(30U);

            // Assert
            Assert.Equal(20U, result); // 30 - 10 = 20
        }

        [Fact]
        public void Subtract_WithULong_ReturnsULongDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract(100UL);

            // Assert
            Assert.Equal(90UL, result); // 100 - 10 = 90
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsFloatDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract(30.5f);

            // Assert
            Assert.Equal(20.5f, result); // 30.5 - 10 = 20.5
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract(30.75);

            // Assert
            Assert.Equal(20.75, result); // 30.75 - 10 = 20.75
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Subtract(30.5m);

            // Assert
            Assert.Equal(20.5m, result); // 30.5 - 10 = 20.5
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_CallsOtherLiteralMultiplyMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(50L, result);
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(7);

            // Assert
            Assert.Equal(35L, result);
        }

        [Fact]
        public void Multiply_WithLong_ReturnsLongProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(20L);

            // Assert
            Assert.Equal(200L, result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsUIntProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply((ushort)10);

            // Assert
            Assert.Equal(50U, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsUIntProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(10U);

            // Assert
            Assert.Equal(50U, result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsULongProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(10UL);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsFloatProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(2.5f);

            // Assert
            Assert.Equal(12.5f, result);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(2.5);

            // Assert
            Assert.Equal(12.5, result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Multiply(2.5m);

            // Assert
            Assert.Equal(12.5m, result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_CallsOtherLiteralDivideMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(50);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(0L, result); // 10 / 50   = 0
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(20);

            // Assert
            Assert.Equal(4L, result); // 20 / 5 = 4
        }

        [Fact]
        public void Divide_WithLong_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Divide(100L);

            // Assert
            Assert.Equal(10L, result); // 100 / 10 = 10
        }

        [Fact]
        public void Divide_WithUShort_ReturnsUIntQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide((ushort)50);

            // Assert
            Assert.Equal(10U, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithUInt_ReturnsUIntQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(50U);

            // Assert
            Assert.Equal(10U, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithULong_ReturnsULongQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(50UL);

            // Assert
            Assert.Equal(10UL, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithFloat_ReturnsFloatQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(50.0f);

            // Assert
            Assert.Equal(10.0f, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithDouble_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(50.0);

            // Assert
            Assert.Equal(10.0, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(5);

            // Act
            var result = literal.Divide(50.0m);

            // Assert
            Assert.Equal(10.0m, result); // 50 / 5 = 10
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_CallsOtherLiteralModulusMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(3);
            var literal2 = new IntArithmeticLiteral(10);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(3L, result); // 3 % 10  = 0
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithLong_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10L);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsUIntRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus((ushort)10);

            // Assert
            Assert.Equal(1U, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsUIntRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10U);

            // Assert
            Assert.Equal(1U, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithULong_ReturnsULongRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10UL);

            // Assert
            Assert.Equal(1UL, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsFloatRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10.0f);

            // Assert
            Assert.Equal(1.0f, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10.0);

            // Assert
            Assert.Equal(1.0, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act
            var result = literal.Modulus(10.0m);

            // Assert
            Assert.Equal(1.0m, result); // 10 % 3 = 1
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithArithmeticLiteral_CallsOtherLiteralBitAndMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(12); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitAnd(literal2);

            // Assert
            Assert.Equal(8L, result); // 1000 in binary = 8
        }

        [Fact]
        public void BitAnd_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithInt_ReturnsUIntResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12); // 1100 in binary

            // Act
            var result = literal.BitAnd(10); // 1010 in binary

            // Assert
            Assert.Equal(8L, result); // 1000 in binary = 8
        }

        [Fact]
        public void BitAnd_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitAnd(10L);

            // Assert
            Assert.Equal(8L, result);
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsUIntResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitAnd((ushort)10);

            // Assert
            Assert.Equal(8U, result);
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsUIntResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitAnd(10U);

            // Assert
            Assert.Equal(8U, result);
        }

        [Fact]
        public void BitAnd_WithULong_ReturnsULongResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitAnd(10UL);

            // Assert
            Assert.Equal(8UL, result);
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithArithmeticLiteral_CallsOtherLiteralBitOrMethod()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(12); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitOr(literal2);

            // Assert
            Assert.Equal(14L, result); // 1110 in binary = 14 (returns long per IntArithmeticLiteral.BitOr(uint))
        }

        [Fact]
        public void BitOr_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(10);

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithInt_ReturnsLongResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12); // 1100 in binary

            // Act
            var result = literal.BitOr(10); // 1010 in binary

            // Assert
            Assert.Equal(14L, result); // 1110 in binary = 14
        }

        [Fact]
        public void BitOr_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitOr(10L);

            // Assert
            Assert.Equal(14L, result);
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsUIntResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitOr((ushort)10);

            // Assert
            Assert.Equal(14U, result);
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsUIntResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitOr(10U);

            // Assert
            Assert.Equal(14U, result);
        }

        [Fact]
        public void BitOr_WithULong_ReturnsULongResult()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(12);

            // Act
            var result = literal.BitOr(10UL);

            // Assert
            Assert.Equal(14UL, result);
        }
        #endregion

        #region Edge Cases and Other ArithmeticLiteral Types
        [Fact]
        public void Add_WithCharArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var charLiteral = new CharArithmeticLiteral('A'); // 65

            // Act
            var result = uintLiteral.Add(charLiteral);

            // Assert
            Assert.Equal(75U, result);
        }

        [Fact]
        public void Add_WithIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(50);
            var intLiteral = new IntArithmeticLiteral(100);

            // Act
            var result = uintLiteral.Add(intLiteral);

            // Assert
            Assert.Equal(150L, result);
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var longLiteral = new LongArithmeticLiteral(20);

            // Act
            var result = uintLiteral.Add(longLiteral);

            // Assert
            Assert.Equal(30L, result);
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var ushortLiteral = new UShortArithmeticLiteral(20);

            // Act
            var result = uintLiteral.Add(ushortLiteral);

            // Assert
            Assert.Equal(30U, result);
        }

        [Fact]
        public void Add_WithULongArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var ulongLiteral = new ULongArithmeticLiteral(20);

            // Act
            var result = uintLiteral.Add(ulongLiteral);

            // Assert
            Assert.Equal(30UL, result);
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var floatLiteral = new FloatArithmeticLiteral(20.5f);

            // Act
            var result = uintLiteral.Add(floatLiteral);

            // Assert
            Assert.Equal(30.5f, result);
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var doubleLiteral = new DoubleArithmeticLiteral(20.75);

            // Act
            var result = uintLiteral.Add(doubleLiteral);

            // Assert
            Assert.Equal(30.75, result);
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(10);
            var decimalLiteral = new DecimalArithmeticLiteral(20.5m);

            // Act
            var result = uintLiteral.Add(decimalLiteral);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var uintLiteral = new UIntArithmeticLiteral(42);
            var stringLiteral = new StringArithmeticLiteral("Answer: ");

            // Act
            var result = uintLiteral.Add(stringLiteral);

            // Assert
            Assert.Equal("42Answer: ", result);
        }

        [Fact]
        public void Divide_WithArithmeticLiteral_DemonstratesOperationOrder()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(4);
            var literal2 = new UIntArithmeticLiteral(2);

            // Act - When passing ArithmeticLiteral, operation follows normal order
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(2U, result); // 4 / 2 = 2
        }

        [Fact]
        public void Divide_WithPrimitiveValue_FollowsReversedOrder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(4);

            // Act - When passing primitive, operation order is reversed
            var result = literal.Divide(2U);

            // Assert
            Assert.Equal(0U, result); // 2 / 4 = 0
        }

        [Fact]
        public void Subtract_WithArithmeticLiteral_DemonstratesOperationOrder()
        {
            // Arrange
            var literal1 = new UIntArithmeticLiteral(10);
            var literal2 = new UIntArithmeticLiteral(3);

            // Act - When passing ArithmeticLiteral, operation follows normal order
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(7U, result); // 10 - 3 = 7
        }

        [Fact]
        public void Subtract_WithPrimitiveValue_FollowsReversedOrder()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(3);

            // Act - When passing primitive, operation order is reversed
            var result = literal.Subtract(10U);

            // Assert
            Assert.Equal(7U, result); // 10 - 3 = 7 (reversed order)
        }

        [Fact]
        public void Add_WithLargeValues_HandlesCorrectly()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(uint.MaxValue - 10);

            // Act
            var result = literal.Add(5U);

            // Assert
            Assert.Equal(uint.MaxValue - 5, result);
        }

        [Fact]
        public void BitAnd_WithLargeValues_HandlesCorrectly()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(0xFFFF0000);

            // Act
            var result = literal.BitAnd(0x0000FFFFU);

            // Assert
            Assert.Equal(0U, result);
        }

        [Fact]
        public void BitOr_WithLargeValues_HandlesCorrectly()
        {
            // Arrange
            var literal = new UIntArithmeticLiteral(0xFFFF0000);

            // Act
            var result = literal.BitOr(0x0000FFFFU);

            // Assert
            Assert.Equal(0xFFFFFFFFU, result);
        }
        #endregion
    }
}