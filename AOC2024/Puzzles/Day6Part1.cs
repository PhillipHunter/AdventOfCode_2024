using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day6Part1 : IAdventPuzzle
    {
        public string Name => "Day 6: Guard Gallivant Part 1";
        public string? Solution => "5409";
        public string? ExampleSolution => "41";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day6.txt";

        public PuzzleOutput GetOutput()
        {
            var stopwatch = Stopwatch.StartNew();

            #region Puzzle

            if (ExampleRun)
            {
                _filename =
                    $"{Path.GetFileNameWithoutExtension(_filename)}Ex{Path.GetExtension(_filename)}";
            }

            var currPuzzleFileLines = File.ReadAllLines(
                Path.Combine(
                    ConfigurationManager.AppSettings["PuzzleInputDirectory"]
                        ?? "../../../../Inputs/",
                    _filename
                )
            );

            var worldMap = new List<char[]>();
            foreach (var currPuzzleFileLine in currPuzzleFileLines)
            {
                worldMap.Add(currPuzzleFileLine.ToCharArray());
            }

            var visited = new HashSet<Point>();
            var pos = GetStartingPos(worldMap);
            var direction = Day6Part1Direction.Up;

            while (true)
            {
                visited.Add(pos);
                var nextDir = GetNextDirection(worldMap, pos, direction);

                if (nextDir == null)
                {
                    break;
                }

                var prospectiveNextPos = GetNextPosition(worldMap, pos, nextDir.Value);

                direction = nextDir.Value;
                pos = prospectiveNextPos.Value;
            }

            var result = visited.Count;

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static char? GetCharAt(List<char[]> map, Point point)
        {
            if (!InBounds(map, point))
                return null;

            return map[point.Y][point.X];
        }

        public static Point GetStartingPos(List<char[]> map)
        {
            var targetRow = map.FirstOrDefault(ca => ca.Contains('^'));

            var targetRowIndex = map.IndexOf(targetRow);
            var targetColIndex = Array.IndexOf(targetRow, '^');

            return new Point(targetColIndex, targetRowIndex);
        }

        public static Point? GetNextPosition(
            List<char[]> map,
            Point currPos,
            Day6Part1Direction direction
        )
        {
            Point nextPos = Point.Empty;
            switch (direction)
            {
                case Day6Part1Direction.Up:
                    nextPos = currPos + new Size(0, -1);
                    break;
                case Day6Part1Direction.Down:
                    nextPos = currPos + new Size(0, 1);
                    break;
                case Day6Part1Direction.Left:
                    nextPos = currPos + new Size(-1, 0);
                    break;
                case Day6Part1Direction.Right:
                    nextPos = currPos + new Size(1, 0);
                    break;
            }

            if (!InBounds(map, nextPos))
                return null;

            return nextPos;
        }

        public static Day6Part1Direction? GetNextDirection(
            List<char[]> map,
            Point currPos,
            Day6Part1Direction currDirection
        )
        {
            Point? nextPos = GetNextPosition(map, currPos, currDirection);

            if (nextPos == null)
            {
                return null;
            }

            var nextChar = GetCharAt(map, nextPos.Value);

            switch (nextChar)
            {
                case '#':
                    switch (currDirection)
                    {
                        case Day6Part1Direction.Up:
                            return Day6Part1Direction.Right;
                        case Day6Part1Direction.Down:
                            return Day6Part1Direction.Left;
                        case Day6Part1Direction.Left:
                            return Day6Part1Direction.Up;
                        case Day6Part1Direction.Right:
                            return Day6Part1Direction.Down;
                    }

                    break;
                default:
                    return currDirection;
            }

            throw new ApplicationException("Invalid direction");
        }

        public static bool InBounds(List<char[]> map, Point point)
        {
            var mapHeight = map.Count;
            var mapWidth = map[0].Length;

            return (point.Y <= mapHeight - 1)
                && (point.X <= mapWidth - 1)
                && (point.Y >= 0)
                && (point.X >= 0);
        }

        public enum Day6Part1Direction
        {
            Left,
            Down,
            Up,
            Right,
        }
    }
}
