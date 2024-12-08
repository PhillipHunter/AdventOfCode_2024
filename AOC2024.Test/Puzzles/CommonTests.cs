using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2024.Tests.Puzzles
{
    public class CommonTests
    {
        public static void ValidateNameField<T>()
            where T : class, new()
        {
            // Arrange
            var classRegex = new Regex(@"Day(\d+)Part(\d+)");
            var className = typeof(T).Name;
            var classMatch = classRegex.Matches(className).First();
            var day = classMatch.Groups[1];
            var part = classMatch.Groups[2];

            var nameRegex = new Regex($"Day {day}: .+ Part {part}");

            dynamic? sut = Activator.CreateInstance(typeof(T), true);
            sut.ExampleRun = true;

            // Act
            var result = nameRegex.Match(sut.Name).Success;

            // Assert
            Assert.True(result);
        }

        public static void ValidateOutput<T>(bool example)
            where T : class, new()
        {
            // Arrange
            dynamic? sut = Activator.CreateInstance(typeof(T), true);
            sut.ExampleRun = example;

            // Act
            var output = sut.GetOutput();

            // Assert
            Assert.Equal(example ? sut.ExampleSolution : sut.Solution, output.Result);
        }
    }
}
