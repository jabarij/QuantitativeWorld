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
        public static DegreeAngle Average(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = DegreeAngle.Zero;
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

        public static DegreeAngle Average<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = DegreeAngle.Zero;
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

        public static DegreeAngle Sum(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(DegreeAngle), (acc, e) => acc + e);
        }

        public static DegreeAngle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(DegreeAngle), (acc, e) => acc + selector(e));
        }
    }
}
