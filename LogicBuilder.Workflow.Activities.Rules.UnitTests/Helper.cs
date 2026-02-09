using LogicBuilder.Workflow.Activities.Rules.UnitTests.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests
{
    internal static class Helper
    {
        internal static RuleValidation GetValidation(RuleSet ruleSet, Type type)
        {
            if (ruleSet == null)
                throw new InvalidOperationException(Resources.ruleSetCannotBeNull);

            RuleValidation ruleValidation = new(type);

            if (!ruleSet.Validate(ruleValidation))
            {
                throw new InvalidOperationException
                (
                    string.Join
                    (
                        Environment.NewLine,
                        ruleValidation.Errors.Aggregate
                        (
                            new List<string>
                            {
                                string.Format(CultureInfo.CurrentCulture, Resources.invalidRuleSetFormat, ruleSet.Name)
                            },
                            (list, next) =>
                            {
                                list.Add(next.ErrorText);
                                return list;
                            }
                        )
                    )
                );
            }

            return ruleValidation;
        }
    }
}
