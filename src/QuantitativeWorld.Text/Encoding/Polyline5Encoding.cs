using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantitativeWorld.Text.Encoding
{
    internal class Polyline5Encoding : PolylineEncoding
    {
        private const double PrecisionFactor = 1e5;

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