using Newtonsoft.Json;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using number = Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Angular
{
    using QuantitativeWorld.Angular;
    using number = Double;
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
