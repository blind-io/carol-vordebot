using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownCore
{
    public class CountdownPuzzle
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

        public List<CountdownNumber> GetCountdownNumbers()
        {
            var cn_list = new List<CountdownNumber>();
            byte hash = 1;
            foreach(int number in numbers)
            {
                var cn = new SingleCountdownNumber(number, hash);
                cn_list.Add(cn);
                hash <<= 1; //bitshift increment the hash
            }
            return cn_list;
        }
    }
}
