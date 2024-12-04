using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day4Part2Tests
    {
        private string[] inputLines;
        private const string target = "XMAS";

        public Day4Part2Tests()
        {
            var input =
                "MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX";
            inputLines = input.Split("\r\n");
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day4Part2 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day4Part2();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void LToRHorz_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 0;
            var col = 5;
            var expected = true;

            // Act
            var result = Day4Part2.LToRHorz(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RToLHorz_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 1;
            var col = 4;
            var expected = true;

            // Act
            var result = Day4Part2.RToLHorz(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TToBVert_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 3;
            var col = 9;
            var expected = true;

            // Act
            var result = Day4Part2.TToBVert(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BToTVert_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 9;
            var col = 9;
            var expected = true;

            // Act
            var result = Day4Part2.BToTVert(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TLToBRDiag_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 0;
            var col = 4;
            var expected = true;

            // Act
            var result = Day4Part2.TLToBRDiag(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TRToBLDiag_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 3;
            var col = 9;
            var expected = true;

            // Act
            var result = Day4Part2.TRToBLDiag(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BLToTRDiag_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 9;
            var col = 1;
            var expected = true;

            // Act
            var result = Day4Part2.BLToTRDiag(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }

        public void BRToTLDiag_WhenCalledWithValid_ReturnsTrue()
        {
            // Arrange
            var row = 9;
            var col = 9;
            var expected = true;

            // Act
            var result = Day4Part2.BRToTLDiag(inputLines, row, col, target);
            Assert.Equal(expected, result);
        }
    }
}
