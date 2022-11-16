#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using number = System.Double;
#endif

    internal class LengthBuilder : ILinearQuantityBuilder<Length, LengthUnit>
    {
        private number? _metres;
        private number? _value;
        private LengthUnit? _unit;

        public LengthBuilder() { }
        public LengthBuilder(Length length)
        {
            _metres = length.Metres;
            _value = length.Value;
            _unit = length.Unit;
        }

        public void SetBaseValue(number metres)
        {
            _metres = metres;
            _value = null;
        }

        public void SetValue(number value)
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
            number? metres = _metres;
            number? value = _value;
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