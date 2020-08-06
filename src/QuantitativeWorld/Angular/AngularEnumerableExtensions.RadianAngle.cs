using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
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

        public static RadianAngle Max(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = RadianAngle.Zero;
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

        public static RadianAngle? Max(this IEnumerable<RadianAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            RadianAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static RadianAngle Max<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector) =>
            source.Select(selector).Max();

        public static RadianAngle? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle?> selector) =>
            source.Select(selector).Max();

        public static RadianAngle Min(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = RadianAngle.Zero;
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

        public static RadianAngle? Min(this IEnumerable<RadianAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            RadianAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static RadianAngle Min<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector) =>
            source.Select(selector).Min();

        public static RadianAngle? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle?> selector) =>
            source.Select(selector).Min();

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
