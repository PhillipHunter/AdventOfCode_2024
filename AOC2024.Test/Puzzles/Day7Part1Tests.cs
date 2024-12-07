using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;
using static AOC2024.Puzzles.Day7Part1;

namespace AOC2024.Tests.Puzzles
{
    public class Day7Part1Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day7Part1 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day7Part1();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void TryGetAnswer_WhenCalledWithValid_ReturnsProperAnswer()
        {
            // Arrange
            var equation = new Day7Part1_Equation()
            {
                Result = 3267,
                Operands = new List<long>() { 81, 40, 27 },
            };

            var operations = new Queue<Day7Part1_Operation>();

            operations.Enqueue(Day7Part1_Operation.Add);
            operations.Enqueue(Day7Part1_Operation.Multiply);

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
            var equation = new Day7Part1_Equation()
            {
                Result = 3267,
                Operands = new List<long>() { 81, 40, 27 },
            };

            var aa = new Queue<Day7Part1_Operation>();
            aa.Enqueue(Day7Part1_Operation.Add);
            aa.Enqueue(Day7Part1_Operation.Add);

            var am = new Queue<Day7Part1_Operation>();
            am.Enqueue(Day7Part1_Operation.Add);
            am.Enqueue(Day7Part1_Operation.Multiply);

            var ma = new Queue<Day7Part1_Operation>();
            ma.Enqueue(Day7Part1_Operation.Multiply);
            ma.Enqueue(Day7Part1_Operation.Add);

            var mm = new Queue<Day7Part1_Operation>();
            mm.Enqueue(Day7Part1_Operation.Multiply);
            mm.Enqueue(Day7Part1_Operation.Multiply);

            var expected = new List<Queue<Day7Part1_Operation>>() { aa, am, ma, mm };

            // Act
            var result = GetAllOperationSets(equation);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DoesEquationSolve_WhenCalledWithSolveable_ReturnsTrue()
        {
            // Arrange
            var equation = new Day7Part1_Equation()
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
            var equation = new Day7Part1_Equation()
            {
                Result = 7290,
                Operands = new List<long>() { 6, 8, 6, 15 },
            };

            // Act
            var result = DoesEquationSolve(equation);

            // Assert
            Assert.False(result);
        }
    }
}
