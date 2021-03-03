using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
#else
namespace QuantitativeWorld.Angular
{
#endif
    partial struct Angle : IEquatable<Angle>, IComparable<Angle>, IComparable
    {
        public bool Equals(Angle other) =>
            Turns == other.Turns;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Turns)
            .CurrentHash;

        public int CompareTo(Angle other) =>
            Turns.CompareTo(other.Turns);
        public int CompareTo(object obj) =>
            obj is Angle angle
            ? CompareTo(angle)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Angle), nameof(obj));
    }
}
