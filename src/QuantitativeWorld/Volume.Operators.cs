using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
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
            new Volume(formatUnit: left._formatUnit ?? right.Unit, cubicMetres: left.CubicMetres + right.CubicMetres);
        public static Volume operator -(Volume left, Volume right) =>
            new Volume(formatUnit: left._formatUnit ?? right.Unit, cubicMetres: left.CubicMetres - right.CubicMetres);
        public static Volume operator -(Volume argument) =>
            new Volume(formatUnit: argument.Unit, cubicMetres: -argument.CubicMetres);

        public static Volume operator *(Volume argument, double factor) =>
            new Volume(formatUnit: argument.Unit, cubicMetres: argument.CubicMetres * factor);
        public static Volume operator *(double argument, Volume factor) =>
            factor * argument;

        public static Volume operator /(Volume nominator, double denominator)
        {
            if (denominator == 0d)
                throw new DivideByZeroException("Denominator is zero.");
            return new Volume(formatUnit: nominator.Unit, cubicMetres: nominator.CubicMetres / denominator);
        }
        public static double operator /(Volume nominator, Volume denominator)
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

        public static Volume? operator *(Volume? argument, double factor) =>
            (argument ?? default(Volume)) * factor;
        public static Volume? operator *(double argument, Volume? factor) =>
            factor * argument;

        public static Volume? operator /(Volume? nominator, double denominator) =>
            (nominator ?? default(Volume)) / denominator;
        public static double operator /(Volume? nominator, Volume? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Volume));
        public static Area? operator /(Volume? nominator, Length? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Length));
        public static Length? operator /(Volume? nominator, Area? denominator) =>
            (nominator ?? default(Volume)) / (denominator ?? default(Area));
    }
}
