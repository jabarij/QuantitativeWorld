#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class AreaJsonConverter : LinearQuantityJsonConverterBase<Area, AreaUnit>
    {
        public AreaJsonConverter(
            AreaJsonSerializationFormat serializationFormat = AreaJsonSerializationFormat.AsSquareMetresWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new AreaUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Area.SquareMetres);
        protected override ILinearQuantityBuilder<Area, AreaUnit> CreateBuilder() =>
            new AreaBuilder();
    }
}
