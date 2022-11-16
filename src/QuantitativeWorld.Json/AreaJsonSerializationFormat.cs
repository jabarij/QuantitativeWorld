#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum AreaJsonSerializationFormat
{
    AsSquareMetres = QuantityJsonSerializationFormat.AsBaseValue,
    AsSquareMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
    AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
}