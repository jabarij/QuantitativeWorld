using System.Text.Json;
using System.Text.Json.Serialization;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class TimeJsonConverter : JsonConverter<Time>
{
    private readonly TimeJsonSerializationFormat _serializationFormat;

    public TimeJsonConverter(TimeJsonSerializationFormat serializationFormat = TimeJsonSerializationFormat.Short)
    {
        _serializationFormat = serializationFormat;
    }


    public override Time Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var builder = new TimeBuilder();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, nameof(Time.TotalSeconds), JsonFunctions.GetNumber, out var totalSeconds))
                {
                    builder.TotalSeconds = totalSeconds;
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(Time.Hours), JsonFunctions.GetInt32, out var hours))
                {
                    builder.Hours = hours;
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(Time.Minutes), JsonFunctions.GetInt32, out var minutes))
                {
                    builder.Minutes = minutes;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(Time.Seconds), JsonFunctions.GetNumber, out var seconds))
                {
                    builder.Seconds = seconds;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(Time.IsNegative), JsonFunctions.GetBoolean, out var isNegative))
                {
                    builder.IsNegative = isNegative;
                }
            }
        }

        if (!builder.TryBuild(out var result))
            throw new InvalidOperationException($"Could not read {typeof(Time)} from JSON.");

        return result;
    }

    public override void Write(Utf8JsonWriter writer, Time value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        switch (_serializationFormat)
        {
            case TimeJsonSerializationFormat.Short:
                writer.WriteNumber(nameof(Time.TotalSeconds), value.TotalSeconds);
                break;
            case TimeJsonSerializationFormat.Extended:
                writer.WriteNumber(nameof(Time.Hours), value.Hours);
                writer.WriteNumber(nameof(Time.Minutes), value.Minutes);
                writer.WriteNumber(nameof(Time.Seconds), value.Seconds);
                writer.WriteBoolean(nameof(Time.IsNegative), value.IsNegative);
                break;
            default:
                throw new InvalidOperationException(
                    $"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
        }

        writer.WriteEndObject();
    }
}