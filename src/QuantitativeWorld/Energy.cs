using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Energy : ILinearQuantity<EnergyUnit>
    {
        private const double MinJoules = double.MinValue;
        private const double MaxJoules = double.MaxValue;

        public static readonly EnergyUnit DefaultUnit = EnergyUnit.Joule;
        public static readonly Energy Zero = new Energy(0d);
        public static readonly Energy PositiveInfinity = new Energy(double.PositiveInfinity, null, null, false);
        public static readonly Energy NegativeInfinity = new Energy(double.NegativeInfinity, null, null, false);

        private readonly EnergyUnit? _unit;
        private double? _value;

        public Energy(double joules)
            : this(
                joules: joules,
                value: null,
                unit: null)
        { }
        public Energy(double value, EnergyUnit unit)
            : this(
                joules: GetJoules(value, unit),
                value: value,
                unit: unit)
        { }
        private Energy(double joules, double? value, EnergyUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(joules, MinJoules, MaxJoules, nameof(value));

            Joules = joules;
            _value = value;
            _unit = unit;
        }

        public double Joules { get; }
        public double Value => EnsureValue();
        public EnergyUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<EnergyUnit>.BaseValue => Joules;
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
            Joules == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(formatProvider, this);

        private static double GetJoules(double value, EnergyUnit sourceUnit) =>
            value * sourceUnit.ValueInJoules;
        private static double GetValue(double metres, EnergyUnit targetUnit) =>
            metres / targetUnit.ValueInJoules;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Joules, Unit);
            return _value.Value;
        }
    }
}
