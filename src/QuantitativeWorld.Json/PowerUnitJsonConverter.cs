using System.Text.Json;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class PowerUnitJsonConverter : LinearNamedUnitJsonConverterBase<PowerUnit>
{
    public PowerUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<PowerUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: PowerUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(PowerUnit.ValueInWatts);

    protected override ILinearNamedUnitBuilder<PowerUnit> CreateBuilder()
        => new PowerUnitBuilder();

    protected override bool TryWritePredefinedUnit(
        Utf8JsonWriter writer,
        PowerUnit value,
        JsonSerializerOptions options)
    {
        if (PredefinedUnits.TryGetValue(value.Abbreviation, out _))
        {
            writer.WriteStringValue(value.Abbreviation);
            return true;
        }

        return base.TryWritePredefinedUnit(writer, value, options);
    }
}