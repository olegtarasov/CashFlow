using System.Collections.Generic;
using System.Linq;

namespace ZenMoneyPlus
{
    public static class CollectionHelpers
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}