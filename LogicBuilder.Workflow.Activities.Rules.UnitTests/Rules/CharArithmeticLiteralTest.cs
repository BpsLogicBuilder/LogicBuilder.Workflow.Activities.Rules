namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class CharArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            char expected = 'A';

            // Act
            var literal = new CharArithmeticLiteral(expected);

            // Assert
            Assert.Equal(expected, literal.Value);
            Assert.Equal(typeof(char), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsCharValue()
        {
            // Arrange
            char expected = 'Z';
            var literal = new CharArithmeticLiteral(expected);

            // Act
            object value = literal.Value;

            // Assert
            Assert.IsType<char>(value);
            Assert.Equal(expected, value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('A');
            var intLiteral = new IntArithmeticLiteral(10);

            // Act
            object result = charLiteral.Add(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(75, result); // 'A' is 65 + 10 = 75
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('B');

            // Act
            object result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsIntSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('C');

            // Act
            object result = literal.Add(5);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(72, result); // 'C' is 67 + 5 = 72
        }

        [Fact]
        public void Add_WithLong_ReturnsLongSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('D');

            // Act
            object result = literal.Add(100L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(168L, result); // 'D' is 68 + 100 = 168
        }

        [Fact]
        public void Add_WithChar_ReturnsIntSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('A');

            // Act
            object result = literal.Add('B');

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(131, result); // 'A' (65) + 'B' (66) = 131
        }

        [Fact]
        public void Add_WithUShort_ReturnsIntSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('E');

            // Act
            object result = literal.Add((ushort)50);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(119, result); // 'E' is 69 + 50 = 119
        }

        [Fact]
        public void Add_WithUInt_ReturnsUIntSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('F');

            // Act
            object result = literal.Add(1000u);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(1070u, result); // 'F' is 70 + 1000 = 1070
        }

        [Fact]
        public void Add_WithULong_ReturnsULongSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('G');

            // Act
            object result = literal.Add(5000UL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(5071UL, result); // 'G' is 71 + 5000 = 5071
        }

        [Fact]
        public void Add_WithFloat_ReturnsFloatSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('H');

            // Act
            object result = literal.Add(10.5f);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(82.5f, result); // 'H' is 72 + 10.5 = 82.5
        }

        [Fact]
        public void Add_WithDouble_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('I');

            // Act
            object result = literal.Add(20.75);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(93.75, result); // 'I' is 73 + 20.75 = 93.75
        }

        [Fact]
        public void Add_WithDecimal_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('J');

            // Act
            object result = literal.Add(15.5m);

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(89.5m, result); // 'J' is 74 + 15.5 = 89.5
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('K');

            // Act
            object result = literal.Add("Hello");

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal("HelloK", result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('Z');
            var intLiteral = new IntArithmeticLiteral(10);

            // Act
            object result = charLiteral.Subtract(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(80, result); // 'Z' is 90 - 10 = 80
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('M');

            // Act
            object result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsIntDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('N');

            // Act
            object result = literal.Subtract(5);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(-73, result); // 5 - 'N' is 78 = -73
        }

        [Fact]
        public void Subtract_WithLong_ReturnsLongDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('O');

            // Act
            object result = literal.Subtract(10L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(-69L, result); // 10 - 'O' is 79 = -69
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsIntDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('P');

            // Act
            object result = literal.Subtract((ushort)20);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(-60, result); // 20 - 'P' is 80 = -60
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsFloatDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('S');

            // Act
            object result = literal.Subtract(3.5f);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(-79.5f, result); // 3.5 - 'S' is 83 = -79.5
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('T');

            // Act
            object result = literal.Subtract(4.25);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(-79.75, result); // 4.25 - 'T' is 84 = -79.75
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('U');

            // Act
            object result = literal.Subtract(5.5m);

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(-79.5m, result); // 5.5 - 'U' is 85 = -79.5
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('B');
            var intLiteral = new IntArithmeticLiteral(2);

            // Act
            object result = charLiteral.Multiply(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(132, result); // 'B' is 66 * 2 = 132
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('V');

            // Act
            object result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsIntProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('C');

            // Act
            object result = literal.Multiply(3);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(201, result); // 'C' is 67 * 3 = 201
        }

        [Fact]
        public void Multiply_WithLong_ReturnsLongProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('D');

            // Act
            object result = literal.Multiply(5L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(340L, result); // 'D' is 68 * 5 = 340
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsIntProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('E');

            // Act
            object result = literal.Multiply((ushort)4);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(276, result); // 'E' is 69 * 4 = 276
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsUIntProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('F');

            // Act
            object result = literal.Multiply(10u);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(700u, result); // 'F' is 70 * 10 = 700
        }

        [Fact]
        public void Multiply_WithULong_ReturnsULongProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('G');

            // Act
            object result = literal.Multiply(100UL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(7100UL, result); // 'G' is 71 * 100 = 7100
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsFloatProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('H');

            // Act
            object result = literal.Multiply(2.5f);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(180f, result); // 'H' is 72 * 2.5 = 180
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('I');

            // Act
            object result = literal.Multiply(1.5);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(109.5, result); // 'I' is 73 * 1.5 = 109.5
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('J');

            // Act
            object result = literal.Multiply(2.5m);

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(185m, result); // 'J' is 74 * 2.5 = 185
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('d');
            var intLiteral = new IntArithmeticLiteral(2);

            // Act
            object result = charLiteral.Divide(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(50, result); // 'd' is 100 / 2 = 50
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('W');

            // Act
            object result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsIntQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('P');

            // Act
            object result = literal.Divide(4);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result); // 4 / 'P' is 80 = 0
        }

        [Fact]
        public void Divide_WithLong_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('Z');

            // Act
            object result = literal.Divide(3L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(0L, result); // 3 / 'Z' is 90  = 0
        }

        [Fact]
        public void Divide_WithUShort_ReturnsIntQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('d');

            // Act
            object result = literal.Divide((ushort)5);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result); // 5/ 'd' is 100  = 0
        }

        [Fact]
        public void Divide_WithUInt_ReturnsUIntQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('x');

            // Act
            object result = literal.Divide(4u);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(0u, result); // 4 /'x' is 120  = 0
        }

        [Fact]
        public void Divide_WithULong_ReturnsULongQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('n');

            // Act
            object result = literal.Divide(2UL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(0UL, result); // 2 / 'n' is 110  = 0
        }

        [Fact]
        public void Divide_WithFloat_ReturnsFloatQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('P');

            // Act
            object result = literal.Divide(4.0f);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(0.0500000007f, result); // 4.0 / 'P' is 80  = 0.0500000007
        }

        [Fact]
        public void Divide_WithDouble_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('Z');

            // Act
            object result = literal.Divide(2.0);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(0.022222222222222223, result); // 2.0 / 'Z' is 90  = 0.022222222222222223
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('d');

            // Act
            object result = literal.Divide(4m);
            //ArithmeticLiteral.Op(ArithmeticLiteral); uses the opposite order of operation compared with ArithmeticLiteral.Op(C# primitive type)
            //e.g. ArithmeticLiteralA.Divide(ArithmeticLiteralB) produces ArithmeticLiteralA divided by B
            //    ArithmeticLiteralA.Divide(C# primitive type) produces C# primitive type divided by ArithmeticLiteralA

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(0.04m, result); // 4 / 'd' is 100  = 0.04
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('G');
            var intLiteral = new IntArithmeticLiteral(10);

            // Act
            object result = charLiteral.Modulus(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(1, result); // 'G' is 71 % 10 = 1
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('X');

            // Act
            object result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsIntRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('M');

            // Act
            object result = literal.Modulus(5);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(5, result); // 5  % 'M' is 77  = 5
        }

        [Fact]
        public void Modulus_WithLong_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('N');

            // Act
            object result = literal.Modulus(7L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(7L, result); //  7 % 'N' is 78 = 7
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsIntRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('O');

            // Act
            object result = literal.Modulus((ushort)8);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(8, result); // 8 % 'O' is 79  =8
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsUIntRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('P');

            // Act
            object result = literal.Modulus(9u);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(9u, result); // 9 % 'P' is 80 = 9
        }

        [Fact]
        public void Modulus_WithULong_ReturnsULongRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('Q');

            // Act
            object result = literal.Modulus(10UL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(10UL, result); // 10 % 'Q' is 81 = 10
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsFloatRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('R');

            // Act
            object result = literal.Modulus(7.0f);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(7.0f, result); // 7.0 % 'R' is 82  = 7.0
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('S');

            // Act
            object result = literal.Modulus(6.0);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(6.0, result); // 6.0 % 'S' is 83  = 6.0
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('T');

            // Act
            object result = literal.Modulus(11m);

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(11m, result); // 11 % 'T' is 84 = 11
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('A'); // 65 = 0x41
            var intLiteral = new IntArithmeticLiteral(0x0F);

            // Act
            object result = charLiteral.BitAnd(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(1, result); // 0x41 & 0x0F = 0x01
        }

        [Fact]
        public void BitAnd_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('Y');

            // Act
            object result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithInt_ReturnsIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('F'); // 70 = 0x46

            // Act
            object result = literal.BitAnd(0xFF);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(70, result); // 0x46 & 0xFF = 0x46
        }

        [Fact]
        public void BitAnd_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('G'); // 71 = 0x47

            // Act
            object result = literal.BitAnd(0xFFL);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(71L, result); // 0x47 & 0xFF = 0x47
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('H'); // 72 = 0x48

            // Act
            object result = literal.BitAnd((ushort)0x0F);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(8, result); // 0x48 & 0x0F = 0x08
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsUIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('I'); // 73 = 0x49

            // Act
            object result = literal.BitAnd(0xFFu);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(73u, result); // 0x49 & 0xFF = 0x49
        }

        [Fact]
        public void BitAnd_WithULong_ReturnsULongResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('J'); // 74 = 0x4A

            // Act
            object result = literal.BitAnd(0xFFUL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(74UL, result); // 0x4A & 0xFF = 0x4A
        }

        [Fact]
        public void BitAnd_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('K');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(1.5f));
        }

        [Fact]
        public void BitAnd_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('L');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(2.5));
        }

        [Fact]
        public void BitAnd_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('M');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(3.5m));
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithArithmeticLiteral_CallsOverloadedMethod()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('A'); // 65 = 0x41
            var intLiteral = new IntArithmeticLiteral(0x0E);

            // Act
            object result = charLiteral.BitOr(intLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(79, result); // 0x41 | 0x0E = 0x4F (79)
        }

        [Fact]
        public void BitOr_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('Z');

            // Act
            object result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithInt_ReturnsIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('B'); // 66 = 0x42

            // Act
            object result = literal.BitOr(0x01);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(67, result); // 0x42 | 0x01 = 0x43 (67)
        }

        [Fact]
        public void BitOr_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('C'); // 67 = 0x43

            // Act
            object result = literal.BitOr(0x10L);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(83L, result); // 0x43 | 0x10 = 0x53 (83)
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('D'); // 68 = 0x44

            // Act
            object result = literal.BitOr((ushort)0x03);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(71, result); // 0x44 | 0x03 = 0x47 (71)
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsUIntResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('E'); // 69 = 0x45

            // Act
            object result = literal.BitOr(0x02u);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(71u, result); // 0x45 | 0x02 = 0x47 (71)
        }

        [Fact]
        public void BitOr_WithULong_ReturnsULongResult()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('F'); // 70 = 0x46

            // Act
            object result = literal.BitOr(0x01UL);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(71UL, result); // 0x46 | 0x01 = 0x47 (71)
        }

        [Fact]
        public void BitOr_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('G');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(1.5f));
        }

        [Fact]
        public void BitOr_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('H');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(2.5));
        }

        [Fact]
        public void BitOr_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = new CharArithmeticLiteral('I');

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(3.5m));
        }
        #endregion

        #region Operations with Other ArithmeticLiteral Types
        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsLong()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('A');
            var longLiteral = new LongArithmeticLiteral(100L);

            // Act
            object result = charLiteral.Add(longLiteral);

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(165L, result); // 'A' (65) + 100 = 165
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsInt()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('B');
            var ushortLiteral = new UShortArithmeticLiteral(50);

            // Act
            object result = charLiteral.Add(ushortLiteral);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(116, result); // 'B' (66) + 50 = 116
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ReturnsUInt()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('C');
            var uintLiteral = new UIntArithmeticLiteral(200u);

            // Act
            object result = charLiteral.Add(uintLiteral);

            // Assert
            Assert.IsType<uint>(result);
            Assert.Equal(267u, result); // 'C' (67) + 200 = 267
        }

        [Fact]
        public void Add_WithULongArithmeticLiteral_ReturnsULong()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('D');
            var ulongLiteral = new ULongArithmeticLiteral(1000UL);

            // Act
            object result = charLiteral.Add(ulongLiteral);

            // Assert
            Assert.IsType<ulong>(result);
            Assert.Equal(1068UL, result); // 'D' (68) + 1000 = 1068
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ReturnsFloat()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('E');
            var floatLiteral = new FloatArithmeticLiteral(5.5f);

            // Act
            object result = charLiteral.Add(floatLiteral);

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(74.5f, result); // 'E' (69) + 5.5 = 74.5
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ReturnsDouble()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('F');
            var doubleLiteral = new DoubleArithmeticLiteral(10.25);

            // Act
            object result = charLiteral.Add(doubleLiteral);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(80.25, result); // 'F' (70) + 10.25 = 80.25
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ReturnsDecimal()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('G');
            var decimalLiteral = new DecimalArithmeticLiteral(15.75m);

            // Act
            object result = charLiteral.Add(decimalLiteral);

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(86.75m, result); // 'G' (71) + 15.75 = 86.75
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsString()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('H');
            var stringLiteral = new StringArithmeticLiteral("Test");

            // Act
            object result = charLiteral.Add(stringLiteral);
            //ArithmeticLiteral.Op(ArithmeticLiteral); uses the opposite order of operation compared with ArithmeticLiteral.Op(C# primitive type)

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal("HTest", result);
        }

        [Fact]
        public void Add_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('I');
            var nullLiteral = new NullArithmeticLiteral(typeof(int?));

            // Act
            object result = charLiteral.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithCharArithmeticLiteral_ReturnsInt()
        {
            // Arrange
            var charLiteral1 = new CharArithmeticLiteral('A'); // 65
            var charLiteral2 = new CharArithmeticLiteral('B'); // 66

            // Act
            object result = charLiteral1.Multiply(charLiteral2);

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(4290, result); // 65 * 66 = 4290
        }

        [Fact]
        public void BitAnd_WithBoolArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var charLiteral = new CharArithmeticLiteral('J');
            var boolLiteral = new BooleanArithmeticLiteral(true);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => charLiteral.BitAnd(boolLiteral));
        }
        #endregion
    }
}