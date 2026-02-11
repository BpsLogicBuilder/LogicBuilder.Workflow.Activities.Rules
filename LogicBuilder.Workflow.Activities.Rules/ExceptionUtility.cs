using System;
using System.Threading;

namespace LogicBuilder.Workflow.Activities
{
    internal static class ExceptionUtility
    {
        internal static bool IsCriticalException(Exception ex)
        {
            return ex is OutOfMemoryException
                or ThreadAbortException
                or StackOverflowException
                or ThreadInterruptedException;
        }
    }
}
