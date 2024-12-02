﻿using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day2Part1 : IAdventPuzzle
    {
        public string Name => "Day 2: Red-Nosed Reports Part 1";
        public string? Solution => "369";
        public string? ExampleSolution => "2";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day2.txt";

        public PuzzleOutput GetOutput()
        {
            var stopwatch = Stopwatch.StartNew();

            #region Puzzle

            if (ExampleRun)
            {
                _filename =
                    $"{Path.GetFileNameWithoutExtension(_filename)}Ex{Path.GetExtension(_filename)}";
            }

            var path = ConfigurationManager.AppSettings["PuzzleInputDirectory"];

            var currPuzzleFileLines = File.ReadAllLines(
                Path.Combine(
                    ConfigurationManager.AppSettings["PuzzleInputDirectory"]
                        ?? "../../../../Inputs/",
                    _filename
                )
            );

            var result = 0;

            foreach (var currPuzzleFileLine in currPuzzleFileLines)
            {
                var reportArray = GetReportArray(currPuzzleFileLine);
                if (
                    (AreAllDecreasing(reportArray) || AreAllIncreasing(reportArray))
                    && (GetMaxDeltaAmount(reportArray) <= 3)
                )
                {
                    result++;
                }
            }

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static int[] GetReportArray(string input)
        {
            var inputSplit = input.Split(" ");
            var result = inputSplit.Select(int.Parse);

            return result.ToArray();
        }

        public static bool AreAllIncreasing(int[] input)
        {
            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] >= input[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreAllDecreasing(int[] input)
        {
            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] <= input[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        public static int GetMaxDeltaAmount(int[] input)
        {
            var deltas = new List<int>();
            for (var i = 0; i < input.Length - 1; i++)
            {
                var delta = Math.Abs(input[i] - input[i + 1]);

                deltas.Add(delta);
            }

            return deltas.Max();
        }
    }
}
