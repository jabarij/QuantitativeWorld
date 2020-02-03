using QuantitativeWorld.DotNetExtensions;

namespace QuantitativeWorld
{
    public partial struct LengthUnit : ILinearUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly decimal? _valueInMetres;

        public LengthUnit(string name, string abbreviation, decimal valueInMetres)
            : this(name, abbreviation, valueInMetres, false) { }
        private LengthUnit(string name, string abbreviation, decimal valueInMetres, bool isPreDefined)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetres, 0m, nameof(valueInMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetres = valueInMetres;
            IsPreDefined = isPreDefined;
        }

        public string Name => _name ?? Metre._name;
        public string Abbreviation => _abbreviation ?? Metre._abbreviation;
        public decimal ValueInMetres => _valueInMetres ?? Metre._valueInMetres.Value;
        internal bool IsPreDefined { get; }

        decimal ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}