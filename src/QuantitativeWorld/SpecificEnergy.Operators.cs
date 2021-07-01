using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld
{
    using Constants = DoubleConstants;
    using number = Double;
#endif

    partial struct SpecificEnergy
    {
        public static bool operator ==(SpecificEnergy left, SpecificEnergy right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(SpecificEnergy left, SpecificEnergy right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(SpecificEnergy left, SpecificEnergy right) =>
            left.JoulesPerKilogram > right.JoulesPerKilogram;
        public static bool operator >=(SpecificEnergy left, SpecificEnergy right) =>
            left.JoulesPerKilogram >= right.JoulesPerKilogram;
        public static bool operator <(SpecificEnergy left, SpecificEnergy right) =>
            left.JoulesPerKilogram < right.JoulesPerKilogram;
        public static bool operator <=(SpecificEnergy left, SpecificEnergy right) =>
            left.JoulesPerKilogram <= right.JoulesPerKilogram;

        public static SpecificEnergy operator +(SpecificEnergy left, SpecificEnergy right) =>
            left.Unit.IsEquivalentOf(right.Unit)
            ? new SpecificEnergy(
                value: left.Value + right.Value,
                unit: left.Unit)
            : new SpecificEnergy(
                joulesPerKilogram: left.JoulesPerKilogram + right.JoulesPerKilogram,
                value: null,
                unit: left._unit ?? right.Unit);
        public static SpecificEnergy operator -(SpecificEnergy left, SpecificEnergy right) =>
            new SpecificEnergy(joulesPerKilogram: left.JoulesPerKilogram - right.JoulesPerKilogram, value: null, unit: left._unit ?? right.Unit);
        public static SpecificEnergy operator -(SpecificEnergy specificEnergy) =>
            new SpecificEnergy(joulesPerKilogram: -specificEnergy.JoulesPerKilogram, value: null, unit: specificEnergy._unit);

        public static SpecificEnergy operator *(SpecificEnergy specificEnergy, number factor) =>
            new SpecificEnergy(joulesPerKilogram: specificEnergy.JoulesPerKilogram * factor, value: null, unit: specificEnergy._unit);
        public static SpecificEnergy operator *(number factor, SpecificEnergy specificEnergy) =>
            specificEnergy * factor;

        public static SpecificEnergy operator /(SpecificEnergy specificEnergy, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new SpecificEnergy(joulesPerKilogram: specificEnergy.JoulesPerKilogram / denominator, value: null, unit: specificEnergy._unit);
        }
        public static number operator /(SpecificEnergy specificEnergy, SpecificEnergy denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return specificEnergy.JoulesPerKilogram / denominator.JoulesPerKilogram;
        }

        public static SpecificEnergy? operator +(SpecificEnergy? left, SpecificEnergy? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(SpecificEnergy)) + (right ?? default(SpecificEnergy))
            : (SpecificEnergy?)null;
        public static SpecificEnergy? operator -(SpecificEnergy? left, SpecificEnergy? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(SpecificEnergy)) - (right ?? default(SpecificEnergy))
            : (SpecificEnergy?)null;

        public static SpecificEnergy? operator *(SpecificEnergy? specificEnergy, number factor) =>
            (specificEnergy ?? default(SpecificEnergy)) * factor;
        public static SpecificEnergy? operator *(number factor, SpecificEnergy? specificEnergy) =>
            specificEnergy * factor;

        public static SpecificEnergy? operator /(SpecificEnergy? specificEnergy, number denominator) =>
            (specificEnergy ?? default(SpecificEnergy)) / denominator;
        public static number operator /(SpecificEnergy? specificEnergy, SpecificEnergy? denominator) =>
            (specificEnergy ?? default(SpecificEnergy)) / (denominator ?? default(SpecificEnergy));

        public static SpecificEnergy Abs(SpecificEnergy specificEnergy) =>
            new SpecificEnergy(
                joulesPerKilogram: Math.Abs(specificEnergy.JoulesPerKilogram),
                value:
                    specificEnergy._value.HasValue
                    ? Math.Abs(specificEnergy._value.Value)
                    : (number?)null,
                unit: specificEnergy._unit);
        public static SpecificEnergy? Abs(SpecificEnergy? specificEnergy) =>
            specificEnergy.HasValue
            ? Abs(specificEnergy.Value)
            : (SpecificEnergy?)null;
    }
}
