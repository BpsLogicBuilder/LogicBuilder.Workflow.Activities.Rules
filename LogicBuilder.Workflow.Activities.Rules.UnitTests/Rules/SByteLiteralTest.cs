namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class SByteLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_ShouldInitializeValue()
        {
            // Arrange
            sbyte expectedValue = 42;

            // Act
            var literal = Literal.MakeLiteral(typeof(sbyte), expectedValue);

            // Assert
            Assert.NotNull(literal);
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Value_ShouldReturnSByteType()
        {
            // Arrange
            sbyte value = 100;
            var literal = Literal.MakeLiteral(typeof(sbyte), value);

            // Act
            var result = literal.Value;

            // Assert
            Assert.IsType<sbyte>(result);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData((sbyte)10, (sbyte)10, true)]
        [InlineData((sbyte)10, (sbyte)20, false)]
        [InlineData((sbyte)0, (sbyte)0, true)]
        [InlineData((sbyte)127, (sbyte)127, true)]
        [InlineData((sbyte)-128, (sbyte)-128, true)]
        [InlineData((sbyte)-10, (sbyte)10, false)]
        public void Equal_WithSByte_ShouldReturnCorrectResult(sbyte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (byte)10, true)]
        [InlineData((sbyte)10, (byte)20, false)]
        [InlineData((sbyte)-1, (byte)255, false)]
        [InlineData((sbyte)127, (byte)127, true)]
        public void Equal_WithByte_ShouldReturnCorrectResult(sbyte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)65, 'A', true)]
        [InlineData((sbyte)66, 'A', false)]
        [InlineData((sbyte)0, '\0', true)]
        [InlineData((sbyte)-1, 'A', false)]
        public void Equal_WithChar_ShouldReturnCorrectResult(sbyte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)10, (short)20, false)]
        [InlineData((sbyte)-10, (short)-10, true)]
        [InlineData((sbyte)127, (short)127, true)]
        public void Equal_WithShort_ShouldReturnCorrectResult(sbyte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)20, false)]
        [InlineData((sbyte)-1, (ushort)100, false)]
        [InlineData((sbyte)127, (ushort)127, true)]
        public void Equal_WithUShort_ShouldReturnCorrectResult(sbyte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)10, 20, false)]
        [InlineData((sbyte)-10, -10, true)]
        [InlineData((sbyte)0, 0, true)]
        public void Equal_WithInt_ShouldReturnCorrectResult(sbyte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10u, true)]
        [InlineData((sbyte)10, 20u, false)]
        [InlineData((sbyte)-1, 10u, false)]
        [InlineData((sbyte)127, 127u, true)]
        public void Equal_WithUInt_ShouldReturnCorrectResult(sbyte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)10, 20L, false)]
        [InlineData((sbyte)-10, -10L, true)]
        public void Equal_WithLong_ShouldReturnCorrectResult(sbyte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10UL, true)]
        [InlineData((sbyte)10, 20UL, false)]
        [InlineData((sbyte)-1, 10UL, false)]
        [InlineData((sbyte)127, 127UL, true)]
        public void Equal_WithULong_ShouldReturnCorrectResult(sbyte left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)10, 10.5f, false)]
        [InlineData((sbyte)-10, -10.0f, true)]
        [InlineData((sbyte)10, 20.0f, false)]
        public void Equal_WithFloat_ShouldReturnCorrectResult(sbyte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)10, 10.5, false)]
        [InlineData((sbyte)-10, -10.0, true)]
        [InlineData((sbyte)10, 20.0, false)]
        public void Equal_WithDouble_ShouldReturnCorrectResult(sbyte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
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
            sbyte left = 10;
            decimal right = 10m;
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)10, (short)20, false)]
        [InlineData((sbyte)-10, (short)-10, true)]
        public void Equal_WithShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)20, false)]
        [InlineData((sbyte)-1, (ushort)100, false)]
        public void Equal_WithUShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)10, 20, false)]
        [InlineData((sbyte)-10, -10, true)]
        public void Equal_WithInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10U, true)]
        [InlineData((sbyte)10, 20U, false)]
        [InlineData((sbyte)-1, 10U, false)]
        public void Equal_WithUInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)10, 20L, false)]
        [InlineData((sbyte)-10, -10L, true)]
        public void Equal_WithLong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10UL, true)]
        [InlineData((sbyte)10, 20UL, false)]
        [InlineData((sbyte)-1, 10UL, false)]
        public void Equal_WithULong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ulong ulongValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(ulongValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)10, 20.0f, false)]
        [InlineData((sbyte)-10, -10.0f, true)]
        public void Equal_WithFloat_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)10, 20.0, false)]
        [InlineData((sbyte)-10, -10.0, true)]
        public void Equal_WithDouble_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.Equal(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equal_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new SByteLiteral(10);

            // Act
            bool resultEqual = literal.Equal(10m);
            bool resultNotEqual = literal.Equal(20m);
            bool resultNegative = literal.Equal(-10m);

            // Assert
            Assert.True(resultEqual);
            Assert.False(resultNotEqual);
            Assert.False(resultNegative);
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData((sbyte)5, (sbyte)10, true)]
        [InlineData((sbyte)10, (sbyte)5, false)]
        [InlineData((sbyte)10, (sbyte)10, false)]
        [InlineData((sbyte)-10, (sbyte)10, true)]
        [InlineData((sbyte)-128, (sbyte)127, true)]
        public void LessThan_WithSByte_ShouldReturnCorrectResult(sbyte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (byte)10, true)]
        [InlineData((sbyte)10, (byte)5, false)]
        [InlineData((sbyte)-1, (byte)0, true)]
        public void LessThan_WithByte_ShouldReturnCorrectResult(sbyte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 'Z', true)]
        [InlineData((sbyte)100, 'A', false)]
        [InlineData((sbyte)-1, 'A', true)]
        public void LessThan_WithChar_ShouldReturnCorrectResult(sbyte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (short)10, true)]
        [InlineData((sbyte)10, (short)5, false)]
        [InlineData((sbyte)-10, (short)-5, true)]
        public void LessThan_WithShort_ShouldReturnCorrectResult(sbyte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)5, false)]
        [InlineData((sbyte)-1, (ushort)100, true)]
        public void LessThan_WithUShort_ShouldReturnCorrectResult(sbyte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10, true)]
        [InlineData((sbyte)10, 5, false)]
        [InlineData((sbyte)-10, -5, true)]
        public void LessThan_WithInt_ShouldReturnCorrectResult(sbyte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10u, true)]
        [InlineData((sbyte)10, 5u, false)]
        [InlineData((sbyte)-1, 10u, true)]
        public void LessThan_WithUInt_ShouldReturnCorrectResult(sbyte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10L, true)]
        [InlineData((sbyte)10, 5L, false)]
        [InlineData((sbyte)-10, -5L, true)]
        public void LessThan_WithLong_ShouldReturnCorrectResult(sbyte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0f, true)]
        [InlineData((sbyte)10, 5.0f, false)]
        [InlineData((sbyte)-10, -5.0f, true)]
        public void LessThan_WithFloat_ShouldReturnCorrectResult(sbyte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0, true)]
        [InlineData((sbyte)10, 5.0, false)]
        [InlineData((sbyte)-10, -5.0, true)]
        public void LessThan_WithDouble_ShouldReturnCorrectResult(sbyte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
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
            sbyte left = 5;
            decimal right = 10m;
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.LessThan(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((sbyte)5, (short)10, true)]
        [InlineData((sbyte)10, (short)5, false)]
        [InlineData((sbyte)-10, (short)-5, true)]
        public void LessThan_WithShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)5, false)]
        [InlineData((sbyte)-1, (ushort)100, true)]
        public void LessThan_WithUShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10, true)]
        [InlineData((sbyte)10, 5, false)]
        [InlineData((sbyte)-10, -5, true)]
        public void LessThan_WithInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10U, true)]
        [InlineData((sbyte)10, 5U, false)]
        [InlineData((sbyte)-1, 10U, true)]
        public void LessThan_WithUInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10L, true)]
        [InlineData((sbyte)10, 5L, false)]
        [InlineData((sbyte)-10, -5L, true)]
        public void LessThan_WithLong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0f, true)]
        [InlineData((sbyte)10, 5.0f, false)]
        [InlineData((sbyte)-10, -5.0f, true)]
        public void LessThan_WithFloat_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0, true)]
        [InlineData((sbyte)10, 5.0, false)]
        [InlineData((sbyte)-10, -5.0, true)]
        public void LessThan_WithDouble_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThan_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new SByteLiteral(5);

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
        [InlineData((sbyte)10, (sbyte)5, true)]
        [InlineData((sbyte)5, (sbyte)10, false)]
        [InlineData((sbyte)10, (sbyte)10, false)]
        [InlineData((sbyte)10, (sbyte)-10, true)]
        [InlineData((sbyte)127, (sbyte)-128, true)]
        public void GreaterThan_WithSByte_ShouldReturnCorrectResult(sbyte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (byte)5, true)]
        [InlineData((sbyte)5, (byte)10, false)]
        [InlineData((sbyte)-1, (byte)0, false)]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult(sbyte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)100, 'A', true)]
        [InlineData((sbyte)5, 'Z', false)]
        [InlineData((sbyte)-1, 'A', false)]
        public void GreaterThan_WithChar_ShouldReturnCorrectResult(sbyte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)5, true)]
        [InlineData((sbyte)5, (short)10, false)]
        [InlineData((sbyte)-5, (short)-10, true)]
        public void GreaterThan_WithShort_ShouldReturnCorrectResult(sbyte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)5, true)]
        [InlineData((sbyte)5, (ushort)10, false)]
        [InlineData((sbyte)-1, (ushort)100, false)]
        public void GreaterThan_WithUShort_ShouldReturnCorrectResult(sbyte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5, true)]
        [InlineData((sbyte)5, 10, false)]
        [InlineData((sbyte)-5, -10, true)]
        public void GreaterThan_WithInt_ShouldReturnCorrectResult(sbyte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5u, true)]
        [InlineData((sbyte)5, 10u, false)]
        [InlineData((sbyte)-1, 10u, false)]
        public void GreaterThan_WithUInt_ShouldReturnCorrectResult(sbyte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5L, true)]
        [InlineData((sbyte)5, 10L, false)]
        [InlineData((sbyte)-5, -10L, true)]
        public void GreaterThan_WithLong_ShouldReturnCorrectResult(sbyte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0f, true)]
        [InlineData((sbyte)5, 10.0f, false)]
        [InlineData((sbyte)-5, -10.0f, true)]
        public void GreaterThan_WithFloat_ShouldReturnCorrectResult(sbyte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0, true)]
        [InlineData((sbyte)5, 10.0, false)]
        [InlineData((sbyte)-5, -10.0, true)]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult(sbyte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
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
            sbyte left = 10;
            decimal right = 5m;
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.GreaterThan(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)5, true)]
        [InlineData((sbyte)5, (short)10, false)]
        [InlineData((sbyte)-5, (short)-10, true)]
        public void GreaterThan_WithShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)5, true)]
        [InlineData((sbyte)5, (ushort)10, false)]
        [InlineData((sbyte)-1, (ushort)100, false)]
        public void GreaterThan_WithUShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5, true)]
        [InlineData((sbyte)5, 10, false)]
        [InlineData((sbyte)-5, -10, true)]
        public void GreaterThan_WithInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5U, true)]
        [InlineData((sbyte)5, 10U, false)]
        [InlineData((sbyte)-1, 10U, false)]
        public void GreaterThan_WithUInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5L, true)]
        [InlineData((sbyte)5, 10L, false)]
        [InlineData((sbyte)-5, -10L, true)]
        public void GreaterThan_WithLong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0f, true)]
        [InlineData((sbyte)5, 10.0f, false)]
        [InlineData((sbyte)-5, -10.0f, true)]
        public void GreaterThan_WithFloat_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0, true)]
        [InlineData((sbyte)5, 10.0, false)]
        [InlineData((sbyte)-5, -10.0, true)]
        public void GreaterThan_WithDouble_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThan(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThan_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new SByteLiteral(10);

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
        [InlineData((sbyte)5, (sbyte)10, true)]
        [InlineData((sbyte)10, (sbyte)5, false)]
        [InlineData((sbyte)10, (sbyte)10, true)]
        [InlineData((sbyte)-10, (sbyte)10, true)]
        [InlineData((sbyte)-128, (sbyte)127, true)]
        public void LessThanOrEqual_WithSByte_ShouldReturnCorrectResult(sbyte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (byte)10, true)]
        [InlineData((sbyte)10, (byte)10, true)]
        [InlineData((sbyte)10, (byte)5, false)]
        [InlineData((sbyte)-1, (byte)0, true)]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult(sbyte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 'Z', true)]
        [InlineData((sbyte)65, 'A', true)]
        [InlineData((sbyte)100, 'A', false)]
        public void LessThanOrEqual_WithChar_ShouldReturnCorrectResult(sbyte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (short)10, true)]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)10, (short)5, false)]
        [InlineData((sbyte)-10, (short)-5, true)]
        public void LessThanOrEqual_WithShort_ShouldReturnCorrectResult(sbyte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)5, false)]
        [InlineData((sbyte)-1, (ushort)100, true)]
        public void LessThanOrEqual_WithUShort_ShouldReturnCorrectResult(sbyte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10, true)]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)10, 5, false)]
        [InlineData((sbyte)-10, -5, true)]
        public void LessThanOrEqual_WithInt_ShouldReturnCorrectResult(sbyte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10u, true)]
        [InlineData((sbyte)10, 10u, true)]
        [InlineData((sbyte)10, 5u, false)]
        [InlineData((sbyte)-1, 10u, true)]
        public void LessThanOrEqual_WithUInt_ShouldReturnCorrectResult(sbyte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10L, true)]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)10, 5L, false)]
        [InlineData((sbyte)-10, -5L, true)]
        public void LessThanOrEqual_WithLong_ShouldReturnCorrectResult(sbyte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0f, true)]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)10, 5.0f, false)]
        [InlineData((sbyte)-10, -5.0f, true)]
        public void LessThanOrEqual_WithFloat_ShouldReturnCorrectResult(sbyte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0, true)]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)10, 5.0, false)]
        [InlineData((sbyte)-10, -5.0, true)]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult(sbyte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
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
            sbyte left = 5;
            decimal right = 10m;
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((sbyte)5, (short)10, true)]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)10, (short)5, false)]
        public void LessThanOrEqual_WithShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)10, (ushort)5, false)]
        public void LessThanOrEqual_WithUShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10, true)]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)10, 5, false)]
        public void LessThanOrEqual_WithInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10U, true)]
        [InlineData((sbyte)10, 10U, true)]
        [InlineData((sbyte)10, 5U, false)]
        public void LessThanOrEqual_WithUInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10L, true)]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)10, 5L, false)]
        public void LessThanOrEqual_WithLong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0f, true)]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)10, 5.0f, false)]
        public void LessThanOrEqual_WithFloat_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)5, 10.0, true)]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)10, 5.0, false)]
        public void LessThanOrEqual_WithDouble_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.LessThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LessThanOrEqual_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new SByteLiteral(5);

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
        [InlineData((sbyte)10, (sbyte)5, true)]
        [InlineData((sbyte)10, (sbyte)10, true)]
        [InlineData((sbyte)5, (sbyte)10, false)]
        [InlineData((sbyte)10, (sbyte)-10, true)]
        [InlineData((sbyte)127, (sbyte)-128, true)]
        public void GreaterThanOrEqual_WithSByte_ShouldReturnCorrectResult(sbyte left, sbyte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (byte)5, true)]
        [InlineData((sbyte)10, (byte)10, true)]
        [InlineData((sbyte)5, (byte)10, false)]
        [InlineData((sbyte)-1, (byte)0, false)]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult(sbyte left, byte right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)100, 'A', true)]
        [InlineData((sbyte)65, 'A', true)]
        [InlineData((sbyte)5, 'Z', false)]
        public void GreaterThanOrEqual_WithChar_ShouldReturnCorrectResult(sbyte left, char right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)5, true)]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)5, (short)10, false)]
        [InlineData((sbyte)-5, (short)-10, true)]
        public void GreaterThanOrEqual_WithShort_ShouldReturnCorrectResult(sbyte left, short right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)5, true)]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)5, (ushort)10, false)]
        [InlineData((sbyte)-1, (ushort)100, false)]
        public void GreaterThanOrEqual_WithUShort_ShouldReturnCorrectResult(sbyte left, ushort right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5, true)]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)5, 10, false)]
        [InlineData((sbyte)-5, -10, true)]
        public void GreaterThanOrEqual_WithInt_ShouldReturnCorrectResult(sbyte left, int right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5u, true)]
        [InlineData((sbyte)10, 10u, true)]
        [InlineData((sbyte)5, 10u, false)]
        [InlineData((sbyte)-1, 10u, false)]
        public void GreaterThanOrEqual_WithUInt_ShouldReturnCorrectResult(sbyte left, uint right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5L, true)]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)5, 10L, false)]
        [InlineData((sbyte)-5, -10L, true)]
        public void GreaterThanOrEqual_WithLong_ShouldReturnCorrectResult(sbyte left, long right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0f, true)]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)5, 10.0f, false)]
        [InlineData((sbyte)-5, -10.0f, true)]
        public void GreaterThanOrEqual_WithFloat_ShouldReturnCorrectResult(sbyte left, float right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0, true)]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)5, 10.0, false)]
        [InlineData((sbyte)-5, -10.0, true)]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult(sbyte left, double right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
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
            sbyte left = 10;
            decimal right = 5m;
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(decimal), right);

            // Act
            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData((sbyte)10, (short)5, true)]
        [InlineData((sbyte)10, (short)10, true)]
        [InlineData((sbyte)5, (short)10, false)]
        public void GreaterThanOrEqual_WithShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, short shortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(shortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, (ushort)5, true)]
        [InlineData((sbyte)10, (ushort)10, true)]
        [InlineData((sbyte)5, (ushort)10, false)]
        public void GreaterThanOrEqual_WithUShort_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, ushort ushortValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(ushortValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5, true)]
        [InlineData((sbyte)10, 10, true)]
        [InlineData((sbyte)5, 10, false)]
        public void GreaterThanOrEqual_WithInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, int intValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(intValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5U, true)]
        [InlineData((sbyte)10, 10U, true)]
        [InlineData((sbyte)5, 10U, false)]
        public void GreaterThanOrEqual_WithUInt_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, uint uintValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(uintValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5L, true)]
        [InlineData((sbyte)10, 10L, true)]
        [InlineData((sbyte)5, 10L, false)]
        public void GreaterThanOrEqual_WithLong_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, long longValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(longValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0f, true)]
        [InlineData((sbyte)10, 10.0f, true)]
        [InlineData((sbyte)5, 10.0f, false)]
        public void GreaterThanOrEqual_WithFloat_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, float floatValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(floatValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((sbyte)10, 5.0, true)]
        [InlineData((sbyte)10, 10.0, true)]
        [InlineData((sbyte)5, 10.0, false)]
        public void GreaterThanOrEqual_WithDouble_DirectCall_ReturnsExpectedResult(sbyte sbyteValue, double doubleValue, bool expected)
        {
            // Arrange
            var literal = new SByteLiteral(sbyteValue);

            // Act
            bool result = literal.GreaterThanOrEqual(doubleValue);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GreaterThanOrEqual_WithDecimal_DirectCall_ReturnsExpectedResult()
        {
            // Arrange
            var literal = new SByteLiteral(10);

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
        public void SByte_MinValue_ShouldHandleCorrectly()
        {
            // Arrange
            sbyte minValue = sbyte.MinValue;
            var literal = Literal.MakeLiteral(typeof(sbyte), minValue);

            // Act & Assert
            Assert.Equal(minValue, literal.Value);
            Assert.Equal(-128, minValue);
        }

        [Fact]
        public void SByte_MaxValue_ShouldHandleCorrectly()
        {
            // Arrange
            sbyte maxValue = sbyte.MaxValue;
            var literal = Literal.MakeLiteral(typeof(sbyte), maxValue);

            // Act & Assert
            Assert.Equal(maxValue, literal.Value);
            Assert.Equal(127, maxValue);
        }

        [Fact]
        public void SByte_Zero_ShouldHandleCorrectly()
        {
            // Arrange
            sbyte zero = 0;
            var literal = Literal.MakeLiteral(typeof(sbyte), zero);

            // Act & Assert
            Assert.Equal(zero, literal.Value);
        }

        [Theory]
        [InlineData((sbyte)-1, 10UL, false)]
        [InlineData((sbyte)0, 0UL, true)]
        [InlineData((sbyte)10, 10UL, true)]
        public void Equal_WithULong_NegativeAndPositive_ShouldHandleCorrectly(sbyte left, ulong right, bool expected)
        {
            // Arrange
            var leftLiteral = Literal.MakeLiteral(typeof(sbyte), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            // Act
            var result = leftLiteral.Equal(rightLiteral);

            // Assert
            Assert.Equal(expected, result);
        }
        #endregion
    }
}