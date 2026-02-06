using System;
using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleConditionCollectionTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_CreatesEmptyCollection()
        {
            // Act
            var collection = new RuleConditionCollection();

            // Assert
            Assert.NotNull(collection);
            Assert.Empty(collection);
        }

        #endregion

        #region Add Tests

        [Fact]
        public void Add_WithValidCondition_AddsSuccessfully()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");

            // Act
            collection.Add(condition);

            // Assert
            Assert.Single(collection);
            Assert.Equal("TestCondition", collection[0].Name);
        }

        [Fact]
        public void Add_WithMultipleConditions_AddsAll()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Condition1");
            var condition2 = new RuleExpressionCondition("Condition2");
            var condition3 = new RuleExpressionCondition("Condition3");

            // Act
            collection.Add(condition1);
            collection.Add(condition2);
            collection.Add(condition3);

            // Assert
            Assert.Equal(3, collection.Count);
            Assert.Equal("Condition1", collection[0].Name);
            Assert.Equal("Condition2", collection[1].Name);
            Assert.Equal("Condition3", collection[2].Name);
        }

        [Fact]
        public void Add_WithNullCondition_ThrowsArgumentNullException()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => collection.Add(null));
        }

        [Fact]
        public void Add_WithConditionWithNullName_ThrowsArgumentException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => collection.Add(condition));
            Assert.Contains("item.Name", exception.Message);
        }

        [Fact]
        public void Add_WithDuplicateName_ThrowsArgumentException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Duplicate");
            var condition2 = new RuleExpressionCondition("Duplicate");
            collection.Add(condition1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => collection.Add(condition2));
            Assert.Contains("Duplicate", exception.Message);
        }

        [Fact]
        public void Add_WhenRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            collection.OnRuntimeInitialized();
            var condition = new RuleExpressionCondition("TestCondition");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection.Add(condition));
        }

        #endregion

        #region Indexer Tests

        [Fact]
        public void Indexer_ByName_ReturnsCorrectCondition()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition", new CodePrimitiveExpression(true));
            collection.Add(condition);

            // Act
            var retrieved = collection["TestCondition"];

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("TestCondition", retrieved.Name);
        }

        [Fact]
        public void Indexer_ByIndex_ReturnsCorrectCondition()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("First");
            var condition2 = new RuleExpressionCondition("Second");
            collection.Add(condition1);
            collection.Add(condition2);

            // Act
            var first = collection[0];
            var second = collection[1];

            // Assert
            Assert.Equal("First", first.Name);
            Assert.Equal("Second", second.Name);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public void Contains_WithExistingName_ReturnsTrue()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            bool contains = collection.Contains("TestCondition");

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void Contains_WithNonExistingName_ReturnsFalse()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            bool contains = collection.Contains("NonExistent");

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void Contains_WithExistingCondition_ReturnsTrue()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            bool contains = collection.Contains(condition);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void Contains_OnEmptyCollection_ReturnsFalse()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act
            bool contains = collection.Contains("AnyCondition");

            // Assert
            Assert.False(contains);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public void Remove_WithExistingName_RemovesSuccessfully()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            bool removed = collection.Remove("TestCondition");

            // Assert
            Assert.True(removed);
            Assert.Empty(collection);
        }

        [Fact]
        public void Remove_WithNonExistingName_ReturnsFalse()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            bool removed = collection.Remove("NonExistent");

            // Assert
            Assert.False(removed);
            Assert.Single(collection);
        }

        [Fact]
        public void Remove_WhenRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);
            collection.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection.Remove("TestCondition"));
        }

        #endregion

        #region Clear Tests

        [Fact]
        public void Clear_RemovesAllConditions()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                new RuleExpressionCondition("Condition1"),
                new RuleExpressionCondition("Condition2"),
                new RuleExpressionCondition("Condition3")
            };

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection);
        }

        #endregion

        #region RuntimeMode Tests

        [Fact]
        public void RuntimeMode_DefaultValue_IsFalse()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act
            bool runtimeMode = collection.RuntimeMode;

            // Assert
            Assert.False(runtimeMode);
        }

        [Fact]
        public void RuntimeMode_CanBeSet()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                // Act
                RuntimeMode = true
            };

            // Assert
            Assert.True(collection.RuntimeMode);
        }

        [Fact]
        public void RuntimeMode_CanBeUnset()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                RuntimeMode = true
            };

            // Act
            collection.RuntimeMode = false;

            // Assert
            Assert.False(collection.RuntimeMode);
        }

        #endregion

        #region OnRuntimeInitialized Tests

        [Fact]
        public void OnRuntimeInitialized_SetsRuntimeModeToTrue()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act
            collection.OnRuntimeInitialized();

            // Assert
            Assert.True(collection.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_CallsOnRuntimeInitializedOnAllConditions()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Condition1");
            var condition2 = new RuleExpressionCondition("Condition2");
            collection.Add(condition1);
            collection.Add(condition2);

            // Act
            collection.OnRuntimeInitialized();

            // Assert - Verify by attempting to modify conditions (should throw)
            Assert.Throws<InvalidOperationException>(() => condition1.Name = "NewName");
            Assert.Throws<InvalidOperationException>(() => condition2.Name = "NewName");
        }

        [Fact]
        public void OnRuntimeInitialized_CanBeCalledMultipleTimes()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                new RuleExpressionCondition("TestCondition")
            };

            // Act
            collection.OnRuntimeInitialized();
            collection.OnRuntimeInitialized(); // Should not throw

            // Assert
            Assert.True(collection.RuntimeMode);
        }

        #endregion

        #region InsertItem Tests

        [Fact]
        public void InsertItem_WhenRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Condition1");
            collection.Add(condition1);
            collection.OnRuntimeInitialized();
            var condition2 = new RuleExpressionCondition("Condition2");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection.Insert(0, condition2));
        }

        #endregion

        #region SetItem Tests

        [Fact]
        public void SetItem_WhenRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Condition1");
            collection.Add(condition1);
            collection.OnRuntimeInitialized();
            var condition2 = new RuleExpressionCondition("Condition2");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection[0] = condition2);
        }

        [Fact]
        public void SetItem_WithValidCondition_ReplacesSuccessfully()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition1 = new RuleExpressionCondition("Condition1");
            var condition2 = new RuleExpressionCondition("Condition2");
            collection.Add(condition1);

            // Act
            collection[0] = condition2;

            // Assert
            Assert.Single(collection);
            Assert.Equal("Condition2", collection[0].Name);
        }

        #endregion

        #region GetKeyForItem Tests

        [Fact]
        public void GetKeyForItem_ReturnsConditionName()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var condition = new RuleExpressionCondition("TestCondition");
            collection.Add(condition);

            // Act
            var retrieved = collection["TestCondition"];

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("TestCondition", retrieved.Name);
        }

        #endregion

        #region Enumeration Tests

        [Fact]
        public void Enumeration_IteratesOverAllConditions()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                new RuleExpressionCondition("Condition1"),
                new RuleExpressionCondition("Condition2"),
                new RuleExpressionCondition("Condition3")
            };

            // Act
            int count = 0;
            foreach (var condition in collection)
            {
                count++;
                Assert.NotNull(condition);
                Assert.NotNull(condition.Name);
            }

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public void Enumeration_OnEmptyCollection_DoesNotIterate()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act
            int count = 0;
            foreach (var condition in collection)
            {
                count++;
            }

            // Assert
            Assert.Equal(0, count);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void Collection_WithComplexCondition_HandlesCorrectly()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var expression = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(5),
                CodeBinaryOperatorType.GreaterThan,
                new CodePrimitiveExpression(3));
            var condition = new RuleExpressionCondition("ComplexCondition", expression);

            // Act
            collection.Add(condition);

            // Assert
            Assert.Single(collection);
            Assert.Equal("ComplexCondition", collection[0].Name);
            Assert.NotNull(((RuleExpressionCondition)collection[0]).Expression);
        }

        [Fact]
        public void Collection_MaintainsInsertionOrder()
        {
            // Arrange
            var collection = new RuleConditionCollection();
            var names = new[] { "Alpha", "Gamma", "Beta", "Delta" };

            // Act
            foreach (var name in names)
            {
                collection.Add(new RuleExpressionCondition(name));
            }

            // Assert
            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], collection[i].Name);
            }
        }

        #endregion

        #region Count Tests

        [Fact]
        public void Count_OnEmptyCollection_ReturnsZero()
        {
            // Arrange
            var collection = new RuleConditionCollection();

            // Act
            int count = collection.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Count_AfterAddingItems_ReturnsCorrectCount()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                // Act
                new RuleExpressionCondition("Condition1"),
                new RuleExpressionCondition("Condition2")
            };

            // Assert
            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void Count_AfterRemovingItems_ReturnsCorrectCount()
        {
            // Arrange
            var collection = new RuleConditionCollection
            {
                new RuleExpressionCondition("Condition1"),
                new RuleExpressionCondition("Condition2"),
                new RuleExpressionCondition("Condition3")
            };

            // Act
            collection.Remove("Condition2");

            // Assert
            Assert.Equal(2, collection.Count);
        }

        #endregion
    }
}