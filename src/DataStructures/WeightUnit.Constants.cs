using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
    partial struct WeightUnit
    {
        [Parsable]
        public static readonly WeightUnit Kilogram = new WeightUnit(name: "kilogram", abbreviation: "kg", valueInKilograms: 1m, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: Constants.KilogramsPerGram, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Milligram = new WeightUnit(name: "milligram", abbreviation: "mg", valueInKilograms: Gram.ValueInKilograms / Constants.MilligramsPerGram, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Decagram = new WeightUnit(name: "decagram", abbreviation: "dag", valueInKilograms: Gram.ValueInKilograms * Constants.GramsPerDecagram, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsPerTon, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Quintal = new WeightUnit(name: "quintal", abbreviation: "q", valueInKilograms: Constants.KilogramsPerQuintal, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lbs", valueInKilograms: Constants.KilogramsPerPound, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.OuncesPerPound, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Stone = new WeightUnit(name: "stone", abbreviation: "st", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerStone, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Hundredweight = new WeightUnit(name: "hundredweight", abbreviation: "cwt", valueInKilograms: Stone.ValueInKilograms * Constants.StonesPerHundredweight, isDefined: true);

        [Parsable]
        public static readonly WeightUnit Cental = new WeightUnit(name: "cental", abbreviation: "ctl", valueInKilograms: Pound.ValueInKilograms * Constants.PoundsPerCental, isDefined: true);

        public static IEnumerable<WeightUnit> GetKnownUnits() =>
            GetParsableUnits();

        internal static IEnumerable<WeightUnit> GetParsableUnits() =>
            typeof(WeightUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(ParsableAttribute), ParsableAttribute.Inherited))
            .Select(e => (WeightUnit)e.GetValue(null));

    }
}
