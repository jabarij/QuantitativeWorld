using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    static class DummyStaticFormatter
    {
        public static string ToString<TQuantity, TUnit>(TQuantity quantity)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit =>
            string.Format("{0:G29} {1}", quantity.Value, quantity.Unit);

        public static string ToString<TQuantity, TUnit>(IFormatProvider formatProvider, TQuantity quantity)
            where TQuantity : ILinearQuantity<TUnit>
            where TUnit : ILinearUnit =>
            string.Format(formatProvider, "{0:G29} {1}", quantity.Value, quantity.Unit);

        public static string ToString(GeoCoordinate location) =>
            string.Format("lat: {0:G29}, lon: {1:G29}", location.Latitude, location.Longitude);

        public static string ToString(IFormatProvider formatProvider, GeoCoordinate location) =>
            string.Format(formatProvider, "lat: {0:G29}, lon: {1:G29}", location.Latitude, location.Longitude);
    }
}
