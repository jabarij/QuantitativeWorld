using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Length : ILinearQuantity<LengthUnit>
    {
        public static readonly Weight MinValue = new Weight(MinMetres);
        public static readonly Weight MaxValue = new Weight(MaxMetres);
        private const decimal MinMetres = decimal.MinValue;
        private const decimal MaxMetres = decimal.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;

        private readonly LengthUnit? _formatUnit;

        public Length(decimal metres)
            : this(formatUnit: null, metres: metres) { }
        public Length(decimal value, LengthUnit unit)
            : this(formatUnit: unit, metres: GetMetres(value, unit)) { }
        private Length(LengthUnit? formatUnit, decimal metres)
        {
            Assert.IsInRange(metres, MinMetres, MaxMetres, nameof(metres));

            _formatUnit = formatUnit;
            Metres = metres;
        }

        public decimal Metres { get; }
        public decimal Value => GetValue(Metres, Unit);
        public LengthUnit Unit => _formatUnit ?? DefaultUnit;
        decimal ILinearQuantity<LengthUnit>.BaseValue => Metres;
        LengthUnit ILinearQuantity<LengthUnit>.BaseUnit => DefaultUnit;

        public Length Convert(LengthUnit targetUnit) =>
            new Length(targetUnit, Metres);

        public bool IsZero() =>
            Metres == decimal.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Length, LengthUnit>(formatProvider, this);

        private static decimal GetMetres(decimal value, LengthUnit sourceUnit) =>
            value * sourceUnit.ValueInMetres;
        private static decimal GetValue(decimal metres, LengthUnit targetUnit) =>
            metres / targetUnit.ValueInMetres;
    }
}
