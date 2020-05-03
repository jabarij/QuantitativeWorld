using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
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

        public static Area Min(this IEnumerable<Area> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Area);
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

        public static Volume Min(this IEnumerable<Volume> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Volume);
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

        public static Power Min(this IEnumerable<Power> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Power);
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

        public static Weight Min(this IEnumerable<Weight> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Weight);
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
    }
}
