using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
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

        public static Energy Max(this IEnumerable<Energy> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Energy);
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

        public static Energy? Max(this IEnumerable<Energy?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Energy? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Energy Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy> selector) =>
            source.Select(selector).Max();

        public static Energy? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy?> selector) =>
            source.Select(selector).Max();

        public static Energy Min(this IEnumerable<Energy> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Energy);
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

        public static Energy? Min(this IEnumerable<Energy?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Energy? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Energy Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy> selector) =>
            source.Select(selector).Min();

        public static Energy? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Energy?> selector) =>
            source.Select(selector).Min();

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
