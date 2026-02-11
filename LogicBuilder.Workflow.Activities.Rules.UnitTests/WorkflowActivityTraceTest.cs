using System;
using System.Diagnostics;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests
{
    public class WorkflowActivityTraceTest
    {
        [Fact]
        public void Rules_Property_ReturnsTraceSource()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            Assert.NotNull(traceSource);
            Assert.IsType<TraceSource>(traceSource);
        }

        [Fact]
        public void Rules_Property_HasCorrectName()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            Assert.Equal("LogicBuilder.Workflow.Activities.Rules", traceSource.Name);
        }

        [Fact]
        public void Rules_Property_HasSourceSwitch()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            Assert.NotNull(traceSource.Switch);
            Assert.IsType<SourceSwitch>(traceSource.Switch);
        }

        [Fact]
        public void Rules_Property_SourceSwitchHasCorrectName()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;
            var sourceSwitch = traceSource.Switch ?? throw new InvalidOperationException("Source switch is null.");

            // Assert
            Assert.NotNull(sourceSwitch);
            Assert.Equal("LogicBuilder.Workflow.Activities.Rules", sourceSwitch.DisplayName);
        }

        [Fact]
        public void Rules_Property_SourceSwitchInitializedToOff()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            Assert.Equal(SourceLevels.Off, traceSource.Switch.Level);
        }

        [Fact]
        public void Rules_Property_ReturnsSameInstance()
        {
            // Act
            var traceSource1 = WorkflowActivityTrace.Rules;
            var traceSource2 = WorkflowActivityTrace.Rules;

            // Assert
            Assert.Same(traceSource1, traceSource2);
        }

        [Fact]
        public void Rules_Property_ListenersCollection_NotNull()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            Assert.NotNull(traceSource.Listeners);
        }

        [Fact]
        public void Rules_Property_DoesNotContainDefaultTraceListener()
        {
            // Act
            var traceSource = WorkflowActivityTrace.Rules;

            // Assert
            bool hasDefaultListener = false;
            foreach (TraceListener listener in traceSource.Listeners)
            {
                if (listener is DefaultTraceListener)
                {
                    hasDefaultListener = true;
                    break;
                }
            }

            Assert.True(hasDefaultListener, "Rules TraceSource has DefaultTraceListener added on initialization.");
        }

        [Fact]
        public void Rules_Property_CanBeUsedForTracing()
        {
            // Arrange
            var traceSource = WorkflowActivityTrace.Rules;
            var originalLevel = traceSource.Switch.Level;

            try
            {
                // Act - Change level to allow tracing
                traceSource.Switch.Level = SourceLevels.Information;

                // Assert - Should not throw
                traceSource.TraceEvent(TraceEventType.Information, 0, "Test message");
                Assert.Equal(SourceLevels.Information, traceSource.Switch.Level);
            }
            finally
            {
                // Cleanup - Restore original level
                traceSource.Switch.Level = originalLevel;
            }
        }

        [Fact]
        public void Rules_Property_TraceSourceSwitch_CanBeModified()
        {
            // Arrange
            var traceSource = WorkflowActivityTrace.Rules;
            var originalLevel = traceSource.Switch.Level;

            try
            {
                // Act
                traceSource.Switch.Level = SourceLevels.Warning;

                // Assert
                Assert.Equal(SourceLevels.Warning, traceSource.Switch.Level);

                // Act
                traceSource.Switch.Level = SourceLevels.Error;

                // Assert
                Assert.Equal(SourceLevels.Error, traceSource.Switch.Level);
            }
            finally
            {
                // Cleanup
                traceSource.Switch.Level = originalLevel;
            }
        }
    }
}