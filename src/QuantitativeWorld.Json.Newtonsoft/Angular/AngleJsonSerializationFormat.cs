#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Angular
{
#else
namespace QuantitativeWorld.Json.Newtonsoft.Angular
{
#endif
    public enum AngleJsonSerializationFormat
    {
        AsTurns = QuantityJsonSerializationFormat.AsBaseValue,
        AsTurnsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}