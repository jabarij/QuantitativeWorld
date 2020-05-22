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

        private readonly SpeedUnit? _formatUnit;

        public Speed(double metresPerSecond)
            : this(formatUnit: null, metresPerSecond: metresPerSecond) { }
        public Speed(double value, SpeedUnit unit)
            : this(formatUnit: unit, metresPerSecond: GetMetresPerSecond(value, unit)) { }
        private Speed(SpeedUnit? formatUnit, double metresPerSecond)
        {
            Assert.IsInRange(metresPerSecond, MinMetresPerSecond, MaxMetresPerSecond, nameof(metresPerSecond));

            _formatUnit = formatUnit;
            MetresPerSecond = metresPerSecond;
        }

        public double MetresPerSecond { get; }
        public double Value => GetValue(MetresPerSecond, Unit);
        public SpeedUnit Unit => _formatUnit ?? DefaultUnit;
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
