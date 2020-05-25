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
            left.Unit.IsEquivalentOf(right.Unit)
            ? new Speed(value: left.Value + right.Value, unit: left.Unit)
            : new Speed(unit: left._unit ?? right.Unit, metresPerSecond: left.MetresPerSecond + right.MetresPerSecond);
        public static Speed operator -(Speed left, Speed right) =>
            left.Unit.IsEquivalentOf(right.Unit)
            ? new Speed(value: left.Value - right.Value, unit: left.Unit)
            : new Speed(unit: left._unit ?? right.Unit, metresPerSecond: left.MetresPerSecond - right.MetresPerSecond);
        public static Speed operator -(Speed speed) =>
            speed._value.HasValue
            ? new Speed(value: -speed._value.Value, unit: speed.Unit)
            : new Speed(unit: speed.Unit, metresPerSecond: -speed.MetresPerSecond);

        public static Speed operator *(Speed speed, double factor)
        {
            if (double.IsNaN(factor))
                throw new ArgumentException("Argument is not a number.", nameof(factor));
            return
                speed._value.HasValue
                ? new Speed(
                    value: speed._value.Value * factor,
                    unit: speed._unit.Value)
                : new Speed(
                    unit: speed.Unit,
                    metresPerSecond: speed.MetresPerSecond * factor);
        }
        public static Speed operator *(double factor, Speed speed) =>
            speed * factor;
        public static Length operator *(Speed speed, Time time) =>
            new Length(speed.MetresPerSecond * time.TotalSeconds);
        public static Length operator *(Time time, Speed speed) =>
            speed * time;

        public static Speed operator /(Speed speed, double denominator)
        {
            if (double.IsNaN(denominator))
                throw new ArgumentException("Argument is not a number.", nameof(denominator));
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return
                speed._value.HasValue
                ? new Speed(
                    value: speed._value.Value / denominator,
                    unit: speed._unit.Value)
                : new Speed(
                    unit: speed.Unit,
                    metresPerSecond: speed.MetresPerSecond / denominator);
        }
        public static double operator /(Speed speed, Speed denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return
                speed.Unit.IsEquivalentOf(denominator.Unit)
                ? speed.Value / denominator.Value
                : speed.MetresPerSecond / denominator.MetresPerSecond;
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
        public static Length? operator *(Speed? speed, Time? time) =>
            (speed ?? default(Speed)) * (time ?? default(Time));
        public static Length? operator *(Time? time, Speed? speed) =>
            speed * time;

        public static Speed? operator /(Speed? speed, double denominator) =>
            (speed ?? default(Speed)) / denominator;
        public static double operator /(Speed? speed, Speed? denominator) =>
            (speed ?? default(Speed)) / (denominator ?? default(Speed));
    }
}