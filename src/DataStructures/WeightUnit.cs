using QuantitativeWorld.DotNetExtensions;

namespace QuantitativeWorld
{
    public partial struct WeightUnit : ILinearUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly decimal? _valueInKilograms;

        public WeightUnit(string name, string abbreviation, decimal valueInKilograms)
            : this(name, abbreviation, valueInKilograms, false) { }
        private WeightUnit(string name, string abbreviation, decimal valueInKilograms, bool isDefined)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInKilograms, 0m, nameof(valueInKilograms));

            _name = name;
            _abbreviation = abbreviation;
            _valueInKilograms = valueInKilograms;
            IsPreDefined = isDefined;
        }

        public string Name => _name ?? Kilogram._name;
        public string Abbreviation => _abbreviation ?? Kilogram._abbreviation;
        public decimal ValueInKilograms => _valueInKilograms ?? Kilogram._valueInKilograms.Value;
        internal bool IsPreDefined { get; }

        decimal ILinearUnit.ValueInBaseUnit => ValueInKilograms;
    }
}