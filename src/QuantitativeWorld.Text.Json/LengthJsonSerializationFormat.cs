namespace QuantitativeWorld.Text.Json
{
    public enum LengthJsonSerializationFormat
    {
        AsMetres = QuantityJsonSerializationFormat.AsBaseValue,
        AsMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}