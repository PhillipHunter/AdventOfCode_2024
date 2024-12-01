using System.Diagnostics;

namespace AOC2024.Puzzles
{
    public class Day1Part1 : IAdventPuzzle
    {
        public string Name { get; }
        public string? Solution { get; }

        private const string Filename = "Day1.txt";

        public Day1Part1()
        {
            Name = "Day 1 Part 1: Unknown?!";
            Solution = "Unknown";
        }

        public PuzzleOutput GetOutput()
        {
            var stopwatch = Stopwatch.StartNew();

            #region Puzzle


            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = 0.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }
    }
}
