using System.Globalization;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class StringArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_WithString_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new StringArithmeticLiteral("Hello");

            // Assert
            Assert.Equal("Hello", literal.Value);
            Assert.Equal(typeof(string), literal.m_type);
        }

        [Fact]
        public void Constructor_WithEmptyString_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new StringArithmeticLiteral("");

            // Assert
            Assert.Equal("", literal.Value);
            Assert.Equal(typeof(string), literal.m_type);
        }

        [Fact]
        public void Value_ReturnsString()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");

            // Act
            var value = literal.Value;

            // Assert
            Assert.IsType<string>(value);
            Assert.Equal("Test", value);
        }
        #endregion

        #region Add Tests with No Parameter
        [Fact]
        public void Add_WithNoParameter_ReturnsOriginalString()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Hello");

            // Act
            var result = literal.Add();

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void Add_WithNoParameter_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("");

            // Act
            var result = literal.Add();

            // Assert
            Assert.Equal("", result);
        }
        #endregion

        #region Add Tests with Primitive Types
        [Fact]
        public void Add_WithCharParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" World");

            // Act
            var result = literal.Add('A');

            // Assert
            Assert.Equal("A World", result);
        }

        [Fact]
        public void Add_WithUShortParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" items");

            // Act
            var result = literal.Add((ushort)42);

            // Assert
            Assert.Equal("42 items", result);
        }

        [Fact]
        public void Add_WithIntParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" degrees");

            // Act
            var result = literal.Add(100);

            // Assert
            Assert.Equal("100 degrees", result);
        }

        [Fact]
        public void Add_WithNegativeIntParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" degrees");

            // Act
            var result = literal.Add(-25);

            // Assert
            Assert.Equal("-25 degrees", result);
        }

        [Fact]
        public void Add_WithUIntParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" bytes");

            // Act
            var result = literal.Add(1024u);

            // Assert
            Assert.Equal("1024 bytes", result);
        }

        [Fact]
        public void Add_WithLongParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" milliseconds");

            // Act
            var result = literal.Add(9876543210L);

            // Assert
            Assert.Equal("9876543210 milliseconds", result);
        }

        [Fact]
        public void Add_WithULongParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" ticks");

            // Act
            var result = literal.Add(18446744073709551615UL);

            // Assert
            Assert.Equal("18446744073709551615 ticks", result);
        }

        [Fact]
        public void Add_WithFloatParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" meters");

            // Act
            var result = literal.Add(3.14f);

            // Assert
            Assert.Equal(3.14f.ToString(CultureInfo.CurrentCulture) + " meters", result);
        }

        [Fact]
        public void Add_WithDoubleParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" seconds");

            // Act
            var result = literal.Add(2.718281828);

            // Assert
            Assert.Equal(2.718281828.ToString(CultureInfo.CurrentCulture) + " seconds", result);
        }

        [Fact]
        public void Add_WithDecimalParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" dollars");

            // Act
            var result = literal.Add(99.99m);

            // Assert
            Assert.Equal("99.99 dollars", result);
        }

        [Fact]
        public void Add_WithBoolParameter_True_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(": ");

            // Act
            var result = literal.Add(true);

            // Assert
            Assert.Equal("True: ", result);
        }

        [Fact]
        public void Add_WithBoolParameter_False_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(": ");

            // Act
            var result = literal.Add(false);

            // Assert
            Assert.Equal("False: ", result);
        }

        [Fact]
        public void Add_WithStringParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" World");

            // Act
            var result = literal.Add("Hello");

            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void Add_WithEmptyStringParameter_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");

            // Act
            var result = literal.Add("");

            // Assert
            Assert.Equal("Test", result);
        }
        #endregion

        #region Add Tests with ArithmeticLiteral Types
        [Fact]
        public void Add_WithCharArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral(" End");
            var charLiteral = new CharArithmeticLiteral('X');

            // Act
            var result = stringLiteral.Add(charLiteral);

            // Assert
            Assert.Equal(" EndX", result);
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("items ");
            var ushortLiteral = new UShortArithmeticLiteral(25);

            // Act
            var result = stringLiteral.Add(ushortLiteral);

            // Assert
            Assert.Equal("items 25", result);
        }

        [Fact]
        public void Add_WithIntArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("points ");
            var intLiteral = new IntArithmeticLiteral(150);

            // Act
            var result = stringLiteral.Add(intLiteral);

            // Assert
            Assert.Equal("points 150", result);
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("units ");
            var uintLiteral = new UIntArithmeticLiteral(500u);

            // Act
            var result = stringLiteral.Add(uintLiteral);

            // Assert
            Assert.Equal("units 500", result);
        }

        [Fact]
        public void Add_WithLongArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("bytes ");
            var longLiteral = new LongArithmeticLiteral(1234567890L);

            // Act
            var result = stringLiteral.Add(longLiteral);

            // Assert
            Assert.Equal("bytes 1234567890", result);
        }

        [Fact]
        public void Add_WithULongArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("operations ");
            var ulongLiteral = new ULongArithmeticLiteral(9999999999UL);

            // Act
            var result = stringLiteral.Add(ulongLiteral);

            // Assert
            Assert.Equal("operations 9999999999", result);
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("km ");
            var floatLiteral = new FloatArithmeticLiteral(12.5f);

            // Act
            var result = stringLiteral.Add(floatLiteral);

            // Assert
            Assert.Equal($"km {12.5f.ToString(CultureInfo.CurrentCulture)}", result);
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("miles ");
            var doubleLiteral = new DoubleArithmeticLiteral(3.14159);

            // Act
            var result = stringLiteral.Add(doubleLiteral);

            // Assert
            Assert.Equal($"miles {3.14159.ToString(CultureInfo.CurrentCulture)}", result);
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("USD ");
            var decimalLiteral = new DecimalArithmeticLiteral(123.45m);

            // Act
            var result = stringLiteral.Add(decimalLiteral);

            // Assert
            Assert.Equal("USD 123.45", result);
        }

        [Fact]
        public void Add_WithBooleanArithmeticLiteral_True_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral(": ");
            var boolLiteral = new BooleanArithmeticLiteral(true);

            // Act
            var result = stringLiteral.Add(boolLiteral);

            // Assert
            Assert.Equal(": True", result);
        }

        [Fact]
        public void Add_WithBooleanArithmeticLiteral_False_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral(": ");
            var boolLiteral = new BooleanArithmeticLiteral(false);

            // Act
            var result = stringLiteral.Add(boolLiteral);

            // Assert
            Assert.Equal(": False", result);
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ConcatenatesCorrectly()
        {
            // Arrange
            var stringLiteral1 = new StringArithmeticLiteral("Hello");
            var stringLiteral2 = new StringArithmeticLiteral(" World");

            // Act
            var result = stringLiteral1.Add(stringLiteral2);

            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void Add_WithNullArithmeticLiteral_ReturnsOriginalString()
        {
            // Arrange
            var stringLiteral = new StringArithmeticLiteral("Test");
            var nullLiteral = new NullArithmeticLiteral(typeof(string));

            // Act
            var result = stringLiteral.Add(nullLiteral);

            // Assert
            Assert.Equal(null, result);
        }
        #endregion

        #region Complex Concatenation Tests
        [Fact]
        public void Add_MultipleOperations_WorksCorrectly()
        {
            // Arrange
            var literal1 = new StringArithmeticLiteral("Hello");
            var literal2 = new StringArithmeticLiteral(" World");

            // Act
            var intermediate = literal1.Add(literal2);
            var literal3 = new StringArithmeticLiteral(intermediate.ToString());
            var result = literal3.Add("!");

            // Assert
            Assert.Equal("!Hello World", result);
        }

        [Fact]
        public void Add_WithSpecialCharacters_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("\n\t\r");

            // Act
            var result = literal.Add("Special");

            // Assert
            Assert.Equal("Special\n\t\r", result);
        }

        [Fact]
        public void Add_WithUnicodeCharacters_ConcatenatesCorrectly()
        {
            // Arrange
            var literal = new StringArithmeticLiteral(" 世界");

            // Act
            var result = literal.Add("Hello");

            // Assert
            Assert.Equal("Hello 世界", result);
        }
        #endregion

        #region Unsupported Operations Tests
        [Fact]
        public void Subtract_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(intLiteral));
        }

        [Fact]
        public void Multiply_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(intLiteral));
        }

        [Fact]
        public void Divide_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(intLiteral));
        }

        [Fact]
        public void Modulus_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(intLiteral));
        }

        [Fact]
        public void BitAnd_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(intLiteral));
        }

        [Fact]
        public void BitOr_WithAnyParameter_ThrowsException()
        {
            // Arrange
            var literal = new StringArithmeticLiteral("Test");
            var intLiteral = new IntArithmeticLiteral(5);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(intLiteral));
        }
        #endregion
    }
}