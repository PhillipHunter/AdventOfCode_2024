using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day3Part2 : IAdventPuzzle
    {
        public string Name => "Day 3: Mull It Over Part 2";
        public string? Solution => "93465710";
        public string? ExampleSolution => "48";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day3P2.txt";

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

        public static Dictionary<int, bool> GetCommandPositions(string input)
        {
            var commandPositions = new Dictionary<int, bool>();
            commandPositions.Add(0, true); // Add initial on

            const string dontPattern = @"don't\(\)";
            var dontRegex = new Regex(dontPattern);
            var dontMatches = dontRegex.Matches(input);

            foreach (Match dontMatch in dontMatches)
            {
                commandPositions.Add(dontMatch.Index, false);
            }

            const string doPattern = @"do\(\)";
            var doRegex = new Regex(doPattern);
            var doMatches = doRegex.Matches(input);

            foreach (Match doMatch in doMatches)
            {
                commandPositions.Add(doMatch.Index, true);
            }

            return commandPositions;
        }

        public static bool GetApplicableCommand(Dictionary<int, bool> commandPositions, int index)
        {
            var keysUnderIndex = commandPositions.Keys.Where(k => k <= index);
            var maxKeyUnderIndex = keysUnderIndex.Max();

            return commandPositions[maxKeyUnderIndex];
        }

        public static List<int> GetMultiplyFunctions(string input)
        {
            var resultValues = new List<int>();

            const string mulPattern = @"mul\((\d+),(\d+)\)";
            var mulRegex = new Regex(mulPattern);
            var mulMatches = mulRegex.Matches(input);

            var commandPositions = GetCommandPositions(input);

            foreach (Match mulMatch in mulMatches)
            {
                if (!mulMatch.Success)
                    continue;

                if (GetApplicableCommand(commandPositions, mulMatch.Index) == false)
                    continue;

                var mul1 = int.Parse(mulMatch.Groups[1].Value);
                var mul2 = int.Parse(mulMatch.Groups[2].Value);

                resultValues.Add(mul1 * mul2);
            }

            return resultValues;
        }
    }
}
