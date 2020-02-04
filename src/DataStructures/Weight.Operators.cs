using System;

namespace QuantitativeWorld
{
    partial struct Weight
    {
        public static Weight operator +(Weight left, Weight right) =>
            new Weight(formatUnit: left.Unit, kilograms: left.Kilograms + right.Kilograms);
        public static Weight operator -(Weight left, Weight right) =>
            new Weight(formatUnit: left.Unit, kilograms: left.Kilograms - right.Kilograms);

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
