using System.Text.Json;
using System.Text.Json.Serialization;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Angular;

using DecimalQuantitativeWorld.Angular;
using number = Decimal;

#else
namespace QuantitativeWorld.Json.Angular;

using QuantitativeWorld.Angular;
using number = Double;
#endif

public sealed class RadianAngleJsonConverter : JsonConverter<RadianAngle>
{
    public override RadianAngle Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        number? radians = null;
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, nameof(RadianAngle.Radians), JsonFunctions.GetNumber, out var value))
                    radians = value;
            }
        }

        if (radians is null)
            throw new InvalidOperationException($"Could not read {typeof(RadianAngle)} from JSON.");

        return new RadianAngle(radians.Value);
    }

    public override void Write(Utf8JsonWriter writer, RadianAngle value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber(nameof(RadianAngle.Radians), value.Radians);
        writer.WriteEndObject();
    }
}