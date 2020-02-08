using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    static class DummyStaticQuantityFormatter
    {
        public static string ToString<TQuantity, TUnit>(TQuantity quantity)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit =>
            string.Format("{0:G29} {1}", quantity.Value, quantity.Unit);

        public static string ToString<TQuantity, TUnit>(IFormatProvider formatProvider, TQuantity quantity)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit =>
            string.Format(formatProvider, "{0:G29} {1}", quantity.Value, quantity.Unit);
    }
}
