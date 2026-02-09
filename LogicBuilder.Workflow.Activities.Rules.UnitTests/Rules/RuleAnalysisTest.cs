using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class RuleAnalysisTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            bool forWrites = true;

            // Act
            var analysis = new RuleAnalysis(validation, forWrites);

            // Assert
            Assert.NotNull(analysis);
            Assert.Equal(forWrites, analysis.ForWrites);
        }

        [Fact]
        public void Constructor_WithForWritesFalse_CreatesInstance()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            bool forWrites = false;

            // Act
            var analysis = new RuleAnalysis(validation, forWrites);

            // Assert
            Assert.NotNull(analysis);
            Assert.False(analysis.ForWrites);
        }

        [Fact]
        public void Constructor_WithNullValidation_CreatesInstance()
        {
            // Arrange & Act
            var analysis = new RuleAnalysis(null, true);

            // Assert
            Assert.NotNull(analysis);
            Assert.True(analysis.ForWrites);
        }

        #endregion

        #region ForWrites Property Tests

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ForWrites_ReturnsCorrectValue(bool forWrites)
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, forWrites);

            // Act
            var result = analysis.ForWrites;

            // Assert
            Assert.Equal(forWrites, result);
        }

        #endregion

        #region AddSymbol Tests

        [Fact]
        public void AddSymbol_WithValidSymbol_AddsSymbol()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            string symbol = "TestSymbol";

            // Act
            analysis.AddSymbol(symbol);
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.Contains(symbol, symbols);
        }

        [Fact]
        public void AddSymbol_WithMultipleSymbols_AddsAllSymbols()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            string symbol1 = "Symbol1";
            string symbol2 = "Symbol2";
            string symbol3 = "Symbol3";

            // Act
            analysis.AddSymbol(symbol1);
            analysis.AddSymbol(symbol2);
            analysis.AddSymbol(symbol3);
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.Equal(3, symbols.Count);
            Assert.Contains(symbol1, symbols);
            Assert.Contains(symbol2, symbols);
            Assert.Contains(symbol3, symbols);
        }

        [Fact]
        public void AddSymbol_WithDuplicateSymbol_OverwritesSymbol()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            string symbol = "DuplicateSymbol";

            // Act
            analysis.AddSymbol(symbol);
            analysis.AddSymbol(symbol);
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.Single(symbols);
            Assert.Contains(symbol, symbols);
        }

        [Fact]
        public void AddSymbol_WithEmptyString_AddsSymbol()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            string symbol = string.Empty;

            // Act
            analysis.AddSymbol(symbol);
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.Contains(symbol, symbols);
        }

        #endregion

        #region GetSymbols Tests

        [Fact]
        public void GetSymbols_WithNoSymbols_ReturnsEmptyCollection()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);

            // Act
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.NotNull(symbols);
            Assert.Empty(symbols);
        }

        [Fact]
        public void GetSymbols_AfterAddingSymbols_ReturnsAllSymbols()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            var expectedSymbols = new[] { "Symbol1", "Symbol2", "Symbol3" };

            foreach (var symbol in expectedSymbols)
            {
                analysis.AddSymbol(symbol);
            }

            // Act
            var symbols = analysis.GetSymbols();

            // Assert
            Assert.Equal(expectedSymbols.Length, symbols.Count);
            foreach (var expectedSymbol in expectedSymbols)
            {
                Assert.Contains(expectedSymbol, symbols);
            }
        }

        [Fact]
        public void GetSymbols_ReturnsNewCollection()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            analysis.AddSymbol("Symbol1");

            // Act
            var symbols1 = analysis.GetSymbols();
            var symbols2 = analysis.GetSymbols();

            // Assert
            Assert.NotSame(symbols1, symbols2);
        }

        [Fact]
        public void GetSymbols_AfterAddingMoreSymbols_ReturnsUpdatedCollection()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            analysis.AddSymbol("Symbol1");
            var symbols1 = analysis.GetSymbols();

            // Act
            analysis.AddSymbol("Symbol2");
            var symbols2 = analysis.GetSymbols();

            // Assert
            Assert.Single(symbols1);
            Assert.Equal(2, symbols2.Count);
        }

        #endregion

        #region AnalyzeRuleAttributes Tests

        [Fact]
        public void AnalyzeRuleAttributes_WithNoAttributes_DoesNotThrow()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            var member = typeof(TestClass).GetProperty(nameof(TestClass.PropertyWithoutAttributes));
            var targetExpr = new CodeThisReferenceExpression();
            var targetQualifier = new RulePathQualifier("test", null);
            var argExprs = new CodeExpressionCollection();
            var parameters = System.Array.Empty<ParameterInfo>();
            var attributedExprs = new List<CodeExpression>();

            // Act & Assert
            analysis.AnalyzeRuleAttributes(member, targetExpr, targetQualifier, argExprs, parameters, attributedExprs);
        }

        [Fact]
        public void AnalyzeRuleAttributes_WithRuleAttributes_CallsAnalyze()
        {
            // Arrange
            var validation = new RuleValidation(typeof(TestClass));
            var analysis = new RuleAnalysis(validation, false);
            var member = typeof(TestClass).GetProperty(nameof(TestClass.PropertyWithReadAttribute));
            var targetExpr = new CodeThisReferenceExpression();
            var targetQualifier = new RulePathQualifier("test", null);
            var argExprs = new CodeExpressionCollection();
            var parameters = System.Array.Empty<ParameterInfo>();
            var attributedExprs = new List<CodeExpression>();

            // Act & Assert - Should not throw
            analysis.AnalyzeRuleAttributes(member, targetExpr, targetQualifier, argExprs, parameters, attributedExprs);
        }

        #endregion

        #region Test Helper Classes

        private class TestClass
        {
            public string? PropertyWithoutAttributes { get; set; }

            [RuleRead("SomeField")]
            public string? PropertyWithReadAttribute { get; set; }

            [RuleWrite("SomeField")]
            public string? PropertyWithWriteAttribute { get; set; }
        }

        #endregion
    }
}