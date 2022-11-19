#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public enum VolumeJsonSerializationFormat
    {
        AsCubicMetres = QuantityJsonSerializationFormat.AsBaseValue,
        AsCubicMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}