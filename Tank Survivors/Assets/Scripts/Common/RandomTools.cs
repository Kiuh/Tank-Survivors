using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class RandomTools
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T TakeRandom<T>(this List<T> list)
        {
            T item = list.GetRandom();
            _ = list.Remove(item);
            return item;
        }

        public static List<T> TakeRandom<T>(this List<T> list, uint count)
        {
            if (list.Count <= count)
            {
                return list;
            }

            List<T> result = new();
            for (int i = 0; i < count; i++)
            {
                result.Add(list.TakeRandom());
            }
            return result;
        }
    }
}
