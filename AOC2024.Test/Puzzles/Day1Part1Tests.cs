using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day1Part1Tests
    {
        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day1Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day1Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day1Part1>(false);
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
