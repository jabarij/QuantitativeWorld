namespace QuantitativeWorld.Text.Json
{
    public enum EnergyJsonSerializationFormat
    {
        AsJoules = QuantityJsonSerializationFormat.AsBaseValue,
        AsJoulesWithUnit = QuantityJsonSerializationFormat.AsBaseValueWithUnit,
        AsValueWithUnit = QuantityJsonSerializationFormat.AsValueWithUnit
    }
}