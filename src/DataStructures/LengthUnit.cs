using QuantitativeWorld.DotNetExtensions;

namespace QuantitativeWorld
{
    public partial struct LengthUnit : ILinearUnit
    {
        public static readonly LengthUnit Empty = new LengthUnit();

        public LengthUnit(string name, string abbreviation, decimal valueInMetres)
        {
            Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Assert.IsNotNullOrWhiteSpace(abbreviation, nameof(abbreviation));
            Assert.IsGreaterThan(valueInMetres, 0m, nameof(valueInMetres));

            Name = name;
            Abbreviation = abbreviation;
            ValueInMetres = valueInMetres;
        }

        public string Name { get; }
        public string Abbreviation { get; }
        public decimal ValueInMetres { get; }

        public bool IsEmpty() =>
            Equals(Empty);

        decimal ILinearUnit.ValueInBaseUnit => ValueInMetres;
    }
}