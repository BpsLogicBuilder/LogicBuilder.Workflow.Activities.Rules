namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class BooleanArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_WithTrue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new BooleanArithmeticLiteral(true);

            // Assert
            Assert.True((bool)literal.Value);
            Assert.Equal(typeof(bool), literal.m_type);
        }

        [Fact]
        public void Constructor_WithFalse_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new BooleanArithmeticLiteral(false);

            // Assert
            Assert.False((bool)literal.Value);
            Assert.Equal(typeof(bool), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsBoxedBoolean()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(true);

            // Act
            var value = literal.Value;

            // Assert
            Assert.IsType<bool>(value);
            Assert.True((bool)value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithNullParameter_ReturnsNull()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(true);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithStringParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(true);

            // Act
            var result = literal.Add("Value: ");

            // Assert
            Assert.Equal("Value: True", result);
        }

        [Fact]
        public void Add_WithFalseAndString_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal.Add("Value: ");

            // Assert
            Assert.Equal("Value: False", result);
        }

        [Fact]
        public void Add_WithIntArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(intLiteral));
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var longLiteral = new LongArithmeticLiteral(100L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(longLiteral));
        }

        [Fact]
        public void Add_WithCharArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var charLiteral = new CharArithmeticLiteral('A');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(charLiteral));
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var ushortLiteral = new UShortArithmeticLiteral(10);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(ushortLiteral));
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var uintLiteral = new UIntArithmeticLiteral(100U);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(uintLiteral));
        }

        [Fact]
        public void Add_WithULongArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var ulongLiteral = new ULongArithmeticLiteral(1000UL);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(ulongLiteral));
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var floatLiteral = new FloatArithmeticLiteral(3.14f);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(floatLiteral));
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var doubleLiteral = new DoubleArithmeticLiteral(3.14159);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(doubleLiteral));
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var decimalLiteral = new DecimalArithmeticLiteral(123.45m);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Add(decimalLiteral));
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsStringConcatenation()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var stringLiteral = new StringArithmeticLiteral("Prefix: ");

            // Act
            var result = boolLiteral.Add(stringLiteral);

            // Assert
            Assert.Equal("TruePrefix: ", result);
        }

        [Fact]
        public void Add_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithNullParameter_WhenValueIsTrue_ReturnsNull()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(true);

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithNullParameter_WhenValueIsFalse_ReturnsFalse()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.False((bool)result);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void BitAnd_WithBoolParameter_ReturnsCorrectResult(bool value1, bool value2, bool expected)
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(value1);

            // Act
            var result = literal.BitAnd(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BitAnd_WithBooleanArithmeticLiteral_TrueAndTrue_ReturnsTrue()
        {
            // Arrange
            var literal1 = new BooleanArithmeticLiteral(true);
            var literal2 = new BooleanArithmeticLiteral(true);

            // Act
            var result = literal1.BitAnd(literal2);

            // Assert
            Assert.True((bool)result);
        }

        [Fact]
        public void BitAnd_WithBooleanArithmeticLiteral_TrueAndFalse_ReturnsFalse()
        {
            // Arrange
            var literal1 = new BooleanArithmeticLiteral(true);
            var literal2 = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal1.BitAnd(literal2);

            // Assert
            Assert.False((bool)result);
        }

        [Fact]
        public void BitAnd_WithNullArithmeticLiteral_WhenValueIsTrue_ReturnsNull()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitAnd(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithNullArithmeticLiteral_WhenValueIsFalse_ReturnsFalse()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(false);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitAnd(nullLiteral);

            // Assert
            Assert.False((bool)result);
        }

        [Fact]
        public void BitAnd_WithIntArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitAnd(intLiteral));
        }

        [Fact]
        public void BitAnd_WithLongArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var longLiteral = new LongArithmeticLiteral(100L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitAnd(longLiteral));
        }

        [Fact]
        public void BitAnd_WithStringArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var stringLiteral = new StringArithmeticLiteral("test");

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitAnd(stringLiteral));
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithNullParameter_WhenValueIsTrue_ReturnsTrue()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(true);

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.True((bool)result);
        }

        [Fact]
        public void BitOr_WithNullParameter_WhenValueIsFalse_ReturnsNull()
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void BitOr_WithBoolParameter_ReturnsCorrectResult(bool value1, bool value2, bool expected)
        {
            // Arrange
            var literal = new BooleanArithmeticLiteral(value1);

            // Act
            var result = literal.BitOr(value2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BitOr_WithBooleanArithmeticLiteral_FalseAndFalse_ReturnsFalse()
        {
            // Arrange
            var literal1 = new BooleanArithmeticLiteral(false);
            var literal2 = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal1.BitOr(literal2);

            // Assert
            Assert.False((bool)result);
        }

        [Fact]
        public void BitOr_WithBooleanArithmeticLiteral_TrueAndFalse_ReturnsTrue()
        {
            // Arrange
            var literal1 = new BooleanArithmeticLiteral(true);
            var literal2 = new BooleanArithmeticLiteral(false);

            // Act
            var result = literal1.BitOr(literal2);

            // Assert
            Assert.True((bool)result);
        }

        [Fact]
        public void BitOr_WithNullArithmeticLiteral_WhenValueIsTrue_ReturnsTrue()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitOr(nullLiteral);

            // Assert
            Assert.True((bool)result);
        }

        [Fact]
        public void BitOr_WithNullArithmeticLiteral_WhenValueIsFalse_ReturnsNull()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(false);
            var nullLiteral = new NullArithmeticLiteral(typeof(bool?));

            // Act
            var result = boolLiteral.BitOr(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithIntArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitOr(intLiteral));
        }

        [Fact]
        public void BitOr_WithLongArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var longLiteral = new LongArithmeticLiteral(100L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitOr(longLiteral));
        }

        [Fact]
        public void BitOr_WithStringArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var stringLiteral = new StringArithmeticLiteral("test");

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.BitOr(stringLiteral));
        }
        #endregion

        #region Unsupported Operations Tests
        [Fact]
        public void Subtract_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Subtract(intLiteral));
        }

        [Fact]
        public void Multiply_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Multiply(intLiteral));
        }

        [Fact]
        public void Divide_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Divide(intLiteral));
        }

        [Fact]
        public void Modulus_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BooleanArithmeticLiteral(true);
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => boolLiteral.Modulus(intLiteral));
        }
        #endregion
    }
}