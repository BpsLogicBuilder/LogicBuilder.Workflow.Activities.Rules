namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DoubleLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_ShouldInitializeValue()
        {
            double expectedValue = 42.5;

            var literal = Literal.MakeLiteral(typeof(double), expectedValue);

            Assert.NotNull(literal);
            Assert.Equal(expectedValue, literal.Value);
        }

        [Fact]
        public void Value_ShouldReturnDoubleType()
        {
            double value = 100.75;
            var literal = Literal.MakeLiteral(typeof(double), value);

            var result = literal.Value;

            Assert.IsType<double>(result);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(double.Epsilon)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Constructor_WithVariousValues_ShouldInitializeCorrectly(double value)
        {
            var literal = Literal.MakeLiteral(typeof(double), value);

            Assert.NotNull(literal);
            Assert.Equal(value, literal.Value);
        }
        #endregion

        #region Equal Tests
        [Theory]
        [InlineData(10.5, 10.5, true)]
        [InlineData(10.5, 20.3, false)]
        [InlineData(0.0, 0.0, true)]
        [InlineData(-5.7, -5.7, true)]
        [InlineData(-5.7, 5.7, false)]
        public void Equal_WithDouble_ShouldReturnCorrectResult(double left, double right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, (sbyte)10, true)]
        [InlineData(10.5, (sbyte)10, false)]
        [InlineData(-10.0, (sbyte)-10, true)]
        public void Equal_WithSByte_ShouldReturnCorrectResult(double left, sbyte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, (byte)10, true)]
        [InlineData(10.5, (byte)10, false)]
        [InlineData(255.0, (byte)255, true)]
        public void Equal_WithByte_ShouldReturnCorrectResult(double left, byte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, (short)10, true)]
        [InlineData(10.5, (short)10, false)]
        [InlineData(-100.0, (short)-100, true)]
        public void Equal_WithShort_ShouldReturnCorrectResult(double left, short right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, (ushort)10, true)]
        [InlineData(10.5, (ushort)10, false)]
        [InlineData(1000.0, (ushort)1000, true)]
        public void Equal_WithUShort_ShouldReturnCorrectResult(double left, ushort right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 10, true)]
        [InlineData(10.5, 10, false)]
        [InlineData(-1000.0, -1000, true)]
        public void Equal_WithInt_ShouldReturnCorrectResult(double left, int right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 10u, true)]
        [InlineData(10.5, 10u, false)]
        [InlineData(1000.0, 1000u, true)]
        public void Equal_WithUInt_ShouldReturnCorrectResult(double left, uint right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 10L, true)]
        [InlineData(10.5, 10L, false)]
        [InlineData(-1000.0, -1000L, true)]
        public void Equal_WithLong_ShouldReturnCorrectResult(double left, long right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 10UL, true)]
        [InlineData(10.5, 10UL, false)]
        [InlineData(1000.0, 1000UL, true)]
        public void Equal_WithULong_ShouldReturnCorrectResult(double left, ulong right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 10.0f, true)]
        [InlineData(10.5, 10.0f, false)]
        [InlineData(-100.0, -100.0f, true)]
        public void Equal_WithFloat_ShouldReturnCorrectResult(double left, float right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, 'A', false)]
        [InlineData(65.0, 'A', true)]
        [InlineData(66.0, 'B', true)]
        public void Equal_WithChar_ShouldReturnCorrectResult(double left, char right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            var result = leftLiteral.Equal(rightLiteral);

            Assert.Equal(expected, result);
        }
        #endregion
    }
}
