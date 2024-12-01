namespace AOC2024
{
    public interface IAdventPuzzle
    {
        public string Name { get; }

        public string? Solution { get; }

        public bool ExampleRun { get; set; }
        public string? ExampleSolution { get; }

        public PuzzleOutput GetOutput();
    }
}
