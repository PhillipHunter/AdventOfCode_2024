namespace AOC2024
{
    public interface IAdventPuzzle
    {
        public string Name { get; }

        public string? Solution { get; }

        public PuzzleOutput GetOutput();
    }
}
