using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    public static class EnumerableExtensions
    {
        public static Weight Sum(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Weight), (acc, e) => acc + e);
        }

        public static Weight Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Weight), (acc, e) => acc + selector(e));
        }
    }
}
