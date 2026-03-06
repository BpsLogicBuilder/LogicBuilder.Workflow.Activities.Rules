using System;
using System.Runtime.Serialization;
using LogicBuilder.Workflow.ComponentModel.Compiler;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleSetValidationExceptionTest
    {
        #region Constructor Tests

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var exception = new RuleSetValidationException();

            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Null(exception.Errors);
        }

        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            var message = "Validation error occurred";

            var exception = new RuleSetValidationException(message);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithNullMessage_CreatesInstance()
        {
            var exception = new RuleSetValidationException((string)null!);

            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsBoth()
        {
            var message = "Validation error occurred";
            var innerException = new InvalidOperationException("Inner exception");

            var exception = new RuleSetValidationException(message, innerException);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndErrors_SetsBoth()
        {
            var message = "Validation error occurred";
            var errors = new ValidationErrorCollection
            {
                new ValidationError("Error 1", 100),
                new ValidationError("Error 2", 200)
            };

            var exception = new RuleSetValidationException(message, errors);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(errors, exception.Errors);
            Assert.Equal(2, exception.Errors.Count);
        }

        [Fact]
        public void Constructor_WithMessageAndNullErrors_SetsMessage()
        {
            var message = "Validation error occurred";

            var exception = new RuleSetValidationException(message, (ValidationErrorCollection)null!);

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.Errors);
        }

        #endregion

        #region Inheritance Tests

        [Fact]
        public void RuleSetValidationException_InheritsFromRuleException()
        {
            var exception = new RuleSetValidationException();

            Assert.IsType<RuleException>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleSetValidationException_InheritsFromException()
        {
            var exception = new RuleSetValidationException();

            Assert.IsType<Exception>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleSetValidationException_ImplementsISerializable()
        {
            var exception = new RuleSetValidationException();

            Assert.IsType<ISerializable>(exception, exactMatch: false);
        }

        #endregion

        #region Exception Behavior Tests

        [Fact]
        public void CanBeThrown_WithDefaultConstructor()
        {
            RuleSetValidationException? caughtException;

            try
            {
                throw new RuleSetValidationException();
            }
            catch (RuleSetValidationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
        }

        [Fact]
        public void CanBeThrown_WithMessage()
        {
            var message = "Validation error occurred";
            RuleSetValidationException? caughtException ;

            try
            {
                throw new RuleSetValidationException(message);
            }
            catch (RuleSetValidationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeThrown_WithMessageAndErrors()
        {
            var message = "Validation error occurred";
            var errors = new ValidationErrorCollection
            {
                new ValidationError("Error 1", 100)
            };
            RuleSetValidationException caughtException;

            try
            {
                throw new RuleSetValidationException(message, errors);
            }
            catch (RuleSetValidationException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
            Assert.Same(errors, caughtException.Errors);
        }

        [Fact]
        public void CanBeCaught_AsRuleException()
        {
            var message = "Validation error occurred";
            RuleException caughtException;

            try
            {
                throw new RuleSetValidationException(message);
            }
            catch (RuleException ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsType<RuleSetValidationException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Message_IsAccessible()
        {
            var message = "Validation error occurred";
            var exception = new RuleSetValidationException(message);

            var actualMessage = exception.Message;

            Assert.Equal(message, actualMessage);
        }

        [Fact]
        public void InnerException_IsAccessible()
        {
            var innerException = new InvalidOperationException("Inner exception");
            var exception = new RuleSetValidationException("Test message", innerException);

            var actualInnerException = exception.InnerException;

            Assert.Same(innerException, actualInnerException);
        }

        [Fact]
        public void Errors_IsAccessible()
        {
            var errors = new ValidationErrorCollection
            {
                new ValidationError("Error 1", 100),
                new ValidationError("Error 2", 200),
                new ValidationError("Error 3", 300)
            };
            var exception = new RuleSetValidationException("Test message", errors);

            var actualErrors = exception.Errors;

            Assert.Same(errors, actualErrors);
            Assert.Equal(3, actualErrors.Count);
        }

        [Fact]
        public void Errors_IsReadOnly()
        {
            var errors = new ValidationErrorCollection
            {
                new ValidationError("Error 1", 100)
            };
            var exception = new RuleSetValidationException("Test message", errors);

            var actualErrors = exception.Errors;

            Assert.Same(errors, actualErrors);
        }

        [Fact]
        public void ToString_ContainsExceptionType()
        {
            var exception = new RuleSetValidationException("Test message");

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains("RuleSetValidationException", result);
        }

        [Fact]
        public void ToString_ContainsMessage()
        {
            var message = "Validation error occurred";
            var exception = new RuleSetValidationException(message);

            var result = exception.ToString();

            Assert.NotNull(result);
            Assert.Contains(message, result);
        }

        #endregion

        #region Errors Collection Tests

        [Fact]
        public void Errors_CanContainMultipleValidationErrors()
        {
            var errors = new ValidationErrorCollection();
            for (int i = 1; i <= 5; i++)
            {
                errors.Add(new ValidationError($"Error {i}", 100 + i));
            }
            var exception = new RuleSetValidationException("Multiple errors", errors);

            Assert.Equal(5, exception.Errors.Count);
        }

        [Fact]
        public void Errors_CanBeEmptyCollection()
        {
            var errors = new ValidationErrorCollection();
            var exception = new RuleSetValidationException("No errors", errors);

            Assert.NotNull(exception.Errors);
            Assert.Empty(exception.Errors);
        }

        #endregion
    }
}
