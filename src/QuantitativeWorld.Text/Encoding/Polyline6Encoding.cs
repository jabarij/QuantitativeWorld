using System;

namespace QuantitativeWorld.Text.Encoding
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class Polyline6Encoding : PolylineEncoding
    {
        private const int PrecisionFactor = 1000000;

        internal override GeoCoordinate ToGeoCoordinate(PolylinePoint polylinePoint) =>
            new GeoCoordinate(
                latitude: polylinePoint.Latitude / PrecisionFactor,
                longitude: polylinePoint.Longitude / PrecisionFactor);

        internal override PolylinePoint ToPolylinePoint(GeoCoordinate coordinate) =>
            new PolylinePoint(
                latitude: (int)Math.Round(coordinate.Latitude.TotalDegrees * PrecisionFactor),
                longitude: (int)Math.Round(coordinate.Longitude.TotalDegrees * PrecisionFactor));
    }
}