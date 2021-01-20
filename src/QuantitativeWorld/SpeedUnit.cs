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