using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct Volume : IEquatable<Volume>, IComparable<Volume>, IComparable
    {
        public bool Equals(Volume other) =>
            CubicMetres == other.CubicMetres;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(CubicMetres)
            .CurrentHash;

        public int CompareTo(Volume other) =>
            CubicMetres.CompareTo(other.CubicMetres);
        public int CompareTo(object obj) =>
            obj is Volume volume
            ? CompareTo(volume)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(Volume), nameof(obj));
    }
}
