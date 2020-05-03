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

        private readonly VolumeUnit? _formatUnit;

        public Volume(double cubicMetres)
            : this(formatUnit: null, cubicMetres: cubicMetres) { }
        public Volume(double value, VolumeUnit unit)
            : this(formatUnit: unit, cubicMetres: GetCubicMetres(value, unit)) { }
        private Volume(VolumeUnit? formatUnit, double cubicMetres)
        {
            Assert.IsInRange(cubicMetres, MinCubicMetres, MaxCubicMetres, nameof(cubicMetres));

            _formatUnit = formatUnit;
            CubicMetres = cubicMetres;
        }

        public double CubicMetres { get; }
        public double Value => GetValue(CubicMetres, Unit);
        public VolumeUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<VolumeUnit>.BaseValue => CubicMetres;
        VolumeUnit ILinearQuantity<VolumeUnit>.BaseUnit => DefaultUnit;

        public Volume Convert(VolumeUnit targetUnit) =>
            new Volume(targetUnit, CubicMetres);

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
    }
}
