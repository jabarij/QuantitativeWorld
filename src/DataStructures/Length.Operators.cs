using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    partial struct Length
    {

        public static Length operator +(Length left, Length right) =>
            left.WithValue(left.Value + UnitConverter.GetValue(right, left.Unit));
        public static Length operator -(Length left, Length right) =>
            left.WithValue(left.Value - UnitConverter.GetValue(right, left.Unit));

        public static Length operator *(Length weight, decimal factor) =>
            weight.WithValue(weight.Value * factor);
        public static Length operator *(decimal factor, Length weight) =>
            weight * factor;

        public static Length operator /(Length weight, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Length(weight.Value / denominator, weight.Unit);
        }
        public static decimal operator /(Length weight, Length denominator)
        {
            if (IsZero(denominator))
                throw new DivideByZeroException("Denominator is zero.");
            return weight.Value / denominator.Value;
        }

        private Length WithValue(decimal newValue) =>
            new Length(newValue, Unit);
    }
}
