namespace QuantitativeWorld.Text.Json
{
    public class LengthJsonConverter : QuantityJsonConverter<Length, LengthUnit>
    {
        public LengthJsonConverter(
            LengthJsonSerializationFormat serializationFormat = LengthJsonSerializationFormat.AsMetresWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Length.Metres);
        protected override ILinearQuantityBuilder<Length, LengthUnit> CreateQuantityBuilder() =>
            new LengthBuilder();
    }
}
