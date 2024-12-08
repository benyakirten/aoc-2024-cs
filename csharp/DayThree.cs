namespace csharp;

public class DayThree
{
    private readonly string _input;

    public DayThree()
    {
        var path = Utilities.GetDayInput(3);
        _input = File.ReadAllText(path);
    }

    public void Solve()
    {
        Console.WriteLine("Day 3");

        var partOne = Part1();
        Console.WriteLine($"Part One: {partOne}");

        var partTwo = Part2();
        Console.WriteLine($"Part Two: {partTwo}");
    }

    private int Part1()
    {
        StringReader sr = new(_input);
        var sum = 0;
        int letter;
        while ((letter = sr.Read()) > 0)
            if (letter == 'm')
            {
                var nums = AttemptParseMultiplier(sr);
                if (nums != null) sum += nums.Aggregate((x, y) => x * y);
            }

        return sum;
    }


    private static int[]? AttemptParseMultiplier(StringReader sr)
    {
        var expectedChars = new[] { 'u', 'l', '(' };
        var expectedCharIndex = 0;
        var isParsingNum1 = true;
        int? num1 = null;
        int? num2 = null;

        int letter;
        while ((letter = sr.Read()) > 0)
        {
            if (expectedCharIndex < expectedChars.Length)
            {
                var expectedChar = expectedChars[expectedCharIndex];
                if (letter != expectedChar) break;

                expectedCharIndex++;
                continue;
            }

            switch (letter)
            {
                case ')' when num1.HasValue && num2.HasValue:
                    return new[] { num1.Value, num2.Value };
                case ',' when num1.HasValue:
                    isParsingNum1 = false;
                    break;
                case var val when val is >= '0' and <= '9':
                    val -= '0';
                    if (isParsingNum1)
                    {
                        num1 ??= 0;
                        num1 = num1 * 10 + int.Parse(val.ToString());
                    }
                    else
                    {
                        num2 ??= 0;
                        num2 = num2 * 10 + int.Parse(val.ToString());
                    }

                    break;
                default:
                    return null;
            }
        }

        return null;
    }

    private int Part2()
    {
        StringReader sr = new(_input);

        var sum = 0;
        var ignoreNextMul = false;
        int letter;
        while ((letter = sr.Read()) > 0)
            switch (letter)
            {
                case 'd':
                    ignoreNextMul = ShouldIgnoreNextMul(sr);
                    break;
                case 'm':
                {
                    var nums = AttemptParseMultiplier(sr);
                    if (nums == null) continue;

                    if (ignoreNextMul) continue;

                    sum += nums.Aggregate((x, y) => x * y);
                    break;
                }
            }

        return sum;
    }

    private static bool ShouldIgnoreNextMul(StringReader sr)
    {
        var expectedChars = new[] { 'o', 'n', '\'', 't' };
        var expectedCharIndex = 0;

        int letter;
        while ((letter = sr.Read()) > 0 && expectedCharIndex < expectedChars.Length)
        {
            var expectedChar = expectedChars[expectedCharIndex];
            if (letter != expectedChar) return false;

            expectedCharIndex++;
        }

        return true;
    }
}