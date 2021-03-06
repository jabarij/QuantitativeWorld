﻿using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    partial struct VolumeUnit : IEquatable<VolumeUnit>
    {
        public bool IsEquivalentOf(VolumeUnit other) =>
            ValueInCubicMetres.Equals(other.ValueInCubicMetres);

        public bool Equals(VolumeUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInCubicMetres.Equals(other.ValueInCubicMetres);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInCubicMetres)
            .CurrentHash;

        public static bool operator ==(VolumeUnit left, VolumeUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(VolumeUnit left, VolumeUnit right) =>
            !(left == right);
    }
}
