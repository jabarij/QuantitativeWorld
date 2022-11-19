#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public enum TimeJsonSerializationFormat
{
    Short,
    Extended
}