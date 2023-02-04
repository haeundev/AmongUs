using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Extensions
{
    public static class EnumerableExtensions
    {
        public static T PeekRandom<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing<T>(new Random());
        }

        private static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            var index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}