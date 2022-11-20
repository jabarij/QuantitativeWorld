using Common.Internals.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial class EnumerableExtensions
    {
        public static Area Average(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Area);
            int count = 0;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                count++;
            }

            if (count == 0)
                throw new InvalidOperationException("Sequence contains no elements.");

            return sum / count;
        }

        public static Area Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Area);
            int count = 0;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                count++;
            }

            if (count == 0)
                throw new InvalidOperationException("Sequence contains no elements.");

            return sum / count;
        }

        public static Area Sum(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Area), (acc, e) => acc + e);
        }

        public static Area Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Area), (acc, e) => acc + selector(e));
        }
    }
}
