using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
    partial struct AngleUnit : IEquatable<AngleUnit>
    {
        public bool IsEquivalentOf(AngleUnit other) =>
            UnitsPerTurn.Equals(other.UnitsPerTurn);

        public bool Equals(AngleUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && UnitsPerTurn.Equals(other.UnitsPerTurn);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, UnitsPerTurn)
            .CurrentHash;

        public static bool operator ==(AngleUnit left, AngleUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(AngleUnit left, AngleUnit right) =>
            !(left == right);
    }
}
