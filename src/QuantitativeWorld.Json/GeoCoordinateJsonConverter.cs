using System.Text.Json;
using System.Text.Json.Serialization;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using number = Decimal;

#else
namespace QuantitativeWorld.Json;

using number = Double;
#endif

public sealed class GeoCoordinateJsonConverter : JsonConverter<GeoCoordinate>
{
    public override GeoCoordinate Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var builder = new GeoCoordinateBuilder();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, nameof(GeoCoordinate.Latitude), JsonFunctions.GetNumber, out var latitude))
                {
                    builder.SetLatitude(latitude);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, nameof(GeoCoordinate.Longitude), JsonFunctions.GetNumber, out var longitude))
                {
                    builder.SetLongitude(longitude);
                }
            }
        }

        if (builder.TryBuild(out var result) is false)
            throw new InvalidOperationException($"Could not read {typeof(GeoCoordinate)} from JSON.");

        return result;
    }

    public override void Write(Utf8JsonWriter writer, GeoCoordinate value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber(nameof(GeoCoordinate.Latitude), value.Latitude.TotalDegrees);
        writer.WriteNumber(nameof(GeoCoordinate.Longitude), value.Longitude.TotalDegrees);
        writer.WriteEndObject();
    }
}