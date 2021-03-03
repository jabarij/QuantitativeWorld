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

    partial struct AreaUnit
    {
        [Predefined]
        public static readonly AreaUnit SquareMetre = new AreaUnit(name: "square metre", abbreviation: "m²", valueInSquareMetres: Constants.One);
        [Predefined]
        public static readonly AreaUnit SquareMillimetre = new AreaUnit(name: "square millimetre", abbreviation: "mm²", valueInSquareMetres: Constants.SquareMetresPerSquareMillimetre);
        [Predefined]
        public static readonly AreaUnit SquareCentimetre = new AreaUnit(name: "square centimetre", abbreviation: "cm²", valueInSquareMetres: Constants.SquareMetresPerSquareCentimetre);
        [Predefined]
        public static readonly AreaUnit Decimetre = new AreaUnit(name: "square decimetre", abbreviation: "dm²", valueInSquareMetres: Constants.SquareMetresPerSquareDecimetre);
        [Predefined]
        public static readonly AreaUnit SquareKilometre = new AreaUnit(name: "square kilometre", abbreviation: "km²", valueInSquareMetres: Constants.SquareMetresPerSquareKilometre);

        [Predefined]
        public static readonly AreaUnit SquareInch = new AreaUnit(name: "square inch", abbreviation: "in²", valueInSquareMetres: Constants.SquareMetresPerSquareInch);
        [Predefined]
        public static readonly AreaUnit SquareFoot = new AreaUnit(name: "square foot", abbreviation: "ft²", valueInSquareMetres: SquareInch.ValueInSquareMetres * Constants.SquareInchesPerSquareFoot);
        [Predefined]
        public static readonly AreaUnit SquareYard = new AreaUnit(name: "square yard", abbreviation: "yd²", valueInSquareMetres: SquareFoot.ValueInSquareMetres * Constants.SquareFeetPerSquareYard);
        [Predefined]
        public static readonly AreaUnit SquareRod = new AreaUnit(name: "rod", abbreviation: "rd²", valueInSquareMetres: SquareYard.ValueInSquareMetres * Constants.SquareYardsPerSquareRod);
        [Predefined]
        public static readonly AreaUnit SquareChain = new AreaUnit(name: "chain", abbreviation: "ch²", valueInSquareMetres: SquareRod.ValueInSquareMetres * Constants.SquareRodsPerSquareChain);
        [Predefined]
        public static readonly AreaUnit SquareFurlong = new AreaUnit(name: "furlong", abbreviation: "fur²", valueInSquareMetres: SquareChain.ValueInSquareMetres * Constants.SquareChainsPerSquareFurlong);
        [Predefined]
        public static readonly AreaUnit SquareMile = new AreaUnit(name: "square mile", abbreviation: "mi²", valueInSquareMetres: SquareFurlong.ValueInSquareMetres * Constants.SquareFurlongsPerSquareMile);
        [Predefined]
        public static readonly AreaUnit SquareLeague = new AreaUnit(name: "square league", abbreviation: "lea²", valueInSquareMetres: SquareMile.ValueInSquareMetres * Constants.SquareMilesPerSquareLeague);

        [Predefined]
        public static readonly AreaUnit Are = new AreaUnit(name: "are", abbreviation: "a", valueInSquareMetres: Constants.SquareMetresPerAre);
        [Predefined]
        public static readonly AreaUnit Hectare = new AreaUnit(name: "hectare", abbreviation: "ha", valueInSquareMetres: Are.ValueInSquareMetres * Constants.AresPerHectare);

        [Predefined]
        public static readonly AreaUnit Acre = new AreaUnit(name: "acre", abbreviation: "ac", valueInSquareMetres: SquareYard.ValueInSquareMetres * Constants.SquareYardsPerAcre);

        public static IEnumerable<AreaUnit> GetPredefinedUnits() =>
            typeof(AreaUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(AreaUnit))
            .Select(e => (AreaUnit)e.GetValue(null));

    }
}
