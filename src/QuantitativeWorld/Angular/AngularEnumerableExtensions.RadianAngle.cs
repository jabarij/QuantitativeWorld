using Common.Internals.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
#else
namespace QuantitativeWorld.Angular
{
#endif
    partial class AngularEnumerableExtensions
    {
        public static RadianAngle Average(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = RadianAngle.Zero;
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

        public static RadianAngle Average<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = RadianAngle.Zero;
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

        public static RadianAngle Sum(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(RadianAngle), (acc, e) => acc + e);
        }

        public static RadianAngle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(RadianAngle), (acc, e) => acc + selector(e));
        }
    }
}
