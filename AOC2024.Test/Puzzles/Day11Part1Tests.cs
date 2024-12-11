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
using static AOC2024.Puzzles.Day11Part1;

namespace AOC2024.Tests.Puzzles
{
    public class Day11Part1Tests
    {
        public Day11Part1Tests() { }

        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day11Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day11Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day11Part1>(false);
        }

        [Fact]
        public void Blink_WhenCalled_AppliesProperly()
        {
            // Arrange
            // csharpier-ignore-start
            var list = new List<long>()
            {
                0, 1, 10, 99, 999
            };

            var expected = new List<long>()
            {
                1, 2024, 1, 0, 9, 9, 2021976
            };
            // csharpier-ignore-end

            // Act
            //var result = Blink(list);
            Blink(list);

            // Assert
            Assert.Equal(expected, list);
        }
    }
}
