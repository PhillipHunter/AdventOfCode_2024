using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day8Part2 : IAdventPuzzle
    {
        public string Name => "Day 8: Resonant Collinearity Part 2";
        public string? Solution => "934";
        public string? ExampleSolution => "34";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day8.txt";

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

            var worldMap = currPuzzleFileLines
                .Select(currPuzzleFileLine => currPuzzleFileLine.ToCharArray())
                .ToList();

            var uniqueFrequencies = GetFrequencies(worldMap);

            var allAntipodePoints = new HashSet<Point>();

            var worldMapVis = JsonConvert.DeserializeObject<List<char[]>>(
                JsonConvert.SerializeObject(worldMap)
            );

            foreach (var frequency in uniqueFrequencies)
            {
                var antipodes = FindAntipodes(worldMap, frequency);
                foreach (var antipode in antipodes)
                {
                    allAntipodePoints.Add(antipode);
                    worldMapVis[antipode.Y][antipode.X] = '#';
                }
            }

            var result = allAntipodePoints.Count;

            AOC.Log(GetMapVisualization(worldMapVis));

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static HashSet<char> GetFrequencies(List<char[]> map)
        {
            var result = new HashSet<char>();
            foreach (var currRow in map)
            {
                foreach (var currCell in currRow)
                {
                    if (currCell != '.')
                    {
                        result.Add(currCell);
                    }
                }
            }

            return result;
        }

        public static List<Point> FindAntipodes(List<char[]> map, char character)
        {
            var result = new List<Point>();
            var pointsForChar = GetPointsForChar(map, character);

            foreach (var currPoint in pointsForChar)
            {
                var otherPointsForChar = pointsForChar.Where(p => p != currPoint);
                foreach (var otherPoint in otherPointsForChar)
                {
                    var slope = GetSlope(currPoint, otherPoint);

                    var antipodeIndex = 0;
                    while (true)
                    {
                        var antipodePoint =
                            otherPoint
                            + new Size(slope.run * antipodeIndex, slope.rise * antipodeIndex);

                        if (!InBounds(map, antipodePoint))
                            break;

                        result.Add(antipodePoint);

                        antipodeIndex++;
                    }
                }
            }

            return result;
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

        public static (int run, int rise) GetSlope(Point a, Point b)
        {
            return (b.X - a.X, b.Y - a.Y);
        }

        public static List<Point> GetPointsForChar(List<char[]> map, char character)
        {
            var result = new List<Point>();

            for (var rowIndex = 0; rowIndex < map.Count; rowIndex++)
            {
                for (var colIndex = 0; colIndex < map[rowIndex].Length; colIndex++)
                {
                    if (map[rowIndex][colIndex] == character)
                    {
                        result.Add(new Point(colIndex, rowIndex));
                    }
                }
            }

            return result;
        }

        public static string GetMapVisualization(List<char[]> map)
        {
            var stringBuilder = new StringBuilder();
            foreach (var currRow in map)
            {
                stringBuilder.AppendLine(string.Join(string.Empty, currRow));
            }

            return stringBuilder.ToString();
        }
    }
}
