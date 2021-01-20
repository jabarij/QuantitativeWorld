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

    public partial struct Weight : ILinearQuantity<WeightUnit>
    {
        private const number MinKilograms = number.MinValue;
        private const number MaxKilograms = number.MaxValue;

        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;
        public static readonly Weight Zero = new Weight(Constants.Zero);

        private readonly WeightUnit? _unit;
        private number? _value;

        public Weight(number kilograms)
            : this(
                kilograms: kilograms,
                value: null,
                unit: null)
        { }
        public Weight(number value, WeightUnit unit)
            : this(
                kilograms: GetKilograms(value, unit),
                value: value,
                unit: unit)
        { }
        private Weight(number kilograms, number? value, WeightUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(kilograms, MinKilograms, MaxKilograms, nameof(value));

            Kilograms = kilograms;
            _value = value;
            _unit = unit;
        }

        public number Kilograms { get; }
        public number Value => EnsureValue();
        public WeightUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<WeightUnit>.BaseValue => Kilograms;
        WeightUnit ILinearQuantity<WeightUnit>.BaseUnit => DefaultUnit;

        public Weight Convert(WeightUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Weight(
                kilograms: Kilograms,
                value: _value,
                unit: targetUnit)
            : new Weight(
                kilograms: Kilograms,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            Kilograms == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Weight, WeightUnit>(formatProvider, this);

        private static number GetKilograms(number value, WeightUnit sourceUnit) =>
            value * sourceUnit.ValueInKilograms;
        private static number GetValue(number metres, WeightUnit targetUnit) =>
            metres / targetUnit.ValueInKilograms;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Kilograms, Unit);
            return _value.Value;
        }
    }
}
