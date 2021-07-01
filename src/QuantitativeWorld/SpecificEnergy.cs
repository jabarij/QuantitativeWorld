using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Interfaces;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    public partial struct SpecificEnergy : ILinearQuantity<SpecificEnergyUnit>
    {
        private const number MinJoulesPerKilogram = number.MinValue;
        private const number MaxJoulesPerKilogram = number.MaxValue;

        public static readonly SpecificEnergyUnit DefaultUnit = SpecificEnergyUnit.JoulePerKilogram;
        public static readonly SpecificEnergy Zero = new SpecificEnergy(Constants.Zero);

        private readonly SpecificEnergyUnit? _unit;
        private number? _value;

        public SpecificEnergy(number joulesPerKilogram)
            : this(
                joulesPerKilogram: joulesPerKilogram,
                value: null,
                unit: null)
        { }
        public SpecificEnergy(number value, SpecificEnergyUnit unit)
            : this(
                joulesPerKilogram: GetJoulesPerKilogram(value, unit),
                value: value,
                unit: unit)
        { }
        private SpecificEnergy(number joulesPerKilogram, number? value, SpecificEnergyUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(joulesPerKilogram, MinJoulesPerKilogram, MaxJoulesPerKilogram, nameof(value));

            JoulesPerKilogram = joulesPerKilogram;
            _value = value;
            _unit = unit;
        }

        public number JoulesPerKilogram { get; }
        public number Value => EnsureValue();
        public SpecificEnergyUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<SpecificEnergyUnit>.BaseValue => JoulesPerKilogram;
        SpecificEnergyUnit ILinearQuantity<SpecificEnergyUnit>.BaseUnit => DefaultUnit;

        public SpecificEnergy Convert(SpecificEnergyUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new SpecificEnergy(
                joulesPerKilogram: JoulesPerKilogram,
                value: _value,
                unit: targetUnit)
            : new SpecificEnergy(
                joulesPerKilogram: JoulesPerKilogram,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            JoulesPerKilogram == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<SpecificEnergy, SpecificEnergyUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<SpecificEnergy, SpecificEnergyUnit>(formatProvider, this);

        private static number GetJoulesPerKilogram(number value, SpecificEnergyUnit sourceUnit) =>
            value * sourceUnit.ValueInJoulesPerKilogram;
        private static number GetValue(number joulesPerKilogram, SpecificEnergyUnit targetUnit) =>
            joulesPerKilogram / targetUnit.ValueInJoulesPerKilogram;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(JoulesPerKilogram, Unit);
            return _value.Value;
        }
    }
}
