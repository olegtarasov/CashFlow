namespace ZenMoneyPlus.Helpers;

internal static class CollectionHelpers
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TValue> valueFunc)
    {
        if (!dic.TryGetValue(key, out var value))
        {
            value = valueFunc();
            dic.Add(key, value);
        }

        return value;
    }

    public static void AddOrUpdate<TKey, TValue>(
        this IDictionary<TKey, TValue> dic,
        TKey key,
        Func<TValue> newValue,
        Func<TValue, TValue> updateValue)
    {
        dic[key] = dic.TryGetValue(key, out var value) ? updateValue(value) : newValue();
    }

    public static IEnumerable<(T, int)> Enumerate<T>(this IEnumerable<T> source)
    {
        return source.Select((item, i) => (item, i));
    }
}