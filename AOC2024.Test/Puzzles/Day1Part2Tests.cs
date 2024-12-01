using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day1Part2Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day1Part2 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day1Part2();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetListSplit_WhenCalled_ReturnsSplitList()
        {
            // Arrange
            var expectedLeft = new[] { 0, 2, 4 };
            var expectedRight = new[] { 1, 3, 5 };

            // Act
            var textLines = new[] { "0 1", "2 3", "4 5" };
            Day1Part2.GetListSplit(textLines, out var leftList, out var rightList);

            // Assert
            Assert.Equal(expectedLeft, leftList);
            Assert.Equal(expectedRight, rightList);
        }

        [Fact]
        public void GetListCounts_WhenCalled_ReturnsListCounts()
        {
            // Arrange
            var input = new List<int>() { 1, 5, 5, 1, 1, 4 };

            var expected = new Dictionary<int, int>()
            {
                { 1, 3 },
                { 5, 2 },
                { 4, 1 },
            };

            // Act
            var result = Day1Part2.GetListCounts(input);

            // Assert
            Assert.Equivalent(expected, result);
        }
    }
}
