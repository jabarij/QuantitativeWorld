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

    public partial struct SpecificEnergyUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInJoulesPerKilogram;

        public SpecificEnergyUnit(string name, string abbreviation, number valueInJoulesPerKilogram)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInJoulesPerKilogram, Constants.Zero, nameof(valueInJoulesPerKilogram));

            _name = name;
            _abbreviation = abbreviation;
            _valueInJoulesPerKilogram = valueInJoulesPerKilogram;
        }

        public string Name => _name ?? JoulePerKilogram._name;
        public string Abbreviation => _abbreviation ?? JoulePerKilogram._abbreviation;
        public number ValueInJoulesPerKilogram => _valueInJoulesPerKilogram ?? JoulePerKilogram._valueInJoulesPerKilogram.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInJoulesPerKilogram;
    }
}