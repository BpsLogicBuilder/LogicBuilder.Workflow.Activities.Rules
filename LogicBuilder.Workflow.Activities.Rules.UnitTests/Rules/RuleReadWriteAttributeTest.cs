using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleReadWriteAttributeTest
    {
        #region Helper Classes
        
        // Test classes for validation scenarios
        public class TestClass
        {
            public string? StringField;
            public int IntProperty { get; set; }
            public TestNestedClass? NestedObject { get; set; }
            public string[]? StringArray { get; set; }
        }

        public class TestNestedClass
        {
            public string? NestedField;
            public int NestedProperty { get; set; }
        }

        public class TestClassWithMethod
        {
#pragma warning disable CA1822 // Disable warning for static method suggestion
            [RuleRead("StringField")]
            public void MethodWithReadAttribute() { }

            [RuleWrite("IntProperty")]
            public void MethodWithWriteAttribute() { }

            [RuleRead("NestedObject/NestedField")]
            public void MethodWithNestedPath() { }

#pragma warning disable IDE0060 // Unused parameter for testing purposes
            [RuleRead("param1", RuleAttributeTarget.Parameter)]
            public void MethodWithParameter(string param1) { }
#pragma warning restore IDE0060

            [RuleRead("this/StringField")]
            public void MethodWithThisPrefix() { }

            [RuleRead("")]
            public void MethodWithEmptyPath() { }

            [RuleRead("*")]
            public void MethodWithWildcard() { }

            [RuleWrite("NestedObject/*")]
            public void MethodWithTrailingWildcard() { }
#pragma warning restore CA1822

            public string? StringField;
            public int IntProperty { get; set; }
            public TestNestedClass? NestedObject { get; set; }
        }

        #endregion

        #region Constructor and Property Tests

        [Fact]
        public void Constructor_WithPathAndTarget_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute("testPath", RuleAttributeTarget.Parameter);

            // Assert
            Assert.Equal("testPath", attribute.Path);
            Assert.Equal(RuleAttributeTarget.Parameter, attribute.Target);
        }

        [Fact]
        public void Constructor_WithPathOnly_SetsTargetToThis()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute("testPath");

            // Assert
            Assert.Equal("testPath", attribute.Path);
            Assert.Equal(RuleAttributeTarget.This, attribute.Target);
        }

        [Fact]
        public void Constructor_WithNullPath_AcceptsNull()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute(null);

            // Assert
            Assert.Null(attribute.Path);
            Assert.Equal(RuleAttributeTarget.This, attribute.Target);
        }

        [Fact]
        public void Constructor_WithEmptyPath_AcceptsEmptyString()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute("");

            // Assert
            Assert.Equal("", attribute.Path);
            Assert.Equal(RuleAttributeTarget.This, attribute.Target);
        }

        #endregion

        #region Validate Method Tests

        [Fact]
        public void Validate_WithNullPath_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute(null);
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithEmptyPath");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithEmptyPath_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithEmptyPath");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithValidFieldPath_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("StringField");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithReadAttribute");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithValidPropertyPath_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("IntProperty");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithWriteAttribute");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithNestedPath_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("NestedObject/NestedField");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithNestedPath");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithInvalidFieldName_ReturnsFalse()
        {
            // Arrange
            var attribute = new RuleReadAttribute("NonExistentField");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithReadAttribute");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithThisPrefix_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("this/StringField");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithThisPrefix");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithTrailingWildcard_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleWriteAttribute("NestedObject/*");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithTrailingWildcard");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithWildcardOnly_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("*");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithWildcard");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithEmbeddedWildcard_ReturnsFalse()
        {
            // Arrange
            var attribute = new RuleReadAttribute("NestedObject/*/NestedField");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithNestedPath");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithParameterTarget_ValidParameter_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("param1", RuleAttributeTarget.Parameter);
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithParameter");
            var parameters = method?.GetParameters();

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), parameters);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithParameterTarget_InvalidParameter_ReturnsFalse()
        {
            // Arrange
            var attribute = new RuleReadAttribute("nonExistentParam", RuleAttributeTarget.Parameter);
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithParameter");
            var parameters = method?.GetParameters();

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), parameters);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithArrayType_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("StringArray");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithReadAttribute");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithTrailingSlash_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleReadAttribute("NestedObject/");
            var validation = new RuleValidation(typeof(TestClass), null);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithNestedPath");

            // Act
            var result = attribute.Validate(validation, method, typeof(TestClass), []);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region RuleReadAttribute Specific Tests

        [Fact]
        public void RuleReadAttribute_Constructor_SetsPathAndTarget()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute("testPath", RuleAttributeTarget.Parameter);

            // Assert
            Assert.Equal("testPath", attribute.Path);
            Assert.Equal(RuleAttributeTarget.Parameter, attribute.Target);
        }

        [Fact]
        public void RuleReadAttribute_DefaultConstructor_SetsTargetToThis()
        {
            // Arrange & Act
            var attribute = new RuleReadAttribute("testPath");

            // Assert
            Assert.Equal(RuleAttributeTarget.This, attribute.Target);
        }

        #endregion

        #region RuleWriteAttribute Specific Tests

        [Fact]
        public void RuleWriteAttribute_Constructor_SetsPathAndTarget()
        {
            // Arrange & Act
            var attribute = new RuleWriteAttribute("testPath", RuleAttributeTarget.Parameter);

            // Assert
            Assert.Equal("testPath", attribute.Path);
            Assert.Equal(RuleAttributeTarget.Parameter, attribute.Target);
        }

        [Fact]
        public void RuleWriteAttribute_DefaultConstructor_SetsTargetToThis()
        {
            // Arrange & Act
            var attribute = new RuleWriteAttribute("testPath");

            // Assert
            Assert.Equal(RuleAttributeTarget.This, attribute.Target);
        }

        #endregion

        #region Analyze Method Tests

        [Fact]
        public void Analyze_RuleReadAttribute_WithForWrites_DoesNotAnalyze()
        {
            // Arrange
            var attribute = new RuleReadAttribute("StringField");
            var analysis = new RuleAnalysis(new RuleValidation(typeof(TestClass), null), true); // ForWrites = true
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithReadAttribute");
            var targetExpression = new CodeThisReferenceExpression();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, method, targetExpression, null, [], [], attributedExpressions);

            // Assert
            Assert.Empty(attributedExpressions);
        }

        [Fact]
        public void Analyze_RuleWriteAttribute_WithForReads_DoesNotAnalyze()
        {
            // Arrange
            var attribute = new RuleWriteAttribute("StringField");
            var analysis = new RuleAnalysis(new RuleValidation(typeof(TestClass), null), false); // ForWrites = false
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithWriteAttribute");
            var targetExpression = new CodeThisReferenceExpression();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, method, targetExpression, null, [], [], attributedExpressions);

            // Assert
            Assert.Empty(attributedExpressions);
        }

        [Fact]
        public void Analyze_WithEmptyPath_ThisTarget_AddsTargetExpression()
        {
            // Arrange
            var attribute = new RuleReadAttribute("");
            var analysis = new RuleAnalysis(new RuleValidation(typeof(TestClass), null), false);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithEmptyPath");
            var targetExpression = new CodeThisReferenceExpression();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, method, targetExpression, null, [], [], attributedExpressions);

            // Assert
            Assert.Single(attributedExpressions);
            Assert.Same(targetExpression, attributedExpressions[0]);
        }

        [Fact]
        public void Analyze_WithEmptyPath_ParameterTarget_AddsAllArguments()
        {
            // Arrange
            var attribute = new RuleReadAttribute("", RuleAttributeTarget.Parameter);
            var analysis = new RuleAnalysis(new RuleValidation(typeof(TestClass), null), false);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithParameter");
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection
            {
                new CodeVariableReferenceExpression("arg1"),
                new CodeVariableReferenceExpression("arg2")
            };
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, method, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert
            Assert.Equal(2, attributedExpressions.Count);
        }

        [Fact]
        public void Analyze_WithPath_ThisTarget_AddsTargetExpression()
        {
            // Arrange
            var attribute = new RuleReadAttribute("StringField");
            var analysis = new RuleAnalysis(new RuleValidation(typeof(TestClass), null), false);
            var method = typeof(TestClassWithMethod).GetMethod("MethodWithReadAttribute");
            var targetExpression = new CodeThisReferenceExpression();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, method, targetExpression, null, [], [], attributedExpressions);

            // Assert
            Assert.Single(attributedExpressions);
            Assert.Same(targetExpression, attributedExpressions[0]);
        }

        #endregion
    }
}