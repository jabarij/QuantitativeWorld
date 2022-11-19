#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum LinearUnitJsonSerializationFormat
{
    AlwaysFull,
    PredefinedAsString
}