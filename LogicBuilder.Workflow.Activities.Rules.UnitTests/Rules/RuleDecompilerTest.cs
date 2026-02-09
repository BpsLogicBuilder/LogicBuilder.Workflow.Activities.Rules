using System;
using System.CodeDom;
using System.Reflection;
using System.Text;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleDecompilerTest
    {
        #region DecompileObjectLiteral Tests

        [Fact]
        public void DecompileObjectLiteral_WithNull_ReturnsNull()
        {
            // Arrange
            StringBuilder sb = new();

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, null);

            // Assert
            Assert.Equal("null", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithString_ReturnsQuotedString()
        {
            // Arrange
            StringBuilder sb = new();
            string value = "Hello World";

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("\"Hello World\"", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithStringContainingEscapes_ReturnsEscapedString()
        {
            // Arrange
            StringBuilder sb = new();
            string value = "Hello\nWorld\t\"Test\"";

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("\"Hello\\nWorld\\t\\\"Test\\\"\"", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithChar_ReturnsSingleQuotedChar()
        {
            // Arrange
            StringBuilder sb = new();
            char value = 'A';

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("'A'", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithCharEscape_ReturnsEscapedChar()
        {
            // Arrange
            StringBuilder sb = new();
            char value = '\n';

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("'\\n'", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithLong_ReturnsLongWithLSuffix()
        {
            // Arrange
            StringBuilder sb = new();
            long value = 123456789L;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123456789L", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithUInt_ReturnsUIntWithUSuffix()
        {
            // Arrange
            StringBuilder sb = new();
            uint value = 123456789U;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123456789U", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithULong_ReturnsULongWithULSuffix()
        {
            // Arrange
            StringBuilder sb = new();
            ulong value = 123456789UL;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123456789UL", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithFloat_ReturnsFloatWithFSuffix()
        {
            // Arrange
            StringBuilder sb = new();
            float value = 123.456f;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123.456f", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithDouble_ReturnsDoubleWithDecimalPoint()
        {
            // Arrange
            StringBuilder sb = new();
            double value = 123.456;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123.456", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithDoubleInteger_ReturnsDoubleWithDecimalPoint()
        {
            // Arrange
            StringBuilder sb = new();
            double value = 123.0;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123.0", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithDecimal_ReturnsDecimalWithMSuffix()
        {
            // Arrange
            StringBuilder sb = new();
            decimal value = 123.456m;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123.456m", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithInt_ReturnsIntString()
        {
            // Arrange
            StringBuilder sb = new();
            int value = 123456;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Equal("123456", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithBool_ReturnsBoolString()
        {
            // Arrange
            StringBuilder sb1 = new();
            StringBuilder sb2 = new();

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb1, true);
            RuleDecompiler.DecompileObjectLiteral(sb2, false);

            // Assert
            Assert.Equal("True", sb1.ToString());
            Assert.Equal("False", sb2.ToString());
        }

        #endregion

        #region DecompileType Tests

        [Fact]
        public void DecompileType_WithNull_ReturnsEmptyString()
        {
            // Act
            string result = RuleDecompiler.DecompileType(null);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void DecompileType_WithPrimitiveTypes_ReturnsKnownTypeNames()
        {
            // Act & Assert
            Assert.Equal("int", RuleDecompiler.DecompileType(typeof(int)));
            Assert.Equal("string", RuleDecompiler.DecompileType(typeof(string)));
            Assert.Equal("bool", RuleDecompiler.DecompileType(typeof(bool)));
            Assert.Equal("char", RuleDecompiler.DecompileType(typeof(char)));
            Assert.Equal("byte", RuleDecompiler.DecompileType(typeof(byte)));
            Assert.Equal("sbyte", RuleDecompiler.DecompileType(typeof(sbyte)));
            Assert.Equal("short", RuleDecompiler.DecompileType(typeof(short)));
            Assert.Equal("ushort", RuleDecompiler.DecompileType(typeof(ushort)));
            Assert.Equal("uint", RuleDecompiler.DecompileType(typeof(uint)));
            Assert.Equal("long", RuleDecompiler.DecompileType(typeof(long)));
            Assert.Equal("ulong", RuleDecompiler.DecompileType(typeof(ulong)));
            Assert.Equal("float", RuleDecompiler.DecompileType(typeof(float)));
            Assert.Equal("double", RuleDecompiler.DecompileType(typeof(double)));
            Assert.Equal("decimal", RuleDecompiler.DecompileType(typeof(decimal)));
            Assert.Equal("object", RuleDecompiler.DecompileType(typeof(object)));
            Assert.Equal("void", RuleDecompiler.DecompileType(typeof(void)));
        }

        [Fact]
        public void DecompileType_WithArrayType_ReturnsArrayNotation()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(int[]));

            // Assert
            Assert.Equal("int[]", result);
        }

        [Fact]
        public void DecompileType_WithMultiDimensionalArray_ReturnsArrayNotation()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(int[,]));

            // Assert
            Assert.Equal("int[,]", result);
        }

        [Fact]
        public void DecompileType_WithGenericType_ReturnsGenericNotation()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(System.Collections.Generic.List<int>));

            // Assert
            Assert.Contains("List<int>", result);
        }

        [Fact]
        public void DecompileType_WithMultipleGenericParameters_ReturnsGenericNotation()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(System.Collections.Generic.Dictionary<string, int>));

            // Assert
            Assert.Contains("Dictionary<string, int>", result);
        }

        [Fact]
        public void DecompileType_WithNestedGenericTypes_ReturnsNestedGenericNotation()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(System.Collections.Generic.List<System.Collections.Generic.List<int>>));

            // Assert
            Assert.Contains("List<", result);
            Assert.Contains("List<int>", result);
        }

        [Fact]
        public void DecompileType_WithCustomType_ReturnsFullTypeName()
        {
            // Act
            string result = RuleDecompiler.DecompileType(typeof(RuleDecompiler));

            // Assert
            Assert.Contains("RuleDecompiler", result);
        }

        #endregion

        #region DecompileType with CodeTypeReference Tests

        [Fact]
        public void DecompileType_WithCodeTypeReference_ReturnsTypeName()
        {
            // Arrange
            StringBuilder sb = new();
            CodeTypeReference typeRef = new(typeof(int));

            // Act
            RuleDecompiler.DecompileType(sb, typeRef);

            // Assert
            Assert.Equal("int", sb.ToString());
        }

        [Fact]
        public void DecompileType_WithCodeTypeReferenceGeneric_ReturnsGenericNotation()
        {
            // Arrange
            StringBuilder sb = new();
            CodeTypeReference typeRef = new(typeof(System.Collections.Generic.List<int>));

            // Act
            RuleDecompiler.DecompileType(sb, typeRef);

            // Assert
            string result = sb.ToString();
            Assert.Contains("List", result);
            Assert.Contains("<", result);
            Assert.Contains(">", result);
        }

        [Fact]
        public void DecompileType_WithCodeTypeReferenceArray_ReturnsArrayNotation()
        {
            // Arrange
            StringBuilder sb = new();
            CodeTypeReference typeRef = new(typeof(int[]), CodeTypeReferenceOptions.GlobalReference);

            // Act
            RuleDecompiler.DecompileType(sb, typeRef);

            // Assert
            Assert.Contains("[]", sb.ToString());
        }

        #endregion

        #region DecompileMethod Tests

        [Fact]
        public void DecompileMethod_WithNull_ReturnsEmptyString()
        {
            // Act
            string result = RuleDecompiler.DecompileMethod(null);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void DecompileMethod_WithSimpleMethod_ReturnsMethodSignature()
        {
            // Arrange
            MethodInfo method = typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!;

            // Act
            string result = RuleDecompiler.DecompileMethod(method);

            // Assert
            Assert.Contains("string", result);
            Assert.Contains("ToUpper", result);
            Assert.Contains("()", result);
        }

        [Fact]
        public void DecompileMethod_WithMethodWithParameters_ReturnsMethodSignatureWithParameters()
        {
            // Arrange
            MethodInfo method = typeof(string).GetMethod("Substring", [typeof(int), typeof(int)])!;

            // Act
            string result = RuleDecompiler.DecompileMethod(method);

            // Assert
            Assert.Contains("string", result);
            Assert.Contains("Substring", result);
            Assert.Contains("int", result);
            Assert.Contains(",", result);
        }

        [Fact]
        public void DecompileMethod_WithOperatorMethod_ReturnsOperatorNotation()
        {
            // Arrange - Find an operator method
            MethodInfo method = typeof(decimal).GetMethod("op_Addition", [typeof(decimal), typeof(decimal)])!;

            // Act
            string result = RuleDecompiler.DecompileMethod(method);

            // Assert
            Assert.Contains("operator +", result);
        }

        #endregion

        #region MustParenthesize Tests

        [Fact]
        public void MustParenthesize_WithNullParent_ReturnsFalse()
        {
            // Arrange
            CodeExpression childExpr = new CodePrimitiveExpression(1);

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MustParenthesize_WithHigherPrecedenceChild_ReturnsFalse()
        {
            // Arrange
            CodeExpression childExpr = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "field");
            CodeExpression parentExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(1),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(2));

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, parentExpr);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MustParenthesize_WithLowerPrecedenceChild_ReturnsTrue()
        {
            // Arrange
            CodeExpression childExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(1),
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(2));
            CodeExpression parentExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(3),
                CodeBinaryOperatorType.Multiply,
                childExpr);

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, parentExpr);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MustParenthesize_WithSamePrecedenceOnRight_ReturnsTrue()
        {
            // Arrange
            CodeExpression childExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(3),
                CodeBinaryOperatorType.Subtract,
                new CodePrimitiveExpression(4));
            CodeExpression parentExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(2),
                CodeBinaryOperatorType.Subtract,
                childExpr);

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, parentExpr);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MustParenthesize_WithSamePrecedenceOnLeft_ReturnsFalse()
        {
            // Arrange
            CodeExpression childExpr = new CodeBinaryOperatorExpression(
                new CodePrimitiveExpression(2),
                CodeBinaryOperatorType.Subtract,
                new CodePrimitiveExpression(3));
            CodeExpression parentExpr = new CodeBinaryOperatorExpression(
                childExpr,
                CodeBinaryOperatorType.Subtract,
                new CodePrimitiveExpression(4));

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, parentExpr);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MustParenthesize_WithCastExpression_ChecksPrecedence()
        {
            // Arrange
            CodeExpression childExpr = new CodeCastExpression(typeof(int), new CodePrimitiveExpression(1));
            CodeExpression parentExpr = new CodeBinaryOperatorExpression(
                childExpr,
                CodeBinaryOperatorType.Add,
                new CodePrimitiveExpression(2));

            // Act
            bool result = RuleDecompiler.MustParenthesize(childExpr, parentExpr);

            // Assert
            Assert.False(result); // Cast has higher precedence than addition
        }

        #endregion

        #region Special Character Tests

        [Theory]
        [InlineData('\0', "'\\0'")]
        [InlineData('\n', "'\\n'")]
        [InlineData('\r', "'\\r'")]
        [InlineData('\b', "'\\b'")]
        [InlineData('\a', "'\\a'")]
        [InlineData('\t', "'\\t'")]
        [InlineData('\f', "'\\f'")]
        [InlineData('\v', "'\\v'")]
        [InlineData('\\', "'\\\\'")]
        [InlineData('\'', "'\\''")]
        public void DecompileObjectLiteral_WithSpecialChar_ReturnsEscapedChar(char input, string expected)
        {
            // Arrange
            StringBuilder sb = new();

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, input);

            // Assert
            Assert.Equal(expected, sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithUnicodeChar_ReturnsUnicodeEscape()
        {
            // Arrange
            StringBuilder sb = new();
            char value = '\u1234';

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Contains("\u1234", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithSurrogatePair_PreservesSurrogatePair()
        {
            // Arrange
            StringBuilder sb = new();
            string value = "Test\uD800\uDC00End"; // Valid surrogate pair

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            string result = sb.ToString();
            Assert.StartsWith("\"", result);
            Assert.EndsWith("\"", result);
            Assert.Contains("\uD800\uDC00", result);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void DecompileObjectLiteral_WithEmptyString_ReturnsEmptyQuotedString()
        {
            // Arrange
            StringBuilder sb = new();

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, "");

            // Assert
            Assert.Equal("\"\"", sb.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithNegativeNumbers_ReturnsNegativeValue()
        {
            // Arrange
            StringBuilder sb1 = new();
            StringBuilder sb2 = new();
            StringBuilder sb3 = new();

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb1, -42);
            RuleDecompiler.DecompileObjectLiteral(sb2, -42L);
            RuleDecompiler.DecompileObjectLiteral(sb3, -42.5f);

            // Assert
            Assert.Equal("-42", sb1.ToString());
            Assert.Equal("-42L", sb2.ToString());
            Assert.Equal("-42.5f", sb3.ToString());
        }

        [Fact]
        public void DecompileObjectLiteral_WithScientificNotation_PreservesFormat()
        {
            // Arrange
            StringBuilder sb = new();
            double value = 1.23e10;

            // Act
            RuleDecompiler.DecompileObjectLiteral(sb, value);

            // Assert
            Assert.Contains(".", sb.ToString().ToUpper());
        }

        #endregion
    }
}