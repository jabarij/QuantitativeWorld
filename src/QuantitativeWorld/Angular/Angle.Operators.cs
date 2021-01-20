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

    partial struct Angle
    {
        public static bool operator ==(Angle left, Angle right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Angle left, Angle right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Angle left, Angle right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Angle left, Angle right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Angle left, Angle right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Angle left, Angle right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Angle operator +(Angle left, Angle right) =>
            new Angle(turns: left.Turns + right.Turns, value: null, unit: left._unit ?? right.Unit);
        public static Angle operator -(Angle left, Angle right) =>
            new Angle(turns: left.Turns - right.Turns, value: null, unit: left._unit ?? right.Unit);
        public static Angle operator -(Angle argument) =>
            new Angle(turns: -argument.Turns, value: null, unit: argument._unit);

        public static Angle operator *(Angle argument, number factor) =>
            new Angle(turns: argument.Turns * factor, value: null, unit: argument._unit);
        public static Angle operator *(number argument, Angle factor) =>
            factor * argument;

        public static Angle operator /(Angle nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Angle(turns: nominator.Turns / denominator, value: null, unit: nominator._unit);
        }
        public static number operator /(Angle nominator, Angle denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.Turns / denominator.Turns;
        }

        public static Angle operator %(Angle nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");

            var nominatorUnit = nominator.Unit;
            if (nominatorUnit.IsEquivalentOf(DefaultUnit))
                return new Angle(nominator.Turns % denominator, value: null, unit: nominator._unit);
            else
            {
                number resultValue = nominator.Value % denominator;
                number turns = GetTurns(resultValue, nominatorUnit);
                return new Angle(turns, resultValue, nominatorUnit);
            }
        }

        public static Angle? operator +(Angle? left, Angle? right)
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
        public static Angle? operator -(Angle? left, Angle? right)
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

        public static Angle? operator *(Angle? argument, number factor) =>
            (argument ?? default(Angle)) * factor;
        public static Angle? operator *(number argument, Angle? factor) =>
            factor * argument;

        public static Angle? operator /(Angle? nominator, number denominator) =>
            (nominator ?? default(Angle)) / denominator;
        public static number operator /(Angle? nominator, Angle? denominator) =>
            (nominator ?? default(Angle)) / (denominator ?? default(Angle));

        public static Angle? operator %(Angle? nominator, number denominator) =>
            (nominator ?? default(Angle)) % denominator;
    }
}
