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
        public static Weight Average(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Weight);
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

        public static Weight Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Weight);
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
