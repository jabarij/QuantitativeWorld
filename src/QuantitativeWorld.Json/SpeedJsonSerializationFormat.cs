#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum SpeedJsonSerializationFormat
{
    AsMetresPerSecond = QuantityJsonSerializationFormat.AsBaseValue,
    AsMetresPerSecondWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}