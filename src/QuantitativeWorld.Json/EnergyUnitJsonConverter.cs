using System.Text.Json;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class EnergyUnitJsonConverter : LinearNamedUnitJsonConverterBase<EnergyUnit>
{
    public EnergyUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<EnergyUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: EnergyUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(EnergyUnit.ValueInJoules);

    protected override ILinearNamedUnitBuilder<EnergyUnit> CreateBuilder()
        => new EnergyUnitBuilder();

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, EnergyUnit value,
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