namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RulePathQualifierTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithNameAndNull_CreatesInstance()
        {
            // Arrange
            string name = "TestProperty";

            // Act
            var qualifier = new RulePathQualifier(name, null);

            // Assert
            Assert.NotNull(qualifier);
            Assert.Equal(name, qualifier.Name);
            Assert.Null(qualifier.Next);
        }

        [Fact]
        public void Constructor_WithNameAndNext_CreatesInstance()
        {
            // Arrange
            string firstName = "FirstProperty";
            string secondName = "SecondProperty";
            var nextQualifier = new RulePathQualifier(secondName, null);

            // Act
            var qualifier = new RulePathQualifier(firstName, nextQualifier);

            // Assert
            Assert.NotNull(qualifier);
            Assert.Equal(firstName, qualifier.Name);
            Assert.NotNull(qualifier.Next);
            Assert.Equal(secondName, qualifier.Next.Name);
        }

        [Fact]
        public void Constructor_WithNullName_CreatesInstance()
        {
            // Arrange & Act
            var qualifier = new RulePathQualifier(null, null);

            // Assert
            Assert.NotNull(qualifier);
            Assert.Null(qualifier.Name);
            Assert.Null(qualifier.Next);
        }

        [Fact]
        public void Constructor_WithEmptyName_CreatesInstance()
        {
            // Arrange
            string name = string.Empty;

            // Act
            var qualifier = new RulePathQualifier(name, null);

            // Assert
            Assert.NotNull(qualifier);
            Assert.Equal(string.Empty, qualifier.Name);
            Assert.Null(qualifier.Next);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Name_ReturnsCorrectValue()
        {
            // Arrange
            string expectedName = "PropertyName";
            var qualifier = new RulePathQualifier(expectedName, null);

            // Act
            string actualName = qualifier.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Next_ReturnsCorrectValue()
        {
            // Arrange
            var nextQualifier = new RulePathQualifier("NextProperty", null);
            var qualifier = new RulePathQualifier("CurrentProperty", nextQualifier);

            // Act
            RulePathQualifier actualNext = qualifier.Next;

            // Assert
            Assert.NotNull(actualNext);
            Assert.Same(nextQualifier, actualNext);
        }

        [Fact]
        public void Next_WhenNull_ReturnsNull()
        {
            // Arrange
            var qualifier = new RulePathQualifier("PropertyName", null);

            // Act
            RulePathQualifier actualNext = qualifier.Next;

            // Assert
            Assert.Null(actualNext);
        }

        #endregion

        #region Chain Tests

        [Fact]
        public void Chain_WithThreeQualifiers_NavigatesCorrectly()
        {
            // Arrange
            var third = new RulePathQualifier("Third", null);
            var second = new RulePathQualifier("Second", third);
            var first = new RulePathQualifier("First", second);

            // Act & Assert
            Assert.Equal("First", first.Name);
            Assert.NotNull(first.Next);
            Assert.Equal("Second", first.Next.Name);
            Assert.NotNull(first.Next.Next);
            Assert.Equal("Third", first.Next.Next.Name);
            Assert.Null(first.Next.Next.Next);
        }

        [Fact]
        public void Chain_WithMultipleQualifiers_PreservesOrder()
        {
            // Arrange
            string[] names = ["Property1", "Property2", "Property3", "Property4"];
            RulePathQualifier? current = null;

            // Build chain in reverse order
            for (int i = names.Length - 1; i >= 0; i--)
            {
                current = new RulePathQualifier(names[i], current);
            }

            // Act & Assert - Traverse chain and verify order
            RulePathQualifier? qualifier = current;
            for (int i = 0; i < names.Length; i++)
            {
                Assert.NotNull(qualifier);
                Assert.Equal(names[i], qualifier?.Name);
                qualifier = qualifier?.Next;
            }
            Assert.Null(qualifier);
        }

        #endregion
    }
}