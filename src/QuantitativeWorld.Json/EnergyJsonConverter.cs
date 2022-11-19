#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class EnergyJsonConverter : LinearQuantityJsonConverterBase<Energy, EnergyUnit>
{
    public EnergyJsonConverter(
        EnergyJsonSerializationFormat serializationFormat = EnergyJsonSerializationFormat.AsJoulesWithUnit)
        : base(serializationFormat: (QuantityJsonSerializationFormat) serializationFormat)
    {
    }

    protected override string BaseValuePropertyName
        => nameof(Energy.Joules);

    protected override ILinearQuantityBuilder<Energy, EnergyUnit> CreateBuilder()
        => new EnergyBuilder();
}