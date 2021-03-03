using Newtonsoft.Json;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json.Angular
{
    using DecimalQuantitativeWorld.Angular;
#else
namespace QuantitativeWorld.Text.Json.Angular
{
    using QuantitativeWorld.Angular;
#endif
    public sealed class DegreeAngleJsonConverter : JsonConverter<DegreeAngle>
    {
        private readonly DegreeAngleJsonSerializationFormat _serializationFormat;

        public DegreeAngleJsonConverter(DegreeAngleJsonSerializationFormat serializationFormat = DegreeAngleJsonSerializationFormat.Short)
        {
            _serializationFormat = serializationFormat;
        }


        public override DegreeAngle ReadJson(JsonReader reader, Type objectType, DegreeAngle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var builder = new DegreeAngleBuilder();
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.TotalSeconds), serializer, e => e.ReadAsNumber(), out var totalSeconds))
                        builder.TotalSeconds = totalSeconds;
                    else if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.Circles), serializer, e => e.ReadAsInt32(), out var circles))
                        builder.Circles = circles;
                    else if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.Degrees), serializer, e => e.ReadAsInt32(), out var degrees))
                        builder.Degrees = degrees;
                    else if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.Minutes), serializer, e => e.ReadAsInt32(), out var minutes))
                        builder.Minutes = minutes;
                    else if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.Seconds), serializer, e => e.ReadAsNumber(), out var seconds))
                        builder.Seconds = seconds;
                    else if (reader.TryReadPropertyAsNullable(nameof(DegreeAngle.IsNegative), serializer, e => e.ReadAsBoolean(), out var isNegative))
                        builder.IsNegative = isNegative;
                }
            }

            if (!builder.TryBuild(out var result))
                throw new InvalidOperationException($"Could not read {typeof(DegreeAngle)} from JSON.");

            return result;
        }

        public override void WriteJson(JsonWriter writer, DegreeAngle value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            switch (_serializationFormat)
            {
                case DegreeAngleJsonSerializationFormat.Short:
                    writer.WritePropertyName(nameof(DegreeAngle.TotalSeconds));
                    writer.WriteValue(value.TotalSeconds);
                    break;
                case DegreeAngleJsonSerializationFormat.Extended:
                    writer.WritePropertyName(nameof(DegreeAngle.Circles));
                    writer.WriteValue(value.Circles);
                    writer.WritePropertyName(nameof(DegreeAngle.Degrees));
                    writer.WriteValue(value.Degrees);
                    writer.WritePropertyName(nameof(DegreeAngle.Minutes));
                    writer.WriteValue(value.Minutes);
                    writer.WritePropertyName(nameof(DegreeAngle.Seconds));
                    writer.WriteValue(value.Seconds);
                    writer.WritePropertyName(nameof(DegreeAngle.IsNegative));
                    writer.WriteValue(value.IsNegative);
                    break;
                default:
                    throw new InvalidOperationException($"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
            }
            writer.WriteEndObject();
        }
    }
}
