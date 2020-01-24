namespace DataStructures
{
    partial struct WeightUnit
    {
        public static readonly WeightUnit Kilogram = new WeightUnit(name: "kilogram", abbreviation: "kg", valueInKilograms: 1m);
        public static readonly WeightUnit Gram = new WeightUnit(name: "gram", abbreviation: "g", valueInKilograms: 1m / Constants.GramsInKilogram);
        public static readonly WeightUnit Milligram = MetricPrefix.Milli * Gram;
        public static readonly WeightUnit Decagram = MetricPrefix.Deca * Gram;
        public static readonly WeightUnit Ton = new WeightUnit(name: "ton", abbreviation: "t", valueInKilograms: Constants.KilogramsInTon);

        public static readonly WeightUnit Pound = new WeightUnit(name: "pound", abbreviation: "lbs", valueInKilograms: Constants.KilogramsInAvoirdupoisPounds);
        public static readonly WeightUnit Ounce = new WeightUnit(name: "ounce", abbreviation: "oz", valueInKilograms: Pound.ValueInKilograms / Constants.AvoirdupoisOuncesInPound);
    }
}
