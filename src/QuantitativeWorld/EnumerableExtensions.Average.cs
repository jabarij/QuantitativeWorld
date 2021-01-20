using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;
using System.Collections.Generic;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class EnumerableExtensions
    {
        public static TQuantity Average<TQuantity, TUnit>(this IEnumerable<TQuantity> source, Func<number, TUnit, TQuantity> factory)
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

        public static TQuantity Average<TSource, TQuantity, TUnit>(this IEnumerable<TSource> source, Func<number, TUnit, TQuantity> factory, Func<TSource, TQuantity> selector)
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

        private static TQuantity Average<TSource, TQuantity, TUnit>(IEnumerator<TSource> enumerator, Func<number, TUnit, TQuantity> factory, Func<TSource, TQuantity> selector)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit
        {
            bool hasElements = enumerator.MoveNext();
            if (!hasElements)
                throw new InvalidOperationException("Sequence contains no elements.");

            var targetUnit = selector(enumerator.Current).Unit;
            int count = 0;
            number sumInBaseUnit = Constants.Zero;
            do
            {
                var quantity = selector(enumerator.Current);
                sumInBaseUnit += quantity.Value * quantity.Unit.ValueInBaseUnit;
                count++;
            }
            while (enumerator.MoveNext());

            if (targetUnit == null || targetUnit.ValueInBaseUnit == Constants.Zero)
                throw new DivideByZeroException($"Could not find non-zero based unit of type {typeof(TUnit).FullName} in source.");
            return factory(sumInBaseUnit / targetUnit.ValueInBaseUnit / count, targetUnit);
        }
    }
}
