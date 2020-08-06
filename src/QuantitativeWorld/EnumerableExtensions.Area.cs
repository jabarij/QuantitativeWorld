using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static Area Average(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Area.Zero;
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

        public static Area Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Area.Zero;
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

        public static Area Max(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Area.Zero;
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

        public static Area? Max(this IEnumerable<Area?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Area? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Area Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector) =>
            source.Select(selector).Max();

        public static Area? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Area?> selector) =>
            source.Select(selector).Max();

        public static Area Min(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Area.Zero;
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

        public static Area? Min(this IEnumerable<Area?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Area? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Area Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector) =>
            source.Select(selector).Min();

        public static Area? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Area?> selector) =>
            source.Select(selector).Min();

        public static Area Sum(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Area), (acc, e) => acc + e);
        }

        public static Area Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Area> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Area), (acc, e) => acc + selector(e));
        }
    }
}
