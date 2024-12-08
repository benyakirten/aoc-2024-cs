using AppDomain = System.AppDomain;

namespace csharp;

public static class Utilities
{
    public static string GetDayInput(int day)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Directory.GetParent(basePath)!.Parent!.Parent!.Parent!.FullName;

        return Path.Combine(Path.Combine(filePath, "Data"), $"day_{day}.txt");
    }
}