using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct WeightUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInKilograms;

        public WeightUnit(string name, string abbreviation, double valueInKilograms)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInKilograms, 0d, nameof(valueInKilograms));

            _name = name;
            _abbreviation = abbreviation;
            _valueInKilograms = valueInKilograms;
        }

        public string Name => _name ?? Kilogram._name;
        public string Abbreviation => _abbreviation ?? Kilogram._abbreviation;
        public double ValueInKilograms => _valueInKilograms ?? Kilogram._valueInKilograms.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInKilograms;
    }
}