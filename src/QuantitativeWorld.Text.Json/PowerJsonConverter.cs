namespace QuantitativeWorld.Text.Json
{
    public sealed class PowerJsonConverter : LinearQuantityJsonConverterBase<Power, PowerUnit>
    {
        public PowerJsonConverter(
            PowerJsonSerializationFormat serializationFormat = PowerJsonSerializationFormat.AsWattsWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new PowerUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Power.Watts);
        protected override ILinearQuantityBuilder<Power, PowerUnit> CreateBuilder() =>
            new PowerBuilder();
    }
}
