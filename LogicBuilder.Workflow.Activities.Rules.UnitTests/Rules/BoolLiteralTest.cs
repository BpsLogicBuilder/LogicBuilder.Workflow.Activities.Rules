namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class BoolLiteralTest
    {
        #region Constructor Tests
        [Fact]
        public void Constructor_WithTrue_SetsValueCorrectly()
        {
            // Arrange & Act
            var boolLiteral = new BoolLiteral(true);

            // Assert
            Assert.True((bool)boolLiteral.Value);
        }

        [Fact]
        public void Constructor_WithFalse_SetsValueCorrectly()
        {
            // Arrange & Act
            var boolLiteral = new BoolLiteral(false);

            // Assert
            Assert.False((bool)boolLiteral.Value);
        }

        [Fact]
        public void Constructor_SetsTypeCorrectly()
        {
            // Arrange & Act
            var boolLiteral = new BoolLiteral(true);

            // Assert
            Assert.Equal(typeof(bool), boolLiteral.m_type);
        }
        #endregion

        #region Value Property Tests
        [Fact]
        public void Value_ReturnsBoxedBoolean()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);

            // Act
            var value = boolLiteral.Value;

            // Assert
            Assert.IsType<bool>(value);
            Assert.True((bool)value);
        }

        [Fact]
        public void Value_ReturnsFalse_WhenConstructedWithFalse()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(false);

            // Act
            var value = boolLiteral.Value;

            // Assert
            Assert.IsType<bool>(value);
            Assert.False((bool)value);
        }
        #endregion

        #region Equal Tests
        [Fact]
        public void Equal_WithSameBoolLiteral_True_ReturnsTrue()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(true);
            var boolLiteral2 = new BoolLiteral(true);

            // Act
            var result = boolLiteral1.Equal(boolLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithSameBoolLiteral_False_ReturnsTrue()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(false);
            var boolLiteral2 = new BoolLiteral(false);

            // Act
            var result = boolLiteral1.Equal(boolLiteral2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithDifferentBoolLiteral_TrueAndFalse_ReturnsFalse()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(true);
            var boolLiteral2 = new BoolLiteral(false);

            // Act
            var result = boolLiteral1.Equal(boolLiteral2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithDifferentBoolLiteral_FalseAndTrue_ReturnsFalse()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(false);
            var boolLiteral2 = new BoolLiteral(true);

            // Act
            var result = boolLiteral1.Equal(boolLiteral2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithBoolValue_True_ReturnsTrue()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);

            // Act
            var result = boolLiteral.Equal(true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithBoolValue_False_ReturnsTrue()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(false);

            // Act
            var result = boolLiteral.Equal(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equal_WithBoolValue_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);

            // Act
            var result = boolLiteral.Equal(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithNonBoolLiteral_ReturnsFalse()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);
            var intLiteral = Literal.MakeLiteral(typeof(int), 1);

            // Act
            var result = boolLiteral.Equal(intLiteral);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equal_WithNullLiteral_ReturnsFalse()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);
            var nullLiteral = new NullLiteral(typeof(bool));

            // Act
            var result = boolLiteral.Equal(nullLiteral);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region LessThan Tests
        [Fact]
        public void LessThan_WithBoolLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(false);
            var boolLiteral2 = new BoolLiteral(true);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral1.LessThan(boolLiteral2));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void LessThan_WithBoolValue_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(false);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.LessThan(true));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void LessThan_WithIntLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);
            var intLiteral = Literal.MakeLiteral(typeof(int), 5);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.LessThan(intLiteral));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }
        #endregion

        #region GreaterThan Tests
        [Fact]
        public void GreaterThan_WithBoolLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(true);
            var boolLiteral2 = new BoolLiteral(false);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral1.GreaterThan(boolLiteral2));
            
            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void GreaterThan_WithBoolValue_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.GreaterThan(false));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void GreaterThan_WithStringLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);
            var stringLiteral = Literal.MakeLiteral(typeof(string), "test");

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.GreaterThan(stringLiteral));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }
        #endregion

        #region LessThanOrEqual Tests
        [Fact]
        public void LessThanOrEqual_WithBoolLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(false);
            var boolLiteral2 = new BoolLiteral(true);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral1.LessThanOrEqual(boolLiteral2));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void LessThanOrEqual_WithBoolValue_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(false);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.LessThanOrEqual(true));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void LessThanOrEqual_WithDecimalLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);
            var decimalLiteral = Literal.MakeLiteral(typeof(decimal), 10.5m);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.LessThanOrEqual(decimalLiteral));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }
        #endregion

        #region GreaterThanOrEqual Tests
        [Fact]
        public void GreaterThanOrEqual_WithBoolLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral1 = new BoolLiteral(true);
            var boolLiteral2 = new BoolLiteral(false);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral1.GreaterThanOrEqual(boolLiteral2));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void GreaterThanOrEqual_WithBoolValue_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(true);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.GreaterThanOrEqual(false));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }

        [Fact]
        public void GreaterThanOrEqual_WithFloatLiteral_ThrowsException()
        {
            // Arrange
            var boolLiteral = new BoolLiteral(false);
            var floatLiteral = Literal.MakeLiteral(typeof(float), 3.14f);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(() => 
                boolLiteral.GreaterThanOrEqual(floatLiteral));

            Assert.Contains("operands of types", exception.Message.ToLower());
            Assert.Contains("can not be compared.", exception.Message.ToLower());
        }
        #endregion

        #region Integration Tests with MakeLiteral
        [Fact]
        public void MakeLiteral_WithBoolType_CreatesBoolLiteral()
        {
            // Arrange & Act
            var literal = Literal.MakeLiteral(typeof(bool), true);

            // Assert
            Assert.IsType<BoolLiteral>(literal);
            Assert.True((bool)literal.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableBoolType_CreatesBoolLiteral()
        {
            // Arrange & Act
            var literal = Literal.MakeLiteral(typeof(bool?), false);

            // Assert
            Assert.IsType<BoolLiteral>(literal);
            Assert.False((bool)literal.Value);
        }

        [Fact]
        public void Equal_BetweenMakeLiteralInstances_WorksCorrectly()
        {
            // Arrange
            var literal1 = Literal.MakeLiteral(typeof(bool), true);
            var literal2 = Literal.MakeLiteral(typeof(bool), true);
            var literal3 = Literal.MakeLiteral(typeof(bool), false);

            // Act & Assert
            Assert.True(literal1.Equal(literal2));
            Assert.False(literal1.Equal(literal3));
        }
        #endregion
    }
}