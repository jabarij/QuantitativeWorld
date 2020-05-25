namespace QuantitativeWorld.Text.Json
{
    internal class SpeedUnitBuilder : ILinearNamedUnitBuilder<SpeedUnit>
    {
        private string _name;
        private string _abbreviation;
        private double? _valueInMetresPerSecond;

        public SpeedUnitBuilder() { }
        public SpeedUnitBuilder(SpeedUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInMetresPerSecond = unit.ValueInMetresPerSecond;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(double valueInBaseUnit) =>
            _valueInMetresPerSecond = valueInBaseUnit;

        public bool TryBuild(out SpeedUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            double? valueInMetresPerSecond = _valueInMetresPerSecond;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInMetresPerSecond.HasValue)
            {
                result = new SpeedUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInMetresPerSecond: valueInMetresPerSecond.Value);
                return true;
            }

            result = default(SpeedUnit);
            return false;
        }
    }
}