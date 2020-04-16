using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Text.Json.Angular
{
    internal class AngleBuilder : ILinearQuantityBuilder<Angle, AngleUnit>
    {
        private decimal? _turns;
        private decimal? _value;
        private AngleUnit? _unit;

        public AngleBuilder() { }
        public AngleBuilder(Angle angle)
        {
            _turns = angle.Turns;
            _value = angle.Value;
            _unit = angle.Unit;
        }

        public void SetBaseValue(decimal turns)
        {
            _turns = turns;
            _value = null;
        }

        public void SetValue(decimal value)
        {
            _turns = null;
            _value = value;
        }

        public void SetUnit(AngleUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Angle result, AngleUnit? defaultUnit = null)
        {
            decimal? turns = _turns;
            decimal? value = _value;
            AngleUnit? unit = _unit ?? defaultUnit;

            if (turns.HasValue)
            {
                result = new Angle(turns.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Angle(value.Value, unit.Value);
                return true;
            }

            result = default(Angle);
            return false;
        }
    }
}