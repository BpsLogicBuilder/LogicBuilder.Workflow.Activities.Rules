using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleValidationTest
    {
        #region Test Classes
        public class TestClass
        {
            public int PublicField;
#pragma warning disable CS0649// Disable warning for unassigned private field
#pragma warning disable CS0169// Disable warning for unused private field
            private readonly int privateField;
            internal int internalField;
#pragma warning restore CS0649
#pragma warning restore CS0169

            public int PublicProperty { get; set; }
            internal int InternalProperty { get; set; }

            public string? Name { get; set; }
            public int Age { get; set; }
#pragma warning disable CA1822 // Disable warning for static method suggestion
            public void PublicMethod() { }
            private void PrivateMethod() { }
            internal void InternalMethod() { }
            public static void StaticMethod() { }

            public int Add(int a, int b) => a + b;
            public int Add(int a, int b, int c) => a + b + c;

            public string GetMessage() => "Hello";
            public string GetMessage(string prefix) => prefix + "Hello";
#pragma warning restore CA1822 // Disable warning for static method suggestion
        }

        public class GenericTestClass<T>
        {
            public T? Value { get; set; }
            public T? GetValue() => Value;
        }

        public enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Constructor_WithObject_InitializesSuccessfully()
        {
            // Arrange
            var testObject = new TestClass();

            // Act
            var validation = new RuleValidation(testObject);

            // Assert
            Assert.NotNull(validation);
            Assert.Equal(typeof(TestClass), validation.ThisType);
            Assert.NotNull(validation.Errors);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Constructor_WithNullObject_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleValidation((object?)null));
        }

        [Fact]
        public void Constructor_WithType_InitializesSuccessfully()
        {
            // Arrange
            var type = typeof(TestClass);

            // Act
            var validation = new RuleValidation(type);

            // Assert
            Assert.NotNull(validation);
            Assert.Equal(type, validation.ThisType);
            Assert.NotNull(validation.Errors);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Constructor_WithNullType_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleValidation(null));
        }

        [Fact]
        public void Constructor_WithTypeAndAssemblies_InitializesSuccessfully()
        {
            // Arrange
            var type = typeof(TestClass);
            var assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };

            // Act
            var validation = new RuleValidation(type, assemblies);

            // Assert
            Assert.NotNull(validation);
            Assert.Equal(type, validation.ThisType);
            Assert.NotNull(validation.Errors);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Constructor_WithNullTypeAndAssemblies_ThrowsArgumentNullException()
        {
            // Arrange
            var assemblies = new List<Assembly>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RuleValidation(null, assemblies));
        }
        #endregion

        #region IsValidBooleanResult Tests
        [Fact]
        public void IsValidBooleanResult_WithBoolType_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.IsValidBooleanResult(typeof(bool));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidBooleanResult_WithNullableBoolType_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.IsValidBooleanResult(typeof(bool?));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidBooleanResult_WithIntType_ReturnsFalse()
        {
            // Act
            var result = RuleValidation.IsValidBooleanResult(typeof(int));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidBooleanResult_WithStringType_ReturnsFalse()
        {
            // Act
            var result = RuleValidation.IsValidBooleanResult(typeof(string));

            // Assert
            Assert.False(result);
        }
        #endregion

        #region ValidateConditionExpression Tests
        [Fact]
        public void ValidateConditionExpression_WithNullExpression_ThrowsArgumentNullException()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validation.ValidateConditionExpression(null));
        }

        [Fact]
        public void ValidateConditionExpression_WithBooleanPrimitive_ReturnsTrue()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(true);

            // Act
            var result = validation.ValidateConditionExpression(expression);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void ValidateConditionExpression_WithNonBooleanPrimitive_ReturnsFalse()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(42);

            // Act
            var result = validation.ValidateConditionExpression(expression);

            // Assert
            Assert.False(result);
            Assert.NotEmpty(validation.Errors);
        }
        #endregion

        #region IsPrivate and IsInternal Tests
        [Fact]
        public void IsPrivate_WithPrivateMethod_ReturnsTrue()
        {
            // Arrange
            var method = typeof(TestClass).GetMethod("PrivateMethod", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = RuleValidation.IsPrivate(method);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPrivate_WithPublicMethod_ReturnsFalse()
        {
            // Arrange
            var method = typeof(TestClass).GetMethod("PublicMethod");

            // Act
            var result = RuleValidation.IsPrivate(method);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInternal_WithInternalMethod_ReturnsTrue()
        {
            // Arrange
            var method = typeof(TestClass).GetMethod("InternalMethod", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = RuleValidation.IsInternal(method);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInternal_WithPublicMethod_ReturnsFalse()
        {
            // Arrange
            var method = typeof(TestClass).GetMethod("PublicMethod");

            // Act
            var result = RuleValidation.IsInternal(method);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPrivate_WithPrivateField_ReturnsTrue()
        {
            // Arrange
            var field = typeof(TestClass).GetField("privateField", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = RuleValidation.IsPrivate(field);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPrivate_WithPublicField_ReturnsFalse()
        {
            // Arrange
            var field = typeof(TestClass).GetField("PublicField");

            // Act
            var result = RuleValidation.IsPrivate(field);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsInternal_WithInternalField_ReturnsTrue()
        {
            // Arrange
            var field = typeof(TestClass).GetField("internalField", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = RuleValidation.IsInternal(field);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region TypesAreAssignable Tests
        [Fact]
        public void TypesAreAssignable_SameTypes_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(int), typeof(int), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void TypesAreAssignable_IntToObject_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(int), typeof(object), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void TypesAreAssignable_DerivedToBase_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(string), typeof(object), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void TypesAreAssignable_IncompatibleTypes_ReturnsFalse()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(string), typeof(int), null, out _);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TypesAreAssignable_NullToReferenceType_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(NullLiteral), typeof(string), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void TypesAreAssignable_NullToValueType_ReturnsFalse()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(NullLiteral), typeof(int), null, out ValidationError error);

            // Assert
            Assert.False(result);
            Assert.NotNull(error);
        }

        [Fact]
        public void TypesAreAssignable_IntToDouble_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(int), typeof(double), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void TypesAreAssignable_ByteToInt_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                typeof(byte), typeof(int), null, out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }
        #endregion

        #region ResolveFieldOrProperty Tests
        [Fact]
        public void ResolveFieldOrProperty_WithValidPublicField_ReturnsFieldInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var member = validation.ResolveFieldOrProperty(typeof(TestClass), "PublicField");

            // Assert
            Assert.NotNull(member);
            Assert.IsType<FieldInfo>(member, exactMatch: false);
            Assert.Equal("PublicField", member.Name);
        }

        [Fact]
        public void ResolveFieldOrProperty_WithValidPublicProperty_ReturnsPropertyInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var member = validation.ResolveFieldOrProperty(typeof(TestClass), "PublicProperty");

            // Assert
            Assert.NotNull(member);
            Assert.IsType<PropertyInfo>(member, exactMatch: false);
            Assert.Equal("PublicProperty", member.Name);
        }

        [Fact]
        public void ResolveFieldOrProperty_WithInvalidName_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var member = validation.ResolveFieldOrProperty(typeof(TestClass), "NonExistentMember");

            // Assert
            Assert.Null(member);
        }
        #endregion

        #region ResolveType Tests
        [Fact]
        public void ResolveType_WithValidTypeReference_ReturnsType()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var typeRef = new CodeTypeReference(typeof(string));

            // Act
            var resolvedType = validation.ResolveType(typeRef);

            // Assert
            Assert.NotNull(resolvedType);
            Assert.Equal(typeof(string), resolvedType);
        }

        [Fact]
        public void ResolveType_WithInvalidTypeReference_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var typeRef = new CodeTypeReference("NonExistent.Type.Name");

            // Act
            var resolvedType = validation.ResolveType(typeRef);

            // Assert
            Assert.Null(resolvedType);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void ResolveType_WithQualifiedName_ReturnsType()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var qualifiedName = typeof(string).AssemblyQualifiedName;

            // Act
            var resolvedType = validation.ResolveType(qualifiedName);

            // Assert
            Assert.NotNull(resolvedType);
            Assert.Equal(typeof(string), resolvedType);
        }

        [Fact]
        public void ResolveType_WithNullQualifiedName_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var resolvedType = validation.ResolveType((string?)null);

            // Assert
            Assert.Null(resolvedType);
        }

        [Fact]
        public void ResolveType_WithArrayType_ReturnsArrayType()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var typeRef = new CodeTypeReference(typeof(int[]));

            // Act
            var resolvedType = validation.ResolveType(typeRef);

            // Assert
            Assert.NotNull(resolvedType);
            Assert.True(resolvedType.IsArray);
            Assert.Equal(typeof(int), resolvedType.GetElementType());
        }
        #endregion

        #region PushParentExpression and PopParentExpression Tests
        [Fact]
        public void PushParentExpression_WithValidExpression_ReturnsTrue()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(42);

            // Act
            var result = validation.PushParentExpression(expression);

            // Assert
            Assert.True(result);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void PushParentExpression_WithNullExpression_ThrowsArgumentNullException()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validation.PushParentExpression(null));
        }

        [Fact]
        public void PushParentExpression_WithCyclicalReference_ReturnsFalse()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(42);

            // Act
            validation.PushParentExpression(expression);
            var result = validation.PushParentExpression(expression);

            // Assert
            Assert.False(result);
            Assert.NotEmpty(validation.Errors);
        }

        [Fact]
        public void PopParentExpression_AfterPush_Succeeds()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(42);

            // Act
            validation.PushParentExpression(expression);
            validation.PopParentExpression();

            // No exception should be thrown
            Assert.Empty(validation.Errors);
        }
        #endregion

        #region ExpressionInfo Tests
        [Fact]
        public void ExpressionInfo_WithNullExpression_ThrowsArgumentNullException()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validation.ExpressionInfo(null));
        }

        [Fact]
        public void ExpressionInfo_WithUnknownExpression_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var expression = new CodePrimitiveExpression(42);

            // Act
            var info = validation.ExpressionInfo(expression);

            // Assert
            Assert.Null(info);
        }
        #endregion

        #region GetTypeProvider Tests
        [Fact]
        public void GetTypeProvider_ReturnsNonNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var provider = validation.GetTypeProvider();

            // Assert
            Assert.NotNull(provider);
        }
        #endregion

        #region Properties Tests
        [Fact]
        public void ThisType_ReturnsCorrectType()
        {
            // Arrange
            var expectedType = typeof(TestClass);
            var validation = new RuleValidation(expectedType);

            // Act
            var actualType = validation.ThisType;

            // Assert
            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void Errors_InitiallyEmpty()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var errors = validation.Errors;

            // Assert
            Assert.NotNull(errors);
            Assert.Empty(errors);
        }

        [Fact]
        public void ErrorsByRuleName_InitiallyEmpty()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var errorsByRuleName = validation.ErrorsByRuleName;

            // Assert
            Assert.NotNull(errorsByRuleName);
            Assert.Empty(errorsByRuleName);
        }

        [Fact]
        public void AddError_AddsToErrorsCollection()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var error = new ValidationError("Test error", 100);

            // Act
            validation.AddError(error);

            // Assert
            Assert.Single(validation.Errors);
            Assert.Contains(error, validation.Errors);
        }
        #endregion

        #region Numeric Conversion Tests
        [Theory]
        [InlineData(typeof(sbyte), typeof(short), true)]
        [InlineData(typeof(sbyte), typeof(int), true)]
        [InlineData(typeof(byte), typeof(short), true)]
        [InlineData(typeof(short), typeof(int), true)]
        [InlineData(typeof(int), typeof(long), true)]
        [InlineData(typeof(float), typeof(double), true)]
        [InlineData(typeof(long), typeof(short), false)]
        [InlineData(typeof(double), typeof(float), false)]
        public void TypesAreAssignable_NumericConversions_ReturnsExpectedResult(
            Type fromType, Type toType, bool expected)
        {
            // Act
            var result = RuleValidation.TypesAreAssignable(
                fromType, toType, null, out _);

            // Assert
            Assert.Equal(expected, result);
        }
        #endregion

        #region Explicit Conversion Tests
        [Fact]
        public void ExplicitConversionSpecified_WithValidConversion_ReturnsTrue()
        {
            // Act
            var result = RuleValidation.ExplicitConversionSpecified(
                typeof(double), typeof(int), out ValidationError error);

            // Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void ExplicitConversionSpecified_WithIncompatibleTypes_ReturnsFalse()
        {
            // Act
            var result = RuleValidation.ExplicitConversionSpecified(
                typeof(string), typeof(int), out _);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region ResolveMethod Tests
        [Fact]
        public void ResolveMethod_WithValidMethod_ReturnsMethodInfo()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(typeof(object));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };

            var arguments = new CodeExpression[]
            {
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2)
            };

            var methodCall = new CodeMethodInvokeExpression
            (
                new CodeThisReferenceExpression(), 
                "Add",
                arguments
            );

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(methodCall));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            // Act
            var result = validation.ResolveMethod(
                typeof(TestClass),
                "Add",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                [.. arguments],
                out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal("Add", result.MethodInfo.Name);
        }

        [Fact]
        public void ResolveMethod_WithInvalidMethodName_ReturnsNull()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var arguments = new List<CodeExpression>();

            // Act
            var result = validation.ResolveMethod(
                typeof(TestClass),
                "NonExistentMethod",
                BindingFlags.Public | BindingFlags.Instance,
                arguments,
                out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        [Fact]
        public void ResolveMethod_WithOverloadedMethod_ResolvesCorrectOverload()
        {
            // Arrange
            var createExpr = new CodeObjectCreateExpression(typeof(object));
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = createExpr,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };

            var arguments = new CodeExpression[]
            {
                new CodePrimitiveExpression(1),
                new CodePrimitiveExpression(2),
                new CodePrimitiveExpression(3)
            };

            var methodCall = new CodeMethodInvokeExpression
            (
                new CodeThisReferenceExpression(),
                "Add",
                arguments
            );

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(methodCall));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            // Act
            var result = validation.ResolveMethod(
                typeof(TestClass),
                "Add",
                BindingFlags.Public | BindingFlags.Instance,
                [.. arguments],
                out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(3, result.MethodInfo.GetParameters().Length);
        }
        #endregion
    }
}