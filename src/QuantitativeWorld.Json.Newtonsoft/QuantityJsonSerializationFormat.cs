#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public enum QuantityJsonSerializationFormat
    {
        AsBaseValue,
        AsBaseValueWithUnit,
        AsValueWithUnit
    }
}