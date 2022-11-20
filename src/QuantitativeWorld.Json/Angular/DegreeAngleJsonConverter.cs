using System.Text.Json.Serialization;
using System;
using System.Text.Json;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Angular;

using DecimalQuantitativeWorld.Angular;

#else
namespace QuantitativeWorld.Json.Angular;

using QuantitativeWorld.Angular;
#endif

public sealed class DegreeAngleJsonConverter : JsonConverter<DegreeAngle>
{
    private readonly DegreeAngleJsonSerializationFormat _serializationFormat;

    public DegreeAngleJsonConverter(
        DegreeAngleJsonSerializationFormat serializationFormat = DegreeAngleJsonSerializationFormat.Short)
    {
        _serializationFormat = serializationFormat;
    }

    public override DegreeAngle Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var builder = new DegreeAngleBuilder();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.TotalSeconds), JsonFunctions.GetNumber,
                        out var totalSeconds))
                {
                    builder.SetTotalSeconds(totalSeconds);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.Circles), JsonFunctions.GetInt32, out var circles))
                {
                    builder.SetCircles(circles);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.Degrees), JsonFunctions.GetInt32, out var degrees))
                {
                    builder.SetDegrees(degrees);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.Minutes), JsonFunctions.GetInt32, out var minutes))
                {
                    builder.SetMinutes(minutes);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.Seconds), JsonFunctions.GetNumber, out var seconds))
                {
                    builder.SetSeconds(seconds);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(DegreeAngle.IsNegative), JsonFunctions.GetBoolean, out var isNegative))
                {
                    builder.SetIsNegative(isNegative);
                    continue;
                }
            }
        }

        if (!builder.TryBuild(out var result))
            throw new InvalidOperationException($"Could not read {typeof(DegreeAngle)} from JSON.");

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DegreeAngle value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        switch (_serializationFormat)
        {
            case DegreeAngleJsonSerializationFormat.Short:
                writer.WriteNumber(nameof(DegreeAngle.TotalSeconds), value.TotalSeconds);
                break;
            case DegreeAngleJsonSerializationFormat.Extended:
                writer.WriteNumber(nameof(DegreeAngle.Circles), value.Circles);
                writer.WriteNumber(nameof(DegreeAngle.Degrees), value.Degrees);
                writer.WriteNumber(nameof(DegreeAngle.Minutes), value.Minutes);
                writer.WriteNumber(nameof(DegreeAngle.Seconds), value.Seconds);
                writer.WriteBoolean(nameof(DegreeAngle.IsNegative), value.IsNegative);
                break;
            default:
                throw new InvalidOperationException(
                    $"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
        }

        writer.WriteEndObject();
    }
}