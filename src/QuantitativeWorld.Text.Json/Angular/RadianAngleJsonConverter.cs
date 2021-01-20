using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using System;

namespace QuantitativeWorld.Text.Json.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public sealed class RadianAngleJsonConverter : JsonConverter<RadianAngle>
    {
        public override RadianAngle ReadJson(JsonReader reader, Type objectType, RadianAngle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            number? radians = null;
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(RadianAngle.Radians), serializer, e => e.ReadAsNumber(), out var value))
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
