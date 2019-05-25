using System;
using System.Collections.Generic;

namespace Blazor.Client.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list, int? seed = null)
        {
            var random = seed == null ? new Random() : new Random(seed.Value);

            var count = list.Count;
            for (var i = 0; i < count - 1; ++i) 
            {
                var r = random.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }
    }
}