namespace QuantitativeWorld.Text.Json
{
    public enum PowerJsonSerializationFormat
    {
        AsWatts = QuantityJsonSerializationFormat.AsBaseValue,
        AsWattsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}