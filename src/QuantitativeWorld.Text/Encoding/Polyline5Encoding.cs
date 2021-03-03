using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Encoding
{
    using number = Decimal;
#else
namespace QuantitativeWorld.Text.Encoding
{
    using number = Double;
#endif

    internal class Polyline5Encoding : PolylineEncoding
    {
        private const int PrecisionFactor = 100000;

        internal override GeoCoordinate ToGeoCoordinate(PolylinePoint polylinePoint) =>
            new GeoCoordinate(
                latitude: Math.Round((number)polylinePoint.Latitude / PrecisionFactor, 5),
                longitude: Math.Round((number)polylinePoint.Longitude / PrecisionFactor, 5));

        internal override PolylinePoint ToPolylinePoint(GeoCoordinate coordinate) =>
            new PolylinePoint(
                latitude: (int)Math.Round(coordinate.Latitude.TotalDegrees * PrecisionFactor),
                longitude: (int)Math.Round(coordinate.Longitude.TotalDegrees * PrecisionFactor));
    }
}