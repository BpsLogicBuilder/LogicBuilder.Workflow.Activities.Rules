using System;
using System.Runtime.Serialization;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleEvaluationExceptionTest
    {
        #region Constructor Tests

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var exception = new RuleEvaluationException();

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            var message = "Evaluation error occurred";

            var exception = new RuleEvaluationException(message);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithNullMessage_CreatesInstance()
        {
            var exception = new RuleEvaluationException((string)null!);

            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsBoth()
        {
            var message = "Evaluation error occurred";
            var innerException = new InvalidOperationException("Inner exception");

            var exception = new RuleEvaluationException(message, innerException);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndNullInnerException_SetsMessage()
        {
            var message = "Evaluation error occurred";

            var exception = new RuleEvaluationException(message, null!);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        #endregion

        #region Inheritance Tests

        [Fact]
        public void RuleEvaluationException_InheritsFromRuleException()
        {
            var exception = new RuleEvaluationException();

            Assert.IsType<RuleException>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleEvaluationException_InheritsFromException()
        {
            var exception = new RuleEvaluationException();

            Assert.IsType<Exception>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleEvaluationException_ImplementsISerializable()
        {
            var exception = new RuleEvaluationException();

            Assert.IsType<ISerializable>(exception, exactMatch: false);
        }

        #endregion

        #region Exception Behavior Tests

        [Fact]
        public void CanBeThrown_WithDefaultConstructor()
        {
            RuleEvaluationException? caughtException;
            try
            {
                throw new RuleEvaluationException();
            }
            catch (RuleEvaluationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
        }

        [Fact]
        public void CanBeThrown_WithMessage()
        {
            var message = "Evaluation error occurred";
            RuleEvaluationException? caughtException;

            try
            {
                throw new RuleEvaluationException(message);
            }
            catch (RuleEvaluationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeThrown_WithMessageAndInnerException()
        {
            var message = "Evaluation error occurred";
            var innerException = new ArgumentException("Inner exception");
            RuleEvaluationException? caughtException;

            try
            {
                throw new RuleEvaluationException(message, innerException);
            }
            catch (RuleEvaluationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
            Assert.Same(innerException, caughtException.InnerException);
        }

        [Fact]
        public void CanBeCaught_AsRuleException()
        {
            var message = "Evaluation error occurred";
            RuleException? caughtException;

            try
            {
                throw new RuleEvaluationException(message);
            }
            catch (RuleException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsType<RuleEvaluationException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeCaught_AsException()
        {
            var message = "Evaluation error occurred";
            Exception? caughtException;

            try
            {
                throw new RuleEvaluationException(message);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsType<RuleEvaluationException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Message_IsAccessible()
        {
            var message = "Evaluation error occurred";
            var exception = new RuleEvaluationException(message);

            var actualMessage = exception.Message;

            Assert.Equal(message, actualMessage);
        }

        [Fact]
        public void InnerException_IsAccessible()
        {
            var innerException = new InvalidOperationException("Inner exception");
            var exception = new RuleEvaluationException("Test message", innerException);

            var actualInnerException = exception.InnerException;

            Assert.Same(innerException, actualInnerException);
        }

        [Fact]
        public void ToString_ContainsExceptionType()
        {
            var exception = new RuleEvaluationException("Test message");

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains("RuleEvaluationException", result);
        }

        [Fact]
        public void ToString_ContainsMessage()
        {
            var message = "Evaluation error occurred";
            var exception = new RuleEvaluationException(message);

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains(message, result);
        }

        #endregion
    }
}
