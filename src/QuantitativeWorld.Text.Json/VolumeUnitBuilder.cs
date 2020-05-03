namespace QuantitativeWorld.Text.Json
{
    internal class VolumeUnitBuilder : ILinearNamedUnitBuilder<VolumeUnit>
    {
        private string _name;
        private string _abbreviation;
        private double? _valueInCubicMetres;

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

        public void SetValueInBaseUnit(double valueInBaseUnit) =>
            _valueInCubicMetres = valueInBaseUnit;

        public bool TryBuild(out VolumeUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            double? valueInCubicMetres = _valueInCubicMetres;

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