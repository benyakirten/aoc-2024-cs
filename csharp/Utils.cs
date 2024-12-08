using AppDomain = System.AppDomain;

namespace csharp;

public static class Utilities
{
    public static string GetRelativePath(params string[] parts)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Directory.GetParent(basePath)!.Parent!.Parent!.Parent!.FullName;

        return parts.Aggregate(filePath, Path.Combine);
    }
}