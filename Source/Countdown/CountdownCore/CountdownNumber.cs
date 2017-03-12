using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownCore
{
    abstract class CountdownNumber
    {
        public CountdownNumber(){}
        /// <summary>
        /// The value total of this number
        /// </summary>
        public int Value {get; protected set;}
        /// <summary>
        /// A binary encoding of a hash value that states what values have been used to make this number
        /// i.e. 00000001 would represent a number that had used only the first number
        /// 00000101 would represent a number that had been made from the first and third numbers
        /// </summary>
        public int NumbersHash { get; protected set;}

        /// <summary>
        /// Tests if the numbers can be combined i.e. the result won't use any duplicate numbers.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CanCombine(ref CountdownNumber other)
        {
            return (this.NumbersHash & other.NumbersHash) == 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>the combined hash of the two numbers</returns>
        public int CombineHash(ref CountdownNumber other)
        {
            return this.NumbersHash | other.NumbersHash;
        }
        
    }

    /// <summary>
    /// A single number that has not been combined with any other number
    /// </summary>
    class SingleCountdownNumber: CountdownNumber
    {
        SingleCountdownNumber(int value, byte numbersHash)
        {
            this.Value = value;
            this.NumbersHash = numbersHash;
        }

        public override String ToString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// A number that is a combination of two other numbers. 
    /// Produced by adding subtracting or multiplying 
    /// </summary>
    class ComboCountdownNumber : CountdownNumber
    {
        /// <summary>
        /// Operations that can be applied to two numbers
        /// </summary>
        public enum Operation
        {
            ADD, MULTIPLY, A_SUBTRACT_B, B_SUBTRACT_A          
        }

        ComboCountdownNumber(ref CountdownNumber parentA, ref CountdownNumber parentB, Operation operation)
        {
            this.parentA = parentA;
            this.parentB = parentB;
            this.operation = operation;
            this.Value = CalculateValue();
            this.NumbersHash = parentA.CombineHash(ref parentB);
        }


        protected CountdownNumber parentA;
        protected CountdownNumber parentB;
        protected Operation operation;

        /// <summary>
        /// Calculates the value of this number by applying operation to parents A and B
        /// </summary>
        /// <returns></returns>
        int CalculateValue()
        {
            switch(operation)
            {
                case Operation.ADD:
                    return parentA.Value + parentB.Value;
                case Operation.MULTIPLY:
                    return parentA.Value * parentB.Value;
                case Operation.A_SUBTRACT_B:
                    return parentA.Value - parentB.Value;
                case Operation.B_SUBTRACT_A:
                    return parentB.Value - parentA.Value;
                default:
                    return 0;
            }
        }



        public override String ToString()
        {
            //select the left and right values
            String left = "?";
            String right = "?";
            switch (operation)
            {
                case Operation.ADD:
                case Operation.MULTIPLY:
                case Operation.A_SUBTRACT_B:
                    left = parentA.ToString();
                    right = parentB.ToString();
                    break;
                case Operation.B_SUBTRACT_A:
                    left = parentB.ToString();
                    right = parentA.ToString();
                    break;
            }
            //select the operation
            String op = "?";
               switch(operation)
            {
                case Operation.ADD:
                    op = "+";
                    break;
                case Operation.MULTIPLY:
                       op = "x";
                    break;
                case Operation.A_SUBTRACT_B:
                case Operation.B_SUBTRACT_A:
                    op = "-";
                    break;
            }
            //return the full string
               return "(" + left + " " + op + " " + right + ")";
        
        }
        



        
    }
}
