using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
    partial class AngularEnumerableExtensions
    {
        public static Angle Min(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Angle);
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

        public static DegreeAngle Min(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(DegreeAngle);
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

        public static DegreeAngle? Min(this IEnumerable<DegreeAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            DegreeAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static DegreeAngle Min<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector) =>
            source.Select(selector).Min();

        public static DegreeAngle? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle?> selector) =>
            source.Select(selector).Min();

        public static RadianAngle Min(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(RadianAngle);
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

        public static RadianAngle? Min(this IEnumerable<RadianAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            RadianAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element < value)
                    value = element;
            }
            return value;
        }

        public static RadianAngle Min<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector) =>
            source.Select(selector).Min();

        public static RadianAngle? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle?> selector) =>
            source.Select(selector).Min();
    }
}
