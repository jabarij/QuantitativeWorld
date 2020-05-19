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
            new Energy(formatUnit: left._formatUnit ?? right.Unit, joules: left.Joules + right.Joules);
        public static Energy operator -(Energy left, Energy right) =>
            new Energy(formatUnit: left._formatUnit ?? right.Unit, joules: left.Joules - right.Joules);
        public static Energy operator -(Energy energy) =>
            new Energy(formatUnit: energy.Unit, joules: -energy.Joules);

        public static Energy operator *(Energy energy, double factor) =>
            new Energy(formatUnit: energy.Unit, joules: energy.Joules * factor);
        public static Energy operator *(double factor, Energy energy) =>
            energy * factor;

        public static Energy operator /(Energy energy, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Energy(formatUnit: energy.Unit, joules: energy.Joules / denominator);
        }
        public static double operator /(Energy energy, Energy denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return energy.Joules / denominator.Joules;
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
    }
}
