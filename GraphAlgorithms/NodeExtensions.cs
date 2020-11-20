using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    public static class NodeExtensions
    {
        #region Алгоритмы поиска
        //Поиск в ширину
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
        //Поиск в глубину
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
        #endregion
        //Поиск компонент связности. Возвращает список компонент
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
        //Поиск кратчайшего пути в невзвешенном графе
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

        #region Алгоритмы топологической сортировки
        //Алгоритм Кана
        public static List<Node> KahnAlgorithm(this Graph graph)
        {
            var topSort = new List<Node>();
            var nodes = graph.Nodes.ToList();
            while(nodes.Count!=0)
            {
                //Ищем вершину с нулевой степенью захода.
                var nodeToDelete = nodes
                    .Where(node => !node.IncidentEdges.Any(edge => edge.Second == node))
                    .FirstOrDefault();

                //Если вершин с нулевой степенью захода не найдено, значит в графе есть циклы.
                //Если в графе есть циклы, то топологическая сортировка невозвожна. Возвращаем null
                if (nodeToDelete == null) return null;
                //Удаляем вершину из списка всех вершин (как бы отмечаем ее просмотренной)
                nodes.Remove(nodeToDelete);
                //Добавляем вершину в список вершин топологической сортировки
                topSort.Add(nodeToDelete);
                //Не забываем удалить все инцидентные ребра у этой вершины
                foreach (var edge in nodeToDelete.IncidentEdges.ToList())
                    graph.Delete(edge);
            }
            return topSort;
        }

        //Алгоритм Тарьяна
        private enum State
        {
            White,
            Gray,
            Black
        }
        public static List<Node> TarjanAlgorithm(this Graph graph)
        {
            var topSort = new List<Node>();
            var states = graph.Nodes.ToDictionary(node => node, node => State.White);
            while(true)
            {
                var nodeToSearch = states
                    .Where(p => p.Value == State.White)
                    .Select(p => p.Key)
                    .FirstOrDefault();
                if (nodeToSearch == null) break;

                if (!TarjanDepthSearch(nodeToSearch, states, topSort))
                    return null;
            }
            topSort.Reverse();
            return topSort;
        }
        //Рекурсивный метод, node - вершина, из которой запускается данный метод
        private static bool TarjanDepthSearch(Node node, Dictionary<Node, State> states, List<Node> topSort)
        {
            //Если зашли в серую вершину - значит нашли цикл, вернем false и вернем null в методе TarjanAlgorithm
            if (states[node] == State.Gray) return false;
            //Если зашли в черную вершину, значит мы здесь уже были и запускать этот метод повторно не нужно, вернем true
            if (states[node] == State.Black) return true;
            //Если  зашли в белую вершину, значит мы здесь еще не были, отметим ее серым цветом и запустим рекурсивный алгоритм
            states[node] = State.Gray;

            //Далее выбираем все вершины, в которые можно прийти из данной вершины (Учитываем стрелочки на ребрах)
            var outgoingNodes = node.IncidentEdges
                .Where(edge => edge.First == node)
                .Select(edge => edge.Second);
            //Из всех найденных вершин запускаем этот метод рекурсивно. Если в процессе мы нашли цикл, то вернем false
            foreach (var nextNode in outgoingNodes)
                if (!TarjanDepthSearch(nextNode, states, topSort)) return false;

            //Если все прошло удачно, то помечаем эту вершину черным цветом и добавляем ее в список топологической сортировки
            states[node] = State.Black;
            topSort.Add(node);
            return true;
        }
        #endregion
    }
}
