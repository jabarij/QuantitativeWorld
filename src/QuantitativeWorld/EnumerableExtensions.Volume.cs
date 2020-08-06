using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Volume Average(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Volume.Zero;
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

        public static Volume Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Volume.Zero;
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

        public static Volume Max(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Volume.Zero;
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

        public static Volume? Max(this IEnumerable<Volume?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Volume? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Volume Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector) =>
            source.Select(selector).Max();

        public static Volume? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume?> selector) =>
            source.Select(selector).Max();

        public static Volume Min(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Volume.Zero;
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

        public static Volume? Min(this IEnumerable<Volume?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Volume? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Volume Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector) =>
            source.Select(selector).Min();

        public static Volume? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume?> selector) =>
            source.Select(selector).Min();

        public static Volume Sum(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Volume), (acc, e) => acc + e);
        }

        public static Volume Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Volume> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Volume), (acc, e) => acc + selector(e));
        }
    }
}
