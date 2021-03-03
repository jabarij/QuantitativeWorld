using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct Speed : IEquatable<Speed>, IComparable<Speed>, IComparable
    {
        public bool Equals(Speed other) =>
            MetresPerSecond.Equals(other.MetresPerSecond);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(MetresPerSecond)
            .CurrentHash;

        public int CompareTo(Speed other) =>
            MetresPerSecond.CompareTo(other.MetresPerSecond);
        public int CompareTo(object obj) =>
            obj is Speed speed
            ? CompareTo(speed)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Speed), nameof(obj));
    }
}
