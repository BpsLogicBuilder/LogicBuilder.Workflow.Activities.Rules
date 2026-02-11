using LogicBuilder.Workflow.ComponentModel.Serialization;
using System;
using System.CodeDom;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests
{
    public partial class RulesTests
    {
        public RulesTests()
        {
            CreateRuleEngine();
        }

        [Fact]
        public void Test_Rule_with_equals_condition_and_setter_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "CT"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal(100, entity.Discount);
        }

        [Fact]
        public void Test_Rule_with_array_indexer_condition_and_setter_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new();
            entity.StringList[1, 1] = "A";

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal(200, entity.Discount);
        }

        [Fact]
        public void Test_Rule_with_method_condition()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                BoolText = "false"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal(300, entity.Discount);
        }

        [Fact]
        public void Test_Rule_with_multiple_conditions()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                BoolText = "false",
                State = "MD"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal(400, entity.Discount);
        }

        [Fact]
        public void Test_Rule_with_static_method_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "MA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal("NH", SampleFlow.FlowEntity.DEFAULTSTATE);
        }

        [Fact]
        public void Test_Rule_with_reference_method_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "PA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.TheType.FullName == typeof(SampleFlow.FlowEntity).FullName);
        }

        [Fact]
        public void Test_Rule_with_cast_object_expression_in_then_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "VA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal("This Description", ((SampleFlow.ChildEntity)entity.DClass).Description);
        }

        [Fact]
        public void Test_Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "NC"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal("AAA", entity.FirstValue);
        }

        [Fact]
        public void Test_Rule_with_list_initialization()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "SC"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal("AValue", entity.MyCollection.First().ToString());
        }

        [Fact]
        public void Test_Rule_with_child_and_granchild_reference()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "GA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.Equal("This Value", entity.FirstClass.SecondClass.Property1);
        }

        [Fact]
        public void Test_Rule_with_generic_object_initialization()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "TN"
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal("Stay", entity.GenericString.CurrentValue);
        }

        [Fact]
        public void Test_Rule_with_generic_list_initialization()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "AL"
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal(1.45m, entity.GenericListOfDecimal.CurrentValue[0]);
        }

        //[Fact(Skip = "Unexpected return charater on GitHub Actions runner.")]
        [Fact]
        public void TestSerialization()
        {
            //Arrange
            string existing = $"<RuleSet ChainingBehavior=\"Full\" Description=\"{{p1:Null}}\" Name=\"MyRuleSet\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">{Environment.NewLine}\t<RuleSet.Rules>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule0\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">TX</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"Parse\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"System.DateTime, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">01/03/2017</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"InvariantCulture\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"System.Globalization.CultureInfo, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"Parse\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"System.TimeSpan, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">12:3:5</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"InvariantCulture\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"System.Globalization.CultureInfo, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"Parse\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"System.Guid, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">{{}}{{7E75868B-CDBE-408C-BEA2-88F887ACD725}}</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100.0012</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100.0012</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Char xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">P</ns1:Char>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Single xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100.01</ns1:Single>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"DClass2\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Single xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100.0012</ns1:Single>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule1\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">CT</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">100</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule2\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression.Indices>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">1</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">1</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression.Indices>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"StringList\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">A</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">200</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule3\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"BoolMethod\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">300</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule4\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"BooleanAnd\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">MD</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"BoolMethod\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">400</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule5\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">MA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetDefaultState\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.FlowEntity\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">NH</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule6\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">PA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"TheType\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetType\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule7\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">VA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Description\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeCastExpression TargetType=\"SampleFlow.ChildEntity\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeCastExpression.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeFieldReferenceExpression FieldName=\"DClass\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeFieldReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeFieldReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeFieldReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeCastExpression.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeCastExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">This Description</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule8\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">NC</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetFirstValue\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"SampleFlow.OtherEntity\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">AAA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">BBB</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule9\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">SC</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetCollection\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"System.Collections.ObjectModel.Collection`1[[System.Object, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression CreateType=\"System.Object, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" Size=\"0\" SizeExpression=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression.Initializers>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">AValue</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">BValue</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression.Initializers>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule10\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">GA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Property1\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"SecondClass\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"FirstClass\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">This Value</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule11\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">TN</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetGenericObject\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"SampleFlow.GenericClass`1[System.String]\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">7</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">VName</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">Stay</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">ObjectData</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule12\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">AL</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetGenericObject\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"SampleFlow.GenericClass`1[System.Collections.Generic.IList`1[System.Decimal]]\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">7</ns1:Int32>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">VName</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"System.Collections.Generic.List`1[[System.Decimal, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression CreateType=\"System.Decimal, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\" Size=\"0\" SizeExpression=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression.Initializers>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">1.45</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">2.35</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression.Initializers>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">ObjectData</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule13\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">AZ</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"MyArray\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"StaticMethod\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.ListConverter`1[System.String]\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"MyList\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeAssignStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule14\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetDiscount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">101</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetState\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">OR</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule15\" Priority=\"100\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetState\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">OR</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetDiscount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">102</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule16\" Priority=\"0\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetDiscount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">103</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetState\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">WA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t\t<RuleUpdateAction Path=\"this/AlwaysTrue\" />{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t\t<Rule Active=\"True\" Description=\"{{p1:Null}}\" Name=\"Rule17\" Priority=\"100\" ReevaluationBehavior=\"Always\">{Environment.NewLine}\t\t\t<Rule.Condition>{Environment.NewLine}\t\t\t\t<RuleExpressionCondition Name=\"{{p1:Null}}\">{Environment.NewLine}\t\t\t\t\t<RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"BooleanAnd\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetState\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">WA</ns1:String>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"AlwaysTrue\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Boolean xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">true</ns1:Boolean>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>{Environment.NewLine}\t\t\t\t\t</RuleExpressionCondition.Expression>{Environment.NewLine}\t\t\t\t</RuleExpressionCondition>{Environment.NewLine}\t\t\t</Rule.Condition>{Environment.NewLine}\t\t\t<Rule.ThenActions>{Environment.NewLine}\t\t\t\t<RuleStatementAction>{Environment.NewLine}\t\t\t\t\t<RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{{p1:Null}}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System.CodeDom, Version=10.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51\">{Environment.NewLine}\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetDiscount\">{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.StaticClass\" />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>{Environment.NewLine}\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Decimal xmlns:ns1=\"clr-namespace:System;Assembly=System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">104</ns1:Decimal>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>{Environment.NewLine}\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>{Environment.NewLine}\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>{Environment.NewLine}\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>{Environment.NewLine}\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>{Environment.NewLine}\t\t\t\t\t\t</ns0:CodeExpressionStatement>{Environment.NewLine}\t\t\t\t\t</RuleStatementAction.CodeDomStatement>{Environment.NewLine}\t\t\t\t</RuleStatementAction>{Environment.NewLine}\t\t\t</Rule.ThenActions>{Environment.NewLine}\t\t</Rule>{Environment.NewLine}\t</RuleSet.Rules>{Environment.NewLine}</RuleSet>";
            //Act
            string rulesSetString = SerializeRules(ruleSet)!;
            //Assert
            Assert.Equal(existing, rulesSetString);
        }

        [Fact]
        public void Test_Rule_set_literals()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "TX"
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal(100.0012f, (float)entity.DClass2);
        }

        [Fact]
        public void Test_Rule_call_method_in_static_generic_class()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                State = "AZ"
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal("Apple", entity.MyArray[0]);
        }

        [Fact]
        public void Test_Rule_reevaluation_WITHOUT_update_action_and_alwaysTrue_property()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                Discount = 101m
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal(101m, entity.Discount);
            Assert.Equal("OR", entity.State);
        }

        [Fact]
        public void Test_Rule_reevaluation_WITH_update_action_and_alwaysTrue_property()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new()
            {
                Discount = 103m
            };

            //Act
            ruleEngine.Execute(entity);

            //Assert
            Assert.Equal(104m, entity.Discount);
            Assert.Equal("WA", entity.State);
        }

        private static Rule Rule_set_literals()
        {
            CodePropertyReferenceExpression invariantCultureReference = new(new CodeTypeReferenceExpression(typeof(System.Globalization.CultureInfo)), "InvariantCulture");

            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("TX")
            };

            CodePropertyReferenceExpression dClassRef = new(new CodeThisReferenceExpression(), "DClass2");

            CodeAssignStatement dateAction = new(
                    dClassRef,
                    new CodeMethodInvokeExpression
                        (
                            new CodeTypeReferenceExpression(typeof(System.DateTime)),
                            "Parse",
                            [new CodePrimitiveExpression("01/03/2017"), invariantCultureReference]
                        )
                );

            CodeAssignStatement timeSpanAction = new(
                    dClassRef,
                    new CodeMethodInvokeExpression
                        (
                            new CodeTypeReferenceExpression(typeof(System.TimeSpan)),
                            "Parse",
                            [new CodePrimitiveExpression("12:3:5"), invariantCultureReference]
                        )
                );
            //System.Guid.Parse()
            CodeAssignStatement guidAction = new                (
                    dClassRef,
                    new CodeMethodInvokeExpression
                        (
                            new CodeTypeReferenceExpression(typeof(System.Guid)),
                            "Parse",
                            [new CodePrimitiveExpression("{7E75868B-CDBE-408C-BEA2-88F887ACD725}")]
                        )
                );
            //decimal d = decimal.Parse("100.0012M", System.Globalization.CultureInfo.InvariantCulture);
            CodeAssignStatement decimalAction = new(dClassRef, new CodePrimitiveExpression(100.0012m));
            CodeAssignStatement decimalAction1 = new(dClassRef, new CodePrimitiveExpression(decimal.Parse("100.0012", System.Globalization.CultureInfo.InvariantCulture)));
            CodeAssignStatement charAction = new(dClassRef, new CodePrimitiveExpression(char.Parse("P")));
            CodeAssignStatement intAction = new(dClassRef, new CodePrimitiveExpression(100));
            CodeAssignStatement floatAction = new(dClassRef, new CodePrimitiveExpression(100.01f));
            CodeAssignStatement floatAction2 = new(dClassRef, new CodePrimitiveExpression(float.Parse("100.0012")));

            Rule rule0 = new("Rule0")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };

            rule0.ThenActions.Add(new RuleStatementAction(dateAction));
            rule0.ThenActions.Add(new RuleStatementAction(timeSpanAction));
            rule0.ThenActions.Add(new RuleStatementAction(guidAction));
            rule0.ThenActions.Add(new RuleStatementAction(decimalAction));
            rule0.ThenActions.Add(new RuleStatementAction(decimalAction1));
            rule0.ThenActions.Add(new RuleStatementAction(charAction));
            rule0.ThenActions.Add(new RuleStatementAction(intAction));
            rule0.ThenActions.Add(new RuleStatementAction(floatAction));
            rule0.ThenActions.Add(new RuleStatementAction(floatAction2));

            return rule0;
        }

        private static Rule Rule_with_equals_condition_and_setter_action()
        {
            // define first predicate: this.State == "CT"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("CT")
            };

            //discount action this.Discount = 100
            CodeAssignStatement discountAction = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(100)
                );

            Rule rule1 = new("Rule1")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule1.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule1;
        }

        private static Rule Rule_with_array_indexer_condition_and_setter_action()
        {
            //this.StringList[1, 1]
            CodeArrayIndexerExpression indexerExpression = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "StringList"),
                    [new CodePrimitiveExpression(1), new CodePrimitiveExpression(1)]
                );

            //this.StringList[1, 1] == "A"
            CodeBinaryOperatorExpression stringIndexTest = new()
            {
                Left = indexerExpression,
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("A")
            };

            //discount action this.Discount = 200
            CodeAssignStatement discountAction = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(200)
                );

            Rule rule2 = new("Rule2")
            {
                Condition = new RuleExpressionCondition(stringIndexTest)
            };
            rule2.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule2;
        }


        private static Rule Rule_with_method_condition()
        {
            //this.boolMethod()
            CodeMethodInvokeExpression boolMethodInvoke = new(new CodeThisReferenceExpression(), "BoolMethod", []);

            //discount action this.discount = 300
            CodeAssignStatement discountAction = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(300)
                );

            Rule rule3 = new("Rule3")
            {
                Condition = new RuleExpressionCondition(boolMethodInvoke)
            };
            rule3.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule3;
        }


        private static Rule Rule_with_multiple_conditions()
        {
            // define first predicate: this.State == "MD"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("MD")
            };

            //this.boolMethod()
            CodeMethodInvokeExpression boolMethodInvoke = new(new CodeThisReferenceExpression(), "BoolMethod", []);

            //combine both expressions this.state == "MD" && this.boolMethod()
            CodeBinaryOperatorExpression codeBothExpression = new()
            {
                Left = ruleStateTest,
                Operator = CodeBinaryOperatorType.BooleanAnd,
                Right = boolMethodInvoke
            };

            //discount action this.Discount = 400
            CodeAssignStatement discountAction = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(400)
                );

            Rule rule4 = new("Rule4")
            {
                Condition = new RuleExpressionCondition(codeBothExpression)
            };
            rule4.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule4;
        }


        private static Rule Rule_with_static_method_action()
        {
            // define first predicate: this.State == "MA"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("MA")
            };

            //action SampleFlow.FlowEntity.SetDefaultState("NH")
            CodeMethodInvokeExpression methodInvoke = new                (
                    new CodeTypeReferenceExpression("SampleFlow.FlowEntity"),
                    "SetDefaultState",
                    [new CodePrimitiveExpression("NH")]
                );

            Rule rule5 = new("Rule5")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule5.ThenActions.Add(new RuleStatementAction(methodInvoke));

            return rule5;
        }


        private static Rule Rule_with_reference_method_action()
        {
            // define first predicate: this.State == "PA"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("PA")
            };


            //action this.TheType = this.GetType
            CodeAssignStatement setTypeAction = new                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TheType"),
                    new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetType", [])
                );

            Rule rule6 = new("Rule6")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule6.ThenActions.Add(new RuleStatementAction(setTypeAction));

            return rule6;
        }


        private static Rule Rule_with_Cast_object_expression_in_then_action()
        {
            // define first predicate: this.State == "VA"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("VA")
            };

            //(SampleFlow.ChildEntity)this.DClass
            CodeCastExpression castExpression = new(
                "SampleFlow.ChildEntity",
                 //this.DClass
                 new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "DClass")
                 );

            //((SampleFlow.ChildEntity)this.DClass).Description = "This Description"
            CodeAssignStatement assignmentAction = new(
                    new CodePropertyReferenceExpression(castExpression, "Description"),
                    new CodePrimitiveExpression("This Description")
                );

            Rule rule7 = new("Rule7")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule7.ThenActions.Add(new RuleStatementAction(assignmentAction));

            return rule7;
        }


        private static Rule Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action()
        {
            // define first predicate: this.State == "NC"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("NC")
            };

            //this.DoNothing(new SampleFlow.OtherEntity("AAA", "BBB"))
            CodeMethodInvokeExpression methodInvokeDoNothing = new(
                    new CodeThisReferenceExpression(),
                    "SetFirstValue",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference("SampleFlow.OtherEntity"),
                            [
                                new CodePrimitiveExpression("AAA"),
                                new CodePrimitiveExpression("BBB")
                            ]
                        )
                );

            Rule rule8 = new("Rule8")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule8.ThenActions.Add(new RuleStatementAction(methodInvokeDoNothing));

            return rule8;
        }

        private static Rule Rule_with_list_initialization()
        {
            // define first predicate: this.State == "SC"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("SC")
            };

            //this.SetCollection(new System.Collections.ObjectModel.Collection<object>(new object[] { "AValue", "BValue"}))
            CodeMethodInvokeExpression methodInvokeSetCollection = new(
                    new CodeThisReferenceExpression(),
                    "SetCollection",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference
                                (
                                    "System.Collections.ObjectModel.Collection",
                                    [new CodeTypeReference("System.Object")]
                                ),
                            new CodeArrayCreateExpression
                            (
                                "System.Object",
                                [new CodePrimitiveExpression("AValue"), new CodePrimitiveExpression("BValue")]
                            )
                        )
                );

            Rule rule9 = new("Rule9")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule9.ThenActions.Add(new RuleStatementAction(methodInvokeSetCollection));

            return rule9;
        }

        private static Rule Rule_with_child_and_granchild_reference()
        {
            // define first predicate: this.State == "GA"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("GA")
            };

            //this.FirstClass.SecondClass.Property1 = "This Value"
            CodeAssignStatement setProperty1Action = new(
                    new CodePropertyReferenceExpression
                    (
                        new CodePropertyReferenceExpression
                        (
                            new CodePropertyReferenceExpression
                            (
                                new CodeThisReferenceExpression(),
                                "FirstClass"
                            ),
                            "SecondClass"
                        ),
                        "Property1"
                    ),
                    new CodePrimitiveExpression("This Value")
                );

            Rule rule10 = new("Rule10")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule10.ThenActions.Add(new RuleStatementAction(setProperty1Action));

            return rule10;
        }

        private static Rule Rule_with_generic_object_initialization()
        {
            // define first predicate: this.State == "TN"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("TN")
            };

            //this.SetGenericObject(new SampleFlow.GenericClass<string>(7, "VName", "Stay", "ObjectData")
            CodeMethodInvokeExpression methodInvokeSetGenericObject = new(
                    new CodeThisReferenceExpression(),
                    "SetGenericObject",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference
                                (
                                    "SampleFlow.GenericClass",
                                    [new CodeTypeReference("System.String")]
                                ),
                            new CodePrimitiveExpression(7),
                            new CodePrimitiveExpression("VName"),
                            new CodePrimitiveExpression("Stay"),
                            new CodePrimitiveExpression("ObjectData")
                        )
                );

            Rule rule11 = new("Rule11")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule11.ThenActions.Add(new RuleStatementAction(methodInvokeSetGenericObject));

            return rule11;
        }

        private static Rule Rule_with_generic_list_initialization()
        {
            // define first predicate: this.State == "AL"
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("AL")
            };

            //this.SetGenericObject(new SampleFlow.GenericClass<string>(7, "VName", new List<decimal> { 1.45m, 2.35m }, "ObjectData")
            CodeMethodInvokeExpression methodInvokeSetGenericList = new(
                    new CodeThisReferenceExpression(),
                    "SetGenericObject",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference
                                (
                                    "SampleFlow.GenericClass",
                                    [new CodeTypeReference("System.Collections.Generic.IList`1[[System.Decimal]]")]
                                ),
                            new CodePrimitiveExpression(7),
                            new CodePrimitiveExpression("VName"),
                            new CodeObjectCreateExpression
                            (
                                new CodeTypeReference
                                    (
                                        "System.Collections.Generic.List",
                                        [new CodeTypeReference("System.Decimal")]
                                    ),
                                new CodeArrayCreateExpression
                                (
                                    "System.Decimal",
                                    [new CodePrimitiveExpression(1.45m), new CodePrimitiveExpression(2.35m)]
                                )
                            ),
                            new CodePrimitiveExpression("ObjectData")
                        )
                );

            Rule rule12 = new("Rule12")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule12.ThenActions.Add(new RuleStatementAction(methodInvokeSetGenericList));

            return rule12;
        }

        private static Rule Rule_call_method_in_static_generic_class()
        {
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("AZ")
            };

            CodePropertyReferenceExpression myArrayRef = new(new CodeThisReferenceExpression(), "MyArray");
            CodePropertyReferenceExpression myListRef = new(new CodeThisReferenceExpression(), "MyList");

            CodeAssignStatement convertListToArray = new(
                    myArrayRef,
                    new CodeMethodInvokeExpression
                        (
                            new CodeTypeReferenceExpression
                                (
                                    new CodeTypeReference("SampleFlow.ListConverter",
                                    [new CodeTypeReference("System.String")])
                                ),
                            "StaticMethod",
                            myListRef
                        )
                );

            Rule rule13 = new("Rule13")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };

            rule13.ThenActions.Add(new RuleStatementAction(convertListToArray));

            return rule13;
        }

        private static Rule Rule_set_state_WITHOUT_update_action()
        {
            CodeBinaryOperatorExpression ruleDiscountTest = new()
            {
                Left = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "GetDiscount", [new CodeThisReferenceExpression()]),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(101m)
            };

            CodeMethodInvokeExpression setState = new(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "SetState", [new CodeThisReferenceExpression(), new CodePrimitiveExpression("OR")]);

            Rule rule14 = new("Rule14")
            {
                Condition = new RuleExpressionCondition(ruleDiscountTest)
            };

            rule14.ThenActions.Add(new RuleStatementAction(setState));

            return rule14;
        }

        private static Rule Rule_get_state_WITHOUT_AlwaysTrue_property_for_reevaluation()
        {
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "GetState", [new CodeThisReferenceExpression()]),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("OR")
            };

            CodeMethodInvokeExpression setDiscount = new(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "SetDiscount", [new CodeThisReferenceExpression(), new CodePrimitiveExpression(102m)]);

            Rule rule15 = new("Rule15")
            {
                Condition = new RuleExpressionCondition(ruleStateTest),
                Priority = 100
            };

            rule15.ThenActions.Add(new RuleStatementAction(setDiscount));

            return rule15;
        }

        private static Rule Rule_set_state_WITH_update_action_set_targeting_AlwaysTrue_property()
        {
            CodeBinaryOperatorExpression ruleDiscountTest = new()
            {
                Left = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "GetDiscount", [new CodeThisReferenceExpression()]),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(103m)
            };

            CodeMethodInvokeExpression setState = new(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "SetState", [new CodeThisReferenceExpression(), new CodePrimitiveExpression("WA")]);

            Rule rule16 = new("Rule16")
            {
                Condition = new RuleExpressionCondition(ruleDiscountTest),
                Priority = 0
            };

            rule16.ThenActions.Add(new RuleStatementAction(setState));
            rule16.ThenActions.Add(new RuleUpdateAction("this/AlwaysTrue"));

            return rule16;
        }

        private static Rule Rule_get_state_WITH_AlwaysTrue_property_to_reevaluate_update_action()
        {
            CodeBinaryOperatorExpression ruleStateTest = new()
            {
                Left = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "GetState", [new CodeThisReferenceExpression()]),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("WA")
            };

            CodeBinaryOperatorExpression ruleAlwaysTrueTest = new()
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "AlwaysTrue"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression(true)
            };

            CodeBinaryOperatorExpression ruleBothTest = new()
            {
                Left = ruleStateTest,
                Operator = CodeBinaryOperatorType.BooleanAnd,
                Right = ruleAlwaysTrueTest
            };

            CodeMethodInvokeExpression setDiscount = new(new CodeTypeReferenceExpression("SampleFlow.StaticClass"), "SetDiscount", [new CodeThisReferenceExpression(), new CodePrimitiveExpression(104m)]);

            Rule rule17 = new("Rule17")
            {
                Condition = new RuleExpressionCondition(ruleBothTest),
                Priority = 100
            };

            rule17.ThenActions.Add(new RuleStatementAction(setDiscount));

            return rule17;
        }

        [System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(ruleSet), nameof(ruleEngine))]
        private void CreateRuleEngine()
        {
            ruleSet = new RuleSet
            {
                Name = "MyRuleSet",
                ChainingBehavior = RuleChainingBehavior.Full
            };

            ruleSet.Rules.Add(Rule_set_literals());
            ruleSet.Rules.Add(Rule_with_equals_condition_and_setter_action());
            ruleSet.Rules.Add(Rule_with_array_indexer_condition_and_setter_action());
            ruleSet.Rules.Add(Rule_with_method_condition());
            ruleSet.Rules.Add(Rule_with_multiple_conditions());
            ruleSet.Rules.Add(Rule_with_static_method_action());
            ruleSet.Rules.Add(Rule_with_reference_method_action());
            ruleSet.Rules.Add(Rule_with_Cast_object_expression_in_then_action());
            ruleSet.Rules.Add(Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action());
            ruleSet.Rules.Add(Rule_with_list_initialization());
            ruleSet.Rules.Add(Rule_with_child_and_granchild_reference());
            ruleSet.Rules.Add(Rule_with_generic_object_initialization());
            ruleSet.Rules.Add(Rule_with_generic_list_initialization());
            ruleSet.Rules.Add(Rule_call_method_in_static_generic_class());
            ruleSet.Rules.Add(Rule_set_state_WITHOUT_update_action());
            ruleSet.Rules.Add(Rule_get_state_WITHOUT_AlwaysTrue_property_for_reevaluation());
            ruleSet.Rules.Add(Rule_set_state_WITH_update_action_set_targeting_AlwaysTrue_property());
            ruleSet.Rules.Add(Rule_get_state_WITH_AlwaysTrue_property_to_reevaluate_update_action());

            string? ruleSetString = SerializeRules(ruleSet) ?? throw new InvalidOperationException("Rule set serialization failed.");

            ruleSet = DeserializeRuleSet(ruleSetString) ?? throw new InvalidOperationException("Rule set deserialization failed.");

            RuleValidation ruleValidation = Helper.GetValidation(ruleSet, typeof(SampleFlow.FlowEntity));
            ruleEngine = new RuleEngine(ruleSet, ruleValidation);
        }

        private RuleSet ruleSet;
        private RuleEngine ruleEngine;

        private static string? SerializeRules(object drs)
        {
            System.Text.StringBuilder ruleDefinition = new();
            WorkflowMarkupSerializer serializer = new();
            using (System.IO.StringWriter stringWriter = new(ruleDefinition, System.Globalization.CultureInfo.InvariantCulture))
            {
                using (System.Xml.XmlTextWriter writer = new(stringWriter))
                {
                    serializer.Serialize(writer, drs);
                    writer.Flush();
                }
                stringWriter.Flush();
            }

            return UpdateStrongNames(ruleDefinition.ToString());
        }

        private static string? UpdateStrongNames(string ruleSetXml)
        {
            if (ruleSetXml == null) return null;

            ruleSetXml = NetCoreStrongNameRegex().Replace(ruleSetXml, AssemblyStrongNames.NETCORE);
            ruleSetXml = CodeDomNetCoreStongNameRegex().Replace(ruleSetXml, AssemblyStrongNames.CODEDOM_NETCORE);

            return ruleSetXml;
        }

        private static RuleSet? DeserializeRuleSet(string ruleSetXmlDefinition)
        {

            WorkflowMarkupSerializer serializer = new();
            if (!string.IsNullOrEmpty(ruleSetXmlDefinition))
            {
                using System.IO.StringReader stringReader = new(ruleSetXmlDefinition);
                using System.Xml.XmlTextReader reader = new(stringReader);
                return serializer.Deserialize(reader) as RuleSet;
            }
            else
            {
                return null;
            }
        }

        [GeneratedRegex(AssemblyStrongNames.NETCORE_MATCH)]
        private static partial Regex NetCoreStrongNameRegex();
        [GeneratedRegex(AssemblyStrongNames.CODEDOM_NETCORE_MATCH)]
        private static partial Regex CodeDomNetCoreStongNameRegex();
    }

    internal struct AssemblyStrongNames
    {
        internal const string NETCORE = "System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";
        internal const string CODEDOM_NETCORE = "System.CodeDom, Version=4.0.1.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51";

        internal const string NETCORE_MATCH = @"System.Private.CoreLib, Version=\d.\d.\d.\d, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";
        internal const string CODEDOM_NETCORE_MATCH = @"System.CodeDom, Version=\d.\d.\d.\d, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51";
    }
}
