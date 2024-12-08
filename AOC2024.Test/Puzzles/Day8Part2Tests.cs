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
using static AOC2024.Puzzles.Day8Part2;

namespace AOC2024.Tests.Puzzles
{
    public class Day8Part2Tests
    {
        private List<char[]> exampleMap;

        public Day8Part2Tests()
        {
            exampleMap = JsonConvert.DeserializeObject<List<char[]>>(
                "[[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\"0\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\"0\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\"0\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\"0\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\"A\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\"A\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\"A\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"]]"
            );
        }

        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day8Part2>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day8Part2>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day8Part2>(false);
        }

        [Fact]
        public void GetSlope_WhenCalled_ReturnsProperAnswer()
        {
            // Arrange
            var pointA = new Point(3, 4);
            var pointB = new Point(6, 5);

            var expectedAB = (3, 1);

            // Act
            var resultAB = GetSlope(pointA, pointB);

            // Assert
            Assert.Equal(expectedAB, resultAB);

            // Arrange
            var pointC = new Point(3, 4);
            var pointD = new Point(6, 2);

            var expectedCD = (3, -2);

            // Act
            var resultCD = GetSlope(pointC, pointD);

            // Assert
            Assert.Equal(expectedCD, resultCD);
        }

        [Fact]
        public void GetPointsForChar_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var character = '0';
            var expected = new List<Point>() { new(5, 2), new(8, 1), new(4, 4), new(7, 3) };

            // Act
            var result = GetPointsForChar(exampleMap, character);

            // Assert
            Assert.Equivalent(expected, result);
        }

        //TODO: Fix failing tests

        //[Fact]
        //public void FindAntipodes_WhenCalledInBounds_ReturnsProperValues()
        //{
        //    // Arrange
        //    var character = 'T';
        //    var map = JsonConvert.DeserializeObject<List<char[]>>(
        //        "[[\"T\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\"T\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\"T\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"]]\r\n"
        //    );

        //    var expected = new List<Point>()
        //    {
        //        new(5, 0),
        //        new(6, 2),
        //        new(9, 3),
        //        new(2, 4),
        //        new(3, 6),
        //        new(4, 8),
        //    };

        //    // Act
        //    var result = FindAntipodes(map, character);

        //    // Assert
        //    Assert.Equivalent(expected, result);
        //    Assert.Equal(expected.Count, result.Count);
        //}

        //[Fact]
        //public void FindAntipodes_WhenCalledOutOfBounds_ReturnsProperValues()
        //{
        //    // Arrange
        //    var character = 'a';
        //    var map = JsonConvert.DeserializeObject<List<char[]>>(
        //        "[[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\"a\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\"a\",\".\"],[\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\",\".\"]]\r\n"
        //    );

        //    var expected = new List<Point>() { new(4, 1) };

        //    // Act
        //    var result = FindAntipodes(map, character);

        //    // Assert
        //    Assert.Equivalent(expected, result);
        //    Assert.Equal(expected.Count, result.Count);
        //}

        [Fact]
        public void InBounds_WhenCalledInBounds_ReturnsTrue()
        {
            // Arrange
            var inBoundsPoint = new Point(11, 0);

            // Act
            var result = InBounds(exampleMap, inBoundsPoint);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InBounds_WhenCalledOutOfBounds_ReturnsFalse()
        {
            // Arrange
            var inBoundsPoint = new Point(12, 11);

            // Act
            var result = InBounds(exampleMap, inBoundsPoint);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetFrequencies_WhenCalled_ReturnsProperValues()
        {
            // Arrange
            var expected = new HashSet<char>() { '0', 'A' };

            // Act
            var result = GetFrequencies(exampleMap);

            // Assert
            Assert.Equivalent(expected, result);
            Assert.Equal(expected.Count, result.Count);
        }
    }
}
