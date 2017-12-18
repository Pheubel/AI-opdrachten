using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TilesOfMonkeyIsland.TileWorld;

namespace TilesOfMonkeyIsland.Searcher
{
    class ExperimentResults
    {
        /**
     * The following three variables contain information on results of the 
     * searches by the three different algorithms
     */
        private AlgorithmResults aStar;
        private AlgorithmResults dijkstra;
        private AlgorithmResults bfsSearch;

        
        /// <summary>
        /// Initializes the three algorithm results.
        /// </summary>
        public ExperimentResults()
        {
            aStar = new AlgorithmResults();
            dijkstra = new AlgorithmResults();
            bfsSearch = new AlgorithmResults();
        }

        public AlgorithmResults GetaStar()
        {
            return aStar;
        }

        public void SetaStar(AlgorithmResults aStar)
        {
            this.aStar = aStar;
        }

        public AlgorithmResults GetDijkstra()
        {
            return dijkstra;
        }

        public void SetDijkstra(AlgorithmResults dijkstra)
        {
            this.dijkstra = dijkstra;
        }

        public AlgorithmResults GetBFSSearch()
        {
            return bfsSearch;
        }

        public void SetBFSSearch(AlgorithmResults bfsSearch)
        {
            this.bfsSearch = bfsSearch;
        }
    }
}
