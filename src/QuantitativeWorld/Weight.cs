using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Weight : ILinearQuantity<WeightUnit>
    {
        public static readonly Weight MinValue = new Weight(MinKilograms);
        public static readonly Weight MaxValue = new Weight(MaxKilograms);
        private const decimal MinKilograms = decimal.MinValue;
        private const decimal MaxKilograms = decimal.MaxValue;
        
        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;

        private readonly WeightUnit? _formatUnit;

        public Weight(decimal kilograms)
            : this(formatUnit: null, kilograms: kilograms) { }
        public Weight(decimal value, WeightUnit unit)
            : this(formatUnit: unit, kilograms: GetKilograms(value, unit)) { }
        private Weight(WeightUnit? formatUnit, decimal kilograms)
        {
            Assert.IsInRange(kilograms, MinKilograms, MaxKilograms, nameof(kilograms));

            _formatUnit = formatUnit;
            Kilograms = kilograms;
        }

        public decimal Kilograms { get; }
        public decimal Value => GetValue(Kilograms, Unit);
        public WeightUnit Unit => _formatUnit ?? DefaultUnit;

        public Weight Convert(WeightUnit targetUnit) =>
            new Weight(targetUnit, Kilograms);

        public bool IsZero() =>
            Kilograms == decimal.Zero;

        public override string ToString() =>
            DummyStaticQuantityFormatter.ToString<Weight, WeightUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticQuantityFormatter.ToString<Weight, WeightUnit>(formatProvider, this);

        private static decimal GetKilograms(decimal value, WeightUnit sourceUnit) =>
            value * sourceUnit.ValueInKilograms;
        private static decimal GetValue(decimal kilograms, WeightUnit targetUnit) =>
            kilograms / targetUnit.ValueInKilograms;
    }
}
