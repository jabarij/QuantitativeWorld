using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct VolumeUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInCubicMetres;

        public VolumeUnit(string name, string abbreviation, double valueInCubicMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInCubicMetres, 0d, nameof(valueInCubicMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInCubicMetres = valueInCubicMetres;
        }

        public string Name => _name ?? CubicMetre._name;
        public string Abbreviation => _abbreviation ?? CubicMetre._abbreviation;
        public double ValueInCubicMetres => _valueInCubicMetres ?? CubicMetre._valueInCubicMetres.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInCubicMetres;
    }
}