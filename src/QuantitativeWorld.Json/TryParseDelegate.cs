using System.Diagnostics.CodeAnalysis;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public delegate bool TryParseDelegate<T>(string? str, out T result);