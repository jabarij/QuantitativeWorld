using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Power : ILinearQuantity<PowerUnit>
    {
        private const double MinWatts = double.MinValue;
        private const double MaxWatts = double.MaxValue;

        public static readonly PowerUnit DefaultUnit = PowerUnit.Watt;
        public static readonly Power Zero = new Power(0d);
        public static readonly Power PositiveInfinity = new Power(double.PositiveInfinity, null, null, false);
        public static readonly Power NegativeInfinity = new Power(double.NegativeInfinity, null, null, false);

        private readonly PowerUnit? _unit;
        private double? _value;

        public Power(double watts)
            : this(
                watts: watts,
                value: null,
                unit: null)
        { }
        public Power(double value, PowerUnit unit)
            : this(
                watts: GetWatts(value, unit),
                value: value,
                unit: unit)
        { }
        private Power(double watts, double? value, PowerUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(watts, MinWatts, MaxWatts, nameof(value));

            Watts = watts;
            _value = value;
            _unit = unit;
        }

        public double Watts { get; }
        public double Value => EnsureValue();
        public PowerUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<PowerUnit>.BaseValue => Watts;
        PowerUnit ILinearQuantity<PowerUnit>.BaseUnit => DefaultUnit;

        public Power Convert(PowerUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Power(
                watts: Watts,
                value: _value,
                unit: targetUnit)
            : new Power(
                watts: Watts,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            Watts == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(formatProvider, this);

        private static double GetWatts(double value, PowerUnit sourceUnit) =>
            value * sourceUnit.ValueInWatts;
        private static double GetValue(double metres, PowerUnit targetUnit) =>
            metres / targetUnit.ValueInWatts;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Watts, Unit);
            return _value.Value;
        }
    }
}
