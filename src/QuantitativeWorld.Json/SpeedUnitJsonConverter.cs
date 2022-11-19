using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class SpeedUnitJsonConverter : LinearNamedUnitJsonConverterBase<SpeedUnit>
{
    private readonly Dictionary<string, SpeedUnit> _predefinedUnits;

    public SpeedUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<SpeedUnit> tryReadCustomPredefinedUnit = null)
        : base(serializationFormat, tryReadCustomPredefinedUnit)
    {
        _predefinedUnits = SpeedUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(SpeedUnit.ValueInMetresPerSecond);

    protected override ILinearNamedUnitBuilder<SpeedUnit> CreateBuilder()
        => new SpeedUnitBuilder();

    protected override bool TryReadPredefinedUnit(string value, out SpeedUnit predefinedUnit)
        => _predefinedUnits.TryGetValue(value, out predefinedUnit)
           || base.TryReadPredefinedUnit(value, out predefinedUnit);

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, SpeedUnit value,
        JsonSerializerOptions options)
    {
        if (_predefinedUnits.TryGetValue(value.Abbreviation, out _))
        {
            writer.WriteStringValue(value.Abbreviation);
            return true;
        }

        return base.TryWritePredefinedUnit(writer, value, options);
    }
}