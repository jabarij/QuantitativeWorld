using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct SpeedUnit : IEquatable<SpeedUnit>
    {
        public bool IsEquivalentOf(SpeedUnit other) =>
            ValueInMetresPerSecond.Equals(other.ValueInMetresPerSecond);

        public bool Equals(SpeedUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInMetresPerSecond.Equals(other.ValueInMetresPerSecond);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInMetresPerSecond)
            .CurrentHash;

        public static bool operator ==(SpeedUnit left, SpeedUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(SpeedUnit left, SpeedUnit right) =>
            !(left == right);
    }
}
