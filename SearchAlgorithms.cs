using System.Collections.Generic;
using System.Drawing;

namespace GraphAlgorithms
{
    public static class SearchAlgorithms
    {
        //Recursion Depth-First Search
        public static void RecursionDFS(State[,] map, int x, int y)
        {
            if (x < 0 || x > map.GetLength(0) - 1 || y < 0 || y > map.GetLength(1) - 1) return;
            if (map[x, y] == State.Visited || map[x, y] == State.Wall) return;

            Program.Print(map);
            map[x, y] = State.Visited;
            var dx = new[] { -1, 0, 1, 0 };
            var dy = new[] { 0, -1, 0, 1 };
            for (int i = 0; i < dx.Length; i++)
                RecursionDFS(map, x + dx[i], y + dy[i]);
        }

        //Stack Depth-First Seacrh
        public static void StackDFS(State[,] map, int x, int y)
        {
            var stack = new Stack<Point>();
            stack.Push(new Point(x, y));
            while (stack.Count != 0)
            {
                var point = stack.Pop();
                if (point.X < 0 || point.X > map.GetLength(0) - 1 || point.Y < 0 || point.Y > map.GetLength(1) - 1) continue;
                if (map[point.X, point.Y] == State.Visited || map[point.X, point.Y] == State.Wall) continue;

                Program.Print(map);
                map[point.X, point.Y] = State.Visited;
                var dx = new[] { -1, 0, 1, 0 };
                var dy = new[] { 0, -1, 0, 1 };
                for (int i = 0; i < dx.Length; i++)
                    stack.Push(new Point(point.X + dx[i], point.Y + dy[i]));
            }
        }

        //Queue Breadth-First Search
        public static void QueueBFS(State[,] map, int x, int y)
        {
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(x, y));
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X > map.GetLength(0) - 1 || point.Y < 0 || point.Y > map.GetLength(1) - 1) continue;
                if (map[point.X, point.Y] == State.Visited || map[point.X, point.Y] == State.Wall) continue;

                Program.Print(map);
                map[point.X, point.Y] = State.Visited;
                var dx = new[] { -1, 0, 1, 0 };
                var dy = new[] { 0, -1, 0, 1 };
                for (int i = 0; i < dx.Length; i++)
                    queue.Enqueue(new Point(point.X + dx[i], point.Y + dy[i]));
            }
        }
    }
}
