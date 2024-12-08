namespace csharp.DayOne;

public class DayOne
{
    public void Solve()
    {
        Console.WriteLine("Day One");

        var lists = ReadInput();

        var partOne = PartOne(lists);
        Console.WriteLine($"Part One: {partOne}");
        
        var partTwo = PartTwo(lists);
        Console.WriteLine($"Part Two: {partTwo}");
    }

    private static int PartOne((List<int>, List<int>) lists)
    {
        lists.Item1.Sort();
        lists.Item2.Sort();

        var sum = 0;

        for (var i = 0; i < lists.Item1.Count; i++)
        {
            var left = lists.Item1[i];
            var right = lists.Item2[i];

            var difference = Math.Abs(left - right);

            sum += difference;
        }

        return sum;
    }

    private static int PartTwo((List<int>, List<int>) lists)
    {
        var total = 0;
        var cache = new Dictionary<int, int>();

        foreach (var leftItem in lists.Item1)
        {
            if (cache.TryGetValue(leftItem, out var sum))
            {
                total += sum;
                continue;
            }

            var count = 0;
            foreach (var rightItem in lists.Item2)
                if (rightItem == leftItem)
                    count++;

            sum = count * leftItem;
            cache[leftItem] = sum;

            total += sum;
        }

        return total;
    }

    private (List<int>, List<int>) ReadInput()
    {
        var leftList = new List<int>();
        var rightList = new List<int>();

        var path = Utilities.GetRelativePath("Data", "day_one.txt");

        foreach (var line in File.ReadAllLines(path))
        {
            var values = line.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(digits => digits.Trim())
                .Select(int.Parse)
                .ToArray();

            if (values.Length != 2) throw new Exception($"Invalid input: {line}");

            leftList.Add(values[0]);
            rightList.Add(values[1]);
        }

        return (leftList, rightList);
    }
}