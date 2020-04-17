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
        public static RadianAngle operator -(RadianAngle radianAngle) =>
            new RadianAngle(-radianAngle.Radians);

        public static RadianAngle operator *(RadianAngle radianAngle, decimal factor) =>
            new RadianAngle(radianAngle.Radians * factor);
        public static RadianAngle operator *(decimal factor, RadianAngle radianAngle) =>
            radianAngle * factor;

        public static RadianAngle operator /(RadianAngle radianAngle, decimal denominator)
        {
            if (denominator == decimal.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new RadianAngle(radianAngle.Radians / denominator);
        }
        public static decimal operator /(RadianAngle radianAngle, RadianAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return radianAngle.Radians / denominator.Radians;
        }

        public static RadianAngle? operator +(RadianAngle? left, RadianAngle? right)
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
        public static RadianAngle? operator -(RadianAngle? left, RadianAngle? right)
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

        public static RadianAngle? operator *(RadianAngle? radianAngle, decimal factor) =>
            (radianAngle ?? default(RadianAngle)) * factor;
        public static RadianAngle? operator *(decimal factor, RadianAngle? radianAngle) =>
            radianAngle * factor;

        public static RadianAngle? operator /(RadianAngle? radianAngle, decimal denominator) =>
            (radianAngle ?? default(RadianAngle)) / denominator;
        public static decimal operator /(RadianAngle? radianAngle, RadianAngle? denominator) =>
            (radianAngle ?? default(RadianAngle)) / (denominator ?? default(RadianAngle));
    }
}
