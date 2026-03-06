namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ULongArithmeticLiteralTestNew
    {
        #region Constructor and Value Tests

        [Fact]
        public void Constructor_SetsValueAndType()
        {
            // Arrange & Act
            var literal = new ULongArithmeticLiteral(42);

            // Assert
            Assert.Equal(42UL, literal.Value);
            Assert.Equal(typeof(ulong), literal.m_type);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new ULongArithmeticLiteral(0);

            // Assert
            Assert.Equal(0UL, literal.Value);
        }

        [Fact]
        public void Constructor_WithMaxValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new ULongArithmeticLiteral(ulong.MaxValue);

            // Assert
            Assert.Equal(ulong.MaxValue, literal.Value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_CallsOtherLiteralAddMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30UL, result);
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithPositiveInt_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(-50));
        }

        [Fact]
        public void Add_WithPositiveLong_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50L);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(-50L));
        }

        [Fact]
        public void Add_WithChar_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add('A');

            // Assert
            Assert.Equal(165UL, result); // 100 + 65 (ASCII of 'A')
        }

        [Fact]
        public void Add_WithUShort_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add((ushort)50);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50U);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithULong_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50UL);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithFloat_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50.5f);

            // Assert
            Assert.Equal(150.5f, result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50.5);

            // Assert
            Assert.Equal(150.5, result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsSum()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add(50.5m);

            // Assert
            Assert.Equal(150.5m, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Add("test");

            // Assert
            Assert.Equal("test100", result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_CallsOtherLiteralSubtractMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5UL, result);
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithPositiveInt_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100);

            // Assert
            Assert.Equal(70UL, result); // 100 - 30 = 70
        }

        [Fact]
        public void Subtract_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(-30));
        }

        [Fact]
        public void Subtract_WithPositiveLong_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100L);

            // Assert
            Assert.Equal(70UL, result); // 100 - 30 = 70
        }

        [Fact]
        public void Subtract_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(-30L));
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract((ushort)100);

            // Assert
            Assert.Equal(70UL, result); // 100 - 30 = 70
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100U);

            // Assert
            Assert.Equal(70UL, result); // 100 - 30 = 70
        }

        [Fact]
        public void Subtract_WithULong_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100UL);

            // Assert
            Assert.Equal(70UL, result); // 100 - 30 = 70
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100.5f);

            // Assert
            Assert.Equal(70.5f, result); // 100.5 - 30 = 70.5
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100.5);

            // Assert
            Assert.Equal(70.5, result); // 100.5 - 30 = 70.5
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDifference()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Subtract(100.5m);

            // Assert
            Assert.Equal(70.5m, result); // 100.5 - 30 = 70.5
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_CallsOtherLiteralMultiplyMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithPositiveInt_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(5);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(-5));
        }

        [Fact]
        public void Multiply_WithPositiveLong_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(5L);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(-5L));
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply((ushort)5);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(5U);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(5UL);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(2.5f);

            // Assert
            Assert.Equal(25.0f, result);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(2.5);

            // Assert
            Assert.Equal(25.0, result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsProduct()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(10);

            // Act
            var result = literal.Multiply(2.5m);

            // Assert
            Assert.Equal(25.0m, result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_CallsOtherLiteralDivideMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(10);
            var literal2 = new IntArithmeticLiteral(50);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(0UL, result); // 10 / 50 = 0
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithPositiveInt_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(5);

            // Act
            var result = literal.Divide(100);

            // Assert
            Assert.Equal(20UL, result); // 100 / 5 = 20
        }

        [Fact]
        public void Divide_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(-5));
        }

        [Fact]
        public void Divide_WithPositiveLong_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(5);

            // Act
            var result = literal.Divide(100L);

            // Assert
            Assert.Equal(20UL, result); // 100 / 5 = 20
        }

        [Fact]
        public void Divide_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(-5L));
        }

        [Fact]
        public void Divide_WithUShort_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(5);

            // Act
            var result = literal.Divide((ushort)100);

            // Assert
            Assert.Equal(20UL, result); // 100 / 5 = 20
        }

        [Fact]
        public void Divide_WithUInt_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(5);

            // Act
            var result = literal.Divide(100U);

            // Assert
            Assert.Equal(20UL, result); // 100 / 5 = 20
        }

        [Fact]
        public void Divide_WithULong_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(5);

            // Act
            var result = literal.Divide(100UL);

            // Assert
            Assert.Equal(20UL, result); // 100 / 5 = 20
        }

        [Fact]
        public void Divide_WithFloat_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(4);

            // Act
            var result = literal.Divide(100.0f);

            // Assert
            Assert.Equal(25.0f, result); // 100 / 4 = 25
        }

        [Fact]
        public void Divide_WithDouble_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(4);

            // Act
            var result = literal.Divide(100.0);

            // Assert
            Assert.Equal(25.0, result); // 100 / 4 = 25
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsQuotient()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(4);

            // Act
            var result = literal.Divide(100.0m);

            // Assert
            Assert.Equal(25.0m, result); // 100 / 4 = 25
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_CallsOtherLiteralModulusMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(3);
            var literal2 = new IntArithmeticLiteral(10);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(3UL, result); // 3 % 10 = 3
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithPositiveInt_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100);

            // Assert
            Assert.Equal(10UL, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(-30));
        }

        [Fact]
        public void Modulus_WithPositiveLong_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100L);

            // Assert
            Assert.Equal(10UL, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(100);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(-30L));
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus((ushort)100);

            // Assert
            Assert.Equal(10UL, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100U);

            // Assert
            Assert.Equal(10UL, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithULong_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100UL);

            // Assert
            Assert.Equal(10UL, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100.0f);

            // Assert
            Assert.Equal(10.0f, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100.0);

            // Assert
            Assert.Equal(10.0, result); // 100 % 30 = 10
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsRemainder()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(30);

            // Act
            var result = literal.Modulus(100.0m);

            // Assert
            Assert.Equal(10.0m, result); // 100 % 30 = 10
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithArithmeticLiteral_CallsOtherLiteralBitAndMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(12); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitAnd(literal2);

            // Assert
            Assert.Equal(8UL, result); // 1000 in binary = 8
        }

        [Fact]
        public void BitAnd_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithPositiveInt_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd(15);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(-1));
        }

        [Fact]
        public void BitAnd_WithPositiveLong_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd(15L);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(-1L));
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd((ushort)15);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd(15U);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithULong_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(255);

            // Act
            var result = literal.BitAnd(15UL);

            // Assert
            Assert.Equal(15UL, result);
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithArithmeticLiteral_CallsOtherLiteralBitOrMethod()
        {
            // Arrange
            var literal1 = new ULongArithmeticLiteral(12); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitOr(literal2);

            // Assert
            Assert.Equal(14UL, result); // 1110 in binary = 14
        }

        [Fact]
        public void BitOr_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithPositiveInt_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr(15);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(-1));
        }

        [Fact]
        public void BitOr_WithPositiveLong_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr(15L);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(-1L));
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr((ushort)15);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr(15U);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithULong_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = new ULongArithmeticLiteral(240);

            // Act
            var result = literal.BitOr(15UL);

            // Assert
            Assert.Equal(255UL, result);
        }
        #endregion

        #region Edge Cases and Other ArithmeticLiteral Types

        [Fact]
        public void Add_WithCharArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var charLiteral = new CharArithmeticLiteral('A'); // 65

            // Act
            var result = ulongLiteral.Add(charLiteral);

            // Assert
            Assert.Equal(75UL, result);
        }

        [Fact]
        public void Add_WithIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(50);
            var intLiteral = new IntArithmeticLiteral(100);

            // Act
            var result = ulongLiteral.Add(intLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var longLiteral = new LongArithmeticLiteral(20);

            // Act
            var result = ulongLiteral.Add(longLiteral);

            // Assert
            Assert.Equal(30UL, result);
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var ushortLiteral = new UShortArithmeticLiteral(20);

            // Act
            var result = ulongLiteral.Add(ushortLiteral);

            // Assert
            Assert.Equal(30UL, result);
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var uintLiteral = new UIntArithmeticLiteral(20);

            // Act
            var result = ulongLiteral.Add(uintLiteral);

            // Assert
            Assert.Equal(30UL, result);
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var floatLiteral = new FloatArithmeticLiteral(20.5f);

            // Act
            var result = ulongLiteral.Add(floatLiteral);

            // Assert
            Assert.Equal(30.5f, result);
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var doubleLiteral = new DoubleArithmeticLiteral(20.5);

            // Act
            var result = ulongLiteral.Add(doubleLiteral);

            // Assert
            Assert.Equal(30.5, result);
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(10);
            var decimalLiteral = new DecimalArithmeticLiteral(20.5m);

            // Act
            var result = ulongLiteral.Add(decimalLiteral);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(42);
            var stringLiteral = new StringArithmeticLiteral("Answer: ");

            // Act
            var result = ulongLiteral.Add(stringLiteral);

            // Assert
            Assert.Equal("42Answer: ", result); // ulong.ToString() + string
        }

        [Fact]
        public void Add_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(100);
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));

            // Act
            var result = ulongLiteral.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(50);
            var uintLiteral = new UIntArithmeticLiteral(30);

            // Act
            var result = ulongLiteral.Subtract(uintLiteral);

            // Assert - ulongLiteral.Subtract(uintLiteral) => uintLiteral.Subtract(ulongLiteral.value)
            // => UIntArithmeticLiteral.Subtract(ulong) => ulong - uint = 50 - 30 = 20
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Multiply_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(5);
            var uintLiteral = new UIntArithmeticLiteral(10);

            // Act
            var result = ulongLiteral.Multiply(uintLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Divide_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(50);
            var uintLiteral = new UIntArithmeticLiteral(5);

            // Act
            var result = ulongLiteral.Divide(uintLiteral);

            // Assert - ulongLiteral.Divide(uintLiteral) => uintLiteral.Divide(ulongLiteral.value)
            // => UIntArithmeticLiteral.Divide(ulong) => ulong / uint = 50 / 5 = 10
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(3);
            var uintLiteral = new UIntArithmeticLiteral(10);

            // Act
            var result = ulongLiteral.Modulus(uintLiteral);

            // Assert - uintLiteral.Modulus(ulong) returns ulong % uint = 3 % 10 = 3
            Assert.Equal(3UL, result);
        }

        [Fact]
        public void BitAnd_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(12);
            var uintLiteral = new UIntArithmeticLiteral(10);

            // Act
            var result = ulongLiteral.BitAnd(uintLiteral);

            // Assert
            Assert.Equal(8UL, result);
        }

        [Fact]
        public void BitOr_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var ulongLiteral = new ULongArithmeticLiteral(12);
            var uintLiteral = new UIntArithmeticLiteral(10);

            // Act
            var result = ulongLiteral.BitOr(uintLiteral);

            // Assert
            Assert.Equal(14UL, result);
        }

        #endregion
    }
}