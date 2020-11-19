using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.MakeGraph(
               0, 1,
               1, 4,
               0, 2,
               2, 3,
               3, 4);

            foreach(var e in graph.FindTheShortestPath(graph[1], graph[3]))
            {
                Console.WriteLine(e);
            }
        }
    }
}
