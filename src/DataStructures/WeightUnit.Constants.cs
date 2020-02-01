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
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: 1m / Constants.GramsInKilogram);

        [Parsable]
        public static readonly WeightUnit Milligram = MetricPrefix.Milli * Gram;

        [Parsable]
        public static readonly WeightUnit Decagram = MetricPrefix.Deca * Gram;

        [Parsable]
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsInTon);

        [Parsable]
        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lbs", valueInKilograms: Constants.KilogramsInAvoirdupoisPounds);

        [Parsable]
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.AvoirdupoisOuncesInPound);

        public static IEnumerable<WeightUnit> GetKnownUnits() =>
            GetParsableUnits();

        internal static IEnumerable<WeightUnit> GetParsableUnits() =>
            typeof(WeightUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(ParsableAttribute), ParsableAttribute.Inherited))
            .Select(e => (WeightUnit)e.GetValue(null));

    }
}
