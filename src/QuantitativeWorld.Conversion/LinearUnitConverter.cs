using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Conversion
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class LinearUnitConverter<TUnit> : IUnitConverter<TUnit>
        where TUnit : ILinearUnit
    {
        public number ConvertValue(number value, TUnit sourceUnit, TUnit targetUnit) =>
            value * sourceUnit.ValueInBaseUnit / targetUnit.ValueInBaseUnit;
    }
}
