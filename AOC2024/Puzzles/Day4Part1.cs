using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day4Part1 : IAdventPuzzle
    {
        public string Name => "Day 4: Ceres Search Part 1";
        public string? Solution => "2547";
        public string? ExampleSolution => "18";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day4.txt";

        private const string targetWord = "XMAS";

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

            var result = 0;

            var targetStartPoints = new List<Point>();

            for (var currRowIndex = 0; currRowIndex < currPuzzleFileLines.Length; currRowIndex++)
            {
                var currLine = currPuzzleFileLines[currRowIndex];
                for (var currColIndex = 0; currColIndex < currLine.Length; currColIndex++)
                {
                    if (currLine[currColIndex] == targetWord[0])
                    {
                        targetStartPoints.Add(new Point(currColIndex, currRowIndex));
                    }
                }
            }

            foreach (var targetStartPoint in targetStartPoints)
            {
                result += GetXmasValid(currPuzzleFileLines, targetStartPoint.Y, targetStartPoint.X);
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

        public static int GetXmasValid(string[] inputLines, int row, int col)
        {
            int matches = 0;

            if (LToRHorz(inputLines, row, col, targetWord))
                matches++;

            if (RToLHorz(inputLines, row, col, targetWord))
                matches++;

            if (TToBVert(inputLines, row, col, targetWord))
                matches++;

            if (BToTVert(inputLines, row, col, targetWord))
                matches++;

            if (TRToBLDiag(inputLines, row, col, targetWord))
                matches++;

            if (TLToBRDiag(inputLines, row, col, targetWord))
                matches++;

            if (BLToTRDiag(inputLines, row, col, targetWord))
                matches++;

            if (BRToTLDiag(inputLines, row, col, targetWord))
                matches++;

            return matches;
        }

        public static bool LToRHorz(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row][col + i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool RToLHorz(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row][col - i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool TToBVert(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row + i][col] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool BToTVert(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row - i][col] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool TLToBRDiag(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row + i][col + i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool TRToBLDiag(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row + i][col - i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool BLToTRDiag(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row - i][col + i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }

        public static bool BRToTLDiag(string[] inputLines, int row, int col, string target)
        {
            try
            {
                var validChars = target.Where((t, i) => inputLines[row - i][col - i] == t).Count();

                if (validChars == target.Length)
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false; // Out of bounds
            }

            return false;
        }
    }
}
