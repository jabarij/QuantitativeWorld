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
            new Weight(kilograms: left.Kilograms + right.Kilograms, value: null, unit: left._unit ?? right.Unit);
        public static Weight operator -(Weight left, Weight right) =>
            new Weight(kilograms: left.Kilograms - right.Kilograms, value: null, unit: left._unit ?? right.Unit);
        public static Weight operator -(Weight weight) =>
            new Weight(kilograms: -weight.Kilograms, value: null, unit: weight.Unit);

        public static Weight operator *(Weight weight, double factor) =>
            new Weight(kilograms: weight.Kilograms * factor, value: null, unit: weight._unit);
        public static Weight operator *(double factor, Weight weight) =>
            weight * factor;

        public static Weight operator /(Weight weight, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Weight(kilograms: weight.Kilograms / denominator, value: null, unit: weight._unit);
        }
        public static double operator /(Weight weight, Weight denominator)
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

        public static Weight? operator *(Weight? weight, double factor) =>
            (weight ?? default(Weight)) * factor;
        public static Weight? operator *(double factor, Weight? weight) =>
            weight * factor;

        public static Weight? operator /(Weight? weight, double denominator) =>
            (weight ?? default(Weight)) / denominator;
        public static double operator /(Weight? weight, Weight? denominator) =>
            (weight ?? default(Weight)) / (denominator ?? default(Weight));
    }
}