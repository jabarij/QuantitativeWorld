namespace QuantitativeWorld.Text.Json
{
    internal class WeightBuilder : ILinearQuantityBuilder<Weight, WeightUnit>
    {
        private double? _kilograms;
        private double? _value;
        private WeightUnit? _unit;

        public WeightBuilder() { }
        public WeightBuilder(Weight weight)
        {
            _kilograms = weight.Kilograms;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(double kilograms)
        {
            _kilograms = kilograms;
            _value = null;
        }

        public void SetValue(double value)
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
            double? kilograms = _kilograms;
            double? value = _value;
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