using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;

namespace QuantitativeWorld
{
    public partial struct Length : IQuantity<LengthUnit>
    {
        public const decimal MinValue = decimal.MinValue;
        public const decimal MaxValue = decimal.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;

        public static readonly Length Empty = new Length();

        private readonly LengthUnit? _formatUnit;

        public Length(decimal metres)
            : this(formatUnit: DefaultUnit, metres: metres) { }
        public Length(decimal value, LengthUnit unit)
            : this(formatUnit: unit, metres: GetMetres(value, unit)) { }
        private Length(LengthUnit formatUnit, decimal metres)
        {
            if (formatUnit.IsEmpty())
                throw new ArgumentException("Unit cannot be empty.", nameof(formatUnit));
            Assert.IsInRange(metres, MinValue, MaxValue, nameof(metres));

            _formatUnit = formatUnit;
            Metres = metres;
        }

        public decimal Metres { get; }
        public decimal Value => GetValue(Metres, Unit);
        public LengthUnit Unit => _formatUnit ?? DefaultUnit;

        public Length Convert(LengthUnit targetUnit) =>
            new Length(targetUnit, Metres);

        public bool IsZero() =>
            Metres == decimal.Zero;

        private static decimal GetMetres(decimal value, LengthUnit sourceUnit) =>
            value * sourceUnit.ValueInMetres;
        private static decimal GetValue(decimal metres, LengthUnit targetUnit) =>
            metres / targetUnit.ValueInMetres;
    }
}
