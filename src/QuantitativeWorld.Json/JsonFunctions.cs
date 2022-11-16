using System.Text.Json;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public class JsonFunctions
{
#if DECIMAL
    public static decimal GetNumber(ref Utf8JsonReader reader)
        => reader.GetDecimal();
#else
    public static double GetNumber(ref Utf8JsonReader reader)
        => reader.GetDouble();
#endif

    public static string? GetString(ref Utf8JsonReader reader)
        => reader.GetString();

    public static bool GetBoolean(ref Utf8JsonReader reader)
        => reader.GetBoolean();

    public static int GetInt32(ref Utf8JsonReader reader)
        => reader.GetInt32();
}