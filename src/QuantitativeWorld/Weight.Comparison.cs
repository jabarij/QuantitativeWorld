using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Weight : IEquatable<Weight>, IComparable<Weight>, IComparable
    {
        public bool Equals(Weight other) =>
            Kilograms.Equals(other.Kilograms);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Kilograms)
            .CurrentHash;

        public int CompareTo(Weight other) =>
            Kilograms.CompareTo(other.Kilograms);
        public int CompareTo(object obj) =>
            obj is Weight weight
            ? CompareTo(weight)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Weight), nameof(obj));
    }
}
