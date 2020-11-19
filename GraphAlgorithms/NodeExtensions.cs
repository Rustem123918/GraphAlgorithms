using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    public static class NodeExtensions
    {
        public static IEnumerable<Node> BreadthSearch(this Node startNode)
        {
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            visited.Add(startNode);
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                yield return node;
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
        }
        public static IEnumerable<Node> DepthSearch(this Node startNode)
        {
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var nextNode in node.IncidentNodes)
                    stack.Push(nextNode);
            }
        }
        public static IEnumerable<IEnumerable<Node>> FindConnectedComponents(this Graph graph)
        {
            var visited = new HashSet<Node>();
            while(true)
            {
                var node = graph.Nodes.Where(n => !visited.Contains(n)).FirstOrDefault();
                if (node == null) break;
                var connectedComponent = node.BreadthSearch().ToList();
                foreach (var visitedNode in connectedComponent)
                    visited.Add(visitedNode);
                yield return connectedComponent;
            }
        }
        public static IEnumerable<Node> FindTheShortestPath(this Graph graph, Node start, Node end)
        {
            var track = new Dictionary<Node, Node>();
            track[start] = null;
            //visited - вершины, которы мы уже раскрыли. Данная структура данных ялвяется лишней.
            //Здесь будет достаточно только словаря track и проверки track.ContainsKey(nextNode).
            //Однако visited полезна для понимания алгоритма.
            var visited = new HashSet<Node>();
            //queue - вершины, которые мы будем раскрываем
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                visited.Add(node);
                bool endIsFound = false;
                foreach (var nextNode in node.IncidentNodes)
                {
                    if (visited.Contains(nextNode)) continue;
                    track[nextNode] = node;
                    queue.Enqueue(nextNode);
                    if (nextNode == end)
                        endIsFound = true;
                }
                if (endIsFound) break;
            }
            if (!track.ContainsKey(end)) return null;
            var list = new List<Node>();
            while(end!=null)
            {
                list.Add(end);
                end = track[end];
            }
            list.Reverse();
            return list;
        }
    }
}
