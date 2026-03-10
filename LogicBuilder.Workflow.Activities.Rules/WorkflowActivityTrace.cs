using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("LogicBuilder.Workflow.Activities.Rules.UnitTests, PublicKey=002400000480000094000000060200000024000052534131000400000100010059b59302e7303accd5cc84fd482cae54dea8d8b8de7faaef37abbac4b08e3d91283087f48ae04c4fdd117752a3fcafcda61cd2099e2d5432b9bce70e5fe083b15e43cd652617b06dc1422d347ffe7b2aeb7b466e567c6988f26dccbf9723b4b57b1aeaa0a2dbd00478d7135da9bb04a6138d5f29e54ac7e9ac9ae3b7956cf6c2")]
namespace LogicBuilder.Workflow.Activities
{
    internal static class WorkflowActivityTrace
    {
        private const string TraceSourceName = "LogicBuilder.Workflow.Activities.Rules";
        static readonly TraceSource rules = new(TraceSourceName)
        {
            Switch = new SourceSwitch(TraceSourceName, SourceLevels.Off.ToString())
        };

        internal static TraceSource Rules
        {
            get { return rules; }
        }

        /// <summary>
        /// Statically set up trace sources
        /// 
        /// To enable logging to a file, add lines like the following to your app config file.
        /*
            <system.diagnostics>
                <switches>
                    <add name="LogicBuilder.Workflow LogToFile" value="1" />
                </switches>
            </system.diagnostics>
        */
        /// To enable tracing to default trace listeners, add lines like the following
        /*
            <system.diagnostics>
                <switches>
                    <add name="LogicBuilder.Workflow LogToTraceListener" value="1" />
                </switches>
            </system.diagnostics>
        */
        /// </summary>
        static WorkflowActivityTrace()
        {
            foreach (TraceListener listener in Trace.Listeners)
            {
                if (listener is not DefaultTraceListener)
                {
                    rules.Listeners.Add(listener);
                }
            }
        }
    }
}
