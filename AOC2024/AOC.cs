using System.Diagnostics;
using AOC2024.Puzzles;

namespace AOC2024
{
    public class AOC
    {
        private static readonly List<IAdventPuzzle> AdventPuzzles = [];

        public static IAdventPuzzle[] GetAdventPuzzles()
        {
            // Add Puzzles Here
            AdventPuzzles.Add(new Day1Part1());
            AdventPuzzles.Add(new Day1Part2());
            AdventPuzzles.Add(new Day2Part1());
            AdventPuzzles.Add(new Day2Part2());
            AdventPuzzles.Add(new Day3Part1());
            AdventPuzzles.Add(new Day3Part2());
            AdventPuzzles.Add(new Day4Part1());
            AdventPuzzles.Add(new Day4Part2());
            AdventPuzzles.Add(new Day5Part1());
            AdventPuzzles.Add(new Day5Part2());
            AdventPuzzles.Add(new Day6Part1());
            AdventPuzzles.Add(new Day7Part1());

            return AdventPuzzles.ToArray();
        }

        public static void Log(string message)
        {
#if DEBUG
            Debug.WriteLine(message);
#endif
        }
    }
}
