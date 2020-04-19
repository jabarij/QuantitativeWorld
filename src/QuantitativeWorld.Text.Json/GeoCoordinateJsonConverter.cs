using Newtonsoft.Json;
using System;

namespace QuantitativeWorld.Text.Json
{
    public sealed class GeoCoordinateJsonConverter : JsonConverter<GeoCoordinate>
    {
        public override GeoCoordinate ReadJson(JsonReader reader, Type objectType, GeoCoordinate existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            double? latitude = null;
            double? longitude = null;
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(GeoCoordinate.Latitude), serializer, e => e.ReadAsDouble(), out var latitudeValue))
                        latitude = latitudeValue;
                    else if (reader.TryReadPropertyAsNullable(nameof(GeoCoordinate.Longitude), serializer, e => e.ReadAsDouble(), out var longitudeValue))
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
            writer.WriteValue(value.Latitude);
            writer.WritePropertyName(nameof(GeoCoordinate.Longitude));
            writer.WriteValue(value.Longitude);
            writer.WriteEndObject();
        }
    }
}
