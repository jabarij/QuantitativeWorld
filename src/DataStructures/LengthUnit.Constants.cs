namespace DataStructures
{
    partial struct LengthUnit
    {
        public static readonly LengthUnit Metre = new LengthUnit(name: "metre", abbreviation: "m", valueInMetres: 1m);
        public static readonly LengthUnit Millimetre = MetricPrefix.Milli * Metre;
        public static readonly LengthUnit Centimetre = MetricPrefix.Centi * Metre;
        public static readonly LengthUnit Kilometre = MetricPrefix.Kilo * Metre;

        public static readonly LengthUnit Inch = new LengthUnit(name: "inch", abbreviation: "in", valueInMetres: Constants.MetersInInch);
        public static readonly LengthUnit Foot = new LengthUnit(name: "foot", abbreviation: "ft", valueInMetres: Inch.ValueInMetres * Constants.InchesInFoot);
        public static readonly LengthUnit Yard = new LengthUnit(name: "yard", abbreviation: "yd", valueInMetres: Foot.ValueInMetres * Constants.FeetInYard);
        public static readonly LengthUnit Chain = new LengthUnit(name: "chain", abbreviation: "ch", valueInMetres: Yard.ValueInMetres * Constants.YardsInChain);
        public static readonly LengthUnit Mile = new LengthUnit(name: "mile", abbreviation: "mi", valueInMetres: Chain.ValueInMetres * Constants.ChainsInMile);
    }
}
