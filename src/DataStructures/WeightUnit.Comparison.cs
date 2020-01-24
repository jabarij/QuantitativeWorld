using Plant.QAM.BusinessLogic.PublishedLanguage.Comparison;
using Plant.QAM.BusinessLogic.PublishedLanguage.Runtime;
using System;

namespace DataStructures
{
    partial struct WeightUnit : IEquatable<WeightUnit>
    {
        public bool IsEquivalentOf(WeightUnit other) =>
            ValueInKilograms.Equals(other.ValueInKilograms);

        public bool Equals(WeightUnit other) =>
            string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Abbreviation, other.Abbreviation, StringComparison.Ordinal)
            && ValueInKilograms.Equals(other.ValueInKilograms);
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(Name, Abbreviation, ValueInKilograms)
            .CurrentHash;

        public static bool operator ==(WeightUnit left, WeightUnit right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(WeightUnit left, WeightUnit right) =>
            !(left == right);
    }
}
