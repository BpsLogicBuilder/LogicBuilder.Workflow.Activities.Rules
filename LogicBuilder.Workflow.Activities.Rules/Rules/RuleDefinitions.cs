// ---------------------------------------------------------------------------
// Copyright (C) 2006 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

#define CODE_ANALYSIS
using System;
using System.ComponentModel;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region class RuleDefinitions

    public sealed class RuleDefinitions
    {

        private RuleConditionCollection conditions;
        private RuleSetCollection ruleSets;
        private bool runtimeInitialized;
        [NonSerialized]
        private readonly object syncLock = new();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RuleConditionCollection Conditions
        {
            get
            {
                this.conditions ??= [];
                return conditions;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RuleSetCollection RuleSets
        {
            get
            {
                this.ruleSets ??= [];
                return this.ruleSets;
            }
        }

        internal void OnRuntimeInitialized()
        {
            lock (syncLock)
            {
                if (runtimeInitialized)
                    return;
                Conditions.OnRuntimeInitialized();
                RuleSets.OnRuntimeInitialized();
                runtimeInitialized = true;
            }
        }

        internal RuleDefinitions Clone()
        {
            RuleDefinitions newRuleDefinitions = new();

            if (this.ruleSets != null)
            {
                newRuleDefinitions.ruleSets = [];
                foreach (RuleSet r in this.ruleSets)
                    newRuleDefinitions.ruleSets.Add(r.Clone());
            }

            if (this.conditions != null)
            {
                newRuleDefinitions.conditions = [];
                foreach (RuleCondition r in this.conditions)
                    newRuleDefinitions.conditions.Add(r.Clone());
            }

            return newRuleDefinitions;
        }
    }
    #endregion
}
