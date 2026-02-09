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
        #endregion
    }
}