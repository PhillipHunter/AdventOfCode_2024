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
    public class Day9Part1 : IAdventPuzzle
    {
        public string Name => "Day 9: Disk Fragmenter Part 1";
        public string? Solution => "6430446922192";
        public string? ExampleSolution => "1928";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day9.txt";

        public PuzzleOutput GetOutput()
        {
            var stopwatch = Stopwatch.StartNew();

            #region Puzzle

            if (ExampleRun)
            {
                _filename =
                    $"{Path.GetFileNameWithoutExtension(_filename)}Ex{Path.GetExtension(_filename)}";
            }

            var currPuzzleFileText = File.ReadAllText(
                Path.Combine(
                    ConfigurationManager.AppSettings["PuzzleInputDirectory"]
                        ?? "../../../../Inputs/",
                    _filename
                )
            );

            var fileBlocks = GetFileBlocks(currPuzzleFileText);
            var defragmentedBlocks = GetDefragmentedBlocks(fileBlocks);
            var result = CalculateChecksum(defragmentedBlocks);

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static List<int?> GetFileBlocks(string diskMap)
        {
            var fileBlocks = new List<int?>();

            var currFileId = 0;
            for (var mapIndex = 0; mapIndex < diskMap.Length; mapIndex++)
            {
                var sectorLength = int.Parse(diskMap[mapIndex].ToString());
                if (mapIndex % 2 == 0) // File Length
                {
                    for (var fileSectorIndex = 0; fileSectorIndex < sectorLength; fileSectorIndex++)
                    {
                        fileBlocks.Add(currFileId);
                    }

                    currFileId++;
                }
                else // Free Space
                {
                    for (var nullSpaceIndex = 0; nullSpaceIndex < sectorLength; nullSpaceIndex++)
                    {
                        fileBlocks.Add(null);
                    }
                }
            }

            return fileBlocks;
        }

        public static long CalculateChecksum(List<int?> defraggedFileBlocks)
        {
            long sum = 0;
            for (var i = 0; i < defraggedFileBlocks.Count; i++)
            {
                if (defraggedFileBlocks[i] != null)
                {
                    sum += (long)(i * defraggedFileBlocks[i]);
                }
                else
                {
                    break;
                }
            }

            return sum;
        }

        public static List<int?> GetDefragmentedBlocks(List<int?> fileBlocks)
        {
            var result = new List<int?>(fileBlocks);

            for (var blockIndex = result.Count - 1; blockIndex >= 0; blockIndex--)
            {
                if (result[blockIndex] == null) // Skip if empty space
                    continue;

                var firstNullIndex = GetFirstEmptyBlockIndex(result);

                //if (firstNullIndex == -1) // Done early
                //    return result;

                result[firstNullIndex] = result[blockIndex];
                result[blockIndex] = null;

                //AOC.Log($"{result[firstNullIndex]} - {JsonConvert.SerializeObject(result)}");
            }

            // Handle final edge case, where the loop is done, but a null is at the start since that value was moved to the next null
            result.RemoveAt(0);
            result.Add(null);

            AOC.Log($"F - {JsonConvert.SerializeObject(result)}");

            return result;
        }

        public static int GetFirstEmptyBlockIndex(List<int?> fileBlocks)
        {
            return fileBlocks.FindIndex(x => x == null);
        }
    }
}
