using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day3Part1 : IAdventPuzzle
    {
        public string Name => "Day 3: Mull It Over Part 1";
        public string? Solution => "166630675";
        public string? ExampleSolution => "161";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day3.txt";

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

            var resultValues = GetMultiplyFunctions(currPuzzleFile);

            var result = resultValues.Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static List<int> GetMultiplyFunctions(string input)
        {
            var resultValues = new List<int>();

            const string mulPattern = @"mul\((\d+),(\d+)\)";
            var mulRegex = new Regex(mulPattern);
            var mulMatches = mulRegex.Matches(input);

            foreach (Match mulMatch in mulMatches)
            {
                if (!mulMatch.Success)
                    continue;

                var mul1 = int.Parse(mulMatch.Groups[1].Value);
                var mul2 = int.Parse(mulMatch.Groups[2].Value);

                resultValues.Add(mul1 * mul2);
            }

            return resultValues;
        }
    }
}
