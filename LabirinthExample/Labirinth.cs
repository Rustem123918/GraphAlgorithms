using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    public class Labirinth
    {
        public static string[] labyrinth = new string[]
        {
        " X   X    ",
        " X XXXXX X",
        "      X   ",
        "XXXX XXX X",
        "         X",
        " XXX XXXXX",
        " X        ",
        };

        public static void LabirinthRun(Action<State[,], int, int> searchAlgorithm)
        {
            var map = new State[labyrinth[0].Length, labyrinth.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = labyrinth[y][x] == ' ' ? State.Empty : State.Wall;
            searchAlgorithm(map, 0, 0);
            Print(map);
        }

        public static void Print(State[,] map)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int x = 0; x < map.GetLength(0) + 2; x++)
                Console.Write("X");
            Console.WriteLine();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Console.Write("X");
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    switch (map[x, y])
                    {
                        case State.Empty: Console.Write(" "); break;
                        case State.Visited: Console.Write("."); break;
                        case State.Wall: Console.Write("X"); break;
                    }
                }
                Console.WriteLine("X");
            }
            for (int x = 0; x < map.GetLength(0) + 2; x++)
                Console.Write("X");
            Console.ReadKey();
        }
    }
}
