using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct WeightUnit
    {
        [Parsable]
        public static readonly WeightUnit Kilogram = new WeightUnit(name: "kilogram", abbreviation: "kg", valueInKilograms: 1m);

        [Parsable]
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: Constants.KilogramsPerGram);

        [Parsable]
        public static readonly WeightUnit Milligram = MetricPrefix.Milli * Gram;

        [Parsable]
        public static readonly WeightUnit Decagram = MetricPrefix.Deca * Gram;

        [Parsable]
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsPerTon);

        [Parsable]
        public static readonly WeightUnit Quintal = new WeightUnit(name: "quintal", abbreviation: "q", valueInKilograms: Constants.KilogramsPerQuintal);

        [Parsable]
        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lbs", valueInKilograms: Constants.KilogramsPerPound);

        [Parsable]
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.OuncesPerPound);

        [Parsable]
        public static readonly WeightUnit Stone = new WeightUnit(name: "stone", abbreviation: "st", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerStone);

        [Parsable]
        public static readonly WeightUnit Hundredweight = new WeightUnit(name: "hundredweight", abbreviation: "cwt", valueInKilograms: Stone.ValueInKilograms * Constants.StonesPerHundredweight);

        [Parsable]
        public static readonly WeightUnit Cental = new WeightUnit(name: "cental", abbreviation: "ctl", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerCental);

        public static IEnumerable<WeightUnit> GetKnownUnits() =>
            GetParsableUnits();

        internal static IEnumerable<WeightUnit> GetParsableUnits() =>
            typeof(WeightUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(ParsableAttribute), ParsableAttribute.Inherited))
            .Select(e => (WeightUnit)e.GetValue(null));

    }
}
