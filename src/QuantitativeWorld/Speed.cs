using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Speed : ILinearQuantity<SpeedUnit>
    {
        private const double MinMetresPerSecond = double.MinValue;
        private const double MaxMetresPerSecond = double.MaxValue;

        public static readonly SpeedUnit DefaultUnit = SpeedUnit.MetrePerSecond;
        public static readonly Speed Zero = new Speed(0d);
        public static readonly Speed PositiveInfinity = new Speed(double.PositiveInfinity, null, null, false);
        public static readonly Speed NegativeInfinity = new Speed(double.NegativeInfinity, null, null, false);

        private readonly SpeedUnit? _unit;
        private double? _value;

        public Speed(double metresPerSecond)
            : this(
                metresPerSecond: metresPerSecond,
                value: null,
                unit: null)
        { }
        public Speed(double value, SpeedUnit unit)
            : this(
                metresPerSecond: GetMetresPerSecond(value, unit),
                value: value,
                unit: unit)
        { }
        private Speed(double metresPerSecond, double? value, SpeedUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(metresPerSecond, MinMetresPerSecond, MaxMetresPerSecond, nameof(value));

            MetresPerSecond = metresPerSecond;
            _value = value;
            _unit = unit;
        }

        public double MetresPerSecond { get; }
        public double Value => EnsureValue();
        public SpeedUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<SpeedUnit>.BaseValue => MetresPerSecond;
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
            MetresPerSecond == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(formatProvider, this);

        private static double GetMetresPerSecond(double value, SpeedUnit sourceUnit) =>
            value * sourceUnit.ValueInMetresPerSecond;
        private static double GetValue(double metres, SpeedUnit targetUnit) =>
            metres / targetUnit.ValueInMetresPerSecond;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(MetresPerSecond, Unit);
            return _value.Value;
        }
    }
}
