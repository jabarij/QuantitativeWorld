using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace QuantitativeWorld
{
    public partial struct WeightUnit : ILinearUnit
    {
        public static readonly WeightUnit Empty = new WeightUnit();

        public WeightUnit(string name, string abbreviation, decimal valueInKilograms)
        {
            Name = Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Abbreviation = Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            ValueInKilograms = Assert.IsGreaterThan(valueInKilograms, 0m, nameof(valueInKilograms));
        }

        public string Name { get; }
        public string Abbreviation { get; }
        public decimal ValueInKilograms { get; }

        public bool IsEmpty() =>
            Equals(Empty);

        decimal ILinearUnit.ValueInBaseUnit => ValueInKilograms;
    }
}