using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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