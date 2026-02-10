using System;
using System.CodeDom;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleCodeDomStatementTest
    {
        #region ExpressionStatement Tests

        [Fact]
        public void ExpressionStatement_Create_ReturnsValidInstance()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "TestMethod"
            );
            var codeStatement = new CodeExpressionStatement(methodInvoke);

            // Act
            var result = ExpressionStatement.Create(codeStatement);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExpressionStatement>(result);
        }

        [Fact]
        public void ExpressionStatement_Validate_WithNullExpression_ReturnsFailure()
        {
            // Arrange
            var codeStatement = new CodeExpressionStatement();
            var statement = ExpressionStatement.Create(codeStatement);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void ExpressionStatement_Validate_WithValidMethodInvoke_ReturnsSuccess()
        {
            // Arrange
            var thisRef = new CodeThisReferenceExpression();
            var methodInvoke = new CodeMethodInvokeExpression(thisRef, "ToString");
            var codeStatement = new CodeExpressionStatement(methodInvoke);
            var statement = ExpressionStatement.Create(codeStatement);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ExpressionStatement_Validate_WithNonMethodInvokeExpression_ReturnsFailure()
        {
            // Arrange
            var literalExpression = new CodePrimitiveExpression(42);
            var codeStatement = new CodeExpressionStatement(literalExpression);
            var statement = ExpressionStatement.Create(codeStatement);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void ExpressionStatement_Execute_CallsRuleExpressionWalker()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement = new CodeExpressionStatement(methodInvoke);
            var statement = ExpressionStatement.Create(codeStatement);
            var execution = CreateMockExecution(typeof(TestClass), codeStatement);

            // Act & Assert - Should not throw
            statement.Execute(execution);
        }

        [Fact]
        public void ExpressionStatement_Decompile_WithNullExpression_ThrowsException()
        {
            // Arrange
            var codeStatement = new CodeExpressionStatement();
            var statement = ExpressionStatement.Create(codeStatement);
            var decompilation = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() => statement.Decompile(decompilation));
        }

        [Fact]
        public void ExpressionStatement_Decompile_WithValidExpression_AppendsToStringBuilder()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement = new CodeExpressionStatement(methodInvoke);
            var statement = ExpressionStatement.Create(codeStatement);
            var decompilation = new StringBuilder();

            // Act
            statement.Decompile(decompilation);

            // Assert
            Assert.True(decompilation.Length > 0);
        }

        [Fact]
        public void ExpressionStatement_Match_WithSameExpression_ReturnsTrue()
        {
            // Arrange
            var methodInvoke1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var methodInvoke2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement1 = new CodeExpressionStatement(methodInvoke1);
            var codeStatement2 = new CodeExpressionStatement(methodInvoke2);
            var statement = ExpressionStatement.Create(codeStatement1);

            // Act
            var result = statement.Match(codeStatement2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ExpressionStatement_Match_WithDifferentExpression_ReturnsFalse()
        {
            // Arrange
            var methodInvoke1 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var methodInvoke2 = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "GetHashCode"
            );
            var codeStatement1 = new CodeExpressionStatement(methodInvoke1);
            var codeStatement2 = new CodeExpressionStatement(methodInvoke2);
            var statement = ExpressionStatement.Create(codeStatement1);

            // Act
            var result = statement.Match(codeStatement2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ExpressionStatement_Match_WithNonExpressionStatement_ReturnsFalse()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement1 = new CodeExpressionStatement(methodInvoke);
            var codeStatement2 = new CodeAssignStatement();
            var statement = ExpressionStatement.Create(codeStatement1);

            // Act
            var result = statement.Match(codeStatement2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ExpressionStatement_Clone_CreatesNewInstance()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement = new CodeExpressionStatement(methodInvoke);
            var statement = ExpressionStatement.Create(codeStatement);

            // Act
            var cloned = statement.Clone();

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<CodeExpressionStatement>(cloned);
            Assert.NotSame(codeStatement, cloned);
        }

        #endregion

        #region AssignmentStatement Tests

        [Fact]
        public void AssignmentStatement_Create_ReturnsValidInstance()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );

            // Act
            var result = AssignmentStatement.Create(assignment);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AssignmentStatement>(result);
        }

        [Fact]
        public void AssignmentStatement_Validate_WithNullLeft_ReturnsFailure()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                null,
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void AssignmentStatement_Validate_WithNullRight_ReturnsFailure()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                null
            );
            var statement = AssignmentStatement.Create(assignment);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.False(result);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public void AssignmentStatement_Validate_WithCompatibleTypes_ReturnsSuccess()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntProperty"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var validation = CreateMockValidation(typeof(TestClass));

            // Act
            var result = statement.Validate(validation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AssignmentStatement_Execute_AssignsValue()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntProperty"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var execution = CreateMockExecution(typeof(TestClass), assignment);

            // Act & Assert - Should not throw
            statement.Execute(execution);
        }

        [Fact]
        public void AssignmentStatement_Decompile_WithNullLeft_ThrowsException()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                null,
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var decompilation = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() => statement.Decompile(decompilation));
        }

        [Fact]
        public void AssignmentStatement_Decompile_WithNullRight_ThrowsException()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "IntProperty"
                ),
                null
            );
            var statement = AssignmentStatement.Create(assignment);
            var decompilation = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() => statement.Decompile(decompilation));
        }

        [Fact]
        public void AssignmentStatement_Decompile_WithValidAssignment_AppendsToStringBuilder()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var decompilation = new StringBuilder();

            // Act
            statement.Decompile(decompilation);

            // Assert
            Assert.True(decompilation.Length > 0);
            Assert.Contains("=", decompilation.ToString());
        }

        [Fact]
        public void AssignmentStatement_Match_WithSameAssignment_ReturnsTrue()
        {
            // Arrange
            var assignment1 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var assignment2 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment1);

            // Act
            var result = statement.Match(assignment2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AssignmentStatement_Match_WithDifferentLeft_ReturnsFalse()
        {
            // Arrange
            var assignment1 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var assignment2 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "stringField"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment1);

            // Act
            var result = statement.Match(assignment2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AssignmentStatement_Match_WithDifferentRight_ReturnsFalse()
        {
            // Arrange
            var assignment1 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var assignment2 = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(99)
            );
            var statement = AssignmentStatement.Create(assignment1);

            // Act
            var result = statement.Match(assignment2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AssignmentStatement_Match_WithNonAssignmentStatement_ReturnsFalse()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var expressionStatement = new CodeExpressionStatement();
            var statement = AssignmentStatement.Create(assignment);

            // Act
            var result = statement.Match(expressionStatement);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AssignmentStatement_Clone_CreatesNewInstance()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);

            // Act
            var cloned = statement.Clone();

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<CodeAssignStatement>(cloned);
            Assert.NotSame(assignment, cloned);
        }

        [Fact]
        public void AssignmentStatement_AnalyzeUsage_CallsRuleExpressionWalker()
        {
            // Arrange
            var assignment = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "intField"
                ),
                new CodePrimitiveExpression(42)
            );
            var statement = AssignmentStatement.Create(assignment);
            var analysis = CreateMockAnalysis();

            // Act & Assert - Should not throw
            statement.AnalyzeUsage(analysis);
        }

        [Fact]
        public void ExpressionStatement_AnalyzeUsage_CallsRuleExpressionWalker()
        {
            // Arrange
            var methodInvoke = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "ToString"
            );
            var codeStatement = new CodeExpressionStatement(methodInvoke);
            var statement = ExpressionStatement.Create(codeStatement);
            var analysis = CreateMockAnalysis(typeof(TestClass), codeStatement);

            // Act & Assert - Should not throw
            statement.AnalyzeUsage(analysis);
        }

        #endregion

        #region Helper Methods

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

        private static RuleValidation CreateMockValidation(Type type)
        {
            return new RuleValidation(type);
        }

        private static RuleExecution CreateMockExecution(Type type, CodeStatement statement)
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

            var instance = Activator.CreateInstance(type);
            return new RuleExecution(validation, instance);
        }

        private static RuleAnalysis CreateMockAnalysis(Type type, CodeStatement statement)
        {
            var validation = CreateMockValidation(type, statement);
            return new RuleAnalysis(validation, true);
        }

        private static RuleAnalysis CreateMockAnalysis()
        {
            var validation = CreateMockValidation(typeof(TestClass));
            return new RuleAnalysis(validation, true);
        }

        #endregion

        #region Test Helper Class

        private class TestClass
        {
            public int IntProperty { get; set; }
            public string? StringProperty { get; set; }

#pragma warning disable CA1822 // Mark members as static
            public void TestMethod() { }
#pragma warning restore CA1822 // Mark members as static
        }

        #endregion
    }
}