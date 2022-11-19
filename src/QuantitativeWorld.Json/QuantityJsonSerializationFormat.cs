#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif
    
public enum QuantityJsonSerializationFormat
{
    AsBaseValue,
    AsBaseValueWithUnit,
    AsValueWithUnit
}