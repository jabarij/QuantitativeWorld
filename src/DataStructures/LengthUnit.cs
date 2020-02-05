using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct LengthUnit : ILinearUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly decimal? _valueInMetres;

        public LengthUnit(string name, string abbreviation, decimal valueInMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetres, 0m, nameof(valueInMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetres = valueInMetres;
        }

        public string Name => _name ?? Metre._name;
        public string Abbreviation => _abbreviation ?? Metre._abbreviation;
        public decimal ValueInMetres => _valueInMetres ?? Metre._valueInMetres.Value;

        public override string ToString() => Abbreviation;

        decimal ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}