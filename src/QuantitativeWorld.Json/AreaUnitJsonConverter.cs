using System.Text.Json;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class AreaUnitJsonConverter : LinearNamedUnitJsonConverterBase<AreaUnit>
{
    public AreaUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<AreaUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: AreaUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(AreaUnit.ValueInSquareMetres);

    protected override ILinearNamedUnitBuilder<AreaUnit> CreateBuilder()
        => new AreaUnitBuilder();

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, AreaUnit value, JsonSerializerOptions options)
    {
        if (PredefinedUnits.ContainsKey(value.Abbreviation))
        {
            writer.WriteStringValue(value.Abbreviation);
            return true;
        }

        return base.TryWritePredefinedUnit(writer, value, options);
    }
}