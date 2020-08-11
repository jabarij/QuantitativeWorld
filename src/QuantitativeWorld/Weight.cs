using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Weight : ILinearQuantity<WeightUnit>
    {
        private const double MinKilograms = double.MinValue;
        private const double MaxKilograms = double.MaxValue;

        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;
        public static readonly Weight Zero = new Weight(0d);
        public static readonly Weight PositiveInfinity = new Weight(double.PositiveInfinity, null, null, false);
        public static readonly Weight NegativeInfinity = new Weight(double.NegativeInfinity, null, null, false);

        private readonly WeightUnit? _unit;
        private double? _value;

        public Weight(double kilograms)
            : this(
                kilograms: kilograms,
                value: null,
                unit: null)
        { }
        public Weight(double value, WeightUnit unit)
            : this(
                kilograms: GetKilograms(value, unit),
                value: value,
                unit: unit)
        { }
        private Weight(double kilograms, double? value, WeightUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(kilograms, MinKilograms, MaxKilograms, nameof(value));

            Kilograms = kilograms;
            _value = value;
            _unit = unit;
        }

        public double Kilograms { get; }
        public double Value => EnsureValue();
        public WeightUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<WeightUnit>.BaseValue => Kilograms;
        WeightUnit ILinearQuantity<WeightUnit>.BaseUnit => DefaultUnit;

        public Weight Convert(WeightUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Weight(
                kilograms: Kilograms,
                value: _value,
                unit: targetUnit)
            : new Weight(
                kilograms: Kilograms,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            Kilograms == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(formatProvider, this);

        private static double GetKilograms(double value, WeightUnit sourceUnit) =>
            value * sourceUnit.ValueInKilograms;
        private static double GetValue(double metres, WeightUnit targetUnit) =>
            metres / targetUnit.ValueInKilograms;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Kilograms, Unit);
            return _value.Value;
        }
    }
}
