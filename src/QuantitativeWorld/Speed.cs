using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Speed : ILinearQuantity<SpeedUnit>
    {
        public static readonly SpeedUnit DefaultUnit = SpeedUnit.MetrePerSecond;

        private readonly double? _metresPerSecond;
        private readonly double? _value;
        private readonly SpeedUnit? _unit;

        public Speed(double metresPerSecond)
        {
            _metresPerSecond = metresPerSecond;
            _value = metresPerSecond;
            _unit = DefaultUnit;
        }
        public Speed(double value, SpeedUnit unit)
        {
            _metresPerSecond = null;
            _value = value;
            _unit = unit;
        }
        private Speed(SpeedUnit unit, double metresPerSecond)
        {
            _metresPerSecond = metresPerSecond;
            _value = null;
            _unit = unit;
        }

        public double MetresPerSecond => _metresPerSecond ?? (_value.HasValue ? GetMetresPerSecond(_value.Value, Unit) : 0d);
        public double Value => _value ?? (_metresPerSecond.HasValue ? GetValue(_metresPerSecond.Value, Unit) : 0d);
        public SpeedUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<SpeedUnit>.BaseValue => MetresPerSecond;
        SpeedUnit ILinearQuantity<SpeedUnit>.BaseUnit => DefaultUnit;

        public Speed Convert(SpeedUnit targetUnit) =>
            new Speed(targetUnit, MetresPerSecond);

        public bool IsZero() =>
            MetresPerSecond == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Speed, SpeedUnit>(formatProvider, this);

        private static double GetMetresPerSecond(double value, SpeedUnit sourceUnit)
        {
            double result =
                sourceUnit.ValueInMetresPerSecond < 1d
                ? value / (1d / sourceUnit.ValueInMetresPerSecond)
                : value * sourceUnit.ValueInMetresPerSecond;
            if (double.IsInfinity(result))
                throw Error.ArgumentOutOfRange(nameof(value), value, $"{value} multiplied by {sourceUnit.ValueInMetresPerSecond} gave {result}.");
            return result;
        }
        private static double GetValue(double metresPerSecond, SpeedUnit targetUnit) =>
            metresPerSecond / targetUnit.ValueInMetresPerSecond;
    }
}
