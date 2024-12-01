using System.Configuration;
using System.Diagnostics;

namespace AOC2024.Puzzles
{
    public class Day1Part1 : IAdventPuzzle
    {
        public string Name => "Day 1: Historian Hysteria";
        public string? Solution => "1873376";
        public string? ExampleSolution => "11";
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

            var diffSum = leftList.Select((leftVal, i) => Math.Abs(leftVal - rightList[i])).Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = diffSum.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public void GetListSplit(
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
    }
}
