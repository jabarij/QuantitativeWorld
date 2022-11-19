using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class EnergyUnitJsonConverter : LinearNamedUnitJsonConverterBase<EnergyUnit>
{
    private readonly Dictionary<string, EnergyUnit> _predefinedUnits;

    public EnergyUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<EnergyUnit> tryReadCustomPredefinedUnit = null)
        : base(serializationFormat, tryReadCustomPredefinedUnit)
    {
        _predefinedUnits = EnergyUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName
        => nameof(EnergyUnit.ValueInJoules);

    protected override ILinearNamedUnitBuilder<EnergyUnit> CreateBuilder()
        => new EnergyUnitBuilder();

    protected override bool TryReadPredefinedUnit(string value, out EnergyUnit predefinedUnit)
        => _predefinedUnits.TryGetValue(value, out predefinedUnit)
           || base.TryReadPredefinedUnit(value, out predefinedUnit);

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, EnergyUnit value,
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