namespace QuantitativeWorld.Text.Json
{
    internal class WeightBuilder : ILinearQuantityBuilder<Weight, WeightUnit>
    {
        private decimal? _kilograms;
        private decimal? _value;
        private WeightUnit? _unit;

        public WeightBuilder() { }
        public WeightBuilder(Weight weight)
        {
            _kilograms = weight.Kilograms;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(decimal kilograms)
        {
            _kilograms = kilograms;
            _value = null;
        }

        public void SetValue(decimal value)
        {
            _kilograms = null;
            _value = value;
        }

        public void SetUnit(WeightUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Weight result, WeightUnit? defaultUnit = null)
        {
            decimal? kilograms = _kilograms;
            decimal? value = _value;
            WeightUnit? unit = _unit ?? defaultUnit;

            if (kilograms.HasValue)
            {
                result = new Weight(kilograms.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Weight(value.Value, unit.Value);
                return true;
            }

            result = default(Weight);
            return false;
        }
    }
}