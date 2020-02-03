using System;

namespace QuantitativeWorld
{
    partial struct Weight
    {
        public static Weight operator +(Weight left, Weight right) =>
            left.WithValue(left.Kilograms + right.Kilograms);
        public static Weight operator -(Weight left, Weight right) =>
            left.WithValue(left.Kilograms - right.Kilograms);

        public static Weight operator *(Weight weight, decimal factor) =>
            weight.WithValue(weight.Kilograms * factor);
        public static Weight operator *(decimal factor, Weight weight) =>
            weight * factor;

        public static Weight operator /(Weight weight, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Weight(weight.Kilograms / denominator);
        }
        public static decimal operator /(Weight weight, Weight denominator)
        {
            if (denominator.Kilograms == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return weight.Kilograms / denominator.Kilograms;
        }

        private Weight WithValue(decimal newValue) =>
            new Weight(Unit, newValue);
    }
}
