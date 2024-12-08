namespace csharp;

public class DayTemplate
{
    private readonly string _input;

    public DayTemplate()
    {
        var path = Utilities.GetDayInput(3);
        _input = File.ReadAllText(path);
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