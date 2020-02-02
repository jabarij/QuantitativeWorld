using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct LengthUnit : IEquatable<LengthUnit>
    {
        public bool IsEquivalentOf(LengthUnit other) =>
            ValueInMetres.Equals(other.ValueInMetres);

        public bool Equals(LengthUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInMetres.Equals(other.ValueInMetres);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInMetres)
            .CurrentHash;

        public static bool operator ==(LengthUnit left, LengthUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(LengthUnit left, LengthUnit right) =>
            !(left == right);
    }
}
