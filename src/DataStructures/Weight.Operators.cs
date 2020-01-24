using System;

namespace DataStructures
{
    partial struct Weight
    {
        public static Weight operator +(Weight left, Weight right) =>
            left.WithValue(left.Value + UnitConverter.GetValue(right, left.Unit));
        public static Weight operator -(Weight left, Weight right) =>
            left.WithValue(left.Value - UnitConverter.GetValue(right, left.Unit));

        public static Weight operator *(Weight weight, decimal factor) =>
            weight.WithValue(weight.Value * factor);
        public static Weight operator *(decimal factor, Weight weight) =>
            weight * factor;

        public static Weight operator /(Weight weight, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Weight(weight.Value / denominator, weight.Unit);
        }
        public static decimal operator /(Weight weight, Weight denominator)
        {
            if (IsZero(denominator))
                throw new DivideByZeroException("Denominator is zero.");
            return weight.Value / denominator.Value;
        }

        private Weight WithValue(decimal newValue) =>
            new Weight(newValue, Unit);
    }
}
