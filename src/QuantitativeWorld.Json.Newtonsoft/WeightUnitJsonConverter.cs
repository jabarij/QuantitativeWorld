using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class WeightUnitJsonConverter : LinearNamedUnitJsonConverterBase<WeightUnit>
    {
        private readonly Dictionary<string, WeightUnit> _predefinedUnits;

        public WeightUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
            TryParseDelegate<WeightUnit> tryReadCustomPredefinedUnit = null)
            : base(serializationFormat, tryReadCustomPredefinedUnit)
        {
            _predefinedUnits = WeightUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(WeightUnit.ValueInKilograms);
        protected override ILinearNamedUnitBuilder<WeightUnit> CreateBuilder() =>
            new WeightUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out WeightUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, WeightUnit value, JsonSerializer serializer)
        {
            if (_predefinedUnits.TryGetValue(value.Abbreviation, out var predefinedUnit))
            {
                writer.WriteValue(value.Abbreviation);
                return true;
            }

            return base.TryWritePredefinedUnit(writer, value, serializer);
        }
    }
}
