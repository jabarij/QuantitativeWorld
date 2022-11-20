#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum WeightJsonSerializationFormat
{
    AsKilograms = QuantityJsonSerializationFormat.AsBaseValue,
    AsKilogramsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}