using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
    partial class AngularEnumerableExtensions
    {
        public static Angle Average(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var enumerator = source.GetEnumerator();
            var sum = Angle.Zero;
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

        public static Angle Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            var enumerator = source.GetEnumerator();
            var sum = Angle.Zero;
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

        public static Angle Max(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Angle.Zero;
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

        public static Angle? Max(this IEnumerable<Angle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Angle? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static Angle Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector) =>
            source.Select(selector).Max();

        public static Angle? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle?> selector) =>
            source.Select(selector).Max();

        public static Angle Min(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = Angle.Zero;
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

        public static Angle? Min(this IEnumerable<Angle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            Angle? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static Angle Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector) =>
            source.Select(selector).Min();

        public static Angle? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle?> selector) =>
            source.Select(selector).Min();

        public static Angle Sum(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Angle), (acc, e) => acc + e);
        }

        public static Angle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Angle), (acc, e) => acc + selector(e));
        }
    }
}
