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
        public static DegreeAngle operator -(DegreeAngle degreeAngle) =>
            new DegreeAngle(-degreeAngle.TotalSeconds);

        public static DegreeAngle operator *(DegreeAngle degreeAngle, double factor) =>
            new DegreeAngle(degreeAngle.TotalSeconds * factor);
        public static DegreeAngle operator *(double factor, DegreeAngle degreeAngle) =>
            degreeAngle * factor;

        public static DegreeAngle operator /(DegreeAngle degreeAngle, double denominator)
        {
            if (double.IsNaN(denominator))
                throw new ArgumentException("Denominator is not a number.", nameof(denominator));
            if (double.IsInfinity(denominator))
                throw new ArgumentException("Denominator is not finite number.", nameof(denominator));
            if (denominator == 0f)
                throw new DivideByZeroException("Denominator is zero.");

            return new DegreeAngle(degreeAngle.TotalSeconds / denominator);
        }
        public static double operator /(DegreeAngle degreeAngle, DegreeAngle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");

            return degreeAngle.TotalSeconds / denominator.TotalSeconds;
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

        public static DegreeAngle? operator *(DegreeAngle? degreeAngle, double factor) =>
            (degreeAngle ?? default(DegreeAngle)) * factor;
        public static DegreeAngle? operator *(double factor, DegreeAngle? degreeAngle) =>
            degreeAngle * factor;

        public static DegreeAngle? operator /(DegreeAngle? degreeAngle, double denominator) =>
            (degreeAngle ?? default(DegreeAngle)) / denominator;
        public static double operator /(DegreeAngle? degreeAngle, DegreeAngle? denominator) =>
            (degreeAngle ?? default(DegreeAngle)) / (denominator ?? default(DegreeAngle));
    }
}
