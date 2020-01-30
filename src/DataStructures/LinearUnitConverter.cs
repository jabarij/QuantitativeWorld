namespace DataStructures
{
    public class LinearUnitConverter<TUnit> : IUnitConverter<TUnit>
        where TUnit : ILinearUnit
    {
        public decimal ConvertValue(decimal value, TUnit sourceUnit, TUnit targetUnit) =>
            value * GetConversionFactor(sourceUnit, targetUnit);

        public decimal GetConversionFactor(TUnit sourceUnit, TUnit targetUnit) =>
            sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}
