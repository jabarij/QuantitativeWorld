using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    public static class EnumerableExtensions
    {
        public static Weight Sum(this IEnumerable<Weight> source) =>
            throw new NotImplementedException();

        public static Weight Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector) =>
            throw new NotImplementedException();
    }
}
