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

    public partial struct Energy : ILinearQuantity<EnergyUnit>
    {
        private const number MinJoules = number.MinValue;
        private const number MaxJoules = number.MaxValue;

        public static readonly EnergyUnit DefaultUnit = EnergyUnit.Joule;
        public static readonly Energy Zero = new Energy(Constants.Zero);

        private readonly EnergyUnit? _unit;
        private number? _value;

        public Energy(number joules)
            : this(
                joules: joules,
                value: null,
                unit: null)
        { }
        public Energy(number value, EnergyUnit unit)
            : this(
                joules: GetJoules(value, unit),
                value: value,
                unit: unit)
        { }
        private Energy(number joules, number? value, EnergyUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(joules, MinJoules, MaxJoules, nameof(value));

            Joules = joules;
            _value = value;
            _unit = unit;
        }

        public number Joules { get; }
        public number Value => EnsureValue();
        public EnergyUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<EnergyUnit>.BaseValue => Joules;
        EnergyUnit ILinearQuantity<EnergyUnit>.BaseUnit => DefaultUnit;

        public Energy Convert(EnergyUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Energy(
                joules: Joules,
                value: _value,
                unit: targetUnit)
            : new Energy(
                joules: Joules,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            Joules == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(formatProvider, this);

        private static number GetJoules(number value, EnergyUnit sourceUnit) =>
            value * sourceUnit.ValueInJoules;
        private static number GetValue(number metres, EnergyUnit targetUnit) =>
            metres / targetUnit.ValueInJoules;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Joules, Unit);
            return _value.Value;
        }
    }
}
