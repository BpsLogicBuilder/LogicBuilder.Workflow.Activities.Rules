using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class IndexerPropertyExpressionTest
    {
        private readonly IndexerPropertyExpression _indexerPropertyExpression;

        public IndexerPropertyExpressionTest()
        {
            _indexerPropertyExpression = new IndexerPropertyExpression();
        }

        #region Test Helper Classes

        private class TestClass
        {
            private readonly Dictionary<int, string> _intIndex = new()
            {
                { 0, "zero" },
                { 1, "one" },
                { 2, "two" }
            };

            private readonly Dictionary<string, int> _stringIndex = new()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            private readonly int[] _array = [10, 20, 30, 40, 50];

            private readonly int[,] _twoDimentionalArray = new int [2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            public string this[int index]
            {
                get => _intIndex.TryGetValue(index, out var value) ? value : "not found";
                set => _intIndex[index] = value;
            }

            public int this[string key]
            {
                get => _stringIndex.TryGetValue(key, out var value) ? value : -1;
                set => _stringIndex[key] = value;
            }

            public int this[int x, int y]
            {
                get => _twoDimentionalArray[x, y];
                set { _twoDimentionalArray[x, y] = value; }
            }

            public int ArrayValue(int index) => _array[index];

            public int[] GetArray() => _array;
        }

        private class TestClassWithReadOnlyIndexer
        {
            private readonly int[] _values = [1, 2, 3];

            public int this[int index] => _values[index];
        }

        private class TestClassWithWriteOnlyIndexer
        {
            private readonly int[] _values = new int[5];

            public int this[int index]
            {
                set => _values[index] = value;
            }
        }

        private class TestClassWithParamsIndexer
        {
            private readonly Dictionary<int, string> _intIndex = new()
            {
                { 0, "zero" },
                { 1, "one" },
                { 2, "two" }
            };

            public string this[int x, params string[] args]
            {
                get => $"{x}: {string.Join(", ", args)}";
                set 
                {
                    _intIndex[x] = string.Join(", ", args);
                }
            }
        }

        private class TestRuleClass
        {
            public TestClass? TestInstance { get; set; } = new();
            public List<int> IntArray { get; set; } = [5, 10, 15, 20, 25];
        }

        #endregion

        #region Validate Tests

        [Fact]
        public void Validate_WithValidIntIndexer_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(1);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithValidStringIndexer_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression("one");
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithMultipleIndices_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(5);
            var indexExpr2 = new CodePrimitiveExpression(10);
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNullTargetObject_ReturnsNullAndAddsError()
        {
            // Arrange
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(null, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("target", validation.Errors[0].ErrorText, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Validate_WithStaticTypeReference_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeTypeReferenceExpression(typeof(TestClass));
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("static", validation.Errors[0].ErrorText, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Validate_WithNoIndices_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeIndexerExpression(targetObject);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
            Assert.Contains("index", validation.Errors[0].ErrorText, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Validate_WithRefOrOutParameter_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodeDirectionExpression(
                FieldDirection.Ref, 
                new CodePrimitiveExpression(0));
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Equal(2, validation.Errors.Count);
            Assert.Contains("ref", validation.Errors[0].ErrorText, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Validate_WithNullLiteralTarget_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodePrimitiveExpression(null);
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_ReadOnlyIndexer_WhenWritten_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClassWithReadOnlyIndexer));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, true);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_WriteOnlyIndexer_WhenRead_ReturnsNullAndAddsError()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestClassWithWriteOnlyIndexer));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.Null(result);
            Assert.Single(validation.Errors);
        }

        [Fact]
        public void Validate_WithArrayIndexer_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntArray");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(int), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithNestedIndexer_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var indexExpr = new CodePrimitiveExpression(1);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var validation = new RuleValidation(typeof(TestRuleClass));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        [Fact]
        public void Validate_WithParamsIndexer_ReturnsCorrectExpressionInfo()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(5);
            var indexExpr2 = new CodePrimitiveExpression("a");
            var indexExpr3 = new CodePrimitiveExpression("b");
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2, indexExpr3);
            var validation = new RuleValidation(typeof(TestClassWithParamsIndexer));

            // Act
            var result = _indexerPropertyExpression.Validate(expression, validation, false);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(validation.Errors);
            Assert.Equal(typeof(string), result.ExpressionType);
        }

        #endregion

        #region AnalyzeUsage Tests

        [Fact]
        public void AnalyzeUsage_WithSimpleIndexer_AnalyzesTargetAndIndex()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            
            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression("value"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, true);

            var analysis = new RuleAnalysis(validation, true);

            // Act
            _indexerPropertyExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            Assert.NotEmpty(analysis.GetSymbols());
        }

        [Fact]
        public void AnalyzeUsage_WhenRead_AnalyzesAsRead()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            
            CodeBinaryOperatorExpression condition = new()
            {
                Left = expression,
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("one")
            };

            RuleSet ruleSet = new();
            Rule rule = new("TestRule") { Condition = new RuleExpressionCondition(condition) };
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var analysis = new RuleAnalysis(validation, false);

            // Act
            _indexerPropertyExpression.AnalyzeUsage(expression, analysis, true, false, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_WithMultipleIndices_AnalyzesAllIndices()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(5);
            var indexExpr2 = new CodePrimitiveExpression(10);
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2);
            
            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression(100));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, true);

            var analysis = new RuleAnalysis(validation, true);

            // Act
            _indexerPropertyExpression.AnalyzeUsage(expression, analysis, false, true, null);

            // Assert
            var symbols = analysis.GetSymbols();
            Assert.NotEmpty(symbols);
        }

        [Fact]
        public void AnalyzeUsage_WithoutValidation_ThrowsInvalidOperationException()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _indexerPropertyExpression.AnalyzeUsage(expression, analysis, true, false, null));
        }

        #endregion

        #region Evaluate Tests

        [Fact]
        public void Evaluate_WithValidIntIndexer_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(1);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression("value"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("one", result.Value);
        }

        [Fact]
        public void Evaluate_WithValidStringIndexer_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression("two");
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression(100));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Value);
        }

        [Fact]
        public void Evaluate_WithMultipleIndices_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(1);
            var indexExpr2 = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression(100));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Value);
        }

        [Fact]
        public void Evaluate_WithArrayIndexer_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestRuleClass();
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "IntArray");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression(999));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(15, result.Value);
        }

        [Fact]
        public void Evaluate_WithNestedIndexer_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestRuleClass();
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var indexExpr = new CodePrimitiveExpression(2);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression("value"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("two", result.Value);
        }

        [Fact]
        public void Evaluate_WithNullTarget_ThrowsRuleEvaluationException()
        {
            // Arrange
            var testInstance = new TestRuleClass { TestInstance = null };
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression("value"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestRuleClass));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                _indexerPropertyExpression.Evaluate(expression, execution));
        }

        [Fact]
        public void Evaluate_WithoutValidation_ThrowsInvalidOperationException()
        {
            // Arrange
            var testInstance = new TestClass();
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            var validation = new RuleValidation(typeof(TestClass));
            var execution = new RuleExecution(validation, testInstance);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _indexerPropertyExpression.Evaluate(expression, execution));
        }

        [Fact]
        public void Evaluate_WithParamsIndexer_ReturnsCorrectValue()
        {
            // Arrange
            var testInstance = new TestClassWithParamsIndexer();
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(42);
            var indexExpr2 = new CodePrimitiveExpression("first");
            var indexExpr3 = new CodePrimitiveExpression("second");
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2, indexExpr3);

            CodeAssignStatement assignStatement = new(expression, new CodePrimitiveExpression("value"));
            RuleSet ruleSet = new();
            Rule rule = new("TestRule");
            rule.ThenActions.Add(new RuleStatementAction(assignStatement));
            ruleSet.Rules.Add(rule);

            var validation = new RuleValidation(typeof(TestClassWithParamsIndexer));
            ruleSet.Validate(validation);
            _indexerPropertyExpression.Validate(expression, validation, false);

            var execution = new RuleExecution(validation, testInstance);

            // Act
            var result = _indexerPropertyExpression.Evaluate(expression, execution);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("42: first, second", result.Value);
        }

        #endregion

        #region Decompile Tests

        [Fact]
        public void Decompile_WithValidExpression_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act
            _indexerPropertyExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("[", result);
            Assert.Contains("]", result);
            Assert.Contains("0", result);
        }

        [Fact]
        public void Decompile_WithMultipleIndices_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(5);
            var indexExpr2 = new CodePrimitiveExpression(10);
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2);
            var stringBuilder = new StringBuilder();

            // Act
            _indexerPropertyExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("[", result);
            Assert.Contains("]", result);
            Assert.Contains("5", result);
            Assert.Contains("10", result);
            Assert.Contains(",", result);
        }

        [Fact]
        public void Decompile_WithStringIndex_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression("key");
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act
            _indexerPropertyExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("[", result);
            Assert.Contains("]", result);
            Assert.Contains("key", result);
        }

        [Fact]
        public void Decompile_WithNestedExpression_ReturnsCorrectString()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act
            _indexerPropertyExpression.Decompile(expression, stringBuilder, null);

            // Assert
            var result = stringBuilder.ToString();
            Assert.Contains("this", result);
            Assert.Contains("TestInstance", result);
            Assert.Contains("[", result);
            Assert.Contains("]", result);
            Assert.Contains("0", result);
        }

        [Fact]
        public void Decompile_WithNullTargetObject_ThrowsRuleEvaluationException()
        {
            // Arrange
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(null, indexExpr);
            var stringBuilder = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                _indexerPropertyExpression.Decompile(expression, stringBuilder, null));
        }

        [Fact]
        public void Decompile_WithNoIndices_ThrowsRuleEvaluationException()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var expression = new CodeIndexerExpression(targetObject);
            var stringBuilder = new StringBuilder();

            // Act & Assert
            Assert.Throws<RuleEvaluationException>(() =>
                _indexerPropertyExpression.Decompile(expression, stringBuilder, null));
        }

        #endregion

        #region Clone Tests

        [Fact]
        public void Clone_WithValidExpression_ReturnsIdenticalCopy()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            // Act
            var cloned = _indexerPropertyExpression.Clone(expression) as CodeIndexerExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Single(cloned.Indices);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);
            Assert.NotSame(expression.Indices[0], cloned.Indices[0]);
        }

        [Fact]
        public void Clone_WithMultipleIndices_ReturnsDeepCopy()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(5);
            var indexExpr2 = new CodePrimitiveExpression(10);
            var expression = new CodeIndexerExpression(targetObject, indexExpr1, indexExpr2);

            // Act
            var cloned = _indexerPropertyExpression.Clone(expression) as CodeIndexerExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.Equal(2, cloned.Indices.Count);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);
            Assert.NotSame(expression.Indices[0], cloned.Indices[0]);
            Assert.NotSame(expression.Indices[1], cloned.Indices[1]);
        }

        [Fact]
        public void Clone_WithNestedExpression_ReturnsDeepCopy()
        {
            // Arrange
            var targetObject = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var indexExpr = new CodePrimitiveExpression(0);
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            // Act
            var cloned = _indexerPropertyExpression.Clone(expression) as CodeIndexerExpression ?? throw new InvalidOperationException("Cloned expression is null.");

            // Assert
            Assert.NotNull(cloned);
            Assert.NotSame(expression, cloned);
            Assert.NotSame(expression.TargetObject, cloned.TargetObject);

            var originalTarget = expression.TargetObject as CodePropertyReferenceExpression ?? throw new InvalidOperationException("Original target is null.");
            var clonedTarget = cloned.TargetObject as CodePropertyReferenceExpression ?? throw new InvalidOperationException("Cloned target is null.");
            Assert.Equal(originalTarget.PropertyName, clonedTarget.PropertyName);
            Assert.NotSame(originalTarget, clonedTarget);
        }

        [Fact]
        public void Clone_WithStringIndex_ReturnsCopyWithSameValue()
        {
            // Arrange
            var targetObject = new CodeThisReferenceExpression();
            var indexExpr = new CodePrimitiveExpression("key");
            var expression = new CodeIndexerExpression(targetObject, indexExpr);

            // Act
            var cloned = _indexerPropertyExpression.Clone(expression) as CodeIndexerExpression;

            // Assert
            Assert.NotNull(cloned);
            Assert.Single(cloned.Indices);
            Assert.NotSame(expression, cloned);
            
            var originalIndex = expression.Indices[0] as CodePrimitiveExpression;
            var clonedIndex = cloned.Indices[0] as CodePrimitiveExpression;
            Assert.NotNull(originalIndex);
            Assert.NotNull(clonedIndex);
            Assert.Equal(originalIndex.Value, clonedIndex.Value);
        }

        #endregion

        #region Match Tests

        [Fact]
        public void Match_WithIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(0);
            var expression1 = new CodeIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeThisReferenceExpression();
            var indexExpr2 = new CodePrimitiveExpression(0);
            var expression2 = new CodeIndexerExpression(targetObject2, indexExpr2);

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithDifferentIndices_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var indexExpr1 = new CodePrimitiveExpression(0);
            var expression1 = new CodeIndexerExpression(targetObject1, indexExpr1);

            var targetObject2 = new CodeThisReferenceExpression();
            var indexExpr2 = new CodePrimitiveExpression(1);
            var expression2 = new CodeIndexerExpression(targetObject2, indexExpr2);

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentTargetObjects_ReturnsFalse()
        {
            // Arrange
            var expression1 = new CodeIndexerExpression(
                new CodeThisReferenceExpression(),
                new CodePrimitiveExpression(0));

            var expression2 = new CodeIndexerExpression(
                new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TestInstance"),
                new CodePrimitiveExpression(0));

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithDifferentIndexCounts_ReturnsFalse()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodeIndexerExpression(targetObject1, new CodePrimitiveExpression(0));

            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodeIndexerExpression(
                targetObject2,
                new CodePrimitiveExpression(0),
                new CodePrimitiveExpression(1));

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Match_WithIdenticalMultipleIndices_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodeIndexerExpression(
                targetObject1,
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(10));

            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodeIndexerExpression(
                targetObject2,
                new CodePrimitiveExpression(5),
                new CodePrimitiveExpression(10));

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithNestedIdenticalExpressions_ReturnsTrue()
        {
            // Arrange
            var targetObject1 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var expression1 = new CodeIndexerExpression(targetObject1, new CodePrimitiveExpression(0));

            var targetObject2 = new CodePropertyReferenceExpression(
                new CodeThisReferenceExpression(),
                "TestInstance");
            var expression2 = new CodeIndexerExpression(targetObject2, new CodePrimitiveExpression(0));

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Match_WithStringIndices_WorksCorrectly()
        {
            // Arrange
            var targetObject1 = new CodeThisReferenceExpression();
            var expression1 = new CodeIndexerExpression(targetObject1, new CodePrimitiveExpression("key"));

            var targetObject2 = new CodeThisReferenceExpression();
            var expression2 = new CodeIndexerExpression(targetObject2, new CodePrimitiveExpression("key"));

            // Act
            var result = _indexerPropertyExpression.Match(expression1, expression2);

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}