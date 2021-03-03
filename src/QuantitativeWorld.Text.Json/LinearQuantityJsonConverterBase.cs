using Newtonsoft.Json;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using DecimalQuantitativeWorld.Interfaces;
#else
namespace QuantitativeWorld.Text.Json
{
    using QuantitativeWorld.Interfaces;
#endif
    public abstract class LinearQuantityJsonConverterBase<TQuantity, TUnit> : JsonConverter<TQuantity>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : struct, ILinearUnit
    {
        private readonly QuantityJsonSerializationFormat _serializationFormat;
        private readonly JsonConverter<TUnit> _unitConverter;

        public LinearQuantityJsonConverterBase(
            JsonConverter<TUnit> unitConverter,
            QuantityJsonSerializationFormat serializationFormat = QuantityJsonSerializationFormat.AsBaseValueWithUnit)
        {
            _unitConverter = unitConverter;
            _serializationFormat = serializationFormat;
        }

        protected abstract string BaseValuePropertyName { get; }

        public override TQuantity ReadJson(JsonReader reader, Type objectType, TQuantity existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            EnsureUnitConverter(serializer);

            var builder = CreateBuilder();
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(BaseValuePropertyName, serializer, e => e.ReadAsNumber(), out var baseValue))
                        builder.SetBaseValue(baseValue);
                    else if (reader.TryDeserializeProperty(nameof(ILinearQuantity<TUnit>.Unit), serializer, out TUnit unit))
                        builder.SetUnit(unit);
                    else if (reader.TryReadPropertyAsNullable(nameof(ILinearQuantity<TUnit>.Value), serializer, e => e.ReadAsNumber(), out var value))
                        builder.SetValue(value);
                }
            }

            if (!builder.TryBuild(out var quantity))
                throw new InvalidOperationException($"Could not read {typeof(TQuantity)} from JSON.");

            return quantity;
        }

        public override void WriteJson(JsonWriter writer, TQuantity value, JsonSerializer serializer)
        {
            EnsureUnitConverter(serializer);

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

        protected abstract ILinearQuantityBuilder<TQuantity, TUnit> CreateBuilder();

        private void EnsureUnitConverter(JsonSerializer serializer)
        {
            if (_unitConverter != null && !serializer.Converters.Contains(_unitConverter))
                serializer.Converters.Add(_unitConverter);
        }
    }
}
