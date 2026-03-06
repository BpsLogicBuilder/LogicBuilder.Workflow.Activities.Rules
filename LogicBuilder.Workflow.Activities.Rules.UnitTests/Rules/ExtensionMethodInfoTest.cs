using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Globalization;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ExtensionMethodInfoTest
    {
        #region Helper Classes and Extension Methods

        public class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; }
        }

        public class TestResult
        {
            public int ResultValue { get; set; }
            public string? ResultName { get; set; }
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidExtensionMethod_CreatesInstance()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();

            // Act
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Assert
            Assert.NotNull(extensionMethodInfo);
            Assert.Equal(extensionMethod.Name, extensionMethodInfo.Name);
            Assert.Equal(extensionMethod.ReturnType, extensionMethodInfo.ReturnType);
        }

        [Fact]
        public void Constructor_WithNoParameters_CreatesEmptyParameterList()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("NoParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();

            // Act
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Assert
            Assert.NotNull(extensionMethodInfo);
            Assert.Empty(extensionMethodInfo.GetParameters());
        }

        [Fact]
        public void Constructor_WithMultipleParameters_RemovesFirstParameter()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("MultiParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();

            // Act
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Assert
            Assert.NotNull(extensionMethodInfo);
            Assert.Equal(2, extensionMethodInfo.GetParameters().Length);
        }

        [Fact]
        public void Constructor_WithRefParameter_DetectsRefParameters()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("RefParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();

            // Act
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Assert
            Assert.NotNull(extensionMethodInfo);
            Assert.Single(extensionMethodInfo.GetParameters());
        }

        [Fact]
        public void Constructor_WithOutParameter_DetectsOutParameters()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("OutParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();

            // Act
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Assert
            Assert.NotNull(extensionMethodInfo);
            Assert.Single(extensionMethodInfo.GetParameters());
        }

        #endregion

        #region Property Tests

        [Fact]
        public void AssumedDeclaringType_ReturnsFirstParameterType()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            Type assumedType = extensionMethodInfo.AssumedDeclaringType;

            // Assert
            Assert.Equal(typeof(TestClass), assumedType);
        }

        [Fact]
        public void DeclaringType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            Type declaringType = extensionMethodInfo.DeclaringType;

            // Assert
            Assert.Equal(typeof(TestExtensions), declaringType);
        }

        [Fact]
        public void ReflectedType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            Type reflectedType = extensionMethodInfo.ReflectedType;

            // Assert
            Assert.Equal(typeof(TestExtensions), reflectedType);
        }

        [Fact]
        public void ReturnType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            Type returnType = extensionMethodInfo.ReturnType;

            // Assert
            Assert.Equal(typeof(string), returnType);
        }

        [Fact]
        public void Name_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            string name = extensionMethodInfo.Name;

            // Assert
            Assert.Equal("SimpleExtension", name);
        }

        [Fact]
        public void Attributes_RemovesStaticFlag()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            MethodAttributes attributes = extensionMethodInfo.Attributes;

            // Assert
            Assert.False(attributes.HasFlag(MethodAttributes.Static));
        }

        [Fact]
        public void MethodHandle_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            RuntimeMethodHandle handle = extensionMethodInfo.MethodHandle;

            // Assert
            Assert.Equal(extensionMethod.MethodHandle, handle);
        }

        [Fact]
        public void ReturnTypeCustomAttributes_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            var customAttributes = extensionMethodInfo.ReturnTypeCustomAttributes;

            // Assert
            Assert.NotNull(customAttributes);
            Assert.Equal(extensionMethod.ReturnTypeCustomAttributes, customAttributes);
        }

        #endregion

        #region Method Tests

        [Fact]
        public void GetBaseDefinition_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            MethodInfo baseDefinition = extensionMethodInfo.GetBaseDefinition();

            // Assert
            Assert.NotNull(baseDefinition);
            Assert.Equal(extensionMethod.GetBaseDefinition(), baseDefinition);
        }

        [Fact]
        public void GetMethodImplementationFlags_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            MethodImplAttributes implFlags = extensionMethodInfo.GetMethodImplementationFlags();

            // Assert
            Assert.Equal(extensionMethod.GetMethodImplementationFlags(), implFlags);
        }

        [Fact]
        public void GetParameters_ReturnsModifiedParameters()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("MultiParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            ParameterInfo[] resultParams = extensionMethodInfo.GetParameters();

            // Assert
            Assert.Equal(2, resultParams.Length);
            Assert.Equal(typeof(int), resultParams[0].ParameterType);
            Assert.Equal(typeof(string), resultParams[1].ParameterType);
        }

        [Fact]
        public void GetCustomAttributes_WithType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            object[] attributes = extensionMethodInfo.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.NotNull(attributes);
        }

        [Fact]
        public void GetCustomAttributes_WithoutType_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            object[] attributes = extensionMethodInfo.GetCustomAttributes(false);

            // Assert
            Assert.NotNull(attributes);
        }

        [Fact]
        public void IsDefined_ReturnsDelegatedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            bool isDefined = extensionMethodInfo.IsDefined(typeof(ObsoleteAttribute), false);

            // Assert
            Assert.False(isDefined);
        }

        #endregion

        #region Invoke Tests

        [Fact]
        public void Invoke_WithSimpleExtensionMethod_InvokesCorrectly()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("SimpleExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 42, Name = "Test" };

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, [], CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("Extension: Test", result);
        }

        [Fact]
        public void Invoke_WithMultipleParameters_InvokesCorrectly()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("MultiParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 10, Name = "Test" };

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, [5, "Suffix"], CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(15, result);
            Assert.Equal("TestSuffix", testObj.Name);
        }

        [Fact]
        public void Invoke_WithNullObject_PassesNullToActualMethod()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("NullableExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);

            // Act
            object result = extensionMethodInfo.Invoke(null, BindingFlags.Default, null, [], CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("null", result);
        }

        [Fact]
        public void Invoke_WithRefParameter_CopiesBackModifiedValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("RefParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 10 };
            object[] args = [20];

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, args, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(30, result);
            Assert.Equal(60, args[0]); // ref parameter should be modified (result * 2 = 30 * 2)
        }

        [Fact]
        public void Invoke_WithOutParameter_SetsOutValue()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("OutParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 15 };
            object[] args = [0];

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, args, CultureInfo.InvariantCulture);

            // Assert
            Assert.True((bool)result);
            Assert.Equal(30, args[0]); // out parameter should be set
        }

        [Fact]
        public void Invoke_WithNoAdditionalParameters_InvokesCorrectly()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("NoExtraParamExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 100 };

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, [], CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(200, result);
        }

        [Fact]
        public void Invoke_WithComplexReturnType_ReturnsCorrectly()
        {
            // Arrange
            MethodInfo extensionMethod = typeof(TestExtensions).GetMethod("ComplexReturnExtension", BindingFlags.Public | BindingFlags.Static)!;
            ParameterInfo[] parameters = extensionMethod.GetParameters();
            var extensionMethodInfo = new ExtensionMethodInfo(extensionMethod, parameters);
            var testObj = new TestClass { Value = 50, Name = "Original" };

            // Act
            object result = extensionMethodInfo.Invoke(testObj, BindingFlags.Default, null, [], CultureInfo.InvariantCulture);

            // Assert
            Assert.NotNull(result);
            var testResult = result as TestResult;
            Assert.NotNull(testResult);
            Assert.Equal(50, testResult.ResultValue);
            Assert.Equal("Original_Result", testResult.ResultName);
        }

        #endregion
    }

    #region Extension Methods for Testing

    public static class TestExtensions
    {
        public static string SimpleExtension(this ExtensionMethodInfoTest.TestClass obj)
        {
            return $"Extension: {obj.Name}";
        }

        public static string NoParamExtension(this ExtensionMethodInfoTest.TestClass obj)
        {
            return obj?.Name ?? "null";
        }

        public static int MultiParamExtension(this ExtensionMethodInfoTest.TestClass obj, int addValue, string suffix)
        {
            obj.Name += suffix;
            return obj.Value + addValue;
        }

        public static string NullableExtension(this ExtensionMethodInfoTest.TestClass? obj)
        {
            return obj?.Name ?? "null";
        }

        public static int RefParamExtension(this ExtensionMethodInfoTest.TestClass obj, ref int value)
        {
            int result = obj.Value + value;
            value = result * 2;
            return result;
        }

        public static bool OutParamExtension(this ExtensionMethodInfoTest.TestClass obj, out int result)
        {
            result = obj.Value * 2;
            return true;
        }

        public static int NoExtraParamExtension(this ExtensionMethodInfoTest.TestClass obj)
        {
            return obj.Value * 2;
        }

        public static ExtensionMethodInfoTest.TestResult ComplexReturnExtension(this ExtensionMethodInfoTest.TestClass obj)
        {
            return new ExtensionMethodInfoTest.TestResult
            {
                ResultValue = obj.Value,
                ResultName = $"{obj.Name}_Result"
            };
        }
    }

    #endregion
}
