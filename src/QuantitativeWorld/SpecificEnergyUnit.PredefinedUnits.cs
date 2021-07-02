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

    partial struct SpecificEnergyUnit
    {
        [Predefined]
        public static readonly SpecificEnergyUnit JoulePerKilogram = new SpecificEnergyUnit(name: "joule per kilogram", abbreviation: "J/kg", valueInJoulesPerKilogram: Constants.One);
        [Predefined]
        public static readonly SpecificEnergyUnit KilojoulePerKilogram = new SpecificEnergyUnit(name: "kilojoule per kilogram", abbreviation: "kJ/kg", valueInJoulesPerKilogram: Constants.Thousand);
        [Predefined]
        public static readonly SpecificEnergyUnit MegajoulePerKilogram = new SpecificEnergyUnit(name: "megajoule per kilogram", abbreviation: "MJ/kg", valueInJoulesPerKilogram: Constants.Million);
        [Predefined]
        public static readonly SpecificEnergyUnit JoulePerGram = new SpecificEnergyUnit(name: "joule per gram", abbreviation: "J/g", valueInJoulesPerKilogram: Constants.Thousand);
        [Predefined]
        public static readonly SpecificEnergyUnit KilojoulePerGram = new SpecificEnergyUnit(name: "kilojoule per gram", abbreviation: "kJ/g", valueInJoulesPerKilogram: Constants.Million);

        [Predefined]
        public static readonly SpecificEnergyUnit CaloriePerKilogram = new SpecificEnergyUnit(name: "calorie per kilogram", abbreviation: "cal/kg", valueInJoulesPerKilogram: Constants.One * Constants.JoulesPerCalorie);
        [Predefined]
        public static readonly SpecificEnergyUnit KilocaloriePerKilogram = new SpecificEnergyUnit(name: "kilocalorie per kilogram", abbreviation: "kcal/kg", valueInJoulesPerKilogram: Constants.Thousand * Constants.JoulesPerCalorie);
        [Predefined]
        public static readonly SpecificEnergyUnit CaloriePerGram = new SpecificEnergyUnit(name: "calorie per gram", abbreviation: "cal/g", valueInJoulesPerKilogram: Constants.Thousand * Constants.JoulesPerCalorie);
        [Predefined]
        public static readonly SpecificEnergyUnit KilocaloriePerGram = new SpecificEnergyUnit(name: "kilocalorie per gram", abbreviation: "kcal/g", valueInJoulesPerKilogram: Constants.Million * Constants.JoulesPerCalorie);

        [Predefined]
        public static readonly SpecificEnergyUnit JoulePerHundredGrams = new SpecificEnergyUnit(name: "joule per 100 grams", abbreviation: "J/100 g", valueInJoulesPerKilogram: Constants.Ten);
        [Predefined]
        public static readonly SpecificEnergyUnit KilojoulePerHundredGrams = new SpecificEnergyUnit(name: "kilojoule per 100 grams", abbreviation: "kJ/100 g", valueInJoulesPerKilogram: Constants.TenThousand);
        [Predefined]
        public static readonly SpecificEnergyUnit CaloriePerHundredGrams = new SpecificEnergyUnit(name: "calorie per 100 grams", abbreviation: "cal/100 g", valueInJoulesPerKilogram: Constants.Ten * Constants.JoulesPerCalorie);
        [Predefined]
        public static readonly SpecificEnergyUnit KilocaloriePerHundredGrams = new SpecificEnergyUnit(name: "kilocalorie per 100 grams", abbreviation: "kcal/100 g", valueInJoulesPerKilogram: Constants.TenThousand * Constants.JoulesPerCalorie);

        public static IEnumerable<SpecificEnergyUnit> GetPredefinedUnits() =>
            typeof(SpecificEnergyUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(SpecificEnergyUnit))
            .Select(e => (SpecificEnergyUnit)e.GetValue(null));

    }
}
