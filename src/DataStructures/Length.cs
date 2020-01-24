using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace DataStructures
{
    public partial struct Length : IQuantity<LengthUnit>
    {
        public const decimal MinValue = decimal.MinValue;
        public const decimal MaxValue = decimal.MaxValue;

        public static readonly LengthUnit DefaultUnit = LengthUnit.Metre;

        public static readonly Length Empty = new Length();

        private readonly LengthUnit? _unit;

        public Length(decimal value, LengthUnit unit)
        {
            Assert.IsInRange(value, MinValue, MaxValue, nameof(value));
            Value = value;
            _unit = unit;
        }

        public decimal Value { get; }
        public LengthUnit Unit => _unit ?? DefaultUnit;

        public static Length FromKilometres(decimal kilograms) =>
            new Length(kilograms, LengthUnit.Kilometre);

        public static bool IsZero(Length weight) =>
            weight.Value == decimal.Zero;
    }
}
