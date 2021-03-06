﻿using Common.Internals.DotNetExtensions;
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
            new Speed(metresPerSecond: left.MetresPerSecond + right.MetresPerSecond, value: null, unit: left._unit ?? right.Unit);
        public static Speed operator -(Speed left, Speed right) =>
            new Speed(metresPerSecond: left.MetresPerSecond - right.MetresPerSecond, value: null, unit: left._unit ?? right.Unit);
        public static Speed operator -(Speed speed) =>
            new Speed(metresPerSecond: -speed.MetresPerSecond, value: null, unit: speed._unit);

        public static Speed operator *(Speed speed, number factor) =>
            new Speed(metresPerSecond: speed.MetresPerSecond * factor, value: null, unit: speed._unit);
        public static Speed operator *(number factor, Speed speed) =>
            speed * factor;
        public static Length operator *(Speed speed, Time time) =>
            new Length(speed.MetresPerSecond * time.TotalSeconds);
        public static Length operator *(Time time, Speed speed) =>
            speed * time;

        public static Speed operator /(Speed speed, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Speed(metresPerSecond: speed.MetresPerSecond / denominator, value: null, unit: speed._unit);
        }
        public static number operator /(Speed speed, Speed denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return speed.MetresPerSecond / denominator.MetresPerSecond;
        }
        public static Time operator /(Length length, Speed speed)
        {
            if (speed.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Time(length.Metres / speed.MetresPerSecond);
        }

        public static Speed? operator +(Speed? left, Speed? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Speed)) + (right ?? default(Speed))
            : (Speed?)null;
        public static Speed? operator -(Speed? left, Speed? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Speed)) - (right ?? default(Speed))
            : (Speed?)null;

        public static Speed? operator *(Speed? speed, number factor) =>
            (speed ?? default(Speed)) * factor;
        public static Speed? operator *(number factor, Speed? speed) =>
            speed * factor;
        public static Length? operator *(Speed? speed, Time? time) =>
            (speed ?? default(Speed)) * (time ?? default(Time));
        public static Length? operator *(Time? time, Speed? speed) =>
            speed * time;

        public static Speed? operator /(Speed? speed, number denominator) =>
            (speed ?? default(Speed)) / denominator;
        public static number operator /(Speed? speed, Speed? denominator) =>
            (speed ?? default(Speed)) / (denominator ?? default(Speed));
        public static Time? operator /(Length? length, Speed? denominator) =>
            (length ?? default(Length)) / (denominator ?? default(Speed));
    }
}