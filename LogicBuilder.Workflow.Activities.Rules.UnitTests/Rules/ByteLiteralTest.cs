namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ByteLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_ShouldInitializeValue()
        {
            // Arrange
            byte expectedValue = 42;

            // Act
            var literal = Literal.MakeLiteral(typeof(byte), expectedValue);

            // Assert
            Assert.NotNull(literal);
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Value_ShouldReturnByteType()
        {
            // Arrange
            byte value = 100;
            var literal = Literal.MakeLiteral(typeof(byte), value);

            // Act
            var result = literal.Value;

            // Assert
            Assert.IsType<byte>(result);
        }

        [Theory]
        [InlineData((byte)0)]
        [InlineData((byte)1)]
        [InlineData((byte)127)]
        [InlineData((byte)128)]
        [InlineData((byte)255)]
        public void Constructor_HandlesVariousByteValues(byte value)
        {
            // Act
            var literal = Literal.MakeLiteral(typeof(byte), value);

            // Assert
            Assert.Equal(value, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData(10, 10, true)]
        [InlineData(10, 20, false)]
        [InlineData(0, 0, true)]
        [InlineData(255, 255, true)]
        [InlineData(255, 0, false)]
        public void Equal_WithByte_ShouldReturnCorrectResult(byte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (sbyte)10, true)]
        [InlineData(10, (sbyte)20, false)]
        [InlineData(10, (sbyte)-1, false)]
        [InlineData(127, (sbyte)127, true)]
        public void Equal_WithSByte_ShouldReturnCorrectResult(byte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 'A', true)]
        [InlineData(66, 'A', false)]
        [InlineData(0, '\0', true)]
        public void Equal_WithChar_ShouldReturnCorrectResult(byte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (short)10, true)]
        [InlineData(10, (short)20, false)]
        [InlineData(10, (short)-10, false)]
        [InlineData(255, (short)255, true)]
        public void Equal_WithShort_ShouldReturnCorrectResult(byte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (ushort)10, true)]
        [InlineData(10, (ushort)20, false)]
        [InlineData(255, (ushort)255, true)]
        public void Equal_WithUShort_ShouldReturnCorrectResult(byte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10, true)]
        [InlineData(10, 20, false)]
        [InlineData(10, -10, false)]
        [InlineData(0, 0, true)]
        public void Equal_WithInt_ShouldReturnCorrectResult(byte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10u, true)]
        [InlineData(10, 20u, false)]
        [InlineData(255, 255u, true)]
        public void Equal_WithUInt_ShouldReturnCorrectResult(byte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10L, true)]
        [InlineData(10, 20L, false)]
        [InlineData(10, -10L, false)]
        public void Equal_WithLong_ShouldReturnCorrectResult(byte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10UL, true)]
        [InlineData(10, 20UL, false)]
        [InlineData(255, 255UL, true)]
        public void Equal_WithULong_ShouldReturnCorrectResult(byte left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10.0f, true)]
        [InlineData(10, 10.5f, false)]
        [InlineData(10, 20.0f, false)]
        public void Equal_WithFloat_ShouldReturnCorrectResult(byte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10.0, true)]
        [InlineData(10, 10.5, false)]
        [InlineData(10, 20.0, false)]
        public void Equal_WithDouble_ShouldReturnCorrectResult(byte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
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
            byte left = 10;
            decimal right = 10m;
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((byte)100, 100, true)]
        [InlineData((byte)100, 99, false)]
        [InlineData((byte)0, 0, true)]
        public void Equal_WithInt_DirectCall_ReturnsExpectedResult(byte byteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.Equal(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)100, 100UL, true)]
        [InlineData((byte)100, 99UL, false)]
        [InlineData((byte)0, 0UL, true)]
        public void Equal_WithULong_DirectCall_ReturnsExpectedResult(byte byteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.Equal(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(10, 10, false)]
        [InlineData(0, 255, true)]
        public void LessThan_WithByte_ShouldReturnCorrectResult(byte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (sbyte)10, true)]
        [InlineData(10, (sbyte)5, false)]
        [InlineData(10, (sbyte)-1, false)]
        public void LessThan_WithSByte_ShouldReturnCorrectResult(byte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(10, -10, false)]
        public void LessThan_WithInt_ShouldReturnCorrectResult(byte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0, true)]
        [InlineData(10, 5.0, false)]
        [InlineData(10, 10.0, false)]
        [InlineData(10, 10.5, true)]
        public void LessThan_WithDouble_ShouldReturnCorrectResult(byte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 'Z', true)]  // 5 < 90
        [InlineData((byte)90, 'A', false)] // 90 > 65
        [InlineData((byte)65, 'A', false)] // 65 == 65
        public void LessThan_WithChar_ReturnsExpectedResult(byte byteValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, (short)10, true)]
        [InlineData((byte)10, (short)5, false)]
        [InlineData((byte)10, (short)10, false)]
        [InlineData((byte)10, (short)-1, false)]
        public void LessThan_WithShort_ReturnsExpectedResult(byte byteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, (ushort)10, true)]
        [InlineData((byte)10, (ushort)5, false)]
        [InlineData((byte)10, (ushort)10, false)]
        public void LessThan_WithUShort_ReturnsExpectedResult(byte byteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10U, true)]
        [InlineData((byte)10, 5U, false)]
        [InlineData((byte)10, 10U, false)]
        public void LessThan_WithUInt_ReturnsExpectedResult(byte byteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10L, true)]
        [InlineData((byte)10, 5L, false)]
        [InlineData((byte)10, 10L, false)]
        [InlineData((byte)10, -1L, false)]
        public void LessThan_WithLong_ReturnsExpectedResult(byte byteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10UL, true)]
        [InlineData((byte)10, 5UL, false)]
        [InlineData((byte)10, 10UL, false)]
        public void LessThan_WithULong_ReturnsExpectedResult(byte byteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10.0f, true)]
        [InlineData((byte)10, 5.0f, false)]
        [InlineData((byte)10, 10.0f, false)]
        public void LessThan_WithFloat_ReturnsExpectedResult(byte byteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new ByteLiteral(5);

            // Act
            bool resultLess = literal.LessThan(10m);
            bool resultGreater = literal.LessThan(3m);
            bool resultEqual = literal.LessThan(5m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.False(resultEqual);
        }
        #endregion

        #region GreaterThan Tests
        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(10, 10, false)]
        [InlineData(255, 0, true)]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult(byte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (sbyte)5, true)]
        [InlineData(5, (sbyte)10, false)]
        [InlineData(10, (sbyte)-1, true)]
        public void GreaterThan_WithSByte_ShouldReturnCorrectResult(byte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(10, -10, true)]
        public void GreaterThan_WithInt_ShouldReturnCorrectResult(byte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0, true)]
        [InlineData(5, 10.0, false)]
        [InlineData(10, 10.0, false)]
        [InlineData(10, 9.5, true)]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult(byte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)90, 'A', true)]  // 90 > 65
        [InlineData((byte)65, 'Z', false)] // 65 < 90
        [InlineData((byte)65, 'A', false)] // 65 == 65
        public void GreaterThan_WithChar_ReturnsExpectedResult(byte byteValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, (short)5, true)]
        [InlineData((byte)5, (short)10, false)]
        [InlineData((byte)10, (short)10, false)]
        public void GreaterThan_WithShort_ReturnsExpectedResult(byte byteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, (ushort)5, true)]
        [InlineData((byte)5, (ushort)10, false)]
        [InlineData((byte)10, (ushort)10, false)]
        public void GreaterThan_WithUShort_ReturnsExpectedResult(byte byteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5U, true)]
        [InlineData((byte)5, 10U, false)]
        [InlineData((byte)10, 10U, false)]
        public void GreaterThan_WithUInt_ReturnsExpectedResult(byte byteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5L, true)]
        [InlineData((byte)5, 10L, false)]
        [InlineData((byte)10, 10L, false)]
        public void GreaterThan_WithLong_ReturnsExpectedResult(byte byteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5UL, true)]
        [InlineData((byte)5, 10UL, false)]
        [InlineData((byte)10, 10UL, false)]
        public void GreaterThan_WithULong_ReturnsExpectedResult(byte byteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5.0f, true)]
        [InlineData((byte)5, 10.0f, false)]
        [InlineData((byte)10, 10.0f, false)]
        public void GreaterThan_WithFloat_ReturnsExpectedResult(byte byteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new ByteLiteral(10);

            // Act
            bool resultGreater = literal.GreaterThan(5m);
            bool resultLess = literal.GreaterThan(15m);
            bool resultEqual = literal.GreaterThan(10m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.False(resultEqual);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 5, false)]
        [InlineData(10, 10, true)]
        [InlineData(0, 0, true)]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult(byte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, (sbyte)10, true)]
        [InlineData(10, (sbyte)10, true)]
        [InlineData(10, (sbyte)5, false)]
        public void LessThanOrEqual_WithSByte_ShouldReturnCorrectResult(byte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10, true)]
        [InlineData(10, 10, true)]
        [InlineData(10, 5, false)]
        public void LessThanOrEqual_WithInt_ShouldReturnCorrectResult(byte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 10.0, true)]
        [InlineData(10, 10.0, true)]
        [InlineData(10, 5.0, false)]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult(byte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)65, 'Z', true)]  // 65 <= 90
        [InlineData((byte)90, 'A', false)] // 90 > 65
        [InlineData((byte)65, 'A', true)]  // 65 == 65
        public void LessThanOrEqual_WithChar_ReturnsExpectedResult(byte byteValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, (short)10, true)]
        [InlineData((byte)10, (short)10, true)]
        [InlineData((byte)10, (short)5, false)]
        public void LessThanOrEqual_WithShort_ReturnsExpectedResult(byte byteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, (ushort)10, true)]
        [InlineData((byte)10, (ushort)10, true)]
        [InlineData((byte)10, (ushort)5, false)]
        public void LessThanOrEqual_WithUShort_ReturnsExpectedResult(byte byteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10U, true)]
        [InlineData((byte)10, 10U, true)]
        [InlineData((byte)10, 5U, false)]
        public void LessThanOrEqual_WithUInt_ReturnsExpectedResult(byte byteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10L, true)]
        [InlineData((byte)10, 10L, true)]
        [InlineData((byte)10, 5L, false)]
        public void LessThanOrEqual_WithLong_ReturnsExpectedResult(byte byteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10UL, true)]
        [InlineData((byte)10, 10UL, true)]
        [InlineData((byte)10, 5UL, false)]
        public void LessThanOrEqual_WithULong_ReturnsExpectedResult(byte byteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)5, 10.0f, true)]
        [InlineData((byte)10, 10.0f, true)]
        [InlineData((byte)10, 5.0f, false)]
        public void LessThanOrEqual_WithFloat_ReturnsExpectedResult(byte byteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.LessThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new ByteLiteral(5);

            // Act
            bool resultLess = literal.LessThanOrEqual(10m);
            bool resultGreater = literal.LessThanOrEqual(3m);
            bool resultEqual = literal.LessThanOrEqual(5m);

            // Assert
            Assert.True(resultLess);
            Assert.False(resultGreater);
            Assert.True(resultEqual);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(5, 10, false)]
        [InlineData(10, 10, true)]
        [InlineData(255, 255, true)]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult(byte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, (sbyte)5, true)]
        [InlineData(10, (sbyte)10, true)]
        [InlineData(5, (sbyte)10, false)]
        public void GreaterThanOrEqual_WithSByte_ShouldReturnCorrectResult(byte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5, true)]
        [InlineData(10, 10, true)]
        [InlineData(5, 10, false)]
        public void GreaterThanOrEqual_WithInt_ShouldReturnCorrectResult(byte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5.0, true)]
        [InlineData(10, 10.0, true)]
        [InlineData(5, 10.0, false)]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult(byte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)90, 'A', true)]  // 90 >= 65
        [InlineData((byte)65, 'Z', false)] // 65 < 90
        [InlineData((byte)65, 'A', true)]  // 65 == 65
        public void GreaterThanOrEqual_WithChar_ReturnsExpectedResult(byte byteValue, char charValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(charValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, (short)5, true)]
        [InlineData((byte)10, (short)10, true)]
        [InlineData((byte)5, (short)10, false)]
        public void GreaterThanOrEqual_WithShort_ReturnsExpectedResult(byte byteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, (ushort)5, true)]
        [InlineData((byte)10, (ushort)10, true)]
        [InlineData((byte)5, (ushort)10, false)]
        public void GreaterThanOrEqual_WithUShort_ReturnsExpectedResult(byte byteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5U, true)]
        [InlineData((byte)10, 10U, true)]
        [InlineData((byte)5, 10U, false)]
        public void GreaterThanOrEqual_WithUInt_ReturnsExpectedResult(byte byteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5L, true)]
        [InlineData((byte)10, 10L, true)]
        [InlineData((byte)5, 10L, false)]
        public void GreaterThanOrEqual_WithLong_ReturnsExpectedResult(byte byteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5UL, true)]
        [InlineData((byte)10, 10UL, true)]
        [InlineData((byte)5, 10UL, false)]
        public void GreaterThanOrEqual_WithULong_ReturnsExpectedResult(byte byteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((byte)10, 5.0f, true)]
        [InlineData((byte)10, 10.0f, true)]
        [InlineData((byte)5, 10.0f, false)]
        public void GreaterThanOrEqual_WithFloat_ReturnsExpectedResult(byte byteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new ByteLiteral(byteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new ByteLiteral(10);

            // Act
            bool resultGreater = literal.GreaterThanOrEqual(5m);
            bool resultLess = literal.GreaterThanOrEqual(15m);
            bool resultEqual = literal.GreaterThanOrEqual(10m);

            // Assert
            Assert.True(resultGreater);
            Assert.False(resultLess);
            Assert.True(resultEqual);
        }
        #endregion

        #region Edge Cases
        [Fact]
        public void ByteLiteral_WithMinValue_ShouldWork()
        {
            // Arrange
            byte minValue = byte.MinValue;
            var literal = Literal.MakeLiteral(typeof(byte), minValue);

            // Act & Assert
            Assert.Equal(minValue, literal.Value);
        }

        [Fact]
        public void ByteLiteral_WithMaxValue_ShouldWork()
        {
            // Arrange
            byte maxValue = byte.MaxValue;
            var literal = Literal.MakeLiteral(typeof(byte), maxValue);

            // Act & Assert
            Assert.Equal(maxValue, literal.Value);
        }

        [Fact]
        public void Comparison_WithDecimal_ShouldWork()
        {
            // Arrange
            byte left = 100;
            decimal right = 100.5m;
            var leftLiteral = Literal.MakeLiteral(typeof(byte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var lessThan = leftLiteral.LessThan(rightLiteral);
            var greaterThan = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.True(lessThan);
            Assert.False(greaterThan);
        }

        [Fact]
        public void EdgeCase_MaxValue_ComparisonsWork()
        {
            // Arrange
            var maxLiteral = new ByteLiteral(byte.MaxValue);
            var normalLiteral = new ByteLiteral(100);

            // Act & Assert
            Assert.True(maxLiteral.GreaterThan(normalLiteral));
            Assert.False(maxLiteral.LessThan(normalLiteral));
            Assert.True(maxLiteral.GreaterThanOrEqual(normalLiteral));
            Assert.False(maxLiteral.LessThanOrEqual(normalLiteral));
            Assert.False(maxLiteral.Equal(normalLiteral));
        }

        [Fact]
        public void EdgeCase_MinValue_ComparisonsWork()
        {
            // Arrange
            var minLiteral = new ByteLiteral(byte.MinValue);
            var normalLiteral = new ByteLiteral(50);

            // Act & Assert
            Assert.False(minLiteral.GreaterThan(normalLiteral));
            Assert.True(minLiteral.LessThan(normalLiteral));
            Assert.False(minLiteral.GreaterThanOrEqual(normalLiteral));
            Assert.True(minLiteral.LessThanOrEqual(normalLiteral));
            Assert.False(minLiteral.Equal(normalLiteral));
        }

        [Fact]
        public void EdgeCase_ByteValue_ComparesWithNegativeSByte()
        {
            // Arrange
            var byteLiteral = new ByteLiteral(10);

            // Act
            bool equalResult = byteLiteral.Equal((sbyte)-1);
            bool lessThanResult = byteLiteral.LessThan((sbyte)-1);
            bool greaterThanResult = byteLiteral.GreaterThan((sbyte)-1);

            // Assert
            Assert.False(equalResult);
            Assert.False(lessThanResult);
            Assert.True(greaterThanResult);
        }

        [Fact]
        public void EdgeCase_EqualValues_AllOperators()
        {
            // Arrange
            var literal1 = new ByteLiteral(42);
            var literal2 = new ByteLiteral(42);

            // Act & Assert
            Assert.True(literal1.Equal(literal2));
            Assert.False(literal1.LessThan(literal2));
            Assert.False(literal1.GreaterThan(literal2));
            Assert.True(literal1.LessThanOrEqual(literal2));
            Assert.True(literal1.GreaterThanOrEqual(literal2));
        }
        #endregion
    }
}