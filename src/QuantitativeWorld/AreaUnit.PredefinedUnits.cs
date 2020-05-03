using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct AreaUnit
    {
        [Predefined]
        public static readonly AreaUnit SquareMetre = new AreaUnit(name: "square metre", abbreviation: "m²", valueInSquareMetres: 1d);
        [Predefined]
        public static readonly AreaUnit SquareMillimetre = new AreaUnit(name: "square millimetre", abbreviation: "mm²", valueInSquareMetres: Constants.SquareMetresPerSquareMillimetre);
        [Predefined]
        public static readonly AreaUnit SquareCentimetre = new AreaUnit(name: "square centimetre", abbreviation: "cm²", valueInSquareMetres: Constants.SquareMetresPerSquareCentimetre);
        [Predefined]
        public static readonly AreaUnit SquareKilometre = new AreaUnit(name: "square kilometre", abbreviation: "km²", valueInSquareMetres: Constants.SquareMetresPerSquareKilometre);

        [Predefined]
        public static readonly AreaUnit SquareInch = new AreaUnit(name: "square inch", abbreviation: "in²", valueInSquareMetres: Constants.SquareMetresPerSquareInch);
        [Predefined]
        public static readonly AreaUnit SquareFoot = new AreaUnit(name: "square foot", abbreviation: "ft²", valueInSquareMetres: SquareInch.ValueInSquareMetres * Constants.SquareInchesPerSquareFoot);
        [Predefined]
        public static readonly AreaUnit SquareYard = new AreaUnit(name: "square yard", abbreviation: "yd²", valueInSquareMetres: SquareFoot.ValueInSquareMetres * Constants.SquareFeetPerSquareYard);
        [Predefined]
        public static readonly AreaUnit SquareMile = new AreaUnit(name: "square mile", abbreviation: "mi²", valueInSquareMetres: SquareYard.ValueInSquareMetres * Constants.SquareYardsPerSquareMile);

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
