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