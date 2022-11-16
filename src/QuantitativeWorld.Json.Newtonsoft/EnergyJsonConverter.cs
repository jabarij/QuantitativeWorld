#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class EnergyJsonConverter : LinearQuantityJsonConverterBase<Energy, EnergyUnit>
    {
        public EnergyJsonConverter(
            EnergyJsonSerializationFormat serializationFormat = EnergyJsonSerializationFormat.AsJoulesWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new EnergyUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Energy.Joules);
        protected override ILinearQuantityBuilder<Energy, EnergyUnit> CreateBuilder() =>
            new EnergyBuilder();
    }
}
