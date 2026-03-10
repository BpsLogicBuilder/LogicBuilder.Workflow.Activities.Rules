using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleFlow
{
    public class FlowEntity
    {
        public static string DEFAULTSTATE = "NY";//NOSONAR - used for testing dynamic invokation using CodeDom
        public static string BoolTestComparison = "false";//NOSONAR - used for testing dynamic invokation using CodeDom

        public bool AlwaysTrue { get { return true; } }//NOSONAR - used for testing dynamic invokation using CodeDom

        public string State { get; set; }
        public string BoolText { get; set; }
        public ChildEntity ChildEntity { get; set; } = new ChildEntity();

        public string[,] StringList { get; set; } = new string[2, 2];

        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public string ThirdValue { get; set; }
        public int FourthValue { get; set; }
        public int[] TheParams;//NOSONAR - used for testing dynamic invokation using CodeDom

        public Type TheType { get; set; }
        public Collection<object> MyCollection { get; set; }
        public List<string> MyList { get; set; } = new List<string> { "Apple", "Orange" };
        public string[] MyArray { get; set; }
        public GenericClass<string> GenericString { get; set; }
        public GenericClass<IList<decimal>> GenericListOfDecimal { get; set; }

        public FirstClass FirstClass { get; set; } = new FirstClass();
        public object DClass = new ChildEntity();//NOSONAR - used for testing dynamic invokation using CodeDom
        public object DClass2 { get; set; }


        private bool BoolMethod()//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            return BoolText == BoolTestComparison;
        }

        private IList<string> GetFirstValue()//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            return null;//NOSONAR - used for testing dynamic invokation using CodeDom
        }

        private void SetFirstValue(OtherEntity entity)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = entity.FirstValue;
        }

        private void SetCollection(Collection<object> obj)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            MyCollection = obj;
        }

        public static void SetDefaultState(string state)
        {
            DEFAULTSTATE = state;
        }

        private void SetGenericObject(GenericClass<string> obj)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            GenericString = obj;
        }

        private void SetGenericObject(GenericClass<IList<decimal>> obj)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            GenericListOfDecimal = obj;
        }

        private void SetValuesWithoutParams(string firstValue, string secondValue, string thirdValue = "", int fourthValue = 0)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = firstValue;
            this.SecondValue = secondValue;
            this.ThirdValue = thirdValue;
            this.FourthValue = fourthValue;
        }

        private void SetMoreValues(string firstValue, string secondValue)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = firstValue;
            this.SecondValue = secondValue;
        }

        private void SetValues(string firstValue, string secondValue, string thirdValue = "", int fourthValue = 0, params int[] theParams)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = firstValue;
            this.SecondValue = secondValue;
            this.ThirdValue = thirdValue;
            this.FourthValue = fourthValue;
            this.TheParams = theParams;
        }

        private void SetValues(OtherEntity otherEntity)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = otherEntity.FirstValue;
            this.SecondValue = otherEntity.SecondValue;
            this.ThirdValue = otherEntity.ThirdValue;
            this.FourthValue = otherEntity.FourthValue;
            this.TheParams = otherEntity.TheParams;
        }

        private void SetValues(YetAnotherEntity otherEntity)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = otherEntity.FirstValue;
            this.SecondValue = otherEntity.SecondValue;
        }

        private void SetValues(EntityWithoutParams otherEntity)//NOSONAR - used for testing dynamic invokation using CodeDom
        {
            this.FirstValue = otherEntity.FirstValue;
            this.SecondValue = otherEntity.SecondValue;
            this.ThirdValue = otherEntity.ThirdValue;
            this.FourthValue = otherEntity.FourthValue;
        }
    }
}
