using Common.Internals.DotNetExtensions;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalQuantitativeWorld.DecimalConstants;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Angular
{
    using QuantitativeWorld.Interfaces;
    using Constants = QuantitativeWorld.DoubleConstants;
    using number = System.Double;
#endif

    public partial struct AngleUnit : ILinearUnit, INamedUnit, ISymbolicUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly string _symbol;
        private readonly number? _unitsPerTurn;

        public AngleUnit(string name, string abbreviation, string symbol, number unitsPerTurn)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            Assert.IsGreaterThan(unitsPerTurn, Constants.Zero, nameof(unitsPerTurn));

            _name = name;
            _abbreviation = abbreviation;
            _symbol = symbol;
            _unitsPerTurn = unitsPerTurn;
        }

        public string Name => _name ?? Turn._name;
        public string Abbreviation => _abbreviation ?? Turn._abbreviation;
        public string Symbol => _symbol ?? Turn._symbol;
        public number UnitsPerTurn => _unitsPerTurn ?? Turn._unitsPerTurn.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => Constants.One / UnitsPerTurn;
    }
}