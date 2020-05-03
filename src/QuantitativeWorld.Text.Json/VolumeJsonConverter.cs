namespace QuantitativeWorld.Text.Json
{
    public sealed class VolumeJsonConverter : LinearQuantityJsonConverterBase<Volume, VolumeUnit>
    {
        public VolumeJsonConverter(
            VolumeJsonSerializationFormat serializationFormat = VolumeJsonSerializationFormat.AsCubicMetresWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Volume.CubicMetres);
        protected override ILinearQuantityBuilder<Volume, VolumeUnit> CreateBuilder() =>
            new VolumeBuilder();
    }
}
