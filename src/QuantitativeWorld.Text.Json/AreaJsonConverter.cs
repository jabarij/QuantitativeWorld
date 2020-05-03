namespace QuantitativeWorld.Text.Json
{
    public sealed class AreaJsonConverter : LinearQuantityJsonConverterBase<Area, AreaUnit>
    {
        public AreaJsonConverter(
            AreaJsonSerializationFormat serializationFormat = AreaJsonSerializationFormat.AsSquareMetresWithUnit)
            : base(serializationFormat: (QuantityJsonSerializationFormat)serializationFormat) { }

        protected override string BaseValuePropertyName =>
            nameof(Area.SquareMetres);
        protected override ILinearQuantityBuilder<Area, AreaUnit> CreateBuilder() =>
            new AreaBuilder();
    }
}
