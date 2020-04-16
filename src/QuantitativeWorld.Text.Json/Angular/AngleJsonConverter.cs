using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Text.Json.Angular
{
    public sealed class AngleJsonConverter : LinearQuantityJsonConverterBase<Angle, AngleUnit>
    {
        public AngleJsonConverter(
            AngleJsonSerializationFormat serializationFormat = AngleJsonSerializationFormat.AsTurnsWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Angle.Turns);
        protected override ILinearQuantityBuilder<Angle, AngleUnit> CreateBuilder() =>
            new AngleBuilder();
    }
}
