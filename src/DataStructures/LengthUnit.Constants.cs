using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct LengthUnit
    {
        [Parsable]
        public static readonly LengthUnit Metre = new LengthUnit(name: "metre", abbreviation: "m", valueInMetres: 1m);
        [Parsable]
        public static readonly LengthUnit Millimetre = MetricPrefix.Milli * Metre;
        [Parsable]
        public static readonly LengthUnit Centimetre = MetricPrefix.Centi * Metre;
        [Parsable]
        public static readonly LengthUnit Decimetre = MetricPrefix.Deci * Metre;
        [Parsable]
        public static readonly LengthUnit Kilometre = MetricPrefix.Kilo * Metre;

        [Parsable]
        public static readonly LengthUnit Inch = new LengthUnit(name: "inch", abbreviation: "in", valueInMetres: Constants.MetresPerInch);
        [Parsable]
        public static readonly LengthUnit Foot = new LengthUnit(name: "foot", abbreviation: "ft", valueInMetres: Inch.ValueInMetres * Constants.InchesPerFoot);
        [Parsable]
        public static readonly LengthUnit Yard = new LengthUnit(name: "yard", abbreviation: "yd", valueInMetres: Foot.ValueInMetres * Constants.FeetPerYard);
        [Parsable]
        public static readonly LengthUnit Rod = new LengthUnit(name: "rod", abbreviation: "rd", valueInMetres: Yard.ValueInMetres * Constants.YardsPerRod);
        [Parsable]
        public static readonly LengthUnit Chain = new LengthUnit(name: "chain", abbreviation: "ch", valueInMetres: Rod.ValueInMetres * Constants.RodsPerChain);
        [Parsable]
        public static readonly LengthUnit Furlong = new LengthUnit(name: "furlong", abbreviation: "fur", valueInMetres: Chain.ValueInMetres * Constants.ChainsPerFurlong);
        [Parsable]
        public static readonly LengthUnit Mile = new LengthUnit(name: "mile", abbreviation: "mi", valueInMetres: Furlong.ValueInMetres * Constants.FurlongsPerMile);
        [Parsable]
        public static readonly LengthUnit NauticalMile = new LengthUnit(name: "nautical mile", abbreviation: "nmi", valueInMetres: Constants.MetresPerNauticalMile);
        [Parsable]
        public static readonly LengthUnit Cable = new LengthUnit(name: "cable", abbreviation: "cbl", valueInMetres: Chain.ValueInMetres * Constants.CablesPerNauticalMile);

        public static IEnumerable<LengthUnit> GetKnownUnits() =>
            GetParsableUnits();

        internal static IEnumerable<LengthUnit> GetParsableUnits() =>
            typeof(LengthUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(ParsableAttribute), ParsableAttribute.Inherited))
            .Select(e => (LengthUnit)e.GetValue(null));

    }
}
