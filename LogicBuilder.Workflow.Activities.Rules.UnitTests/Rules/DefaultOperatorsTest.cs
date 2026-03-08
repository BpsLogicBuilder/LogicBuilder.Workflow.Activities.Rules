using System;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class DefaultOperatorsTest
    {
        #region Addition Tests
        [Fact]
        public void Addition_Int_ReturnsCorrectSum()
        {
            // Arrange
            int x = 10;
            int y = 20;

            // Act
            int result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Addition_Int_WithNegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int x = -15;
            int y = 25;

            // Act
            int result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void Addition_UInt_ReturnsCorrectSum()
        {
            // Arrange
            uint x = 100U;
            uint y = 200U;

            // Act
            uint result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(300U, result);
        }

        [Fact]
        public void Addition_Long_ReturnsCorrectSum()
        {
            // Arrange
            long x = 1000L;
            long y = 2000L;

            // Act
            long result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(3000L, result);
        }

        [Fact]
        public void Addition_ULong_ReturnsCorrectSum()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 7000UL;

            // Act
            ulong result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(12000UL, result);
        }

        [Fact]
        public void Addition_Float_ReturnsCorrectSum()
        {
            // Arrange
            float x = 2.5f;
            float y = 3.5f;

            // Act
            float result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(6.0f, result);
        }

        [Fact]
        public void Addition_Double_ReturnsCorrectSum()
        {
            // Arrange
            double x = 1.5;
            double y = 2.5;

            // Act
            double result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(4.0, result);
        }

        [Fact]
        public void Addition_Decimal_ReturnsCorrectSum()
        {
            // Arrange
            decimal x = 10.5m;
            decimal y = 20.5m;

            // Act
            decimal result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(31.0m, result);
        }

        [Fact]
        public void Addition_String_ConcatenatesStrings()
        {
            // Arrange
            string x = "Hello";
            string y = " World";

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void Addition_StringAndObject_ConcatenatesWithObjectToString()
        {
            // Arrange
            string x = "Value: ";
            object y = 42;

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Value: 42", result);
        }

        [Fact]
        public void Addition_ObjectAndString_ConcatenatesWithObjectToString()
        {
            // Arrange
            object x = 42;
            string y = " is the answer";

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("42 is the answer", result);
        }
        #endregion

        #region Subtraction Tests
        [Fact]
        public void Subtraction_Int_ReturnsCorrectDifference()
        {
            // Arrange
            int x = 50;
            int y = 20;

            // Act
            int result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Subtraction_Int_WithNegativeResult_ReturnsCorrectDifference()
        {
            // Arrange
            int x = 10;
            int y = 30;

            // Act
            int result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(-20, result);
        }

        [Fact]
        public void Subtraction_UInt_ReturnsCorrectDifference()
        {
            // Arrange
            uint x = 300U;
            uint y = 100U;

            // Act
            uint result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(200U, result);
        }

        [Fact]
        public void Subtraction_Long_ReturnsCorrectDifference()
        {
            // Arrange
            long x = 5000L;
            long y = 2000L;

            // Act
            long result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(3000L, result);
        }

        [Fact]
        public void Subtraction_ULong_ReturnsCorrectDifference()
        {
            // Arrange
            ulong x = 10000UL;
            ulong y = 3000UL;

            // Act
            ulong result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(7000UL, result);
        }

        [Fact]
        public void Subtraction_Float_ReturnsCorrectDifference()
        {
            // Arrange
            float x = 5.5f;
            float y = 2.5f;

            // Act
            float result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(3.0f, result);
        }

        [Fact]
        public void Subtraction_Double_ReturnsCorrectDifference()
        {
            // Arrange
            double x = 10.0;
            double y = 3.5;

            // Act
            double result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(6.5, result);
        }

        [Fact]
        public void Subtraction_Decimal_ReturnsCorrectDifference()
        {
            // Arrange
            decimal x = 100.5m;
            decimal y = 50.5m;

            // Act
            decimal result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(50.0m, result);
        }
        #endregion

        #region Multiplication Tests
        [Fact]
        public void Multiply_Int_ReturnsCorrectProduct()
        {
            // Arrange
            int x = 5;
            int y = 7;

            // Act
            int result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(35, result);
        }

        [Fact]
        public void Multiply_Int_WithNegative_ReturnsCorrectProduct()
        {
            // Arrange
            int x = -4;
            int y = 6;

            // Act
            int result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(-24, result);
        }

        [Fact]
        public void Multiply_UInt_ReturnsCorrectProduct()
        {
            // Arrange
            uint x = 10U;
            uint y = 15U;

            // Act
            uint result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(150U, result);
        }

        [Fact]
        public void Multiply_Long_ReturnsCorrectProduct()
        {
            // Arrange
            long x = 100L;
            long y = 200L;

            // Act
            long result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(20000L, result);
        }

        [Fact]
        public void Multiply_ULong_ReturnsCorrectProduct()
        {
            // Arrange
            ulong x = 500UL;
            ulong y = 300UL;

            // Act
            ulong result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(150000UL, result);
        }

        [Fact]
        public void Multiply_Float_ReturnsCorrectProduct()
        {
            // Arrange
            float x = 2.5f;
            float y = 4.0f;

            // Act
            float result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(10.0f, result);
        }

        [Fact]
        public void Multiply_Double_ReturnsCorrectProduct()
        {
            // Arrange
            double x = 3.5;
            double y = 2.0;

            // Act
            double result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(7.0, result);
        }

        [Fact]
        public void Multiply_Decimal_ReturnsCorrectProduct()
        {
            // Arrange
            decimal x = 10.5m;
            decimal y = 3m;

            // Act
            decimal result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(31.5m, result);
        }
        #endregion

        #region Division Tests
        [Fact]
        public void Division_Int_ReturnsCorrectQuotient()
        {
            // Arrange
            int x = 100;
            int y = 5;

            // Act
            int result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(20, result);
        }

        [Fact]
        public void Division_Int_WithRemainder_ReturnsTruncatedQuotient()
        {
            // Arrange
            int x = 10;
            int y = 3;

            // Act
            int result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Division_UInt_ReturnsCorrectQuotient()
        {
            // Arrange
            uint x = 200U;
            uint y = 4U;

            // Act
            uint result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(50U, result);
        }

        [Fact]
        public void Division_Long_ReturnsCorrectQuotient()
        {
            // Arrange
            long x = 10000L;
            long y = 100L;

            // Act
            long result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(100L, result);
        }

        [Fact]
        public void Division_ULong_ReturnsCorrectQuotient()
        {
            // Arrange
            ulong x = 15000UL;
            ulong y = 300UL;

            // Act
            ulong result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(50UL, result);
        }

        [Fact]
        public void Division_Float_ReturnsCorrectQuotient()
        {
            // Arrange
            float x = 10.0f;
            float y = 4.0f;

            // Act
            float result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(2.5f, result);
        }

        [Fact]
        public void Division_Double_ReturnsCorrectQuotient()
        {
            // Arrange
            double x = 15.0;
            double y = 3.0;

            // Act
            double result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void Division_Decimal_ReturnsCorrectQuotient()
        {
            // Arrange
            decimal x = 100m;
            decimal y = 8m;

            // Act
            decimal result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(12.5m, result);
        }
        #endregion

        #region Modulus Tests
        [Fact]
        public void Modulus_Int_ReturnsCorrectRemainder()
        {
            // Arrange
            int x = 10;
            int y = 3;

            // Act
            int result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Modulus_Int_WithZeroRemainder_ReturnsZero()
        {
            // Arrange
            int x = 15;
            int y = 5;

            // Act
            int result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Modulus_UInt_ReturnsCorrectRemainder()
        {
            // Arrange
            uint x = 17U;
            uint y = 5U;

            // Act
            uint result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(2U, result);
        }

        [Fact]
        public void Modulus_Long_ReturnsCorrectRemainder()
        {
            // Arrange
            long x = 100L;
            long y = 7L;

            // Act
            long result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(2L, result);
        }

        [Fact]
        public void Modulus_ULong_ReturnsCorrectRemainder()
        {
            // Arrange
            ulong x = 1000UL;
            ulong y = 9UL;

            // Act
            ulong result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1UL, result);
        }

        [Fact]
        public void Modulus_Float_ReturnsCorrectRemainder()
        {
            // Arrange
            float x = 10.5f;
            float y = 3.0f;

            // Act
            float result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1.5f, result);
        }

        [Fact]
        public void Modulus_Double_ReturnsCorrectRemainder()
        {
            // Arrange
            double x = 20.5;
            double y = 6.0;

            // Act
            double result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(2.5, result);
        }

        [Fact]
        public void Modulus_Decimal_ReturnsCorrectRemainder()
        {
            // Arrange
            decimal x = 10.5m;
            decimal y = 3m;

            // Act
            decimal result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1.5m, result);
        }
        #endregion

        #region BitwiseAnd Tests
        [Fact]
        public void BitwiseAnd_Int_ReturnsCorrectResult()
        {
            // Arrange
            int x = 15; // 1111 in binary
            int y = 7;  // 0111 in binary

            // Act
            int result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(7, result); // 0111 in binary
        }

        [Fact]
        public void BitwiseAnd_Int_WithNoCommonBits_ReturnsZero()
        {
            // Arrange
            int x = 8;  // 1000 in binary
            int y = 4;  // 0100 in binary

            // Act
            int result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void BitwiseAnd_UInt_ReturnsCorrectResult()
        {
            // Arrange
            uint x = 255U;
            uint y = 127U;

            // Act
            uint result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(127U, result);
        }

        [Fact]
        public void BitwiseAnd_Long_ReturnsCorrectResult()
        {
            // Arrange
            long x = 255L;
            long y = 15L;

            // Act
            long result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(15L, result);
        }

        [Fact]
        public void BitwiseAnd_ULong_ReturnsCorrectResult()
        {
            // Arrange
            ulong x = 255UL;
            ulong y = 127UL;

            // Act
            ulong result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(127UL, result);
        }

        [Fact]
        public void BitwiseAnd_Bool_TrueAndTrue_ReturnsTrue()
        {
            // Arrange
            bool x = true;
            bool y = true;

            // Act
            bool result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BitwiseAnd_Bool_TrueAndFalse_ReturnsFalse()
        {
            // Arrange
            bool x = true;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void BitwiseAnd_Bool_FalseAndFalse_ReturnsFalse()
        {
            // Arrange
            bool x = false;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region BitwiseOr Tests
        [Fact]
        public void BitwiseOr_Int_ReturnsCorrectResult()
        {
            // Arrange
            int x = 8; // 1000 in binary
            int y = 4; // 0100 in binary

            // Act
            int result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(12, result); // 1100 in binary
        }

        [Fact]
        public void BitwiseOr_Int_WithSameBits_ReturnsSame()
        {
            // Arrange
            int x = 7;
            int y = 7;

            // Act
            int result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void BitwiseOr_UInt_ReturnsCorrectResult()
        {
            // Arrange
            uint x = 128U;
            uint y = 64U;

            // Act
            uint result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(192U, result);
        }

        [Fact]
        public void BitwiseOr_Long_ReturnsCorrectResult()
        {
            // Arrange
            long x = 16L;
            long y = 32L;

            // Act
            long result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(48L, result);
        }

        [Fact]
        public void BitwiseOr_ULong_ReturnsCorrectResult()
        {
            // Arrange
            ulong x = 1UL;
            ulong y = 2UL;

            // Act
            ulong result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(3UL, result);
        }

        [Fact]
        public void BitwiseOr_Bool_TrueOrTrue_ReturnsTrue()
        {
            // Arrange
            bool x = true;
            bool y = true;

            // Act
            bool result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BitwiseOr_Bool_TrueOrFalse_ReturnsTrue()
        {
            // Arrange
            bool x = true;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BitwiseOr_Bool_FalseOrFalse_ReturnsFalse()
        {
            // Arrange
            bool x = false;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region Equality Tests
        [Fact]
        public void Equality_Int_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            int x = 42;
            int y = 42;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Int_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            int x = 42;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equality_UInt_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            uint x = 100U;
            uint y = 100U;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Long_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            long x = 1000L;
            long y = 1000L;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_ULong_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 5000UL;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Float_WithSimilarValues_ReturnsTrue()
        {
            // Arrange
            float x = 3.14f;
            float y = 3.14f;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Float_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            float x = 3.14f;
            float y = 2.71f;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equality_Double_WithSimilarValues_ReturnsTrue()
        {
            // Arrange
            double x = 2.71828;
            double y = 2.71828;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Double_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            double x = 2.71828;
            double y = 3.14159;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equality_Decimal_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            decimal x = 99.99m;
            decimal y = 99.99m;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Bool_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            bool x = true;
            bool y = true;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Bool_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            bool x = true;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equality_String_WithSameStrings_ReturnsTrue()
        {
            // Arrange
            string x = "test";
            string y = "test";

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_String_WithDifferentStrings_ReturnsFalse()
        {
            // Arrange
            string x = "test1";
            string y = "test2";

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ObjectEquality_WithSameReference_ReturnsTrue()
        {
            // Arrange
            object x = new();
            object y = x;

            // Act
            bool result = Literal.DefaultOperators.ObjectEquals(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ObjectEquality_WithDifferentReferences_ReturnsFalse()
        {
            // Arrange
            object x = new();
            object y = new();

            // Act
            bool result = Literal.DefaultOperators.ObjectEquals(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ObjectEquality_WithNulls_ReturnsTrue()
        {
            // Arrange
            object? x = null;
            object? y = null;

            // Act
            bool result = Literal.DefaultOperators.ObjectEquals(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ObjectEquality_WithOneNull_ReturnsFalse()
        {
            // Arrange
            object x = new();
            object? y = null;

            // Act
            bool result = Literal.DefaultOperators.ObjectEquals(x, y);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_Int_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            int x = 100;
            int y = 50;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_Int_WithSmallerFirst_ReturnsFalse()
        {
            // Arrange
            int x = 50;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_Int_WithEqualValues_ReturnsFalse()
        {
            // Arrange
            int x = 100;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThan_UInt_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            uint x = 200U;
            uint y = 100U;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_Long_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            long x = 5000L;
            long y = 1000L;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_ULong_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            ulong x = 10000UL;
            ulong y = 5000UL;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_Float_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            float x = 5.5f;
            float y = 2.2f;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_Double_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            double x = 10.5;
            double y = 5.2;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_Decimal_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            decimal x = 100.5m;
            decimal y = 50.5m;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_Int_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            int x = 100;
            int y = 50;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Int_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            int x = 100;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Int_WithSmallerFirst_ReturnsFalse()
        {
            // Arrange
            int x = 50;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqual_UInt_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            uint x = 100U;
            uint y = 100U;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Long_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            long x = 2000L;
            long y = 1000L;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_ULong_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 5000UL;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Float_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            float x = 7.5f;
            float y = 3.2f;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Double_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            double x = 5.5;
            double y = 5.5;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_Decimal_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            decimal x = 99.99m;
            decimal y = 50.00m;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_Int_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            int x = 50;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_Int_WithLargerFirst_ReturnsFalse()
        {
            // Arrange
            int x = 100;
            int y = 50;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_Int_WithEqualValues_ReturnsFalse()
        {
            // Arrange
            int x = 100;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThan_UInt_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            uint x = 100U;
            uint y = 200U;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_Long_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            long x = 1000L;
            long y = 5000L;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_ULong_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 10000UL;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_Float_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            float x = 2.2f;
            float y = 5.5f;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_Double_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            double x = 5.2;
            double y = 10.5;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThan_Decimal_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            decimal x = 25.5m;
            decimal y = 100.5m;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_Int_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            int x = 50;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Int_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            int x = 100;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Int_WithLargerFirst_ReturnsFalse()
        {
            // Arrange
            int x = 100;
            int y = 50;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqual_UInt_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            uint x = 100U;
            uint y = 100U;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Long_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            long x = 1000L;
            long y = 2000L;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_ULong_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 5000UL;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Float_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            float x = 3.2f;
            float y = 7.5f;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Double_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            double x = 5.5;
            double y = 5.5;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_Decimal_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            decimal x = 50.00m;
            decimal y = 99.99m;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region Edge Cases Tests
        [Fact]
        public void Addition_Int_Overflow_WrapsAround()
        {
            // Arrange
            int x = int.MaxValue;
            int y = 1;

            // Act
            int result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(int.MinValue, result);
        }

        [Fact]
        public void Subtraction_Int_Underflow_WrapsAround()
        {
            // Arrange
            int x = int.MinValue;
            int y = 1;

            // Act
            int result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(int.MaxValue, result);
        }

        [Fact]
        public void Multiply_Int_WithZero_ReturnsZero()
        {
            // Arrange
            int x = 100;
            int y = 0;

            // Act
            int result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Addition_String_WithNull_ConcatenatesNull()
        {
            // Arrange
            string x = "Hello";
            object? y = null;

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void Addition_String_WithEmptyString_ReturnsOriginal()
        {
            // Arrange
            string x = "Hello";
            string y = "";

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void BitwiseAnd_Int_WithZero_ReturnsZero()
        {
            // Arrange
            int x = 255;
            int y = 0;

            // Act
            int result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void BitwiseOr_Int_WithZero_ReturnsOriginal()
        {
            // Arrange
            int x = 255;
            int y = 0;

            // Act
            int result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(255, result);
        }

        [Fact]
        public void Equality_Float_WithVeryCloseValues_ReturnsTrue()
        {
            // Arrange
            float x = 1.0f;
            float y = 1.0f + float.Epsilon / 2;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equality_Double_WithVeryCloseValues_ReturnsTrue()
        {
            // Arrange
            double x = 1.0;
            double y = 1.0 + double.Epsilon / 2;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}
