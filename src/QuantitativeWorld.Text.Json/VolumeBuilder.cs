namespace QuantitativeWorld.Text.Json
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    internal class VolumeBuilder : ILinearQuantityBuilder<Volume, VolumeUnit>
    {
        private number? _cubicMetres;
        private number? _value;
        private VolumeUnit? _unit;

        public VolumeBuilder() { }
        public VolumeBuilder(Volume volume)
        {
            _cubicMetres = volume.CubicMetres;
            _value = volume.Value;
            _unit = volume.Unit;
        }

        public void SetBaseValue(number cubicMetres)
        {
            _cubicMetres = cubicMetres;
            _value = null;
        }

        public void SetValue(number value)
        {
            _cubicMetres = null;
            _value = value;
        }

        public void SetUnit(VolumeUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Volume result, VolumeUnit? defaultUnit = null)
        {
            number? cubicMetres = _cubicMetres;
            number? value = _value;
            VolumeUnit? unit = _unit ?? defaultUnit;

            if (cubicMetres.HasValue)
            {
                result = new Volume(cubicMetres.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Volume(value.Value, unit.Value);
                return true;
            }

            result = default(Volume);
            return false;
        }
    }
}