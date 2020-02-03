using QuantitativeWorld.DotNetExtensions;

namespace QuantitativeWorld
{
    public partial struct Weight : IQuantity<WeightUnit>
    {
        public const decimal MinValue = decimal.MinValue;
        public const decimal MaxValue = decimal.MaxValue;

        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;

        private readonly WeightUnit? _formatUnit;

        public Weight(decimal kilograms)
            : this(formatUnit: DefaultUnit, kilograms: kilograms) { }
        public Weight(decimal value, WeightUnit unit)
            : this(formatUnit: unit, kilograms: GetKilograms(value, unit)) { }
        private Weight(WeightUnit formatUnit, decimal kilograms)
        {
            Assert.IsInRange(kilograms, MinValue, MaxValue, nameof(kilograms));

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

        private static decimal GetKilograms(decimal value, WeightUnit sourceUnit) =>
            value * sourceUnit.ValueInKilograms;
        private static decimal GetValue(decimal kilograms, WeightUnit targetUnit) =>
            kilograms / targetUnit.ValueInKilograms;
    }
}
