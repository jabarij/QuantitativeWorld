using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Power : IEquatable<Power>, IComparable<Power>, IComparable
    {
        public bool Equals(Power other) =>
            Watts.Equals(other.Watts);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Watts)
            .CurrentHash;

        public int CompareTo(Power other) =>
            Watts.CompareTo(other.Watts);
        public int CompareTo(object obj) =>
            obj is Power power
            ? CompareTo(power)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Power), nameof(obj));
    }
}
