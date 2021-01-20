using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial struct WeightUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInKilograms;

        public WeightUnit(string name, string abbreviation, number valueInKilograms)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInKilograms, Constants.Zero, nameof(valueInKilograms));

            _name = name;
            _abbreviation = abbreviation;
            _valueInKilograms = valueInKilograms;
        }

        public string Name => _name ?? Kilogram._name;
        public string Abbreviation => _abbreviation ?? Kilogram._abbreviation;
        public number ValueInKilograms => _valueInKilograms ?? Kilogram._valueInKilograms.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInKilograms;
    }
}