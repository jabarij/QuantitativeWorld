using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Energy
    {
        public static bool operator ==(Energy left, Energy right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Energy left, Energy right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Energy left, Energy right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Energy left, Energy right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Energy left, Energy right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Energy left, Energy right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Energy operator +(Energy left, Energy right) =>
            new Energy(joules: left.Joules + right.Joules, value: null, unit: left._unit ?? right.Unit);
        public static Energy operator -(Energy left, Energy right) =>
            new Energy(joules: left.Joules - right.Joules, value: null, unit: left._unit ?? right.Unit);
        public static Energy operator -(Energy energy) =>
            new Energy(joules: -energy.Joules, value: null, unit: energy._unit);

        public static Energy operator *(Energy energy, double factor) =>
            new Energy(joules: energy.Joules * factor, value: null, unit: energy._unit);
        public static Energy operator *(double factor, Energy energy) =>
            energy * factor;

        public static Energy operator /(Energy energy, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Energy(joules: energy.Joules / denominator, value: null, unit: energy._unit);
        }
        public static double operator /(Energy energy, Energy denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return energy.Joules / denominator.Joules;
        }
        public static Power operator /(Energy energy, Time time)
        {
            if (time.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Power(energy.Joules / time.TotalSeconds);
        }
        public static Time operator /(Energy energy, Power power)
        {
            if (power.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Time(
                totalSeconds: energy.Joules / power.Watts);
        }

        public static Energy? operator +(Energy? left, Energy? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Energy)) + (right ?? default(Energy))
            : (Energy?)null;
        public static Energy? operator -(Energy? left, Energy? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Energy)) - (right ?? default(Energy))
            : (Energy?)null;

        public static Energy? operator *(Energy? energy, double factor) =>
            (energy ?? default(Energy)) * factor;
        public static Energy? operator *(double factor, Energy? energy) =>
            energy * factor;

        public static Energy? operator /(Energy? energy, double denominator) =>
            (energy ?? default(Energy)) / denominator;
        public static double operator /(Energy? energy, Energy? denominator) =>
            (energy ?? default(Energy)) / (denominator ?? default(Energy));
        public static Power? operator /(Energy? energy, Time? time) =>
            (energy ?? default(Energy)) / (time ?? default(Time));
        public static Time? operator /(Energy? energy, Power? power) =>
            (energy ?? default(Energy)) / (power ?? default(Power));
    }
}
