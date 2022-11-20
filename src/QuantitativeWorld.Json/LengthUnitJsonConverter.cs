using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class LengthUnitJsonConverter : LinearNamedUnitJsonConverterBase<LengthUnit>
{
    private readonly Dictionary<string, LengthUnit> _predefinedUnits;

    public LengthUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<LengthUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: LengthUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
        _predefinedUnits = LengthUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(LengthUnit.ValueInMetres);

    protected override ILinearNamedUnitBuilder<LengthUnit> CreateBuilder()
        => new LengthUnitBuilder();

    protected override bool TryWritePredefinedUnit(
        Utf8JsonWriter writer,
        LengthUnit value,
        JsonSerializerOptions options)
    {
        if (PredefinedUnits.ContainsKey(value.Abbreviation))
        {
            writer.WriteStringValue(value.Abbreviation);
            return true;
        }

        return base.TryWritePredefinedUnit(writer, value, options);
    }
}