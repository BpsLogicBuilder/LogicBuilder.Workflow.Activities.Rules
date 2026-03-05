using System;
using System.CodeDom;
using System.Runtime.Serialization;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleEvaluationIncompatibleTypesExceptionTest
    {
        #region Constructor Tests

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.Left);
            Assert.Null(exception.Right);
        }

        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            var message = "Incompatible types error";

            var exception = new RuleEvaluationIncompatibleTypesException(message);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithNullMessage_CreatesInstance()
        {
            var exception = new RuleEvaluationIncompatibleTypesException((string)null!);

            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsBoth()
        {
            var message = "Incompatible types error";
            var innerException = new InvalidOperationException("Inner exception");

            var exception = new RuleEvaluationIncompatibleTypesException(message, innerException);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageLeftOpRight_SetsAllProperties()
        {
            var message = "Incompatible types error";
            var leftType = typeof(int);
            var op = CodeBinaryOperatorType.Add;
            var rightType = typeof(string);

            var exception = new RuleEvaluationIncompatibleTypesException(message, leftType, op, rightType);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(leftType, exception.Left);
            Assert.Equal(op, exception.Operator);
            Assert.Same(rightType, exception.Right);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageLeftOpRightInnerException_SetsAllProperties()
        {
            var message = "Incompatible types error";
            var leftType = typeof(double);
            var op = CodeBinaryOperatorType.Multiply;
            var rightType = typeof(bool);
            var innerException = new ArgumentException("Inner exception");

            var exception = new RuleEvaluationIncompatibleTypesException(
                message, leftType, op, rightType, innerException);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(leftType, exception.Left);
            Assert.Equal(op, exception.Operator);
            Assert.Same(rightType, exception.Right);
            Assert.Same(innerException, exception.InnerException);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Left_CanBeSet()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();
            var leftType = typeof(long);

            exception.Left = leftType;

            Assert.Same(leftType, exception.Left);
        }

        [Fact]
        public void Operator_CanBeSet()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();
            var op = CodeBinaryOperatorType.Subtract;

            exception.Operator = op;

            Assert.Equal(op, exception.Operator);
        }

        [Fact]
        public void Right_CanBeSet()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();
            var rightType = typeof(decimal);

            exception.Right = rightType;

            Assert.Same(rightType, exception.Right);
        }

        [Fact]
        public void Properties_CanBeSetToNull()
        {
            var exception = new RuleEvaluationIncompatibleTypesException(
                "test", typeof(int), CodeBinaryOperatorType.Add, typeof(string))
            {
                Left = null,
                Right = null
            };

            Assert.Null(exception.Left);
            Assert.Null(exception.Right);
        }

        #endregion

        #region Inheritance Tests

        [Fact]
        public void RuleEvaluationIncompatibleTypesException_InheritsFromRuleException()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();

            Assert.IsType<RuleException>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleEvaluationIncompatibleTypesException_InheritsFromException()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();

            Assert.IsType<Exception>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleEvaluationIncompatibleTypesException_ImplementsISerializable()
        {
            var exception = new RuleEvaluationIncompatibleTypesException();

            Assert.IsType<ISerializable>(exception, exactMatch: false);
        }

        #endregion

        #region Exception Behavior Tests

        [Fact]
        public void CanBeThrown_WithDefaultConstructor()
        {
            RuleEvaluationIncompatibleTypesException? caughtException;

            try
            {
                throw new RuleEvaluationIncompatibleTypesException();
            }
            catch (RuleEvaluationIncompatibleTypesException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
        }

        [Fact]
        public void CanBeThrown_WithMessage()
        {
            var message = "Incompatible types error";
            RuleEvaluationIncompatibleTypesException caughtException;

            try
            {
                throw new RuleEvaluationIncompatibleTypesException(message);
            }
            catch (RuleEvaluationIncompatibleTypesException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeThrown_WithMessageAndTypeInfo()
        {
            var message = "Incompatible types error";
            var leftType = typeof(int);
            var op = CodeBinaryOperatorType.Divide;
            var rightType = typeof(string);
            RuleEvaluationIncompatibleTypesException caughtException;

            try
            {
                throw new RuleEvaluationIncompatibleTypesException(message, leftType, op, rightType);
            }
            catch (RuleEvaluationIncompatibleTypesException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
            Assert.Same(leftType, caughtException.Left);
            Assert.Equal(op, caughtException.Operator);
            Assert.Same(rightType, caughtException.Right);
        }

        [Fact]
        public void CanBeCaught_AsRuleException()
        {
            var message = "Incompatible types error";
            RuleException caughtException;

            try
            {
                throw new RuleEvaluationIncompatibleTypesException(message);
            }
            catch (RuleException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsType<RuleEvaluationIncompatibleTypesException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeCaught_AsException()
        {
            var message = "Incompatible types error";
            Exception caughtException;

            try
            {
                throw new RuleEvaluationIncompatibleTypesException(message);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsType<RuleEvaluationIncompatibleTypesException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        #endregion

        #region ToString Tests

        [Fact]
        public void ToString_ContainsExceptionType()
        {
            var exception = new RuleEvaluationIncompatibleTypesException("Test message");

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains("RuleEvaluationIncompatibleTypesException", result);
        }

        [Fact]
        public void ToString_ContainsMessage()
        {
            var message = "Incompatible types error";
            var exception = new RuleEvaluationIncompatibleTypesException(message);

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains(message, result);
        }

        #endregion

        #region Operator Type Tests

        [Theory]
        [InlineData(CodeBinaryOperatorType.Add)]
        [InlineData(CodeBinaryOperatorType.Subtract)]
        [InlineData(CodeBinaryOperatorType.Multiply)]
        [InlineData(CodeBinaryOperatorType.Divide)]
        [InlineData(CodeBinaryOperatorType.Modulus)]
        [InlineData(CodeBinaryOperatorType.BitwiseAnd)]
        [InlineData(CodeBinaryOperatorType.BitwiseOr)]
        [InlineData(CodeBinaryOperatorType.GreaterThan)]
        [InlineData(CodeBinaryOperatorType.LessThan)]
        [InlineData(CodeBinaryOperatorType.IdentityEquality)]
        public void Constructor_WithDifferentOperators_SetsOperatorCorrectly(CodeBinaryOperatorType op)
        {
            var exception = new RuleEvaluationIncompatibleTypesException(
                "test", typeof(int), op, typeof(string));

            Assert.Equal(op, exception.Operator);
        }

        #endregion
    }
}
