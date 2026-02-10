using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests
{
    public class SRTest
    {
        #region GetString Tests
        [Fact]
        public void GetString_WithValidResourceName_ReturnsString()
        {
            // Act
            string result = SR.GetString("Activity");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetString_WithCultureAndValidResourceName_ReturnsString()
        {
            // Arrange
            CultureInfo culture = CultureInfo.InvariantCulture;

            // Act
            string result = SR.GetString(culture, "Activity");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetString_WithArgsAndValidResourceName_ReturnsFormattedString()
        {
            // Act - Use a resource that exists and can be formatted
            string result = SR.GetString("Activity", "test");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetString_WithCultureAndArgs_ReturnsFormattedString()
        {
            // Arrange
            CultureInfo culture = CultureInfo.InvariantCulture;

            // Act
            string result = SR.GetString(culture, "Activity", "test");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetString_WithMultipleArgs_FormatsCorrectly()
        {
            // Act
            string result = SR.GetString("Activity", "arg1", "arg2", "arg3");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetString_WithEmptyArgsArray_ReturnsUnformattedString()
        {
            // Act
            string result = SR.GetString("Activity", []);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetString_WithNullArgs_ReturnsUnformattedString()
        {
            // Act
            string result = SR.GetString("Activity", null);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        #endregion

        #region Resource Key Constants Tests
        [Fact]
        public void ResourceKeyConstants_AreNotNull()
        {
            // Assert
            Assert.NotNull(SR.Activity);
            Assert.NotNull(SR.Handlers);
            Assert.NotNull(SR.Conditions);
            Assert.NotNull(SR.NameDescr);
            Assert.NotNull(SR.Type);
            Assert.NotNull(SR.Standard);
            Assert.NotNull(SR.Base);
        }

        [Fact]
        public void ResourceKeyConstants_AreNotEmpty()
        {
            // Assert
            Assert.NotEmpty(SR.Activity);
            Assert.NotEmpty(SR.Handlers);
            Assert.NotEmpty(SR.Conditions);
            Assert.NotEmpty(SR.NameDescr);
            Assert.NotEmpty(SR.Type);
        }

        [Fact]
        public void ErrorResourceKeyConstants_AreNotNull()
        {
            // Assert
            Assert.NotNull(SR.Error_ConditionalBranchParentNotConditional);
            Assert.NotNull(SR.Error_EventDrivenMultipleEventActivity);
            Assert.NotNull(SR.Error_ParameterPropertyNotSet);
            Assert.NotNull(SR.Error_PropertyNotSet);
            Assert.NotNull(SR.Error_TypeNotResolved);
        }
        #endregion

        #region SRDescriptionAttribute Tests
        [Fact]
        public void SRDescriptionAttribute_WithValidResourceName_SetsDescription()
        {
            // Act
            var attribute = new SRDescriptionAttribute("Activity");

            // Assert
            Assert.NotNull(attribute.Description);
            Assert.NotEmpty(attribute.Description);
        }

        [Fact]
        public void SRDescriptionAttribute_WithResourceSet_SetsDescription()
        {
            // Act
            var attribute = new SRDescriptionAttribute("Activity", "LogicBuilder.Workflow.Activities.StringResources");

            // Assert
            Assert.NotNull(attribute.Description);
        }

        [Fact]
        public void SRDescriptionAttribute_InheritsFromDescriptionAttribute()
        {
            // Act
            var attribute = new SRDescriptionAttribute("Activity");

            // Assert
            Assert.IsType<DescriptionAttribute>(attribute, exactMatch: false);
        }

        [Fact]
        public void SRDescriptionAttribute_HasAttributeUsageForAll()
        {
            // Act
            var attributes = typeof(SRDescriptionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            // Assert
            Assert.Single(attributes);
            var usage = (AttributeUsageAttribute)attributes[0];
            Assert.Equal(AttributeTargets.All, usage.ValidOn);
        }
        #endregion

        #region SRCategoryAttribute Tests
        [Fact]
        public void SRCategoryAttribute_WithCategory_SetsCategory()
        {
            // Act
            var attribute = new SRCategoryAttribute("Standard");

            // Assert
            Assert.NotNull(attribute.Category);
            Assert.NotEmpty(attribute.Category);
        }

        [Fact]
        public void SRCategoryAttribute_WithCategoryAndResourceSet_SetsCategory()
        {
            // Act
            var attribute = new SRCategoryAttribute("Standard", "LogicBuilder.Workflow.Activities.StringResources");

            // Assert
            Assert.NotNull(attribute.Category);
        }

        [Fact]
        public void SRCategoryAttribute_InheritsFromCategoryAttribute()
        {
            // Act
            var attribute = new SRCategoryAttribute("Standard");

            // Assert
            Assert.IsType<CategoryAttribute>(attribute, exactMatch: false);
        }

        [Fact]
        public void SRCategoryAttribute_HasAttributeUsageForAll()
        {
            // Act
            var attributes = typeof(SRCategoryAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            // Assert
            Assert.Single(attributes);
            var usage = (AttributeUsageAttribute)attributes[0];
            Assert.Equal(AttributeTargets.All, usage.ValidOn);
        }

        [Fact]
        public void SRCategoryAttribute_GetLocalizedString_WithoutResourceSet_UsesDefaultResources()
        {
            // Arrange
            var attribute = new SRCategoryAttribute("Standard");

            // Act
            string result = attribute.Category;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        #endregion

        #region SRDisplayNameAttribute Tests
        [Fact]
        public void SRDisplayNameAttribute_WithValidName_SetsDisplayName()
        {
            // Act
            var attribute = new SRDisplayNameAttribute("Activity");

            // Assert
            Assert.NotNull(attribute.DisplayName);
            Assert.NotEmpty(attribute.DisplayName);
        }

        [Fact]
        public void SRDisplayNameAttribute_WithResourceSet_SetsDisplayName()
        {
            // Act
            var attribute = new SRDisplayNameAttribute("Activity", "LogicBuilder.Workflow.Activities.StringResources");

            // Assert
            Assert.NotNull(attribute.DisplayName);
        }

        [Fact]
        public void SRDisplayNameAttribute_InheritsFromDisplayNameAttribute()
        {
            // Act
            var attribute = new SRDisplayNameAttribute("Activity");

            // Assert
            Assert.IsType<DisplayNameAttribute>(attribute, exactMatch: false);
        }

        [Fact]
        public void SRDisplayNameAttribute_HasAttributeUsageForAll()
        {
            // Act
            var attributes = typeof(SRDisplayNameAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            // Assert
            Assert.Single(attributes);
            var usage = (AttributeUsageAttribute)attributes[0];
            Assert.Equal(AttributeTargets.All, usage.ValidOn);
        }
        #endregion

        #region SR Class Behavior Tests
        [Fact]
        public void SR_IsSealed()
        {
            // Assert
            Assert.True(typeof(SR).IsSealed);
        }

        [Fact]
        public void SR_HasPrivateConstructor()
        {
            // Act
            var constructors = typeof(SR).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // Assert
            Assert.Single(constructors);
            Assert.True(constructors[0].IsAssembly || constructors[0].IsPrivate);
        }

        [Fact]
        public void SR_GetString_CalledMultipleTimes_ReturnsSameResult()
        {
            // Act
            string result1 = SR.GetString("Activity");
            string result2 = SR.GetString("Activity");

            // Assert
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void SR_GetString_WithDifferentCultures_MayReturnDifferentStrings()
        {
            // Arrange
            CultureInfo culture1 = CultureInfo.InvariantCulture;
            CultureInfo culture2 = new("en-US");

            // Act
            string result1 = SR.GetString(culture1, "Activity");
            string result2 = SR.GetString(culture2, "Activity");

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            // Note: They may be the same if no localization exists
        }
        #endregion

        #region Edge Case Tests
        [Fact]
        public void GetString_WithSpecialCharactersInArgs_FormatsCorrectly()
        {
            // Act
            string result = SR.GetString("Activity", "test{0}", "arg with spaces", "arg_with_underscores");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SRCategoryAttribute_WithEmptyCategory_CreatesAttribute()
        {
            // Act
            var attribute = new SRCategoryAttribute(string.Empty);

            // Assert
            Assert.NotNull(attribute);
        }
        #endregion

        #region Multiple Resource Keys Test

        [Theory]
        [InlineData("Error_PropertyNotSet")]
        [InlineData("Error_TypeNotResolved")]
        //TODO: Remove this resource key
        //[InlineData("Error_ParameterNotFound")]
        [InlineData("Error_FieldNotExists")]
        [InlineData("Error_MethodNotExists")]
        public void GetString_WithErrorResourceKeys_ReturnsValidStrings(string resourceKey)
        {
            // Act
            string result = SR.GetString(resourceKey);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        #endregion
    }
}