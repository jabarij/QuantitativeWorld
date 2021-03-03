using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld
{
    using Constants = DoubleConstants;
    using number = Double;
#endif

    partial struct Time
    {
        public static explicit operator TimeSpan(Time time) =>
            time.ToTimeSpan();
        public static explicit operator Time(TimeSpan timeSpan) =>
            FromTimeSpan(timeSpan);

        public static bool operator ==(Time left, Time right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Time left, Time right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Time left, Time right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Time left, Time right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Time left, Time right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Time left, Time right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Time operator +(Time left, Time right) =>
            new Time(left.TotalSeconds + right.TotalSeconds);
        public static Time operator -(Time left, Time right) =>
            new Time(left.TotalSeconds - right.TotalSeconds);
        public static Time operator -(Time time) =>
            new Time(-time.TotalSeconds);

        public static Time operator *(Time time, number factor) =>
            new Time(time.TotalSeconds * factor);
        public static Time operator *(number factor, Time time) =>
            time * factor;

        public static Time operator /(Time time, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Time(time.TotalSeconds / denominator);
        }
        public static number operator /(Time time, Time denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return time.TotalSeconds / denominator.TotalSeconds;
        }

        public static Time? operator +(Time? left, Time? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Time)) + (right ?? default(Time))
            : (Time?)null;
        public static Time? operator -(Time? left, Time? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Time)) - (right ?? default(Time))
            : (Time?)null;

        public static Time? operator *(Time? time, number factor) =>
            (time ?? default(Time)) * factor;
        public static Time? operator *(number factor, Time? time) =>
            time * factor;

        public static Time? operator /(Time? time, number denominator) =>
            (time ?? default(Time)) / denominator;
        public static number operator /(Time? time, Time? denominator) =>
            (time ?? default(Time)) / (denominator ?? default(Time));
    }
}
