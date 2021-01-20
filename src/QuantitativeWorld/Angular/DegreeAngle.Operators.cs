using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct DegreeAngle
    {
        public static bool operator ==(DegreeAngle left, DegreeAngle right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(DegreeAngle left, DegreeAngle right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(DegreeAngle left, DegreeAngle right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(DegreeAngle left, DegreeAngle right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(DegreeAngle left, DegreeAngle right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(DegreeAngle left, DegreeAngle right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static DegreeAngle operator +(DegreeAngle left, DegreeAngle right) =>
            new DegreeAngle(left.TotalSeconds + right.TotalSeconds);
        public static DegreeAngle operator -(DegreeAngle left, DegreeAngle right) =>
            new DegreeAngle(left.TotalSeconds - right.TotalSeconds);
        public static DegreeAngle operator -(DegreeAngle argument) =>
            new DegreeAngle(-argument.TotalSeconds);

        public static DegreeAngle operator *(DegreeAngle argument, number factor) =>
            new DegreeAngle(argument.TotalSeconds * factor);
        public static DegreeAngle operator *(number argument, DegreeAngle factor) =>
            factor * argument;

        public static DegreeAngle operator /(DegreeAngle nominator, number denominator)
        {
#if !DECIMAL
            if (number.IsNaN(denominator))
                throw new ArgumentException("Denominator is not a number.", nameof(denominator));
            if (number.IsInfinity(denominator))
                throw new ArgumentException("Denominator is not finite number.", nameof(denominator));
#endif
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");

            return new DegreeAngle(nominator.TotalSeconds / denominator);
        }
        public static number operator /(DegreeAngle nominator, DegreeAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");

            return nominator.TotalSeconds / denominator.TotalSeconds;
        }

        public static DegreeAngle operator %(DegreeAngle nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new DegreeAngle(nominator.TotalSeconds % denominator);
        }

        public static DegreeAngle? operator +(DegreeAngle? left, DegreeAngle? right)
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
        public static DegreeAngle? operator -(DegreeAngle? left, DegreeAngle? right)
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

        public static DegreeAngle? operator *(DegreeAngle? argument, number factor) =>
            (argument ?? default(DegreeAngle)) * factor;
        public static DegreeAngle? operator *(number argument, DegreeAngle? factor) =>
            factor * argument;

        public static DegreeAngle? operator /(DegreeAngle? nominator, number denominator) =>
            (nominator ?? default(DegreeAngle)) / denominator;
        public static number operator /(DegreeAngle? nominator, DegreeAngle? denominator) =>
            (nominator ?? default(DegreeAngle)) / (denominator ?? default(DegreeAngle));

        public static DegreeAngle? operator %(DegreeAngle? nominator, number denominator) =>
            (nominator ?? default(DegreeAngle)) % denominator;
    }
}
