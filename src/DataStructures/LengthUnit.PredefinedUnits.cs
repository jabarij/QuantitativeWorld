using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct LengthUnit
    {
        [Predefined, Parsable]
        public static readonly LengthUnit Metre = new LengthUnit(name: "metre", abbreviation: "m", valueInMetres: 1m);
        [Predefined, Parsable]
        public static readonly LengthUnit Millimetre = new LengthUnit(name: "millimetre", abbreviation: "mm", valueInMetres: Constants.MetresPerMillimetre);
        [Predefined, Parsable]
        public static readonly LengthUnit Centimetre = new LengthUnit(name: "centimetre", abbreviation: "cm", valueInMetres: Constants.MetresPerCentimetre);
        [Predefined, Parsable]
        public static readonly LengthUnit Decimetre = new LengthUnit(name: "decimetre", abbreviation: "dm", valueInMetres: Constants.MetresPerDecimetre);
        [Predefined, Parsable]
        public static readonly LengthUnit Kilometre = new LengthUnit(name: "kilometre", abbreviation: "km", valueInMetres: Constants.MetresPerKilometre);

        [Predefined, Parsable]
        public static readonly LengthUnit Inch = new LengthUnit(name: "inch", abbreviation: "in", valueInMetres: Constants.MetresPerInch);
        [Predefined, Parsable]
        public static readonly LengthUnit Foot = new LengthUnit(name: "foot", abbreviation: "ft", valueInMetres: Inch.ValueInMetres * Constants.InchesPerFoot);
        [Predefined, Parsable]
        public static readonly LengthUnit Yard = new LengthUnit(name: "yard", abbreviation: "yd", valueInMetres: Foot.ValueInMetres * Constants.FeetPerYard);
        [Predefined, Parsable]
        public static readonly LengthUnit Rod = new LengthUnit(name: "rod", abbreviation: "rd", valueInMetres: Yard.ValueInMetres * Constants.YardsPerRod);
        [Predefined, Parsable]
        public static readonly LengthUnit Chain = new LengthUnit(name: "chain", abbreviation: "ch", valueInMetres: Rod.ValueInMetres * Constants.RodsPerChain);
        [Predefined, Parsable]
        public static readonly LengthUnit Furlong = new LengthUnit(name: "furlong", abbreviation: "fur", valueInMetres: Chain.ValueInMetres * Constants.ChainsPerFurlong);
        [Predefined, Parsable]
        public static readonly LengthUnit Mile = new LengthUnit(name: "mile", abbreviation: "mi", valueInMetres: Furlong.ValueInMetres * Constants.FurlongsPerMile);
        [Predefined, Parsable]
        public static readonly LengthUnit NauticalMile = new LengthUnit(name: "nautical mile", abbreviation: "nmi", valueInMetres: Constants.MetresPerNauticalMile);
        [Predefined, Parsable]
        public static readonly LengthUnit Cable = new LengthUnit(name: "cable", abbreviation: "cbl", valueInMetres: Chain.ValueInMetres * Constants.CablesPerNauticalMile);

        public static IEnumerable<LengthUnit> GetPredefinedUnits() =>
            typeof(LengthUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(LengthUnit))
            .Select(e => (LengthUnit)e.GetValue(null));

        internal static IEnumerable<LengthUnit> GetParsableUnits() =>
            typeof(LengthUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(ParsableAttribute), ParsableAttribute.Inherited) && e.FieldType == typeof(LengthUnit))
            .Select(e => (LengthUnit)e.GetValue(null));

    }
}
