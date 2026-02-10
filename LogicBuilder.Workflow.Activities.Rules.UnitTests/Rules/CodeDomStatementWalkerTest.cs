using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class CodeDomStatementWalkerTest
    {
        #region Validate Tests

        [Fact]
        public void Validate_WithCodeExpressionStatement_ReturnsTrue()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var statement = new CodeExpressionStatement(methodInvoke);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = CodeDomStatementWalker.Validate(validation, statement);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithCodeAssignStatement_ReturnsTrue()
        {
            // Arrange
            var target = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(100));
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = CodeDomStatementWalker.Validate(validation, statement);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithNullExpression_ReturnsFalse()
        {
            // Arrange
            var statement = new CodeExpressionStatement();
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = CodeDomStatementWalker.Validate(validation, statement);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithInvalidExpressionType_ReturnsFalse()
        {
            // Arrange
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = CodeDomStatementWalker.Validate(validation, statement);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void Validate_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var statement = new CodeIterationStatement();
            var validation = CreateMockValidation(typeof(TestClass));

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.Validate(validation, statement));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        [Fact]
        public void Validate_WithFieldAssignment_ReturnsTrue()
        {
            // Arrange
            var target = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(50));
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = CodeDomStatementWalker.Validate(validation, statement);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Execute Tests

        [Fact]
        public void Execute_WithExpressionStatement_ExecutesMethodInvoke()
        {
            // Arrange
            var testInstance = new TestClass();
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "IncrementCounter"
            );
            var statement = new CodeExpressionStatement(methodInvoke);
            var execution = CreateMockExecution(testInstance, statement);

            // Act
            CodeDomStatementWalker.Execute(execution, statement);

            // Assert
            Assert.Equal(1, testInstance.Counter);
        }

        [Fact]
        public void Execute_WithAssignStatement_AssignsValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var target = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(123));
            var execution = CreateMockExecution(testInstance, statement);

            // Act
            CodeDomStatementWalker.Execute(execution, statement);

            // Assert
            Assert.Equal(123, testInstance.IntProperty);
        }

        [Fact]
        public void Execute_WithFieldAssignment_AssignsValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var target = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(999));
            var execution = CreateMockExecution(testInstance, statement);

            // Act
            CodeDomStatementWalker.Execute(execution, statement);

            // Assert
            Assert.Equal(999, testInstance.intField);
        }

        [Fact]
        public void Execute_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var testInstance = new TestClass();
            var statement = new CodeIterationStatement();
            var validation = CreateMockValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.Execute(execution, statement));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithExpressionStatement_CompletesSuccessfully()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var statement = new CodeExpressionStatement(methodInvoke);
            var analysis = CreateMockAnalysis(typeof(TestClass), statement);

            // Act & Assert - Should not throw
            CodeDomStatementWalker.AnalyzeUsage(analysis, statement);
        }

        [Fact]
        public void AnalyzeUsage_WithAssignStatement_CompletesSuccessfully()
        {
            // Arrange
            var target = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(100));
            var analysis = CreateMockAnalysis(typeof(TestClass), statement);

            // Act & Assert - Should not throw
            CodeDomStatementWalker.AnalyzeUsage(analysis, statement);
        }

        [Fact]
        public void AnalyzeUsage_WithFieldAssignment_CompletesSuccessfully()
        {
            // Arrange
            var target = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(50));
            var analysis = CreateMockAnalysis(typeof(TestClass), statement);

            // Act & Assert - Should not throw
            CodeDomStatementWalker.AnalyzeUsage(analysis, statement);
        }

        [Fact]
        public void AnalyzeUsage_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var statement = new CodeIterationStatement();
            var validation = CreateMockValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, true);

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.AnalyzeUsage(analysis, statement));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithExpressionStatement_AppendsMethodCall()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var statement = new CodeExpressionStatement(methodInvoke);
            var sb = new StringBuilder();

            // Act
            CodeDomStatementWalker.Decompile(sb, statement);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("TestMethod", result);
        }

        [Fact]
        public void Decompile_WithAssignStatement_AppendsAssignment()
        {
            // Arrange
            var target = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(42));
            var sb = new StringBuilder();

            // Act
            CodeDomStatementWalker.Decompile(sb, statement);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("IntProperty", result);
            Assert.Contains("42", result);
        }

        [Fact]
        public void Decompile_WithFieldAssignment_AppendsFieldAssignment()
        {
            // Arrange
            var target = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(100));
            var sb = new StringBuilder();

            // Act
            CodeDomStatementWalker.Decompile(sb, statement);

            // Assert
            var result = sb.ToString();
            Assert.Contains("this", result);
            Assert.Contains("intField", result);
            Assert.Contains("100", result);
        }

        [Fact]
        public void Decompile_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var statement = new CodeIterationStatement();
            var sb = new StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.Decompile(sb, statement));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithBothNull_ReturnsTrue()
        {
            // Act
            var result = CodeDomStatementWalker.Match(null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithFirstNull_ReturnsFalse()
        {
            // Arrange
            var statement = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );

            // Act
            var result = CodeDomStatementWalker.Match(null, statement);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSecondNull_ReturnsFalse()
        {
            // Arrange
            var statement = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );

            // Act
            var result = CodeDomStatementWalker.Match(statement, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentStatementTypes_ReturnsFalse()
        {
            // Arrange
            var statement1 = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );
            var statement2 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );

            // Act
            var result = CodeDomStatementWalker.Match(statement1, statement2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameExpressionStatement_ReturnsTrue()
        {
            // Arrange
            var statement1 = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );
            var statement2 = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );

            // Act
            var result = CodeDomStatementWalker.Match(statement1, statement2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentExpressionStatements_ReturnsFalse()
        {
            // Arrange
            var statement1 = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "TestMethod"
                )
            );
            var statement2 = new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeThisReferenceExpression(),
                    "OtherMethod"
                )
            );

            // Act
            var result = CodeDomStatementWalker.Match(statement1, statement2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithSameAssignStatement_ReturnsTrue()
        {
            // Arrange
            var target1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement1 = new CodeAssignStatement(target1, new CodePrimitiveExpression(100));

            var target2 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement2 = new CodeAssignStatement(target2, new CodePrimitiveExpression(100));

            // Act
            var result = CodeDomStatementWalker.Match(statement1, statement2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentAssignStatements_ReturnsFalse()
        {
            // Arrange
            var target1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement1 = new CodeAssignStatement(target1, new CodePrimitiveExpression(100));

            var target2 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement2 = new CodeAssignStatement(target2, new CodePrimitiveExpression(200));

            // Act
            var result = CodeDomStatementWalker.Match(statement1, statement2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var statement1 = new CodeIterationStatement();
            var statement2 = new CodeIterationStatement();

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.Match(statement1, statement2));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithNull_ReturnsNull()
        {
            // Act
            var result = CodeDomStatementWalker.Clone(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Clone_WithExpressionStatement_ReturnsNewInstance()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var statement = new CodeExpressionStatement(methodInvoke);

            // Act
            var result = CodeDomStatementWalker.Clone(statement);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeExpressionStatement>(result);
            Assert.NotSame(statement, result);

            var clonedStatement = (CodeExpressionStatement)result;
            Assert.NotNull(clonedStatement.Expression);
            Assert.IsType<CodeMethodInvokeExpression>(clonedStatement.Expression);
            Assert.NotSame(methodInvoke, clonedStatement.Expression);
        }

        [Fact]
        public void Clone_WithAssignStatement_ReturnsNewInstance()
        {
            // Arrange
            var target = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntProperty"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(42));

            // Act
            var result = CodeDomStatementWalker.Clone(statement);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeAssignStatement>(result);
            Assert.NotSame(statement, result);

            var clonedStatement = (CodeAssignStatement)result;
            Assert.NotNull(clonedStatement.Left);
            Assert.NotNull(clonedStatement.Right);
            Assert.NotSame(target, clonedStatement.Left);
        }

        [Fact]
        public void Clone_WithFieldAssignment_ReturnsNewInstance()
        {
            // Arrange
            var target = new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                "intField"
            );
            var statement = new CodeAssignStatement(target, new CodePrimitiveExpression(99));

            // Act
            var result = CodeDomStatementWalker.Clone(statement);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CodeAssignStatement>(result);
            Assert.NotSame(statement, result);

            var clonedStatement = (CodeAssignStatement)result;
            var clonedTarget = clonedStatement.Left as CodeFieldReferenceExpression;
            Assert.NotNull(clonedTarget);
            Assert.Equal("intField", clonedTarget.FieldName);
        }

        [Fact]
        public void Clone_WithUnsupportedStatementType_ThrowsNotSupportedException()
        {
            // Arrange
            var statement = new CodeIterationStatement();

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                CodeDomStatementWalker.Clone(statement));
            Assert.Contains("CodeIterationStatement", exception.Message);
        }

        #endregion

        #region Helper Methods

        private static RuleValidation CreateMockValidation(Type type)
        {
            return new RuleValidation(type);
        }

        private static RuleExecution CreateMockExecution(object instance)
        {
            var validation = CreateMockValidation(instance.GetType());
            return new RuleExecution(validation, instance);
        }

        private static RuleAnalysis CreateMockAnalysis(Type type)
        {
            var validation = CreateMockValidation(type);
            return new RuleAnalysis(validation, true);
        }

        private static RuleAnalysis CreateMockAnalysis(Type type, CodeStatement statement)
        {
            var validation = CreateMockValidation(type, statement);
            return new RuleAnalysis(validation, true);
        }

        private static RuleValidation CreateMockValidation(Type type, CodeStatement statement)
        {
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = new CodePrimitiveExpression(1),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(1)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(statement));
            ruleSet.Rules.Add(rule);
            var validation = CreateMockValidation(type);
            ruleSet.Validate(validation);
            return validation;
        }

        private static RuleExecution CreateMockExecution(object instance, CodeStatement statement)
        {
            Type type = instance.GetType();
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = new CodePrimitiveExpression(1),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(1)
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(new RuleStatementAction(statement));
            ruleSet.Rules.Add(rule);
            var validation = CreateMockValidation(type);
            ruleSet.Validate(validation);

            return new RuleExecution(validation, instance);
        }

        #endregion

        #region Test Helper Classes

        public class TestClass
        {
            public int intField;
            public string? stringField;

            public int IntProperty { get; set; }
            public object? ObjectProperty { get; set; }
            public string? StringProperty { get; set; }
            public int Counter { get; set; }

#pragma warning disable CA1822 // Mark members as static
            public void TestMethod() { }

            public void IncrementCounter()
            {
                Counter++;
            }

            public int GetValue() => 42;
#pragma warning restore CA1822 // Mark members as static
        }

        #endregion
    }
}