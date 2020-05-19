using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct EnergyUnit : IEquatable<EnergyUnit>
    {
        public bool IsEquivalentOf(EnergyUnit other) =>
            ValueInJoules.Equals(other.ValueInJoules);

        public bool Equals(EnergyUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInJoules.Equals(other.ValueInJoules);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInJoules)
            .CurrentHash;

        public static bool operator ==(EnergyUnit left, EnergyUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(EnergyUnit left, EnergyUnit right) =>
            !(left == right);
    }
}
