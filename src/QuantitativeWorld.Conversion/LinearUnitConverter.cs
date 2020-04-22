using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Conversion
{
    public class LinearUnitConverter<TUnit> : IUnitConverter<TUnit>
        where TUnit : ILinearUnit
    {
        public double ConvertValue(double value, TUnit sourceUnit, TUnit targetUnit) =>
            value * sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}
