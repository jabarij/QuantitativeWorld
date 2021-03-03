#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public enum AreaJsonSerializationFormat
    {
        AsSquareMetres = QuantityJsonSerializationFormat.AsBaseValue,
        AsSquareMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}