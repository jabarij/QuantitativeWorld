using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
    partial struct DegreeAngle : IEquatable<DegreeAngle>, IComparable<DegreeAngle>, IComparable
    {
        public bool Equals(DegreeAngle other) =>
            TotalSeconds.Equals(other.TotalSeconds);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(TotalDegrees)
            .CurrentHash;

        public int CompareTo(DegreeAngle other) =>
            TotalSeconds.CompareTo(other.TotalSeconds);
        public int CompareTo(object obj) =>
            obj is DegreeAngle degreeAngle
            ? CompareTo(degreeAngle)
            : throw Error.ArgumentIsOfUnexpectedType(obj, typeof(DegreeAngle), nameof(obj));
    }
}
