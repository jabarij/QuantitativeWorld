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
        public static Energy Average(this IEnumerable<Energy> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Energy);
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

        public static Energy Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Energy);
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

        public static Energy Sum(this IEnumerable<Energy> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Energy), (acc, e) => acc + e);
        }

        public static Energy Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Energy), (acc, e) => acc + selector(e));
        }
    }
}
