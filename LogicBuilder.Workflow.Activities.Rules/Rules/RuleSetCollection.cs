using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region class RuleSetCollection

    public sealed class RuleSetCollection : KeyedCollection<string, RuleSet>
    {
        #region members and constructors

        [NonSerialized]
        private readonly object syncLock = new();

        public RuleSetCollection()
        {
        }

        #endregion

        #region keyed collection members

        protected override string GetKeyForItem(RuleSet item)
        {
            return item.Name;
        }

        protected override void InsertItem(int index, RuleSet item)
        {
            if (this.RuntimeMode)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            if (!string.IsNullOrEmpty(item.Name) && this.Contains(item.Name))
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.RuleSetExists, item.Name);
                throw new ArgumentException(message);
            }

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (this.RuntimeMode)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, RuleSet item)
        {
            if (this.RuntimeMode)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            base.SetItem(index, item);
        }

        new public void Add(RuleSet item)
        {
            if (this.RuntimeMode)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (null == item.Name)
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.InvalidRuleSetName, "item.Name");
                throw new ArgumentException(message);
            }

            base.Add(item);
        }

        #endregion

        #region runtime initializing

        internal void OnRuntimeInitialized()
        {
            lock (this.syncLock)
            {
                if (this.RuntimeMode)
                    return;

                foreach (RuleSet ruleSet in this)
                {
                    ruleSet.OnRuntimeInitialized();
                }
                RuntimeMode = true;
            }
        }

        internal bool RuntimeMode { get; set; }

        internal string GenerateRuleSetName()
        {
            string nameBase = Messages.NewRuleSetName;
            string newName;
            int i = 1;
            do
            {
                newName = nameBase + i.ToString(CultureInfo.InvariantCulture);
                i++;
            } while (this.Contains(newName));

            return newName;
        }

        #endregion
    }
    #endregion
}
