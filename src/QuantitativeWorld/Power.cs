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

        private readonly PowerUnit? _formatUnit;

        public Power(double watts)
            : this(formatUnit: null, watts: watts) { }
        public Power(double value, PowerUnit unit)
            : this(formatUnit: unit, watts: GetWatts(value, unit)) { }
        private Power(PowerUnit? formatUnit, double watts)
        {
            Assert.IsInRange(watts, MinWatts, MaxWatts, nameof(watts));

            _formatUnit = formatUnit;
            Watts = watts;
        }

        public double Watts { get; }
        public double Value => GetValue(Watts, Unit);
        public PowerUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<PowerUnit>.BaseValue => Watts;
        PowerUnit ILinearQuantity<PowerUnit>.BaseUnit => DefaultUnit;

        public Power Convert(PowerUnit targetUnit) =>
            new Power(targetUnit, Watts);

        public bool IsZero() =>
            Watts == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Power, PowerUnit>(formatProvider, this);

        private static double GetWatts(double value, PowerUnit sourceUnit) =>
            value * sourceUnit.ValueInWatts;
        private static double GetValue(double watts, PowerUnit targetUnit) =>
            watts / targetUnit.ValueInWatts;
    }
}
