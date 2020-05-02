using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
    partial struct Angle
    {
        public static bool operator ==(Angle left, Angle right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Angle left, Angle right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Angle left, Angle right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Angle left, Angle right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Angle left, Angle right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Angle left, Angle right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Angle operator +(Angle left, Angle right) =>
            new Angle(formatUnit: left._formatUnit ?? right.Unit, turns: left.Turns + right.Turns);
        public static Angle operator -(Angle left, Angle right) =>
            new Angle(formatUnit: left._formatUnit ?? right.Unit, turns: left.Turns - right.Turns);
        public static Angle operator -(Angle argument) =>
            new Angle(formatUnit: argument.Unit, turns: -argument.Turns);

        public static Angle operator *(Angle argument, double factor) =>
            new Angle(formatUnit: argument.Unit, turns: argument.Turns * factor);
        public static Angle operator *(double argument, Angle factor) =>
            factor * argument;

        public static Angle operator /(Angle nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Angle(formatUnit: nominator.Unit, turns: nominator.Turns / denominator);
        }
        public static double operator /(Angle nominator, Angle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.Turns / denominator.Turns;
        }

        public static Angle operator %(Angle nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Angle(nominator._formatUnit, nominator.Turns % denominator);
        }

        public static Angle? operator +(Angle? left, Angle? right)
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
        public static Angle? operator -(Angle? left, Angle? right)
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

        public static Angle? operator *(Angle? argument, double factor) =>
            (argument ?? default(Angle)) * factor;
        public static Angle? operator *(double argument, Angle? factor) =>
            factor * argument;

        public static Angle? operator /(Angle? nominator, double denominator) =>
            (nominator ?? default(Angle)) / denominator;
        public static double operator /(Angle? nominator, Angle? denominator) =>
            (nominator ?? default(Angle)) / (denominator ?? default(Angle));

        public static Angle? operator %(Angle? nominator, double denominator) =>
            (nominator ?? default(Angle)) % denominator;
    }
}
