using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct Length
    {
        public static bool operator ==(Length left, Length right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Length left, Length right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Length left, Length right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Length left, Length right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Length left, Length right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Length left, Length right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Length operator +(Length left, Length right) =>
            new Length(metres: left.Metres + right.Metres,value:null, unit: left._unit ?? right.Unit);
        public static Length operator -(Length left, Length right) =>
            new Length(metres: left.Metres - right.Metres, value: null, unit: left._unit ?? right.Unit);
        public static Length operator -(Length length) =>
            new Length(metres: -length.Metres, value: null, unit: length._unit);

        public static Length operator *(Length length, number factor) =>
            new Length(metres: length.Metres * factor, value: null, unit: length._unit);
        public static Length operator *(number factor, Length length) =>
            length * factor;

        public static Area operator *(Length argument, Length factor) =>
            new Area(argument.Metres * factor.Metres);

        public static Length operator /(Length length, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Length(metres: length.Metres / denominator, value: null, unit: length._unit);
        }
        public static number operator /(Length length, Length denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return length.Metres / denominator.Metres;
        }
        public static Speed operator /(Length length, Time time)
        {
            if (time.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Speed(length.Metres / time.TotalSeconds);
        }

        public static Length? operator +(Length? left, Length? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Length)) + (right ?? default(Length))
            : (Length?)null;
        public static Length? operator -(Length? left, Length? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Length)) - (right ?? default(Length))
            : (Length?)null;

        public static Length? operator *(Length? length, number factor) =>
            (length ?? default(Length)) * factor;
        public static Length? operator *(number factor, Length? length) =>
            length * factor;

        public static Area? operator *(Length? argument, Length? factor) =>
            argument.HasValue || factor.HasValue
            ? (argument ?? default(Length)) * (factor ?? default(Length))
            : (Area?)null;

        public static Length? operator /(Length? length, number denominator) =>
            (length ?? default(Length)) / denominator;
        public static number operator /(Length? length, Length? denominator) =>
            (length ?? default(Length)) / (denominator ?? default(Length));
        public static Speed operator /(Length? length, Time? time) =>
            (length ?? default(Length)) / (time ?? default(Time));

        public static Length Abs(Length length) =>
            new Length(
                metres: Math.Abs(length.Metres),
                value:
                    length._value.HasValue
                    ? Math.Abs(length._value.Value)
                    : (number?)null,
                unit: length._unit);
        public static Length? Abs(Length? length) =>
            length.HasValue
            ? Abs(length.Value)
            : (Length?)null;
    }
}
