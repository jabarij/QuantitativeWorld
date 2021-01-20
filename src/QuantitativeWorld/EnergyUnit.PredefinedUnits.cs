using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct EnergyUnit
    {
        [Predefined]
        public static readonly EnergyUnit Joule = new EnergyUnit(name: "joule", abbreviation: "J", valueInJoules: Constants.One);
        [Predefined]
        public static readonly EnergyUnit Kilojoule = new EnergyUnit(name: "kilojoule", abbreviation: "kJ", valueInJoules: Constants.JoulesPerKilojoule);
        [Predefined]
        public static readonly EnergyUnit Megajoule = new EnergyUnit(name: "megajoule", abbreviation: "MJ", valueInJoules: Constants.JoulesPerMegajoule);

        [Predefined]
        public static readonly EnergyUnit WattSecond = new EnergyUnit(name: "watt-second", abbreviation: "Ws", valueInJoules: Constants.JoulesPerWattSecond);
        [Predefined]
        public static readonly EnergyUnit WattHour = new EnergyUnit(name: "watt-hour", abbreviation: "Wh", valueInJoules: Constants.JoulesPerWattHour);
        [Predefined]
        public static readonly EnergyUnit KilowattHour = new EnergyUnit(name: "kilowatt-hour", abbreviation: "kWh", valueInJoules: Constants.JoulesPerKilowattHour);
        [Predefined]
        public static readonly EnergyUnit MegawattHour = new EnergyUnit(name: "megawatt-hour", abbreviation: "MWh", valueInJoules: Constants.JoulesPerMegawattHour);

        [Predefined]
        public static readonly EnergyUnit Calorie = new EnergyUnit(name: "calorie", abbreviation: "cal", valueInJoules: Constants.JoulesPerCalorie);
        [Predefined]
        public static readonly EnergyUnit Kilocalorie = new EnergyUnit(name: "kilocalorie", abbreviation: "kcal", valueInJoules: Constants.JoulesPerKilocalorie);

        [Predefined]
        public static readonly EnergyUnit Electronvolt = new EnergyUnit(name: "electronvolt", abbreviation: "eV", valueInJoules: Constants.JoulesPerElectronvolt);
        [Predefined]
        public static readonly EnergyUnit FootPoundForce = new EnergyUnit(name: "foot-pound force", abbreviation: "ft⋅lbf", valueInJoules: Constants.JoulesPerFootPoundForce);
        [Predefined]
        public static readonly EnergyUnit Erg = new EnergyUnit(name: "erg", abbreviation: "erg", valueInJoules: Constants.JoulesPerErg);
        [Predefined]
        public static readonly EnergyUnit StheneMetre = new EnergyUnit(name: "sthène-metre", abbreviation: "sn⋅m", valueInJoules: Constants.JoulesPerStheneMetre);

        public static IEnumerable<EnergyUnit> GetPredefinedUnits() =>
            typeof(EnergyUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(EnergyUnit))
            .Select(e => (EnergyUnit)e.GetValue(null));

    }
}
