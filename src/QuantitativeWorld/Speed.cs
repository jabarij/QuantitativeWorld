using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial struct Speed : ILinearQuantity<SpeedUnit>
    {
        private const number MinMetresPerSecond = number.MinValue;
        private const number MaxMetresPerSecond = number.MaxValue;

        public static readonly SpeedUnit DefaultUnit = SpeedUnit.MetrePerSecond;
        public static readonly Speed Zero = new Speed(Constants.Zero);

        private readonly SpeedUnit? _unit;
        private number? _value;

        public Speed(number metresPerSecond)
            : this(
                metresPerSecond: metresPerSecond,
                value: null,
                unit: null)
        { }
        public Speed(number value, SpeedUnit unit)
            : this(
                metresPerSecond: GetMetresPerSecond(value, unit),
                value: value,
                unit: unit)
        { }
        private Speed(number metresPerSecond, number? value, SpeedUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(metresPerSecond, MinMetresPerSecond, MaxMetresPerSecond, nameof(value));

            MetresPerSecond = metresPerSecond;
            _value = value;
            _unit = unit;
        }

        public number MetresPerSecond { get; }
        public number Value => EnsureValue();
        public SpeedUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<SpeedUnit>.BaseValue => MetresPerSecond;
        SpeedUnit ILinearQuantity<SpeedUnit>.BaseUnit => DefaultUnit;

        public Speed Convert(SpeedUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Speed(
                metresPerSecond: MetresPerSecond,
                value: _value,
                unit: targetUnit)
            : new Speed(
                metresPerSecond: MetresPerSecond,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            MetresPerSecond == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(formatProvider, this);

        private static number GetMetresPerSecond(number value, SpeedUnit sourceUnit) =>
            value * sourceUnit.ValueInMetresPerSecond;
        private static number GetValue(number metres, SpeedUnit targetUnit) =>
            metres / targetUnit.ValueInMetresPerSecond;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(MetresPerSecond, Unit);
            return _value.Value;
        }
    }
}
