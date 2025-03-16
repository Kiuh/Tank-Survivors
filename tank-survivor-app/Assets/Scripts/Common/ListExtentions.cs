using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class ListExtensions
    {
        public static U GetConcrete<U, T>(this IEnumerable<T> list)
            where U : class
        {
            T found = list.FirstOrDefault(x => x is U);
            return found == null ? null : found as U;
        }
    }
}
