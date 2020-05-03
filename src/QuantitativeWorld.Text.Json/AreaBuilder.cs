namespace QuantitativeWorld.Text.Json
{
    internal class AreaBuilder : ILinearQuantityBuilder<Area, AreaUnit>
    {
        private double? _squareMetres;
        private double? _value;
        private AreaUnit? _unit;

        public AreaBuilder() { }
        public AreaBuilder(Area area)
        {
            _squareMetres = area.SquareMetres;
            _value = area.Value;
            _unit = area.Unit;
        }

        public void SetBaseValue(double squareMetres)
        {
            _squareMetres = squareMetres;
            _value = null;
        }

        public void SetValue(double value)
        {
            _squareMetres = null;
            _value = value;
        }

        public void SetUnit(AreaUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Area result, AreaUnit? defaultUnit = null)
        {
            double? metres = _squareMetres;
            double? value = _value;
            AreaUnit? unit = _unit ?? defaultUnit;

            if (metres.HasValue)
            {
                result = new Area(metres.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Area(value.Value, unit.Value);
                return true;
            }

            result = default(Area);
            return false;
        }
    }
}