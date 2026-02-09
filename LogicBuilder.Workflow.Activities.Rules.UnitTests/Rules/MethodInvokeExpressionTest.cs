using LogicBuilder.Workflow.Activities.Rules;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;
using System.CodeDom;
using System.Globalization;
using System.Linq.Expressions;
using Xunit;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class MethodInvokeExpressionTest
    {
        #region Validation Tests

        [Fact]
        public void Validate_ValidInstanceMethod_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_ValidStaticMethod_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)),
                "StaticMethod",
                new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_MethodWithParameters_ValidatesSuccessfully()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_MethodWithStringParameter_ValidatesSuccessfully()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetLength",
                new CodePrimitiveExpression("test"));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(int), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        [Fact]
        public void Validate_IsWritten_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_InvalidAssignTarget, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_NullMethodTarget_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression
            {
                Method = new CodeMethodReferenceExpression(null, "GetValue")
            };

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_NullMethod_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression();

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_ParameterNotSet, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_GenericMethod_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodRef = new CodeMethodReferenceExpression(
                new CodeThisReferenceExpression(),
                "GenericMethod");
            methodRef.TypeArguments.Add(new CodeTypeReference(typeof(int)));
            var methodInvoke = new CodeMethodInvokeExpression(methodRef);

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_CodeExpressionNotHandled, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_TypeReferenceExpressionAsArgument_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodeTypeReferenceExpression(typeof(int)));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Equal(Common.ErrorNumbers.Error_CodeExpressionNotHandled, validation.Errors[0].ErrorNumber);
        }

        [Fact]
        public void Validate_NonExistentMethod_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "NonExistentMethod");

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_WrongNumberOfParameters_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_WrongParameterType_AddsError()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression("invalid"),
                new CodePrimitiveExpression(3));

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_VoidMethod_ReturnsVoidType()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "VoidMethod");

            // Act
            var result = RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(void), result.ExpressionType);
            Assert.Empty(validation.Errors);
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_SimpleInstanceMethod_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass { Value = 42 };
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Value);
        }

        [Fact]
        public void Evaluate_MethodWithParameters_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(8, result.Value);
        }

        [Fact]
        public void Evaluate_StaticMethod_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)),
                "StaticMethod",
                new CodePrimitiveExpression(10));
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(20, result.Value);
        }

        [Fact]
        public void Evaluate_MethodWithStringParameter_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetLength",
                new CodePrimitiveExpression("hello"));
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Value);
        }

        [Fact]
        public void Evaluate_MethodWithRefParameter_UpdatesParameter()
        {
            // Arrange
            var testInstance = new TestClass();
            
            var valueField = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "Value");
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "DoubleValue",
                new CodeDirectionExpression(FieldDirection.Ref, valueField));
            testInstance.Value = 5;

            CodeAssignStatement setIntAction = new(valueField, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            rule.ThenActions.Add(new RuleStatementAction(methodInvoke));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            ruleSet.Validate(validation);

            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            _ = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.Equal(10, testInstance.Value);
        }

        [Fact]
        public void Evaluate_MethodWithOutParameter_SetsParameter()
        {
            // Arrange
            var testInstance = new TestClass();
            var valueField = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "Value");
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TryGetValue",
                new CodeDirectionExpression(FieldDirection.Out, valueField));

            CodeAssignStatement setIntAction = new(valueField, new CodePrimitiveExpression(999));

            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(setIntAction));
            rule.ThenActions.Add(new RuleStatementAction(methodInvoke));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            var execution = new RuleExecution(validation, testInstance);
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.True((bool)result.Value);
            Assert.Equal(100, testInstance.Value);
        }

        [Fact]
        public void Evaluate_NullTarget_ThrowsException()
        {
            // Arrange
            var testInstance = new TestClass { NullChild = null };
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "NullChild"),
                "GetValue");

            CodeBinaryOperatorExpression equalityTest = new()
            {
                Left = methodInvoke,
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(null)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(equalityTest) };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            var execution = new RuleExecution(validation, testInstance);
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                RuleExpressionWalker.Evaluate(execution, methodInvoke).Value);
            Assert.Contains("null", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Evaluate_NotValidated_ThrowsException()
        {
            // Arrange
            var testInstance = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            // Don't validate

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                RuleExpressionWalker.Evaluate(execution, methodInvoke));
            Assert.Contains("not validated", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Evaluate_VoidMethod_ReturnsNull()
        {
            // Arrange
            var testInstance = new TestClass();
            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "VoidMethod");
            RuleExpressionWalker.Validate(validation, methodInvoke, false);

            // Act
            var result = RuleExpressionWalker.Evaluate(execution, methodInvoke);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_SimpleMethod_GeneratesCorrectString()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, methodInvoke, null);

            // Assert
            Assert.Equal("this.GetValue()", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_MethodWithParameters_GeneratesCorrectString()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, methodInvoke, null);

            // Assert
            Assert.Equal("this.Add(5, 3)", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_StaticMethod_GeneratesCorrectString()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)),
                "StaticMethod",
                new CodePrimitiveExpression(10));
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, methodInvoke, null);

            // Assert
            var expected = $"{typeof(TestClass).FullName!.Replace('+', '.')}.StaticMethod(10)";
            Assert.Equal(expected, stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_MethodWithStringParameter_GeneratesCorrectString()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetLength",
                new CodePrimitiveExpression("test"));
            var stringBuilder = new System.Text.StringBuilder();

            // Act
            RuleExpressionWalker.Decompile(stringBuilder, methodInvoke, null);

            // Assert
            Assert.Equal("this.GetLength(\"test\")", stringBuilder.ToString());
        }

        [Fact]
        public void Decompile_NullMethodTarget_ThrowsException()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression
            {
                Method = new CodeMethodReferenceExpression(null, "Test")
            };
            var stringBuilder = new System.Text.StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<RuleEvaluationException>(() =>
                RuleExpressionWalker.Decompile(stringBuilder, methodInvoke, null));
            Assert.Contains("null", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_SimpleMethod_CreatesIndependentCopy()
        {
            // Arrange
            var original = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");

            // Act
            var clone = RuleExpressionWalker.Clone(original);

            // Assert
            Assert.NotSame(original, clone);
            Assert.IsType<CodeMethodInvokeExpression>(clone);
            var clonedMethod = (CodeMethodInvokeExpression)clone;
            Assert.Equal("GetValue", clonedMethod.Method.MethodName);
        }

        [Fact]
        public void Clone_MethodWithParameters_CreatesIndependentCopy()
        {
            // Arrange
            var original = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));

            // Act
            var clone = RuleExpressionWalker.Clone(original);

            // Assert
            Assert.NotSame(original, clone);
            var clonedMethod = (CodeMethodInvokeExpression)clone;
            Assert.Equal(2, clonedMethod.Parameters.Count);
            Assert.NotSame(original.Parameters[0], clonedMethod.Parameters[0]);
            Assert.NotSame(original.Parameters[1], clonedMethod.Parameters[1]);
        }

        [Fact]
        public void Clone_ModifyingClone_DoesNotAffectOriginal()
        {
            // Arrange
            var original = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");

            // Act
            var clone = (CodeMethodInvokeExpression)RuleExpressionWalker.Clone(original);
            clone.Method.MethodName = "Different";

            // Assert
            Assert.Equal("GetValue", original.Method.MethodName);
            Assert.Equal("Different", clone.Method.MethodName);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_IdenticalMethods_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            var expr2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_MethodsWithSameParameterValues_ReturnsTrue()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));
            var expr2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_DifferentMethodNames_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            var expr2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "SetValue");

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_DifferentTargets_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetValue");
            var expr2 = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(TestClass)),
                "GetValue");

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_DifferentParameterCount_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5));
            var expr2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_DifferentParameterValues_ReturnsFalse()
        {
            // Arrange
            var expr1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(3));
            var expr2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "Add",
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(7));

            // Act
            var result = RuleExpressionWalker.Match(expr1, expr2);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Helper Classes

        public class TestClass
        {
            public int Value { get; set; }
            public TestClass? NullChild { get; set; }

            public int GetValue()
            {
                return Value;
            }
#pragma warning disable CA1822
            public int Add(int a, int b)
            {
                return a + b;
            }

            public int GetLength(string s)
            {
                return s?.Length ?? 0;
            }

            public static int StaticMethod(int x)
            {
                return x * 2;
            }

            public void VoidMethod()
            {
                // Do nothing
                if (Value != int.MaxValue)
                {
                    Value += 1;
                }
            }

            public void DoubleValue(ref int value)
            {
                if (Value != int.MaxValue)
                    value *= 2;
                else
                    value = 0;
            }

            public bool TryGetValue(out int value)
            {
                value = 100;
                return true;
            }
#pragma warning restore CA1822
        }

        #endregion
    }
}