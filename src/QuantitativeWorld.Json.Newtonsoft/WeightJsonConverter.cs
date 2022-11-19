#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class WeightJsonConverter : LinearQuantityJsonConverterBase<Weight, WeightUnit>
    {
        public WeightJsonConverter(
            WeightJsonSerializationFormat serializationFormat = WeightJsonSerializationFormat.AsKilogramsWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new WeightUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Weight.Kilograms);
        protected override ILinearQuantityBuilder<Weight, WeightUnit> CreateBuilder() =>
            new WeightBuilder();
    }
}
