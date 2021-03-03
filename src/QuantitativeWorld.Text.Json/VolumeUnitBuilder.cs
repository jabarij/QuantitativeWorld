#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json
{
    using number = System.Double;
#endif

    internal class VolumeUnitBuilder : ILinearNamedUnitBuilder<VolumeUnit>
    {
        private string _name;
        private string _abbreviation;
        private number? _valueInCubicMetres;

        public VolumeUnitBuilder() { }
        public VolumeUnitBuilder(VolumeUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInCubicMetres = unit.ValueInCubicMetres;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(number valueInBaseUnit) =>
            _valueInCubicMetres = valueInBaseUnit;

        public bool TryBuild(out VolumeUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            number? valueInCubicMetres = _valueInCubicMetres;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInCubicMetres.HasValue)
            {
                result = new VolumeUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInCubicMetres: valueInCubicMetres.Value);
                return true;
            }

            result = default(VolumeUnit);
            return false;
        }
    }
}