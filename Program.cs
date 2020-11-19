using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.MakeGraph(
               0, 0,
               1, 2,
               3, 4,
               4, 5,
               3, 5);

            foreach(var compoment in graph.FindConnectedComponents())
            {
                foreach (var node in compoment)
                    Console.Write(node.Number + " ");
                Console.WriteLine();
            }
        }
    }
}
