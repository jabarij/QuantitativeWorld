using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class PowerUnitJsonConverter : LinearNamedUnitJsonConverterBase<PowerUnit>
{
    private readonly Dictionary<string, PowerUnit> _predefinedUnits;

    public PowerUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<PowerUnit> tryReadCustomPredefinedUnit = null)
        : base(serializationFormat, tryReadCustomPredefinedUnit)
    {
        _predefinedUnits = PowerUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName 
        => nameof(PowerUnit.ValueInWatts);

    protected override ILinearNamedUnitBuilder<PowerUnit> CreateBuilder()
        => new PowerUnitBuilder();

    protected override bool TryReadPredefinedUnit(string value, out PowerUnit predefinedUnit) 
        => _predefinedUnits.TryGetValue(value, out predefinedUnit)
           || base.TryReadPredefinedUnit(value, out predefinedUnit);

    protected override bool TryWritePredefinedUnit(
        Utf8JsonWriter writer,
        PowerUnit value,
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