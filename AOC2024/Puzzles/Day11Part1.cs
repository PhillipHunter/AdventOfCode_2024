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
    public class Day11Part1 : IAdventPuzzle
    {
        public string Name => "Day 11: Plutonian Pebbles Part 1";
        public string? Solution => "186424";
        public string? ExampleSolution => "55312";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day11.txt";

        public PuzzleOutput GetOutput()
        {
            var stopwatch = Stopwatch.StartNew();

            #region Puzzle

            if (ExampleRun)
            {
                _filename =
                    $"{Path.GetFileNameWithoutExtension(_filename)}Ex{Path.GetExtension(_filename)}";
            }

            var currPuzzleFile = File.ReadAllText(
                Path.Combine(
                    ConfigurationManager.AppSettings["PuzzleInputDirectory"]
                        ?? "../../../../Inputs/",
                    _filename
                )
            );

            var arrangement = new List<long>();
            arrangement.AddRange(currPuzzleFile.Split(' ').Select(long.Parse));

            for (int i = 0; i < 25; i++)
            {
                Blink(arrangement);
            }

            var result = arrangement.Count;

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static void Blink(List<long> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var stringRep = list[i].ToString();
                if (list[i] == 0)
                {
                    list[i] = 1;
                }
                else if (stringRep.Length % 2 == 0)
                {
                    var firstHalf = stringRep.Substring(0, stringRep.Length / 2);
                    var firstVal = long.Parse(firstHalf);

                    // Roundabout way to strip leading zeros
                    var secondHalf = stringRep.Substring(stringRep.Length / 2);
                    var trimmedSecondHalf =
                        secondHalf.Length > 1 ? secondHalf.TrimStart('0') : secondHalf;
                    var secondVal =
                        (trimmedSecondHalf.Length == 0) ? 0 : long.Parse(trimmedSecondHalf);

                    list[i] = secondVal;
                    list.Insert(i, firstVal);
                    i++;
                }
                else
                {
                    list[i] *= 2024;
                }
            }
        }
    }
}
