using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Weight
    {
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

        public static Weight operator +(Weight left, Weight right) =>
            new Weight(formatUnit: left.Unit, kilograms: left.Kilograms + right.Kilograms);
        public static Weight operator -(Weight left, Weight right) =>
            new Weight(formatUnit: left.Unit, kilograms: left.Kilograms - right.Kilograms);
        public static Weight operator -(Weight weight) =>
            new Weight(formatUnit: weight.Unit, kilograms: -weight.Kilograms);

        public static Weight operator *(Weight weight, decimal factor) =>
            new Weight(formatUnit: weight.Unit, kilograms: weight.Kilograms * factor);
        public static Weight operator *(decimal factor, Weight weight) =>
            weight * factor;

        public static Weight operator /(Weight weight, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Weight(formatUnit: weight.Unit, kilograms: weight.Kilograms / denominator);
        }
        public static decimal operator /(Weight weight, Weight denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return weight.Kilograms / denominator.Kilograms;
        }

        public static Weight? operator +(Weight? left, Weight? right)
        {
            if (left.HasValue && right.HasValue)
                return left.Value + right.Value;
            else if (!left.HasValue && !right.HasValue)
                return null;
            else if (left.HasValue)
                return left.Value;
            else
                return right.Value;
        }
        public static Weight? operator -(Weight? left, Weight? right)
        {
            if (left.HasValue && right.HasValue)
                return left.Value - right.Value;
            else if (!left.HasValue && !right.HasValue)
                return null;
            else if (left.HasValue)
                return left.Value;
            else
                return -right.Value;
        }

        public static Weight? operator *(Weight? weight, decimal factor) =>
            (weight ?? default(Weight)) * factor;
        public static Weight? operator *(decimal factor, Weight? weight) =>
            weight * factor;

        public static Weight? operator /(Weight? weight, decimal denominator) =>
            (weight ?? default(Weight)) / denominator;
        public static decimal operator /(Weight? weight, Weight? denominator) =>
            (weight ?? default(Weight)) / (denominator ?? default(Weight));
    }
}
