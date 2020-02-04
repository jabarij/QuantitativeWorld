using System;

namespace QuantitativeWorld
{
    partial struct Length
    {
        public static Length operator +(Length left, Length right) =>
            new Length(formatUnit: left.Unit, metres: left.Metres + right.Metres);
        public static Length operator -(Length left, Length right) =>
            new Length(formatUnit: left.Unit, metres: left.Metres - right.Metres);

        public static Length operator *(Length length, decimal factor) =>
            new Length(formatUnit: length.Unit, metres: length.Metres * factor);
        public static Length operator *(decimal factor, Length length) =>
            length * factor;

        public static Length operator /(Length length, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Length(formatUnit: length.Unit, metres: length.Metres / denominator);
        }
        public static decimal operator /(Length length, Length denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return length.Metres / denominator.Metres;
        }
    }
}
