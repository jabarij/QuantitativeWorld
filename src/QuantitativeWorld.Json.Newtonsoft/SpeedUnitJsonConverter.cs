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
    public sealed class SpeedUnitJsonConverter : LinearNamedUnitJsonConverterBase<SpeedUnit>
    {
        private readonly Dictionary<string, SpeedUnit> _predefinedUnits;

        public SpeedUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
            TryParseDelegate<SpeedUnit> tryReadCustomPredefinedUnit = null)
            : base(serializationFormat, tryReadCustomPredefinedUnit)
        {
            _predefinedUnits = SpeedUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(SpeedUnit.ValueInMetresPerSecond);
        protected override ILinearNamedUnitBuilder<SpeedUnit> CreateBuilder() =>
            new SpeedUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out SpeedUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, SpeedUnit value, JsonSerializer serializer)
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
