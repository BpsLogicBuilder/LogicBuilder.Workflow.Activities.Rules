using System;
using System.CodeDom;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleStatementActionTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithCodeStatement_SetsCodeDomStatement()
        {
            // Arrange
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression(42));

            // Act
            var action = new RuleStatementAction(statement);

            // Assert
            Assert.NotNull(action.CodeDomStatement);
            Assert.Same(statement, action.CodeDomStatement);
        }

        [Fact]
        public void Constructor_WithCodeExpression_CreatesCodeExpressionStatement()
        {
            // Arrange
            var expression = new CodePrimitiveExpression(42);

            // Act
            var action = new RuleStatementAction(expression);

            // Assert
            Assert.NotNull(action.CodeDomStatement);
            Assert.IsType<CodeExpressionStatement>(action.CodeDomStatement);
            var expressionStatement = (CodeExpressionStatement)action.CodeDomStatement;
            Assert.Same(expression, expressionStatement.Expression);
        }

        [Fact]
        public void Constructor_Default_CreatesInstanceWithNullStatement()
        {
            // Act
            var action = new RuleStatementAction();

            // Assert
            Assert.Null(action.CodeDomStatement);
        }

        #endregion

        #region Property Tests

        [Fact]
        public void CodeDomStatement_CanSetAndGet()
        {
            // Arrange
            var action = new RuleStatementAction();
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression("test"));

            // Act
            action.CodeDomStatement = statement;

            // Assert
            Assert.Same(statement, action.CodeDomStatement);
        }

        [Fact]
        public void CodeDomStatement_CanSetToNull()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)))
            {
                // Act
                CodeDomStatement = null
            };

            // Assert
            Assert.Null(action.CodeDomStatement);
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithNullValidator_ThrowsArgumentNullException()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => action.Validate(null));
        }

        [Fact]
        public void Validate_WithNullCodeDomStatement_ReturnsFalseAndAddsError()
        {
            // Arrange
            var action = new RuleStatementAction();
            var validator = new RuleValidation(typeof(TestClass));

            // Act
            var result = action.Validate(validator);

            // Assert
            Assert.False(result);
            Assert.True(validator.Errors.Count > 0);
            var error = validator.Errors[0];
            Assert.Equal(Common.ErrorNumbers.Error_ParameterNotSet, error.ErrorNumber);
        }

        [Fact]
        public void Validate_WithValidStatement_ReturnsTrue()
        {
            // Arrange
            var statement = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "Value"),
                new CodePrimitiveExpression(42));
            var action = new RuleStatementAction(statement);
            var condition = new CodeObjectCreateExpression("System.Object");
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = condition,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(action);
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            // Act
            var result = action.Validate(validation);

            // Assert
            Assert.True(result);
            Assert.Equal(0, validation.Errors.Count);
        }

        #endregion

        #region Execute Tests

        [Fact]
        public void Execute_WithNullCodeDomStatement_ThrowsInvalidOperationException()
        {
            // Arrange
            var action = new RuleStatementAction();
            var context = new RuleExecution(new RuleValidation(typeof(TestClass)), new TestClass());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => action.Execute(context));
        }

        [Fact]
        public void Execute_WithValidStatement_ExecutesSuccessfully()
        {
            // Arrange
            var testObj = new TestClass { Value = 10 };
            var statement = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "Value"),
                new CodePrimitiveExpression(20));
            var action = new RuleStatementAction(statement);
            var validator = new RuleValidation(typeof(TestClass));
            action.Validate(validator);
            var context = new RuleExecution(validator, testObj);

            // Act
            action.Execute(context);

            // Assert
            Assert.Equal(20, testObj.Value);
        }

        #endregion

        #region GetSideEffects Tests

        [Fact]
        public void GetSideEffects_WithNullCodeDomStatement_ReturnsEmptyCollection()
        {
            // Arrange
            var action = new RuleStatementAction();
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var sideEffects = action.GetSideEffects(validation);

            // Assert
            Assert.NotNull(sideEffects);
            Assert.Empty(sideEffects);
        }

        [Fact]
        public void GetSideEffects_WithAssignmentStatement_ReturnsModifiedPaths()
        {
            // Arrange
            var statement = new CodeAssignStatement(
                new CodePropertyReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "Value"),
                new CodePrimitiveExpression(42));
            var action = new RuleStatementAction(statement);
            var condition = new CodeObjectCreateExpression("System.Object");
            CodeBinaryOperatorExpression ruleNullTest = new()
            {
                Left = condition,
                Operator = CodeBinaryOperatorType.IdentityInequality,
                Right = new CodePrimitiveExpression(null)
            };
            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(ruleNullTest) };
            rule.ThenActions.Add(action);
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);

            // Act
            var sideEffects = action.GetSideEffects(validation);

            // Assert
            Assert.NotNull(sideEffects);
            Assert.Contains("this/Value/", sideEffects);
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            // Arrange
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var action = new RuleStatementAction(statement);

            // Act
            var cloned = action.Clone();

            // Assert
            Assert.NotNull(cloned);
            Assert.IsType<RuleStatementAction>(cloned);
            Assert.NotSame(action, cloned);
        }

        [Fact]
        public void Clone_ClonesCodeDomStatement()
        {
            // Arrange
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var action = new RuleStatementAction(statement);

            // Act
            var cloned = (RuleStatementAction)action.Clone();

            // Assert
            Assert.NotNull(cloned.CodeDomStatement);
            Assert.NotSame(action.CodeDomStatement, cloned.CodeDomStatement);
        }

        [Fact]
        public void Clone_WithNullStatement_ReturnsCloneWithNullStatement()
        {
            // Arrange
            var action = new RuleStatementAction();

            // Act
            var cloned = (RuleStatementAction)action.Clone();

            // Assert
            Assert.NotSame(action, cloned);
            Assert.Null(cloned.CodeDomStatement);
        }

        #endregion

        #region ToString Tests

        [Fact]
        public void ToString_WithNullCodeDomStatement_ReturnsEmptyString()
        {
            // Arrange
            var action = new RuleStatementAction();

            // Act
            var result = action.ToString();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ToString_WithValidStatement_ReturnsDecompiledString()
        {
            // Arrange
            var statement = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var action = new RuleStatementAction(statement);

            // Act
            var result = action.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        #endregion

        #region Equals Tests

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)));

            // Act
            var result = action.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)));
            var other = new RuleHaltAction();

            // Act
            var result = action.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithMatchingStatements_ReturnsTrue()
        {
            // Arrange
            var statement1 = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var statement2 = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var action1 = new RuleStatementAction(statement1);
            var action2 = new RuleStatementAction(statement2);

            // Act
            var result = action1.Equals(action2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentStatements_ReturnsFalse()
        {
            // Arrange
            var statement1 = new CodeExpressionStatement(new CodePrimitiveExpression(42));
            var statement2 = new CodeExpressionStatement(new CodePrimitiveExpression(99));
            var action1 = new RuleStatementAction(statement1);
            var action2 = new RuleStatementAction(statement2);

            // Act
            var result = action1.Equals(action2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_BothWithNullStatements_ReturnsTrue()
        {
            // Arrange
            var action1 = new RuleStatementAction();
            var action2 = new RuleStatementAction();

            // Act
            var result = action1.Equals(action2);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region GetHashCode Tests

        [Fact]
        public void GetHashCode_ReturnsValue()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)));

            // Act
            var hashCode = action.GetHashCode();

            // Assert
            Assert.NotEqual(0, hashCode);
        }

        [Fact]
        public void GetHashCode_CalledTwice_ReturnsSameValue()
        {
            // Arrange
            var action = new RuleStatementAction(new CodeExpressionStatement(new CodePrimitiveExpression(42)));

            // Act
            var hashCode1 = action.GetHashCode();
            var hashCode2 = action.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        #endregion

        #region Helper Classes

        private class TestClass
        {
            public int Value { get; set; }
            public string? Name { get; set; } 
        }

        #endregion
    }
}