using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Text.Json.Angular
{
    internal class AngleUnitBuilder
    {
        private string _name;
        private string _abbreviation;
        private string _symbol;
        private double? _unitsPerTurn;

        public AngleUnitBuilder() { }
        public AngleUnitBuilder(AngleUnit unit)
        {
            _name = unit.Name;
            _abbreviation = unit.Abbreviation;
            _symbol = unit.Symbol;
            _unitsPerTurn = unit.UnitsPerTurn;
        }

        public void SetAbbreviation(string abbreviation) =>
            _abbreviation = abbreviation;

        public void SetName(string name) =>
            _name = name;

        public void SetSymbol(string symbol) =>
            _symbol = symbol;

        public void SetUnitsPerTurn(double unitsPerTurn) =>
            _unitsPerTurn = unitsPerTurn;

        public bool TryBuild(out AngleUnit result)
        {
            string name = _name;
            string abbreviation = _abbreviation;
            string symbol = _symbol;
            double? unitsPerTurn = _unitsPerTurn;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(abbreviation)
                && !string.IsNullOrWhiteSpace(symbol)
                && unitsPerTurn.HasValue)
            {
                result = new AngleUnit(
                    name: name,
                    abbreviation: abbreviation,
                    symbol: symbol,
                    unitsPerTurn: unitsPerTurn.Value);
                return true;
            }

            result = default(AngleUnit);
            return false;
        }
    }
}