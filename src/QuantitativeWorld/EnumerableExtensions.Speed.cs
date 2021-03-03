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
        public static Speed Average(this IEnumerable<Speed> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Speed);
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

        public static Speed Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Speed);
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

        public static Speed Sum(this IEnumerable<Speed> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Speed), (acc, e) => acc + e);
        }

        public static Speed Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Speed), (acc, e) => acc + selector(e));
        }
    }
}
