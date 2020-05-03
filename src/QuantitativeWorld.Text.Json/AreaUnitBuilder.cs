namespace QuantitativeWorld.Text.Json
{
    internal class AreaUnitBuilder : ILinearNamedUnitBuilder<AreaUnit>
    {
        private string _name;
        private string _abbreviation;
        private double? _valueInMetres;

        public AreaUnitBuilder() { }
        public AreaUnitBuilder(AreaUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInMetres = unit.ValueInSquareMetres;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(double valueInBaseUnit) =>
            _valueInMetres = valueInBaseUnit;

        public bool TryBuild(out AreaUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            double? valueInMetres = _valueInMetres;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInMetres.HasValue)
            {
                result = new AreaUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInSquareMetres: valueInMetres.Value);
                return true;
            }

            result = default(AreaUnit);
            return false;
        }
    }
}