using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Power Average(this IEnumerable<Power> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Power.Zero;
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

        public static Power Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Power> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Power.Zero;
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

        public static Power Max(this IEnumerable<Power> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Power.Zero;
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

        public static Power? Max(this IEnumerable<Power?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Power? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Power Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Power> selector) =>
            source.Select(selector).Max();

        public static Power? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Power?> selector) =>
            source.Select(selector).Max();

        public static Power Min(this IEnumerable<Power> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Power.Zero;
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

        public static Power? Min(this IEnumerable<Power?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Power? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Power Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Power> selector) =>
            source.Select(selector).Min();

        public static Power? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Power?> selector) =>
            source.Select(selector).Min();

        public static Power Sum(this IEnumerable<Power> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Power), (acc, e) => acc + e);
        }

        public static Power Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Power> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Power), (acc, e) => acc + selector(e));
        }
    }
}
