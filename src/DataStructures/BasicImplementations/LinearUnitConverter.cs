using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.BasicImplementations
{
    public class LinearUnitConverter<TUnit> : IUnitConverter<TUnit>
        where TUnit : ILinearUnit
    {
        public decimal ConvertValue(decimal value, TUnit sourceUnit, TUnit targetUnit) =>
            value * sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}
