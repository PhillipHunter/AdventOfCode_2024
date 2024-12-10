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
    public class Day9Part2 : IAdventPuzzle
    {
        public string Name => "Day 9: Disk Fragmenter Part 2";
        public string? Solution => "6460170593016";
        public string? ExampleSolution => "2858";
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

            Parallel.For(
                0,
                defraggedFileBlocks.Count,
                () => 0L,
                (i, loopState, localSum) =>
                {
                    if (defraggedFileBlocks[i] != null)
                        localSum += (long)(i * defraggedFileBlocks[i]);

                    return localSum;
                },
                localSum =>
                {
                    lock (defraggedFileBlocks)
                    {
                        sum += localSum;
                    }
                }
            );

            return sum;
        }

        public static List<int?> GetDefragmentedBlocks(List<int?> fileBlocks)
        {
            var result = new List<int?>(fileBlocks);

            var emptyBlocks = GetEmptyBlocks(fileBlocks);

            // Pre-calculate counts
            var counts = new Dictionary<int, int>();
            foreach (var fileBlock in fileBlocks.Where(b => b != null))
            {
                if (!counts.TryAdd(fileBlock.Value, 1))
                {
                    counts[fileBlock.Value]++;
                }
            }

            var seen = new HashSet<int>();

            for (var blockIndex = result.Count - 1; blockIndex >= 0; blockIndex--)
            {
                if (result[blockIndex] == null) // Skip if empty space
                    continue;

                var fileId = result[blockIndex].Value;
                var fileBlockLength = counts[fileId];
                //var firstNullIndex = GetFirstEmptyBlockIndexOfProperLength(result, fileBlockLength);

                var firstIndexOfFile = result.FindIndex(x => x == fileId);

                if (seen.Contains(fileId))
                {
                    continue;
                }

                seen.Add(fileId);

                var firstEmptyBlockIndex = emptyBlocks.FindIndex(b => b.length >= fileBlockLength);
                int? firstNullIndex =
                    (firstEmptyBlockIndex == -1) ? null : emptyBlocks[firstEmptyBlockIndex].pos;

                if (firstNullIndex.HasValue)
                {
                    var posDiff = emptyBlocks[firstEmptyBlockIndex].length - fileBlockLength;
                    if (posDiff > 0)
                    {
                        emptyBlocks[firstEmptyBlockIndex] = (
                            firstEmptyBlockIndex + fileBlockLength,
                            posDiff
                        );
                    }
                    else
                    {
                        emptyBlocks.RemoveAt(firstEmptyBlockIndex);
                    }

                    if (firstNullIndex.Value > firstIndexOfFile)
                        continue;

                    for (var i = 0; i < result.Count; i++)
                    {
                        if (result[i] == fileId)
                            result[i] = null;
                    }

                    for (
                        var i = firstNullIndex.Value;
                        i < fileBlockLength + firstNullIndex.Value;
                        i++
                    )
                    {
                        result[i] = fileId;
                    }
                }
            }

            return result;
        }

        public static List<(int pos, int length)>? GetEmptyBlocks(List<int?> fileBlocks)
        {
            var result = new List<(int, int)>();
            var nullCount = 0;
            int? nullStartIndex = null;

            for (var fileBlockIndex = 0; fileBlockIndex < fileBlocks.Count; fileBlockIndex++)
            {
                if (fileBlocks[fileBlockIndex] == null)
                {
                    if (nullStartIndex == null)
                    {
                        nullStartIndex = fileBlockIndex;
                    }

                    nullCount++;
                }
                else
                {
                    if (nullStartIndex != null)
                        result.Add((nullStartIndex.Value, nullCount));

                    nullStartIndex = null;
                    nullCount = 0;
                }
            }

            return result;
        }

        public static int? GetFirstEmptyBlockIndexOfProperLength(
            List<int?> fileBlocks,
            int neededLength
        )
        {
            var nullCount = 0;
            int? nullStartIndex = null;

            for (var fileBlockIndex = 0; fileBlockIndex < fileBlocks.Count; fileBlockIndex++)
            {
                if (fileBlocks[fileBlockIndex] == null)
                {
                    if (nullStartIndex == null)
                    {
                        nullStartIndex = fileBlockIndex;
                        nullCount++;
                    }
                    else
                    {
                        nullCount++;

                        if (nullCount >= neededLength)
                        {
                            return nullStartIndex;
                        }
                    }
                }
                else
                {
                    nullStartIndex = null;
                    nullCount = 0;
                }
            }

            return null;
        }
    }
}
