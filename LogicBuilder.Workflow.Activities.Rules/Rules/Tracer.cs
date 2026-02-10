// ---------------------------------------------------------------------------
// Copyright (C) 2006 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace LogicBuilder.Workflow.Activities.Rules
{
    [ExcludeFromCodeCoverage]
    internal class Tracer
    {
        private readonly string tracePrefix;

        // get the localized trace messages once
        private static readonly string traceRuleHeader = Messages.TraceRuleHeader;
        private static readonly string traceRuleSetEvaluate = Messages.TraceRuleSetEvaluate;
        private static readonly string traceRuleEvaluate = Messages.TraceRuleEvaluate;
        private static readonly string traceRuleResult = Messages.TraceRuleResult;
        private static readonly string traceRuleActions = Messages.TraceRuleActions;
        private static readonly string traceCondition = Messages.Condition;
        private static readonly string traceThen = Messages.Then;
        private static readonly string traceElse = Messages.Else;
        private static readonly string traceUpdate = Messages.TraceUpdate;
        private static readonly string traceRuleTriggers = Messages.TraceRuleTriggers;
        private static readonly string traceRuleConditionDependency = Messages.TraceRuleConditionDependency;
        private static readonly string traceRuleActionSideEffect = Messages.TraceRuleActionSideEffect;

        internal Tracer(string name)
        {
            tracePrefix = string.Format(CultureInfo.CurrentCulture, traceRuleHeader, name);
        }

        internal void StartRuleSet()
        {
            WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Information, 0, traceRuleSetEvaluate, tracePrefix);
        }

        internal void StartRule(string ruleName)
        {
            WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Verbose, 0, traceRuleEvaluate, tracePrefix, ruleName);
        }

        internal void RuleResult(string ruleName, bool result)
        {
            WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Information, 0, traceRuleResult, tracePrefix, ruleName, result.ToString());
        }

        internal void StartActions(string ruleName, bool result)
        {
            WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Verbose, 0, traceRuleActions, tracePrefix,
                (result ? traceThen : traceElse), ruleName);
        }

        internal void TraceUpdate(string ruleName, string otherName)
        {
            WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Verbose, 0, traceUpdate, tracePrefix, ruleName, otherName);
        }

        internal void TraceConditionSymbols(string ruleName, ICollection<string> symbols)
        {
            TraceRuleSymbols(traceRuleConditionDependency, traceCondition, ruleName, symbols);
        }

        internal void TraceThenSymbols(string ruleName, ICollection<string> symbols)
        {
            TraceRuleSymbols(traceRuleActionSideEffect, traceThen, ruleName, symbols);
        }

        internal void TraceElseSymbols(string ruleName, ICollection<string> symbols)
        {
            TraceRuleSymbols(traceRuleActionSideEffect, traceElse, ruleName, symbols);
        }

        private void TraceRuleSymbols(string message, string clause, string ruleName, ICollection<string> symbols)
        {
            foreach (string symbol in symbols)
                WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Verbose, 0, message, tracePrefix, ruleName, clause, symbol);
        }

        internal void TraceThenTriggers(string currentRuleName, ICollection<int> triggeredRules, List<RuleState> ruleStates)
        {
            TraceRuleTriggers(traceThen, currentRuleName, triggeredRules, ruleStates);
        }

        internal void TraceElseTriggers(string currentRuleName, ICollection<int> triggeredRules, List<RuleState> ruleStates)
        {
            TraceRuleTriggers(traceElse, currentRuleName, triggeredRules, ruleStates);
        }

        private void TraceRuleTriggers(string thenOrElse, string currentRuleName, ICollection<int> triggeredRules, List<RuleState> ruleStates)
        {
            foreach (int r in triggeredRules)
                WorkflowActivityTrace.Rules.TraceEvent(TraceEventType.Verbose, 0, traceRuleTriggers, tracePrefix, currentRuleName, thenOrElse, ruleStates[r].Rule.Name);
        }
    }
}
