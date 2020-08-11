using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Area : ILinearQuantity<AreaUnit>
    {
        private const double MinSquareMetres = double.MinValue;
        private const double MaxSquareMetres = double.MaxValue;

        public static readonly AreaUnit DefaultUnit = AreaUnit.SquareMetre;
        public static readonly Area Zero = new Area(0d);
        public static readonly Area PositiveInfinity = new Area(double.PositiveInfinity, null, null, false);
        public static readonly Area NegativeInfinity = new Area(double.NegativeInfinity, null, null, false);

        private readonly AreaUnit? _unit;
        private double? _value;

        public Area(double squareMetres)
            : this(
                squareMetres: squareMetres,
                value: null,
                unit: null)
        { }
        public Area(double value, AreaUnit unit)
            : this(
                squareMetres: GetSquareMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Area(double squareMetres, double? value, AreaUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(squareMetres, MinSquareMetres, MaxSquareMetres, nameof(value));

            SquareMetres = squareMetres;
            _value = value;
            _unit = unit;
        }

        public double SquareMetres { get; }
        public double Value => EnsureValue();
        public AreaUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<AreaUnit>.BaseValue => SquareMetres;
        AreaUnit ILinearQuantity<AreaUnit>.BaseUnit => DefaultUnit;

        public Area Convert(AreaUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Area(
                squareMetres: SquareMetres,
                value: _value,
                unit: targetUnit)
            : new Area(
                squareMetres: SquareMetres,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            SquareMetres == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(formatProvider, this);

        private static double GetSquareMetres(double value, AreaUnit sourceUnit) =>
            value * sourceUnit.ValueInSquareMetres;
        private static double GetValue(double metres, AreaUnit targetUnit) =>
            metres / targetUnit.ValueInSquareMetres;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(SquareMetres, Unit);
            return _value.Value;
        }
    }
}
