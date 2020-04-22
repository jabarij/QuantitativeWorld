using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Length : ILinearQuantity<LengthUnit>
    {
        public static readonly Weight MinValue = new Weight(MinMetres);
        public static readonly Weight MaxValue = new Weight(MaxMetres);
        private const double MinMetres = double.MinValue;
        private const double MaxMetres = double.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;

        private readonly LengthUnit? _formatUnit;

        public Length(double metres)
            : this(formatUnit: null, metres: metres) { }
        public Length(double value, LengthUnit unit)
            : this(formatUnit: unit, metres: GetMetres(value, unit)) { }
        private Length(LengthUnit? formatUnit, double metres)
        {
            Assert.IsInRange(metres, MinMetres, MaxMetres, nameof(metres));

            _formatUnit = formatUnit;
            Metres = metres;
        }

        public double Metres { get; }
        public double Value => GetValue(Metres, Unit);
        public LengthUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<LengthUnit>.BaseValue => Metres;
        LengthUnit ILinearQuantity<LengthUnit>.BaseUnit => DefaultUnit;

        public Length Convert(LengthUnit targetUnit) =>
            new Length(targetUnit, Metres);

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
    }
}
