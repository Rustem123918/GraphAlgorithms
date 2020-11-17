using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class Graph
    {
        private readonly Node[] nodes;
        public Graph(int nodesCount)
        {
            nodes = Enumerable
                .Range(0, nodesCount)
                .Select(i => new Node(i))
                .ToArray();
        }
        public Node this[int index]
        {
            get { return nodes[index]; }
        }
        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in nodes)
                    yield return node;
            }
        }
        public IEnumerable<Edge> Edges
        {
            get
            {
                return Nodes.SelectMany(x => x.IncidentEdges).Distinct();
            }
        }

        public void Connect(int v1, int v2)
        {
            nodes[v1].Connect(nodes[v2]);
        }
    }
}
