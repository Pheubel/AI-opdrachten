using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using TilesOfMonkeyIsland.TileWorld;
using TilesOfMonkeyIsland.Algorithm;

namespace TilesOfMonkeyIsland.Searcher
{
    class Searcher
    {
        /// <summary>
        /// Runs different pathfinders on the given maps.
        /// </summary>
        /// <param name="_amountMapImages">The amount of maps in the assembly</param>
        public static void Start(int _amountMapImages)
        {
            String filename;

            eTileType[,] map;
            // search all files
            for (int fileNr = 1; fileNr <= _amountMapImages; fileNr++)
            {
                // determine file to search
                filename = "i" + fileNr;
                Image rawmap;
                try
                {
                    rawmap = Image.FromFile(@"..\..\" + filename + ".png");
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("FILE NOT FOUND!");
                    Console.WriteLine(ex);
                    System.Threading.Thread.Sleep(5000);
                    return;
                }

                Bitmap bm = (Bitmap)rawmap;
                map = new eTileType[rawmap.Width, rawmap.Height];
                for (int i = 0; i < rawmap.Width; i++)
                {
                    for (int i2 = 0; i2 < rawmap.Height; i2++)
                    {
                        Color pixel = bm.GetPixel(i, i2);
                        eTileType pixelType = eTileType.UNKNOWN;
                        switch (pixel.Name)
                        {
                            case "ffffff00"://yellow
                                pixelType = eTileType.SAND;
                                break;
                            case "ff000000"://black
                                pixelType = eTileType.NONWALKABLE;
                                break;
                            case "ffffffff"://white
                                pixelType = eTileType.ROAD;
                                break;
                            case "ffff0000"://red
                                pixelType = eTileType.START;
                                break;
                            case "ff00ff00"://green
                                pixelType = eTileType.END;
                                break;
                            case "ff0000ff"://blue
                                pixelType = eTileType.WATER;
                                break;
                            case "ff808080"://gray
                                pixelType = eTileType.MOUNTAIN;
                                break;
                            case "ff00ffff"://cyan
                                pixelType = eTileType.PATH;
                                break;
                        }
                        map[i, i2] = pixelType;
                    }
                }


                // search tile world
                ExperimentResults info = Search(map, filename);

                // print the results to System.out
                PrintAllResults(filename, info);
            }
            Console.WriteLine("Press Enter to close the application.");
            Console.ReadLine();
        }


        /// <summary>
        /// Searches a file for the best path using three algorithms.
        /// The solutions can be shown on screen.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="filename">The file (without extension) to be searched.</param>
        /// <returns> All relevant statistics for the three experiments.</returns>
        public static ExperimentResults Search(eTileType[,] map, String filename)
        {
            ExperimentResults info = new ExperimentResults();

            // Dijkstra
            Dijkstra dijkstraAlgorithm = new Algorithm.Dijkstra(new TileWorld.TileWorld(map));
            AlgorithmResults dijkstra = dijkstraAlgorithm.run();

            info.setDijkstra(dijkstra);
            SaveImage(filename + "_dijkstra", dijkstraAlgorithm.getMap());

            // A*
            AStar aStarAlgorithm = new AStar(new TileWorld.TileWorld(map));
            AlgorithmResults aStar = aStarAlgorithm.run();

            info.setaStar(aStar);
            SaveImage(filename + "_aStar", aStarAlgorithm.getMap());

            // BFS Search
            BFS bfsAlgorithm = new BFS(new TileWorld.TileWorld(map));
            AlgorithmResults bfsSearch = bfsAlgorithm.run();

            info.setBFSSearch(bfsSearch);
            SaveImage(filename + "_bfs", bfsAlgorithm.getMap());

            return info;
        }

        /// <summary>
        /// Prints the results for all three algorithms on System.out.
        /// </summary>
        /// <param name="filename"> Filename containing the world that has been searched.</param>
        /// <param name="info"> Results of the experiments for all three algorithms for that file.</param>
        public static void PrintAllResults(String filename, ExperimentResults info)
        {
            Console.WriteLine("#######################");
            Console.WriteLine("Testcase: " + filename);
            Console.WriteLine("#######################");
            PrintAlgorithmResult("A*", info.getaStar());
            Console.WriteLine("-------------------------------------");
            PrintAlgorithmResult("Dijkstra", info.getDijkstra());
            Console.WriteLine("-------------------------------------");
            PrintAlgorithmResult("BFS", info.getBFSSearch());
        }


        /// <summary>
        /// Saves an image of the solved version of the map
        /// </summary>
        /// <param name="originalFileName"> The name of the original map image</param>
        /// <param name="map"> The tilemap</param>
        public static void SaveImage(String originalFileName, eTileType[,] map)
        {
            String newFileName = originalFileName + "_solution";
            String saveLocation = @"..\..\" + newFileName + ".png";
            Bitmap bm = new Bitmap(map.GetUpperBound(0) + 1, map.GetUpperBound(1) + 1);
            for (int i = 0; i < map.GetUpperBound(0) + 1; i++)
            {
                for (int i2 = 0; i2 < map.GetUpperBound(1) + 1; i2++)
                {
                    Color color;
                    switch (map[i, i2])
                    {
                        case eTileType.SAND://yellow
                            color = Color.FromArgb(255, 255, 0);
                            break;
                        case eTileType.NONWALKABLE://black
                            color = Color.FromArgb(0, 0, 0);
                            break;
                        case eTileType.ROAD://white
                            color = Color.FromArgb(255, 255, 255);
                            break;
                        case eTileType.START://red
                            color = Color.FromArgb(255, 0, 0);
                            break;
                        case eTileType.END://green
                            color = Color.FromArgb(0, 255, 0);
                            break;
                        case eTileType.WATER://blue
                            color = Color.FromArgb(0, 0, 255);
                            break;
                        case eTileType.MOUNTAIN://gray
                            color = Color.FromArgb(128, 128, 128);
                            break;
                        case eTileType.PATH://cyan
                            color = Color.FromArgb(0, 255, 255);
                            break;
                        default://unknown = purple
                            color = Color.FromArgb(255, 0, 255);
                            break;
                    }
                    bm.SetPixel(i, i2, color);
                }
            }
            bm.Save(saveLocation);
        }

        /// <summary>
        /// Prints the results of one algorithm on System.out.
        /// </summary>
        /// <param name="algorithmString">Name of the algorithm used (A*, Dijkstra, ...).</param>
        /// <param name="info">Results of the algorithm.</param>
        private static void PrintAlgorithmResult(String algorithmString, AlgorithmResults info)
        {
            Console.WriteLine(algorithmString);
            if (info == null)
            {
                Console.WriteLine("No results found.");
                return;
            }
            Console.WriteLine("#nodes: " + info.getNodesExpanded());
            Console.WriteLine("#path cost: " + info.getBestPathCost());
        }
    }
}
