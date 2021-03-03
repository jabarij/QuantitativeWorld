using Newtonsoft.Json;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public sealed class TimeJsonConverter : JsonConverter<Time>
    {
        private readonly TimeJsonSerializationFormat _serializationFormat;

        public TimeJsonConverter(TimeJsonSerializationFormat serializationFormat = TimeJsonSerializationFormat.Short)
        {
            _serializationFormat = serializationFormat;
        }


        public override Time ReadJson(JsonReader reader, Type objectType, Time existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var builder = new TimeBuilder();
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(Time.TotalSeconds), serializer, e => e.ReadAsNumber(), out var totalSeconds))
                        builder.TotalSeconds = totalSeconds;
                    else if (reader.TryReadPropertyAsNullable(nameof(Time.Hours), serializer, e => e.ReadAsInt32(), out var hours))
                        builder.Hours = hours;
                    else if (reader.TryReadPropertyAsNullable(nameof(Time.Minutes), serializer, e => e.ReadAsInt32(), out var minutes))
                        builder.Minutes = minutes;
                    else if (reader.TryReadPropertyAsNullable(nameof(Time.Seconds), serializer, e => e.ReadAsNumber(), out var seconds))
                        builder.Seconds = seconds;
                    else if (reader.TryReadPropertyAsNullable(nameof(Time.IsNegative), serializer, e => e.ReadAsBoolean(), out var isNegative))
                        builder.IsNegative = isNegative;
                }
            }

            if (!builder.TryBuild(out var result))
                throw new InvalidOperationException($"Could not read {typeof(Time)} from JSON.");

            return result;
        }

        public override void WriteJson(JsonWriter writer, Time value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            switch (_serializationFormat)
            {
                case TimeJsonSerializationFormat.Short:
                    writer.WritePropertyName(nameof(Time.TotalSeconds));
                    writer.WriteValue(value.TotalSeconds);
                    break;
                case TimeJsonSerializationFormat.Extended:
                    writer.WritePropertyName(nameof(Time.Hours));
                    writer.WriteValue(value.Hours);
                    writer.WritePropertyName(nameof(Time.Minutes));
                    writer.WriteValue(value.Minutes);
                    writer.WritePropertyName(nameof(Time.Seconds));
                    writer.WriteValue(value.Seconds);
                    writer.WritePropertyName(nameof(Time.IsNegative));
                    writer.WriteValue(value.IsNegative);
                    break;
                default:
                    throw new InvalidOperationException($"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
            }
            writer.WriteEndObject();
        }
    }
}
