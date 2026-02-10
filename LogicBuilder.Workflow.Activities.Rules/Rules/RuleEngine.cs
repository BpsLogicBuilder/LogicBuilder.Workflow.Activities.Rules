// ---------------------------------------------------------------------------
// Copyright (C) 2006 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

#define CODE_ANALYSIS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace LogicBuilder.Workflow.Activities.Rules
{
    public class RuleEngine
    {
        private readonly string name;
        private readonly RuleValidation validation;
        private readonly IList<RuleState> analyzedRules;

        public RuleEngine(RuleSet ruleSet, Type objectType)
            : this(ruleSet, new RuleValidation(objectType))
        {
        }

        public RuleEngine(RuleSet ruleSet, RuleValidation validation)
        {
            // now validate it
            if (!ruleSet.Validate(validation))
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.RuleSetValidationFailed, ruleSet.name);
                throw new RuleSetValidationException(message, validation.Errors);
            }

            this.name = ruleSet.Name;
            this.validation = validation;
            Tracer tracer = null;
            if (WorkflowActivityTrace.Rules.Switch.ShouldTrace(TraceEventType.Information))
                tracer = new Tracer(ruleSet.Name);
            this.analyzedRules = Executor.Preprocess(ruleSet.ChainingBehavior, ruleSet.Rules, validation, tracer);
        }

        public void Execute(object thisObject)
        {
            Execute(new RuleExecution(validation, thisObject));
        }

        internal void Execute(RuleExecution ruleExecution)
        {
            if (ruleExecution == null)
                throw new ArgumentNullException(nameof(ruleExecution));

            Tracer tracer = null;
            if (WorkflowActivityTrace.Rules.Switch.ShouldTrace(TraceEventType.Information))
            {
                tracer = new Tracer(name);
                tracer.StartRuleSet();
            }
            Executor.ExecuteRuleSet(analyzedRules, ruleExecution, tracer);
        }
    }
}
