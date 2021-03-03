using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct GeoCoordinate : IEquatable<GeoCoordinate>
    {
        public bool Equals(GeoCoordinate other) =>
            _latitude == other._latitude
            && _longitude == other._longitude;
        public override bool Equals(object obj) =>
            obj is GeoCoordinate other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(_latitude, _longitude)
            .CurrentHash;

        public static bool operator ==(GeoCoordinate left, GeoCoordinate right) =>
            left.Equals(right);
        public static bool operator !=(GeoCoordinate left, GeoCoordinate right) =>
            !left.Equals(right);
    }
}
