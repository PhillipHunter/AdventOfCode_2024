using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;
using Newtonsoft.Json;
using static AOC2024.Puzzles.Day7Part2;

namespace AOC2024.Tests.Puzzles
{
    public class Day7Part2Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day7Part2 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day7Part2();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void TryGetAnswer_WhenCalledWithValid_ReturnsProperAnswer()
        {
            // Arrange
            var equation = new Day7Part2_Equation()
            {
                Result = 3267,
                Operands = new List<long>() { 81, 40, 27 },
            };

            var operations = new Queue<Day7Part2_Operation>();

            operations.Enqueue(Day7Part2_Operation.Add);
            operations.Enqueue(Day7Part2_Operation.Multiply);

            var expectedAnswer = equation.Result;
            var expectedSolved = true;

            // Act
            var resultSolved = TryGetAnswer(equation, operations, out var resultAnswer);

            // Assert
            Assert.Equal(expectedSolved, resultSolved);
            Assert.Equal(expectedAnswer, resultAnswer);
        }

        [Fact]
        public void GetAllOperationSets_WhenCalled_ReturnsAllPossibilities()
        {
            // Arrange
            var equation = new Day7Part2_Equation()
            {
                Result = 3267,
                Operands = new List<long>() { 81, 40, 27 },
            };

            //[[0,0],[0,1],[0,2],[1,0],[1,1],[1,2],[2,0],[2,1],[2,2]]

            var aa = new Queue<Day7Part2_Operation>();
            aa.Enqueue(Day7Part2_Operation.Add);
            aa.Enqueue(Day7Part2_Operation.Add);

            var am = new Queue<Day7Part2_Operation>();
            am.Enqueue(Day7Part2_Operation.Add);
            am.Enqueue(Day7Part2_Operation.Multiply);

            var ac = new Queue<Day7Part2_Operation>();
            ac.Enqueue(Day7Part2_Operation.Add);
            ac.Enqueue(Day7Part2_Operation.Concat);

            var ma = new Queue<Day7Part2_Operation>();
            ma.Enqueue(Day7Part2_Operation.Multiply);
            ma.Enqueue(Day7Part2_Operation.Add);

            var mm = new Queue<Day7Part2_Operation>();
            mm.Enqueue(Day7Part2_Operation.Multiply);
            mm.Enqueue(Day7Part2_Operation.Multiply);

            var mc = new Queue<Day7Part2_Operation>();
            mc.Enqueue(Day7Part2_Operation.Multiply);
            mc.Enqueue(Day7Part2_Operation.Concat);

            var ca = new Queue<Day7Part2_Operation>();
            ca.Enqueue(Day7Part2_Operation.Concat);
            ca.Enqueue(Day7Part2_Operation.Add);

            var cm = new Queue<Day7Part2_Operation>();
            cm.Enqueue(Day7Part2_Operation.Concat);
            cm.Enqueue(Day7Part2_Operation.Multiply);

            var cc = new Queue<Day7Part2_Operation>();
            cc.Enqueue(Day7Part2_Operation.Concat);
            cc.Enqueue(Day7Part2_Operation.Concat);

            var expected = new List<Queue<Day7Part2_Operation>>()
            {
                aa,
                am,
                ac,
                ma,
                mm,
                mc,
                ca,
                cm,
                cc,
            };

            // Act
            var result = GetAllOperationSets(equation);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DoesEquationSolve_WhenCalledWithSolveable_ReturnsTrue()
        {
            // Arrange
            var equation = new Day7Part2_Equation()
            {
                Result = 3267,
                Operands = new List<long>() { 81, 40, 27 },
            };

            // Act
            var result = DoesEquationSolve(equation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DoesEquationSolve_WhenCalledWithUnsolveable_ReturnsFalse()
        {
            // Arrange
            var equation = new Day7Part2_Equation()
            {
                Result = 7290,
                Operands = new List<long>() { 6, 8, 6, 15, 81 },
            };

            // Act
            var result = DoesEquationSolve(equation);

            // Assert
            Assert.False(result);
        }
    }
}
