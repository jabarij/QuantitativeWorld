using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
    partial class AngularEnumerableExtensions
    {
        public static Angle Average(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Angle.Zero;
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

        public static Angle Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Angle.Zero;
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

        public static Angle Sum(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Angle), (acc, e) => acc + e);
        }

        public static Angle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Angle), (acc, e) => acc + selector(e));
        }
    }
}
