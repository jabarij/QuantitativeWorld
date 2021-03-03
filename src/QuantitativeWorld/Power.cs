using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Interfaces;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    public partial struct Power : ILinearQuantity<PowerUnit>
    {
        private const number MinWatts = number.MinValue;
        private const number MaxWatts = number.MaxValue;

        public static readonly PowerUnit DefaultUnit = PowerUnit.Watt;
        public static readonly Power Zero = new Power(Constants.Zero);
#if !DECIMAL
        public static readonly Power PositiveInfinity = new Power(number.PositiveInfinity, null, null, false);
        public static readonly Power NegativeInfinity = new Power(number.NegativeInfinity, null, null, false);
#endif

        private readonly PowerUnit? _unit;
        private number? _value;

        public Power(number watts)
            : this(
                watts: watts,
                value: null,
                unit: null)
        { }
        public Power(number value, PowerUnit unit)
            : this(
                watts: GetWatts(value, unit),
                value: value,
                unit: unit)
        { }
        private Power(number watts, number? value, PowerUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(watts, MinWatts, MaxWatts, nameof(value));

            Watts = watts;
            _value = value;
            _unit = unit;
        }

        public number Watts { get; }
        public number Value => EnsureValue();
        public PowerUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<PowerUnit>.BaseValue => Watts;
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
            Watts == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(formatProvider, this);

        private static number GetWatts(number value, PowerUnit sourceUnit) =>
            value * sourceUnit.ValueInWatts;
        private static number GetValue(number metres, PowerUnit targetUnit) =>
            metres / targetUnit.ValueInWatts;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Watts, Unit);
            return _value.Value;
        }
    }
}
