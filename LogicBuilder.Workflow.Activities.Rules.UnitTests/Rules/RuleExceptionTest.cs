using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleExceptionTest
    {
        #region Constructor Tests

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            // Act
            var exception = new RuleException();

            // Assert
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            // Arrange
            var message = "Test error message";

            // Act
            var exception = new RuleException(message);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithNullMessage_CreatesInstance()
        {
            // Act
            var exception = new RuleException((string)null!);

            // Assert
            Assert.NotNull(exception);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsBoth()
        {
            // Arrange
            var message = "Test error message";
            var innerException = new InvalidOperationException("Inner exception");

            // Act
            var exception = new RuleException(message, innerException);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndNullInnerException_SetsMessage()
        {
            // Arrange
            var message = "Test error message";

            // Act
            var exception = new RuleException(message, null!);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Null(exception.InnerException);
        }

        #endregion

        #region Inheritance Tests

        [Fact]
        public void RuleException_InheritsFromException()
        {
            // Arrange
            var exception = new RuleException();

            // Assert
            Assert.IsType<Exception>(exception, exactMatch: false);
        }

        [Fact]
        public void RuleException_ImplementsISerializable()
        {
            // Arrange
            var exception = new RuleException();

            // Assert
            Assert.IsType<ISerializable>(exception, exactMatch: false);
        }

        #endregion

        #region Exception Behavior Tests

        [Fact]
        public async Task CanBeThrown_WithDefaultConstructor()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<RuleException>(() => throw new RuleException());
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task TaskCanBeThrown_WithMessage()
        {
            // Arrange
            var message = "Test error message";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RuleException>(() => throw new RuleException(message));
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task CanBeThrown_WithMessageAndInnerException()
        {
            // Arrange
            var message = "Test error message";
            var innerException = new ArgumentException("Inner exception");

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RuleException>(() => 
                throw new RuleException(message, innerException));
            Assert.Equal(message, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public async Task CanBeCaught_AsException()
        {
            // Arrange
            var message = "Test error message";

            // Act
            Exception caughtException = await Assert.ThrowsAsync<RuleException>(() => throw new RuleException(message));

            // Assert
            Assert.NotNull(caughtException);
            Assert.IsType<RuleException>(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        [Fact]
        public void CanBeCaught_AsRuleException()
        {
            // Arrange
            var message = "Test error message";
            RuleException? caughtException;

            // Act
            try
            {
                throw new RuleException(message);
            }
            catch (RuleException ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
            Assert.Equal(message, caughtException.Message);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void Message_IsAccessible()
        {
            // Arrange
            var message = "Test error message";
            var exception = new RuleException(message);

            // Act
            var actualMessage = exception.Message;

            // Assert
            Assert.Equal(message, actualMessage);
        }

        [Fact]
        public void InnerException_IsAccessible()
        {
            // Arrange
            var innerException = new InvalidOperationException("Inner exception");
            var exception = new RuleException("Test message", innerException);

            // Act
            var actualInnerException = exception.InnerException;

            // Assert
            Assert.Same(innerException, actualInnerException);
        }

        [Fact]
        public void ToString_ContainsExceptionType()
        {
            // Arrange
            var exception = new RuleException("Test message");

            // Act
            var result = exception.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.Contains("RuleException", result);
        }

        [Fact]
        public void ToString_ContainsMessage()
        {
            // Arrange
            var message = "Test error message";
            var exception = new RuleException(message);

            // Act
            var result = exception.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(message, result);
        }

        #endregion
    }
}