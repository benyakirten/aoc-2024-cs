namespace csharp;

public class DayTwo
{
    private const int MinDifference = 1;
    private const int MaxDifference = 3;
    private readonly List<List<int>> _reports = ReadInput();

    public void Solve()
    {
        Console.WriteLine("Day Two");

        var partOne = PartOne(_reports);
        Console.WriteLine($"Part one: {partOne}");

        var partTwo = PartTwo(_reports);
        Console.WriteLine($"Part Two: {partTwo}");
    }

    private int PartOne(List<List<int>> lists)
    {
        return lists.Count(list => IsReportSafe(list, out var _));
    }

    private static bool IsReportSafe(List<int> report, out int unsafeFloorIndex)
    {
        unsafeFloorIndex = -1;

        var (previousLevel, positiveTrend) = GetReportInitialData(report);
        if (!positiveTrend.HasValue)
        {
            unsafeFloorIndex = 0;
            return false;
        }

        for (var i = 1; i < report.Count; i++)
        {
            var level = report[i];

            var isSafe = IsLevelSafe(positiveTrend.Value, level, previousLevel);
            if (!isSafe)
            {
                unsafeFloorIndex = i;
                return false;
            }

            previousLevel = level;
        }

        return true;
    }

    private static (int FirstLevel, bool? PositiveTrend) GetReportInitialData(List<int> report)
    {
        var firstLevel = report.First();
        var secondLevel = report[1];

        if (secondLevel == firstLevel) return (firstLevel, null);

        var positiveTrend = secondLevel - firstLevel > 0;
        return (firstLevel, positiveTrend);
    }

    private static bool IsLevelSafe(bool positiveTrend, int currentLevel, int previousLevel)
    {
        if (currentLevel == previousLevel) return false;

        var diff = currentLevel - previousLevel;
        if ((positiveTrend && diff < 0) || (!positiveTrend && diff > 0)) return false;

        diff = Math.Abs(diff);
        return diff is <= MaxDifference and >= MinDifference;
    }

    private int PartTwo(List<List<int>> lists)
    {
        Console.WriteLine("Part 2");
        return lists.Count(IsReportSafeWithSafety);
    }

    private static bool IsReportSafeWithSafety(List<int> report)
    {
        var isSafe = IsReportSafe(report, out var unsafeFloorIndex);
        if (isSafe) return true;

        for (var i = 0; i <= unsafeFloorIndex + 1 && i < report.Count; i++)
        {
            var reportWithoutLevel = ExcludeIndexFromReport(report, i);
            if (IsReportSafe(reportWithoutLevel, out var _)) return true;
        }

        return false;
    }


    private static List<int> ExcludeIndexFromReport(List<int> report, int index)
    {
        var firstHalf = report.Slice(0, index);
        var secondHalf = report.Slice(index + 1, report.Count - index - 1);

        return firstHalf.Concat(secondHalf).ToList();
    }

    private static List<List<int>> ReadInput()
    {
        var input = Utilities.GetDayInput(2);

        return File.ReadLines(input)
            .Select(line => line.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToList())
            .ToList();
    }
}