#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class SpeedJsonConverter : LinearQuantityJsonConverterBase<Speed, SpeedUnit>
{
    public SpeedJsonConverter(
        SpeedJsonSerializationFormat serializationFormat = SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit)
        : base(serializationFormat: (QuantityJsonSerializationFormat) serializationFormat)
    {
    }

    protected override string BaseValuePropertyName
        => nameof(Speed.MetresPerSecond);

    protected override ILinearQuantityBuilder<Speed, SpeedUnit> CreateBuilder()
        => new SpeedBuilder();
}