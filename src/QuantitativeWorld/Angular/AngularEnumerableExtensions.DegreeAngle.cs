using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
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

        public static DegreeAngle Max(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = DegreeAngle.Zero;
            bool hasValue = false;
            foreach (var element in source)
            {
                if (hasValue)
                {
                    if (element > value)
                        value = element;
                }
                else
                {
                    value = element;
                    hasValue = true;
                }
            }

            if (!hasValue)
                throw new InvalidOperationException("Sequence contains no elements.");
            return value;
        }

        public static DegreeAngle? Max(this IEnumerable<DegreeAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            DegreeAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static DegreeAngle Max<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector) =>
            source.Select(selector).Max();

        public static DegreeAngle? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle?> selector) =>
            source.Select(selector).Max();

        public static DegreeAngle Min(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = DegreeAngle.Zero;
            bool hasValue = false;
            foreach (var element in source)
            {
                if (hasValue)
                {
                    if (element < value)
                        value = element;
                }
                else
                {
                    value = element;
                    hasValue = true;
                }
            }

            if (!hasValue)
                throw new InvalidOperationException("Sequence contains no elements.");
            return value;
        }

        public static DegreeAngle? Min(this IEnumerable<DegreeAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            DegreeAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static DegreeAngle Min<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector) =>
            source.Select(selector).Min();

        public static DegreeAngle? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle?> selector) =>
            source.Select(selector).Min();

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
