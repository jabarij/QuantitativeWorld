using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
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
            new Power(formatUnit: left._formatUnit ?? right.Unit, watts: left.Watts + right.Watts);
        public static Power operator -(Power left, Power right) =>
            new Power(formatUnit: left._formatUnit ?? right.Unit, watts: left.Watts - right.Watts);
        public static Power operator -(Power power) =>
            new Power(formatUnit: power.Unit, watts: -power.Watts);

        public static Power operator *(Power power, double factor) =>
            new Power(formatUnit: power.Unit, watts: power.Watts * factor);
        public static Power operator *(double factor, Power power) =>
            power * factor;

        public static Power operator /(Power power, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Power(formatUnit: power.Unit, watts: power.Watts / denominator);
        }
        public static double operator /(Power power, Power denominator)
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

        public static Power? operator *(Power? power, double factor) =>
            (power ?? default(Power)) * factor;
        public static Power? operator *(double factor, Power? power) =>
            power * factor;

        public static Power? operator /(Power? power, double denominator) =>
            (power ?? default(Power)) / denominator;
        public static double operator /(Power? power, Power? denominator) =>
            (power ?? default(Power)) / (denominator ?? default(Power));
    }
}
