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
using static AOC2024.Puzzles.Day10Part1;

namespace AOC2024.Tests.Puzzles
{
    public class Day10Part1Tests
    {
        private List<int[]> _exampleMap;

        public Day10Part1Tests()
        {
            _exampleMap = JsonConvert.DeserializeObject<List<int[]>>(
                "[[8,9,0,1,0,1,2,3],[7,8,1,2,1,8,7,4],[8,7,4,3,0,9,6,5],[9,6,5,4,9,8,7,4],[4,5,6,7,8,9,0,3],[3,2,0,1,9,0,1,2],[0,1,3,2,9,8,0,1],[1,0,4,5,6,7,3,2]]\r\n"
            );
        }

        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day10Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day10Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day10Part1>(false);
        }

        [Fact]
        public void Agent_GetValidNextPoints_WhenCalled_ReturnsProperAnswer()
        {
            // Arrange
            var sut = new Day10Part1_Agent(new Point(5, 4), new Point(5, 4));

            var expected = new List<Point>() { new Point(5, 3), new Point(4, 4) };

            // Act
            var result = sut.GetValidNextPoints(_exampleMap);

            // Assert
            Assert.Equivalent(expected, result);
        }

        [Fact]
        public void GetAllPointsOfValue_WhenCalled_ReturnsProperAnswer()
        {
            // Arrange
            var value = 4;

            var expected = new List<Point>()
            {
                new(2, 2),
                new(7, 1),
                new(3, 3),
                new(7, 3),
                new(0, 4),
                new(2, 7),
            };

            // Act
            var result = Day10Part1.GetAllPointsOfValue(_exampleMap, value);

            // Assert
            Assert.Equivalent(expected, result);
            Assert.Equal(expected.Count, result.Count);
        }
    }
}
