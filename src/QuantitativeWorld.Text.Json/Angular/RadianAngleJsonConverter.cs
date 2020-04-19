using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using System;

namespace QuantitativeWorld.Text.Json.Angular
{
    public sealed class RadianAngleJsonConverter : JsonConverter<RadianAngle>
    {
        public override RadianAngle ReadJson(JsonReader reader, Type objectType, RadianAngle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            double? radians = null;
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(RadianAngle.Radians), serializer, e => e.ReadAsDouble(), out var value))
                        radians = value;
                }
            }

            if (radians == null)
                throw new InvalidOperationException($"Could not read {typeof(RadianAngle)} from JSON.");

            return new RadianAngle(radians.Value);
        }

        public override void WriteJson(JsonWriter writer, RadianAngle value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(RadianAngle.Radians));
            writer.WriteValue(value.Radians);
            writer.WriteEndObject();
        }
    }
}
