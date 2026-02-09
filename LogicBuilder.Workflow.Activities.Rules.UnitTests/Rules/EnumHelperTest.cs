using System;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class EnumHelperTest
    {
        #region Test Enums

        private enum TestByteEnum : byte
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestShortEnum : short
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestIntEnum : int
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestLongEnum : long
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestSByteEnum : sbyte
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestUShortEnum : ushort
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestUIntEnum : uint
        {
            Value1 = 1,
            Value2 = 2
        }

        private enum TestULongEnum : ulong
        {
            Value1 = 1,
            Value2 = 2
        }

        #endregion

        #region GetUnderlyingType Tests

        [Fact]
        public void GetUnderlyingType_WithByteEnum_ReturnsByteType()
        {
            // Arrange
            Type enumType = typeof(TestByteEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(byte), result);
        }

        [Fact]
        public void GetUnderlyingType_WithShortEnum_ReturnsShortType()
        {
            // Arrange
            Type enumType = typeof(TestShortEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(short), result);
        }

        [Fact]
        public void GetUnderlyingType_WithIntEnum_ReturnsIntType()
        {
            // Arrange
            Type enumType = typeof(TestIntEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(int), result);
        }

        [Fact]
        public void GetUnderlyingType_WithLongEnum_ReturnsLongType()
        {
            // Arrange
            Type enumType = typeof(TestLongEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(long), result);
        }

        [Fact]
        public void GetUnderlyingType_WithSByteEnum_ReturnsSByteType()
        {
            // Arrange
            Type enumType = typeof(TestSByteEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(sbyte), result);
        }

        [Fact]
        public void GetUnderlyingType_WithUShortEnum_ReturnsUShortType()
        {
            // Arrange
            Type enumType = typeof(TestUShortEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(ushort), result);
        }

        [Fact]
        public void GetUnderlyingType_WithUIntEnum_ReturnsUIntType()
        {
            // Arrange
            Type enumType = typeof(TestUIntEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(uint), result);
        }

        [Fact]
        public void GetUnderlyingType_WithULongEnum_ReturnsULongType()
        {
            // Arrange
            Type enumType = typeof(TestULongEnum);

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(ulong), result);
        }

        [Fact]
        public void GetUnderlyingType_WithDefaultEnum_ReturnsIntType()
        {
            // Arrange
            Type enumType = typeof(DayOfWeek); // Standard enum with int as underlying type

            // Act
            Type result = EnumHelper.GetUnderlyingType(enumType);

            // Assert
            Assert.Equal(typeof(int), result);
        }

        #endregion
    }
}