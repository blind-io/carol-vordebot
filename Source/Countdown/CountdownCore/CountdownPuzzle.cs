using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownCore
{
    class CountdownPuzzle
    {
        public CountdownPuzzle(int solution, List<int> numbers)
        {
            this.solution = solution;
            this.numbers = numbers;
            this.size = numbers.Count;
        }

        private List<int> numbers {  get; set;}
        public int solution { get; private set; }
        public int size { get; private set; }


        public int GetNumber(int index)
        {
            return numbers[index];
        }
    }
}
