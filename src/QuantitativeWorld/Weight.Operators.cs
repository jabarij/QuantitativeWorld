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
            new Weight(kilograms: left.Kilograms + right.Kilograms, value: null, unit: left._unit ?? right.Unit);
        public static Weight operator -(Weight left, Weight right) =>
            new Weight(kilograms: left.Kilograms - right.Kilograms, value: null, unit: left._unit ?? right.Unit);
        public static Weight operator -(Weight weight) =>
            new Weight(kilograms: -weight.Kilograms, value: null, unit: weight.Unit);

        public static Weight operator *(Weight weight, number factor) =>
            new Weight(kilograms: weight.Kilograms * factor, value: null, unit: weight._unit);
        public static Weight operator *(number factor, Weight weight) =>
            weight * factor;

        public static Weight operator /(Weight weight, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Weight(kilograms: weight.Kilograms / denominator, value: null, unit: weight._unit);
        }
        public static number operator /(Weight weight, Weight denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return weight.Kilograms / denominator.Kilograms;
        }

        public static Weight? operator +(Weight? left, Weight? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Weight)) + (right ?? default(Weight))
            : (Weight?)null;
        public static Weight? operator -(Weight? left, Weight? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Weight)) - (right ?? default(Weight))
            : (Weight?)null;

        public static Weight? operator *(Weight? weight, number factor) =>
            (weight ?? default(Weight)) * factor;
        public static Weight? operator *(number factor, Weight? weight) =>
            weight * factor;

        public static Weight? operator /(Weight? weight, number denominator) =>
            (weight ?? default(Weight)) / denominator;
        public static number operator /(Weight? weight, Weight? denominator) =>
            (weight ?? default(Weight)) / (denominator ?? default(Weight));
    }
}