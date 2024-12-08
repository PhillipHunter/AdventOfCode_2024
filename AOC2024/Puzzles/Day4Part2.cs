using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day4Part2 : IAdventPuzzle
    {
        public string Name => "Day 4: Ceres Search Part 2";
        public string? Solution => "1939";
        public string? ExampleSolution => "9";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day4.txt";

        private const string targetWord = "MAS";

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

            var targetStartPoints = new List<Point>();

            var diagonalCountForMidpoint = new Dictionary<Point, int>();

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
                GetXmasValid(
                    currPuzzleFileLines,
                    targetStartPoint.Y,
                    targetStartPoint.X,
                    diagonalCountForMidpoint
                );
            }

            var result = diagonalCountForMidpoint.Count(a => (a.Value == 2)); // Get the diagonals that have been "seen" exactly twice
            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static void GetXmasValid(
            string[] inputLines,
            int row,
            int col,
            Dictionary<Point, int> diagonalCountForMidpoint
        )
        {
            var midOffset = (int)Math.Floor(targetWord.Length / 2.0d); // Get the midpoint offset position (aka A for the puzzle)

            void IncrementCount(Point key)
            {
                if (diagonalCountForMidpoint.TryGetValue(key, out var count))
                {
                    diagonalCountForMidpoint[key] = count + 1;
                }
                else
                {
                    diagonalCountForMidpoint[key] = 1;
                }
            }

            if (TRToBLDiag(inputLines, row, col, targetWord))
            {
                IncrementCount(new Point(col - midOffset, row + midOffset));
            }

            if (TLToBRDiag(inputLines, row, col, targetWord))
            {
                IncrementCount(new Point(col + midOffset, row + midOffset));
            }

            if (BLToTRDiag(inputLines, row, col, targetWord))
            {
                IncrementCount(new Point(col + midOffset, row - midOffset));
            }

            if (BRToTLDiag(inputLines, row, col, targetWord))
            {
                IncrementCount(new Point(col - midOffset, row - midOffset));
            }
        }

        private static bool CheckLettersInDir(
            string[] inputLines,
            int row,
            int col,
            string target,
            Point targetDir
        )
        {
            var validChars = 0;
            for (var i = 0; i < target.Length; i++)
            {
                var selectPoint = new Point(col + (i * targetDir.X), row + (i * targetDir.Y));

                if (!InBounds(inputLines, selectPoint))
                    return false;

                if (inputLines[selectPoint.Y][selectPoint.X] == target[i])
                {
                    validChars++;
                }
            }

            return (validChars == target.Length);
        }

        public static bool LToRHorz(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(1, 0);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool RToLHorz(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(-1, 0);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool TToBVert(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(0, 1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool BToTVert(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(0, -1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool TLToBRDiag(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(1, 1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool TRToBLDiag(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(-1, 1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool BLToTRDiag(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(1, -1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool BRToTLDiag(string[] inputLines, int row, int col, string target)
        {
            var targetDir = new Point(-1, -1);
            return CheckLettersInDir(inputLines, row, col, target, targetDir);
        }

        public static bool InBounds(string[] inputLines, Point point)
        {
            var mapHeight = inputLines.Length;
            var mapWidth = inputLines[0].Length;

            return (point.Y <= mapHeight - 1)
                && (point.X <= mapWidth - 1)
                && (point.Y >= 0)
                && (point.X >= 0);
        }
    }
}
