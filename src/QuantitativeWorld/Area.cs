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

    public partial struct Area : ILinearQuantity<AreaUnit>
    {
        private const number MinSquareMetres = number.MinValue;
        private const number MaxSquareMetres = number.MaxValue;

        public static readonly AreaUnit DefaultUnit = AreaUnit.SquareMetre;
        public static readonly Area Zero = new Area(Constants.Zero);

        private readonly AreaUnit? _unit;
        private number? _value;

        public Area(number squareMetres)
            : this(
                squareMetres: squareMetres,
                value: null,
                unit: null)
        { }
        public Area(number value, AreaUnit unit)
            : this(
                squareMetres: GetSquareMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Area(number squareMetres, number? value, AreaUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(squareMetres, MinSquareMetres, MaxSquareMetres, nameof(value));

            SquareMetres = squareMetres;
            _value = value;
            _unit = unit;
        }

        public number SquareMetres { get; }
        public number Value => EnsureValue();
        public AreaUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<AreaUnit>.BaseValue => SquareMetres;
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
            SquareMetres == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(formatProvider, this);

        private static number GetSquareMetres(number value, AreaUnit sourceUnit) =>
            value * sourceUnit.ValueInSquareMetres;
        private static number GetValue(number metres, AreaUnit targetUnit) =>
            metres / targetUnit.ValueInSquareMetres;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(SquareMetres, Unit);
            return _value.Value;
        }
    }
}
