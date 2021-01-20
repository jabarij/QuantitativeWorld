namespace QuantitativeWorld.Text.Json
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    internal class AreaUnitBuilder : ILinearNamedUnitBuilder<AreaUnit>
    {
        private string _name;
        private string _abbreviation;
        private number? _valueInSquareMetres;

        public AreaUnitBuilder() { }
        public AreaUnitBuilder(AreaUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _valueInSquareMetres = unit.ValueInSquareMetres;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetValueInBaseUnit(number valueInBaseUnit) =>
            _valueInSquareMetres = valueInBaseUnit;

        public bool TryBuild(out AreaUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            number? valueInSquareMetres = _valueInSquareMetres;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && valueInSquareMetres.HasValue)
            {
                result = new AreaUnit(
                    name: name,
                    abbreviation: abbreviation,
                    valueInSquareMetres: valueInSquareMetres.Value);
                return true;
            }

            result = default(AreaUnit);
            return false;
        }
    }
}