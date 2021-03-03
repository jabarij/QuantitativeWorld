using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct AreaUnit : IEquatable<AreaUnit>
    {
        public bool IsEquivalentOf(AreaUnit other) =>
            ValueInSquareMetres.Equals(other.ValueInSquareMetres);

        public bool Equals(AreaUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInSquareMetres.Equals(other.ValueInSquareMetres);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInSquareMetres)
            .CurrentHash;

        public static bool operator ==(AreaUnit left, AreaUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(AreaUnit left, AreaUnit right) =>
            !(left == right);
    }
}
