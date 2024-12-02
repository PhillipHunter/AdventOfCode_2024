using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day2Part2Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day2Part2 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day2Part2();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetReportArray_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var input = "7 6 4 2 1";
            var expected = new int[] { 7, 6, 4, 2, 1 };

            // Act
            var result = Day2Part2.GetReportArray(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AreAllIncreasing_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var input = new List<int[]>
            {
                new[] { 7, 6, 4, 2, 1 },
                new[] { 1, 2, 7, 8, 9 },
                new[] { 9, 7, 6, 2, 1 },
                new[] { 1, 3, 2, 4, 5 },
                new[] { 8, 6, 4, 4, 1 },
                new[] { 1, 3, 6, 7, 9 },
                new[] { 64, 65, 67, 70, 72, 74, 77, 77 },
            };

            var expected = new List<bool> { false, true, false, false, false, true, false };

            // Act
            var result = input.Select(Day2Part2.AreAllIncreasing).ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AreAllDecreasing_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var input = new List<int[]>
            {
                new[] { 7, 6, 4, 2, 1 },
                new[] { 1, 2, 7, 8, 9 },
                new[] { 9, 7, 6, 2, 1 },
                new[] { 1, 3, 2, 4, 5 },
                new[] { 8, 6, 4, 4, 1 },
                new[] { 1, 3, 6, 7, 9 },
                new[] { 64, 65, 67, 70, 72, 74, 77, 77 },
            };

            var expected = new List<bool> { true, false, true, false, false, false, false };

            // Act
            var result = input.Select(Day2Part2.AreAllDecreasing).ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMaxDeltaAmount_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var input = new List<int[]>
            {
                new[] { 7, 6, 4, 2, 1 },
                new[] { 1, 2, 7, 8, 9 },
                new[] { 9, 7, 6, 2, 1 },
                new[] { 1, 3, 2, 4, 5 },
                new[] { 8, 6, 4, 4, 1 },
                new[] { 1, 3, 6, 7, 9 },
            };

            var expected = new List<int> { 2, 5, 4, 2, 3, 3 };

            // Act
            var result = input.Select(Day2Part2.GetMaxDeltaAmount).ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetItemRemovedPermutations_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var input = new[] { 10, 20, 30, 40, 50 };
            var expected = new List<int[]>
            {
                // csharpier-ignore-start
                new[] { 10, 20, 30, 40, 50 },
                new[] {     20, 30, 40, 50 },
                new[] { 10,     30, 40, 50 },
                new[] { 10, 20,     40, 50 },
                new[] { 10, 20, 30,     50 },
                new[] { 10, 20, 30, 40     },
                // csharpier-ignore-end
            };

            // Act
            var result = Day2Part2.GetItemRemovedPermutations(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
