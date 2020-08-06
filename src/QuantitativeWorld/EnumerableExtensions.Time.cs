using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Time Average(this IEnumerable<Time> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Time.Zero;
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

        public static Time Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Time> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Time.Zero;
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

        public static Time Max(this IEnumerable<Time> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Time.Zero;
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

        public static Time? Max(this IEnumerable<Time?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Time? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Time Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Time> selector) =>
            source.Select(selector).Max();

        public static Time? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Time?> selector) =>
            source.Select(selector).Max();

        public static Time Min(this IEnumerable<Time> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Time.Zero;
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

        public static Time? Min(this IEnumerable<Time?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Time? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Time Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Time> selector) =>
            source.Select(selector).Min();

        public static Time? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Time?> selector) =>
            source.Select(selector).Min();

        public static Time Sum(this IEnumerable<Time> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Time), (acc, e) => acc + e);
        }

        public static Time Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Time> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Time), (acc, e) => acc + selector(e));
        }
    }
}
