#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public enum SpeedJsonSerializationFormat
    {
        AsMetresPerSecond = QuantityJsonSerializationFormat.AsBaseValue,
        AsMetresPerSecondWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}