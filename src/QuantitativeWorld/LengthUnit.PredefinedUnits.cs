using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct LengthUnit
    {
        [Predefined]
        public static readonly LengthUnit Metre = new LengthUnit(name: "metre", abbreviation: "m", valueInMetres: 1d);
        [Predefined]
        public static readonly LengthUnit Millimetre = new LengthUnit(name: "millimetre", abbreviation: "mm", valueInMetres: Constants.MetresPerMillimetre);
        [Predefined]
        public static readonly LengthUnit Centimetre = new LengthUnit(name: "centimetre", abbreviation: "cm", valueInMetres: Constants.MetresPerCentimetre);
        [Predefined]
        public static readonly LengthUnit Decimetre = new LengthUnit(name: "decimetre", abbreviation: "dm", valueInMetres: Constants.MetresPerDecimetre);
        [Predefined]
        public static readonly LengthUnit Kilometre = new LengthUnit(name: "kilometre", abbreviation: "km", valueInMetres: Constants.MetresPerKilometre);

        [Predefined]
        public static readonly LengthUnit Inch = new LengthUnit(name: "inch", abbreviation: "in", valueInMetres: Constants.MetresPerInch);
        [Predefined]
        public static readonly LengthUnit Foot = new LengthUnit(name: "foot", abbreviation: "ft", valueInMetres: Inch.ValueInMetres * Constants.InchesPerFoot);
        [Predefined]
        public static readonly LengthUnit Yard = new LengthUnit(name: "yard", abbreviation: "yd", valueInMetres: Foot.ValueInMetres * Constants.FeetPerYard);
        [Predefined]
        public static readonly LengthUnit Rod = new LengthUnit(name: "rod", abbreviation: "rd", valueInMetres: Yard.ValueInMetres * Constants.YardsPerRod);
        [Predefined]
        public static readonly LengthUnit Chain = new LengthUnit(name: "chain", abbreviation: "ch", valueInMetres: Rod.ValueInMetres * Constants.RodsPerChain);
        [Predefined]
        public static readonly LengthUnit Furlong = new LengthUnit(name: "furlong", abbreviation: "fur", valueInMetres: Chain.ValueInMetres * Constants.ChainsPerFurlong);
        [Predefined]
        public static readonly LengthUnit Mile = new LengthUnit(name: "mile", abbreviation: "mi", valueInMetres: Furlong.ValueInMetres * Constants.FurlongsPerMile);
        [Predefined]
        public static readonly LengthUnit League = new LengthUnit(name: "league", abbreviation: "lea", valueInMetres: Mile.ValueInMetres * Constants.MilesPerLeague);

        [Predefined]
        public static readonly LengthUnit Fathom = new LengthUnit(name: "fathom", abbreviation: "ftm", valueInMetres: Yard.ValueInMetres * Constants.YardsPerFathom);
        [Predefined]
        public static readonly LengthUnit Cable = new LengthUnit(name: "cable", abbreviation: "cbl", valueInMetres: Fathom.ValueInMetres * Constants.FathomsPerCable);
        [Predefined]
        public static readonly LengthUnit NauticalMile = new LengthUnit(name: "nautical mile", abbreviation: "nmi", valueInMetres: Constants.MetresPerNauticalMile);

        public static IEnumerable<LengthUnit> GetPredefinedUnits() =>
            typeof(LengthUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(LengthUnit))
            .Select(e => (LengthUnit)e.GetValue(null));

    }
}
