using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Power : ILinearQuantity<PowerUnit>
    {
        public static readonly Weight MinValue = new Weight(MinWatts);
        public static readonly Weight MaxValue = new Weight(MaxWatts);
        private const decimal MinWatts = decimal.MinValue;
        private const decimal MaxWatts = decimal.MaxValue;

        public static readonly PowerUnit DefaultUnit = PowerUnit.Watt;

        private readonly PowerUnit? _formatUnit;

        public Power(decimal watts)
            : this(formatUnit: null, watts: watts) { }
        public Power(decimal value, PowerUnit unit)
            : this(formatUnit: unit, watts: GetWatts(value, unit)) { }
        private Power(PowerUnit? formatUnit, decimal watts)
        {
            Assert.IsInRange(watts, MinWatts, MaxWatts, nameof(watts));

            _formatUnit = formatUnit;
            Watts = watts;
        }

        public decimal Watts { get; }
        public decimal Value => GetValue(Watts, Unit);
        public PowerUnit Unit => _formatUnit ?? DefaultUnit;
        decimal ILinearQuantity<PowerUnit>.BaseValue => Watts;

        public Power Convert(PowerUnit targetUnit) =>
            new Power(targetUnit, Watts);

        public bool IsZero() =>
            Watts == decimal.Zero;

        public override string ToString() =>
            DummyStaticQuantityFormatter.ToString<Power, PowerUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticQuantityFormatter.ToString<Power, PowerUnit>(formatProvider, this);

        private static decimal GetWatts(decimal value, PowerUnit sourceUnit) =>
            value * sourceUnit.ValueInWatts;
        private static decimal GetValue(decimal watts, PowerUnit targetUnit) =>
            watts / targetUnit.ValueInWatts;
    }
}
