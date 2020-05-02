using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld.Angular
{
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

        public static DegreeAngle operator *(DegreeAngle argument, double factor) =>
            new DegreeAngle(argument.TotalSeconds * factor);
        public static DegreeAngle operator *(double argument, DegreeAngle factor) =>
            factor * argument;

        public static DegreeAngle operator /(DegreeAngle nominator, double denominator)
        {
            if (double.IsNaN(denominator))
                throw new ArgumentException("Denominator is not a number.", nameof(denominator));
            if (double.IsInfinity(denominator))
                throw new ArgumentException("Denominator is not finite number.", nameof(denominator));
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");

            return new DegreeAngle(nominator.TotalSeconds / denominator);
        }
        public static double operator /(DegreeAngle nominator, DegreeAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");

            return nominator.TotalSeconds / denominator.TotalSeconds;
        }

        public static DegreeAngle operator %(DegreeAngle nominator, double denominator)
        {
            if (denominator == 0d)
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

        public static DegreeAngle? operator *(DegreeAngle? argument, double factor) =>
            (argument ?? default(DegreeAngle)) * factor;
        public static DegreeAngle? operator *(double argument, DegreeAngle? factor) =>
            factor * argument;

        public static DegreeAngle? operator /(DegreeAngle? nominator, double denominator) =>
            (nominator ?? default(DegreeAngle)) / denominator;
        public static double operator /(DegreeAngle? nominator, DegreeAngle? denominator) =>
            (nominator ?? default(DegreeAngle)) / (denominator ?? default(DegreeAngle));

        public static DegreeAngle? operator %(DegreeAngle? nominator, double denominator) =>
            (nominator ?? default(DegreeAngle)) % denominator;
    }
}
