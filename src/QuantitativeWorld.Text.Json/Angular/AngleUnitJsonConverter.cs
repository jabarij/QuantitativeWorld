using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using QuantitativeWorld.Text.Json.Angular;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Json
{
    public class AngleUnitJsonConverter : JsonConverter<AngleUnit>
    {
        private readonly Dictionary<string, AngleUnit> _predefinedUnits;
        private readonly LinearUnitJsonSerializationFormat _serializationFormat;

        public AngleUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull)
        {
            _predefinedUnits = AngleUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
            _serializationFormat = serializationFormat;
        }

        public override AngleUnit ReadJson(JsonReader reader, Type objectType, AngleUnit existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
                return
                    TryReadPredefinedUnit((string)reader.Value, out var predefinedUnit)
                    ? predefinedUnit
                    : throw new InvalidOperationException($"Could not read predefined {typeof(AngleUnit)} from JSON.");

            var builder = new AngleUnitBuilder();
            if (reader.TokenType == JsonToken.StartObject)
            {
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TryReadPropertyAsNullable(nameof(AngleUnit.UnitsPerTurn), serializer, e => e.ReadAsDecimal(), out var baseValue))
                        builder.SetUnitPerTurn(baseValue);
                    else if (reader.TryReadPropertyAs(nameof(AngleUnit.Name), serializer, e => e.ReadAsString(), out var name))
                        builder.SetName(name);
                    else if (reader.TryReadPropertyAs(nameof(AngleUnit.Abbreviation), serializer, e => e.ReadAsString(), out var abbreviation))
                        builder.SetAbbreviation(abbreviation);
                    else if (reader.TryReadPropertyAs(nameof(AngleUnit.Symbol), serializer, e => e.ReadAsString(), out var symbol))
                        builder.SetSymbol(symbol);
                }
            }

            if (!builder.TryBuild(out var quantity))
                throw new InvalidOperationException($"Could not read {typeof(AngleUnit)} from JSON.");

            return quantity;
        }

        public override void WriteJson(JsonWriter writer, AngleUnit value, JsonSerializer serializer)
        {
            if (_serializationFormat == LinearUnitJsonSerializationFormat.PredefinedAsString
                && TryWritePredefinedUnit(writer, value, serializer))
                return;

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(AngleUnit.Name));
            writer.WriteValue(value.Name);
            writer.WritePropertyName(nameof(AngleUnit.Abbreviation));
            writer.WriteValue(value.Abbreviation);
            writer.WritePropertyName(nameof(AngleUnit.Symbol));
            writer.WriteValue(value.Symbol);
            writer.WritePropertyName(nameof(AngleUnit.UnitsPerTurn));
            writer.WriteValue(value.UnitsPerTurn);

            writer.WriteEndObject();
        }

        protected virtual bool TryReadPredefinedUnit(string value, out AngleUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit);
        protected virtual bool TryWritePredefinedUnit(JsonWriter writer, AngleUnit unit, JsonSerializer serializer)
        {
            if (_predefinedUnits.TryGetValue(unit.Abbreviation, out var _))
            {
                writer.WriteValue(unit.Abbreviation);
                return true;
            }

            return false;
        }
    }
}
