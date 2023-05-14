using Newtonsoft.Json;

namespace Project.UserService.Core.Common.Extensions;

public static class TypeExtensions
{
    public static string ToJson<TSource>(this TSource source)
        where TSource : class
    {
        return JsonConvert.SerializeObject(source);
    }

    public static TTarget? ConvertTo<TTarget>(this string json)
    {
        return JsonConvert.DeserializeObject<TTarget>(json);
    }
}