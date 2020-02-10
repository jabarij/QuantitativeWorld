﻿using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
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
            return source.Aggregate(default(Weight), (acc, e) => acc + e);
        }

        public static Weight Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Weight> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Weight), (acc, e) => acc + selector(e));
        }

        public static Length Average(this IEnumerable<Length> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Length), (acc, e) => acc + e);
        }

        public static Length Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Length> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Length), (acc, e) => acc + selector(e));
        }

        public static TQuantity Average<TQuantity, TUnit>(this IEnumerable<TQuantity> source, Func<decimal, TUnit, TQuantity> factory)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(factory, nameof(factory));
            return Average(source.GetEnumerator(), factory, e => e);
        }

        public static TQuantity Average<TQuantity, TUnit>(this IEnumerable<TQuantity> source, ILinearQuantityFactory<TQuantity, TUnit> factory)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(factory, nameof(factory));
            return Average<TQuantity, TQuantity, TUnit>(source.GetEnumerator(), factory.Create, e => e);
        }

        public static TQuantity Average<TSource, TQuantity, TUnit>(this IEnumerable<TSource> source, Func<decimal, TUnit, TQuantity> factory, Func<TSource, TQuantity> selector)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(factory, nameof(factory));
            Assert.IsNotNull(selector, nameof(selector));
            return Average(source.GetEnumerator(), factory, selector);
        }

        public static TQuantity Average<TSource, TQuantity, TUnit>(this IEnumerable<TSource> source, ILinearQuantityFactory<TQuantity, TUnit> factory, Func<TSource, TQuantity> selector)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(factory, nameof(factory));
            Assert.IsNotNull(selector, nameof(selector));
            return Average<TSource, TQuantity, TUnit>(source.GetEnumerator(), factory.Create, selector);
        }

        private static TQuantity Average<TSource, TQuantity, TUnit>(IEnumerator<TSource> enumerator, Func<decimal, TUnit, TQuantity> factory, Func<TSource, TQuantity> selector)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            bool hasElements = enumerator.MoveNext();
            if (!hasElements)
                return default(TQuantity);

            var targetUnit = selector(enumerator.Current).Unit;
            decimal valueInBaseUnit = 0m;
            do
            {
                var quantity = selector(enumerator.Current);
                valueInBaseUnit += quantity.Value * quantity.Unit.ValueInBaseUnit;
            }
            while (enumerator.MoveNext());

            if (targetUnit == null || targetUnit.ValueInBaseUnit == decimal.Zero)
                throw new DivideByZeroException($"Could not find non-zero based unit of type {typeof(TUnit).FullName} in source.");
            return factory(valueInBaseUnit / targetUnit.ValueInBaseUnit, targetUnit);
        }
    }
}