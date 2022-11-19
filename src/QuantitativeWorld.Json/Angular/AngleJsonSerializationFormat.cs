#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Angular;
#else
namespace QuantitativeWorld.Json.Angular;
#endif

public enum AngleJsonSerializationFormat
{
    AsTurns = QuantityJsonSerializationFormat.AsBaseValue,
    AsTurnsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}