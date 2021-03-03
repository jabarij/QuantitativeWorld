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

    public partial struct VolumeUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInCubicMetres;

        public VolumeUnit(string name, string abbreviation, number valueInCubicMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInCubicMetres, Constants.Zero, nameof(valueInCubicMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInCubicMetres = valueInCubicMetres;
        }

        public string Name => _name ?? CubicMetre._name;
        public string Abbreviation => _abbreviation ?? CubicMetre._abbreviation;
        public number ValueInCubicMetres => _valueInCubicMetres ?? CubicMetre._valueInCubicMetres.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInCubicMetres;
    }
}