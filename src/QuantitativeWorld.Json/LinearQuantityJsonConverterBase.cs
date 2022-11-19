using System.Text.Json.Serialization;
using System;
using System.Text.Json;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using DecimalQuantitativeWorld.Interfaces;

#else
namespace QuantitativeWorld.Json;

using QuantitativeWorld.Interfaces;
#endif

public abstract class LinearQuantityJsonConverterBase<TQuantity, TUnit> : JsonConverter<TQuantity>
    where TQuantity : ILinearQuantity<TUnit>
    where TUnit : struct, ILinearUnit
{
    private readonly QuantityJsonSerializationFormat _serializationFormat;

    public LinearQuantityJsonConverterBase(
        QuantityJsonSerializationFormat serializationFormat = QuantityJsonSerializationFormat.AsBaseValueWithUnit)
    {
        _serializationFormat = serializationFormat;
    }

    protected abstract string BaseValuePropertyName { get; }

    public override TQuantity Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var builder = CreateBuilder();

        const string valuePropertyName = nameof(ILinearQuantity<TUnit>.Value);
        const string unitPropertyName = nameof(ILinearQuantity<TUnit>.Unit);

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (ReaderFacade
                    .TryRead(ref reader, BaseValuePropertyName, JsonFunctions.GetNumber, out var baseValue))
                {
                    builder.SetBaseValue(baseValue);
                    continue;
                }

                if (ReaderFacade
                    .TryDeserialize(ref reader, unitPropertyName, options, out TUnit unit))
                {
                    builder.SetUnit(unit);
                    continue;
                }

                if (ReaderFacade
                    .TryRead(ref reader, valuePropertyName, JsonFunctions.GetNumber, out var value))
                {
                    builder.SetValue(value);
                    continue;
                }
            }
        }

        if (!builder.TryBuild(out var quantity))
            throw new InvalidOperationException($"Could not read {typeof(TQuantity)} from JSON.");

        return quantity;
    }

    public override void Write(Utf8JsonWriter writer, TQuantity value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        switch (_serializationFormat)
        {
            case QuantityJsonSerializationFormat.AsBaseValue:
                writer.WriteNumber(BaseValuePropertyName, value.BaseValue);
                break;
            case QuantityJsonSerializationFormat.AsBaseValueWithUnit:
                writer.WriteNumber(BaseValuePropertyName, value.BaseValue);
                writer.WritePropertyName(nameof(ILinearQuantity<TUnit>.Unit));
                JsonSerializer.Serialize(writer, value.Unit, options);
                break;
            case QuantityJsonSerializationFormat.AsValueWithUnit:
                writer.WriteNumber(nameof(ILinearQuantity<TUnit>.Value), value.Value);
                writer.WritePropertyName(nameof(ILinearQuantity<TUnit>.Unit));
                JsonSerializer.Serialize(writer, value.Unit, options);
                break;
            default:
                throw new InvalidOperationException(
                    $"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
        }

        writer.WriteEndObject();
    }

    protected abstract ILinearQuantityBuilder<TQuantity, TUnit> CreateBuilder();
}