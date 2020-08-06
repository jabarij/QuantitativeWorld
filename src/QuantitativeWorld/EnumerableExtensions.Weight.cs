using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Weight Average(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Weight.Zero;
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

        public static Weight Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Weight.Zero;
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

        public static Weight Max(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Weight.Zero;
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

        public static Weight? Max(this IEnumerable<Weight?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Weight? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Weight Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector) =>
            source.Select(selector).Max();

        public static Weight? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight?> selector) =>
            source.Select(selector).Max();

        public static Weight Min(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Weight.Zero;
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

        public static Weight? Min(this IEnumerable<Weight?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Weight? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Weight Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector) =>
            source.Select(selector).Min();

        public static Weight? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight?> selector) =>
            source.Select(selector).Min();

        public static Weight Sum(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Weight), (acc, e) => acc + e);
        }

        public static Weight Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Weight), (acc, e) => acc + selector(e));
        }
    }
}
