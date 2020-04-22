namespace QuantitativeWorld.Text.Json
{
    internal class LengthBuilder : ILinearQuantityBuilder<Length, LengthUnit>
    {
        private double? _metres;
        private double? _value;
        private LengthUnit? _unit;

        public LengthBuilder() { }
        public LengthBuilder(Length length)
        {
            _metres = length.Metres;
            _value = length.Value;
            _unit = length.Unit;
        }

        public void SetBaseValue(double metres)
        {
            _metres = metres;
            _value = null;
        }

        public void SetValue(double value)
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
            double? metres = _metres;
            double? value = _value;
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