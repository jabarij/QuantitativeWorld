#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum VolumeJsonSerializationFormat
{
    AsCubicMetres = QuantityJsonSerializationFormat.AsBaseValue,
    AsCubicMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}