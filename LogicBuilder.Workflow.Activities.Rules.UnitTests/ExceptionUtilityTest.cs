using System;
using System.Threading;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests
{
    public class ExceptionUtilityTest
    {
        #region IsCriticalException Tests

        [Fact]
        public void IsCriticalException_WithOutOfMemoryException_ReturnsTrue()
        {
            var exception = new OutOfMemoryException("Out of memory");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.True(result);
        }

        [Fact]
        public void IsCriticalException_WithStackOverflowException_ReturnsTrue()
        {
            var exception = new StackOverflowException("Stack overflow");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.True(result);
        }

        [Fact]
        public void IsCriticalException_WithThreadInterruptedException_ReturnsTrue()
        {
            var exception = new ThreadInterruptedException("Thread interrupted");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.True(result);
        }

        [Fact]
        public void IsCriticalException_WithArgumentException_ReturnsFalse()
        {
            var exception = new ArgumentException("Argument error");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithInvalidOperationException_ReturnsFalse()
        {
            var exception = new InvalidOperationException("Invalid operation");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithNullReferenceException_ReturnsFalse()
        {
            var exception = new NullReferenceException("Null reference");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithGenericException_ReturnsFalse()
        {
            var exception = new Exception("Generic error");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithDivideByZeroException_ReturnsFalse()
        {
            var exception = new DivideByZeroException("Divide by zero");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithIndexOutOfRangeException_ReturnsFalse()
        {
            var exception = new IndexOutOfRangeException("Index out of range");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithFormatException_ReturnsFalse()
        {
            var exception = new FormatException("Format error");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        [Fact]
        public void IsCriticalException_WithNotSupportedException_ReturnsFalse()
        {
            var exception = new NotSupportedException("Not supported");

            var result = ExceptionUtility.IsCriticalException(exception);

            Assert.False(result);
        }

        #endregion
    }
}
