namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ULongArithmeticLiteralTest
    {
        #region Constructor and Value Tests

        [Fact]
        public void Constructor_InitializesValueAndType()
        {
            // Arrange
            ulong testValue = 12345UL;

            // Act
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), testValue);

            // Assert
            Assert.NotNull(literal);
            Assert.Equal(testValue, literal.Value);
            Assert.Equal(typeof(ulong), literal.m_type);
        }

        [Fact]
        public void Constructor_MaxValue()
        {
            // Arrange
            ulong testValue = ulong.MaxValue;

            // Act
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), testValue);

            // Assert
            Assert.Equal(testValue, literal.Value);
        }

        [Fact]
        public void Constructor_MinValue()
        {
            // Arrange
            ulong testValue = ulong.MinValue;

            // Act
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), testValue);

            // Assert
            Assert.Equal(testValue, literal.Value);
        }

        #endregion

        #region Add Tests

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithPositiveInt_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 50);

            // Act
            var result = literal.Add(intLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -50);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(intLiteral));
        }

        [Fact]
        public void Add_WithPositiveLong_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 50L);

            // Act
            var result = literal.Add(longLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), -50L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(longLiteral));
        }

        [Fact]
        public void Add_WithChar_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var charLiteral = ArithmeticLiteral.MakeLiteral(typeof(char), 'A');

            // Act
            var result = literal.Add(charLiteral);

            // Assert
            Assert.Equal(165UL, result); // 100 + 65 (ASCII of 'A')
        }

        [Fact]
        public void Add_WithUShort_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)50);

            // Act
            var result = literal.Add(ushortLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 50U);

            // Act
            var result = literal.Add(uintLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithULong_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 50UL);

            // Act
            var result = literal.Add(ulongLiteral);

            // Assert
            Assert.Equal(150UL, result);
        }

        [Fact]
        public void Add_WithFloat_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 50.5f);

            // Act
            var result = literal.Add(floatLiteral);

            // Assert
            Assert.Equal(150.5f, result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 50.5);

            // Act
            var result = literal.Add(doubleLiteral);

            // Assert
            Assert.Equal(150.5, result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsSum()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 50.5m);

            // Act
            var result = literal.Add(decimalLiteral);

            // Assert
            Assert.Equal(150.5m, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var stringLiteral = ArithmeticLiteral.MakeLiteral(typeof(string), "test");

            // Act
            var result = literal.Add(stringLiteral);

            // Assert
            Assert.Equal("100test", result);
        }

        [Fact]
        public void Add_WithBool_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var boolLiteral = ArithmeticLiteral.MakeLiteral(typeof(bool), true);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(boolLiteral));
        }

        #endregion

        #region Subtract Tests

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.Subtract(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithPositiveInt_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 30);

            // Act
            var result = literal.Subtract(intLiteral);

            // Assert
            Assert.Equal(70UL, result);
        }

        [Fact]
        public void Subtract_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -30);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(intLiteral));
        }

        [Fact]
        public void Subtract_WithPositiveLong_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 30L);

            // Act
            var result = literal.Subtract(longLiteral);

            // Assert
            Assert.Equal(70UL, result);
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)30);

            // Act
            var result = literal.Subtract(ushortLiteral);

            // Assert
            Assert.Equal(70UL, result);
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 30U);

            // Act
            var result = literal.Subtract(uintLiteral);

            // Assert
            Assert.Equal(70UL, result);
        }

        [Fact]
        public void Subtract_WithULong_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 30UL);

            // Act
            var result = literal.Subtract(ulongLiteral);

            // Assert
            Assert.Equal(70UL, result);
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 30.5f);

            // Act
            var result = literal.Subtract(floatLiteral);

            // Assert
            Assert.Equal(69.5f, result);
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 30.5);

            // Act
            var result = literal.Subtract(doubleLiteral);

            // Assert
            Assert.Equal(69.5, result);
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDifference()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 30.5m);

            // Act
            var result = literal.Subtract(decimalLiteral);

            // Assert
            Assert.Equal(69.5m, result);
        }

        #endregion

        #region Multiply Tests

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.Multiply(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithPositiveInt_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 5);

            // Act
            var result = literal.Multiply(intLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(intLiteral));
        }

        [Fact]
        public void Multiply_WithPositiveLong_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 5L);

            // Act
            var result = literal.Multiply(longLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)5);

            // Act
            var result = literal.Multiply(ushortLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 5U);

            // Act
            var result = literal.Multiply(uintLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithULong_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 5UL);

            // Act
            var result = literal.Multiply(ulongLiteral);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 2.5f);

            // Act
            var result = literal.Multiply(floatLiteral);

            // Assert
            Assert.Equal(25.0f, result);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 2.5);

            // Act
            var result = literal.Multiply(doubleLiteral);

            // Assert
            Assert.Equal(25.0, result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsProduct()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 10UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 2.5m);

            // Act
            var result = literal.Multiply(decimalLiteral);

            // Assert
            Assert.Equal(25.0m, result);
        }

        #endregion

        #region Divide Tests

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.Divide(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithPositiveInt_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 5);

            // Act
            var result = literal.Divide(intLiteral);

            // Assert
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Divide_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(intLiteral));
        }

        [Fact]
        public void Divide_WithPositiveLong_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 5L);

            // Act
            var result = literal.Divide(longLiteral);

            // Assert
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Divide_WithUShort_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)5);

            // Act
            var result = literal.Divide(ushortLiteral);

            // Assert
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Divide_WithUInt_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 5U);

            // Act
            var result = literal.Divide(uintLiteral);

            // Assert
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Divide_WithULong_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 5UL);

            // Act
            var result = literal.Divide(ulongLiteral);

            // Assert
            Assert.Equal(20UL, result);
        }

        [Fact]
        public void Divide_WithFloat_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 4.0f);

            // Act
            var result = literal.Divide(floatLiteral);

            // Assert
            Assert.Equal(25.0f, result);
        }

        [Fact]
        public void Divide_WithDouble_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 4.0);

            // Act
            var result = literal.Divide(doubleLiteral);

            // Assert
            Assert.Equal(25.0, result);
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsQuotient()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 4.0m);

            // Act
            var result = literal.Divide(decimalLiteral);

            // Assert
            Assert.Equal(25.0m, result);
        }

        #endregion

        #region Modulus Tests

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.Modulus(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithPositiveInt_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 30);

            // Act
            var result = literal.Modulus(intLiteral);

            // Assert
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -30);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(intLiteral));
        }

        [Fact]
        public void Modulus_WithPositiveLong_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 30L);

            // Act
            var result = literal.Modulus(longLiteral);

            // Assert
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)30);

            // Act
            var result = literal.Modulus(ushortLiteral);

            // Assert
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 30U);

            // Act
            var result = literal.Modulus(uintLiteral);

            // Assert
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithULong_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 30UL);

            // Act
            var result = literal.Modulus(ulongLiteral);

            // Assert
            Assert.Equal(10UL, result);
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 30.0f);

            // Act
            var result = literal.Modulus(floatLiteral);

            // Assert
            Assert.Equal(10.0f, result);
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 30.0);

            // Act
            var result = literal.Modulus(doubleLiteral);

            // Assert
            Assert.Equal(10.0, result);
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsRemainder()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 100UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 30.0m);

            // Act
            var result = literal.Modulus(decimalLiteral);

            // Assert
            Assert.Equal(10.0m, result);
        }

        #endregion

        #region BitAnd Tests

        [Fact]
        public void BitAnd_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.BitAnd(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithPositiveInt_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 15);

            // Act
            var result = literal.BitAnd(intLiteral);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -1);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(intLiteral));
        }

        [Fact]
        public void BitAnd_WithPositiveLong_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 15L);

            // Act
            var result = literal.BitAnd(longLiteral);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)15);

            // Act
            var result = literal.BitAnd(ushortLiteral);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 15U);

            // Act
            var result = literal.BitAnd(uintLiteral);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithULong_ReturnsBitwiseAnd()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 15UL);

            // Act
            var result = literal.BitAnd(ulongLiteral);

            // Assert
            Assert.Equal(15UL, result);
        }

        [Fact]
        public void BitAnd_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 15.0f);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(floatLiteral));
        }

        [Fact]
        public void BitAnd_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 15.0);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(doubleLiteral));
        }

        [Fact]
        public void BitAnd_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 255UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 15.0m);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(decimalLiteral));
        }

        #endregion

        #region BitOr Tests

        [Fact]
        public void BitOr_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var nullLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong?), null);

            // Act
            var result = literal.BitOr(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithPositiveInt_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), 15);

            // Act
            var result = literal.BitOr(intLiteral);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithNegativeInt_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var intLiteral = ArithmeticLiteral.MakeLiteral(typeof(int), -1);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(intLiteral));
        }

        [Fact]
        public void BitOr_WithPositiveLong_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var longLiteral = ArithmeticLiteral.MakeLiteral(typeof(long), 15L);

            // Act
            var result = literal.BitOr(longLiteral);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var ushortLiteral = ArithmeticLiteral.MakeLiteral(typeof(ushort), (ushort)15);

            // Act
            var result = literal.BitOr(ushortLiteral);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var uintLiteral = ArithmeticLiteral.MakeLiteral(typeof(uint), 15U);

            // Act
            var result = literal.BitOr(uintLiteral);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithULong_ReturnsBitwiseOr()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var ulongLiteral = ArithmeticLiteral.MakeLiteral(typeof(ulong), 15UL);

            // Act
            var result = literal.BitOr(ulongLiteral);

            // Assert
            Assert.Equal(255UL, result);
        }

        [Fact]
        public void BitOr_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var floatLiteral = ArithmeticLiteral.MakeLiteral(typeof(float), 15.0f);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(floatLiteral));
        }

        [Fact]
        public void BitOr_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var doubleLiteral = ArithmeticLiteral.MakeLiteral(typeof(double), 15.0);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(doubleLiteral));
        }

        [Fact]
        public void BitOr_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = ArithmeticLiteral.MakeLiteral(typeof(ulong), 240UL);
            var decimalLiteral = ArithmeticLiteral.MakeLiteral(typeof(decimal), 15.0m);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(decimalLiteral));
        }

        #endregion
    }
}