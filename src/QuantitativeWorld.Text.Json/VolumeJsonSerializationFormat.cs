namespace QuantitativeWorld.Text.Json
{
    public enum VolumeJsonSerializationFormat
    {
        AsCubicMetres = QuantityJsonSerializationFormat.AsBaseValue,
        AsCubicMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}