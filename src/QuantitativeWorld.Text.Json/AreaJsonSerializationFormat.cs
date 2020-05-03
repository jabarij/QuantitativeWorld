namespace QuantitativeWorld.Text.Json
{
    public enum AreaJsonSerializationFormat
    {
        AsSquareMetres = QuantityJsonSerializationFormat.AsBaseValue,
        AsSquareMetresWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}