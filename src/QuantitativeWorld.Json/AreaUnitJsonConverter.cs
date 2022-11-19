using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class AreaUnitJsonConverter : LinearNamedUnitJsonConverterBase<AreaUnit>
{
    private readonly Dictionary<string, AreaUnit> _predefinedUnits;

    public AreaUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<AreaUnit> tryReadCustomPredefinedUnit = null)
        : base(serializationFormat, tryReadCustomPredefinedUnit)
    {
        _predefinedUnits = AreaUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName 
        => nameof(AreaUnit.ValueInSquareMetres);

    protected override ILinearNamedUnitBuilder<AreaUnit> CreateBuilder()
        => new AreaUnitBuilder();

    protected override bool TryReadPredefinedUnit(string value, out AreaUnit predefinedUnit)
    {
        if (_predefinedUnits.TryGetValue(value, out var unit))
        {
            predefinedUnit = unit;
            return true;
        }

        return base.TryReadPredefinedUnit(value, out predefinedUnit);
    }

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, AreaUnit value,
        JsonSerializerOptions options)
    {
        if (_predefinedUnits.ContainsKey(value.Abbreviation))
        {
            writer.WriteStringValue(value.Abbreviation);
            return true;
        }

        return base.TryWritePredefinedUnit(writer, value, options);
    }
}