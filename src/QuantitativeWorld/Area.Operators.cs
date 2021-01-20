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

    partial struct Area
    {
        public static bool operator ==(Area left, Area right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Area left, Area right) =>
            !Equality.AreEqualStructures(left, right);

        public static bool operator >(Area left, Area right) =>
            Equality.IsStructureGreaterThan(left, right);
        public static bool operator >=(Area left, Area right) =>
            Equality.IsStructureGreaterThanOrEqual(left, right);
        public static bool operator <(Area left, Area right) =>
            Equality.IsStructureLowerThan(left, right);
        public static bool operator <=(Area left, Area right) =>
            Equality.IsStructureLowerThanOrEqual(left, right);

        public static Area operator +(Area left, Area right) =>
            new Area(squareMetres: left.SquareMetres + right.SquareMetres, value: null, unit: left._unit ?? right.Unit);
        public static Area operator -(Area left, Area right) =>
            new Area(squareMetres: left.SquareMetres - right.SquareMetres, value: null, unit: left._unit ?? right.Unit);
        public static Area operator -(Area argument) =>
            new Area(squareMetres: -argument.SquareMetres, value: null, unit: argument._unit);

        public static Area operator *(Area argument, number factor) =>
            new Area(squareMetres: argument.SquareMetres * factor, value: null, unit: argument._unit);
        public static Area operator *(number argument, Area factor) =>
            factor * argument;

        public static Volume operator *(Area argument, Length factor) =>
            new Volume(cubicMetres: argument.SquareMetres * factor.Metres);
        public static Volume operator *(Length argument, Area factor) =>
            factor * argument;

        public static Area operator /(Area nominator, number denominator)
        {
            if (denominator == Constants.Zero)
                throw new DivideByZeroException("Denominator is zero.");
            return new Area(squareMetres: nominator.SquareMetres / denominator, value: null, unit: nominator._unit);
        }
        public static number operator /(Area nominator, Area denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return nominator.SquareMetres / denominator.SquareMetres;
        }
        public static Length operator /(Area nominator, Length denominator)
        {
            if (denominator.IsZero())
                throw new DivideByZeroException("Denominator is zero.");
            return new Length(nominator.SquareMetres / denominator.Metres);
        }

        public static Area? operator +(Area? left, Area? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Area)) + (right ?? default(Area))
            : (Area?)null;
        public static Area? operator -(Area? left, Area? right) =>
            left.HasValue || right.HasValue
            ? (left ?? default(Area)) - (right ?? default(Area))
            : (Area?)null;

        public static Area? operator *(Area? argument, number factor) =>
            (argument ?? default(Area)) * factor;
        public static Area? operator *(number argument, Area? factor) =>
            factor * argument;

        public static Volume? operator *(Area? argument, Length? factor) =>
            argument.HasValue || factor.HasValue
            ? (argument ?? default(Area)) * (factor ?? default(Length))
            : (Volume?)null;
        public static Volume? operator *(Length? argument, Area? factor) =>
            factor * argument;

        public static Area? operator /(Area? nominator, number denominator) =>
            (nominator ?? default(Area)) / denominator;
        public static number operator /(Area? nominator, Area? denominator) =>
            (nominator ?? default(Area)) / (denominator ?? default(Area));
        public static Length? operator /(Area? nominator, Length? denominator) =>
            (nominator ?? default(Area)) / (denominator ?? default(Length));
    }
}
