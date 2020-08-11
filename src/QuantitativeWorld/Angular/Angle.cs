using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct Angle : ILinearQuantity<AngleUnit>
    {
        private const double MinTurns = double.MinValue;
        private const double MaxTurns = double.MaxValue;

        public static readonly AngleUnit DefaultUnit = AngleUnit.Turn;
        public static readonly Angle Zero = new Angle(0d);
        public static readonly Angle PositiveInfinity = new Angle(double.PositiveInfinity, null, null, false);
        public static readonly Angle NegativeInfinity = new Angle(double.NegativeInfinity, null, null, false);

        private readonly AngleUnit? _unit;
        private double? _value;

        public Angle(double turns)
            : this(
                turns: turns,
                value: null,
                unit: null)
        { }
        public Angle(double value, AngleUnit unit)
            : this(
                turns: GetTurns(value, unit),
                value: value,
                unit: unit)
        { }
        private Angle(double turns, double? value, AngleUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(turns, MinTurns, MaxTurns, nameof(value));

            Turns = turns;
            _value = value;
            _unit = unit;
        }

        public double Turns { get; }
        public double Value => EnsureValue();
        public AngleUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<AngleUnit>.BaseValue => Turns;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => DefaultUnit;

        public Angle Convert(AngleUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Angle(
                turns: Turns,
                value: _value,
                unit: targetUnit)
            : new Angle(
                turns: Turns,
                value: null,
                unit: targetUnit);
        public Angle ToNormalized() =>
            new Angle(
                turns: Turns % 1d,
                value: null,
                unit: _unit);

        public bool IsZero() =>
            Turns == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Angle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Angle, AngleUnit>(formatProvider, this);

        private static double GetTurns(double value, AngleUnit sourceUnit) =>
            value / sourceUnit.UnitsPerTurn;
        private static double GetValue(double turns, AngleUnit targetUnit) =>
            turns * targetUnit.UnitsPerTurn;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Turns, Unit);
            return _value.Value;
        }
    }
}
