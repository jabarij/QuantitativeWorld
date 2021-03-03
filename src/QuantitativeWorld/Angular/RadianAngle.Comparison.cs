using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
#else
namespace QuantitativeWorld.Angular
{
#endif
    partial struct RadianAngle : IEquatable<RadianAngle>, IComparable<RadianAngle>, IComparable
    {
        public bool Equals(RadianAngle other) =>
            Radians.Equals(other.Radians);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Radians)
            .CurrentHash;

        public int CompareTo(RadianAngle other) =>
            Radians.CompareTo(other.Radians);
        public int CompareTo(object obj) =>
            obj is RadianAngle radianAngle
            ? CompareTo(radianAngle)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(RadianAngle), nameof(obj));
    }
}
