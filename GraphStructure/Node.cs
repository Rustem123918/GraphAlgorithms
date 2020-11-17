using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class Node
    {
        private readonly List<Node> incidentNodes = new List<Node>();
        public readonly int Number;
        public Node(int number)
        {
            Number = number;
        }
        public IEnumerable<Node> IncidentNodes
        {
            get
            {
                foreach (var node in incidentNodes)
                    yield return node;
            }
        }
        public void Connect(Node anotherNode)
        {
            incidentNodes.Add(anotherNode);
            anotherNode.incidentNodes.Add(this);
        }
    }
}
