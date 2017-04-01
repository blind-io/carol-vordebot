using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CountdownCore
{
    abstract class CountdownSolver_Base
    {

        CountdownSolverMethod_Base method;
        CountdownPuzzle puzzle;
        
    }

    public abstract class CountdownSolverMethod_Base
    {
        protected CountdownSolverContainer_Base container;
        protected CountdownPuzzle puzzle;
        protected List<CountdownNumber> solutions;

        public CountdownSolverMethod_Base(CountdownSolverContainer_Base container)
        {
            this.container = container;
        }


        public virtual void Solve(CountdownPuzzle puzzle)
        {
            this.puzzle = puzzle;
            this.container.Initialise(puzzle);
            solutions.Clear();
            SolveImplementation();
        }

        protected abstract void SolveImplementation();

        protected bool CheckAddSolution(CountdownNumber number)
        {
            bool match = number.Value == puzzle.solution;
            if(match)
            {
                solutions.Add(number);
            }
            return match;
        }

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

        public CountDownSolverMethod_Simple(CountdownSolverContainer_Base container)
            : base(container)
        { }
        

        protected override void SolveImplementation()
        {
            int size = this.puzzle.size;
            for(int i=1; i<size; i++) //stop when we know we have tried all combinations.
            {
                CombineAll(); //calling this multiple times will generate a lot of duplicate solutions
            }
            

        }

        /// <summary>
        /// Tries every group against every other group
        /// </summary>
        private void CombineAll()
        {
            var hashes = container.GetHashes(); //do I need to copy?
            var count = hashes.Count;
            for(int first=0; first<count; first++)
            {
                for(int second =first+1; second<count; second++)
                {
                    
                    if (CountdownNumber.CanCombine(hashes[first], hashes[second]))
                    {
                        CombineTwoSets(hashes[first], hashes[second]);
                    }
                }
            }
        }

        private void CombineTwoSets(int firstHash, int secondHash)
        {
            Debug.Assert(CountdownNumber.CanCombine(firstHash, secondHash));
            var firstList = container.GetNumbers(firstHash);
            var secondList = container.GetNumbers(secondHash);
            
            //pre-allocate results list
            var results = new List<CountdownNumber>(3* firstList.Count * secondList.Count); //3 is the magic number (of operations)
            foreach(var n1 in firstList)
            {
                foreach(var n2 in secondList)
                {
                    foreach(var op in  Enum.GetValues(typeof(ComboCountdownNumber.Operation)).Cast<ComboCountdownNumber.Operation>())
                    {
                        results.Add(new ComboCountdownNumber( n1,   n2, op));                    }
                }
            }
        }

        private void PopulateSolution()
        {
            foreach(var hash in container.GetHashes())
            {
                var numbers = container.GetNumbers(hash);
                foreach(var number in numbers)
                {
                    if(number.Value == puzzle.solution)
                    {
                        this.solutions.Add(number);
                    }
                }
            }
        }
    }
}
