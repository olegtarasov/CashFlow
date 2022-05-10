namespace ZenMoneyPlus.Helpers;

public static class CollectionHelpers
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }
}