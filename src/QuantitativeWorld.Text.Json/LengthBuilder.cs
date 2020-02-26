namespace QuantitativeWorld.Text.Json
{
    internal class LengthBuilder : ILinearQuantityBuilder<Length, LengthUnit>
    {
        private decimal? _metres;
        private decimal? _value;
        private LengthUnit? _unit;

        public LengthBuilder() { }
        public LengthBuilder(Length weight)
        {
            _metres = weight.Metres;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(decimal metres)
        {
            _metres = metres;
            _value = null;
        }

        public void SetValue(decimal value)
        {
            _metres = null;
            _value = value;
        }

        public void SetUnit(LengthUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Length result, LengthUnit? defaultUnit = null)
        {
            decimal? metres = _metres;
            decimal? value = _value;
            LengthUnit? unit = _unit ?? defaultUnit;

            if (metres.HasValue)
            {
                result = new Length(metres.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Length(value.Value, unit.Value);
                return true;
            }

            result = default(Length);
            return false;
        }
    }
}