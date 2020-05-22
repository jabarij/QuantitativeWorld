using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    partial struct Speed
    {
        public static bool operator ==(Speed left, Speed right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Speed left, Speed right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Speed left, Speed right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Speed left, Speed right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Speed left, Speed right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Speed left, Speed right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Speed operator +(Speed left, Speed right) =>
            new Speed(formatUnit: left._formatUnit ?? right.Unit, metresPerSecond: left.MetresPerSecond + right.MetresPerSecond);
        public static Speed operator -(Speed left, Speed right) =>
            new Speed(formatUnit: left._formatUnit ?? right.Unit, metresPerSecond: left.MetresPerSecond - right.MetresPerSecond);
        public static Speed operator -(Speed speed) =>
            new Speed(formatUnit: speed.Unit, metresPerSecond: -speed.MetresPerSecond);

        public static Speed operator *(Speed speed, double factor) =>
            new Speed(formatUnit: speed.Unit, metresPerSecond: speed.MetresPerSecond * factor);
        public static Speed operator *(double factor, Speed speed) =>
            speed * factor;

        public static Speed operator /(Speed speed, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Speed(formatUnit: speed.Unit, metresPerSecond: speed.MetresPerSecond / denominator);
        }
        public static double operator /(Speed speed, Speed denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return speed.MetresPerSecond / denominator.MetresPerSecond;
        }

        public static Speed? operator +(Speed? left, Speed? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Speed)) + (right ?? default(Speed))
            : (Speed?)null;
        public static Speed? operator -(Speed? left, Speed? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Speed)) - (right ?? default(Speed))
            : (Speed?)null;

        public static Speed? operator *(Speed? speed, double factor) =>
            (speed ?? default(Speed)) * factor;
        public static Speed? operator *(double factor, Speed? speed) =>
            speed * factor;

        public static Speed? operator /(Speed? speed, double denominator) =>
            (speed ?? default(Speed)) / denominator;
        public static double operator /(Speed? speed, Speed? denominator) =>
            (speed ?? default(Speed)) / (denominator ?? default(Speed));
    }
}