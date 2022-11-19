#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum EnergyJsonSerializationFormat
{
    AsJoules = QuantityJsonSerializationFormat.AsBaseValue,
    AsJoulesWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}