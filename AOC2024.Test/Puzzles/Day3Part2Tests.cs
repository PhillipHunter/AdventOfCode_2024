using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day3Part2Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day3Part2 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day3Part2();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetMultiplyFunctions_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            var expected = new List<int>() { 8, 40 };

            // Act
            var result = Day3Part2.GetMultiplyFunctions(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCommandPositions_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            var expected = new Dictionary<int, bool>
            {
                [0] = true,
                [20] = false,
                [59] = true,
            };

            // Act
            var result = Day3Part2.GetCommandPositions(input);

            // Assert
            Assert.Equivalent(expected, result);
        }

        [Fact]
        public void GetApplicableCommand_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var input = new Dictionary<int, bool>
            {
                [0] = true,
                [20] = false,
                [59] = true,
            };

            var testVals = new int[] { 1, 28, 64 };
            var expected = new bool[] { true, false, true };

            for (var i = 0; i < testVals.Length; i++)
            {
                // Act
                var result = Day3Part2.GetApplicableCommand(input, testVals[i]);

                // Assert
                Assert.Equivalent(expected[i], result);
            }
        }
    }
}
