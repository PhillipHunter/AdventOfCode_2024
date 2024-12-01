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
