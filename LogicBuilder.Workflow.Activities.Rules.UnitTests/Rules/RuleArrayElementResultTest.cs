using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleArrayElementResultTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidArguments_CreatesInstance()
        {
            // Arrange
            int[] array = [1, 2, 3];
            long[] indices = [0];

            // Act
            RuleArrayElementResult result = new(array, indices);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Constructor_WithNullArray_ThrowsArgumentNullException()
        {
            // Arrange
            Array array = null!;
            long[] indices = [0];

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => 
                new RuleArrayElementResult(array, indices));
            Assert.Equal("targetArray", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithNullIndices_ThrowsArgumentNullException()
        {
            // Arrange
            int[] array = [1, 2, 3];
            long[] indices = null!;

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => 
                new RuleArrayElementResult(array, indices));
            Assert.Equal("indexerArguments", exception.ParamName);
        }

        #endregion

        #region Value Getter Tests

        [Fact]
        public void Value_Get_ReturnsCorrectElementFromIntArray()
        {
            // Arrange
            int[] array = [10, 20, 30, 40, 50];
            long[] indices = [2];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal(30, value);
        }

        [Fact]
        public void Value_Get_ReturnsCorrectElementFromStringArray()
        {
            // Arrange
            string[] array = ["first", "second", "third"];
            long[] indices = [1];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal("second", value);
        }

        [Fact]
        public void Value_Get_ReturnsCorrectElementFromMultiDimensionalArray()
        {
            // Arrange
            int[,] array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            long[] indices = [1, 2];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal(6, value);
        }

        [Fact]
        public void Value_Get_ReturnsCorrectElementFrom3DArray()
        {
            // Arrange
            int[,,] array = new int[2, 2, 2];
            array[1, 0, 1] = 42;
            long[] indices = [1, 0, 1];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal(42, value);
        }

        [Fact]
        public void Value_Get_ReturnsFirstElement()
        {
            // Arrange
            double[] array = [1.5, 2.5, 3.5];
            long[] indices = [0];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal(1.5, value);
        }

        [Fact]
        public void Value_Get_ReturnsLastElement()
        {
            // Arrange
            double[] array = [1.5, 2.5, 3.5];
            long[] indices = [2];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;

            // Assert
            Assert.Equal(3.5, value);
        }

        #endregion

        #region Value Setter Tests

        [Fact]
        public void Value_Set_UpdatesIntArrayElement()
        {
            // Arrange
            int[] array = [10, 20, 30];
            long[] indices = [1];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = 99
            };

            // Assert
            Assert.Equal(99, array[1]);
        }

        [Fact]
        public void Value_Set_UpdatesStringArrayElement()
        {
            // Arrange
            string[] array = ["first", "second", "third"];
            long[] indices = [2];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = "updated"
            };

            // Assert
            Assert.Equal("updated", array[2]);
        }

        [Fact]
        public void Value_Set_UpdatesMultiDimensionalArrayElement()
        {
            // Arrange
            int[,] array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            long[] indices = [0, 2];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = 100
            };

            // Assert
            Assert.Equal(100, array[0, 2]);
        }

        [Fact]
        public void Value_Set_Updates3DArrayElement()
        {
            // Arrange
            int[,,] array = new int[2, 2, 2];
            long[] indices = [1, 1, 0];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = 77
            };

            // Assert
            Assert.Equal(77, array[1, 1, 0]);
        }

        [Fact]
        public void Value_Set_UpdatesFirstElement()
        {
            // Arrange
            bool[] array = [false, false, false];
            long[] indices = [0];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = true
            };

            // Assert
            Assert.True(array[0]);
        }

        [Fact]
        public void Value_Set_UpdatesLastElement()
        {
            // Arrange
            bool[] array = [false, false, false];
            long[] indices = [2];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = true
            };

            // Assert
            Assert.True(array[2]);
        }

        [Fact]
        public void Value_Set_WithNull_UpdatesReferenceTypeArrayElement()
        {
            // Arrange
            string[] array = ["first", "second", "third"];
            long[] indices = [1];
            _ = new RuleArrayElementResult(array, indices)
            {
                // Act
                Value = null
            };

            // Assert
            Assert.Null(array[1]);
        }

        #endregion

        #region Get and Set Integration Tests

        [Fact]
        public void Value_GetAfterSet_ReturnsUpdatedValue()
        {
            // Arrange
            int[] array = [1, 2, 3, 4, 5];
            long[] indices = [3];
            RuleArrayElementResult result = new(array, indices)
            {
                // Act
                Value = 999
            };
            object value = result.Value;

            // Assert
            Assert.Equal(999, value);
        }

        [Fact]
        public void Value_MultipleSetOperations_UpdatesCorrectly()
        {
            // Arrange
            int[] array = [0, 0, 0, 0];
            long[] indices = [2];
            RuleArrayElementResult result = new(array, indices)
            {
                // Act
                Value = 10
            };
            result.Value = 20;
            result.Value = 30;

            // Assert
            Assert.Equal(30, array[2]);
            Assert.Equal(30, result.Value);
        }

        #endregion

        #region Different Array Types Tests

        [Fact]
        public void Value_WorksWithDecimalArray()
        {
            // Arrange
            decimal[] array = [10.5m, 20.75m, 30.25m];
            long[] indices = [1];
            RuleArrayElementResult result = new(array, indices)
            {
                // Act
                Value = 99.99m
            };

            // Assert
            Assert.Equal(99.99m, result.Value);
            Assert.Equal(99.99m, array[1]);
        }

        [Fact]
        public void Value_WorksWithObjectArray()
        {
            // Arrange
            object[] array = [1, "test", 3.14];
            long[] indices = [1];
            RuleArrayElementResult result = new(array, indices);

            // Act
            object value = result.Value;
            result.Value = "updated";

            // Assert
            Assert.Equal("test", value);
            Assert.Equal("updated", array[1]);
        }

        [Fact]
        public void Value_WorksWithCustomTypeArray()
        {
            // Arrange
            TestClass[] array =
            [
                new TestClass { Id = 1 }, 
                new TestClass { Id = 2 }, 
                new TestClass { Id = 3 } 
            ];
            long[] indices = [0];
            RuleArrayElementResult result = new(array, indices);

            // Act
            TestClass value = (TestClass)result.Value;
            result.Value = new TestClass { Id = 10 };

            // Assert
            Assert.Equal(1, value.Id);
            Assert.Equal(10, array[0].Id);
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int Id { get; set; }
        }

        #endregion
    }
}