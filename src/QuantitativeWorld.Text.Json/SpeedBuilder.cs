namespace QuantitativeWorld.Text.Json
{
    internal class SpeedBuilder : ILinearQuantityBuilder<Speed, SpeedUnit>
    {
        private double? _metresPerSecond;
        private double? _value;
        private SpeedUnit? _unit;

        public SpeedBuilder() { }
        public SpeedBuilder(Speed speed)
        {
            _metresPerSecond = speed.MetresPerSecond;
            _value = speed.Value;
            _unit = speed.Unit;
        }

        public void SetBaseValue(double metresPerSecond)
        {
            _metresPerSecond = metresPerSecond;
            _value = null;
        }

        public void SetValue(double value)
        {
            _metresPerSecond = null;
            _value = value;
        }

        public void SetUnit(SpeedUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Speed result, SpeedUnit? defaultUnit = null)
        {
            double? metresPerSecond = _metresPerSecond;
            double? value = _value;
            SpeedUnit? unit = _unit ?? defaultUnit;

            if (metresPerSecond.HasValue)
            {
                result = new Speed(metresPerSecond.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Speed(value.Value, unit.Value);
                return true;
            }

            result = default(Speed);
            return false;
        }
    }
}