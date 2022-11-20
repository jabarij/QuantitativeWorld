using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using DecimalQuantitativeWorld.Interfaces;

#else
namespace QuantitativeWorld.Json;

using QuantitativeWorld.Interfaces;
#endif

public abstract class LinearNamedUnitJsonConverterBase<TUnit> : JsonConverter<TUnit>
    where TUnit : ILinearUnit, INamedUnit
{
    private readonly LinearUnitJsonSerializationFormat _serializationFormat;
    private readonly TryParseDelegate<TUnit>? _tryReadCustomPredefinedUnit;
    protected readonly IReadOnlyDictionary<string, TUnit> PredefinedUnits;

    protected LinearNamedUnitJsonConverterBase(
        LinearUnitJsonSerializationFormat serializationFormat,
        TryParseDelegate<TUnit>? tryReadCustomPredefinedUnit,
        IReadOnlyDictionary<string, TUnit> predefinedUnits)
    {
        _serializationFormat = serializationFormat;
        _tryReadCustomPredefinedUnit = tryReadCustomPredefinedUnit;
        PredefinedUnits = predefinedUnits;
    }

    protected abstract string ValueInBaseUnitPropertyName { get; }

    public override TUnit? Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return TryReadPredefinedUnit(reader.GetString(), out var predefinedUnit)
                ? predefinedUnit
                : throw new InvalidOperationException($"Could not read predefined {typeof(TUnit)} from JSON.");

        const string nameProperty = nameof(INamedUnit.Name);
        const string abbreviationProperty = nameof(INamedUnit.Abbreviation);

        var builder = CreateBuilder();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, ValueInBaseUnitPropertyName, JsonFunctions.GetNumber, out var baseValue))
                {
                    builder.SetValueInBaseUnit(baseValue);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameProperty, JsonFunctions.GetString, out var name))
                {
                    builder.SetName(name);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, abbreviationProperty, JsonFunctions.GetString, out var abbreviation))
                {
                    builder.SetAbbreviation(abbreviation);
                }
            }
        }

        if (!builder.TryBuild(out var quantity))
            throw new InvalidOperationException($"Could not read {typeof(TUnit)} from JSON.");

        return quantity;
    }

    public override void Write(Utf8JsonWriter writer, TUnit value, JsonSerializerOptions options)
    {
        if (_serializationFormat == LinearUnitJsonSerializationFormat.PredefinedAsString
            && TryWritePredefinedUnit(writer, value, options))
            return;

        writer.WriteStartObject();

        writer.WriteString(nameof(INamedUnit.Name), value.Name);
        writer.WriteString(nameof(INamedUnit.Abbreviation), value.Abbreviation);
        writer.WriteNumber(ValueInBaseUnitPropertyName, value.ValueInBaseUnit);

        writer.WriteEndObject();
    }

    private bool TryReadPredefinedUnit(string? value, [NotNullWhen(true)] out TUnit? predefinedUnit)
    {
        if (_tryReadCustomPredefinedUnit is not null)
            return _tryReadCustomPredefinedUnit(value, out predefinedUnit)
                || TryReadCustomPredefinedUnit(value, out predefinedUnit);

        return TryReadCustomPredefinedUnit(value, out predefinedUnit);
    }

    protected virtual bool TryWritePredefinedUnit(Utf8JsonWriter writer, TUnit value, JsonSerializerOptions options)
        => false;

    protected abstract ILinearNamedUnitBuilder<TUnit> CreateBuilder();

    private bool TryReadCustomPredefinedUnit(string? value, [NotNullWhen(true)] out TUnit? predefinedUnit)
    {
        if (value is not null)
            return PredefinedUnits.TryGetValue(value, out predefinedUnit);

        predefinedUnit = default;
        return false;
    }
}