using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Length
    {
        public static bool operator ==(Length left, Length right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Length left, Length right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Length left, Length right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Length left, Length right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Length left, Length right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Length left, Length right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Length operator +(Length left, Length right) =>
            new Length(formatUnit: left._formatUnit ?? right.Unit, metres: left.Metres + right.Metres);
        public static Length operator -(Length left, Length right) =>
            new Length(formatUnit: left._formatUnit ?? right.Unit, metres: left.Metres - right.Metres);
        public static Length operator -(Length length) =>
            new Length(formatUnit: length.Unit, metres: -length.Metres);

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

        public static Length? operator +(Length? left, Length? right)
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
        public static Length? operator -(Length? left, Length? right)
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
    }
}
