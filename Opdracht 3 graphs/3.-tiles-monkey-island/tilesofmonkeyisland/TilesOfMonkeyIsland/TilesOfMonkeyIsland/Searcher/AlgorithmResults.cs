using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilesOfMonkeyIsland.Searcher
{
    class AlgorithmResults
    {
        private int bestPathCost;
        private int nodesExpanded;
        /**
         * Integer list containing the indices of the tiles in the best path.
         */
        private ArrayList solutionPath = new ArrayList();

        public AlgorithmResults()
        {
            this.bestPathCost = 0;
            this.nodesExpanded = 0;
        }


        /// <summary>
        /// This constructor immediately initializes the class attributes.
        /// </summary>
        /// <param name="bestPathCost">The cost of the best path</param>
        /// <param name="nodesExpanded">Number of expanded nodes</param>
        public AlgorithmResults(int bestPathCost, int nodesExpanded)
        {
            this.bestPathCost = bestPathCost;
            this.nodesExpanded = nodesExpanded;
        }

        public int GetBestPathCost()
        {
            return bestPathCost;
        }

        public void SetBestPathCost(int cost)
        {
            this.bestPathCost = cost;
        }

        public int GetNodesExpanded()
        {
            return nodesExpanded;
        }

        public void SetNodesExpanded(int nodesExpanded)
        {
            this.nodesExpanded = nodesExpanded;
        }

        public ArrayList GetSolutionPath()
        {
            return solutionPath;
        }

        public void SetSolutionPath(ArrayList solutionPath)
        {
            this.solutionPath = solutionPath;
        }
    }
}
