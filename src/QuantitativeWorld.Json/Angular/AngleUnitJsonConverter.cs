using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Angular;

using DecimalQuantitativeWorld.Angular;

#else
namespace QuantitativeWorld.Json.Angular;

using QuantitativeWorld.Angular;
#endif

public sealed class AngleUnitJsonConverter : JsonConverter<AngleUnit>
{
    private readonly Dictionary<string, AngleUnit> _predefinedUnits;
    private readonly LinearUnitJsonSerializationFormat _serializationFormat;
    private readonly TryParseDelegate<AngleUnit>? _tryReadCustomPredefinedUnit;

    public AngleUnitJsonConverter(
        LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
        TryParseDelegate<AngleUnit>? tryReadCustomPredefinedUnit = null)
    {
        _predefinedUnits = AngleUnit.GetPredefinedUnits()
            .ToDictionary(e => e.Abbreviation);
        _serializationFormat = serializationFormat;
        _tryReadCustomPredefinedUnit = tryReadCustomPredefinedUnit;
    }

    public override AngleUnit Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return TryReadPredefinedUnit(reader.GetString(), out var predefinedUnit)
                ? predefinedUnit
                : throw new InvalidOperationException(
                    $"Could not read predefined {typeof(AngleUnit)} from JSON.");

        var builder = new AngleUnitBuilder();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade.TryRead(
                        ref reader,
                        nameof(AngleUnit.UnitsPerTurn),
                        JsonFunctions.GetNumber,
                        out var baseValue))
                {
                    builder.SetUnitsPerTurn(baseValue);
                    continue;
                }

                if (ReaderFacade.TryRead(
                        ref reader,
                        nameof(AngleUnit.Name),
                        JsonFunctions.GetString,
                        out var name))
                {
                    builder.SetName(name);
                    continue;
                }

                if (ReaderFacade.TryRead(
                        ref reader,
                        nameof(AngleUnit.Abbreviation),
                        JsonFunctions.GetString,
                        out var abbreviation))
                {
                    builder.SetAbbreviation(abbreviation);
                    continue;
                }

                if (ReaderFacade.TryRead(
                        ref reader,
                        nameof(AngleUnit.Symbol),
                        JsonFunctions.GetString,
                        out var symbol))
                {
                    builder.SetSymbol(symbol);
                    continue;
                }
            }
        }

        if (!builder.TryBuild(out var quantity))
            throw new InvalidOperationException($"Could not read {typeof(AngleUnit)} from JSON.");

        return quantity;
    }

    public override void Write(Utf8JsonWriter writer, AngleUnit value, JsonSerializerOptions options)
    {
        if (_serializationFormat == LinearUnitJsonSerializationFormat.PredefinedAsString
            && TryWritePredefinedUnit(writer, value, options))
            return;

        writer.WriteStartObject();

        writer.WriteString(nameof(AngleUnit.Name), value.Name);
        writer.WriteString(nameof(AngleUnit.Abbreviation), value.Abbreviation);
        writer.WriteString(nameof(AngleUnit.Symbol), value.Symbol);
        writer.WriteNumber(nameof(AngleUnit.UnitsPerTurn), value.UnitsPerTurn);

        writer.WriteEndObject();
    }

    private bool TryReadPredefinedUnit(string? value, out AngleUnit predefinedUnit)
    {
        if (_tryReadCustomPredefinedUnit?.Invoke(value, out predefinedUnit) is true)
            return true;

        if (value is null)
        {
            predefinedUnit = default;
            return false;
        }

        return _predefinedUnits.TryGetValue(value, out predefinedUnit);
    }

    private bool TryReadCustomPredefinedUnit(string? value, [NotNullWhen(true)] out AngleUnit predefinedUnit)
    {
        if (value is not null)
            return _predefinedUnits.TryGetValue(value, out predefinedUnit);

        predefinedUnit = default;
        return false;
    }

    private bool TryWritePredefinedUnit(Utf8JsonWriter writer, AngleUnit unit, JsonSerializerOptions options)
    {
        if (_predefinedUnits.TryGetValue(unit.Abbreviation, out _))
        {
            writer.WriteStringValue(unit.Abbreviation);
            return true;
        }

        return false;
    }
}