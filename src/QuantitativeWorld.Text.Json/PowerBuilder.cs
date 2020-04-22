namespace QuantitativeWorld.Text.Json
{
    internal class PowerBuilder : ILinearQuantityBuilder<Power, PowerUnit>
    {
        private double? _watts;
        private double? _value;
        private PowerUnit? _unit;

        public PowerBuilder() { }
        public PowerBuilder(Power weight)
        {
            _watts = weight.Watts;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(double watts)
        {
            _watts = watts;
            _value = null;
        }

        public void SetValue(double value)
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
            double? watts = _watts;
            double? value = _value;
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