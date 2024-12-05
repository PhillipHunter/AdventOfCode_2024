using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day5Part1Tests
    {
        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            // Arrange
            var sut = new Day5Part1 { ExampleRun = true };

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.ExampleSolution, output.Result);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            // Arrange
            var sut = new Day5Part1();

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(sut.Solution, output.Result);
        }

        [Fact]
        public void GetPertinentRules_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var ruleSet = new List<Point>()
            {
                new(47, 53),
                new(97, 13),
                new(97, 61),
                new(4, 5),
                new(97, 47),
            };

            var pageUpdates = new[] { 47, 61 };

            var expected = new List<Point>() { new(47, 53), new(97, 61), new(97, 47) };

            // Act
            var result = Day5Part1.GetPertinentRules(ruleSet, pageUpdates);

            // Assert
            Assert.Equivalent(expected, result);
        }

        [Fact]
        public void GetPageInProperPlace_WhenCalledInOrder_ReturnsTrue()
        {
            // Arrange
            var pertinentRules = new List<Point>()
            {
                new(47, 53),
                new(97, 61),
                new(97, 47),
                new(47, 61),
            };

            var pageUpdates = new[] { 97, 47, 61 };

            foreach (var pageUpdate in pageUpdates)
            {
                // Act
                var result = Day5Part1.GetPageInProperPlace(
                    pageUpdate,
                    pageUpdates,
                    pertinentRules
                );

                // Assert
                Assert.Equivalent(true, result);
            }
        }

        [Fact]
        public void GetPageInProperPlace_WhenCalledOutOfOrder_ReturnsFalse()
        {
            // Arrange
            var pertinentRules = new List<Point>()
            {
                new(47, 53),
                new(97, 61),
                new(97, 47),
                new(47, 61),
            };

            var pageUpdates = new[] { 47, 97, 61 };

            // Act
            var result = Day5Part1.GetPageInProperPlace(47, pageUpdates, pertinentRules);

            // Assert
            Assert.Equivalent(false, result);
        }
    }
}
