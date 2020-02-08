using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Length : IEquatable<Length>, IComparable<Length>, IComparable
    {
        public bool Equals(Length other) =>
            Metres == other.Metres;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Metres)
            .CurrentHash;

        public int CompareTo(Length other) =>
            Metres.CompareTo(other.Metres);
        public int CompareTo(object obj) =>
            obj is Length length
            ? CompareTo(length)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Length), nameof(obj));
    }
}
