#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json
{
    using number = System.Double;
#endif

    internal class PowerUnitBuilder : ILinearNamedUnitBuilder<PowerUnit>
    {
        private string _name;
        private string _abbreviation;
        private number? _valueInWatts;

        public PowerUnitBuilder() { }
        public PowerUnitBuilder(PowerUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInWatts = unit.ValueInWatts;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(number valueInBaseUnit) =>
            _valueInWatts = valueInBaseUnit;

        public bool TryBuild(out PowerUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            number? valueInWatts = _valueInWatts;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInWatts.HasValue)
            {
                result = new PowerUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInWatts: valueInWatts.Value);
                return true;
            }

            result = default(PowerUnit);
            return false;
        }
    }
}