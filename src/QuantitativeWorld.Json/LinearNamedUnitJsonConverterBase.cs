using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Buffers;

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
    private readonly TryParseDelegate<TUnit> _tryReadCustomPredefinedUnit;

    protected LinearNamedUnitJsonConverterBase(
        LinearUnitJsonSerializationFormat serializationFormat,
        TryParseDelegate<TUnit> tryReadCustomPredefinedUnit)
    {
        _serializationFormat = serializationFormat;
        _tryReadCustomPredefinedUnit =
            tryReadCustomPredefinedUnit
            ?? TryReadCustomPredefinedUnit;
    }

    protected abstract string ValueInBaseUnitPropertyName { get; }

    public override TUnit Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
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
                    continue;
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

    protected virtual bool TryReadPredefinedUnit(string value, out TUnit predefinedUnit)
        => _tryReadCustomPredefinedUnit(value, out predefinedUnit);

    protected virtual bool TryWritePredefinedUnit(Utf8JsonWriter writer, TUnit value, JsonSerializerOptions options)
        => false;

    protected abstract ILinearNamedUnitBuilder<TUnit> CreateBuilder();

    private static bool TryReadCustomPredefinedUnit(string value, out TUnit predefinedUnit)
    {
        predefinedUnit = default;
        return false;
    }
}