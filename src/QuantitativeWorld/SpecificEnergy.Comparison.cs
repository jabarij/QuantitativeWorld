using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct SpecificEnergy : IEquatable<SpecificEnergy>, IComparable<SpecificEnergy>, IComparable
    {
        public bool Equals(SpecificEnergy other) =>
            JoulesPerKilogram == other.JoulesPerKilogram;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(JoulesPerKilogram)
            .CurrentHash;

        public int CompareTo(SpecificEnergy other) =>
            JoulesPerKilogram.CompareTo(other.JoulesPerKilogram);
        public int CompareTo(object obj) =>
            obj is SpecificEnergy specificEnergy
            ? CompareTo(specificEnergy)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(SpecificEnergy), nameof(obj));
    }
}
