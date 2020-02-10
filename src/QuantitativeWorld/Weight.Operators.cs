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
            new Weight(formatUnit: left._formatUnit ?? right.Unit, kilograms: left.Kilograms + right.Kilograms);
        public static Weight operator -(Weight left, Weight right) =>
            new Weight(formatUnit: left._formatUnit ?? right.Unit, kilograms: left.Kilograms - right.Kilograms);

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
    }
}
