using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Volume : ILinearQuantity<VolumeUnit>
    {
        private const double MinCubicMetres = double.MinValue;
        private const double MaxCubicMetres = double.MaxValue;

        public static readonly VolumeUnit DefaultUnit = VolumeUnit.CubicMetre;
        public static readonly Volume Zero = new Volume(0d);
        public static readonly Volume PositiveInfinity = new Volume(double.PositiveInfinity, null, null, false);
        public static readonly Volume NegativeInfinity = new Volume(double.NegativeInfinity, null, null, false);

        private readonly VolumeUnit? _unit;
        private double? _value;

        public Volume(double cubicMetres)
            : this(
                cubicMetres: cubicMetres,
                value: null,
                unit: null)
        { }
        public Volume(double value, VolumeUnit unit)
            : this(
                cubicMetres: GetCubicMetres(value, unit),
                value: value,
                unit: unit)
        { }
        private Volume(double cubicMetres, double? value, VolumeUnit? unit, bool validate = true)
        {
            if (validate)
                Assert.IsInRange(cubicMetres, MinCubicMetres, MaxCubicMetres, nameof(value));

            CubicMetres = cubicMetres;
            _value = value;
            _unit = unit;
        }

        public double CubicMetres { get; }
        public double Value => EnsureValue();
        public VolumeUnit Unit => _unit ?? DefaultUnit;

        double ILinearQuantity<VolumeUnit>.BaseValue => CubicMetres;
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
            CubicMetres == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Volume, VolumeUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Volume, VolumeUnit>(formatProvider, this);

        private static double GetCubicMetres(double value, VolumeUnit sourceUnit) =>
            value * sourceUnit.ValueInCubicMetres;
        private static double GetValue(double metres, VolumeUnit targetUnit) =>
            metres / targetUnit.ValueInCubicMetres;

        private double EnsureValue()
        {
            if (!_value.HasValue)
                _value = GetValue(CubicMetres, Unit);
            return _value.Value;
        }
    }
}
