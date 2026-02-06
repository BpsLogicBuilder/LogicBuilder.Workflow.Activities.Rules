namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class FloatArithmeticLiteralTest
    {
        #region Constructor and Value Tests
        
        [Fact]
        public void Constructor_SetsValueCorrectly()
        {
            // Arrange
            float testValue = 3.14f;
            
            // Act
            var literal = new FloatArithmeticLiteral(testValue);
            
            // Assert
            Assert.Equal(testValue, literal.Value);
            Assert.Equal(typeof(float), literal.m_type);
        }
        
        [Fact]
        public void Constructor_WithNegativeValue_SetsValueCorrectly()
        {
            // Arrange
            float testValue = -42.5f;
            
            // Act
            var literal = new FloatArithmeticLiteral(testValue);
            
            // Assert
            Assert.Equal(testValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_WithZero_SetsValueCorrectly()
        {
            // Arrange
            float testValue = 0f;
            
            // Act
            var literal = new FloatArithmeticLiteral(testValue);
            
            // Assert
            Assert.Equal(testValue, literal.Value);
        }
        
        #endregion
        
        #region Add Tests
        
        [Fact]
        public void Add_WithInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            int value = 3;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(8.5f, result);
        }
        
        [Fact]
        public void Add_WithLong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            long value = 10L;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(15.5f, result);
        }
        
        [Fact]
        public void Add_WithChar_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            char value = 'A'; // ASCII 65
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(70.5f, result);
        }
        
        [Fact]
        public void Add_WithUShort_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ushort value = 100;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(105.5f, result);
        }
        
        [Fact]
        public void Add_WithUInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            uint value = 200u;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(205.5f, result);
        }
        
        [Fact]
        public void Add_WithULong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ulong value = 300ul;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(305.5f, result);
        }
        
        [Fact]
        public void Add_WithFloat_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            float value = 2.3f;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(7.8f, (float)result, 5);
        }
        
        [Fact]
        public void Add_WithDouble_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            double value = 2.3;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(7.8, (double)result, 5);
        }
        
        [Fact]
        public void Add_WithString_ReturnsConcatenatedString()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            string value = "Test";
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal("Test5.5", result);
        }
        
        [Fact]
        public void Add_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            
            // Act
            var result = literal.Add();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Add_WithIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new IntArithmeticLiteral(3);
            
            // Act
            var result = literal.Add(otherLiteral);
            
            // Assert
            Assert.Equal(8.5f, result);
        }
        
        [Fact]
        public void Add_WithLongArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new LongArithmeticLiteral(10L);
            
            // Act
            var result = literal.Add(otherLiteral);
            
            // Assert
            Assert.Equal(15.5f, result);
        }
        
        [Fact]
        public void Add_WithUShortArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new UShortArithmeticLiteral(100);
            
            // Act
            var result = literal.Add(otherLiteral);
            
            // Assert
            Assert.Equal(105.5f, result);
        }
        
        [Fact]
        public void Add_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new DecimalArithmeticLiteral(10m);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(otherLiteral));
        }
        
        [Fact]
        public void Add_WithBooleanArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new BooleanArithmeticLiteral(true);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Add(otherLiteral));
        }
        
        #endregion
        
        #region Subtract Tests
        
        [Fact]
        public void Subtract_WithInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            int value = 3;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-7.5f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithLong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            long value = 5L;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-5.5f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithUShort_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            ushort value = 8;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-2.5f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithUInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            uint value = 5u;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-5.5f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithULong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            ulong value = 3ul;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-7.5f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithFloat_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            float value = 3.2f;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.Equal(-7.3f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithDouble_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            double value = 3.2;
            
            // Act
            var result = literal.Subtract(value);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(-7.3, (double)result, 5);
        }
        
        [Fact]
        public void Subtract_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            
            // Act
            var result = literal.Subtract();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Subtract_WithFloatArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            var otherLiteral = new FloatArithmeticLiteral(3.2f);
            
            // Act
            var result = literal.Subtract(otherLiteral);
            
            // Assert
            Assert.Equal(7.3f, (float)result, 5);
        }
        
        [Fact]
        public void Subtract_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            var otherLiteral = new DecimalArithmeticLiteral(5m);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Subtract(otherLiteral));
        }
        
        #endregion
        
        #region Multiply Tests
        
        [Fact]
        public void Multiply_WithInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            int value = 3;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(16.5f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithLong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            long value = 4L;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(22f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithUShort_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ushort value = 2;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(11f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithUInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            uint value = 3u;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(16.5f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithULong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ulong value = 2ul;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(11f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithFloat_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            float value = 2.5f;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.Equal(13.75f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithDouble_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            double value = 2.5;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(13.75, (double)result, 5);
        }
        
        [Fact]
        public void Multiply_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            
            // Act
            var result = literal.Multiply();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Multiply_WithUIntArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new UIntArithmeticLiteral(3u);
            
            // Act
            var result = literal.Multiply(otherLiteral);
            
            // Assert
            Assert.Equal(16.5f, (float)result, 5);
        }
        
        [Fact]
        public void Multiply_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new DecimalArithmeticLiteral(3m);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Multiply(otherLiteral));
        }
        
        #endregion
        
        #region Divide Tests
        
        [Fact]
        public void Divide_WithInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            int value = 4;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithLong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            long value = 4L;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithUShort_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            ushort value = 4;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithUInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            uint value = 4u;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithULong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            ulong value = 4ul;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithFloat_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            float value = 4f;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(0.4f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithDouble_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            double value = 4.0;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(0.4, (double)result, 5);
        }
        
        [Fact]
        public void Divide_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            
            // Act
            var result = literal.Divide();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Divide_WithCharArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(130f);
            var otherLiteral = new CharArithmeticLiteral('A'); // ASCII 65
            
            // Act
            var result = literal.Divide(otherLiteral);
            
            // Assert
            Assert.Equal(2f, (float)result, 5);
        }
        
        [Fact]
        public void Divide_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10f);
            var otherLiteral = new DecimalArithmeticLiteral(4m);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Divide(otherLiteral));
        }
        
        #endregion
        
        #region Modulus Tests
        
        [Fact]
        public void Modulus_WithInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            int value = 3;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithLong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            long value = 3L;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithUShort_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            ushort value = 3;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithUInt_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            uint value = 3u;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithULong_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            ulong value = 3ul;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithFloat_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            float value = 3f;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.Equal(3.0f, (float)result, 5);
        }
        
        [Fact]
        public void Modulus_WithDouble_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            double value = 3.0;
            
            // Act
            var result = literal.Modulus(value);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(3.0, (double)result, 5);
        }
        
        [Fact]
        public void Modulus_WithNull_ReturnsNull()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            
            // Act
            var result = literal.Modulus();
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Modulus_WithDoubleArithmeticLiteral_ReturnsCorrectResult()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            var otherLiteral = new DoubleArithmeticLiteral(3.0);
            
            // Act
            var result = literal.Modulus(otherLiteral);
            
            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(1.5, (double)result, 5);
        }
        
        [Fact]
        public void Modulus_WithDecimalArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(10.5f);
            var otherLiteral = new DecimalArithmeticLiteral(3m);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.Modulus(otherLiteral));
        }
        
        #endregion
        
        #region BitAnd Tests
        
        [Fact]
        public void BitAnd_WithInt_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            int value = 3;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithLong_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            long value = 3L;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithUShort_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ushort value = 3;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithUInt_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            uint value = 3u;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithULong_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ulong value = 3ul;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            float value = 3f;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            double value = 3.0;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            decimal value = 3m;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithBool_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            bool value = true;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(value));
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        [Fact]
        public void BitAnd_WithArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new IntArithmeticLiteral(3);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd(otherLiteral));
        }
        
        [Fact]
        public void BitAnd_WithNull_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitAnd());
            Assert.Contains("BitwiseAnd", exception.Message);
        }
        
        #endregion
        
        #region BitOr Tests
        
        [Fact]
        public void BitOr_WithInt_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            int value = 3;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithLong_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            long value = 3L;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithUShort_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ushort value = 3;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithUInt_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            uint value = 3u;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithULong_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            ulong value = 3ul;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithFloat_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            float value = 3f;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithDouble_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            double value = 3.0;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithDecimal_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            decimal value = 3m;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithBool_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            bool value = true;
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(value));
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        [Fact]
        public void BitOr_WithArithmeticLiteral_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            var otherLiteral = new ULongArithmeticLiteral(3ul);
            
            // Act & Assert
            Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr(otherLiteral));
        }
        
        [Fact]
        public void BitOr_WithNull_ThrowsException()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(5.5f);
            
            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => literal.BitOr());
            Assert.Contains("BitwiseOr", exception.Message);
        }
        
        #endregion
        
        #region Edge Cases
        
        [Fact]
        public void Add_WithMaxValue_HandlesOverflow()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(float.MaxValue);
            float value = float.MaxValue;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(float.PositiveInfinity, result);
        }
        
        [Fact]
        public void Add_WithMinValue_HandlesUnderflow()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(float.MinValue);
            float value = float.MinValue;
            
            // Act
            var result = literal.Add(value);
            
            // Assert
            Assert.Equal(float.NegativeInfinity, result);
        }
        
        [Fact]
        public void Divide_ByZero_ReturnsInfinity()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(0f);
            float value = 10f;
            
            // Act
            var result = literal.Divide(value);
            
            // Assert
            Assert.Equal(float.PositiveInfinity, result);
        }
        
        [Fact]
        public void Multiply_WithNaN_ReturnsNaN()
        {
            // Arrange
            var literal = new FloatArithmeticLiteral(float.NaN);
            float value = 5f;
            
            // Act
            var result = literal.Multiply(value);
            
            // Assert
            Assert.True(float.IsNaN((float)result));
        }
        
        #endregion
    }
}