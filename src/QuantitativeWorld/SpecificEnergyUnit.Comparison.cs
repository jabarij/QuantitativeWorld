using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct SpecificEnergyUnit : IEquatable<SpecificEnergyUnit>
    {
        public bool IsEquivalentOf(SpecificEnergyUnit other) =>
            ValueInJoulesPerKilogram == other.ValueInJoulesPerKilogram;

        public bool Equals(SpecificEnergyUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInJoulesPerKilogram.Equals(other.ValueInJoulesPerKilogram);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInJoulesPerKilogram)
            .CurrentHash;

        public static bool AreEquivalent(SpecificEnergyUnit left, SpecificEnergyUnit right) =>
            left.IsEquivalentOf(right);

        public static bool operator ==(SpecificEnergyUnit left, SpecificEnergyUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(SpecificEnergyUnit left, SpecificEnergyUnit right) =>
            !(left == right);
    }
}
