// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using LogicBuilder.Workflow.Activities.Common;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;

namespace LogicBuilder.Workflow.Activities.Rules
{
    public enum RuleChainingBehavior
    {
        None,
        UpdateOnly,
        Full
    };

    [Serializable]
    public class RuleSet
    {
        internal const string RuleSetTrackingKey = "RuleSet.";
        internal string name;
        internal string description;
        internal List<Rule> rules;
        internal RuleChainingBehavior behavior = RuleChainingBehavior.Full;
        private bool runtimeInitialized;
        private readonly object syncLock = new();

        public RuleSet()
        {
            this.rules = [];
        }

        public RuleSet(string name)
            : this()
        {
            this.name = name;
        }

        public RuleSet(string name, string description)
            : this(name)
        {
            this.description = description;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (runtimeInitialized)
                    throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));
                name = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (runtimeInitialized)
                    throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));
                description = value;
            }
        }

        public RuleChainingBehavior ChainingBehavior
        {
            get { return behavior; }
            set
            {
                if (runtimeInitialized)
                    throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));
                behavior = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ICollection<Rule> Rules
        {
            get { return rules; }
        }

        public bool Validate(RuleValidation validation)
        {
            if (validation == null)
                throw new ArgumentNullException("validation");

            // Validate each rule.
            Dictionary<string, object> ruleNames = [];
            foreach (Rule r in rules)
            {
                if (!string.IsNullOrEmpty(r.Name))  // invalid names caught when validating the rule
                {
                    if (ruleNames.ContainsKey(r.Name))
                    {
                        // Duplicate rule name found.
                        ValidationError error = new(Messages.Error_DuplicateRuleName, ErrorNumbers.Error_DuplicateConditions);
                        error.UserData[RuleUserDataKeys.ErrorObject] = r;
                        validation.AddError(error);
                    }
                    else
                    {
                        ruleNames.Add(r.Name, null);
                    }
                }

                r.Validate(validation);
            }

            if (validation.Errors == null || validation.Errors.Count == 0)
                return true;

            return false;
        }

        public void Execute(RuleExecution ruleExecution)
        {
            // we have no way of knowing if the ruleset has been changed, so no caching done
            if (ruleExecution == null)
                throw new ArgumentNullException("ruleExecution");
            if (ruleExecution.Validation == null)
                throw new ArgumentException(SR.GetString(SR.Error_MissingValidationProperty), "ruleExecution");

            RuleEngine engine = new(this, ruleExecution.Validation);
            engine.Execute(ruleExecution);
        }

        public RuleSet Clone()
        {
            RuleSet newRuleSet = (RuleSet)this.MemberwiseClone();
            newRuleSet.runtimeInitialized = false;

            if (this.rules != null)
            {
                newRuleSet.rules = [];
                foreach (Rule r in this.rules)
                    newRuleSet.rules.Add(r.Clone());
            }

            return newRuleSet;
        }

        public override bool Equals(object obj)
        {
            if (obj is not RuleSet other)
                return false;
            if ((this.Name != other.Name)
                || (this.Description != other.Description)
                || (this.ChainingBehavior != other.ChainingBehavior)
                || (this.Rules.Count != other.Rules.Count))
                return false;
            // look similar, compare each rule
            for (int i = 0; i < this.rules.Count; ++i)
            {
                if (!this.rules[i].Equals(other.rules[i]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        internal void OnRuntimeInitialized()
        {
            lock (syncLock)
            {
                if (runtimeInitialized)
                    return;

                foreach (Rule rule in rules)
                {
                    rule.OnRuntimeInitialized();
                }
                runtimeInitialized = true;
            }
        }
    }
}
