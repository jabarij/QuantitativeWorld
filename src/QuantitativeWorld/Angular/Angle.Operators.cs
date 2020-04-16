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
        public static Angle operator -(Angle angle) =>
            new Angle(formatUnit: angle.Unit, turns: -angle.Turns);

        public static Angle operator *(Angle angle, decimal factor) =>
            new Angle(formatUnit: angle.Unit, turns: angle.Turns * factor);
        public static Angle operator *(decimal factor, Angle angle) =>
            angle * factor;

        public static Angle operator /(Angle angle, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Angle(formatUnit: angle.Unit, turns: angle.Turns / denominator);
        }
        public static decimal operator /(Angle angle, Angle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return angle.Turns / denominator.Turns;
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

        public static Angle? operator *(Angle? angle, decimal factor) =>
            (angle ?? default(Angle)) * factor;
        public static Angle? operator *(decimal factor, Angle? angle) =>
            angle * factor;

        public static Angle? operator /(Angle? angle, decimal denominator) =>
            (angle ?? default(Angle)) / denominator;
        public static decimal operator /(Angle? angle, Angle? denominator) =>
            (angle ?? default(Angle)) / (denominator ?? default(Angle));
    }
}
