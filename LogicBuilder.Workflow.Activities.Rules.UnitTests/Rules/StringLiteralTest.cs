namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class StringLiteralTest
    {
        #region Constructor and Value Tests
        
        [Fact]
        public void Constructor_CreatesStringLiteral_WithCorrectValue()
        {
            // Arrange
            string testValue = "Hello World";
            
            // Act
            Literal literal = Literal.MakeLiteral(typeof(string), testValue);
            
            // Assert
            Assert.NotNull(literal);
            Assert.Equal(testValue, literal.Value);
        }
        
        [Fact]
        public void Constructor_CreatesStringLiteral_WithEmptyString()
        {
            // Arrange
            string testValue = string.Empty;
            
            // Act
            Literal literal = Literal.MakeLiteral(typeof(string), testValue);
            
            // Assert
            Assert.NotNull(literal);
            Assert.Equal(string.Empty, literal.Value);
        }
        
        #endregion
        
        #region Equal Tests
        
        [Fact]
        public void Equal_SameStrings_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_DifferentStrings_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "TEST");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_EmptyStrings_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), string.Empty);
            Literal literal2 = Literal.MakeLiteral(typeof(string), string.Empty);
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_StringAndNull_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_NullAndString_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equal_BothNull_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region LessThan Tests
        
        [Fact]
        public void LessThan_FirstStringLessThanSecond_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "apple");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "banana");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_FirstStringGreaterThanSecond_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "banana");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "apple");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void LessThan_SameStrings_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void LessThan_EmptyStringAndNonEmpty_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), string.Empty);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_NullAndString_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_StringAndNull_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        #endregion
        
        #region GreaterThan Tests
        
        [Fact]
        public void GreaterThan_FirstStringGreaterThanSecond_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "banana");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "apple");
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_FirstStringLessThanSecond_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "apple");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "banana");
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThan_SameStrings_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThan_StringAndNull_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThan_NullAndString_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThan_NonEmptyAndEmpty_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), string.Empty);
            
            // Act
            bool result = literal1.GreaterThan(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        #endregion
        
        #region LessThanOrEqual Tests
        
        [Fact]
        public void LessThanOrEqual_FirstStringLessThanSecond_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "apple");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "banana");
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_SameStrings_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_FirstStringGreaterThanSecond_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "banana");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "apple");
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void LessThanOrEqual_NullAndNull_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_NullAndString_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqual_StringAndNull_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.LessThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        #endregion
        
        #region GreaterThanOrEqual Tests
        
        [Fact]
        public void GreaterThanOrEqual_FirstStringGreaterThanSecond_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "banana");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "apple");
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_SameStrings_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_FirstStringLessThanSecond_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "apple");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "banana");
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_NullAndNull_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_StringAndNull_ReturnsTrue()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), null);
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqual_NullAndString_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), null);
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.GreaterThanOrEqual(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        #endregion
        
        #region Case Sensitivity Tests
        
        [Fact]
        public void Equal_DifferentCase_ReturnsFalse()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "Test");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "test");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.False(result);
        }
        
        #endregion
        
        #region Special Character Tests
        
        [Fact]
        public void Equal_StringsWithSpecialCharacters_ComparesCorrectly()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "Hello\nWorld");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "Hello\nWorld");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void Equal_StringsWithUnicodeCharacters_ComparesCorrectly()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "Héllo");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "Héllo");
            
            // Act
            bool result = literal1.Equal(literal2);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void LessThan_NumberStrings_ComparesLexicographically()
        {
            // Arrange
            Literal literal1 = Literal.MakeLiteral(typeof(string), "10");
            Literal literal2 = Literal.MakeLiteral(typeof(string), "2");
            
            // Act
            bool result = literal1.LessThan(literal2);
            
            // Assert
            // Lexicographically "10" < "2" because '1' < '2'
            Assert.True(result);
        }
        
        #endregion
    }
}