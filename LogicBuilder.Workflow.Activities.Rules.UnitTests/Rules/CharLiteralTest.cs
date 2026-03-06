namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class CharLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_ShouldInitializeValue()
        {
            // Arrange
            char expectedValue = 'A';

            // Act
            var literal = Literal.MakeLiteral(typeof(char), expectedValue);

            // Assert
            Assert.NotNull(literal);
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Value_ShouldReturnCharType()
        {
            // Arrange
            char value = 'Z';
            var literal = Literal.MakeLiteral(typeof(char), value);

            // Act
            var result = literal.Value;

            // Assert
            Assert.IsType<char>(result);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData('A', 'A', true)]
        [InlineData('A', 'B', false)]
        [InlineData('\0', '\0', true)]
        [InlineData('Z', 'A', false)]
        [InlineData('0', '0', true)]
        public void Equal_WithChar_ShouldReturnCorrectResult(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('B', (sbyte)66, true)]
        [InlineData('A', (sbyte)66, false)]
        [InlineData('Z', (sbyte)-1, false)]
        public void Equal_WithSByte_ShouldReturnCorrectResult(char left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)65, true)]
        [InlineData('B', (byte)66, true)]
        [InlineData('A', (byte)66, false)]
        [InlineData('\0', (byte)0, true)]
        public void Equal_WithByte_ShouldReturnCorrectResult(char left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)65, true)]
        [InlineData('B', (short)66, true)]
        [InlineData('A', (short)66, false)]
        [InlineData('A', (short)-1, false)]
        public void Equal_WithShort_ShouldReturnCorrectResult(char left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)65, true)]
        [InlineData('B', (ushort)66, true)]
        [InlineData('A', (ushort)66, false)]
        [InlineData('\0', (ushort)0, true)]
        public void Equal_WithUShort_ShouldReturnCorrectResult(char left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65, true)]
        [InlineData('B', 66, true)]
        [InlineData('A', 66, false)]
        [InlineData('A', -1, false)]
        [InlineData('\0', 0, true)]
        public void Equal_WithInt_ShouldReturnCorrectResult(char left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65u, true)]
        [InlineData('B', 66u, true)]
        [InlineData('A', 66u, false)]
        [InlineData('\0', 0u, true)]
        public void Equal_WithUInt_ShouldReturnCorrectResult(char left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65L, true)]
        [InlineData('B', 66L, true)]
        [InlineData('A', 66L, false)]
        [InlineData('A', -1L, false)]
        public void Equal_WithLong_ShouldReturnCorrectResult(char left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65UL, true)]
        [InlineData('B', 66UL, true)]
        [InlineData('A', 66UL, false)]
        [InlineData('\0', 0UL, true)]
        public void Equal_WithULong_ShouldReturnCorrectResult(char left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65.0f, true)]
        [InlineData('A', 65.5f, false)]
        [InlineData('B', 66.0f, true)]
        [InlineData('A', 66.0f, false)]
        public void Equal_WithFloat_ShouldReturnCorrectResult(char left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65.0, true)]
        [InlineData('A', 65.5, false)]
        [InlineData('B', 66.0, true)]
        [InlineData('A', 66.0, false)]
        public void Equal_WithDouble_ShouldReturnCorrectResult(char left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            char left = 'A';
            decimal right = 65m;
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('B', (sbyte)66, true)]
        [InlineData('A', (sbyte)66, false)]
        public void Equal_WithSByte_DirectCall_ReturnsExpectedResult(char charValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)65, true)]
        [InlineData('B', (byte)66, true)]
        [InlineData('A', (byte)66, false)]
        public void Equal_WithByte_DirectCall_ReturnsExpectedResult(char charValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'A', true)]
        [InlineData('A', 'B', false)]
        [InlineData('\0', '\0', true)]
        public void Equal_WithChar_DirectCall_ReturnsExpectedResult(char charValue1, char charValue2, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue1);

            // Act
            bool result = literal.Equal(charValue2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)65, true)]
        [InlineData('B', (short)66, true)]
        [InlineData('A', (short)66, false)]
        public void Equal_WithShort_DirectCall_ReturnsExpectedResult(char charValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)65, true)]
        [InlineData('B', (ushort)66, true)]
        [InlineData('A', (ushort)66, false)]
        public void Equal_WithUShort_DirectCall_ReturnsExpectedResult(char charValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65, true)]
        [InlineData('B', 66, true)]
        [InlineData('A', 66, false)]
        public void Equal_WithInt_DirectCall_ReturnsExpectedResult(char charValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65U, true)]
        [InlineData('B', 66U, true)]
        [InlineData('A', 66U, false)]
        public void Equal_WithUInt_DirectCall_ReturnsExpectedResult(char charValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65L, true)]
        [InlineData('B', 66L, true)]
        [InlineData('A', 66L, false)]
        public void Equal_WithLong_DirectCall_ReturnsExpectedResult(char charValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65UL, true)]
        [InlineData('B', 66UL, true)]
        [InlineData('A', 66UL, false)]
        public void Equal_WithULong_DirectCall_ReturnsExpectedResult(char charValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65.0f, true)]
        [InlineData('A', 65.5f, false)]
        [InlineData('B', 66.0f, true)]
        public void Equal_WithFloat_DirectCall_ReturnsExpectedResult(char charValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 65.0, true)]
        [InlineData('A', 65.5, false)]
        [InlineData('B', 66.0, true)]
        public void Equal_WithDouble_DirectCall_ReturnsExpectedResult(char charValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.Equal(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new CharLiteral('A');

            // Act
            bool resultEqual = literal.Equal(65m);
            bool resultNotEqual = literal.Equal(66m);
            bool resultZero = literal.Equal(0m);

            // Assert
            Assert.True(resultEqual);
            Assert.False(resultNotEqual);
            Assert.False(resultZero);
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData('A', 'Z', true)]
        [InlineData('Z', 'A', false)]
        [InlineData('A', 'A', false)]
        [InlineData('\0', 'A', true)]
        [InlineData('0', '9', true)]
        public void LessThan_WithChar_ShouldReturnCorrectResult(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (sbyte)90, true)]
        [InlineData('Z', (sbyte)65, false)]
        [InlineData('A', (sbyte)-1, false)]
        public void LessThan_WithSByte_ShouldReturnCorrectResult(char left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)90, true)]
        [InlineData('Z', (byte)65, false)]
        [InlineData('\0', (byte)1, true)]
        public void LessThan_WithByte_ShouldReturnCorrectResult(char left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)90, true)]
        [InlineData('Z', (short)65, false)]
        [InlineData('A', (short)-1, false)]
        public void LessThan_WithShort_ShouldReturnCorrectResult(char left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)90, true)]
        [InlineData('Z', (ushort)65, false)]
        [InlineData('\0', (ushort)1, true)]
        public void LessThan_WithUShort_ShouldReturnCorrectResult(char left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90, true)]
        [InlineData('Z', 65, false)]
        [InlineData('A', -1, false)]
        public void LessThan_WithInt_ShouldReturnCorrectResult(char left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90u, true)]
        [InlineData('Z', 65u, false)]
        [InlineData('\0', 1u, true)]
        public void LessThan_WithUInt_ShouldReturnCorrectResult(char left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90L, true)]
        [InlineData('Z', 65L, false)]
        [InlineData('A', -1L, false)]
        public void LessThan_WithLong_ShouldReturnCorrectResult(char left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90UL, true)]
        [InlineData('Z', 65UL, false)]
        [InlineData('\0', 1UL, true)]
        public void LessThan_WithULong_ShouldReturnCorrectResult(char left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0f, true)]
        [InlineData('Z', 65.0f, false)]
        [InlineData('A', 65.0f, false)]
        public void LessThan_WithFloat_ShouldReturnCorrectResult(char left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0, true)]
        [InlineData('Z', 65.0, false)]
        [InlineData('A', 65.0, false)]
        public void LessThan_WithDouble_ShouldReturnCorrectResult(char left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            char left = 'A';
            decimal right = 90m;
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('A', (sbyte)90, true)]
        [InlineData('Z', (sbyte)65, false)]
        [InlineData('A', (sbyte)65, false)]
        public void LessThan_WithSByte_DirectCall_ReturnsExpectedResult(char charValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)90, true)]
        [InlineData('Z', (byte)65, false)]
        [InlineData('\0', (byte)1, true)]
        public void LessThan_WithByte_DirectCall_ReturnsExpectedResult(char charValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'Z', true)]
        [InlineData('Z', 'A', false)]
        [InlineData('A', 'A', false)]
        public void LessThan_WithChar_DirectCall_ReturnsExpectedResult(char charValue1, char charValue2, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue1);

            // Act
            bool result = literal.LessThan(charValue2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)90, true)]
        [InlineData('Z', (short)65, false)]
        [InlineData('A', (short)65, false)]
        public void LessThan_WithShort_DirectCall_ReturnsExpectedResult(char charValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)90, true)]
        [InlineData('Z', (ushort)65, false)]
        [InlineData('\0', (ushort)1, true)]
        public void LessThan_WithUShort_DirectCall_ReturnsExpectedResult(char charValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90, true)]
        [InlineData('Z', 65, false)]
        [InlineData('A', 65, false)]
        public void LessThan_WithInt_DirectCall_ReturnsExpectedResult(char charValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90U, true)]
        [InlineData('Z', 65U, false)]
        [InlineData('\0', 1U, true)]
        public void LessThan_WithUInt_DirectCall_ReturnsExpectedResult(char charValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90L, true)]
        [InlineData('Z', 65L, false)]
        [InlineData('A', 65L, false)]
        public void LessThan_WithLong_DirectCall_ReturnsExpectedResult(char charValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90UL, true)]
        [InlineData('Z', 65UL, false)]
        [InlineData('\0', 1UL, true)]
        public void LessThan_WithULong_DirectCall_ReturnsExpectedResult(char charValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0f, true)]
        [InlineData('Z', 65.0f, false)]
        [InlineData('A', 65.0f, false)]
        public void LessThan_WithFloat_DirectCall_ReturnsExpectedResult(char charValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0, true)]
        [InlineData('Z', 65.0, false)]
        [InlineData('A', 65.0, false)]
        public void LessThan_WithDouble_DirectCall_ReturnsExpectedResult(char charValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new CharLiteral('A');

            // Act
            bool resultLess = literal.LessThan(90m);
            bool resultGreater = literal.LessThan(60m);
            bool resultEqual = literal.LessThan(65m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.False(resultEqual);
        }
        #endregion

        #region GreaterThan Tests
        [Theory]
        [InlineData('Z', 'A', true)]
        [InlineData('A', 'Z', false)]
        [InlineData('A', 'A', false)]
        [InlineData('A', '\0', true)]
        [InlineData('9', '0', true)]
        public void GreaterThan_WithChar_ShouldReturnCorrectResult(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (sbyte)65, true)]
        [InlineData('A', (sbyte)90, false)]
        [InlineData('A', (sbyte)-1, true)]
        public void GreaterThan_WithSByte_ShouldReturnCorrectResult(char left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (byte)65, true)]
        [InlineData('A', (byte)90, false)]
        [InlineData('A', (byte)0, true)]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult(char left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (short)65, true)]
        [InlineData('A', (short)90, false)]
        [InlineData('A', (short)-1, true)]
        public void GreaterThan_WithShort_ShouldReturnCorrectResult(char left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (ushort)65, true)]
        [InlineData('A', (ushort)90, false)]
        [InlineData('A', (ushort)0, true)]
        public void GreaterThan_WithUShort_ShouldReturnCorrectResult(char left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65, true)]
        [InlineData('A', 90, false)]
        [InlineData('A', -1, true)]
        public void GreaterThan_WithInt_ShouldReturnCorrectResult(char left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65u, true)]
        [InlineData('A', 90u, false)]
        [InlineData('A', 0u, true)]
        public void GreaterThan_WithUInt_ShouldReturnCorrectResult(char left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65L, true)]
        [InlineData('A', 90L, false)]
        [InlineData('A', -1L, true)]
        public void GreaterThan_WithLong_ShouldReturnCorrectResult(char left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65UL, true)]
        [InlineData('A', 90UL, false)]
        [InlineData('A', 0UL, true)]
        public void GreaterThan_WithULong_ShouldReturnCorrectResult(char left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0f, true)]
        [InlineData('A', 90.0f, false)]
        [InlineData('A', 65.0f, false)]
        public void GreaterThan_WithFloat_ShouldReturnCorrectResult(char left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0, true)]
        [InlineData('A', 90.0, false)]
        [InlineData('A', 65.0, false)]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult(char left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            char left = 'Z';
            decimal right = 65m;
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('Z', (sbyte)65, true)]
        [InlineData('A', (sbyte)90, false)]
        [InlineData('A', (sbyte)65, false)]
        public void GreaterThan_WithSByte_DirectCall_ReturnsExpectedResult(char charValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (byte)65, true)]
        [InlineData('A', (byte)90, false)]
        [InlineData('A', (byte)0, true)]
        public void GreaterThan_WithByte_DirectCall_ReturnsExpectedResult(char charValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 'A', true)]
        [InlineData('A', 'Z', false)]
        [InlineData('A', 'A', false)]
        public void GreaterThan_WithChar_DirectCall_ReturnsExpectedResult(char charValue1, char charValue2, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue1);

            // Act
            bool result = literal.GreaterThan(charValue2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (short)65, true)]
        [InlineData('A', (short)90, false)]
        [InlineData('A', (short)65, false)]
        public void GreaterThan_WithShort_DirectCall_ReturnsExpectedResult(char charValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (ushort)65, true)]
        [InlineData('A', (ushort)90, false)]
        [InlineData('A', (ushort)0, true)]
        public void GreaterThan_WithUShort_DirectCall_ReturnsExpectedResult(char charValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65, true)]
        [InlineData('A', 90, false)]
        [InlineData('A', 65, false)]
        public void GreaterThan_WithInt_DirectCall_ReturnsExpectedResult(char charValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65U, true)]
        [InlineData('A', 90U, false)]
        [InlineData('A', 0U, true)]
        public void GreaterThan_WithUInt_DirectCall_ReturnsExpectedResult(char charValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65L, true)]
        [InlineData('A', 90L, false)]
        [InlineData('A', 65L, false)]
        public void GreaterThan_WithLong_DirectCall_ReturnsExpectedResult(char charValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65UL, true)]
        [InlineData('A', 90UL, false)]
        [InlineData('A', 0UL, true)]
        public void GreaterThan_WithULong_DirectCall_ReturnsExpectedResult(char charValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0f, true)]
        [InlineData('A', 90.0f, false)]
        [InlineData('A', 65.0f, false)]
        public void GreaterThan_WithFloat_DirectCall_ReturnsExpectedResult(char charValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0, true)]
        [InlineData('A', 90.0, false)]
        [InlineData('A', 65.0, false)]
        public void GreaterThan_WithDouble_DirectCall_ReturnsExpectedResult(char charValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new CharLiteral('Z');

            // Act
            bool resultGreater = literal.GreaterThan(65m);
            bool resultLess = literal.GreaterThan(100m);
            bool resultEqual = literal.GreaterThan(90m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.False(resultEqual);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Theory]
        [InlineData('A', 'Z', true)]
        [InlineData('A', 'A', true)]
        [InlineData('Z', 'A', false)]
        [InlineData('\0', 'A', true)]
        [InlineData('0', '9', true)]
        public void LessThanOrEqual_WithChar_ShouldReturnCorrectResult(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (sbyte)90, true)]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('Z', (sbyte)65, false)]
        [InlineData('A', (sbyte)-1, false)]
        public void LessThanOrEqual_WithSByte_ShouldReturnCorrectResult(char left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)90, true)]
        [InlineData('A', (byte)65, true)]
        [InlineData('Z', (byte)65, false)]
        [InlineData('\0', (byte)0, true)]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult(char left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)90, true)]
        [InlineData('A', (short)65, true)]
        [InlineData('Z', (short)65, false)]
        [InlineData('A', (short)-1, false)]
        public void LessThanOrEqual_WithShort_ShouldReturnCorrectResult(char left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)90, true)]
        [InlineData('A', (ushort)65, true)]
        [InlineData('Z', (ushort)65, false)]
        [InlineData('\0', (ushort)0, true)]
        public void LessThanOrEqual_WithUShort_ShouldReturnCorrectResult(char left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90, true)]
        [InlineData('A', 65, true)]
        [InlineData('Z', 65, false)]
        [InlineData('A', -1, false)]
        public void LessThanOrEqual_WithInt_ShouldReturnCorrectResult(char left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90u, true)]
        [InlineData('A', 65u, true)]
        [InlineData('Z', 65u, false)]
        [InlineData('\0', 0u, true)]
        public void LessThanOrEqual_WithUInt_ShouldReturnCorrectResult(char left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90L, true)]
        [InlineData('A', 65L, true)]
        [InlineData('Z', 65L, false)]
        [InlineData('A', -1L, false)]
        public void LessThanOrEqual_WithLong_ShouldReturnCorrectResult(char left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90UL, true)]
        [InlineData('A', 65UL, true)]
        [InlineData('Z', 65UL, false)]
        [InlineData('\0', 0UL, true)]
        public void LessThanOrEqual_WithULong_ShouldReturnCorrectResult(char left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0f, true)]
        [InlineData('A', 65.0f, true)]
        [InlineData('Z', 65.0f, false)]
        public void LessThanOrEqual_WithFloat_ShouldReturnCorrectResult(char left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0, true)]
        [InlineData('A', 65.0, true)]
        [InlineData('Z', 65.0, false)]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult(char left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            char left = 'A';
            decimal right = 90m;
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('A', (sbyte)90, true)]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('Z', (sbyte)65, false)]
        public void LessThanOrEqual_WithSByte_DirectCall_ReturnsExpectedResult(char charValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (byte)90, true)]
        [InlineData('A', (byte)65, true)]
        [InlineData('Z', (byte)65, false)]
        public void LessThanOrEqual_WithByte_DirectCall_ReturnsExpectedResult(char charValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (short)90, true)]
        [InlineData('A', (short)65, true)]
        [InlineData('Z', (short)65, false)]
        public void LessThanOrEqual_WithShort_DirectCall_ReturnsExpectedResult(char charValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 'Z', true)]
        [InlineData('A', 'A', true)]
        [InlineData('Z', 'A', false)]
        public void LessThanOrEqual_WithChar_DirectCall_ReturnsExpectedResult(char charValue1, char charValue2, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue1);

            // Act
            bool result = literal.LessThanOrEqual(charValue2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', (ushort)90, true)]
        [InlineData('A', (ushort)65, true)]
        [InlineData('Z', (ushort)65, false)]
        public void LessThanOrEqual_WithUShort_DirectCall_ReturnsExpectedResult(char charValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90, true)]
        [InlineData('A', 65, true)]
        [InlineData('Z', 65, false)]
        public void LessThanOrEqual_WithInt_DirectCall_ReturnsExpectedResult(char charValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90U, true)]
        [InlineData('A', 65U, true)]
        [InlineData('Z', 65U, false)]
        public void LessThanOrEqual_WithUInt_DirectCall_ReturnsExpectedResult(char charValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90L, true)]
        [InlineData('A', 65L, true)]
        [InlineData('Z', 65L, false)]
        public void LessThanOrEqual_WithLong_DirectCall_ReturnsExpectedResult(char charValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90UL, true)]
        [InlineData('A', 65UL, true)]
        [InlineData('Z', 65UL, false)]
        public void LessThanOrEqual_WithULong_DirectCall_ReturnsExpectedResult(char charValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0f, true)]
        [InlineData('A', 65.0f, true)]
        [InlineData('Z', 65.0f, false)]
        public void LessThanOrEqual_WithFloat_DirectCall_ReturnsExpectedResult(char charValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('A', 90.0, true)]
        [InlineData('A', 65.0, true)]
        [InlineData('Z', 65.0, false)]
        public void LessThanOrEqual_WithDouble_DirectCall_ReturnsExpectedResult(char charValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.LessThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new CharLiteral('A');

            // Act
            bool resultLess = literal.LessThanOrEqual(90m);
            bool resultGreater = literal.LessThanOrEqual(60m);
            bool resultEqual = literal.LessThanOrEqual(65m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.True(resultEqual);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Theory]
        [InlineData('Z', 'A', true)]
        [InlineData('A', 'A', true)]
        [InlineData('A', 'Z', false)]
        [InlineData('A', '\0', true)]
        [InlineData('9', '0', true)]
        public void GreaterThanOrEqual_WithChar_ShouldReturnCorrectResult(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (sbyte)65, true)]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('A', (sbyte)90, false)]
        [InlineData('A', (sbyte)-1, true)]
        public void GreaterThanOrEqual_WithSByte_ShouldReturnCorrectResult(char left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (byte)65, true)]
        [InlineData('A', (byte)65, true)]
        [InlineData('A', (byte)90, false)]
        [InlineData('\0', (byte)0, true)]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult(char left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (short)65, true)]
        [InlineData('A', (short)65, true)]
        [InlineData('A', (short)90, false)]
        [InlineData('A', (short)-1, true)]
        public void GreaterThanOrEqual_WithShort_ShouldReturnCorrectResult(char left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (ushort)65, true)]
        [InlineData('A', (ushort)65, true)]
        [InlineData('A', (ushort)90, false)]
        [InlineData('\0', (ushort)0, true)]
        public void GreaterThanOrEqual_WithUShort_ShouldReturnCorrectResult(char left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65, true)]
        [InlineData('A', 65, true)]
        [InlineData('A', 90, false)]
        [InlineData('A', -1, true)]
        public void GreaterThanOrEqual_WithInt_ShouldReturnCorrectResult(char left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65u, true)]
        [InlineData('A', 65u, true)]
        [InlineData('A', 90u, false)]
        [InlineData('\0', 0u, true)]
        public void GreaterThanOrEqual_WithUInt_ShouldReturnCorrectResult(char left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65L, true)]
        [InlineData('A', 65L, true)]
        [InlineData('A', 90L, false)]
        [InlineData('A', -1L, true)]
        public void GreaterThanOrEqual_WithLong_ShouldReturnCorrectResult(char left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65UL, true)]
        [InlineData('A', 65UL, true)]
        [InlineData('A', 90UL, false)]
        [InlineData('\0', 0UL, true)]
        public void GreaterThanOrEqual_WithULong_ShouldReturnCorrectResult(char left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0f, true)]
        [InlineData('A', 65.0f, true)]
        [InlineData('A', 90.0f, false)]
        public void GreaterThanOrEqual_WithFloat_ShouldReturnCorrectResult(char left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0, true)]
        [InlineData('A', 65.0, true)]
        [InlineData('A', 90.0, false)]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult(char left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ShouldReturnCorrectResult()
        {
            // Arrange
            char left = 'Z';
            decimal right = 65m;
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('Z', (sbyte)65, true)]
        [InlineData('A', (sbyte)65, true)]
        [InlineData('A', (sbyte)90, false)]
        public void GreaterThanOrEqual_WithSByte_DirectCall_ReturnsExpectedResult(char charValue, sbyte sbyteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(sbyteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (byte)65, true)]
        [InlineData('A', (byte)65, true)]
        [InlineData('A', (byte)90, false)]
        public void GreaterThanOrEqual_WithByte_DirectCall_ReturnsExpectedResult(char charValue, byte byteValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(byteValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (short)65, true)]
        [InlineData('A', (short)65, true)]
        [InlineData('A', (short)90, false)]
        public void GreaterThanOrEqual_WithShort_DirectCall_ReturnsExpectedResult(char charValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 'A', true)]
        [InlineData('A', 'A', true)]
        [InlineData('A', 'Z', false)]
        public void GreaterThanOrEqual_WithChar_DirectCall_ReturnsExpectedResult(char charValue1, char charValue2, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue1);

            // Act
            bool result = literal.GreaterThanOrEqual(charValue2);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', (ushort)65, true)]
        [InlineData('A', (ushort)65, true)]
        [InlineData('A', (ushort)90, false)]
        public void GreaterThanOrEqual_WithUShort_DirectCall_ReturnsExpectedResult(char charValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65, true)]
        [InlineData('A', 65, true)]
        [InlineData('A', 90, false)]
        public void GreaterThanOrEqual_WithInt_DirectCall_ReturnsExpectedResult(char charValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65U, true)]
        [InlineData('A', 65U, true)]
        [InlineData('A', 90U, false)]
        public void GreaterThanOrEqual_WithUInt_DirectCall_ReturnsExpectedResult(char charValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65L, true)]
        [InlineData('A', 65L, true)]
        [InlineData('A', 90L, false)]
        public void GreaterThanOrEqual_WithLong_DirectCall_ReturnsExpectedResult(char charValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65UL, true)]
        [InlineData('A', 65UL, true)]
        [InlineData('A', 90UL, false)]
        public void GreaterThanOrEqual_WithULong_DirectCall_ReturnsExpectedResult(char charValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0f, true)]
        [InlineData('A', 65.0f, true)]
        [InlineData('A', 90.0f, false)]
        public void GreaterThanOrEqual_WithFloat_DirectCall_ReturnsExpectedResult(char charValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('Z', 65.0, true)]
        [InlineData('A', 65.0, true)]
        [InlineData('A', 90.0, false)]
        public void GreaterThanOrEqual_WithDouble_DirectCall_ReturnsExpectedResult(char charValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new CharLiteral(charValue);

            // Act
            bool result = literal.GreaterThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new CharLiteral('Z');

            // Act
            bool resultGreater = literal.GreaterThanOrEqual(65m);
            bool resultLess = literal.GreaterThanOrEqual(100m);
            bool resultEqual = literal.GreaterThanOrEqual(90m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.True(resultEqual);
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void Char_NullCharacter_ShouldHandleCorrectly()
        {
            // Arrange
            char nullChar = '\0';
            var literal = Literal.MakeLiteral(typeof(char), nullChar);

            // Act & Assert
            Assert.Equal(nullChar, literal.Value);
            Assert.Equal(0, (int)nullChar);
        }

        [Fact]
        public void Char_MaxValue_ShouldHandleCorrectly()
        {
            // Arrange
            char maxValue = char.MaxValue;
            var literal = Literal.MakeLiteral(typeof(char), maxValue);

            // Act & Assert
            Assert.Equal(maxValue, literal.Value);
            Assert.Equal(65535, (int)maxValue);
        }

        [Theory]
        [InlineData('A', 65)]
        [InlineData('Z', 90)]
        [InlineData('a', 97)]
        [InlineData('z', 122)]
        [InlineData('0', 48)]
        [InlineData('9', 57)]
        public void Char_CommonCharacters_ShouldHaveCorrectNumericValues(char character, int expectedValue)
        {
            // Arrange
            var charLiteral = Literal.MakeLiteral(typeof(char), character);
            var intLiteral = Literal.MakeLiteral(typeof(int), expectedValue);

            // Act
            var result = charLiteral.Equal(intLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData('A', 'a', false)]
        [InlineData('Z', 'z', false)]
        [InlineData('K', 'k', false)]
        [InlineData(' ', ' ', true)]
        [InlineData('\t', '\t', true)]
        [InlineData('\n', '\n', true)]
        public void Char_CaseSensitivity_ShouldBeHandledCorrectly(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = new CharLiteral(left);
            var rightLiteral = new CharLiteral(right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Char_WhitespaceCharacters_ShouldHandleCorrectly()
        {
            // Arrange
            char space = ' ';
            char tab = '\t';
            var spaceLiteral = Literal.MakeLiteral(typeof(char), space);
            var tabLiteral = Literal.MakeLiteral(typeof(char), tab);

            // Act
            var areEqual = spaceLiteral.Equal(tabLiteral);
            var spaceLessThanTab = spaceLiteral.LessThan(tabLiteral);

            // Assert
            Assert.False(areEqual);
            Assert.False(spaceLessThanTab); // space (32) is less than tab (9) is false
        }
        #endregion
    }
}