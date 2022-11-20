using System.Text.Json;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class VolumeUnitJsonConverter : LinearNamedUnitJsonConverterBase<VolumeUnit>
{
    public VolumeUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<VolumeUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: VolumeUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(VolumeUnit.ValueInCubicMetres);

    protected override ILinearNamedUnitBuilder<VolumeUnit> CreateBuilder()
        => new VolumeUnitBuilder();

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, VolumeUnit value,
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