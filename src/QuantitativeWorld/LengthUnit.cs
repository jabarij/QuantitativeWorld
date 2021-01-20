﻿using QuantitativeWorld.DotNetExtensions;
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

    public partial struct LengthUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly number? _valueInMetres;

        public LengthUnit(string name, string abbreviation, number valueInMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetres, Constants.Zero, nameof(valueInMetres));

            _name = name;
            _abbreviation = abbreviation;
            _valueInMetres = valueInMetres;
        }

        public string Name => _name ?? Metre._name;
        public string Abbreviation => _abbreviation ?? Metre._abbreviation;
        public number ValueInMetres => _valueInMetres ?? Metre._valueInMetres.Value;

        public override string ToString() => Abbreviation;

        number ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}