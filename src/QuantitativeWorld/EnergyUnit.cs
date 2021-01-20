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

    public partial struct EnergyUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInJoules;

        public EnergyUnit(string name, string abbreviation, number valueInJoules)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInJoules, Constants.Zero, nameof(valueInJoules));

            _name = name;
            _abbreviation = abbreviation;
            _valueInJoules = valueInJoules;
        }

        public string Name => _name ?? Joule._name;
        public string Abbreviation => _abbreviation ?? Joule._abbreviation;
        public number ValueInJoules => _valueInJoules ?? Joule._valueInJoules.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInJoules;
    }
}