#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public enum SpeedJsonSerializationFormat
    {
        AsMetresPerSecond = QuantityJsonSerializationFormat.AsBaseValue,
        AsMetresPerSecondWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}