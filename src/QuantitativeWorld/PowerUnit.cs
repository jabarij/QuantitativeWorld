﻿using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public partial struct PowerUnit : ILinearUnit, INamedUnit
    {
        private readonly string _name;
        private readonly string _abbreviation;
        private readonly double? _valueInWatts;

        public PowerUnit(string name, string abbreviation, double valueInWatts)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInWatts, 0d, nameof(valueInWatts));

            _name = name;
            _abbreviation = abbreviation;
            _valueInWatts = valueInWatts;
        }

        public string Name => _name ?? Watt._name;
        public string Abbreviation => _abbreviation ?? Watt._abbreviation;
        public double ValueInWatts => _valueInWatts ?? Watt._valueInWatts.Value;

        public override string ToString() => Abbreviation;

        double ILinearUnit.ValueInBaseUnit => ValueInWatts;
    }
}