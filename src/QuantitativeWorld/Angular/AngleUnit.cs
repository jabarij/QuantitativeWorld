using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Angular
{
    public partial struct AngleUnit : ILinearUnit, INamedUnit, ISymbolicUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly string _symbol;
        private readonly double? _unitsPerTurn;

        public AngleUnit(string name, string abbreviation, string symbol, double unitsPerTurn)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            Assert.IsGreaterThan(unitsPerTurn, 0d, nameof(unitsPerTurn));

            _name = name;
            _abbreviation = abbreviation;
            _symbol = symbol;
            _unitsPerTurn = unitsPerTurn;
        }

        public string Name => _name ?? Turn._name;
        public string Abbreviation => _abbreviation ?? Turn._abbreviation;
        public string Symbol => _symbol ?? Turn._symbol;
        public double UnitsPerTurn => _unitsPerTurn ?? Turn._unitsPerTurn.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => 1d / UnitsPerTurn;
    }
}