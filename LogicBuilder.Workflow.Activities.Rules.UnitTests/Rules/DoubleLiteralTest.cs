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

        [Fact]
        public void Equal_WithNaN_ReturnsFalse()
        {
            var literal = Literal.MakeLiteral(typeof(double), double.NaN);
            var literal2 = Literal.MakeLiteral(typeof(double), double.NaN);

            var result = literal.Equal(literal2);

            Assert.False(result);
        }
        #endregion

        #region LessThan Tests
        [Theory]
        [InlineData(10.5, 20.5, true)]
        [InlineData(20.5, 10.5, false)]
        [InlineData(10.5, 10.5, false)]
        [InlineData(-10.5, -5.0, true)]
        public void LessThan_WithDouble_ShouldReturnCorrectResult(double left, double right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.5, (sbyte)20, true)]
        [InlineData(20.5, (sbyte)10, false)]
        [InlineData(10.0, (sbyte)10, false)]
        public void LessThan_WithSByte_ShouldReturnCorrectResult(double left, sbyte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50.0, (byte)100, true)]
        [InlineData(150.0, (byte)100, false)]
        [InlineData(100.0, (byte)100, false)]
        public void LessThan_WithByte_ShouldReturnCorrectResult(double left, byte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(64.0, 'A', true)]
        [InlineData(66.0, 'A', false)]
        [InlineData(65.0, 'A', false)]
        public void LessThan_WithChar_ShouldReturnCorrectResult(double left, char right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(500.0, (short)1000, true)]
        [InlineData(1500.0, (short)1000, false)]
        [InlineData(1000.0, (short)1000, false)]
        public void LessThan_WithShort_ShouldReturnCorrectResult(double left, short right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1000.0, (ushort)2000, true)]
        [InlineData(3000.0, (ushort)2000, false)]
        [InlineData(2000.0, (ushort)2000, false)]
        public void LessThan_WithUShort_ShouldReturnCorrectResult(double left, ushort right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(25000.0, 50000, true)]
        [InlineData(75000.0, 50000, false)]
        [InlineData(50000.0, 50000, false)]
        public void LessThan_WithInt_ShouldReturnCorrectResult(double left, int right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(30000.0, 60000u, true)]
        [InlineData(80000.0, 60000u, false)]
        [InlineData(60000.0, 60000u, false)]
        public void LessThan_WithUInt_ShouldReturnCorrectResult(double left, uint right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50000.0, 100000L, true)]
        [InlineData(150000.0, 100000L, false)]
        [InlineData(100000.0, 100000L, false)]
        public void LessThan_WithLong_ShouldReturnCorrectResult(double left, long right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100000.0, 200000UL, true)]
        [InlineData(250000.0, 200000UL, false)]
        [InlineData(200000.0, 200000UL, false)]
        public void LessThan_WithULong_ShouldReturnCorrectResult(double left, ulong right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1.5, 2.5f, true)]
        [InlineData(3.5, 2.5f, false)]
        [InlineData(2.5, 2.5f, false)]
        public void LessThan_WithFloat_ShouldReturnCorrectResult(double left, float right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            var result = leftLiteral.LessThan(rightLiteral);

            Assert.Equal(expected, result);
        }
        #endregion

        #region GreaterThan Tests
        [Theory]
        [InlineData(20.5, 10.5, true)]
        [InlineData(10.5, 20.5, false)]
        [InlineData(10.5, 10.5, false)]
        [InlineData(-5.0, -10.5, true)]
        public void GreaterThan_WithDouble_ShouldReturnCorrectResult(double left, double right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50.0, (sbyte)20, true)]
        [InlineData(10.0, (sbyte)20, false)]
        [InlineData(20.0, (sbyte)20, false)]
        public void GreaterThan_WithSByte_ShouldReturnCorrectResult(double left, sbyte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(150.0, (byte)100, true)]
        [InlineData(50.0, (byte)100, false)]
        [InlineData(100.0, (byte)100, false)]
        public void GreaterThan_WithByte_ShouldReturnCorrectResult(double left, byte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(70.0, 'A', true)]
        [InlineData(60.0, 'A', false)]
        [InlineData(65.0, 'A', false)]
        public void GreaterThan_WithChar_ShouldReturnCorrectResult(double left, char right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2000.0, (short)1000, true)]
        [InlineData(500.0, (short)1000, false)]
        [InlineData(1000.0, (short)1000, false)]
        public void GreaterThan_WithShort_ShouldReturnCorrectResult(double left, short right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3000.0, (ushort)2000, true)]
        [InlineData(1000.0, (ushort)2000, false)]
        [InlineData(2000.0, (ushort)2000, false)]
        public void GreaterThan_WithUShort_ShouldReturnCorrectResult(double left, ushort right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(75000.0, 50000, true)]
        [InlineData(25000.0, 50000, false)]
        [InlineData(50000.0, 50000, false)]
        public void GreaterThan_WithInt_ShouldReturnCorrectResult(double left, int right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(80000.0, 60000u, true)]
        [InlineData(30000.0, 60000u, false)]
        [InlineData(60000.0, 60000u, false)]
        public void GreaterThan_WithUInt_ShouldReturnCorrectResult(double left, uint right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(150000.0, 100000L, true)]
        [InlineData(50000.0, 100000L, false)]
        [InlineData(100000.0, 100000L, false)]
        public void GreaterThan_WithLong_ShouldReturnCorrectResult(double left, long right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(250000.0, 200000UL, true)]
        [InlineData(100000.0, 200000UL, false)]
        [InlineData(200000.0, 200000UL, false)]
        public void GreaterThan_WithULong_ShouldReturnCorrectResult(double left, ulong right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5.5, 2.5f, true)]
        [InlineData(1.5, 2.5f, false)]
        [InlineData(2.5, 2.5f, false)]
        public void GreaterThan_WithFloat_ShouldReturnCorrectResult(double left, float right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            var result = leftLiteral.GreaterThan(rightLiteral);

            Assert.Equal(expected, result);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Theory]
        [InlineData(10.5, 20.5, true)]
        [InlineData(20.5, 10.5, false)]
        [InlineData(15.5, 15.5, true)]
        [InlineData(-10.5, -5.0, true)]
        public void LessThanOrEqual_WithDouble_ShouldReturnCorrectResult(double left, double right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10.0, (sbyte)20, true)]
        [InlineData(20.0, (sbyte)20, true)]
        [InlineData(30.0, (sbyte)20, false)]
        public void LessThanOrEqual_WithSByte_ShouldReturnCorrectResult(double left, sbyte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50.0, (byte)100, true)]
        [InlineData(100.0, (byte)100, true)]
        [InlineData(150.0, (byte)100, false)]
        public void LessThanOrEqual_WithByte_ShouldReturnCorrectResult(double left, byte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65.0, 'A', true)]
        [InlineData(60.0, 'A', true)]
        [InlineData(70.0, 'A', false)]
        public void LessThanOrEqual_WithChar_ShouldReturnCorrectResult(double left, char right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(500.0, (short)1000, true)]
        [InlineData(1000.0, (short)1000, true)]
        [InlineData(1500.0, (short)1000, false)]
        public void LessThanOrEqual_WithShort_ShouldReturnCorrectResult(double left, short right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1000.0, (ushort)2000, true)]
        [InlineData(2000.0, (ushort)2000, true)]
        [InlineData(3000.0, (ushort)2000, false)]
        public void LessThanOrEqual_WithUShort_ShouldReturnCorrectResult(double left, ushort right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(25000.0, 50000, true)]
        [InlineData(50000.0, 50000, true)]
        [InlineData(75000.0, 50000, false)]
        public void LessThanOrEqual_WithInt_ShouldReturnCorrectResult(double left, int right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(30000.0, 60000u, true)]
        [InlineData(60000.0, 60000u, true)]
        [InlineData(80000.0, 60000u, false)]
        public void LessThanOrEqual_WithUInt_ShouldReturnCorrectResult(double left, uint right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50000.0, 100000L, true)]
        [InlineData(100000.0, 100000L, true)]
        [InlineData(150000.0, 100000L, false)]
        public void LessThanOrEqual_WithLong_ShouldReturnCorrectResult(double left, long right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100000.0, 200000UL, true)]
        [InlineData(200000.0, 200000UL, true)]
        [InlineData(250000.0, 200000UL, false)]
        public void LessThanOrEqual_WithULong_ShouldReturnCorrectResult(double left, ulong right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1.5, 2.5f, true)]
        [InlineData(2.5, 2.5f, true)]
        [InlineData(3.5, 2.5f, false)]
        public void LessThanOrEqual_WithFloat_ShouldReturnCorrectResult(double left, float right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            var result = leftLiteral.LessThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Theory]
        [InlineData(20.5, 10.5, true)]
        [InlineData(10.5, 20.5, false)]
        [InlineData(15.5, 15.5, true)]
        [InlineData(-5.0, -10.5, true)]
        public void GreaterThanOrEqual_WithDouble_ShouldReturnCorrectResult(double left, double right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(double), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(50.0, (sbyte)20, true)]
        [InlineData(20.0, (sbyte)20, true)]
        [InlineData(10.0, (sbyte)20, false)]
        public void GreaterThanOrEqual_WithSByte_ShouldReturnCorrectResult(double left, sbyte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(sbyte), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(150.0, (byte)100, true)]
        [InlineData(100.0, (byte)100, true)]
        [InlineData(50.0, (byte)100, false)]
        public void GreaterThanOrEqual_WithByte_ShouldReturnCorrectResult(double left, byte right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(byte), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65.0, 'A', true)]
        [InlineData(70.0, 'A', true)]
        [InlineData(60.0, 'A', false)]
        public void GreaterThanOrEqual_WithChar_ShouldReturnCorrectResult(double left, char right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(char), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2000.0, (short)1000, true)]
        [InlineData(1000.0, (short)1000, true)]
        [InlineData(500.0, (short)1000, false)]
        public void GreaterThanOrEqual_WithShort_ShouldReturnCorrectResult(double left, short right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(short), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3000.0, (ushort)2000, true)]
        [InlineData(2000.0, (ushort)2000, true)]
        [InlineData(1000.0, (ushort)2000, false)]
        public void GreaterThanOrEqual_WithUShort_ShouldReturnCorrectResult(double left, ushort right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ushort), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(75000.0, 50000, true)]
        [InlineData(50000.0, 50000, true)]
        [InlineData(25000.0, 50000, false)]
        public void GreaterThanOrEqual_WithInt_ShouldReturnCorrectResult(double left, int right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(int), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(80000.0, 60000u, true)]
        [InlineData(60000.0, 60000u, true)]
        [InlineData(30000.0, 60000u, false)]
        public void GreaterThanOrEqual_WithUInt_ShouldReturnCorrectResult(double left, uint right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(uint), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(150000.0, 100000L, true)]
        [InlineData(100000.0, 100000L, true)]
        [InlineData(50000.0, 100000L, false)]
        public void GreaterThanOrEqual_WithLong_ShouldReturnCorrectResult(double left, long right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(long), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(250000.0, 200000UL, true)]
        [InlineData(200000.0, 200000UL, true)]
        [InlineData(100000.0, 200000UL, false)]
        public void GreaterThanOrEqual_WithULong_ShouldReturnCorrectResult(double left, ulong right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(ulong), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5.5, 2.5f, true)]
        [InlineData(2.5, 2.5f, true)]
        [InlineData(1.5, 2.5f, false)]
        public void GreaterThanOrEqual_WithFloat_ShouldReturnCorrectResult(double left, float right, bool expected)
        {
            var leftLiteral = Literal.MakeLiteral(typeof(double), left);
            var rightLiteral = Literal.MakeLiteral(typeof(float), right);

            var result = leftLiteral.GreaterThanOrEqual(rightLiteral);

            Assert.Equal(expected, result);
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void Comparison_WithZero_WorksCorrectly()
        {
            var zero = Literal.MakeLiteral(typeof(double), 0.0);
            var positive = Literal.MakeLiteral(typeof(double), 1.0);
            var negative = Literal.MakeLiteral(typeof(double), -1.0);

            Assert.True(zero.LessThan(positive));
            Assert.True(zero.GreaterThan(negative));
            Assert.True(zero.Equal(Literal.MakeLiteral(typeof(double), 0.0)));
        }

        [Fact]
        public void Comparison_WithPositiveInfinity_WorksCorrectly()
        {
            var infinity = Literal.MakeLiteral(typeof(double), double.PositiveInfinity);
            var maxValue = Literal.MakeLiteral(typeof(double), double.MaxValue);

            Assert.True(infinity.GreaterThan(maxValue));
            Assert.False(infinity.LessThan(maxValue));
        }

        [Fact]
        public void Comparison_WithNegativeInfinity_WorksCorrectly()
        {
            var negInfinity = Literal.MakeLiteral(typeof(double), double.NegativeInfinity);
            var minValue = Literal.MakeLiteral(typeof(double), double.MinValue);

            Assert.True(negInfinity.LessThan(minValue));
            Assert.False(negInfinity.GreaterThan(minValue));
        }

        [Fact]
        public void Comparison_WithVerySmallDifference_WorksCorrectly()
        {
            var literal1 = Literal.MakeLiteral(typeof(double), 1.0);
            var literal2 = Literal.MakeLiteral(typeof(double), 1.0 + double.Epsilon);

            Assert.False(literal1.LessThan(literal2));
            Assert.True(literal1.Equal(literal2));
        }

        [Fact]
        public void Comparison_WithNaN_WorksCorrectly()
        {
            var nan = Literal.MakeLiteral(typeof(double), double.NaN);
            var value = Literal.MakeLiteral(typeof(double), 1.0);

            Assert.False(nan.Equal(value));
            Assert.False(nan.LessThan(value));
            Assert.False(nan.GreaterThan(value));
        }
        #endregion
    }
}
