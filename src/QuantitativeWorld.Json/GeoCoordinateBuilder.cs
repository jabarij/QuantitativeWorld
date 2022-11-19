#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using number = System.Decimal;
#else
namespace QuantitativeWorld.Json;

using number = System.Double;
#endif

internal struct GeoCoordinateBuilder
{
    private number? _longitude;
    private number? _latitude;

    public GeoCoordinateBuilder(GeoCoordinate coordinate)
    {
        _longitude = coordinate.Longitude.TotalDegrees;
        _latitude = coordinate.Latitude.TotalDegrees;
    }

    public void SetLongitude(number longitude)
        => _longitude = longitude;

    public void SetLatitude(number latitude)
        => _latitude = latitude;

    public bool TryBuild(out GeoCoordinate result)
    {
        number? latitude = _latitude;
        number? longitude = _longitude;
        if (latitude.HasValue && longitude.HasValue)
        {
            result = new GeoCoordinate(latitude.Value, longitude.Value);
            return true;
        }

        result = default;
        return false;
    }
}