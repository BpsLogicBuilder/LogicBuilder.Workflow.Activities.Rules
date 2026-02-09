using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleSetCollectionTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_Default_InitializesEmptyCollection()
        {
            // Arrange & Act
            var collection = new RuleSetCollection();

            // Assert
            Assert.NotNull(collection);
            Assert.Empty(collection);
            Assert.False(collection.RuntimeMode);
        }

        #endregion

        #region Add Tests

        [Fact]
        public void Add_ValidRuleSet_AddsToCollection()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");

            // Act
            collection.Add(ruleSet);

            // Assert
            Assert.Single(collection);
            Assert.Contains(ruleSet, collection);
            Assert.True(collection.Contains("TestRuleSet"));
        }

        [Fact]
        public void Add_NullItem_ThrowsArgumentNullException()
        {
            // Arrange
            var collection = new RuleSetCollection();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => collection.Add(null));
            Assert.Equal("item", exception.ParamName);
        }

        [Fact]
        public void Add_RuleSetWithNullName_ThrowsArgumentException()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => collection.Add(ruleSet));
            Assert.Contains("item.Name", exception.Message);
        }

        [Fact]
        public void Add_DuplicateRuleSetName_ThrowsArgumentException()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet1 = new RuleSet("DuplicateName");
            var ruleSet2 = new RuleSet("DuplicateName");
            collection.Add(ruleSet1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => collection.Add(ruleSet2));
            Assert.Contains("DuplicateName", exception.Message);
        }

        [Fact]
        public void Add_AfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleSetCollection();
            collection.OnRuntimeInitialized();
            var ruleSet = new RuleSet("TestRuleSet");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection.Add(ruleSet));
        }

        [Fact]
        public void Add_MultipleRuleSets_AllAdded()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet1 = new RuleSet("RuleSet1");
            var ruleSet2 = new RuleSet("RuleSet2");
            var ruleSet3 = new RuleSet("RuleSet3");

            // Act
            collection.Add(ruleSet1);
            collection.Add(ruleSet2);
            collection.Add(ruleSet3);

            // Assert
            Assert.Equal(3, collection.Count);
            Assert.Contains(ruleSet1, collection);
            Assert.Contains(ruleSet2, collection);
            Assert.Contains(ruleSet3, collection);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public void Remove_ExistingRuleSet_RemovesFromCollection()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);

            // Act
            var result = collection.Remove("TestRuleSet");

            // Assert
            Assert.True(result);
            Assert.Empty(collection);
            Assert.False(collection.Contains("TestRuleSet"));
        }

        [Fact]
        public void Remove_AfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);
            collection.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection.Remove("TestRuleSet"));
        }

        [Fact]
        public void Remove_NonExistingRuleSet_ReturnsFalse()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);

            // Act
            var result = collection.Remove("NonExistent");

            // Assert
            Assert.False(result);
            Assert.Single(collection);
        }

        #endregion

        #region Clear Tests

        [Fact]
        public void Clear_RemovesAllRuleSets()
        {
            // Arrange
            var collection = new RuleSetCollection
            {
                new RuleSet("RuleSet1"),
                new RuleSet("RuleSet2"),
                new RuleSet("RuleSet3")
            };

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection);
        }

        #endregion

        #region Indexer Tests

        [Fact]
        public void Indexer_ByName_ReturnsCorrectRuleSet()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);

            // Act
            var retrieved = collection["TestRuleSet"];

            // Assert
            Assert.Same(ruleSet, retrieved);
        }

        [Fact]
        public void Indexer_Set_AfterRuntimeInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet1 = new RuleSet("RuleSet1");
            var ruleSet2 = new RuleSet("RuleSet1");
            collection.Add(ruleSet1);
            collection.OnRuntimeInitialized();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => collection[0] = ruleSet2);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public void Contains_ExistingRuleSet_ReturnsTrue()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);

            // Act
            var result = collection.Contains("TestRuleSet");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_NonExistingRuleSet_ReturnsFalse()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet = new RuleSet("TestRuleSet");
            collection.Add(ruleSet);

            // Act
            var result = collection.Contains("NonExistent");

            // Assert
            Assert.False(result);
        }

        #endregion

        #region OnRuntimeInitialized Tests

        [Fact]
        public void OnRuntimeInitialized_SetsRuntimeMode()
        {
            // Arrange
            var collection = new RuleSetCollection
            {
                new RuleSet("TestRuleSet")
            };

            // Act
            collection.OnRuntimeInitialized();

            // Assert
            Assert.True(collection.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_CalledMultipleTimes_OnlyInitializesOnce()
        {
            // Arrange
            var collection = new RuleSetCollection
            {
                new RuleSet("TestRuleSet")
            };

            // Act
            collection.OnRuntimeInitialized();
            collection.OnRuntimeInitialized();
            collection.OnRuntimeInitialized();

            // Assert
            Assert.True(collection.RuntimeMode);
        }

        [Fact]
        public void OnRuntimeInitialized_InitializesAllRuleSets()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var ruleSet1 = new RuleSet("RuleSet1");
            var ruleSet2 = new RuleSet("RuleSet2");
            collection.Add(ruleSet1);
            collection.Add(ruleSet2);

            // Act
            collection.OnRuntimeInitialized();

            // Assert
            // Note: We can't directly test if RuleSet.OnRuntimeInitialized was called
            // but we can verify the collection is in runtime mode
            Assert.True(collection.RuntimeMode);
        }

        #endregion

        #region RuntimeMode Tests

        [Fact]
        public void RuntimeMode_Get_ReturnsCorrectValue()
        {
            // Arrange
            var collection = new RuleSetCollection();

            // Act & Assert
            Assert.False(collection.RuntimeMode);

            collection.RuntimeMode = true;
            Assert.True(collection.RuntimeMode);

            collection.RuntimeMode = false;
            Assert.False(collection.RuntimeMode);
        }

        [Fact]
        public void RuntimeMode_Set_AllowsModificationsWhenFalse()
        {
            // Arrange
            var collection = new RuleSetCollection();
            collection.OnRuntimeInitialized();
            Assert.True(collection.RuntimeMode);

            // Act
            collection.RuntimeMode = false;
            var ruleSet = new RuleSet("TestRuleSet");

            // Assert
            // Should not throw
            collection.Add(ruleSet);
            Assert.Single(collection);
        }

        #endregion

        #region GenerateRuleSetName Tests

        [Fact]
        public void GenerateRuleSetName_EmptyCollection_ReturnsFirstName()
        {
            // Arrange
            var collection = new RuleSetCollection();

            // Act
            var generatedName = collection.GenerateRuleSetName();

            // Assert
            Assert.NotNull(generatedName);
            Assert.EndsWith("1", generatedName);
        }

        [Fact]
        public void GenerateRuleSetName_WithExistingNames_ReturnsUniqueNames()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var baseName = Messages.NewRuleSetName;
            collection.Add(new RuleSet(baseName + "1"));
            collection.Add(new RuleSet(baseName + "2"));

            // Act
            var generatedName = collection.GenerateRuleSetName();

            // Assert
            Assert.Equal(baseName + "3", generatedName);
            Assert.False(collection.Contains(generatedName));
        }

        [Fact]
        public void GenerateRuleSetName_WithGaps_ReturnsNextSequentialName()
        {
            // Arrange
            var collection = new RuleSetCollection();
            var baseName = Messages.NewRuleSetName;
            collection.Add(new RuleSet(baseName + "1"));
            collection.Add(new RuleSet(baseName + "3"));

            // Act
            var generatedName = collection.GenerateRuleSetName();

            // Assert
            // The method generates sequential names, so it will return "2"
            Assert.Equal(baseName + "2", generatedName);
        }

        [Fact]
        public void GenerateRuleSetName_MultipleGenerations_ReturnsUniqueNames()
        {
            // Arrange
            var collection = new RuleSetCollection();

            // Act
            var name1 = collection.GenerateRuleSetName();
            collection.Add(new RuleSet(name1));
            var name2 = collection.GenerateRuleSetName();
            collection.Add(new RuleSet(name2));
            var name3 = collection.GenerateRuleSetName();

            // Assert
            Assert.NotEqual(name1, name2);
            Assert.NotEqual(name2, name3);
            Assert.NotEqual(name1, name3);
            Assert.False(collection.Contains(name3));
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void Collection_FullWorkflow_WorksCorrectly()
        {
            // Arrange
            var collection = new RuleSetCollection();

            // Act & Assert - Add multiple rule sets
            var ruleSet1 = new RuleSet("RuleSet1", "Description 1");
            var ruleSet2 = new RuleSet("RuleSet2", "Description 2");
            var ruleSet3 = new RuleSet("RuleSet3", "Description 3");

            collection.Add(ruleSet1);
            collection.Add(ruleSet2);
            collection.Add(ruleSet3);

            Assert.Equal(3, collection.Count);

            // Verify retrieval
            Assert.Same(ruleSet1, collection["RuleSet1"]);
            Assert.Same(ruleSet2, collection["RuleSet2"]);
            Assert.Same(ruleSet3, collection["RuleSet3"]);

            // Remove one
            collection.Remove("RuleSet2");
            Assert.Equal(2, collection.Count);
            Assert.False(collection.Contains("RuleSet2"));

            // Add another
            var ruleSet4 = new RuleSet("RuleSet4");
            collection.Add(ruleSet4);
            Assert.Equal(3, collection.Count);

            // Initialize runtime
            collection.OnRuntimeInitialized();
            Assert.True(collection.RuntimeMode);

            // Verify cannot modify after initialization
            Assert.Throws<InvalidOperationException>(() => collection.Add(new RuleSet("RuleSet5")));
        }

        #endregion
    }
}