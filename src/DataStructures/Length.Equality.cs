using Plant.QAM.BusinessLogic.PublishedLanguage.Comparison;
using Plant.QAM.BusinessLogic.PublishedLanguage.Runtime;
using System;

namespace DataStructures
{
    partial struct Length : IEquatable<Length>
    {
        public bool Equals(Length other) =>
            Unit.IsEquivalentOf(other.Unit)
            ? Value.Equals(other.Value)
            : Value.Equals(UnitConverter.GetValue(other, Unit));
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Value, Unit)
            .CurrentHash;

        public static bool operator ==(Length left, Length right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Length left, Length right) =>
            !(left == right);
    }
}
