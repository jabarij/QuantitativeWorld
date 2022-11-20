#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using number = System.Decimal;

#else
namespace QuantitativeWorld.Json;

using number = System.Double;
#endif

internal struct WeightBuilder : ILinearQuantityBuilder<Weight, WeightUnit>
{
    private number? _kilograms;
    private number? _value;
    private WeightUnit? _unit;

    public WeightBuilder(Weight weight)
    {
        _kilograms = weight.Kilograms;
        _value = weight.Value;
        _unit = weight.Unit;
    }

    public void SetBaseValue(number kilograms)
    {
        _kilograms = kilograms;
        _value = null;
    }

    public void SetValue(number value)
    {
        _kilograms = null;
        _value = value;
    }

    public void SetUnit(WeightUnit unit)
        => _unit = unit;

    public bool TryBuild(out Weight result, WeightUnit? defaultUnit = null)
    {
        number? kilograms = _kilograms;
        number? value = _value;
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