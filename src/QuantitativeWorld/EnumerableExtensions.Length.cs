using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Length Average(this IEnumerable<Length> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = default(Length);
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

        public static Length Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Length> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = default(Length);
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

        public static Length Max(this IEnumerable<Length> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Length);
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

        public static Length? Max(this IEnumerable<Length?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Length? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Length Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Length> selector) =>
            source.Select(selector).Max();

        public static Length? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Length?> selector) =>
            source.Select(selector).Max();

        public static Length Min(this IEnumerable<Length> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Length);
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

        public static Length? Min(this IEnumerable<Length?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Length? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Length Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Length> selector) =>
            source.Select(selector).Min();

        public static Length? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Length?> selector) =>
            source.Select(selector).Min();

        public static Length Sum(this IEnumerable<Length> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Length), (acc, e) => acc + e);
        }

        public static Length Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Length> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Length), (acc, e) => acc + selector(e));
        }
    }
}
