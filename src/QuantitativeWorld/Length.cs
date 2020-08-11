using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Length : ILinearQuantity<LengthUnit>
    {
        private const double MinMetres = double.MinValue;
        private const double MaxMetres = double.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;
        public static readonly Length Zero = new Length(0d);
        public static readonly Length PositiveInfinity = new Length(double.PositiveInfinity, null, null, false);
        public static readonly Length NegativeInfinity = new Length(double.NegativeInfinity, null, null, false);

        private readonly LengthUnit? _unit;
        private double? _value;

        public Length(double metres)
            : this(
                metres: metres,
                value: null,
                unit: null)
        { }
        public Length(double value, LengthUnit unit)
            : this(
                metres: GetMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Length(double metres, double? value, LengthUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(metres, MinMetres, MaxMetres, nameof(value));

            Metres = metres;
            _value = value;
            _unit = unit;
        }

        public double Metres { get; }
        public double Value => EnsureValue();
        public LengthUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<LengthUnit>.BaseValue => Metres;
        LengthUnit ILinearQuantity<LengthUnit>.BaseUnit => DefaultUnit;

        public Length Convert(LengthUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Length(
                metres: Metres,
                value: _value,
                unit: targetUnit)
            : new Length(
                metres: Metres,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            Metres == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(formatProvider, this);

        private static double GetMetres(double value, LengthUnit sourceUnit) =>
            value * sourceUnit.ValueInMetres;
        private static double GetValue(double metres, LengthUnit targetUnit) =>
            metres / targetUnit.ValueInMetres;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Metres, Unit);
            return _value.Value;
        }
    }
}
