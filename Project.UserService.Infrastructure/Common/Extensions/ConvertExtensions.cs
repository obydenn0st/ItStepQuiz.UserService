using System.Text;

namespace Project.UserService.Common.Extensions;

public static class ConvertExtensions
{
    public static byte[] ConvertToBytes(this string source)
    {
        return Enumerable.Range(0, source.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(source.Substring(x, 2), 16))
            .ToArray();
    }

    public static string ConvertToHex(this byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2 + 2);

        foreach (var b in bytes)
            sb.Append($"{b:X2}");

        return sb.ToString();
    }
}