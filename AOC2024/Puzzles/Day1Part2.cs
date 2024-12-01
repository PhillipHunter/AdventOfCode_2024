using System.Configuration;
using System.Diagnostics;

namespace AOC2024.Puzzles
{
    public class Day1Part2 : IAdventPuzzle
    {
        public string Name => "Day 1: Historian Hysteria Part 2";
        public string? Solution => "18997088";
        public string? ExampleSolution => "31";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day1.txt";

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

            GetListSplit(currPuzzleFileLines, out var leftList, out var rightList);

            leftList.Sort();
            rightList.Sort();

            var rightListCounts = GetListCounts(rightList);

            var diffSum = leftList
                .Select((leftVal, i) => leftVal * rightListCounts.GetValueOrDefault(leftVal))
                .Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = diffSum.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static void GetListSplit(
            string[] textLines,
            out List<int> leftList,
            out List<int> rightList
        )
        {
            leftList = new List<int>();
            rightList = new List<int>();

            foreach (var currLine in textLines)
            {
                var lineSplit = currLine.Split(" ");
                leftList.Add(int.Parse(lineSplit.First()));
                rightList.Add(int.Parse(lineSplit.Last()));
            }
        }

        public static Dictionary<int, int> GetListCounts(List<int> list)
        {
            var result = new Dictionary<int, int>();
            foreach (var currLine in list)
            {
                if (!result.TryAdd(currLine, 1))
                {
                    result[currLine]++;
                }
            }

            return result;
        }
    }
}
