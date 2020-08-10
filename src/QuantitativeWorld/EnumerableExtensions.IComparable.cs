using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuantitativeWorld
{
    partial class EnumerableExtensions
    {
        public static TSource Max<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource max;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                max = element.Current;
                while (element.MoveNext())
                {
                    TSource value = element.Current;
                    if (max.CompareTo(value) < 0)
                        max = value;
                }
            }

            return max;
        }

        public static TSource? Max<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource? maxNullable = null;
            using (IEnumerator<TSource?> element = source.GetEnumerator())
            {
                do
                {
                    if (!element.MoveNext())
                        return maxNullable;

                    maxNullable = element.Current;
                }
                while (!maxNullable.HasValue);

                TSource max = maxNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TSource? valueNullable = element.Current;
                    TSource value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & max.CompareTo(value) < 0)
                    {
                        max = value;
                        maxNullable = valueNullable;
                    }
                }
            }

            return maxNullable;
        }

        public static TElement Max<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> selector)
            where TElement : IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement max;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                max = selector(element.Current);
                while (element.MoveNext())
                {
                    TElement value = selector(element.Current);
                    if (max.CompareTo(value) < 0)
                        max = value;
                }
            }

            return max;
        }

        public static TElement? Max<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement?> selector)
            where TElement : struct, IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement? maxNullable = null;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                do
                {
                    if (!element.MoveNext())
                        return maxNullable;

                    maxNullable = selector(element.Current);
                }
                while (!maxNullable.HasValue);

                TElement max = maxNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TElement? valueNullable = selector(element.Current);
                    TElement value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & max.CompareTo(value) < 0)
                    {
                        max = value;
                        maxNullable = valueNullable;
                    }
                }
            }

            return maxNullable;
        }

        public static TSource Min<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource min;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                min = element.Current;
                while (element.MoveNext())
                {
                    TSource value = element.Current;
                    if (min.CompareTo(value) > 0)
                        min = value;
                }
            }

            return min;
        }

        public static TSource? Min<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource? minNullable = null;
            using (IEnumerator<TSource?> element = source.GetEnumerator())
            {
                do
                {
                    if (!element.MoveNext())
                        return minNullable;

                    minNullable = element.Current;
                }
                while (!minNullable.HasValue);

                TSource min = minNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TSource? valueNullable = element.Current;
                    TSource value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & min.CompareTo(value) > 0)
                    {
                        min = value;
                        minNullable = valueNullable;
                    }
                }
            }

            return minNullable;
        }

        public static TElement Min<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> selector)
            where TElement : IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement min;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                min = selector(element.Current);
                while (element.MoveNext())
                {
                    TElement value = selector(element.Current);
                    if (min.CompareTo(value) > 0)
                        min = value;
                }
            }

            return min;
        }

        public static TElement? Min<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement?> selector)
            where TElement : struct, IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement? minNullable = null;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                do
                {
                    if (!element.MoveNext())
                        return minNullable;

                    minNullable = selector(element.Current);
                }
                while (!minNullable.HasValue);

                TElement min = minNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TElement? valueNullable = selector(element.Current);
                    TElement value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & min.CompareTo(value) > 0)
                    {
                        min = value;
                        minNullable = valueNullable;
                    }
                }
            }

            return minNullable;
        }

        public static (TSource min, TSource max) Extremes<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource min, max;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                min = element.Current;
                max = element.Current;
                while (element.MoveNext())
                {
                    TSource value = element.Current;
                    if (min.CompareTo(value) > 0)
                        min = value;
                    else if (max.CompareTo(value) < 0)
                        max = value;
                }
            }

            return (min, max);
        }

        public static (TSource? min, TSource? max) Extremes<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct, IComparable<TSource>
        {
            Assert.IsNotNull(source, nameof(source));

            TSource? minNullable = null;
            TSource? maxNullable = null;
            using (IEnumerator<TSource?> element = source.GetEnumerator())
            {
                TSource? firstNonNull = null;
                do
                {
                    if (!element.MoveNext())
                        return (firstNonNull, firstNonNull);

                    firstNonNull = element.Current;
                }
                while (!firstNonNull.HasValue);

                minNullable = firstNonNull;
                maxNullable = firstNonNull;
                TSource min = minNullable.GetValueOrDefault();
                TSource max = maxNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TSource? valueNullable = element.Current;
                    TSource value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & min.CompareTo(value) > 0)
                    {
                        min = value;
                        minNullable = valueNullable;
                    }
                    else if (valueNullable.HasValue & max.CompareTo(value) < 0)
                    {
                        max = value;
                        maxNullable = valueNullable;
                    }
                }
            }

            return (minNullable, maxNullable);
        }

        public static (TElement min, TElement max) Extremes<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> selector)
            where TElement : IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement min, max;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                if (!element.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                min = selector(element.Current);
                max = selector(element.Current);
                while (element.MoveNext())
                {
                    TElement value = selector(element.Current);
                    if (min.CompareTo(value) > 0)
                        min = value;
                    else if (max.CompareTo(value) < 0)
                        max = value;
                }
            }

            return (min, max);
        }

        public static (TElement? min, TElement? max) Extremes<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement?> selector)
            where TElement : struct, IComparable<TElement>
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));

            TElement? minNullable = null;
            TElement? maxNullable = null;
            using (IEnumerator<TSource> element = source.GetEnumerator())
            {
                TElement? firstNonNull = null;
                do
                {
                    if (!element.MoveNext())
                        return (firstNonNull, firstNonNull);

                    firstNonNull = selector(element.Current);
                }
                while (!firstNonNull.HasValue);

                minNullable = firstNonNull;
                maxNullable = firstNonNull;
                TElement min = minNullable.GetValueOrDefault();
                TElement max = maxNullable.GetValueOrDefault();
                while (element.MoveNext())
                {
                    TElement? valueNullable = selector(element.Current);
                    TElement value = valueNullable.GetValueOrDefault();

                    // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                    // unless nulls either never happen or always happen.
                    if (valueNullable.HasValue & min.CompareTo(value) > 0)
                    {
                        min = value;
                        minNullable = valueNullable;
                    }
                    else if (valueNullable.HasValue & max.CompareTo(value) < 0)
                    {
                        max = value;
                        maxNullable = valueNullable;
                    }
                }
            }

            return (minNullable, maxNullable);
        }
    }
}
