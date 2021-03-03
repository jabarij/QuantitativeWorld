
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Parsing;
    using Constants = DecimalConstants;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Parsing;
    using Constants = DoubleConstants;
#endif

    partial struct VolumeUnit
    {
        [Predefined]
        public static readonly VolumeUnit CubicMetre = new VolumeUnit(name: "cubic metre", abbreviation: "m³", valueInCubicMetres: Constants.One);
        [Predefined]
        public static readonly VolumeUnit CubicMillimetre = new VolumeUnit(name: "cubic millimetre", abbreviation: "mm³", valueInCubicMetres: Constants.CubicMetresPerCubicMillimetre);
        [Predefined]
        public static readonly VolumeUnit CubicCentimetre = new VolumeUnit(name: "cubic centimetre", abbreviation: "cm³", valueInCubicMetres: Constants.CubicMetresPerCubicCentimetre);
        [Predefined]
        public static readonly VolumeUnit CubicDecimetre = new VolumeUnit(name: "cubic decimetre", abbreviation: "dm³", valueInCubicMetres: Constants.CubicMetresPerCubicDecimetre);
        [Predefined]
        public static readonly VolumeUnit CubicKilometre = new VolumeUnit(name: "cubic kilometre", abbreviation: "km³", valueInCubicMetres: Constants.CubicMetresPerCubicKilometre);

        [Predefined]
        public static readonly VolumeUnit CubicInch = new VolumeUnit(name: "cubic inch", abbreviation: "in³", valueInCubicMetres: Constants.CubicMetresPerCubicInch);
        [Predefined]
        public static readonly VolumeUnit CubicFoot = new VolumeUnit(name: "cubic foot", abbreviation: "ft³", valueInCubicMetres: CubicInch.ValueInCubicMetres * Constants.CubicInchesPerCubicFoot);
        [Predefined]
        public static readonly VolumeUnit CubicYard = new VolumeUnit(name: "cubic yard", abbreviation: "yd³", valueInCubicMetres: CubicFoot.ValueInCubicMetres * Constants.CubicFeetPerCubicYard);
        [Predefined]
        public static readonly VolumeUnit CubicRod = new VolumeUnit(name: "rod", abbreviation: "rd³", valueInCubicMetres: CubicYard.ValueInCubicMetres * Constants.CubicYardsPerCubicRod);
        [Predefined]
        public static readonly VolumeUnit CubicChain = new VolumeUnit(name: "chain", abbreviation: "ch³", valueInCubicMetres: CubicRod.ValueInCubicMetres * Constants.CubicRodsPerCubicChain);
        [Predefined]
        public static readonly VolumeUnit CubicFurlong = new VolumeUnit(name: "furlong", abbreviation: "fur³", valueInCubicMetres: CubicChain.ValueInCubicMetres * Constants.CubicChainsPerCubicFurlong);
        [Predefined]
        public static readonly VolumeUnit CubicMile = new VolumeUnit(name: "cubic mile", abbreviation: "mi³", valueInCubicMetres: CubicFurlong.ValueInCubicMetres * Constants.CubicFurlongsPerCubicMile);
        [Predefined]
        public static readonly VolumeUnit CubicLeague = new VolumeUnit(name: "cubic league", abbreviation: "lea³", valueInCubicMetres: CubicMile.ValueInCubicMetres * Constants.CubicMilesPerCubicLeague);

        [Predefined]
        public static readonly VolumeUnit Litre = new VolumeUnit(name: "litre", abbreviation: "l", valueInCubicMetres: Constants.CubicMetresPerLitre);
        [Predefined]
        public static readonly VolumeUnit Millilitre = new VolumeUnit(name: "millilitre", abbreviation: "ml", valueInCubicMetres: Litre.ValueInCubicMetres * Constants.MillilitresPerLitre);
        [Predefined]
        public static readonly VolumeUnit Centilitre = new VolumeUnit(name: "centilitre", abbreviation: "cl", valueInCubicMetres: Litre.ValueInCubicMetres * Constants.CentilitresPerLitre);
        [Predefined]
        public static readonly VolumeUnit Decilitre = new VolumeUnit(name: "decilitre", abbreviation: "dl", valueInCubicMetres: Litre.ValueInCubicMetres * Constants.DecilitresPerLitre);
        [Predefined]
        public static readonly VolumeUnit Hectolitre = new VolumeUnit(name: "hectolitre", abbreviation: "hl", valueInCubicMetres: Litre.ValueInCubicMetres * Constants.HectolitresPerLitre);

        [Predefined]
        public static readonly VolumeUnit ImperialGallon = new VolumeUnit(name: "imperial gallon", abbreviation: "imp gal", valueInCubicMetres: Constants.ImperialGallonsPerCubicMetre);
        [Predefined]
        public static readonly VolumeUnit ImperialQuart = new VolumeUnit(name: "imperial quart", abbreviation: "imp qt", valueInCubicMetres: ImperialGallon.ValueInCubicMetres * Constants.ImperialQuartsPerImperialGallon);
        [Predefined]
        public static readonly VolumeUnit ImperialPint = new VolumeUnit(name: "imperial pint", abbreviation: "imp pt", valueInCubicMetres: ImperialGallon.ValueInCubicMetres * Constants.ImperialPintsPerImperialGallon);

        [Predefined]
        public static readonly VolumeUnit USLiquidGallon = new VolumeUnit(name: "US gallon", abbreviation: "US gal", valueInCubicMetres: Constants.USLiquidGallonsPerCubicMetre);
        [Predefined]
        public static readonly VolumeUnit USDryGallon = new VolumeUnit(name: "US dry gallon", abbreviation: "US dry gal", valueInCubicMetres: Constants.USDryGallonsPerCubicMetre);

        [Predefined]
        public static readonly VolumeUnit OilBarrel = new VolumeUnit(name: "oil barrel", abbreviation: "bbl", valueInCubicMetres: Constants.USLiquidGallonsPerOilBarrel);
        [Predefined]
        public static readonly VolumeUnit ThousandOilBarrels = new VolumeUnit(name: "thousand of oil barrels", abbreviation: "Mbbl", valueInCubicMetres: OilBarrel.ValueInCubicMetres * Constants.Thousand);
        [Predefined]
        public static readonly VolumeUnit MillionOilBarrels = new VolumeUnit(name: "million of oil barrels", abbreviation: "MMbbl", valueInCubicMetres: OilBarrel.ValueInCubicMetres * Constants.Million);
        [Predefined]
        public static readonly VolumeUnit BillionOilBarrels = new VolumeUnit(name: "billion of oil barrel", abbreviation: "Gbbl", valueInCubicMetres: OilBarrel.ValueInCubicMetres * Constants.Billion);

        public static IEnumerable<VolumeUnit> GetPredefinedUnits() =>
            typeof(VolumeUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(VolumeUnit))
            .Select(e => (VolumeUnit)e.GetValue(null));

    }
}
