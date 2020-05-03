using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
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
            new Area(formatUnit: left._formatUnit ?? right.Unit, metres: left.SquareMetres + right.SquareMetres);
        public static Area operator -(Area left, Area right) =>
            new Area(formatUnit: left._formatUnit ?? right.Unit, metres: left.SquareMetres - right.SquareMetres);
        public static Area operator -(Area argument) =>
            new Area(formatUnit: argument.Unit, metres: -argument.SquareMetres);

        public static Area operator *(Area argument, double factor) =>
            new Area(formatUnit: argument.Unit, metres: argument.SquareMetres * factor);
        public static Area operator *(double argument, Area factor) =>
            factor * argument;

        public static Area operator /(Area nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Area(formatUnit: nominator.Unit, metres: nominator.SquareMetres / denominator);
        }
        public static double operator /(Area nominator, Area denominator)
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

        public static Area? operator *(Area? argument, double factor) =>
            (argument ?? default(Area)) * factor;
        public static Area? operator *(double argument, Area? factor) =>
            factor * argument;

        public static Area? operator /(Area? nominator, double denominator) =>
            (nominator ?? default(Area)) / denominator;
        public static double operator /(Area? nominator, Area? denominator) =>
            (nominator ?? default(Area)) / (denominator ?? default(Area));
        public static Length? operator /(Area? nominator, Length? denominator) =>
            (nominator ?? default(Area)) / (denominator ?? default(Length));
    }
}
