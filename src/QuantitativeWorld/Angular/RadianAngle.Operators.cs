using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Angular
{
    using Constants = DoubleConstants;
    using number = Double;
#endif

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

        public static RadianAngle operator *(RadianAngle argument, number factor) =>
            new RadianAngle(argument.Radians * factor);
        public static RadianAngle operator *(number argument, RadianAngle factor) =>
            factor * argument;

        public static RadianAngle operator /(RadianAngle nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new RadianAngle(nominator.Radians / denominator);
        }
        public static number operator /(RadianAngle nominator, RadianAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.Radians / denominator.Radians;
        }

        public static RadianAngle operator %(RadianAngle nominator, number denominator)
        {
            if (denominator == Constants.Zero)
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

        public static RadianAngle? operator *(RadianAngle? argument, number factor) =>
            (argument ?? default(RadianAngle)) * factor;
        public static RadianAngle? operator *(number argument, RadianAngle? factor) =>
            factor * argument;

        public static RadianAngle? operator /(RadianAngle? nominator, number denominator) =>
            (nominator ?? default(RadianAngle)) / denominator;
        public static number operator /(RadianAngle? nominator, RadianAngle? denominator) =>
            (nominator ?? default(RadianAngle)) / (denominator ?? default(RadianAngle));

        public static RadianAngle? operator %(RadianAngle? nominator, number denominator) =>
            (nominator ?? default(RadianAngle)) % denominator;
    }
}
