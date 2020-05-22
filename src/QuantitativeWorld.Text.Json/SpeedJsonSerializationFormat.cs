namespace QuantitativeWorld.Text.Json
{
    public enum SpeedJsonSerializationFormat
    {
        AsMetresPerSecond = QuantityJsonSerializationFormat.AsBaseValue,
        AsMetresPerSecondWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}