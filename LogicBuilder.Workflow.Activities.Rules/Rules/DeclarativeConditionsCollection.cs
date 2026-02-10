// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation - All Rights Reserved
// ---------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region RuleConditionCollection Class
    [Serializable]
    public sealed class RuleConditionCollection : KeyedCollection<string, RuleCondition>//, IWorkflowChangeDiff
    {
        private bool _runtimeInitialized;
        [NonSerialized]
        private readonly object _runtimeInitializationLock = new();

        public RuleConditionCollection()
        {
        }

        protected override string GetKeyForItem(RuleCondition item)
        {
            return item.Name;
        }

        /// <summary>
        /// Mark the DeclarativeConditionDefinitionCollection as Runtime Initialized to prevent direct runtime updates.
        /// </summary>
        internal void OnRuntimeInitialized()
        {
            lock (_runtimeInitializationLock)
            {
                if (_runtimeInitialized)
                    return;

                foreach (RuleCondition condition in this)
                {
                    condition.OnRuntimeInitialized();
                }
                _runtimeInitialized = true;
            }
        }

        protected override void InsertItem(int index, RuleCondition item)
        {
            if (this._runtimeInitialized)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            if (item.Name != null && item.Name.Length >= 0 && this.Contains(item.Name))
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.ConditionExists, item.Name);
                throw new ArgumentException(message);
            }

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (this._runtimeInitialized)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, RuleCondition item)
        {
            if (this._runtimeInitialized)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            base.SetItem(index, item);
        }

        internal bool RuntimeMode
        {
            set { this._runtimeInitialized = value; }
            get { return this._runtimeInitialized; }
        }

        new public void Add(RuleCondition item)
        {
            if (this._runtimeInitialized)
                throw new InvalidOperationException(SR.GetString(SR.Error_CanNotChangeAtRuntime));

            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            if (null == item.Name)
            {
                string message = string.Format(CultureInfo.CurrentCulture, Messages.InvalidConditionName, "item.Name");
                throw new ArgumentException(message);
            }

            base.Add(item);
        }
    }
    #endregion
}

