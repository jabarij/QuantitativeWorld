using System.Text.Json;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class WeightUnitJsonConverter : LinearNamedUnitJsonConverterBase<WeightUnit>
{
    public WeightUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<WeightUnit>? tryReadCustomPredefinedUnit = null)
        : base(
            serializationFormat,
            tryReadCustomPredefinedUnit,
            predefinedUnits: WeightUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation)
        )
    {
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(WeightUnit.ValueInKilograms);

    protected override ILinearNamedUnitBuilder<WeightUnit> CreateBuilder()
        => new WeightUnitBuilder();

    protected override bool TryWritePredefinedUnit(
        Utf8JsonWriter writer,
        WeightUnit value,
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