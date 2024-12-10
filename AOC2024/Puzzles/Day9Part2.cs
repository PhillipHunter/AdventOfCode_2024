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
            for (var i = 0; i < defraggedFileBlocks.Count; i++)
            {
                if (defraggedFileBlocks[i] != null)
                {
                    sum += (long)(i * defraggedFileBlocks[i]);
                }
            }

            return sum;
        }

        public static List<int?> GetDefragmentedBlocks(List<int?> fileBlocks)
        {
            var result = new List<int?>(fileBlocks);

            var seen = new HashSet<int>();

            for (var blockIndex = result.Count - 1; blockIndex >= 0; blockIndex--)
            {
                if (result[blockIndex] == null) // Skip if empty space
                    continue;

                var fileId = result[blockIndex];
                var fileBlockLength = result.Count(i => i == result[blockIndex]); // Inefficient way to do this
                var firstNullIndex = GetFirstEmptyBlockIndexOfProperLength(result, fileBlockLength);

                var firstIndexOfFile = result.FindIndex(x => x == fileId);

                if (seen.Contains(fileId.Value))
                {
                    continue;
                }

                seen.Add(fileId.Value);

                if (firstNullIndex.HasValue)
                {
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
                        //AOC.Log($"result at {i} to {fileId}");
                    }
                }

                //AOC.Log(
                //    $"fid: {fileId} - fni: {firstNullIndex} - {JsonConvert.SerializeObject(result)}"
                //);
            }

            //AOC.Log($"F - {JsonConvert.SerializeObject(result)}");

            return result;
        }

        public static int? GetFirstEmptyBlockIndexOfProperLength(
            List<int?> fileBlocks,
            int neededLength
        )
        {
            var nullFlag = false;
            var nullCount = 0;
            var nullStartIndex = 0;
            for (var fileBlockIndex = 0; fileBlockIndex < fileBlocks.Count; fileBlockIndex++)
            {
                if (fileBlocks[fileBlockIndex] == null)
                {
                    if (!nullFlag)
                    {
                        nullFlag = true;
                        nullStartIndex = fileBlockIndex;
                    }

                    if (nullFlag)
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
                    nullFlag = false;
                    nullCount = 0;
                }
            }

            return null;
        }
    }
}
