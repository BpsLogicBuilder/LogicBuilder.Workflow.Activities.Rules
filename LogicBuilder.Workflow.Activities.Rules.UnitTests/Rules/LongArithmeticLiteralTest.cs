using System;
using System.CodeDom;
using LogicBuilder.Workflow.Activities.Rules;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LongArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        [Fact]
        public void Constructor_SetsValueAndType()
        {
            // Arrange & Act
            var literal = new LongArithmeticLiteral(42L);

            // Assert
            Assert.Equal(42L, literal.Value);
            Assert.Equal(typeof(long), literal.m_type);
        }

        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongArithmeticLiteral(-100L);

            // Assert
            Assert.Equal(-100L, literal.Value);
        }

        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongArithmeticLiteral(0L);

            // Assert
            Assert.Equal(0L, literal.Value);
        }

        [Fact]
        public void Constructor_WithLargeValue_SetsValueCorrectly()
        {
            // Arrange & Act
            var literal = new LongArithmeticLiteral(long.MaxValue);

            // Assert
            Assert.Equal(long.MaxValue, literal.Value);
        }
        #endregion

        #region Add Tests
        [Fact]
        public void Add_WithArithmeticLiteral_CallsOtherLiteralAddMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(10L);
            var literal2 = new IntArithmeticLiteral(20);

            // Act
            var result = literal1.Add(literal2);

            // Assert
            Assert.Equal(30L, result);
        }

        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithInt_ReturnsLongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(15L);

            // Act
            var result = literal.Add(25);

            // Assert
            Assert.Equal(40L, result);
        }

        [Fact]
        public void Add_WithLong_ReturnsLongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(100L);

            // Act
            var result = literal.Add(200L);

            // Assert
            Assert.Equal(300L, result);
        }

        [Fact]
        public void Add_WithChar_ReturnsLongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add('A'); // 'A' = 65

            // Assert
            Assert.Equal(75L, result);
        }

        [Fact]
        public void Add_WithUShort_ReturnsLongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(50L);

            // Act
            var result = literal.Add((ushort)100);

            // Assert
            Assert.Equal(150L, result);
        }

        [Fact]
        public void Add_WithUInt_ReturnsLongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add(20U);

            // Assert
            Assert.Equal(30L, result);
        }

        [Fact]
        public void Add_WithULong_WhenPositiveLong_ReturnsULongSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add(100UL);

            // Assert
            Assert.Equal(110UL, result);
        }

        [Fact]
        public void Add_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-10L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(100UL));
        }

        [Fact]
        public void Add_WithFloat_ReturnsFloatSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add(20.5f);

            // Assert
            Assert.Equal(30.5f, result);
        }

        [Fact]
        public void Add_WithDouble_ReturnsDoubleSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add(20.75);

            // Assert
            Assert.Equal(30.75, result);
        }

        [Fact]
        public void Add_WithDecimal_ReturnsDecimalSum()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Add(20.5m);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(42L);

            // Act
            var result = literal.Add("Answer: ");

            // Assert
            Assert.Equal("Answer: 42", result);
        }
        #endregion

        #region Subtract Tests
        [Fact]
        public void Subtract_WithArithmeticLiteral_CallsOtherLiteralSubtractMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(10L);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Subtract(literal2);

            // Assert
            Assert.Equal(5L, result); // 10 - 5 = 5
        }

        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Subtract_WithInt_ReturnsLongDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(15L);

            // Act
            var result = literal.Subtract(25);

            // Assert
            Assert.Equal(10L, result); // 25 - 15 = 10
        }

        [Fact]
        public void Subtract_WithLong_ReturnsLongDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(100L);

            // Act
            var result = literal.Subtract(300L);

            // Assert
            Assert.Equal(200L, result); // 300 - 100 = 200
        }

        [Fact]
        public void Subtract_WithUShort_ReturnsLongDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(50L);

            // Act
            var result = literal.Subtract((ushort)100);

            // Assert
            Assert.Equal(50L, result); // 100 - 50 = 50
        }

        [Fact]
        public void Subtract_WithUInt_ReturnsLongDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(30U);

            // Assert
            Assert.Equal(20L, result); // 30 - 10 = 20
        }

        [Fact]
        public void Subtract_WithULong_WhenPositiveLong_ReturnsULongDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(100UL);

            // Assert
            Assert.Equal(90UL, result); // 100 - 10 = 90
        }

        [Fact]
        public void Subtract_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-10L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(100UL));
        }

        [Fact]
        public void Subtract_WithFloat_ReturnsFloatDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(30.5f);

            // Assert
            Assert.Equal(20.5f, result); // 30.5 - 10 = 20.5
        }

        [Fact]
        public void Subtract_WithDouble_ReturnsDoubleDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(30.75);

            // Assert
            Assert.Equal(20.75, result); // 30.75 - 10 = 20.75
        }

        [Fact]
        public void Subtract_WithDecimal_ReturnsDecimalDifference()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(30.5m);

            // Assert
            Assert.Equal(20.5m, result); // 30.5 - 10 = 20.5
        }
        #endregion

        #region Multiply Tests
        [Fact]
        public void Multiply_WithArithmeticLiteral_CallsOtherLiteralMultiplyMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(10L);
            var literal2 = new IntArithmeticLiteral(5);

            // Act
            var result = literal1.Multiply(literal2);

            // Assert
            Assert.Equal(50L, result);
        }

        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Multiply();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithInt_ReturnsLongProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(7);

            // Assert
            Assert.Equal(35L, result);
        }

        [Fact]
        public void Multiply_WithLong_ReturnsLongProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Multiply(20L);

            // Assert
            Assert.Equal(200L, result);
        }

        [Fact]
        public void Multiply_WithUShort_ReturnsLongProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply((ushort)10);

            // Assert
            Assert.Equal(50L, result);
        }

        [Fact]
        public void Multiply_WithUInt_ReturnsLongProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(10U);

            // Assert
            Assert.Equal(50L, result);
        }

        [Fact]
        public void Multiply_WithULong_WhenPositiveLong_ReturnsULongProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(10UL);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Multiply_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-5L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(10UL));
        }

        [Fact]
        public void Multiply_WithFloat_ReturnsFloatProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(2.5f);

            // Assert
            Assert.Equal(12.5f, result);
        }

        [Fact]
        public void Multiply_WithDouble_ReturnsDoubleProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(2.5);

            // Assert
            Assert.Equal(12.5, result);
        }

        [Fact]
        public void Multiply_WithDecimal_ReturnsDecimalProduct()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Multiply(2.5m);

            // Assert
            Assert.Equal(12.5m, result);
        }
        #endregion

        #region Divide Tests
        [Fact]
        public void Divide_WithArithmeticLiteral_CallsOtherLiteralDivideMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(10L);
            var literal2 = new IntArithmeticLiteral(50);

            // Act
            var result = literal1.Divide(literal2);

            // Assert
            Assert.Equal(0L, result); // 10 / 50 = 0
        }

        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Divide();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Divide_WithInt_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(20);

            // Assert
            Assert.Equal(4L, result); // 20 / 5 = 4
        }

        [Fact]
        public void Divide_WithLong_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Divide(100L);

            // Assert
            Assert.Equal(10L, result); // 100 / 10 = 10
        }

        [Fact]
        public void Divide_WithUShort_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide((ushort)50);

            // Assert
            Assert.Equal(10L, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithUInt_ReturnsLongQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(50U);

            // Assert
            Assert.Equal(10L, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithULong_WhenPositiveLong_ReturnsULongQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(50UL);

            // Assert
            Assert.Equal(10UL, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-5L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(50UL));
        }

        [Fact]
        public void Divide_WithFloat_ReturnsFloatQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(50.0f);

            // Assert
            Assert.Equal(10.0f, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithDouble_ReturnsDoubleQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(50.0);

            // Assert
            Assert.Equal(10.0, result); // 50 / 5 = 10
        }

        [Fact]
        public void Divide_WithDecimal_ReturnsDecimalQuotient()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(5L);

            // Act
            var result = literal.Divide(50.0m);

            // Assert
            Assert.Equal(10.0m, result); // 50 / 5 = 10
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_WithArithmeticLiteral_CallsOtherLiteralModulusMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(3L);
            var literal2 = new IntArithmeticLiteral(10);

            // Act
            var result = literal1.Modulus(literal2);

            // Assert
            Assert.Equal(3L, result); // 3 % 10 = 3
        }

        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Modulus();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Modulus_WithInt_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithLong_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10L);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithUShort_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus((ushort)10);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithUInt_ReturnsLongRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10U);

            // Assert
            Assert.Equal(1L, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithULong_WhenPositiveLong_ReturnsULongRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10UL);

            // Assert
            Assert.Equal(1UL, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-3L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(10UL));
        }

        [Fact]
        public void Modulus_WithFloat_ReturnsFloatRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10.0f);

            // Assert
            Assert.Equal(1.0f, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithDouble_ReturnsDoubleRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10.0);

            // Assert
            Assert.Equal(1.0, result); // 10 % 3 = 1
        }

        [Fact]
        public void Modulus_WithDecimal_ReturnsDecimalRemainder()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(3L);

            // Act
            var result = literal.Modulus(10.0m);

            // Assert
            Assert.Equal(1.0m, result); // 10 % 3 = 1
        }
        #endregion

        #region BitAnd Tests
        [Fact]
        public void BitAnd_WithArithmeticLiteral_CallsOtherLiteralBitAndMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(12L); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitAnd(literal2);

            // Assert
            Assert.Equal(8L, result); // 1000 in binary = 8
        }

        [Fact]
        public void BitAnd_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.BitAnd();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitAnd_WithInt_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L); // 1100 in binary

            // Act
            var result = literal.BitAnd(10); // 1010 in binary

            // Assert
            Assert.Equal(8L, result); // 1000 in binary = 8
        }

        [Fact]
        public void BitAnd_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitAnd(10L);

            // Assert
            Assert.Equal(8L, result);
        }

        [Fact]
        public void BitAnd_WithUShort_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitAnd((ushort)10);

            // Assert
            Assert.Equal(8L, result);
        }

        [Fact]
        public void BitAnd_WithUInt_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitAnd(10U);

            // Assert
            Assert.Equal(8L, result);
        }

        [Fact]
        public void BitAnd_WithULong_WhenPositiveLong_ReturnsULongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitAnd(10UL);

            // Assert
            Assert.Equal(8UL, result);
        }

        [Fact]
        public void BitAnd_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-12L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(10UL));
        }
        #endregion

        #region BitOr Tests
        [Fact]
        public void BitOr_WithArithmeticLiteral_CallsOtherLiteralBitOrMethod()
        {
            // Arrange
            var literal1 = new LongArithmeticLiteral(12L); // 1100 in binary
            var literal2 = new IntArithmeticLiteral(10); // 1010 in binary

            // Act
            var result = literal1.BitOr(literal2);

            // Assert
            Assert.Equal(14L, result); // 1110 in binary = 14
        }

        [Fact]
        public void BitOr_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.BitOr();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void BitOr_WithInt_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L); // 1100 in binary

            // Act
            var result = literal.BitOr(10); // 1010 in binary

            // Assert
            Assert.Equal(14L, result); // 1110 in binary = 14
        }

        [Fact]
        public void BitOr_WithLong_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitOr(10L);

            // Assert
            Assert.Equal(14L, result);
        }

        [Fact]
        public void BitOr_WithUShort_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitOr((ushort)10);

            // Assert
            Assert.Equal(14L, result);
        }

        [Fact]
        public void BitOr_WithUInt_ReturnsLongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitOr(10U);

            // Assert
            Assert.Equal(14L, result);
        }

        [Fact]
        public void BitOr_WithULong_WhenPositiveLong_ReturnsULongResult()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(12L);

            // Act
            var result = literal.BitOr(10UL);

            // Assert
            Assert.Equal(14UL, result);
        }

        [Fact]
        public void BitOr_WithULong_WhenNegativeLong_ThrowsException()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(-12L);

            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(10UL));
        }
        #endregion

        #region Edge Cases and Other ArithmeticLiteral Types
        [Fact]
        public void Add_WithCharArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var charLiteral = new CharArithmeticLiteral('A'); // 65

            // Act
            var result = longLiteral.Add(charLiteral);

            // Assert
            Assert.Equal(75L, result);
        }

        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(50L);
            var ushortLiteral = new UShortArithmeticLiteral(100);

            // Act
            var result = longLiteral.Add(ushortLiteral);

            // Assert
            Assert.Equal(150L, result);
        }

        [Fact]
        public void Add_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var uintLiteral = new UIntArithmeticLiteral(20);

            // Act
            var result = longLiteral.Add(uintLiteral);

            // Assert
            Assert.Equal(30L, result);
        }

        [Fact]
        public void Add_WithFloatArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var floatLiteral = new FloatArithmeticLiteral(20.5f);

            // Act
            var result = longLiteral.Add(floatLiteral);

            // Assert
            Assert.Equal(30.5f, result);
        }

        [Fact]
        public void Add_WithDoubleArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var doubleLiteral = new DoubleArithmeticLiteral(20.75);

            // Act
            var result = longLiteral.Add(doubleLiteral);

            // Assert
            Assert.Equal(30.75, result);
        }

        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var decimalLiteral = new DecimalArithmeticLiteral(20.5m);

            // Act
            var result = longLiteral.Add(decimalLiteral);

            // Assert
            Assert.Equal(30.5m, result);
        }

        [Fact]
        public void Add_WithStringArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(42L);
            var stringLiteral = new StringArithmeticLiteral("Answer: ");

            // Act
            var result = longLiteral.Add(stringLiteral);
            /*
             * LongArithmeticLiteral
             * internal override object Add(ArithmeticLiteral v)
                {
                    return v.Add(m_value);
                }

                StringArithmeticLiteral
                internal override object Add(long v)
                {
                    return (v.ToString(CultureInfo.CurrentCulture) + m_value);
                }
             */

            // Assert
            //Assert.Equal("Answer: 42", result);
            Assert.Equal("42Answer: ", result);
        }

        [Fact]
        public void Add_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var nullLiteral = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = longLiteral.Add(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Multiply_WithNullArithmeticLiteral_ReturnsNull()
        {
            // Arrange
            var longLiteral = new LongArithmeticLiteral(10L);
            var nullLiteral = new NullArithmeticLiteral(typeof(long?));

            // Act
            var result = longLiteral.Multiply(nullLiteral);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_WithZero_ReturnsSameValue()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(0L);

            // Act
            var result = literal.Add(42);

            // Assert
            Assert.Equal(42L, result);
        }

        [Fact]
        public void Multiply_WithZero_ReturnsZero()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(0L);

            // Act
            var result = literal.Multiply(42);

            // Assert
            Assert.Equal(0L, result);
        }

        [Fact]
        public void BitOr_WithZero_ReturnsOtherValue()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(0L);

            // Act
            var result = literal.BitOr(42);

            // Assert
            Assert.Equal(42L, result);
        }

        [Fact]
        public void BitAnd_WithZero_ReturnsZero()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(0L);

            // Act
            var result = literal.BitAnd(42);

            // Assert
            Assert.Equal(0L, result);
        }

        [Fact]
        public void Add_WithLargeValues_HandlesCorrectly()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(long.MaxValue - 100);

            // Act
            var result = literal.Add(50);

            // Assert
            Assert.Equal(long.MaxValue - 50, result);
        }

        [Fact]
        public void Subtract_WithNegativeResult_ReturnsCorrectValue()
        {
            // Arrange
            var literal = new LongArithmeticLiteral(10L);

            // Act
            var result = literal.Subtract(5);

            // Assert
            Assert.Equal(-5L, result); // 5 - 10 = -5
        }
        #endregion
    }
}