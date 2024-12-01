using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day1Part1Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day1Part1 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day1Part1();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetListSplit_WhenCalled_ReturnsSplitList()
        {
            // Arrange
            var sut = new Day1Part1 { ExampleRun = true };
            var expectedLeft = new[] { 0, 2, 4 };
            var expectedRight = new[] { 1, 3, 5 };

            // Act
            var textLines = new[] { "0 1", "2 3", "4 5" };
            sut.GetListSplit(textLines, out var leftList, out var rightList);

            // Assert
            Assert.Equal(expectedLeft, leftList);
            Assert.Equal(expectedRight, rightList);
        }
    }
}
