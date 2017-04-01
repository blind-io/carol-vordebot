using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownCore
{
    abstract class CountdownSolver_Base
    {

        CountdownSolverMethod_Base method;
        CountdownSolverContainer_Base container;
        CountdownPuzzle puzzle;
        
    }

    public abstract class CountdownSolverMethod_Base
    {
        
        public abstract void Initialise(CountdownSolverContainer_Base container, CountdownPuzzle puzzle);

        public abstract void Solve();

    }

    public abstract class CountdownSolverContainer_Base
    {


        /// <summary>
        /// Initialises the container to the 'first' iteration by populating the container with the intitial numbers
        /// in the puzzle
        /// </summary>
        /// <param name="puzzle"></param>
        public abstract void Initialise(CountdownPuzzle puzzle);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>hashes that have been computed so far</returns>
        public abstract List<int> GetHashes();
        /// <summary>
        /// Returns a list of countdownNumber corresponding to the supplied hash.
        /// </summary>
        /// <param name="hash">hash representing the combination of numbers used to make the puzzle</param>
        /// <returns>List of CountdownNumber</returns>
        public abstract List<CountdownNumber> GetNumbers(int hash);

        public abstract void AppendNumbers(int hash, List<CountdownNumber> numbers);
    }


    class CountdownSolverContainer_Dictionary: CountdownSolverContainer_Base
    {
        private Dictionary<int, List<CountdownNumber>> map = new Dictionary<int, List<CountdownNumber>>();
        
        public override void Initialise(CountdownPuzzle puzzle)
        {
            map.Clear();
            var numbers = puzzle.GetCountdownNumbers();
            foreach(var number in numbers)
            {
                //add each number as a list.
                map.Add(number.GetHashCode(), new List<CountdownNumber> {number});
            }
        }

        public override List<int> GetHashes()
        {
            throw new NotImplementedException();
        }

        public override List<CountdownNumber> GetNumbers(int hash)
        {
            return map[hash];
        }

        public override void AppendNumbers(int hash, List<CountdownNumber> numbers)
        {
            if(!map.ContainsKey(hash))
            {
                //insert a new list
                map.Add(hash, numbers);
            }
            else
            {
                //append the numbers
                GetNumbers(hash).AddRange(numbers);
            }
            
        }

    }

    public class CountDownSolverMethod_Simple: CountdownSolverMethod_Base
    {
        public override void Initialise(CountdownSolverContainer_Base container, CountdownPuzzle puzzle)
        {
            throw new NotImplementedException();
        }

        public override void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
