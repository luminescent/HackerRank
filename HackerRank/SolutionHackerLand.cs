using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace HackerRank
{

    class SolutionHackerLand
    {

        // Strategy: find connected components. 
        // If clib <= croad => return clib*noOfCities
        // Else compute minimum spanning tree => return noOfEdges*croad
        // Graph: array of sets 

        public class HackerLandBuilder
        {
            private long _noCities;
            private long _libraryCost;
            private long _roadCost;
            private HashSet<long>[] _roads;

            public HackerLandBuilder WithNoCities(long noCities)
            {
                _noCities = noCities;
                _roads = Enumerable.Range(0, (int)_noCities)
                    .Select(e => new HashSet<long>())
                    .ToArray();
                return this;
            }
            public HackerLandBuilder WithLibraryCost(long libraryCost)
            {
                _libraryCost = libraryCost;
                return this;
            }
            public HackerLandBuilder WithRoadCost(long roadCost)
            {
                _roadCost = roadCost;
                return this;
            }
            public HackerLandBuilder AddRoad(long vertex1, long vertex2)
            {
                // add validation here 
                // the input values are numbered from 1 
                _roads[vertex1 - 1].Add(vertex2 - 1);
                _roads[vertex2 - 1].Add(vertex1 - 1);

                return this;
            }

            public HackerLand Build()
            {
                var hackerLand = new HackerLand(_noCities, _roadCost, _libraryCost, _roads);
                return hackerLand;
            }

        }

        public class HackerLand
        {
            private Graph _innerGraph;
            private long _libraryCost;
            private long _roadCost;
            private long _noCities;

            public HackerLand(long noCities, long roadCost, long libraryCost, HashSet<long>[] roads)
            {
                _innerGraph = new Graph(noCities, roadCost, roads);
                _noCities = noCities;
                _libraryCost = libraryCost;
                _roadCost = roadCost;
            }

            public long GetMinLibraryAccessCost()
            {

                if (_libraryCost <= _roadCost)
                    return _noCities * _libraryCost;

                // compute connected components 
                // get minimum spanning trees cost - this is always count of vertices - 1 because this is a undirected graph
                // if a city is isolated, then its cost is _libraryCost
                // return sum of min spanning tree 

                var connectedComponents = GraphUtilities.GetConnectedComponents(_innerGraph);
                return connectedComponents
                    .Select(c => (c.Count == 1) ? _libraryCost : _libraryCost + (c.Count - 1) * _roadCost)
                    .Sum();
            }
        }


        /// <summary>
        /// Graph were all edges are bidirectional and have the same cost 
        /// </summary>
        public class Graph
        {
            public long Vertices { get; private set; }
            public HashSet<long>[] Edges { get; private set; }
            public long EdgeCost { get; private set; }
            public Graph(long vertices, long edgeCost, HashSet<long>[] edges)
            {
                Vertices = vertices;
                EdgeCost = edgeCost;
                Edges = edges;
            }
        }

        public class GraphUtilities
        {
            public static HashSet<HashSet<long>> GetConnectedComponents(Graph graph)
            {
                var result = new HashSet<HashSet<long>>();

                var chosen = Enumerable.Range(0, (int) graph.Vertices)
                    .Select(i => false)
                    .ToArray();

                foreach (var node in Enumerable.Range(0, (int) graph.Vertices))
                {
                    if (!chosen[node])
                    {
                        var connectedComponent = GetConnectedComponentForNode(node, graph.Edges, new HashSet<long>());
                        foreach (var v in connectedComponent)
                        {
                            chosen[v] = true;
                        }
                        result.Add(connectedComponent);
                    }
                }

                return result;
            }

            private static HashSet<long> GetConnectedComponentForNode(long node, HashSet<long>[] edges, HashSet<long> connectedComponent)
            {
                if (connectedComponent.Contains(node))
                    return connectedComponent;
                else
                {
                    connectedComponent.Add(node);
                    foreach (var connectedNode in edges[node])
                    {
                        if (!connectedComponent.Contains(connectedNode))
                            connectedComponent.UnionWith(GetConnectedComponentForNode(connectedNode, edges, connectedComponent));
                    }

                    return connectedComponent;
                }
            }

        }

        static void Execute(String[] args)
        {
            long q = Convert.ToInt32(Console.ReadLine());

            for (long a0 = 0; a0 < q; a0++)
            {

                string[] tokens_n = Console.ReadLine().Split(' ');
                long n = Convert.ToInt32(tokens_n[0]);
                long m = Convert.ToInt32(tokens_n[1]);
                long x = Convert.ToInt32(tokens_n[2]);
                long y = Convert.ToInt32(tokens_n[3]);

                var hackerLandBuilder =
                    new HackerLandBuilder()
                    .WithNoCities(n)
                    .WithLibraryCost(x)
                    .WithRoadCost(y);

                for (long a1 = 0; a1 < m; a1++)
                {
                    string[] tokens_city_1 = Console.ReadLine().Split(' ');
                    long city_1 = Convert.ToInt32(tokens_city_1[0]);
                    long city_2 = Convert.ToInt32(tokens_city_1[1]);

                    hackerLandBuilder.AddRoad(city_1, city_2);
                }

                var hackerLand = hackerLandBuilder.Build();
                var cost = hackerLand.GetMinLibraryAccessCost();

                Console.WriteLine(cost);
            }
        }
    }

}