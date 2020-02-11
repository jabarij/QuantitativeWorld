using Newtonsoft.Json;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Text.Json
{
    public abstract class QuantityJsonConverter<TQuantity, TUnit> : JsonConverter<TQuantity>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : struct, ILinearUnit
    {
        private readonly QuantityJsonSerializationFormat _serializationFormat;

        public QuantityJsonConverter(
            QuantityJsonSerializationFormat serializationFormat = QuantityJsonSerializationFormat.AsBaseValueWithUnit)
        {
            _serializationFormat = serializationFormat;
        }

        protected abstract string BaseValuePropertyName { get; }

        public override TQuantity ReadJson(JsonReader reader, Type objectType, TQuantity existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                return default(TQuantity);

            var builder = CreateQuantityBuilder();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TryReadPropertyAs(BaseValuePropertyName, serializer, e => e.ReadAsDecimal(), out var baseValue))
                    builder.SetBaseValue(baseValue);
                else if (reader.TryDeserializeProperty(nameof(ILinearQuantity<TUnit>.Unit), serializer, out TUnit unit))
                    builder.SetUnit(unit);
                else if (reader.TryReadPropertyAs(nameof(ILinearQuantity<TUnit>.Value), serializer, e => e.ReadAsDecimal(), out var value))
                    builder.SetValue(value);
            }

            if (!builder.TryBuild(out var quantity))
                throw new InvalidOperationException($"Could not read {typeof(TQuantity)} from JSON.");

            return quantity;
        }

        public override void WriteJson(JsonWriter writer, TQuantity value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            switch (_serializationFormat)
            {
                case QuantityJsonSerializationFormat.AsBaseValue:
                    writer.WritePropertyName(BaseValuePropertyName);
                    writer.WriteValue(value.BaseValue);
                    break;
                case QuantityJsonSerializationFormat.AsBaseValueWithUnit:
                    writer.WritePropertyName(BaseValuePropertyName);
                    writer.WriteValue(value.BaseValue);
                    writer.WritePropertyName(nameof(ILinearQuantity<TUnit>.Unit));
                    serializer.Serialize(writer, value.Unit);
                    break;
                case QuantityJsonSerializationFormat.AsValueWithUnit:
                    writer.WritePropertyName(nameof(ILinearQuantity<TUnit>.Value));
                    writer.WriteValue(value.Value);
                    writer.WritePropertyName(nameof(ILinearQuantity<TUnit>.Unit));
                    serializer.Serialize(writer, value.Unit);
                    break;
                default:
                    throw new InvalidOperationException($"Handling {_serializationFormat.GetType().FullName}.{_serializationFormat} is not implemented.");
            }

            writer.WriteEndObject();
        }

        protected abstract ILinearQuantityBuilder<TQuantity, TUnit> CreateQuantityBuilder();
    }
}
