using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Text;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class TypeReferenceExpressionTest
    {
        #region Validate Tests

        [Fact]
        public void Validate_WithIsWrittenTrue_AddsValidationError()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, typeRefExpr, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
            Assert.Contains("Cannot write to an expression of this type.", validation.Errors[0].ErrorText);
        }

        [Fact]
        public void Validate_WithValidTypeReference_ReturnsRuleExpressionInfo()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(int));
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, typeRefExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithStringType_ReturnsCorrectType()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, typeRefExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(string), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_WithComplexType_ReturnsCorrectType()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(TestClass));
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Validate(validation, typeRefExpr, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(TestClass), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_DoesNotTrackAnyDependencies()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(int));
            var validation = new RuleValidation(typeof(TestClass));
            RuleExpressionWalker.Validate(validation, typeRefExpr, false);
            var analysis = new RuleAnalysis(validation, true);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, typeRefExpr, true, false, null);

            // Assert
            Assert.Empty(analysis.GetSymbols());
        }

        [Fact]
        public void AnalyzeUsage_WithWriteMode_DoesNotTrackDependencies()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));
            var validation = new RuleValidation(typeof(TestClass));
            RuleExpressionWalker.Validate(validation, typeRefExpr, false);
            var analysis = new RuleAnalysis(validation, false);

            // Act
            RuleExpressionWalker.AnalyzeUsage(analysis, typeRefExpr, false, true, null);

            // Assert
            Assert.Empty(analysis.GetSymbols());
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_ReturnsNullValue()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(int));
            var validation = new RuleValidation(typeof(TestClass));
            RuleExpressionWalker.Validate(validation, typeRefExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, typeRefExpr);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Evaluate_WithDifferentType_StillReturnsNull()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));
            var validation = new RuleValidation(typeof(TestClass));
            RuleExpressionWalker.Validate(validation, typeRefExpr, false);
            var execution = new RuleExecution(validation, new TestClass());

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, typeRefExpr);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithSimpleType_ReturnsTypeName()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(int));
            var stringBuilder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, typeRefExpr, null);

            // Assert
            Assert.Equal("int", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_WithStringType_ReturnsTypeName()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));
            var stringBuilder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, typeRefExpr, null);

            // Assert
            Assert.Equal("string", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_WithCustomType_ReturnsFullTypeName()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(TestClass));
            var stringBuilder = new StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, typeRefExpr, null);

            // Assert
            Assert.Contains("TestClass", stringBuilder.ToString());
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesIdenticalExpression()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(int));

            // Act
            var clonedExpr = RuleExpressionWalker.Clone(typeRefExpr);

            // Assert
            Assert.NotNull(clonedExpr);
            Assert.IsType<CodeTypeReferenceExpression>(clonedExpr);
            var clonedTypeRef = (CodeTypeReferenceExpression)clonedExpr;
            Assert.Equal(typeRefExpr.Type.BaseType, clonedTypeRef.Type.BaseType);
        }

        [Fact]
        public void Clone_CreatesDeepCopy()
        {
            // Arrange
            var typeRefExpr = new CodeTypeReferenceExpression(typeof(string));

            // Act
            var clonedExpr = RuleExpressionWalker.Clone(typeRefExpr);

            // Assert
            Assert.NotSame(typeRefExpr, clonedExpr);
            var clonedTypeRef = (CodeTypeReferenceExpression)clonedExpr;
            Assert.NotSame(typeRefExpr.Type, clonedTypeRef.Type);
        }

        [Fact]
        public void CloneType_WithNullType_ReturnsNull()
        {
            // Act
            var result = TypeReferenceExpression.CloneType(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CloneType_WithSimpleType_CreatesIdenticalType()
        {
            // Arrange
            var typeRef = new CodeTypeReference(typeof(int));

            // Act
            var clonedType = TypeReferenceExpression.CloneType(typeRef);

            // Assert
            Assert.NotNull(clonedType);
            Assert.Equal(typeRef.BaseType, clonedType.BaseType);
            Assert.NotSame(typeRef, clonedType);
        }

        [Fact]
        public void CloneType_WithArrayType_ClonesArrayProperties()
        {
            // Arrange
            var typeRef = new CodeTypeReference(typeof(int[]));

            // Act
            var clonedType = TypeReferenceExpression.CloneType(typeRef);

            // Assert
            Assert.NotNull(clonedType);
            Assert.Equal(typeRef.ArrayRank, clonedType.ArrayRank);
            Assert.Equal(typeRef.BaseType, clonedType.BaseType);
        }

        [Fact]
        public void CloneType_WithGenericType_ClonesTypeArguments()
        {
            // Arrange
            var typeRef = new CodeTypeReference(typeof(System.Collections.Generic.List<int>));

            // Act
            var clonedType = TypeReferenceExpression.CloneType(typeRef);

            // Assert
            Assert.NotNull(clonedType);
            Assert.Equal(typeRef.TypeArguments.Count, clonedType.TypeArguments.Count);
            Assert.Equal(typeRef.BaseType, clonedType.BaseType);
        }

        [Fact]
        public void CloneType_WithUserData_ClonesUserData()
        {
            // Arrange
            var typeRef = new CodeTypeReference(typeof(int));
            typeRef.UserData["TestKey"] = "TestValue";

            // Act
            var clonedType = TypeReferenceExpression.CloneType(typeRef);

            // Assert
            Assert.NotNull(clonedType);
            Assert.Equal("TestValue", clonedType.UserData["TestKey"]);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalTypes_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeTypeReferenceExpression(typeof(int));
            var expr2 = new CodeTypeReferenceExpression(typeof(int));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentTypes_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeTypeReferenceExpression(typeof(int));
            var expr2 = new CodeTypeReferenceExpression(typeof(string));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameComplexType_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeTypeReferenceExpression(typeof(TestClass));
            var expr2 = new CodeTypeReferenceExpression(typeof(TestClass));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MatchType_WithIdenticalBaseTypes_ReturnsTrue()
        {
            // Arrange
            var type1 = new CodeTypeReference(typeof(int));
            var type2 = new CodeTypeReference(typeof(int));

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MatchType_WithDifferentBaseTypes_ReturnsFalse()
        {
            // Arrange
            var type1 = new CodeTypeReference(typeof(int));
            var type2 = new CodeTypeReference(typeof(string));

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MatchType_WithDifferentTypeArgumentCounts_ReturnsFalse()
        {
            // Arrange
            var type1 = new CodeTypeReference("List");
            type1.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            
            var type2 = new CodeTypeReference("List");
            type2.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            type2.TypeArguments.Add(new CodeTypeReference(typeof(string)));

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MatchType_WithMatchingTypeArguments_ReturnsTrue()
        {
            // Arrange
            var type1 = new CodeTypeReference("List");
            type1.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            
            var type2 = new CodeTypeReference("List");
            type2.TypeArguments.Add(new CodeTypeReference(typeof(int)));

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MatchType_WithDifferentTypeArguments_ReturnsFalse()
        {
            // Arrange
            var type1 = new CodeTypeReference("List");
            type1.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            
            var type2 = new CodeTypeReference("List");
            type2.TypeArguments.Add(new CodeTypeReference(typeof(string)));

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MatchType_WithNestedTypeArguments_ComparesRecursively()
        {
            // Arrange
            var innerType1 = new CodeTypeReference("Dictionary");
            innerType1.TypeArguments.Add(new CodeTypeReference(typeof(string)));
            innerType1.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            
            var type1 = new CodeTypeReference("List");
            type1.TypeArguments.Add(innerType1);
            
            var innerType2 = new CodeTypeReference("Dictionary");
            innerType2.TypeArguments.Add(new CodeTypeReference(typeof(string)));
            innerType2.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            
            var type2 = new CodeTypeReference("List");
            type2.TypeArguments.Add(innerType2);

            // Act
            var result = TypeReferenceExpression.MatchType(type1, type2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Test Helper Class

        private class TestClass
        {
            public int IntValue { get; set; }
            public string? StringValue { get; set; }
        }

        #endregion
    }
}