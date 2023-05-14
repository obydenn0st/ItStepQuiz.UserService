namespace Project.UserService.Core.Common.Extensions;

public static class EqualityExtensions
{
    public static bool IsNullOrEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    public static bool IsNullOrEmpty<T>(this T[] source)
    {
        return source == null || source.Length == 0;
    }
}