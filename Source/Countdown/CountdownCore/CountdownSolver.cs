using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownCore
{
    abstract class CountdownSolverBase
    {

        CountdownSolverMethod method;
        CountdownSolverContainer container;
        CountdownPuzzle puzzle;
        
    }

    abstract class CountdownSolverMethod
    {
        
        public abstract void Initialise(CountdownSolverContainer container, CountdownPuzzle puzzle);

    }

    abstract class CountdownSolverContainer
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
    }
}
