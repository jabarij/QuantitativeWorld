namespace QuantitativeWorld.Text.Json
{
    public sealed class LengthJsonConverter : LinearQuantityJsonConverterBase<Length, LengthUnit>
    {
        public LengthJsonConverter(
            LengthJsonSerializationFormat serializationFormat = LengthJsonSerializationFormat.AsMetresWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Length.Metres);
        protected override ILinearQuantityBuilder<Length, LengthUnit> CreateBuilder() =>
            new LengthBuilder();
    }
}
