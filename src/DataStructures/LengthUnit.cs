using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace DataStructures
{
    public partial struct LengthUnit : ILinearUnit
    {
        public LengthUnit(string name, string abbreviation, decimal valueInMetres)
        {
            Name = Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Abbreviation = Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            ValueInMetres = Assert.IsGreaterThan(valueInMetres, 0m, nameof(valueInMetres));
        }

        public string Name { get; }
        public string Abbreviation { get; }
        public decimal ValueInMetres { get; }

        decimal ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}
