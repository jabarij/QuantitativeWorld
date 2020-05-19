namespace QuantitativeWorld.Text.Json
{
    public sealed class EnergyJsonConverter : LinearQuantityJsonConverterBase<Energy, EnergyUnit>
    {
        public EnergyJsonConverter(
            EnergyJsonSerializationFormat serializationFormat = EnergyJsonSerializationFormat.AsJoulesWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Energy.Joules);
        protected override ILinearQuantityBuilder<Energy, EnergyUnit> CreateBuilder() =>
            new EnergyBuilder();
    }
}
