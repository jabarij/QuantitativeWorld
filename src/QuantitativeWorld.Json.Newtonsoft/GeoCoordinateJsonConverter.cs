using Newtonsoft.Json;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using number = Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using number = Double;
#endif

    public sealed class GeoCoordinateJsonConverter : JsonConverter<GeoCoordinate>
    {
        public override GeoCoordinate ReadJson(JsonReader reader, Type objectType, GeoCoordinate existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            number? latitude = null;
            number? longitude = null;
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(GeoCoordinate.Latitude), serializer, e => e.ReadAsNumber(), out var latitudeValue))
                        latitude = latitudeValue;
                    else if (reader.TryReadPropertyAsNullable(nameof(GeoCoordinate.Longitude), serializer, e => e.ReadAsNumber(), out var longitudeValue))
                        longitude = longitudeValue;

                }
            }

            if (latitude == null
                || longitude == null)
                throw new InvalidOperationException($"Could not read {typeof(GeoCoordinate)} from JSON.");

            return new GeoCoordinate(latitude.Value, longitude.Value);
        }

        public override void WriteJson(JsonWriter writer, GeoCoordinate value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(GeoCoordinate.Latitude));
            writer.WriteValue(value.Latitude.TotalDegrees);
            writer.WritePropertyName(nameof(GeoCoordinate.Longitude));
            writer.WriteValue(value.Longitude.TotalDegrees);
            writer.WriteEndObject();
        }
    }
}
