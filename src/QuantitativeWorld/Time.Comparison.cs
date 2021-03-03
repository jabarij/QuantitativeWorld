using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct Time : IEquatable<Time>, IComparable<Time>, IComparable
    {
        public bool Equals(Time other) =>
            TotalSeconds.Equals(other.TotalSeconds);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(TotalSeconds)
            .CurrentHash;

        public int CompareTo(Time other) =>
            TotalSeconds.CompareTo(other.TotalSeconds);
        public int CompareTo(object obj) =>
            obj is Time time
            ? CompareTo(time)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Time), nameof(obj));
    }
}
