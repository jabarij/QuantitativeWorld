#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json
{
    using number = System.Double;
#endif

    internal class SpeedBuilder : ILinearQuantityBuilder<Speed, SpeedUnit>
    {
        private number? _metresPerSecond;
        private number? _value;
        private SpeedUnit? _unit;

        public SpeedBuilder() { }
        public SpeedBuilder(Speed speed)
        {
            _metresPerSecond = speed.MetresPerSecond;
            _value = speed.Value;
            _unit = speed.Unit;
        }

        public void SetBaseValue(number metresPerSecond)
        {
            _metresPerSecond = metresPerSecond;
            _value = null;
        }

        public void SetValue(number value)
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
            number? metresPerSecond = _metresPerSecond;
            number? value = _value;
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