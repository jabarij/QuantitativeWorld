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

    partial struct WeightUnit
    {
        [Predefined]
        public static readonly WeightUnit Kilogram = new WeightUnit(name: "kilogram", abbreviation: "kg", valueInKilograms: Constants.One);
        [Predefined]
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: Constants.KilogramsPerGram);
        [Predefined]
        public static readonly WeightUnit Milligram = new WeightUnit(name: "milligram", abbreviation: "mg", valueInKilograms: Gram.ValueInKilograms / Constants.MilligramsPerGram);
        [Predefined]
        public static readonly WeightUnit Microgram = new WeightUnit(name: "microgram", abbreviation: "μg", valueInKilograms: Gram.ValueInKilograms / Constants.MicrogramsPerGram);
        [Predefined]
        public static readonly WeightUnit Decagram = new WeightUnit(name: "decagram", abbreviation: "dag", valueInKilograms: Gram.ValueInKilograms * Constants.GramsPerDecagram);
        [Predefined]
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsPerTon);
        [Predefined]
        public static readonly WeightUnit Quintal = new WeightUnit(name: "quintal", abbreviation: "q", valueInKilograms: Constants.KilogramsPerQuintal);

        [Predefined]
        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lb", valueInKilograms: Constants.KilogramsPerPound);
        [Predefined]
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.OuncesPerPound);
        [Predefined]
        public static readonly WeightUnit Grain = new WeightUnit(name: "grain", abbreviation: "gr", valueInKilograms: Pound.ValueInKilograms / Constants.GrainsPerPound);
        [Predefined]
        public static readonly WeightUnit TroyOunce = new WeightUnit(name: "troy ounce", abbreviation: "oz t", valueInKilograms: Grain.ValueInKilograms * Constants.GrainsPerTroyOunce);
        [Predefined]
        public static readonly WeightUnit TroyPound = new WeightUnit(name: "troy pound", abbreviation: "lb t", valueInKilograms: Grain.ValueInKilograms * Constants.GrainsPerTroyPound);
        [Predefined]
        public static readonly WeightUnit Stone = new WeightUnit(name: "stone", abbreviation: "st", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerStone);
        [Predefined]
        public static readonly WeightUnit Hundredweight = new WeightUnit(name: "hundredweight", abbreviation: "cwt", valueInKilograms: Stone.ValueInKilograms * Constants.StonesPerHundredweight);
        [Predefined]
        public static readonly WeightUnit Cental = new WeightUnit(name: "cental", abbreviation: "ctl", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerCental);

        public static IEnumerable<WeightUnit> GetPredefinedUnits() =>
            typeof(WeightUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(WeightUnit))
            .Select(e => (WeightUnit)e.GetValue(null));

    }
}
