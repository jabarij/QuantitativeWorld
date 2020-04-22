using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct Angle : ILinearQuantity<AngleUnit>
    {
        public static readonly AngleUnit DefaultUnit = AngleUnit.Turn;

        private readonly AngleUnit? _formatUnit;

        public Angle(double turns)
            : this(formatUnit: null, turns: turns) { }
        public Angle(double value, AngleUnit unit)
            : this(formatUnit: unit, turns: GetTurns(value, unit)) { }
        private Angle(AngleUnit? formatUnit, double turns)
        {
            _formatUnit = formatUnit;
            Turns = turns;
        }

        public double Turns { get; }
        public double Value => GetValue(Turns, Unit);
        public AngleUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<AngleUnit>.BaseValue => Turns;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => DefaultUnit;

        public Angle Convert(AngleUnit targetUnit) =>
            new Angle(targetUnit, Turns);
        public Angle ToNormalized() =>
            new Angle(
                formatUnit: _formatUnit,
                turns: Turns % 1d);

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
    }
}
