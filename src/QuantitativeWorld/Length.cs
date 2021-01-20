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

    public partial struct Length : ILinearQuantity<LengthUnit>
    {
        private const number MinMetres = number.MinValue;
        private const number MaxMetres = number.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;
        public static readonly Length Zero = new Length(Constants.Zero);

        private readonly LengthUnit? _unit;
        private number? _value;

        public Length(number metres)
            : this(
                metres: metres,
                value: null,
                unit: null)
        { }
        public Length(number value, LengthUnit unit)
            : this(
                metres: GetMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Length(number metres, number? value, LengthUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(metres, MinMetres, MaxMetres, nameof(value));

            Metres = metres;
            _value = value;
            _unit = unit;
        }

        public number Metres { get; }
        public number Value => EnsureValue();
        public LengthUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<LengthUnit>.BaseValue => Metres;
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
            Metres == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(formatProvider, this);

        private static number GetMetres(number value, LengthUnit sourceUnit) =>
            value * sourceUnit.ValueInMetres;
        private static number GetValue(number metres, LengthUnit targetUnit) =>
            metres / targetUnit.ValueInMetres;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(Metres, Unit);
            return _value.Value;
        }
    }
}
