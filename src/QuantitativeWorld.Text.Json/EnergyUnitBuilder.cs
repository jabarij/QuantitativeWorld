namespace QuantitativeWorld.Text.Json
{
    internal class EnergyUnitBuilder : ILinearNamedUnitBuilder<EnergyUnit>
    {
        private string _name;
        private string _abbreviation;
        private double? _valueInJoules;

        public EnergyUnitBuilder() { }
        public EnergyUnitBuilder(EnergyUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInJoules = unit.ValueInJoules;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(double valueInBaseUnit) =>
            _valueInJoules = valueInBaseUnit;

        public bool TryBuild(out EnergyUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            double? valueInJoules = _valueInJoules;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInJoules.HasValue)
            {
                result = new EnergyUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInJoules: valueInJoules.Value);
                return true;
            }

            result = default(EnergyUnit);
            return false;
        }
    }
}