using Plant.QAM.BusinessLogic.PublishedLanguage.Comparison;
using Plant.QAM.BusinessLogic.PublishedLanguage.Runtime;
using System;

namespace DataStructures
{
    partial struct Weight : IEquatable<Weight>, IComparable<Weight>
    {
        public bool Equals(Weight other) =>
            Unit.IsEquivalentOf(other.Unit)
            ? Value.Equals(other.Value)
            : Value.Equals(UnitConverter.GetValue(other, Unit));
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Value, Unit)
            .CurrentHash;

        public int CompareTo(Weight other) =>
            Unit.IsEquivalentOf(other.Unit)
            ? Value.CompareTo(other.Value)
            : Value.CompareTo(UnitConverter.GetValue(other, Unit));

        public static bool operator ==(Weight left, Weight right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Weight left, Weight right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Weight left, Weight right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Weight left, Weight right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Weight left, Weight right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Weight left, Weight right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);
    }
}
