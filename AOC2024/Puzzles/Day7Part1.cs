using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AOC2024.Puzzles
{
    public class Day7Part1 : IAdventPuzzle
    {
        public string Name => "Day 7: Bridge Repair Part 1";
        public string? Solution => "945512582195";
        public string? ExampleSolution => "3749";
        public bool ExampleRun { get; set; } = false;

        private string _filename = "Day7.txt";

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

            var equations = new List<Day7Part1_Equation>();

            foreach (var currPuzzleFileLine in currPuzzleFileLines)
            {
                var resultSplit = currPuzzleFileLine.Split(':');

                equations.Add(
                    new Day7Part1_Equation()
                    {
                        Result = long.Parse(resultSplit[0]),
                        Operands = resultSplit[1]
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(long.Parse)
                            .ToList(),
                    }
                );
            }

            var solveable = new List<Day7Part1_Equation>();
            foreach (var equation in equations)
            {
                if (DoesEquationSolve(equation))
                {
                    solveable.Add(equation);
                }
            }

            var result = solveable.Select(e => e.Result).Sum();

            #endregion Puzzle

            stopwatch.Stop();

            var puzzleOutput = new PuzzleOutput
            {
                Result = result.ToString(),
                CompletionTime = stopwatch.ElapsedMilliseconds,
            };

            return puzzleOutput;
        }

        public static bool DoesEquationSolve(Day7Part1_Equation equation)
        {
            var operationSets = GetAllOperationSets(equation);

            foreach (var operations in operationSets)
            {
                if (TryGetAnswer(equation, operations, out var answer))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<Queue<Day7Part1_Operation>> GetAllOperationSets(
            Day7Part1_Equation equation
        )
        {
            var operandCount = equation.Operands.Count - 1;
            var result = new List<Queue<Day7Part1_Operation>>();

            GenerateCombinations(new Queue<Day7Part1_Operation>(), operandCount, result);

            return result;
        }

        public static void GenerateCombinations(
            Queue<Day7Part1_Operation> current,
            int remaining,
            List<Queue<Day7Part1_Operation>> result
        )
        {
            if (remaining == 0)
            {
                result.Add(current);
                return;
            }

            var addQueue = new Queue<Day7Part1_Operation>(current);
            addQueue.Enqueue(Day7Part1_Operation.Add);
            GenerateCombinations(addQueue, remaining - 1, result);

            var multQueue = new Queue<Day7Part1_Operation>(current);
            multQueue.Enqueue(Day7Part1_Operation.Multiply);
            GenerateCombinations(multQueue, remaining - 1, result);
        }

        public static bool TryGetAnswer(
            Day7Part1_Equation equation,
            Queue<Day7Part1_Operation> operations,
            out long answer
        )
        {
            answer = 0;
            for (var operandIndex = 0; operandIndex < equation.Operands.Count - 1; operandIndex++)
            {
                var currOperand = (operandIndex == 0) ? equation.Operands[operandIndex] : answer;
                var nextOperand = equation.Operands[operandIndex + 1];

                var currOperation = operations.Dequeue();

                switch (currOperation)
                {
                    case Day7Part1_Operation.Add:
                        answer = (currOperand + nextOperand);
                        break;
                    case Day7Part1_Operation.Multiply:
                        answer = (currOperand * nextOperand);
                        break;
                }
            }

            return (answer == equation.Result);
        }

        public struct Day7Part1_Equation
        {
            public Day7Part1_Equation()
            {
                Result = 0;
            }

            public long Result { get; set; }
            public List<long> Operands { get; set; } = new List<long>();
        }

        public enum Day7Part1_Operation
        {
            Add,
            Multiply,
        }
    }
}
