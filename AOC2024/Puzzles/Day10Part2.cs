using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day10Part2 : IAdventPuzzle
    {
        public string Name => "Day 10: Hoof It Part 2";
        public string? Solution => "1801";
        public string? ExampleSolution => "81";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day10.txt";

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
                .Select(currPuzzleFileLine =>
                    currPuzzleFileLine.ToCharArray().Select(a => int.Parse(a.ToString())).ToArray()
                )
                .ToList();

            var agents = new List<Day10Part2_Agent>();

            var startingPoints = GetAllPointsOfValue(worldMap, 9);

            foreach (var startingPoint in startingPoints)
            {
                agents.Add(new Day10Part2_Agent(startingPoint, startingPoint));
            }

            var scores = new Dictionary<Point, int>();
            var rating = new Dictionary<Point, int>();
            var originToTrailheadsFound = new HashSet<(Point, Point)>();
            while (agents.Count > 0)
            {
                foreach (var agent in new List<Day10Part2_Agent>(agents))
                {
                    if (GetHeightAt(worldMap, agent.CurrentPosition) == 0)
                    {
                        if (
                            !originToTrailheadsFound.Contains(
                                (agent.OriginPosition, agent.CurrentPosition)
                            )
                        )
                        {
                            if (!scores.TryAdd(agent.CurrentPosition, 1))
                            {
                                scores[agent.CurrentPosition]++;
                            }

                            originToTrailheadsFound.Add(
                                (agent.OriginPosition, agent.CurrentPosition)
                            );
                        }

                        if (!rating.TryAdd(agent.CurrentPosition, 1))
                        {
                            rating[agent.CurrentPosition]++;
                        }
                    }
                    else
                    {
                        var nextPositions = agent.GetValidNextPoints(worldMap);
                        foreach (var nextPosition in nextPositions)
                        {
                            agents.Add(new Day10Part2_Agent(nextPosition, agent.OriginPosition));
                        }
                    }

                    agents.Remove(agent);
                }
            }

            //AOC.Log(
            //    JsonConvert.SerializeObject(
            //        scores.OrderBy(a => a.Key.Y).ThenBy(a => a.Key.X),
            //        Formatting.Indented
            //    )
            //);

            var result = rating.Values.Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static int? GetHeightAt(List<int[]> map, Point point)
        {
            return InBounds(map, point) ? map[point.Y][point.X] : null;
        }

        public static bool InBounds(List<int[]> map, Point point)
        {
            var mapHeight = map.Count;
            var mapWidth = map[0].Length;

            return (point.Y <= mapHeight - 1)
                && (point.X <= mapWidth - 1)
                && (point.Y >= 0)
                && (point.X >= 0);
        }

        public static List<Point> GetAllPointsOfValue(List<int[]> map, int value)
        {
            var result = new List<Point>();
            for (var currRowIndex = 0; currRowIndex < map.Count; currRowIndex++)
            {
                var currLine = map[currRowIndex];
                for (var currColIndex = 0; currColIndex < currLine.Length; currColIndex++)
                {
                    if (currLine[currColIndex] == value)
                    {
                        result.Add(new Point(currColIndex, currRowIndex));
                    }
                }
            }

            return result;
        }

        //public static Point? GetNextRevTrailPoint(List<int[]> map)
        //{
        //    throw new NotImplementedException();
        //}



        public class Day10Part2_Agent(Point point, Point originPoint)
        {
            public Point CurrentPosition { get; set; } = point;
            public Point OriginPosition { get; set; } = originPoint;

            public List<Point> GetValidNextPoints(List<int[]> map)
            {
                List<Point> validNextPoints = new List<Point>();

                var currTopoHeight = map[CurrentPosition.Y][CurrentPosition.X];

                if (CurrentPosition == Point.Empty)
                    return validNextPoints;

                // Up
                CheckDirectionAndAddPoints(new Point(0, -1));
                // Down
                CheckDirectionAndAddPoints(new Point(0, 1));
                // Left
                CheckDirectionAndAddPoints(new Point(-1, 0));
                // Right
                CheckDirectionAndAddPoints(new Point(1, 0));

                return validNextPoints;

                void CheckDirectionAndAddPoints(Point testDir)
                {
                    var testPoint = new Point(
                        CurrentPosition.X + testDir.X,
                        CurrentPosition.Y + testDir.Y
                    );

                    var testPointTopoHeight = GetHeightAt(map, testPoint);

                    if (testPointTopoHeight != null)
                    {
                        if (testPointTopoHeight == currTopoHeight - 1)
                        {
                            validNextPoints.Add(testPoint);
                        }
                    }
                }
            }
        }
    }
}
