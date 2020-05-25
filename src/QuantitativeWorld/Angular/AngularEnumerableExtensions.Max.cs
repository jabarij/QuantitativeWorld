﻿using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
    partial class AngularEnumerableExtensions
    {
        public static Angle Max(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(Angle);
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

        public static DegreeAngle Max(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(DegreeAngle);
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

        public static DegreeAngle? Max(this IEnumerable<DegreeAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            DegreeAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static DegreeAngle Max<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector) =>
            source.Select(selector).Max();

        public static DegreeAngle? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle?> selector) =>
            source.Select(selector).Max();

        public static RadianAngle Max(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));

            var value = default(RadianAngle);
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

        public static RadianAngle? Max(this IEnumerable<RadianAngle?> source)
        {
            Assert.IsNotNull(source, nameof(source));

            RadianAngle? value = null;
            foreach (var element in source)
            {
                if (value == null || element > value)
                    value = element;
            }
            return value;
        }

        public static RadianAngle Max<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector) =>
            source.Select(selector).Max();

        public static RadianAngle? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle?> selector) =>
            source.Select(selector).Max();
    }
}