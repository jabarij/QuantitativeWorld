namespace QuantitativeWorld.Text.Json
{
    class WeightUnitBuilder : ILinearUnitBuilder<WeightUnit>
    {
        private string _name;
        private string _abbreviation;
        private decimal? _valueInKilograms;

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

        public void SetValueInBaseUnit(decimal valueInBaseUnit) =>
            _valueInKilograms = valueInBaseUnit;

        public bool TryBuild(out WeightUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            decimal? valueInKilograms = _valueInKilograms;

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