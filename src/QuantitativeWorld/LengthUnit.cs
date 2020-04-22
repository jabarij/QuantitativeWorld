using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct LengthUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInMetres;

        public LengthUnit(string name, string abbreviation, double valueInMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetres, 0d, nameof(valueInMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetres = valueInMetres;
        }

        public string Name => _name ?? Metre._name;
        public string Abbreviation => _abbreviation ?? Metre._abbreviation;
        public double ValueInMetres => _valueInMetres ?? Metre._valueInMetres.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}