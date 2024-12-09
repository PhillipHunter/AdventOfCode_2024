using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;
using Newtonsoft.Json;
using static AOC2024.Puzzles.Day9Part1;

namespace AOC2024.Tests.Puzzles
{
    public class Day9Part1Tests
    {
        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day9Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day9Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day9Part1>(false);
        }

        [Fact]
        public void GetFileBlocks_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var diskMap = "2333133121414131402";
            // csharpier-ignore
            var expected = new List<int?> { 0, 0, null, null, null, 1, 1, 1, null, null, null, 2, null, null, null, 3, 3, 3, null, 4, 4, null, 5, 5, 5, 5, null, 6, 6, 6, 6, null, 7, 7, 7, null, 8, 8, 8, 8, 9, 9 };

            // Act
            var result = GetFileBlocks(diskMap);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetFirstEmptyBlockIndex_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            // csharpier-ignore
            var fileBlocks = new List<int?> { 0, 0, null, null, 0, null };
            var expected = 2;

            // Act
            var result = GetFirstEmptyBlockIndex(fileBlocks);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetDefragmentedList_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            // csharpier-ignore-start
            var fileBlocks = new List<int?> { 0, 0, null, null, null, 1, 1, 1, null, null, null, 2, null, null, null, 3, 3, 3, null, 4, 4, null, 5, 5, 5, 5, null, 6, 6, 6, 6, null, 7, 7, 7, null, 8, 8, 8, 8, 9, 9 };
            var expected =   new List<int?> { 0, 0, 9, 9, 8, 1, 1, 1, 8, 8, 8, 2, 7, 7, 7, 3, 3, 3, 6, 4, 4, 6, 5, 5, 5, 5, 6, 6, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            // csharpier-ignore-end

            // Act
            var result = GetDefragmentedBlocks(fileBlocks);

            // Assert
            Assert.NotSame(fileBlocks, result); // Make sure the list was cloned
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateChecksum_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            // csharpier-ignore-start
            var defragmented = new List<int?> { 0, 0, 9, 9, 8, 1, 1, 1, 8, 8, 8, 2, 7, 7, 7, 3, 3, 3, 6, 4, 4, 6, 5, 5, 5, 5, 6, 6, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            // csharpier-ignore-end

            var expected = 1928;

            // Act
            var result = CalculateChecksum(defragmented);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
