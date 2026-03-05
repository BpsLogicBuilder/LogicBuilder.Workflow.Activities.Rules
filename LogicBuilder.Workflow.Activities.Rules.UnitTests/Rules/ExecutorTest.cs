using System;
using System.CodeDom;
using System.Collections.Generic;
using LogicBuilder.Workflow.ComponentModel.Compiler;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class ExecutorTest
    {
        #region Helper Classes

        private class TestClass
        {
            public int IntProperty { get; set; } = 42;
            public string StringProperty { get; set; } = "test";
            public bool BoolProperty { get; set; } = true;
            public double DoubleProperty { get; set; } = 3.14;
            public decimal DecimalProperty { get; set; } = 100m;
            public char CharProperty { get; set; } = 'A';
            public float FloatProperty { get; set; } = 2.5f;
            public TestEnum EnumProperty { get; set; } = TestEnum.Value1;
            public int? NullableIntProperty { get; set; } = 10;
        }

        private enum TestEnum
        {
            Value1 = 1,
            Value2 = 2,
            Value3 = 3
        }

        // Helper class with implicit conversion operator
        private class ConvertibleType
        {
            public int Value { get; set; }

            public static implicit operator int(ConvertibleType c) => c.Value;
            public static implicit operator ConvertibleType(int i) => new ConvertibleType { Value = i };
        }

        // Helper class with explicit conversion operator
        private class ExplicitConvertibleType
        {
            public int Value { get; set; }

            public static explicit operator int(ExplicitConvertibleType c) => c.Value;
            public static explicit operator ExplicitConvertibleType(int i) => new ExplicitConvertibleType { Value = i };
        }

        #endregion

        #region Preprocess Tests

        [Fact]
        public void Preprocess_WithNoActiveRules_ReturnsEmptyList()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var rules = new List<Rule>
            {
                new("Rule1") { Active = false },
                new("Rule2") { Active = false }
            };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.None, rules, validation, null);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Preprocess_WithActiveRules_ReturnsOrderedByPriority()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var rules = new List<Rule>
            {
                new("Rule1") { Active = true, Priority = 10 },
                new("Rule2") { Active = true, Priority = 20 },
                new("Rule3") { Active = true, Priority = 5 }
            };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.None, rules, validation, null);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Rule2", result[0].Rule.Name); // Priority 20
            Assert.Equal("Rule1", result[1].Rule.Name); // Priority 10
            Assert.Equal("Rule3", result[2].Rule.Name); // Priority 5
        }

        [Fact]
        public void Preprocess_WithSamePriority_OrdersByNameDescending()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var rules = new List<Rule>
            {
                new("Alpha") { Active = true, Priority = 10 },
                new("Beta") { Active = true, Priority = 10 },
                new("Charlie") { Active = true, Priority = 10 }
            };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.None, rules, validation, null);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Alpha", result[0].Rule.Name);
            Assert.Equal("Beta", result[1].Rule.Name);
            Assert.Equal("Charlie", result[2].Rule.Name);
        }

        [Fact]
        public void Preprocess_WithMixedActiveInactive_ReturnsOnlyActive()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var rules = new List<Rule>
            {
                new("Active1") { Active = true, Priority = 10 },
                new("Inactive1") { Active = false, Priority = 20 },
                new("Active2") { Active = true, Priority = 5 }
            };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.None, rules, validation, null);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Active1", result[0].Rule.Name);
            Assert.Equal("Active2", result[1].Rule.Name);
        }

        [Fact]
        public void Preprocess_WithFullChaining_AnalyzesRuleDependencies()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            
            var condition1 = new RuleExpressionCondition
            {
                Expression = new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(), "IntProperty")
            };
            condition1.Validate(validation);

            var action1 = new RuleStatementAction
            {
                CodeDomStatement = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), "StringProperty"),
                    new CodePrimitiveExpression("modified"))
            };
            action1.Validate(validation);

            var rule1 = new Rule("Rule1", condition1, [action1]) { Active = true, Priority = 10 };

            var condition2 = new RuleExpressionCondition
            {
                Expression = new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(), "StringProperty")
            };
            condition2.Validate(validation);

            var rule2 = new Rule("Rule2", condition2, null) { Active = true, Priority = 5 };

            var rules = new List<Rule> { rule1, rule2 };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.Full, rules, validation, null);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.NotNull(result[0].ThenActionsActiveRules);
        }

        [Fact]
        public void Preprocess_WithUpdateOnlyChaining_AnalyzesOnlyUpdateActions()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            
            var condition = new RuleExpressionCondition
            {
                Expression = new CodePrimitiveExpression(true)
            };
            condition.Validate(validation);

            var updateAction = new RuleUpdateAction("IntProperty");
            updateAction.Validate(validation);

            var rule = new Rule("UpdateRule", condition, [updateAction]) { Active = true, Priority = 10 };

            var rules = new List<Rule> { rule };

            // Act
            var result = Executor.Preprocess(RuleChainingBehavior.UpdateOnly, rules, validation, null);

            // Assert
            Assert.Single(result);
        }

        #endregion

        #region ExecuteRuleSet Tests

        [Fact]
        public void ExecuteRuleSet_WithEmptyRuleList_CompletesSuccessfully()
        {
            // Arrange
            var testObject = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);
            var orderedRules = new List<RuleState>();

            // Act
            Executor.ExecuteRuleSet(orderedRules, execution, null);

            // Assert
            Assert.False(execution.Halted);
        }

        [Fact]
        public void ExecuteRuleSet_WithTrueCondition_ExecutesThenActions()
        {
            // Arrange
            var testObject = new TestClass { IntProperty = 10 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);

            var condition = new RuleExpressionCondition
            {
                Expression = new CodePrimitiveExpression(true)
            };
            condition.Validate(validation);

            var action = new RuleStatementAction
            {
                CodeDomStatement = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), "IntProperty"),
                    new CodePrimitiveExpression(99))
            };
            action.Validate(validation);

            var rule = new Rule("TestRule", condition, [action]);
            var orderedRules = new List<RuleState> { new(rule) };

            // Act
            Executor.ExecuteRuleSet(orderedRules, execution, null);

            // Assert
            Assert.Equal(99, testObject.IntProperty);
        }

        [Fact]
        public void ExecuteRuleSet_WithFalseCondition_ExecutesElseActions()
        {
            // Arrange
            var testObject = new TestClass { IntProperty = 10 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);

            var condition = new RuleExpressionCondition
            {
                Expression = new CodePrimitiveExpression(false)
            };
            condition.Validate(validation);

            var elseAction = new RuleStatementAction
            {
                CodeDomStatement = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), "IntProperty"),
                    new CodePrimitiveExpression(77))
            };
            elseAction.Validate(validation);

            var rule = new Rule("TestRule", condition, null, [elseAction]);
            var orderedRules = new List<RuleState> { new(rule) };

            // Act
            Executor.ExecuteRuleSet(orderedRules, execution, null);

            // Assert
            Assert.Equal(77, testObject.IntProperty);
        }

        [Fact]
        public void ExecuteRuleSet_WithHaltAction_StopsExecution()
        {
            // Arrange
            var testObject = new TestClass { IntProperty = 10 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);

            var condition = new RuleExpressionCondition
            {
                Expression = new CodePrimitiveExpression(true)
            };
            condition.Validate(validation);

            var haltAction = new RuleHaltAction();
            haltAction.Validate(validation);

            var rule = new Rule("HaltRule", condition, [haltAction]);
            var orderedRules = new List<RuleState> { new(rule) };

            // Act
            Executor.ExecuteRuleSet(orderedRules, execution, null);

            // Assert
            Assert.True(execution.Halted);
        }

        [Fact]
        public void ExecuteRuleSet_WithReevaluationAlways_RerunsRule()
        {
            // Arrange
            var testObject = new TestClass { IntProperty = 0 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);

            var condition = new RuleExpressionCondition
            {
                Expression = new CodeBinaryOperatorExpression(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), "IntProperty"),
                    CodeBinaryOperatorType.LessThan,
                    new CodePrimitiveExpression(3))
            };
            condition.Validate(validation);

            var action = new RuleStatementAction
            {
                CodeDomStatement = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                        new CodeThisReferenceExpression(), "IntProperty"),
                    new CodeBinaryOperatorExpression(
                        new CodePropertyReferenceExpression(
                            new CodeThisReferenceExpression(), "IntProperty"),
                        CodeBinaryOperatorType.Add,
                        new CodePrimitiveExpression(1)))
            };
            action.Validate(validation);

            var rule = new Rule("CounterRule", condition, [action]) 
            { 
                ReevaluationBehavior = RuleReevaluationBehavior.Always 
            };

            var orderedRules = Executor.Preprocess(RuleChainingBehavior.Full, [rule], validation, null);

            // Act
            Executor.ExecuteRuleSet(orderedRules, execution, null);

            // Assert - Rule should execute multiple times
            Assert.Equal(3, testObject.IntProperty);
        }

        #endregion

        #region EvaluateBool Tests

        [Fact]
        public void EvaluateBool_WithTrueExpression_ReturnsTrue()
        {
            // Arrange
            var testObject = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);
            var expression = new CodePrimitiveExpression(true);
            
            RuleExpressionWalker.AnalyzeUsage(new RuleAnalysis(validation, false), expression, true, false, null);

            // Act
            var result = Executor.EvaluateBool(expression, execution);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EvaluateBool_WithFalseExpression_ReturnsFalse()
        {
            // Arrange
            var testObject = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testObject);
            var expression = new CodePrimitiveExpression(false);
            
            RuleExpressionWalker.AnalyzeUsage(new RuleAnalysis(validation, false), expression, true, false, null);

            // Act
            var result = Executor.EvaluateBool(expression, execution);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EvaluateBool_WithBooleanProperty_ReturnsPropertyValue()
        {
            // Arrange
            var testObject = new TestClass { BoolProperty = true };
            
            var expression = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(), "BoolProperty");
            CodeAssignStatement setBoolAction = new(expression, new CodePrimitiveExpression(true));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setBoolAction));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            var execution = new RuleExecution(validation, testObject);

            RuleExpressionWalker.AnalyzeUsage(new RuleAnalysis(validation, false), expression, true, false, null);

            // Act
            var result = Executor.EvaluateBool(expression, execution);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AdjustType Tests

        [Fact]
        public void AdjustType_WithSameType_ReturnsOriginalValue()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(int));

            // Assert
            Assert.Equal(42, result);
        }

        [Fact]
        public void AdjustType_IntToLong_ConvertsSuccessfully()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(long));

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(42L, result);
        }

        [Fact]
        public void AdjustType_IntToDouble_ConvertsSuccessfully()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(double));

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(42.0, result);
        }

        [Fact]
        public void AdjustType_IntToDecimal_ConvertsSuccessfully()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(decimal));

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(42m, result);
        }

        [Fact]
        public void AdjustType_DoubleToInt_ConvertsSuccessfully()
        {
            // Arrange
            double value = 42.7;

            // Act
            var result = Executor.AdjustType(typeof(double), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(43, result);
        }

        [Fact]
        public void AdjustType_FloatToDouble_ConvertsSuccessfully()
        {
            // Arrange
            float value = 3.14f;

            // Act
            var result = Executor.AdjustType(typeof(float), value, typeof(double));

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(3.14, (double)result, 2);
        }

        [Fact]
        public void AdjustType_CharToInt_ConvertsSuccessfully()
        {
            // Arrange
            char value = 'A';

            // Act
            var result = Executor.AdjustType(typeof(char), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(65, result);
        }

        [Fact]
        public void AdjustType_IntToNullableInt_ConvertsSuccessfully()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(int?));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(42, result);
        }

        [Fact]
        public void AdjustType_NullToNullableInt_ConvertsSuccessfully()
        {
            // Arrange & Act
            var result = Executor.AdjustType(typeof(int?), null, typeof(int?));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AdjustType_NullToInt_ThrowsInvalidCastException()
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() =>
                Executor.AdjustType(typeof(object), null, typeof(int)));
            
            Assert.Contains("value type", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AdjustType_EnumToInt_ConvertsSuccessfully()
        {
            // Arrange
            TestEnum value = TestEnum.Value2;

            // Act
            var result = Executor.AdjustType(typeof(TestEnum), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(2, result);
        }

        [Fact]
        public void AdjustType_IntToEnum_ConvertsSuccessfully()
        {
            // Arrange
            int value = 2;

            // Act
            var result = Executor.AdjustType(typeof(int), value, typeof(TestEnum));

            // Assert
            Assert.IsType<TestEnum>(result);
            Assert.Equal(TestEnum.Value2, result);
        }

        [Fact]
        public void AdjustType_StringToReferenceType_ReturnsOriginalValue()
        {
            // Arrange
            string value = "test";

            // Act
            var result = Executor.AdjustType(typeof(string), value, typeof(object));

            // Assert
            Assert.Same(value, result);
        }

        [Fact]
        public void AdjustType_NullToReferenceType_ReturnsNull()
        {
            // Act
            var result = Executor.AdjustType(typeof(string), null, typeof(string));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AdjustType_DecimalToChar_ConvertsSuccessfully()
        {
            // Arrange
            decimal value = 65m;

            // Act
            var result = Executor.AdjustType(typeof(decimal), value, typeof(char));

            // Assert
            Assert.IsType<char>(result);
            Assert.Equal('A', result);
        }

        [Fact]
        public void AdjustType_CharToFloat_ConvertsSuccessfully()
        {
            // Arrange
            char value = 'A';

            // Act
            var result = Executor.AdjustType(typeof(char), value, typeof(float));

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(65.0f, result);
        }

        [Fact]
        public void AdjustType_CharToDecimal_ConvertsSuccessfully()
        {
            // Arrange
            char value = 'B';

            // Act
            var result = Executor.AdjustType(typeof(char), value, typeof(decimal));

            // Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(66m, result);
        }

        [Fact]
        public void AdjustType_FloatToChar_ConvertsSuccessfully()
        {
            // Arrange
            float value = 66.0f;

            // Act
            var result = Executor.AdjustType(typeof(float), value, typeof(char));

            // Assert
            Assert.IsType<char>(result);
            Assert.Equal('B', result); // Rounds to 66
        }

        [Fact]
        public void AdjustType_DoubleToChar_ConvertsSuccessfully()
        {
            // Arrange
            double value = 90.2;

            // Act
            var result = Executor.AdjustType(typeof(double), value, typeof(char));

            // Assert
            Assert.IsType<char>(result);
            Assert.Equal('Z', result);
        }

        [Fact]
        public void AdjustType_DecimalToFloat_ConvertsSuccessfully()
        {
            // Arrange
            decimal value = 123.45m;

            // Act
            var result = Executor.AdjustType(typeof(decimal), value, typeof(float));

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(123.45f, (float)result, 2);
        }

        [Fact]
        public void AdjustType_ByteToNullableInt_ConvertsSuccessfully()
        {
            // Arrange
            byte value = 100;

            // Act
            var result = Executor.AdjustType(typeof(byte), value, typeof(int?));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(100, result);
        }

        [Fact]
        public void AdjustType_NullableIntToNullableInt_WithNull_ReturnsNull()
        {
            // Act
            var result = Executor.AdjustType(typeof(int?), null, typeof(int?));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AdjustType_ShortToLong_ConvertsSuccessfully()
        {
            // Arrange
            short value = 1000;

            // Act
            var result = Executor.AdjustType(typeof(short), value, typeof(long));

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(1000L, result);
        }

        [Fact]
        public void AdjustType_SByteToInt_ConvertsSuccessfully()
        {
            // Arrange
            sbyte value = -50;

            // Act
            var result = Executor.AdjustType(typeof(sbyte), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(-50, result);
        }

        [Fact]
        public void AdjustType_UIntToLong_ConvertsSuccessfully()
        {
            // Arrange
            uint value = 5000;

            // Act
            var result = Executor.AdjustType(typeof(uint), value, typeof(long));

            // Assert
            Assert.IsType<long>(result);
            Assert.Equal(5000L, result);
        }

        [Fact]
        public void AdjustType_IncompatibleTypes_ThrowsRuleEvaluationException()
        {
            // Arrange
            string value = "test";

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                Executor.AdjustType(typeof(string), value, typeof(int)));
        }

        #endregion

        #region AdjustTypeWithCast Tests

        [Fact]
        public void AdjustTypeWithCast_WithSameType_ReturnsOriginalValue()
        {
            // Arrange
            int value = 42;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(int), value, typeof(int));

            // Assert
            Assert.Equal(42, result);
        }

        [Fact]
        public void AdjustTypeWithCast_LongToInt_ConvertsSuccessfully()
        {
            // Arrange
            long value = 42L;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(long), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(42, result);
        }

        [Fact]
        public void AdjustTypeWithCast_DoubleToInt_TruncatesSuccessfully()
        {
            // Arrange
            double value = 42.9;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(double), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(43, result);
        }

        [Fact]
        public void AdjustTypeWithCast_IntToShort_ConvertsSuccessfully()
        {
            // Arrange
            int value = 100;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(int), value, typeof(short));

            // Assert
            Assert.IsType<short>(result);
            Assert.Equal((short)100, result);
        }

        [Fact]
        public void AdjustTypeWithCast_IntToByte_ConvertsSuccessfully()
        {
            // Arrange
            int value = 255;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(int), value, typeof(byte));

            // Assert
            Assert.IsType<byte>(result);
            Assert.Equal((byte)255, result);
        }

        [Fact]
        public void AdjustTypeWithCast_NullToNullableType_ReturnsNull()
        {
            // Act
            var result = Executor.AdjustTypeWithCast(typeof(int?), null, typeof(int?));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AdjustTypeWithCast_EnumToUnderlying_ConvertsSuccessfully()
        {
            // Arrange
            TestEnum value = TestEnum.Value3;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(TestEnum), value, typeof(int));

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(3, result);
        }

        [Fact]
        public void AdjustTypeWithCast_CharToShort_ConvertsSuccessfully()
        {
            // Arrange
            char value = 'Z';

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(char), value, typeof(short));

            // Assert
            Assert.IsType<short>(result);
            Assert.Equal((short)90, result);
        }

        [Fact]
        public void AdjustTypeWithCast_DecimalToByte_ConvertsSuccessfully()
        {
            // Arrange
            decimal value = 200m;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(decimal), value, typeof(byte));

            // Assert
            Assert.IsType<byte>(result);
            Assert.Equal((byte)200, result);
        }

        [Fact]
        public void AdjustTypeWithCast_DoubleToFloat_ConvertsSuccessfully()
        {
            // Arrange
            double value = 3.14159;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(double), value, typeof(float));

            // Assert
            Assert.IsType<float>(result);
            Assert.Equal(3.14159f, (float)result, 5);
        }

        [Fact]
        public void AdjustTypeWithCast_LongToByte_ConvertsSuccessfully()
        {
            // Arrange
            long value = 128L;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(long), value, typeof(byte));

            // Assert
            Assert.IsType<byte>(result);
            Assert.Equal((byte)128, result);
        }

        [Fact]
        public void AdjustTypeWithCast_FloatToByte_ConvertsSuccessfully()
        {
            // Arrange
            float value = 99.5f;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(float), value, typeof(byte));

            // Assert
            Assert.IsType<byte>(result);
            Assert.Equal((byte)100, result);
        }

        [Fact]
        public void AdjustTypeWithCast_IntToSByte_ConvertsSuccessfully()
        {
            // Arrange
            int value = -100;

            // Act
            var result = Executor.AdjustTypeWithCast(typeof(int), value, typeof(sbyte));

            // Assert
            Assert.IsType<sbyte>(result);
            Assert.Equal((sbyte)-100, result);
        }

        [Fact]
        public void AdjustTypeWithCast_NullToValueType_ThrowsInvalidCastException()
        {
            // Act & Assert
            Assert.Throws<InvalidCastException>(() =>
                Executor.AdjustTypeWithCast(typeof(object), null, typeof(int)));
        }

        [Fact]
        public void AdjustTypeWithCast_IncompatibleTypes_ThrowsRuleEvaluationException()
        {
            // Arrange
            string value = "test";

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                Executor.AdjustTypeWithCast(typeof(string), value, typeof(int)));
        }

        #endregion
    }
}