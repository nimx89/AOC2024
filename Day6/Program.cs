using System;
using System.Collections.Generic;
using System.IO;

namespace Day6
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public int X { get; }
        public int Y { get; }

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) =>
            new Vector2D(a.X + b.X, a.Y + b.Y);

        public override bool Equals(object? obj) =>
            obj is Vector2D other && Equals(other);

        public bool Equals(Vector2D other) =>
            X == other.X && Y == other.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);

        public static bool operator !=(Vector2D left, Vector2D right) => !(left == right);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File '{filename}' does not exist in the current directory.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            var grid = new Dictionary<Vector2D, char>();

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    grid[new Vector2D(x, y)] = line[x];
                }
            }


            Vector2D pos = default;
            Vector2D direction = default;

            foreach (var kvp in grid)
            {
                if ("<>v^".Contains(kvp.Value))
                {
                    pos = kvp.Key;
                    direction = kvp.Value switch
                    {
                        '>' => new Vector2D(1, 0),
                        'v' => new Vector2D(0, 1),
                        '<' => new Vector2D(-1, 0),
                        '^' => new Vector2D(0, -1),
                        _ => throw new InvalidOperationException("Invalid direction"),
                    };
                    break;
                }
            }

            // Pathfinding
            var paths = new HashSet<Vector2D>();
            var cache = new HashSet<(Vector2D, Vector2D)>();
            var obstacles = new HashSet<Vector2D>();

            while (grid.ContainsKey(pos))
            {
                paths.Add(pos);
                cache.Add((pos, direction));

                if (grid.TryGetValue(pos + direction, out char nextChar) && nextChar == '#')
                {
                    direction = new Vector2D(-direction.Y, direction.X);
                }
                else
                {
                    Vector2D obs = pos + direction;

                    if (grid.ContainsKey(obs) && !paths.Contains(obs))
                    {
                        var n_pos = pos;
                        var n_dir = new Vector2D(-direction.Y, direction.X);
                        var n_temp = new HashSet<(Vector2D, Vector2D)>(cache);

                        while (grid.ContainsKey(n_pos))
                        {
                            n_temp.Add((n_pos, n_dir));

                            if (grid.TryGetValue(n_pos + n_dir, out char charAhead) && (charAhead == '#' || (n_pos + n_dir) == obs))
                            {
                                n_dir = new Vector2D(-n_dir.Y, n_dir.X);
                            }
                            else
                            {
                                n_pos += n_dir;
                            }

                            if (n_temp.Contains((n_pos, n_dir)))
                            {
                                obstacles.Add(obs);
                                break;
                            }
                        }
                    }

                    pos += direction;
                }
            }


            Console.WriteLine("Part 1:"+paths.Count);
            Console.WriteLine("Part 2:"+obstacles.Count);
        }
    }
}
