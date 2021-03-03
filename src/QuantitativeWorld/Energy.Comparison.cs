using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct Energy : IEquatable<Energy>, IComparable<Energy>, IComparable
    {
        public bool Equals(Energy other) =>
            Joules.Equals(other.Joules);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Joules)
            .CurrentHash;

        public int CompareTo(Energy other) =>
            Joules.CompareTo(other.Joules);
        public int CompareTo(object obj) =>
            obj is Energy energy
            ? CompareTo(energy)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Energy), nameof(obj));
    }
}
