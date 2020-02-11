namespace QuantitativeWorld.Text.Json
{
    public enum WeightJsonSerializationFormat
    {
        AsKilograms = QuantityJsonSerializationFormat.AsBaseValue,
        AsKilogramsWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}