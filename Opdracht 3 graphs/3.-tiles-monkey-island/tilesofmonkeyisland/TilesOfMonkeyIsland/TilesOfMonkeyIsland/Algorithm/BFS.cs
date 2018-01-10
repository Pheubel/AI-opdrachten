﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TilesOfMonkeyIsland.TileWorld;

namespace TilesOfMonkeyIsland.Algorithm
{
    class BFS : Algorithm
    {
        public BFS(TileWorld.TileWorld world)
            : base(world)
        { }

        override protected float CalculateHeuristic(Node node)
        {
            // Calculate the minimal distance walking horizontally / vertically and diagonally.
            float distanceX = Math.Abs(node.x - this.goalNode.x);
            float distanceY = Math.Abs(node.y - this.goalNode.y);
            float distance;

            distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            

            // Return the heuristic.
            return distance;
        }
    }
}
