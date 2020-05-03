using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct AreaUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInSquareMetres;

        public AreaUnit(string name, string abbreviation, double valueInSquareMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInSquareMetres, 0d, nameof(valueInSquareMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInSquareMetres = valueInSquareMetres;
        }

        public string Name => _name ?? SquareMetre._name;
        public string Abbreviation => _abbreviation ?? SquareMetre._abbreviation;
        public double ValueInSquareMetres => _valueInSquareMetres ?? SquareMetre._valueInSquareMetres.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInSquareMetres;
    }
}