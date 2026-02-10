// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation All Rights Reserved
// ---------------------------------------------------------------------------

#define CODE_ANALYSIS
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules
{
    public class RulePathQualifier(string name, RulePathQualifier next)
    {
        private readonly string name = name;
        private readonly RulePathQualifier next = next;

        public string Name
        {
            get { return name; }
        }

        public RulePathQualifier Next
        {
            get { return next; }
        }
    }

    public class RuleAnalysis(RuleValidation validation, bool forWrites)
    {
        private readonly RuleValidation validation = validation;
        private readonly bool forWrites = forWrites;
        private readonly Dictionary<string, object> symbols = [];

        internal RuleValidation Validation
        {
            get { return validation; }
        }

        public bool ForWrites
        {
            get { return forWrites; }
        }

        public void AddSymbol(string symbol)
        {
            symbols[symbol] = null;
        }

        public ICollection<string> GetSymbols()
        {
            List<string> symbolList = new(symbols.Keys.Count);

            foreach (KeyValuePair<string, object> pair in symbols)
                symbolList.Add(pair.Key);

            return symbolList;
        }

        #region RuleAttribute Analysis

        internal void AnalyzeRuleAttributes(MemberInfo member, CodeExpression targetExpr, RulePathQualifier targetQualifier, CodeExpressionCollection argExprs, ParameterInfo[] parameters, List<CodeExpression> attributedExprs)
        {
            object[] attrs = member.GetCustomAttributes(typeof(RuleAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                RuleAttribute[] ruleAttrs = (RuleAttribute[])attrs;
                for (int i = 0; i < ruleAttrs.Length; ++i)
                    ruleAttrs[i].Analyze(this, member, targetExpr, targetQualifier, argExprs, parameters, attributedExprs);
            }
        }

        #endregion
    }
}
