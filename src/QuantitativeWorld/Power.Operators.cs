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

    partial struct Power
    {
        public static bool operator ==(Power left, Power right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Power left, Power right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Power left, Power right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Power left, Power right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Power left, Power right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Power left, Power right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Power operator +(Power left, Power right) =>
            new Power(watts: left.Watts + right.Watts, value: null, unit: left._unit ?? right.Unit);
        public static Power operator -(Power left, Power right) =>
            new Power(watts: left.Watts - right.Watts, value: null, unit: left._unit ?? right.Unit);
        public static Power operator -(Power power) =>
            new Power(watts: -power.Watts, value: null, unit: power._unit);

        public static Power operator *(Power power, number factor) =>
            new Power(watts: power.Watts * factor, value: null, unit: power._unit);
        public static Power operator *(number factor, Power power) =>
            power * factor;

        public static Power operator /(Power power, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Power(watts: power.Watts / denominator, value: null, unit: power._unit);
        }
        public static number operator /(Power power, Power denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return power.Watts / denominator.Watts;
        }

        public static Power? operator +(Power? left, Power? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Power)) + (right ?? default(Power))
            : (Power?)null;
        public static Power? operator -(Power? left, Power? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Power)) - (right ?? default(Power))
            : (Power?)null;

        public static Power? operator *(Power? power, number factor) =>
            (power ?? default(Power)) * factor;
        public static Power? operator *(number factor, Power? power) =>
            power * factor;

        public static Power? operator /(Power? power, number denominator) =>
            (power ?? default(Power)) / denominator;
        public static number operator /(Power? power, Power? denominator) =>
            (power ?? default(Power)) / (denominator ?? default(Power));

        public static Energy operator *(Power power, Time time) =>
            new Energy(power.Watts * time.TotalSeconds);
        public static Energy operator *(Time argument, Power factor) =>
            new Energy(argument.TotalSeconds * factor.Watts);

        public static Energy? operator *(Power? power, Time? time) =>
            power.HasValue || time.HasValue
            ? (power ?? default(Power)) * (time ?? default(Time))
            : (Energy?)null;
        public static Energy? operator *(Time? time, Power? power) =>
            time.HasValue || power.HasValue
            ? (time ?? default(Time)) * (power ?? default(Power))
            : (Energy?)null;
    }
}
