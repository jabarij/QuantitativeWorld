using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct Angle : ILinearQuantity<AngleUnit>
    {
        public static readonly AngleUnit DefaultUnit = AngleUnit.Turn;

        private readonly AngleUnit? _formatUnit;

        public Angle(decimal turns)
            : this(formatUnit: null, turns: turns) { }
        public Angle(decimal value, AngleUnit unit)
            : this(formatUnit: unit, turns: GetTurns(value, unit)) { }
        private Angle(AngleUnit? formatUnit, decimal turns)
        {
            _formatUnit = formatUnit;
            Turns = turns;
        }

        public decimal Turns { get; }
        public decimal Value => GetValue(Turns, Unit);
        public AngleUnit Unit => _formatUnit ?? DefaultUnit;
        decimal ILinearQuantity<AngleUnit>.BaseValue => Turns;

        public Angle Convert(AngleUnit targetUnit) =>
            new Angle(targetUnit, Turns);
        public Angle ToNormalized() =>
            new Angle(
                formatUnit: _formatUnit,
                turns: Turns % 1m);

        public bool IsZero() =>
            Turns == decimal.Zero;

        public override string ToString() =>
            DummyStaticQuantityFormatter.ToString<Angle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticQuantityFormatter.ToString<Angle, AngleUnit>(formatProvider, this);

        private static decimal GetTurns(decimal value, AngleUnit sourceUnit) =>
            value / sourceUnit.UnitsPerTurn;
        private static decimal GetValue(decimal turns, AngleUnit targetUnit) =>
            turns * targetUnit.UnitsPerTurn;
    }
}
