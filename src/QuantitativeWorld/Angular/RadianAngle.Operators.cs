using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
    partial struct RadianAngle
    {
        public static bool operator ==(RadianAngle left, RadianAngle right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(RadianAngle left, RadianAngle right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(RadianAngle left, RadianAngle right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(RadianAngle left, RadianAngle right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(RadianAngle left, RadianAngle right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(RadianAngle left, RadianAngle right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static RadianAngle operator +(RadianAngle left, RadianAngle right) =>
            new RadianAngle(left.Radians + right.Radians);
        public static RadianAngle operator -(RadianAngle left, RadianAngle right) =>
            new RadianAngle(left.Radians - right.Radians);
        public static RadianAngle operator -(RadianAngle argument) =>
            new RadianAngle(-argument.Radians);

        public static RadianAngle operator *(RadianAngle argument, double factor) =>
            new RadianAngle(argument.Radians * factor);
        public static RadianAngle operator *(double argument, RadianAngle factor) =>
            factor * argument;

        public static RadianAngle operator /(RadianAngle nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new RadianAngle(nominator.Radians / denominator);
        }
        public static double operator /(RadianAngle nominator, RadianAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.Radians / denominator.Radians;
        }

        public static RadianAngle operator %(RadianAngle nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new RadianAngle(nominator.Radians % denominator);
        }

        public static RadianAngle? operator +(RadianAngle? left, RadianAngle? right) =>
            !left.HasValue && !right.HasValue
            ? (RadianAngle?)null
            : (left ?? default(RadianAngle)) + (right ?? default(RadianAngle));
        public static RadianAngle? operator -(RadianAngle? left, RadianAngle? right) =>
            !left.HasValue && !right.HasValue
            ? (RadianAngle?)null
            : (left ?? default(RadianAngle)) - (right ?? default(RadianAngle));

        public static RadianAngle? operator *(RadianAngle? argument, double factor) =>
            (argument ?? default(RadianAngle)) * factor;
        public static RadianAngle? operator *(double argument, RadianAngle? factor) =>
            factor * argument;

        public static RadianAngle? operator /(RadianAngle? nominator, double denominator) =>
            (nominator ?? default(RadianAngle)) / denominator;
        public static double operator /(RadianAngle? nominator, RadianAngle? denominator) =>
            (nominator ?? default(RadianAngle)) / (denominator ?? default(RadianAngle));

        public static RadianAngle? operator %(RadianAngle? nominator, double denominator) =>
            (nominator ?? default(RadianAngle)) % denominator;
    }
}
