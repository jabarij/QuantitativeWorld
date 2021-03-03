#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public enum PowerJsonSerializationFormat
    {
        AsWatts = QuantityJsonSerializationFormat.AsBaseValue,
        AsWattsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}