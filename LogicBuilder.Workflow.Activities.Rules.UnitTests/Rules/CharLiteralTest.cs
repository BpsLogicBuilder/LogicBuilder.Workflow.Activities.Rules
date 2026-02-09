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
        [InlineData(' ', ' ', true)]
        [InlineData('\t', '\t', true)]
        [InlineData('\n', '\n', true)]
        public void Char_CaseSensitivity_ShouldBeHandledCorrectly(char left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(char), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

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