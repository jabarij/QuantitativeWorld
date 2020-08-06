using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
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

        public static Speed Max(this IEnumerable<Speed> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Speed);
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

        public static Speed? Max(this IEnumerable<Speed?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Speed? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Speed Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed> selector) =>
            source.Select(selector).Max();

        public static Speed? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed?> selector) =>
            source.Select(selector).Max();

        public static Speed Min(this IEnumerable<Speed> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Speed);
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

        public static Speed? Min(this IEnumerable<Speed?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Speed? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Speed Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed> selector) =>
            source.Select(selector).Min();

        public static Speed? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Speed?> selector) =>
            source.Select(selector).Min();

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
