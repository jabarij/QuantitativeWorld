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

    public partial struct SpeedUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInMetresPerSecond;

        public SpeedUnit(string name, string abbreviation, number valueInMetresPerSecond)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetresPerSecond, Constants.Zero, nameof(valueInMetresPerSecond));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetresPerSecond = valueInMetresPerSecond;
        }

        public string Name => _name ?? MetrePerSecond._name;
        public string Abbreviation => _abbreviation ?? MetrePerSecond._abbreviation;
        public number ValueInMetresPerSecond => _valueInMetresPerSecond ?? MetrePerSecond._valueInMetresPerSecond.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInMetresPerSecond;
    }
}