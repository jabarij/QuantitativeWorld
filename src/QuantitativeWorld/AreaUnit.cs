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

    public partial struct AreaUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInSquareMetres;

        public AreaUnit(string name, string abbreviation, number valueInSquareMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInSquareMetres, Constants.Zero, nameof(valueInSquareMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInSquareMetres = valueInSquareMetres;
        }

        public string Name => _name ?? SquareMetre._name;
        public string Abbreviation => _abbreviation ?? SquareMetre._abbreviation;
        public number ValueInSquareMetres => _valueInSquareMetres ?? SquareMetre._valueInSquareMetres.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInSquareMetres;
    }
}