using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Text.Json.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    internal class AngleBuilder : ILinearQuantityBuilder<Angle, AngleUnit>
    {
        private number? _turns;
        private number? _value;
        private AngleUnit? _unit;

        public AngleBuilder() { }
        public AngleBuilder(Angle angle)
        {
            _turns = angle.Turns;
            _value = angle.Value;
            _unit = angle.Unit;
        }

        public void SetBaseValue(number turns)
        {
            _turns = turns;
            _value = null;
        }

        public void SetValue(number value)
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
            number? turns = _turns;
            number? value = _value;
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