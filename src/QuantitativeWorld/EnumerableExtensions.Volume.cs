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
        public static Volume Average(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Volume);
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

        public static Volume Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Volume);
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

        public static Volume Sum(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Volume), (acc, e) => acc + e);
        }

        public static Volume Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Volume), (acc, e) => acc + selector(e));
        }
    }
}
