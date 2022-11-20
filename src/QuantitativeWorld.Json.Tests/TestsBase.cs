using System.Text.Json;
using System.Text.Json.Serialization;

#if DECIMAL
using DecimalQuantitativeWorld.TestAbstractions;

namespace DecimalQuantitativeWorld.Json.Tests;
#else
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Json.Tests;
#endif

#if DECIMAL
public class TestsBase : DecimalQuantitativeWorld.TestAbstractions.TestsBase
{
#else
public class TestsBase : QuantitativeWorld.TestAbstractions.TestsBase
{
#endif
    public TestsBase(TestFixture testFixture)
        : base(testFixture)
    {
    }

    protected virtual void Configure(JsonSerializerOptions options)
    {
    }

    protected string Serialize<T>(T value, params JsonConverter[] converters)
    {
        var options = new JsonSerializerOptions();
        Configure(options);

        foreach (var converter in converters)
            options.Converters.Add(converter);

        return JsonSerializer.Serialize(value, options);
    }

    protected T? Deserialize<T>(string json, params JsonConverter[] converters)
    {
        var options = new JsonSerializerOptions();
        Configure(options);

        foreach (var converter in converters)
            options.Converters.Add(converter);

        return JsonSerializer.Deserialize<T>(json, options);
    }
}