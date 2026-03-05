using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class LiteralTest
    {
        #region MakeLiteral Tests - Primitive Types
        [Fact]
        public void MakeLiteral_WithBoolType_CreatesBoolLiteral()
        {
            // Arrange
            Type type = typeof(bool);
            object value = true;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BoolLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithByteType_CreatesByteLiteral()
        {
            // Arrange
            Type type = typeof(byte);
            object value = (byte)42;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ByteLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithSByteType_CreatesSByteLiteral()
        {
            // Arrange
            Type type = typeof(sbyte);
            object value = (sbyte)-42;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SByteLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithShortType_CreatesShortLiteral()
        {
            // Arrange
            Type type = typeof(short);
            object value = (short)1000;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ShortLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithIntType_CreatesIntLiteral()
        {
            // Arrange
            Type type = typeof(int);
            object value = 123456;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IntLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithLongType_CreatesLongLiteral()
        {
            // Arrange
            Type type = typeof(long);
            object value = 9876543210L;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LongLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithUShortType_CreatesUShortLiteral()
        {
            // Arrange
            Type type = typeof(ushort);
            object value = (ushort)5000;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UShortLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithUIntType_CreatesUIntLiteral()
        {
            // Arrange
            Type type = typeof(uint);
            object value = 3000000000U;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UIntLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithULongType_CreatesULongLiteral()
        {
            // Arrange
            Type type = typeof(ulong);
            object value = 18446744073709551615UL;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ULongLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithFloatType_CreatesFloatLiteral()
        {
            // Arrange
            Type type = typeof(float);
            object value = 3.14f;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FloatLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithDoubleType_CreatesDoubleLiteral()
        {
            // Arrange
            Type type = typeof(double);
            object value = 3.14159265359;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DoubleLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithCharType_CreatesCharLiteral()
        {
            // Arrange
            Type type = typeof(char);
            object value = 'A';

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CharLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithStringType_CreatesStringLiteral()
        {
            // Arrange
            Type type = typeof(string);
            object value = "Hello, World!";

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StringLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithDecimalType_CreatesDecimalLiteral()
        {
            // Arrange
            Type type = typeof(decimal);
            object value = 123.456m;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DecimalLiteral>(result);
            Assert.Equal(value, result.Value);
        }
        #endregion

        #region MakeLiteral Tests - Nullable Types
        [Fact]
        public void MakeLiteral_WithNullableBoolType_CreatesBoolLiteral()
        {
            // Arrange
            Type type = typeof(bool?);
            object value = true;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BoolLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableIntType_CreatesIntLiteral()
        {
            // Arrange
            Type type = typeof(int?);
            object value = 42;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IntLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableLongType_CreatesLongLiteral()
        {
            // Arrange
            Type type = typeof(long?);
            object value = 1234567890L;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LongLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableDecimalType_CreatesDecimalLiteral()
        {
            // Arrange
            Type type = typeof(decimal?);
            object value = 99.99m;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DecimalLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableFloatType_CreatesFloatLiteral()
        {
            // Arrange
            Type type = typeof(float?);
            object value = 2.5f;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FloatLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableDoubleType_CreatesDoubleLiteral()
        {
            // Arrange
            Type type = typeof(double?);
            object value = 3.14159;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DoubleLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullableCharType_CreatesCharLiteral()
        {
            // Arrange
            Type type = typeof(char?);
            object value = 'Z';

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CharLiteral>(result);
            Assert.Equal(value, result.Value);
        }
        #endregion

        #region MakeLiteral Tests - Null Values
        [Fact]
        public void MakeLiteral_WithNullValue_CreatesNullLiteral()
        {
            // Arrange
            Type type = typeof(string);
            object? value = null;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NullLiteral>(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullValueAndIntType_CreatesNullLiteral()
        {
            // Arrange
            Type type = typeof(int);
            object? value = null;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NullLiteral>(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void MakeLiteral_WithNullValueAndNullableIntType_CreatesNullLiteral()
        {
            // Arrange
            Type type = typeof(int?);
            object? value = null;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NullLiteral>(result);
            Assert.Null(result.Value);
        }
        #endregion

        #region MakeLiteral Tests - Unsupported Types
        [Fact]
        public void MakeLiteral_WithUnsupportedType_ReturnsNull()
        {
            // Arrange
            Type type = typeof(DateTime);
            object value = DateTime.Now;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void MakeLiteral_WithCustomClassType_ReturnsNull()
        {
            // Arrange
            Type type = typeof(object);
            object value = new();

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region MakeLiteral Tests - Invalid Cast
        [Fact]
        public void MakeLiteral_WithInvalidCast_ThrowsRuleEvaluationIncompatibleTypesException()
        {
            // Arrange
            Type type = typeof(int);
            object value = "not an int";

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(
                () => Literal.MakeLiteral(type, value));
            Assert.NotNull(exception);
            Assert.Equal(type, exception.Left);
            Assert.Equal(CodeBinaryOperatorType.Assign, exception.Operator);
            Assert.Equal(value.GetType(), exception.Right);
        }

        [Fact]
        public void MakeLiteral_WithInvalidCastFromDoubleToInt_ThrowsRuleEvaluationIncompatibleTypesException()
        {
            // Arrange
            Type type = typeof(int);
            object value = 3.14;

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(
                () => Literal.MakeLiteral(type, value));
            Assert.NotNull(exception);
        }

        [Fact]
        public void MakeLiteral_WithInvalidCastFromStringToBool_ThrowsRuleEvaluationIncompatibleTypesException()
        {
            // Arrange
            Type type = typeof(bool);
            object value = "true";

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationIncompatibleTypesException>(
                () => Literal.MakeLiteral(type, value));
            Assert.NotNull(exception);
        }
        #endregion

        #region AllowedComparison Tests - Same Type Comparisons
        [Fact]
        public void AllowedComparison_IntEqualityWithInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(5);
            CodeExpression rhsExpression = new CodePrimitiveExpression(10);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(bool), result.ExpressionType);
        }

        [Fact]
        public void AllowedComparison_StringEqualityWithString_Succeeds()
        {
            // Arrange
            Type lhs = typeof(string);
            Type rhs = typeof(string);
            CodeExpression lhsExpression = new CodePrimitiveExpression("hello");
            CodeExpression rhsExpression = new CodePrimitiveExpression("world");
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_BoolEqualityWithBool_Succeeds()
        {
            // Arrange
            Type lhs = typeof(bool);
            Type rhs = typeof(bool);
            CodeExpression lhsExpression = new CodePrimitiveExpression(true);
            CodeExpression rhsExpression = new CodePrimitiveExpression(false);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_BoolLessThanWithBool_Fails()
        {
            // Arrange
            Type lhs = typeof(bool);
            Type rhs = typeof(bool);
            CodeExpression lhsExpression = new CodePrimitiveExpression(true);
            CodeExpression rhsExpression = new CodePrimitiveExpression(false);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        [Fact]
        public void AllowedComparison_DecimalGreaterThanWithDecimal_Succeeds()
        {
            // Arrange
            Type lhs = typeof(decimal);
            Type rhs = typeof(decimal);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(50m);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region AllowedComparison Tests - Mixed Type Comparisons
        [Fact]
        public void AllowedComparison_IntWithLong_Succeeds()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(long);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100);
            CodeExpression rhsExpression = new CodePrimitiveExpression(200L);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_IntWithUInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(uint);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100);
            CodeExpression rhsExpression = new CodePrimitiveExpression(200U);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_FloatWithInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(float);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(3.14f);
            CodeExpression rhsExpression = new CodePrimitiveExpression(5);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_DecimalWithInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(decimal);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(50);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_ULongWithUInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(ulong);
            Type rhs = typeof(uint);
            CodeExpression lhsExpression = new CodePrimitiveExpression(1000UL);
            CodeExpression rhsExpression = new CodePrimitiveExpression(500U);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_StringWithInt_Fails()
        {
            // Arrange
            Type lhs = typeof(string);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression("hello");
            CodeExpression rhsExpression = new CodePrimitiveExpression(42);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        [Fact]
        public void AllowedComparison_BoolWithInt_Fails()
        {
            // Arrange
            Type lhs = typeof(bool);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(true);
            CodeExpression rhsExpression = new CodePrimitiveExpression(1);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }

        [Fact]
        public void AllowedComparison_DecimalWithFloat_Fails()
        {
            // Arrange
            Type lhs = typeof(decimal);
            Type rhs = typeof(float);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(50f);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }
        #endregion

        #region AllowedComparison Tests - Null Comparisons
        [Fact]
        public void AllowedComparison_NullWithNull_Succeeds()
        {
            // Arrange
            Type lhs = typeof(NullLiteral);
            Type rhs = typeof(NullLiteral);
            CodeExpression lhsExpression = new CodePrimitiveExpression(null);
            CodeExpression rhsExpression = new CodePrimitiveExpression(null);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_NullWithString_Succeeds()
        {
            // Arrange
            Type lhs = typeof(NullLiteral);
            Type rhs = typeof(string);
            CodeExpression lhsExpression = new CodePrimitiveExpression(null);
            CodeExpression rhsExpression = new CodePrimitiveExpression("test");
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region MapOperatorToMethod Tests - Equality Operators
        [Fact]
        public void MapOperatorToMethod_EqualityOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(5);
            CodeExpression rhsExpression = new CodePrimitiveExpression(10);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Equality", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_EqualityOperatorWithObjectTypes_ReturnsObjectEquality()
        {
            // Arrange
            Type lhs = typeof(object);
            Type rhs = typeof(NullLiteral);
            CodeExpression lhsExpression = new CodeObjectCreateExpression(typeof(object));
            CodeExpression rhsExpression = new CodePrimitiveExpression(null);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal("ObjectEquality", result.Name);
        }
        #endregion

        #region MapOperatorToMethod Tests - Relational Operators
        [Fact]
        public void MapOperatorToMethod_GreaterThanOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(10);
            CodeExpression rhsExpression = new CodePrimitiveExpression(5);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("GreaterThan", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_LessThanOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(double);
            Type rhs = typeof(double);
            CodeExpression lhsExpression = new CodePrimitiveExpression(3.14);
            CodeExpression rhsExpression = new CodePrimitiveExpression(6.28);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("LessThan", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_LessThanOrEqualOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(long);
            Type rhs = typeof(long);
            CodeExpression lhsExpression = new CodePrimitiveExpression(1000L);
            CodeExpression rhsExpression = new CodePrimitiveExpression(2000L);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThanOrEqual;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("LessThanOrEqual", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_GreaterThanOrEqualOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(uint);
            Type rhs = typeof(uint);
            CodeExpression lhsExpression = new CodePrimitiveExpression(500U);
            CodeExpression rhsExpression = new CodePrimitiveExpression(300U);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThanOrEqual;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("GreaterThanOrEqual", result.Name);
        }
        #endregion

        #region MapOperatorToMethod Tests - Arithmetic Operators
        [Fact]
        public void MapOperatorToMethod_AdditionOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(10);
            CodeExpression rhsExpression = new CodePrimitiveExpression(20);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Add;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Addition", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_SubtractionOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(decimal);
            Type rhs = typeof(decimal);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(30m);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Subtract;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Subtraction", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_MultiplicationOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(float);
            Type rhs = typeof(float);
            CodeExpression lhsExpression = new CodePrimitiveExpression(2.5f);
            CodeExpression rhsExpression = new CodePrimitiveExpression(4f);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Multiply;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Multiply", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_DivisionOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(double);
            Type rhs = typeof(double);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100.0);
            CodeExpression rhsExpression = new CodePrimitiveExpression(25.0);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Divide;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Division", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_ModulusOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(10);
            CodeExpression rhsExpression = new CodePrimitiveExpression(3);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Modulus;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("Modulus", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_BitwiseAndOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(15);
            CodeExpression rhsExpression = new CodePrimitiveExpression(7);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.BitwiseAnd;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("BitwiseAnd", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_BitwiseOrOperator_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(long);
            Type rhs = typeof(long);
            CodeExpression lhsExpression = new CodePrimitiveExpression(8L);
            CodeExpression rhsExpression = new CodePrimitiveExpression(4L);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.BitwiseOr;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Contains("BitwiseOr", result.Name);
        }
        #endregion

        #region MapOperatorToMethod Tests - String Addition
        [Fact]
        public void MapOperatorToMethod_StringAddition_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(string);
            Type rhs = typeof(string);
            CodeExpression lhsExpression = new CodePrimitiveExpression("Hello");
            CodeExpression rhsExpression = new CodePrimitiveExpression(" World");
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Add;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal("Addition", result.Name);
        }

        [Fact]
        public void MapOperatorToMethod_StringAdditionWithObject_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(string);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression("Value: ");
            CodeExpression rhsExpression = new CodePrimitiveExpression(42);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Add;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region MapOperatorToMethod Tests - Ambiguous Operators
        [Fact]
        public void MapOperatorToMethod_WithIncompatibleTypes_ReturnsErrorAndNull()
        {
            // Arrange
            Type lhs = typeof(string);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression("test");
            CodeExpression rhsExpression = new CodePrimitiveExpression(42);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Subtract;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.Null(result);
            Assert.NotNull(error);
        }
        #endregion

        #region DefaultOperators Tests - Addition
        [Fact]
        public void DefaultOperators_IntAddition_ReturnsCorrectResult()
        {
            // Arrange
            int x = 10;
            int y = 20;

            // Act
            int result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void DefaultOperators_UIntAddition_ReturnsCorrectResult()
        {
            // Arrange
            uint x = 100U;
            uint y = 200U;

            // Act
            uint result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(300U, result);
        }

        [Fact]
        public void DefaultOperators_LongAddition_ReturnsCorrectResult()
        {
            // Arrange
            long x = 1000L;
            long y = 2000L;

            // Act
            long result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(3000L, result);
        }

        [Fact]
        public void DefaultOperators_ULongAddition_ReturnsCorrectResult()
        {
            // Arrange
            ulong x = 5000UL;
            ulong y = 7000UL;

            // Act
            ulong result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(12000UL, result);
        }

        [Fact]
        public void DefaultOperators_FloatAddition_ReturnsCorrectResult()
        {
            // Arrange
            float x = 2.5f;
            float y = 3.5f;

            // Act
            float result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(6.0f, result);
        }

        [Fact]
        public void DefaultOperators_DoubleAddition_ReturnsCorrectResult()
        {
            // Arrange
            double x = 1.5;
            double y = 2.5;

            // Act
            double result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(4.0, result);
        }

        [Fact]
        public void DefaultOperators_DecimalAddition_ReturnsCorrectResult()
        {
            // Arrange
            decimal x = 10.5m;
            decimal y = 20.5m;

            // Act
            decimal result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal(31.0m, result);
        }

        [Fact]
        public void DefaultOperators_StringAddition_ReturnsCorrectResult()
        {
            // Arrange
            string x = "Hello";
            string y = " World";

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void DefaultOperators_StringAdditionWithObject_ReturnsCorrectResult()
        {
            // Arrange
            string x = "Value: ";
            object y = 42;

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("Value: 42", result);
        }

        [Fact]
        public void DefaultOperators_ObjectAdditionWithString_ReturnsCorrectResult()
        {
            // Arrange
            object x = 42;
            string y = " is the answer";

            // Act
            string result = Literal.DefaultOperators.Addition(x, y);

            // Assert
            Assert.Equal("42 is the answer", result);
        }
        #endregion

        #region DefaultOperators Tests - Subtraction
        [Fact]
        public void DefaultOperators_IntSubtraction_ReturnsCorrectResult()
        {
            // Arrange
            int x = 50;
            int y = 20;

            // Act
            int result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void DefaultOperators_UIntSubtraction_ReturnsCorrectResult()
        {
            // Arrange
            uint x = 300U;
            uint y = 100U;

            // Act
            uint result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(200U, result);
        }

        [Fact]
        public void DefaultOperators_DecimalSubtraction_ReturnsCorrectResult()
        {
            // Arrange
            decimal x = 100.5m;
            decimal y = 50.5m;

            // Act
            decimal result = Literal.DefaultOperators.Subtraction(x, y);

            // Assert
            Assert.Equal(50.0m, result);
        }
        #endregion

        #region DefaultOperators Tests - Multiplication
        [Fact]
        public void DefaultOperators_IntMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            int x = 5;
            int y = 7;

            // Act
            int result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(35, result);
        }

        [Fact]
        public void DefaultOperators_FloatMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            float x = 2.5f;
            float y = 4f;

            // Act
            float result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(10f, result);
        }

        [Fact]
        public void DefaultOperators_DecimalMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            decimal x = 10m;
            decimal y = 5m;

            // Act
            decimal result = Literal.DefaultOperators.Multiply(x, y);

            // Assert
            Assert.Equal(50m, result);
        }
        #endregion

        #region DefaultOperators Tests - Division
        [Fact]
        public void DefaultOperators_IntDivision_ReturnsCorrectResult()
        {
            // Arrange
            int x = 100;
            int y = 5;

            // Act
            int result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(20, result);
        }

        [Fact]
        public void DefaultOperators_DoubleDivision_ReturnsCorrectResult()
        {
            // Arrange
            double x = 10.0;
            double y = 4.0;

            // Act
            double result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(2.5, result);
        }

        [Fact]
        public void DefaultOperators_DecimalDivision_ReturnsCorrectResult()
        {
            // Arrange
            decimal x = 100m;
            decimal y = 8m;

            // Act
            decimal result = Literal.DefaultOperators.Division(x, y);

            // Assert
            Assert.Equal(12.5m, result);
        }
        #endregion

        #region DefaultOperators Tests - Modulus
        [Fact]
        public void DefaultOperators_IntModulus_ReturnsCorrectResult()
        {
            // Arrange
            int x = 10;
            int y = 3;

            // Act
            int result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void DefaultOperators_UIntModulus_ReturnsCorrectResult()
        {
            // Arrange
            uint x = 17U;
            uint y = 5U;

            // Act
            uint result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(2U, result);
        }

        [Fact]
        public void DefaultOperators_DecimalModulus_ReturnsCorrectResult()
        {
            // Arrange
            decimal x = 10.5m;
            decimal y = 3m;

            // Act
            decimal result = Literal.DefaultOperators.Modulus(x, y);

            // Assert
            Assert.Equal(1.5m, result);
        }
        #endregion

        #region DefaultOperators Tests - Bitwise Operations
        [Fact]
        public void DefaultOperators_IntBitwiseAnd_ReturnsCorrectResult()
        {
            // Arrange
            int x = 15; // 1111 in binary
            int y = 7;  // 0111 in binary

            // Act
            int result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(7, result); // 0111 in binary
        }

        [Fact]
        public void DefaultOperators_ULongBitwiseAnd_ReturnsCorrectResult()
        {
            // Arrange
            ulong x = 255UL;
            ulong y = 127UL;

            // Act
            ulong result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.Equal(127UL, result);
        }

        [Fact]
        public void DefaultOperators_BoolBitwiseAnd_ReturnsCorrectResult()
        {
            // Arrange
            bool x = true;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseAnd(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DefaultOperators_IntBitwiseOr_ReturnsCorrectResult()
        {
            // Arrange
            int x = 8; // 1000 in binary
            int y = 4; // 0100 in binary

            // Act
            int result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.Equal(12, result); // 1100 in binary
        }

        [Fact]
        public void DefaultOperators_BoolBitwiseOr_ReturnsCorrectResult()
        {
            // Arrange
            bool x = true;
            bool y = false;

            // Act
            bool result = Literal.DefaultOperators.BitwiseOr(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region DefaultOperators Tests - Equality
        [Fact]
        public void DefaultOperators_IntEquality_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            int x = 42;
            int y = 42;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_IntEquality_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            int x = 42;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DefaultOperators_FloatEquality_WithSimilarValues_ReturnsTrue()
        {
            // Arrange
            float x = 3.14f;
            float y = 3.14f;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_DoubleEquality_WithSimilarValues_ReturnsTrue()
        {
            // Arrange
            double x = 2.71828;
            double y = 2.71828;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_StringEquality_WithSameStrings_ReturnsTrue()
        {
            // Arrange
            string x = "test";
            string y = "test";

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_StringEquality_WithDifferentStrings_ReturnsFalse()
        {
            // Arrange
            string x = "test1";
            string y = "test2";

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DefaultOperators_BoolEquality_WithSameValues_ReturnsTrue()
        {
            // Arrange
            bool x = true;
            bool y = true;

            // Act
            bool result = Literal.DefaultOperators.Equality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_ObjectEquality_WithSameReference_ReturnsTrue()
        {
            // Arrange
            object x = new();
            object y = x;

            // Act
            bool result = Literal.DefaultOperators.ObjectEquality(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_ObjectEquality_WithDifferentReferences_ReturnsFalse()
        {
            // Arrange
            object x = new();
            object y = new();

            // Act
            bool result = Literal.DefaultOperators.ObjectEquality(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DefaultOperators_ObjectEquality_WithNulls_ReturnsTrue()
        {
            // Arrange
            object? x = null;
            object? y = null;

            // Act
            bool result = Literal.DefaultOperators.ObjectEquality(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region DefaultOperators Tests - Relational Operations
        [Fact]
        public void DefaultOperators_IntGreaterThan_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            int x = 100;
            int y = 50;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_IntGreaterThan_WithSmallerFirst_ReturnsFalse()
        {
            // Arrange
            int x = 50;
            int y = 100;

            // Act
            bool result = Literal.DefaultOperators.GreaterThan(x, y);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DefaultOperators_DecimalLessThan_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            decimal x = 25.5m;
            decimal y = 100.5m;

            // Act
            bool result = Literal.DefaultOperators.LessThan(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_ULongGreaterThanOrEqual_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            ulong x = 1000UL;
            ulong y = 1000UL;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_IntLessThanOrEqual_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            int x = 42;
            int y = 42;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_FloatGreaterThanOrEqual_WithLargerFirst_ReturnsTrue()
        {
            // Arrange
            float x = 10.5f;
            float y = 5.2f;

            // Act
            bool result = Literal.DefaultOperators.GreaterThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DefaultOperators_DoubleLessThanOrEqual_WithSmallerFirst_ReturnsTrue()
        {
            // Arrange
            double x = 1.23;
            double y = 4.56;

            // Act
            bool result = Literal.DefaultOperators.LessThanOrEqual(x, y);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region MakeLiteral Tests - Edge Cases
        [Fact]
        public void MakeLiteral_WithIntMaxValue_CreatesIntLiteral()
        {
            // Arrange
            Type type = typeof(int);
            object value = int.MaxValue;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IntLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithIntMinValue_CreatesIntLiteral()
        {
            // Arrange
            Type type = typeof(int);
            object value = int.MinValue;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IntLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithEmptyString_CreatesStringLiteral()
        {
            // Arrange
            Type type = typeof(string);
            object value = string.Empty;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StringLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithZeroByte_CreatesByteLiteral()
        {
            // Arrange
            Type type = typeof(byte);
            object value = (byte)0;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ByteLiteral>(result);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void MakeLiteral_WithByteMaxValue_CreatesByteLiteral()
        {
            // Arrange
            Type type = typeof(byte);
            object value = byte.MaxValue;

            // Act
            var result = Literal.MakeLiteral(type, value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ByteLiteral>(result);
            Assert.Equal(value, result.Value);
        }
        #endregion

        #region AllowedComparison Tests - Nullable Types
        [Fact]
        public void AllowedComparison_NullableIntWithInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(int?);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100);
            CodeExpression rhsExpression = new CodePrimitiveExpression(50);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.GreaterThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_NullableIntWithNullableInt_Succeeds()
        {
            // Arrange
            Type lhs = typeof(int?);
            Type rhs = typeof(int?);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100);
            CodeExpression rhsExpression = new CodePrimitiveExpression(50);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThan;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void AllowedComparison_NullableDecimalWithDecimal_Succeeds()
        {
            // Arrange
            Type lhs = typeof(decimal?);
            Type rhs = typeof(decimal);
            CodeExpression lhsExpression = new CodePrimitiveExpression(99.99m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(100m);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.LessThanOrEqual;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region AllowedComparison Tests - All Comparison Operators
        [Theory]
        [InlineData(CodeBinaryOperatorType.ValueEquality)]
        [InlineData(CodeBinaryOperatorType.LessThan)]
        [InlineData(CodeBinaryOperatorType.LessThanOrEqual)]
        [InlineData(CodeBinaryOperatorType.GreaterThan)]
        [InlineData(CodeBinaryOperatorType.GreaterThanOrEqual)]
        public void AllowedComparison_AllComparisonOperatorsWithIntegers_Succeed(CodeBinaryOperatorType op)
        {
            // Arrange
            Type lhs = typeof(int);
            Type rhs = typeof(int);
            CodeExpression lhsExpression = new CodePrimitiveExpression(50);
            CodeExpression rhsExpression = new CodePrimitiveExpression(100);
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
            Assert.Equal(typeof(bool), result.ExpressionType);
        }

        [Theory]
        [InlineData(CodeBinaryOperatorType.ValueEquality)]
        [InlineData(CodeBinaryOperatorType.LessThan)]
        [InlineData(CodeBinaryOperatorType.LessThanOrEqual)]
        [InlineData(CodeBinaryOperatorType.GreaterThan)]
        [InlineData(CodeBinaryOperatorType.GreaterThanOrEqual)]
        public void AllowedComparison_AllComparisonOperatorsWithDecimals_Succeed(CodeBinaryOperatorType op)
        {
            // Arrange
            Type lhs = typeof(decimal);
            Type rhs = typeof(decimal);
            CodeExpression lhsExpression = new CodePrimitiveExpression(12.34m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(56.78m);
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.AllowedComparison(lhs, lhsExpression, rhs, rhsExpression, op, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region MapOperatorToMethod Tests - Nullable Types
        [Fact]
        public void MapOperatorToMethod_NullableIntAddition_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(int?);
            Type rhs = typeof(int?);
            CodeExpression lhsExpression = new CodePrimitiveExpression(10);
            CodeExpression rhsExpression = new CodePrimitiveExpression(20);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Add;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void MapOperatorToMethod_NullableDecimalSubtraction_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(decimal?);
            Type rhs = typeof(decimal);
            CodeExpression lhsExpression = new CodePrimitiveExpression(100m);
            CodeExpression rhsExpression = new CodePrimitiveExpression(30m);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Subtract;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void MapOperatorToMethod_NullableFloatMultiplication_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(float?);
            Type rhs = typeof(float?);
            CodeExpression lhsExpression = new CodePrimitiveExpression(2.5f);
            CodeExpression rhsExpression = new CodePrimitiveExpression(4f);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.Multiply;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void MapOperatorToMethod_NullableDoubleEquality_ReturnsValidMethod()
        {
            // Arrange
            Type lhs = typeof(double?);
            Type rhs = typeof(double?);
            CodeExpression lhsExpression = new CodePrimitiveExpression(3.14);
            CodeExpression rhsExpression = new CodePrimitiveExpression(3.14);
            CodeBinaryOperatorType op = CodeBinaryOperatorType.ValueEquality;
            RuleValidation validator = new(typeof(TestClass), null);

            // Act
            var result = Literal.MapOperatorToMethod(op, lhs, lhsExpression, rhs, rhsExpression, validator, out ValidationError error);

            // Assert
            Assert.NotNull(result);
            Assert.Null(error);
        }
        #endregion

        #region Helper Test Class
        private class TestClass
        {
            public int IntValue { get; set; }//NOSONAR needed for testing
            public string StringValue { get; set; }//NOSONAR needed for testing
        }
        #endregion
    }
}
