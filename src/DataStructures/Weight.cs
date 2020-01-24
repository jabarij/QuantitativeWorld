using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace DataStructures
{
    public partial struct Weight : IQuantity<WeightUnit>
    {
        public const decimal MinValue = decimal.MinValue;
        public const decimal MaxValue = decimal.MaxValue;

        public static readonly WeightUnit DefaultUnit = WeightUnit.Kilogram;

        public static readonly Weight Empty = new Weight();

        private readonly WeightUnit? _unit;

        public Weight(decimal value, WeightUnit unit)
        {
            Assert.IsInRange(value, MinValue, MaxValue, nameof(value));
            Value = value;
            _unit = unit;
        }

        public decimal Value { get; }
        public WeightUnit Unit => _unit ?? DefaultUnit;

        public static Weight FromKilograms(decimal kilograms) =>
            new Weight(kilograms, WeightUnit.Kilogram);
        public static Weight FromTons(decimal tons) =>
            new Weight(tons, WeightUnit.Ton);

        public static bool IsZero(Weight weight) =>
            weight.Value == decimal.Zero;
    }
}
