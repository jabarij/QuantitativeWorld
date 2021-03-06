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

    partial struct Volume
    {
        public static bool operator ==(Volume left, Volume right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Volume left, Volume right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Volume left, Volume right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Volume left, Volume right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Volume left, Volume right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Volume left, Volume right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Volume operator +(Volume left, Volume right) =>
            new Volume(cubicMetres: left.CubicMetres + right.CubicMetres, value: null, unit: left._unit ?? right.Unit);
        public static Volume operator -(Volume left, Volume right) =>
            new Volume(cubicMetres: left.CubicMetres - right.CubicMetres, value: null, unit: left._unit ?? right.Unit);
        public static Volume operator -(Volume argument) =>
            new Volume(cubicMetres: -argument.CubicMetres, value: null, unit: argument._unit);

        public static Volume operator *(Volume argument, number factor) =>
            new Volume(cubicMetres: argument.CubicMetres * factor, value: null, unit: argument._unit);
        public static Volume operator *(number argument, Volume factor) =>
            factor * argument;

        public static Volume operator /(Volume nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Volume(cubicMetres: nominator.CubicMetres / denominator, value: null, unit: nominator._unit);
        }
        public static number operator /(Volume nominator, Volume denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.CubicMetres / denominator.CubicMetres;
        }
        public static Area operator /(Volume nominator, Length denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Area(nominator.CubicMetres / denominator.Metres);
        }
        public static Length operator /(Volume nominator, Area denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Length(nominator.CubicMetres / denominator.SquareMetres);
        }

        public static Volume? operator +(Volume? left, Volume? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Volume)) + (right ?? default(Volume))
            : (Volume?)null;
        public static Volume? operator -(Volume? left, Volume? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Volume)) - (right ?? default(Volume))
            : (Volume?)null;

        public static Volume? operator *(Volume? argument, number factor) =>
            (argument ?? default(Volume)) * factor;
        public static Volume? operator *(number argument, Volume? factor) =>
            factor * argument;

        public static Volume? operator /(Volume? nominator, number denominator) =>
            (nominator ?? default(Volume)) / denominator;
        public static number operator /(Volume? nominator, Volume? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Volume));
        public static Area? operator /(Volume? nominator, Length? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Length));
        public static Length? operator /(Volume? nominator, Area? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Area));
    }
}
