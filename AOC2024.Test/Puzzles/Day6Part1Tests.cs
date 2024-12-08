using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2024.Puzzles;

namespace AOC2024.Tests.Puzzles
{
    public class Day6Part1Tests
    {
        [Fact]
        public void NameField_WhenCalled_ReturnsProperFormat()
        {
            CommonTests.ValidateNameField<Day6Part1>();
        }

        [Fact]
        public void GetOutput_WhenCalledWithExample_ReturnsExampleAnswer()
        {
            CommonTests.ValidateOutput<Day6Part1>(true);
        }

        [Fact]
        public void GetOutput_WhenCalledWithoutExample_ReturnsAnswer()
        {
            CommonTests.ValidateOutput<Day6Part1>(false);
        }

        [Fact]
        public void GetCharAt_WhenCalledWithValid_ReturnsProperAnswer()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { 'A', 'B', 'C', 'D' },
                new[] { 'E', 'F', 'G', 'H' },
                new[] { 'I', 'J', 'K', 'L' },
            };

            var point = new Point(1, 1);
            var expected = 'F';

            // Act
            var result = Day6Part1.GetCharAt(map, point);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCharAt_WhenCalledWithInValid_ReturnsNull()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { 'A', 'B', 'C', 'D' },
                new[] { 'E', 'F', 'G', 'H' },
                new[] { 'I', 'J', 'K', 'L' },
            };

            var point = new Point(6, 1);

            // Act
            var result = Day6Part1.GetCharAt(map, point);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCharAt_WhenCalledGoingUpCollision_ReturnsRight()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '.', '^', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var currPos = new Point(2, 3);
            var expected = Day6Part1.Day6Part1Direction.Right;

            // Act
            var result = Day6Part1.GetNextDirection(map, currPos, Day6Part1.Day6Part1Direction.Up);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void GetStartingPos_WhenCalled_ReturnsProperValue()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '^', '.', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var expected = new Point(1, 3);

            // Act
            var result = Day6Part1.GetStartingPos(map);

            // Assert
            Assert.Equal(expected, result);
        }

        //TODO: Add tests for other directions
        [Fact]
        public void GetNextPosition_WhenCalledGoingUp_ReturnsProperValue()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '^', '.', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var currPos = new Point(1, 3);
            var direction = Day6Part1.Day6Part1Direction.Up;

            var expected = new Point(1, 2);

            // Act
            var result = Day6Part1.GetNextPosition(map, currPos, direction);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNextPosition_WhenCalledGoingOOB_ReturnsNull()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '.', '^', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var currPos = new Point(1, 0);
            var direction = Day6Part1.Day6Part1Direction.Up;

            // Act
            var result = Day6Part1.GetNextPosition(map, currPos, direction);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void InBounds_WhenCalledInBounds_ReturnsTrue()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '.', '^', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var inBoundsPoint = new Point(3, 0);

            // Act
            var result = Day6Part1.InBounds(map, inBoundsPoint);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InBounds_WhenCalledOutOfBounds_ReturnsFalse()
        {
            // Arrange
            var map = new List<char[]>()
            {
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '.', '.' },
                new[] { '.', '.', '#', '.' },
                new[] { '.', '.', '^', '.' },
                new[] { '.', '.', '.', '.' },
            };

            var inBoundsPoint = new Point(4, 4);

            // Act
            var result = Day6Part1.InBounds(map, inBoundsPoint);

            // Assert
            Assert.False(result);
        }
    }
}
