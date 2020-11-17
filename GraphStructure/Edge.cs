using System;

namespace GraphAlgorithms
{
    public class Edge
    {
        public readonly Node First;
        public readonly Node Second;
        public Edge(Node first, Node second)
        {
            First = first;
            Second = second;
        }
        public bool IsIncident(Node node)
        {
            return First == node || Second == node;
        }
        public Node OtherNode(Node node)
        {
            if (node == First) return Second;
            else if (node == Second) return First;
            else throw new ArgumentException();
        }
    }
}
