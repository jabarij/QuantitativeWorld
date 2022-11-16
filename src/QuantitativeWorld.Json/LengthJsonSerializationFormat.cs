#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum LengthJsonSerializationFormat
{
    AsMetres = QuantityJsonSerializationFormat.AsBaseValue,
    AsMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}