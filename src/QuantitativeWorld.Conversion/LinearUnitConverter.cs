#if DECIMAL
namespace DecimalQuantitativeWorld.Conversion
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Conversion
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
#endif

    public class LinearUnitConverter<TUnit> : IUnitConverter<TUnit>
        where TUnit : ILinearUnit
    {
        public number ConvertValue(number value, TUnit sourceUnit, TUnit targetUnit) =>
            value * sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}
