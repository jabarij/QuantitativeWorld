using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct PowerUnit : IEquatable<PowerUnit>
    {
        public bool IsEquivalentOf(PowerUnit other) =>
            ValueInWatts.Equals(other.ValueInWatts);

        public bool Equals(PowerUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInWatts.Equals(other.ValueInWatts);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInWatts)
            .CurrentHash;

        public static bool operator ==(PowerUnit left, PowerUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(PowerUnit left, PowerUnit right) =>
            !(left == right);
    }
}
