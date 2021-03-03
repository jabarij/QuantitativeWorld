#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json.Angular
{
    using DecimalQuantitativeWorld.Angular;
#else
namespace QuantitativeWorld.Text.Json.Angular
{
    using QuantitativeWorld.Angular;
#endif
    public sealed class AngleJsonConverter : LinearQuantityJsonConverterBase<Angle, AngleUnit>
    {
        public AngleJsonConverter(
            AngleJsonSerializationFormat serializationFormat = AngleJsonSerializationFormat.AsTurnsWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new AngleUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Angle.Turns);
        protected override ILinearQuantityBuilder<Angle, AngleUnit> CreateBuilder() =>
            new AngleBuilder();
    }
}
