using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct EnergyUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInJoules;

        public EnergyUnit(string name, string abbreviation, double valueInJoules)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInJoules, 0d, nameof(valueInJoules));

            _name = name;
            _abbreviation = abbreviation;
            _valueInJoules = valueInJoules;
        }

        public string Name => _name ?? Joule._name;
        public string Abbreviation => _abbreviation ?? Joule._abbreviation;
        public double ValueInJoules => _valueInJoules ?? Joule._valueInJoules.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInJoules;
    }
}