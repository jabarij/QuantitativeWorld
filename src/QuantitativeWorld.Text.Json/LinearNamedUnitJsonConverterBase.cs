using Newtonsoft.Json;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Text.Json
{
    public abstract class LinearNamedUnitJsonConverterBase<TUnit> : JsonConverter<TUnit>
        where TUnit : ILinearUnit, INamedUnit
    {
        private readonly LinearUnitJsonSerializationFormat _serializationFormat;

        protected LinearNamedUnitJsonConverterBase(LinearUnitJsonSerializationFormat serializationFormat)
        {
            _serializationFormat = serializationFormat;
        }

        protected abstract string ValueInBaseUnitPropertyName { get; }

        public override TUnit ReadJson(JsonReader reader, Type objectType, TUnit existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
                return
                    TryReadPredefinedUnit((string)reader.Value, out var predefinedUnit)
                    ? predefinedUnit
                    : throw new InvalidOperationException($"Could not read predefined {typeof(TUnit)} from JSON.");

            var builder = CreateBuilder();
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(ValueInBaseUnitPropertyName, serializer, e => e.ReadAsDouble(), out var baseValue))
                        builder.SetValueInBaseUnit(baseValue);
                    else if (reader.TryReadPropertyAs(nameof(INamedUnit.Name), serializer, e => e.ReadAsString(), out var name))
                        builder.SetName(name);
                    else if (reader.TryReadPropertyAs(nameof(INamedUnit.Abbreviation), serializer, e => e.ReadAsString(), out var abbreviation))
                        builder.SetAbbreviation(abbreviation);
                }
            }

            if (!builder.TryBuild(out var quantity))
                throw new InvalidOperationException($"Could not read {typeof(TUnit)} from JSON.");

            return quantity;
        }

        public override void WriteJson(JsonWriter writer, TUnit value, JsonSerializer serializer)
        {
            if (_serializationFormat == LinearUnitJsonSerializationFormat.PredefinedAsString
                && TryWritePredefinedUnit(writer, value, serializer))
                return;

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(INamedUnit.Name));
            writer.WriteValue(value.Name);
            writer.WritePropertyName(nameof(INamedUnit.Abbreviation));
            writer.WriteValue(value.Abbreviation);
            writer.WritePropertyName(ValueInBaseUnitPropertyName);
            writer.WriteValue(value.ValueInBaseUnit);

            writer.WriteEndObject();
        }

        protected virtual bool TryReadPredefinedUnit(string value, out TUnit predefinedUnit)
        {
            predefinedUnit = default(TUnit);
            return false;
        }
        protected virtual bool TryWritePredefinedUnit(JsonWriter writer, TUnit value, JsonSerializer serializer) =>
            false;

        protected abstract ILinearNamedUnitBuilder<TUnit> CreateBuilder();
    }
}
