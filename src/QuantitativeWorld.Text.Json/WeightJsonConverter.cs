namespace QuantitativeWorld.Text.Json
{
    public class WeightJsonConverter : QuantityJsonConverter<Weight, WeightUnit>
    {
        public WeightJsonConverter(
            WeightJsonSerializationFormat serializationFormat = WeightJsonSerializationFormat.AsKilogramsWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Weight.Kilograms);
        protected override ILinearQuantityBuilder<Weight, WeightUnit> CreateQuantityBuilder() =>
            new WeightBuilder();
    }
}
