#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class LengthJsonConverter : LinearQuantityJsonConverterBase<Length, LengthUnit>
    {
        public LengthJsonConverter(
            LengthJsonSerializationFormat serializationFormat = LengthJsonSerializationFormat.AsMetresWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new LengthUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Length.Metres);
        protected override ILinearQuantityBuilder<Length, LengthUnit> CreateBuilder() =>
            new LengthBuilder();
    }
}
