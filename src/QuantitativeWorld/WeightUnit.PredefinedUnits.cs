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

    partial struct WeightUnit
    {
        [Predefined]
        public static readonly WeightUnit Kilogram = new WeightUnit(name: "kilogram", abbreviation: "kg", valueInKilograms: Constants.One);
        [Predefined]
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: Constants.KilogramsPerGram);
        [Predefined]
        public static readonly WeightUnit Milligram = new WeightUnit(name: "milligram", abbreviation: "mg", valueInKilograms: Gram.ValueInKilograms / Constants.MilligramsPerGram);
        [Predefined]
        public static readonly WeightUnit Decagram = new WeightUnit(name: "decagram", abbreviation: "dag", valueInKilograms: Gram.ValueInKilograms * Constants.GramsPerDecagram);
        [Predefined]
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsPerTon);
        [Predefined]
        public static readonly WeightUnit Quintal = new WeightUnit(name: "quintal", abbreviation: "q", valueInKilograms: Constants.KilogramsPerQuintal);

        [Predefined]
        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lbs", valueInKilograms: Constants.KilogramsPerPound);
        [Predefined]
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.OuncesPerPound);
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
