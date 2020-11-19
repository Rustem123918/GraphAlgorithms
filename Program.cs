using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            DoKahnAlgorithm();
        }
        static void DoFindTheShortestPathAlgorithm()
        {
            var graph = Graph.MakeGraph(
               0, 1,
               1, 4,
               0, 2,
               2, 3,
               3, 4);

            foreach (var e in graph.FindTheShortestPath(graph[1], graph[3]))
            {
                Console.WriteLine(e);
            }
        }
        static void DoKahnAlgorithm()
        {
            var graph = Graph.MakeGraph(
               0, 1,
               1, 2,
               1, 3,
               3, 2,
               3, 4,
               2, 4);

            foreach (var e in graph.KahnAlgorithm())
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();

            graph = Graph.MakeGraph(
               0, 1,
               0, 2,
               1, 2,
               1, 3,
               2, 3,
               2, 4,
               3, 4);
            foreach (var e in graph.KahnAlgorithm())
            {
                Console.WriteLine(e);
            }
        }
    }
}
