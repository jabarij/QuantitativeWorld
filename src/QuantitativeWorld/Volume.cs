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

    public partial struct Volume : ILinearQuantity<VolumeUnit>
    {
        private const number MinCubicMetres = number.MinValue;
        private const number MaxCubicMetres = number.MaxValue;

        public static readonly VolumeUnit DefaultUnit = VolumeUnit.CubicMetre;
        public static readonly Volume Zero = new Volume(Constants.Zero);

        private readonly VolumeUnit? _unit;
        private number? _value;

        public Volume(number cubicMetres)
            : this(
                cubicMetres: cubicMetres,
                value: null,
                unit: null)
        { }
        public Volume(number value, VolumeUnit unit)
            : this(
                cubicMetres: GetCubicMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Volume(number cubicMetres, number? value, VolumeUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(cubicMetres, MinCubicMetres, MaxCubicMetres, nameof(value));

            CubicMetres = cubicMetres;
            _value = value;
            _unit = unit;
        }

        public number CubicMetres { get; }
        public number Value => EnsureValue();
        public VolumeUnit Unit => _unit ?? DefaultUnit;

        number ILinearQuantity<VolumeUnit>.BaseValue => CubicMetres;
        VolumeUnit ILinearQuantity<VolumeUnit>.BaseUnit => DefaultUnit;

        public Volume Convert(VolumeUnit targetUnit) =>
            targetUnit.IsEquivalentOf(Unit)
            ? new Volume(
                cubicMetres: CubicMetres,
                value: _value,
                unit: targetUnit)
            : new Volume(
                cubicMetres: CubicMetres,
                value: null,
                unit: targetUnit);

        public bool IsZero() =>
            CubicMetres == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Volume, VolumeUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Volume, VolumeUnit>(formatProvider, this);

        private static number GetCubicMetres(number value, VolumeUnit sourceUnit) =>
            value * sourceUnit.ValueInCubicMetres;
        private static number GetValue(number metres, VolumeUnit targetUnit) =>
            metres / targetUnit.ValueInCubicMetres;

        private number EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(CubicMetres, Unit);
            return _value.Value;
        }
    }
}
