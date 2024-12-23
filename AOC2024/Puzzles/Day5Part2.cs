﻿using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day5Part2 : IAdventPuzzle
    {
        public string Name => "Day 5: Print Queue Part 2";
        public string? Solution => "4077";
        public string? ExampleSolution => "123";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day5.txt";

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

            var ruleSet = new List<Point>();
            var pageUpdates = new List<int[]>();

            foreach (var currPuzzleFileLine in currPuzzleFileLines)
            {
                if (currPuzzleFileLine.Length == 0)
                    continue;

                if (currPuzzleFileLine.Contains("|"))
                {
                    var ruleSplit = currPuzzleFileLine.Split('|');
                    var beforeNumber = int.Parse(ruleSplit[0]);
                    var afterNumber = int.Parse(ruleSplit[1]);

                    ruleSet.Add(new Point(beforeNumber, afterNumber));
                }
                else
                {
                    pageUpdates.Add(currPuzzleFileLine.Split(",").Select(int.Parse).ToArray());
                }
            }

            var invalidPageUpdates = new List<int[]>();

            foreach (var pageUpdate in pageUpdates)
            {
                var pertinentRules = GetPertinentRules(ruleSet, pageUpdate);

                var valid = pageUpdate.All(currPage =>
                    GetPageInProperPlace(currPage, pageUpdate, pertinentRules)
                );

                if (!valid)
                    invalidPageUpdates.Add(pageUpdate);
            }

            foreach (var invalidPageUpdate in invalidPageUpdates)
            {
                var pertinentRules = GetPertinentRules(ruleSet, invalidPageUpdate);

                Array.Sort(invalidPageUpdate, new Part5Comparer(pertinentRules));
            }

            var result = invalidPageUpdates.Select(u => u[(int)Math.Floor(u.Length / 2.0d)]).Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static List<Point> GetPertinentRules(List<Point> ruleSet, int[] pageUpdates)
        {
            var result = new List<Point>();

            foreach (var pageUpdate in pageUpdates)
            {
                result.AddRange(ruleSet.Where(rs => (rs.X == pageUpdate || rs.Y == pageUpdate)));
            }

            return result;
        }

        public static bool GetPageInProperPlace(
            int page,
            int[] pageUpdates,
            List<Point> pertinentRules
        )
        {
            foreach (var pertinentRule in pertinentRules.Where(r => (r.X == page || r.Y == page)))
            {
                if (
                    !pageUpdates.Contains(pertinentRule.X) || !pageUpdates.Contains(pertinentRule.Y)
                )
                {
                    continue;
                }

                var lessPage = pertinentRule.X;
                var greaterPage = pertinentRule.Y;

                var indexOfLess = Array.IndexOf(pageUpdates, lessPage);
                var indexOfGreater = Array.IndexOf(pageUpdates, greaterPage);

                if (indexOfLess > indexOfGreater)
                    return false;
            }

            return true;
        }

        public class Part5Comparer : IComparer<int>
        {
            private readonly List<Point> _pertinentRules;

            public Part5Comparer(List<Point> pertinentRules)
            {
                _pertinentRules = pertinentRules;
            }

            public int Compare(int x, int y)
            {
                // Get pertinent rule for both of these values
                var pertinentRule = _pertinentRules.FirstOrDefault(r =>
                    (r.X == x && r.Y == y) || (r.X == y && r.Y == x)
                );

                if (x == pertinentRule.Y)
                {
                    return 1;
                }

                if (x == pertinentRule.X)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
