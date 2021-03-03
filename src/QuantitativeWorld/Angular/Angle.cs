using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Angular
{
    using QuantitativeWorld.Interfaces;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    public partial struct Angle : ILinearQuantity<AngleUnit>
    {
        private const number MinTurns = number.MinValue;
        private const number MaxTurns = number.MaxValue;

        public static readonly AngleUnit DefaultUnit = AngleUnit.Turn;
        public static readonly Angle Zero = new Angle(Constants.Zero);
#if !DECIMAL
        public static readonly Angle PositiveInfinity = new Angle(number.PositiveInfinity, null, null, false);
        public static readonly Angle NegativeInfinity = new Angle(number.NegativeInfinity, null, null, false);
#endif

        private readonly AngleUnit? _unit;
        private number? _value;

        public Angle(number turns)
            : this(
                turns: turns,
                value: null,
                unit: null)
        { }
        public Angle(number value, AngleUnit unit)
            : this(
                turns: GetTurns(value, unit),
                value: value,
                unit: unit)
        { }
        private Angle(number turns, number? value, AngleUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(turns, MinTurns, MaxTurns, nameof(value));

            Turns = turns;
            _value = value;
            _unit = unit;
        }

        public number Turns { get; }
        public number Value => EnsureValue();
        public AngleUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<AngleUnit>.BaseValue => Turns;
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
                turns: Turns % Constants.One,
                value: null,
                unit: _unit);

        public bool IsZero() =>
            Turns == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Angle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Angle, AngleUnit>(formatProvider, this);

        private static number GetTurns(number value, AngleUnit sourceUnit) =>
            value / sourceUnit.UnitsPerTurn;
        private static number GetValue(number turns, AngleUnit targetUnit) =>
            turns * targetUnit.UnitsPerTurn;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Turns, Unit);
            return _value.Value;
        }
    }
}
