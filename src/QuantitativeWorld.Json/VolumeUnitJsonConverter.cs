using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class VolumeUnitJsonConverter : LinearNamedUnitJsonConverterBase<VolumeUnit>
{
    private readonly Dictionary<string, VolumeUnit> _predefinedUnits;

    public VolumeUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<VolumeUnit> tryReadCustomPredefinedUnit = null)
        : base(serializationFormat, tryReadCustomPredefinedUnit)
    {
        _predefinedUnits = VolumeUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
    }

    protected override string ValueInBaseUnitPropertyName 
        => nameof(VolumeUnit.ValueInCubicMetres);

    protected override ILinearNamedUnitBuilder<VolumeUnit> CreateBuilder()
        => new VolumeUnitBuilder();

    protected override bool TryReadPredefinedUnit(string value, out VolumeUnit predefinedUnit) 
        => _predefinedUnits.TryGetValue(value, out predefinedUnit)
           || base.TryReadPredefinedUnit(value, out predefinedUnit);

    protected override bool TryWritePredefinedUnit(Utf8JsonWriter writer, VolumeUnit value,
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