using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct GeoCoordinate : IEquatable<GeoCoordinate>
    {
        public bool Equals(GeoCoordinate other) =>
            Equals(Latitude, other.Latitude)
            && Equals(Longitude, other.Longitude)
            && Equals(Altitude, other.Altitude);
        public override bool Equals(object obj) =>
            !ReferenceEquals(obj, null)
            && obj is GeoCoordinate other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Latitude, Longitude, Altitude)
            .CurrentHash;

        public static bool operator ==(GeoCoordinate left, GeoCoordinate right) =>
            left.Equals(right);
        public static bool operator !=(GeoCoordinate left, GeoCoordinate right) =>
            !(left == right);
    }
}
