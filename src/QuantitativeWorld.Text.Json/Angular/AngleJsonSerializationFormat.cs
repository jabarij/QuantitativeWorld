#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json.Angular
{
#else
namespace QuantitativeWorld.Text.Json.Angular
{
#endif
    public enum AngleJsonSerializationFormat
    {
        AsTurns = QuantityJsonSerializationFormat.AsBaseValue,
        AsTurnsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}