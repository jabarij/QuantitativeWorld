using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Area : IEquatable<Area>, IComparable<Area>, IComparable
    {
        public bool Equals(Area other) =>
            SquareMetres == other.SquareMetres;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(SquareMetres)
            .CurrentHash;

        public int CompareTo(Area other) =>
            SquareMetres.CompareTo(other.SquareMetres);
        public int CompareTo(object obj) =>
            obj is Area area
            ? CompareTo(area)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Area), nameof(obj));
    }
}
