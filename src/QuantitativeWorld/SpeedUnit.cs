using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct SpeedUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInMetresPerSecond;

        public SpeedUnit(string name, string abbreviation, double valueInMetresPerSecond)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetresPerSecond, 0d, nameof(valueInMetresPerSecond));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetresPerSecond = valueInMetresPerSecond;
        }

        public string Name => _name ?? MetrePerSecond._name;
        public string Abbreviation => _abbreviation ?? MetrePerSecond._abbreviation;
        public double ValueInMetresPerSecond => _valueInMetresPerSecond ?? MetrePerSecond._valueInMetresPerSecond.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInMetresPerSecond;
    }
}