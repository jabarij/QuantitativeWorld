using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

internal static class ReaderFacade
{
    public static bool IsPropertyName(ref Utf8JsonReader reader, string propertyName)
        => reader.TokenType == JsonTokenType.PropertyName
           && reader.ValueTextEquals(Encoding.UTF8.GetBytes(propertyName));

    public delegate T? ReadNullable<T>(ref Utf8JsonReader reader) where T : struct;

    public delegate T Read<out T>(ref Utf8JsonReader reader);

    public static bool TryReadNullable<T>(ref Utf8JsonReader reader, string name, ReadNullable<T> read, out T result)
        where T : struct
    {
        if (IsPropertyName(ref reader, name) && reader.Read())
        {
            var value = read(ref reader);
            result = value ?? default(T);
            return value != null;
        }

        result = default;
        return false;
    }

    public static bool TryRead<T>(
        ref Utf8JsonReader reader,
        string name,
        Read<T> read,
        [NotNullWhen(true)] out T? result)
    {
        if (IsPropertyName(ref reader, name) && reader.Read())
        {
            result = read(ref reader);
            return result is not null;
        }

        result = default;
        return false;
    }

    public static bool TryDeserialize<T>(
        ref Utf8JsonReader reader,
        string name,
        JsonSerializerOptions options,
        [NotNullWhen(true)]out T? result)
    {
        if (IsPropertyName(ref reader, name) && reader.Read())
        {
            result = JsonSerializer.Deserialize<T>(ref reader, options);
            return result is not null;
        }

        result = default;
        return false;
    }
}