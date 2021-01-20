﻿namespace QuantitativeWorld.Text.Json
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    internal class EnergyBuilder : ILinearQuantityBuilder<Energy, EnergyUnit>
    {
        private number? _joules;
        private number? _value;
        private EnergyUnit? _unit;

        public EnergyBuilder() { }
        public EnergyBuilder(Energy weight)
        {
            _joules = weight.Joules;
            _value = weight.Value;
            _unit = weight.Unit;
        }

        public void SetBaseValue(number joules)
        {
            _joules = joules;
            _value = null;
        }

        public void SetValue(number value)
        {
            _joules = null;
            _value = value;
        }

        public void SetUnit(EnergyUnit unit)
        {
            _unit = unit;
        }

        public bool TryBuild(out Energy result, EnergyUnit? defaultUnit = null)
        {
            number? joules = _joules;
            number? value = _value;
            EnergyUnit? unit = _unit ?? defaultUnit;

            if (joules.HasValue)
            {
                result = new Energy(joules.Value);
                if (unit.HasValue)
                    result = result.Convert(unit.Value);
                return true;
            }

            if (value.HasValue && unit.HasValue)
            {
                result = new Energy(value.Value, unit.Value);
                return true;
            }

            result = default(Energy);
            return false;
        }
    }
}