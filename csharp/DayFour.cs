namespace csharp;

public class DayFour
{
    private readonly List<string> Grid;

    public DayFour()
    {
        var path = Utilities.GetDayInput(4);
        Grid = ReadInput(path);
    }

    private static List<string> ReadInput(string path)
    {
        return File.ReadLines(path).ToList();
    }

    public void Solve()
    {
        var partOne = Part1();
        Console.WriteLine($"Part 1: {partOne}");

        var partTwo = Part2();
        Console.WriteLine($"Part Two: ${partTwo}");
    }

    private int Part1()
    {
        return 0;
    }

    private int Part2()
    {
        return 0;
    }
}