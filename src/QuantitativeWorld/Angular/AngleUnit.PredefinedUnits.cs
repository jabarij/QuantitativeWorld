using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld.Angular
{
    partial struct AngleUnit
    {
        [Predefined]
        public static readonly AngleUnit Turn = new AngleUnit(name: "turn", abbreviation: "tr", symbol: "tr", unitsPerTurn: 1d);
        [Predefined]
        public static readonly AngleUnit Radian = new AngleUnit(name: "radian", abbreviation: "rad", symbol: "c", unitsPerTurn: Constants.RadiansPerTurn);
        [Predefined]
        public static readonly AngleUnit Degree = new AngleUnit(name: "degree", abbreviation: "deg", symbol: "°", unitsPerTurn: Constants.DegreesPerTurn);
        [Predefined]
        public static readonly AngleUnit Arcminute = new AngleUnit(name: "arcminute", abbreviation: "arcmin", symbol: "′", unitsPerTurn: Constants.ArcminutesPerTurn);
        [Predefined]
        public static readonly AngleUnit Arcsecond = new AngleUnit(name: "arcsecond", abbreviation: "arcsec", symbol: "″", unitsPerTurn: Constants.ArcsecondsPerTurn);
        [Predefined]
        public static readonly AngleUnit Gradian = new AngleUnit(name: "gradian", abbreviation: "gon", symbol: "ᵍ", unitsPerTurn: Constants.GradiansPerTurn);
        [Predefined]
        public static readonly AngleUnit NATOMil = new AngleUnit(name: "NATO mil", abbreviation: "mil", symbol: "mil", unitsPerTurn: Constants.NATOMilsPerTurn);

        public static IEnumerable<AngleUnit> GetPredefinedUnits() =>
            typeof(AngleUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(AngleUnit))
            .Select(e => (AngleUnit)e.GetValue(null));

    }
}
