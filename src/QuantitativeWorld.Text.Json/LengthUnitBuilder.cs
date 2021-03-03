#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json
{
    using number = System.Double;
#endif

    internal class LengthUnitBuilder : ILinearNamedUnitBuilder<LengthUnit>
    {
        private string _name;
        private string _abbreviation;
        private number? _valueInMetres;

        public LengthUnitBuilder() { }
        public LengthUnitBuilder(LengthUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInMetres = unit.ValueInMetres;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(number valueInBaseUnit) =>
            _valueInMetres = valueInBaseUnit;

        public bool TryBuild(out LengthUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            number? valueInMetres = _valueInMetres;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInMetres.HasValue)
            {
                result = new LengthUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInMetres: valueInMetres.Value);
                return true;
            }

            result = default(LengthUnit);
            return false;
        }
    }
}