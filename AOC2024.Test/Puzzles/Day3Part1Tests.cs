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
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day3Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day3Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day3Part1>(false);
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
