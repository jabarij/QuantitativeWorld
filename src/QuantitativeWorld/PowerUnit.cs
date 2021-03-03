using Common.Internals.DotNetExtensions;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalConstants;
    using number = System.Decimal;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Interfaces;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    public partial struct PowerUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInWatts;

        public PowerUnit(string name, string abbreviation, number valueInWatts)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInWatts, Constants.Zero, nameof(valueInWatts));

            _name = name;
            _abbreviation = abbreviation;
            _valueInWatts = valueInWatts;
        }

        public string Name => _name ?? Watt._name;
        public string Abbreviation => _abbreviation ?? Watt._abbreviation;
        public number ValueInWatts => _valueInWatts ?? Watt._valueInWatts.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInWatts;
    }
}