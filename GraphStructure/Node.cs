using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class Node
    {
        private readonly List<Edge> incidentEdges = new List<Edge>();
        public readonly int Number;
        public Node(int number)
        {
            Number = number;
        }
        public IEnumerable<Node> IncidentNodes
        {
            get
            {
                return incidentEdges.Select(z => z.OtherNode(this));
            }
        }
        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var edge in incidentEdges)
                    yield return edge;
            }
        }
        public void Connect(Node anotherNode)
        {
            var edge = new Edge(this, anotherNode);
            incidentEdges.Add(edge);
            anotherNode.incidentEdges.Add(edge);
        }
        public void Disconnect(Edge edge)
        {
            edge.First.incidentEdges.Remove(edge);
            edge.Second.incidentEdges.Remove(edge);
        }
    }
}
