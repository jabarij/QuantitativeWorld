#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using number = System.Double;
#endif

    internal class WeightUnitBuilder : ILinearNamedUnitBuilder<WeightUnit>
    {
        private string _name;
        private string _abbreviation;
        private number? _valueInKilograms;

        public WeightUnitBuilder() { }
        public WeightUnitBuilder(WeightUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInKilograms = unit.ValueInKilograms;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(number valueInBaseUnit) =>
            _valueInKilograms = valueInBaseUnit;

        public bool TryBuild(out WeightUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            number? valueInKilograms = _valueInKilograms;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInKilograms.HasValue)
            {
                result = new WeightUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInKilograms: valueInKilograms.Value);
                return true;
            }

            result = default(WeightUnit);
            return false;
        }
    }
}