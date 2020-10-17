using System;

namespace QuantitativeWorld.Text.Encoding
{
    public class Polyline6Encoding : PolylineEncoding
    {
        private const double PrecisionFactor = 1e6;

        internal override GeoCoordinate ToGeoCoordinate(PolylinePoint polylinePoint) =>
            new GeoCoordinate(
                latitude: polylinePoint.Latitude / PrecisionFactor,
                longitude: polylinePoint.Longitude / PrecisionFactor);

        internal override PolylinePoint ToPolylinePoint(GeoCoordinate coordinate) =>
            new PolylinePoint(
                latitude: (int)Math.Round(coordinate.Latitude * PrecisionFactor),
                longitude: (int)Math.Round(coordinate.Longitude * PrecisionFactor));
    }
}