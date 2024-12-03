using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day3Part1Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day3Part1 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day3Part1();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetMultiplyFunctions_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
            var expected = new List<int>() { 8, 25, 88, 40 };

            // Act
            var result = Day3Part1.GetMultiplyFunctions(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
