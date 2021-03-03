#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public enum QuantityJsonSerializationFormat
    {
        AsBaseValue,
        AsBaseValueWithUnit,
        AsValueWithUnit
    }
}