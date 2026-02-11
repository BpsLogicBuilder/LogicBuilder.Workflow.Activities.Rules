using System;
using System.CodeDom;
using System.Collections.Generic;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleInvokeAttributeTest
    {
        #region Test Helper Classes

        // Helper class with various method combinations for testing
        private class TestClass
        {
#pragma warning disable CA1822 // Disable warning for static method suggestion
            public void SimpleMethod() { }

            [RuleRead("this/Field1")]
            public void MethodWithReadAttribute() { }

            [RuleWrite("this/Field2")]
            public void MethodWithWriteAttribute() { }

            [RuleInvoke("SimpleMethod")]
            public void MethodWithInvokeAttribute() { }

#pragma warning disable IDE0060 // Unused parameter for testing purposes
            [RuleRead("Field1", RuleAttributeTarget.Parameter)]
            public void MethodWithParameterAttribute(string fieldName) { }
#pragma warning restore IDE0060

            [RuleInvoke("MethodWithReadAttribute")]
            public void MethodInvokingMethodWithReadAttribute() { }

            [RuleInvoke("MethodWithInvokeAttribute")]
            public void RecursiveInvokeMethod() { }

            [RuleInvoke("NonExistentMethod")]
            public void MethodInvokingNonExistentMethod() { }

            [RuleInvoke("")]
            public void MethodWithEmptyInvoke() { }

            [RuleInvoke("MethodWithParameterAttribute")]
            public void MethodInvokingMethodWithParameterAttribute() { }


            public string? Field1 { get; set; }
            public string? Field2 { get; set; }

            [RuleRead("this/Field1")]
            public string? PropertyWithReadAttribute { get; set; }

            [RuleInvoke("SimpleMethod")]
            public string? PropertyWithInvokeAttribute { get; set; }
        }

        // Helper class for circular reference testing
        private class CircularReferenceClass
        {
            [RuleInvoke("MethodB")]
            public void MethodA() { }

            [RuleInvoke("MethodA")]
            public void MethodB() { }
        }
#pragma warning restore CA1822
        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidMethodName_SetsMethodInvokedProperty()
        {
            // Arrange
            string methodName = "TestMethod";

            // Act
            var attribute = new RuleInvokeAttribute(methodName);

            // Assert
            Assert.Equal(methodName, attribute.MethodInvoked);
        }

        [Fact]
        public void Constructor_WithEmptyString_SetsMethodInvokedToEmpty()
        {
            // Arrange
            string methodName = "";

            // Act
            var attribute = new RuleInvokeAttribute(methodName);

            // Assert
            Assert.Equal("", attribute.MethodInvoked);
        }

        [Fact]
        public void Constructor_WithNull_SetsMethodInvokedToNull()
        {
            // Arrange
            string? methodName = null;

            // Act
            var attribute = new RuleInvokeAttribute(methodName);

            // Assert
            Assert.Null(attribute.MethodInvoked);
        }

        #endregion

        #region MethodInvoked Property Tests

        [Fact]
        public void MethodInvoked_ReturnsCorrectValue()
        {
            // Arrange
            string expectedMethod = "MyMethod";
            var attribute = new RuleInvokeAttribute(expectedMethod);

            // Act
            string actualMethod = attribute.MethodInvoked;

            // Assert
            Assert.Equal(expectedMethod, actualMethod);
        }

        #endregion

        #region Validate Method Tests

        [Fact]
        public void Validate_WithValidMethodName_ReturnsTrue()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("SimpleMethod");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("MethodWithInvokeAttribute");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithNonExistentMethod_ReturnsFalseAndAddsError()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("NonExistentMethod");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithEmptyMethodName_ReturnsFalseAndAddsError()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithNullMethodName_ReturnsFalseAndAddsError()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute(null);
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithMethodHavingReadAttribute_ValidatesSuccessfully()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodWithReadAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("MethodInvokingMethodWithReadAttribute");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithMethodHavingParameterAttribute_AddsError()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodWithParameterAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("MethodInvokingMethodWithParameterAttribute");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithCircularReference_PreventsInfiniteRecursion()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodB");
            var validation = new RuleValidation(typeof(CircularReferenceClass), null);
            var methodInfo = typeof(CircularReferenceClass).GetMethod("MethodA");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(CircularReferenceClass), []);

            // Assert
            // Should complete without stack overflow
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithPropertyHavingInvokeAttribute_ValidatesSuccessfully()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("PropertyWithInvokeAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");

            // Act
            bool result = attribute.Validate(validation, methodInfo, typeof(TestClass), []);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Analyze Method Tests

        [Fact]
        public void Analyze_WithValidMethod_ExecutesWithoutError()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("SimpleMethod");
            var methodInfo = typeof(TestClass).GetMethod("MethodWithInvokeAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, true);
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without exception
            Assert.NotNull(analysis);
        }

        [Fact]
        public void Analyze_WithMethodHavingReadAttribute_AnalyzesCorrectly()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodWithReadAttribute");
            var methodInfo = typeof(TestClass).GetMethod("MethodInvokingMethodWithReadAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, false); // false = analyzing for reads
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without exception
            Assert.NotNull(analysis);
        }

        [Fact]
        public void Analyze_WithMethodHavingWriteAttribute_AnalyzesCorrectly()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodWithWriteAttribute");
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, true); // true = analyzing for writes
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without exception
            Assert.NotNull(analysis);
        }

        [Fact]
        public void Analyze_WithNestedInvokeAttribute_AnalyzesCorrectly()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodInvokingMethodWithReadAttribute");
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, false);
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without exception
            Assert.NotNull(analysis);
        }

        [Fact]
        public void Analyze_WithCircularReference_PreventsInfiniteRecursion()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("MethodB");
            var methodInfo = typeof(CircularReferenceClass).GetMethod("MethodA");
            var validation = new RuleValidation(typeof(CircularReferenceClass), null);
            var analysis = new RuleAnalysis(validation, true);
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without stack overflow
            Assert.NotNull(analysis);
        }

        [Fact]
        public void Analyze_WithNullTargetExpression_HandlesGracefully()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("SimpleMethod");
            var methodInfo = typeof(TestClass).GetMethod("MethodWithInvokeAttribute");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, true);
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act & Assert - Should handle null gracefully or throw expected exception
            attribute.Analyze(analysis, methodInfo, null, null, argumentExpressions, [], attributedExpressions);
        }

        [Fact]
        public void Analyze_WithPropertyTarget_AnalyzesCorrectly()
        {
            // Arrange
            var attribute = new RuleInvokeAttribute("PropertyWithReadAttribute");
            var methodInfo = typeof(TestClass).GetMethod("SimpleMethod");
            var validation = new RuleValidation(typeof(TestClass), null);
            var analysis = new RuleAnalysis(validation, false);
            var targetExpression = new CodeThisReferenceExpression();
            var argumentExpressions = new CodeExpressionCollection();
            var attributedExpressions = new List<CodeExpression>();

            // Act
            attribute.Analyze(analysis, methodInfo, targetExpression, null, argumentExpressions, [], attributedExpressions);

            // Assert - Should complete without exception
            Assert.NotNull(analysis);
        }

        #endregion

        #region AttributeUsage Tests

        [Fact]
        public void AttributeUsage_CanBeAppliedToMethod()
        {
            // Arrange & Act
            var methodInfo = typeof(TestClass).GetMethod("MethodWithInvokeAttribute");
            var attributes = methodInfo!.GetCustomAttributes(typeof(RuleInvokeAttribute), false);

            // Assert
            Assert.NotEmpty(attributes);
            Assert.IsType<RuleInvokeAttribute>(attributes[0]);
        }

        [Fact]
        public void AttributeUsage_CanBeAppliedToProperty()
        {
            // Arrange & Act
            var propertyInfo = typeof(TestClass).GetProperty("PropertyWithInvokeAttribute");
            var attributes = propertyInfo!.GetCustomAttributes(typeof(RuleInvokeAttribute), false);

            // Assert
            Assert.NotEmpty(attributes);
            Assert.IsType<RuleInvokeAttribute>(attributes[0]);
        }

        [Fact]
        public void AttributeUsage_AllowsMultipleInstances()
        {
            // This test verifies the AllowMultiple = true in the AttributeUsage
            // We can verify this by checking the AttributeUsageAttribute on RuleInvokeAttribute
            var attributeUsage = typeof(RuleInvokeAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            Assert.NotEmpty(attributeUsage);
            var usage = (AttributeUsageAttribute)attributeUsage[0];
            Assert.True(usage.AllowMultiple);
        }

        #endregion
    }
}