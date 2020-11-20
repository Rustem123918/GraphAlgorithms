using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Find connected components algorithm");
            DoFindConnectedComponentsAlgorithm();

            Console.WriteLine("Find the shortest path algorithm");
            DoFindTheShortestPathAlgorithm();

            Console.WriteLine("Kahn algorithm");
            DoKahnAlgorithm();

            Console.WriteLine("Tarjan algorithm");
            DoTarjanAlgorithm();
        }
        static void DoFindConnectedComponentsAlgorithm()
        {
            var graph = Graph.MakeGraph(
                0, 0,
                1, 2,
                3, 4,
                4, 5,
                5, 3);

            foreach(var connectedComponent in graph.FindConnectedComponents())
            {
                foreach (var node in connectedComponent)
                    Console.Write(node + " ");
                Console.WriteLine();
            }
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
        static void DoTarjanAlgorithm()
        {
            var graph = Graph.MakeGraph(
               0, 1,
               1, 2,
               1, 3,
               3, 2,
               3, 4,
               2, 4);

            foreach (var e in graph.TarjanAlgorithm())
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

            foreach (var e in graph.TarjanAlgorithm())
            {
                Console.WriteLine(e);
            }
        }
    }
}
