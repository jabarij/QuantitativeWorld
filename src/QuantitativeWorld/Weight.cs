using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Weight : ILinearQuantity<WeightUnit>
    {
        public static readonly Weight MinValue = new Weight(MinKilograms);
        public static readonly Weight MaxValue = new Weight(MaxKilograms);
        private const double MinKilograms = double.MinValue;
        private const double MaxKilograms = double.MaxValue;

        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;

        private readonly WeightUnit? _formatUnit;

        public Weight(double kilograms)
            : this(formatUnit: null, kilograms: kilograms) { }
        public Weight(double value, WeightUnit unit)
            : this(formatUnit: unit, kilograms: GetKilograms(value, unit)) { }
        private Weight(WeightUnit? formatUnit, double kilograms)
        {
            Assert.IsInRange(kilograms, MinKilograms, MaxKilograms, nameof(kilograms));

            _formatUnit = formatUnit;
            Kilograms = kilograms;
        }

        public double Kilograms { get; }
        public double Value => GetValue(Kilograms, Unit);
        public WeightUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<WeightUnit>.BaseValue => Kilograms;
        WeightUnit ILinearQuantity<WeightUnit>.BaseUnit => DefaultUnit;

        public Weight Convert(WeightUnit targetUnit) =>
            new Weight(targetUnit, Kilograms);

        public bool IsZero() =>
            Kilograms == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(formatProvider, this);

        private static double GetKilograms(double value, WeightUnit sourceUnit)
        {
            double result =
                sourceUnit.ValueInKilograms < 1d
                ? value / (1d / sourceUnit.ValueInKilograms)
                : value * sourceUnit.ValueInKilograms;
            if (double.IsInfinity(result))
                throw Error.ArgumentOutOfRange(nameof(value), value, $"{value} multiplied by {sourceUnit.ValueInKilograms} gave {result}.");
            return result;
        }
        private static double GetValue(double kilograms, WeightUnit targetUnit) =>
            kilograms / targetUnit.ValueInKilograms;
    }
}
