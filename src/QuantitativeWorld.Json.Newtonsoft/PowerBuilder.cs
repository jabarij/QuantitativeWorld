#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using number = System.Double;
#endif

    internal class PowerBuilder : ILinearQuantityBuilder<Power, PowerUnit>
    {
        private number? _watts;
        private number? _value;
        private PowerUnit? _unit;

        public PowerBuilder() { }
        public PowerBuilder(Power weight)
        {
            _watts = weight.Watts;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(number watts)
        {
            _watts = watts;
            _value = null;
        }

        public void SetValue(number value)
        {
            _watts = null;
            _value = value;
        }

        public void SetUnit(PowerUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Power result, PowerUnit? defaultUnit = null)
        {
            number? watts = _watts;
            number? value = _value;
            PowerUnit? unit = _unit ?? defaultUnit;

            if (watts.HasValue)
            {
                result = new Power(watts.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Power(value.Value, unit.Value);
                return true;
            }

            result = default(Power);
            return false;
        }
    }
}