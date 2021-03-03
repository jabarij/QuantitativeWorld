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

    partial struct PowerUnit
    {
        [Predefined]
        public static readonly PowerUnit Watt = new PowerUnit(name: "watt", abbreviation: "W", valueInWatts: Constants.One);
        [Predefined]
        public static readonly PowerUnit Milliwatt = new PowerUnit(name: "milliwatt", abbreviation: "mW", valueInWatts: Constants.WattsPerMilliwatt);
        [Predefined]
        public static readonly PowerUnit Kilowatt = new PowerUnit(name: "kilowatt", abbreviation: "kW", valueInWatts: Constants.WattsPerKilowatt);
        [Predefined]
        public static readonly PowerUnit Megawatt = new PowerUnit(name: "megawatt", abbreviation: "MW", valueInWatts: Constants.WattsPerMegawatt);

        [Predefined]
        public static readonly PowerUnit ErgPerSecond = new PowerUnit(name: "erg per second", abbreviation: "erg/s", valueInWatts: Constants.WattsPerErgPerSecond);

        [Predefined]
        public static readonly PowerUnit StheneMetrePerSecond = new PowerUnit(name: "sthène-metre per second", abbreviation: " sn⋅m/s", valueInWatts: Constants.WattsPerStheneMetrePerSecond);

        [Predefined]
        public static readonly PowerUnit MechanicalHorsepower = new PowerUnit(name: "mechanical horsepower", abbreviation: "hp", valueInWatts: Constants.WattsPerMechanicalHorsepower);
        [Predefined]
        public static readonly PowerUnit MetricHorsepower = new PowerUnit(name: "metric horsepower", abbreviation: "PS", valueInWatts: Constants.WattsPerMetricHorsepower);

        public static IEnumerable<PowerUnit> GetPredefinedUnits() =>
            typeof(PowerUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(PowerUnit))
            .Select(e => (PowerUnit)e.GetValue(null));

    }
}
