using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
#else
namespace QuantitativeWorld.Text.Json
{
#endif
    public sealed class LengthUnitJsonConverter : LinearNamedUnitJsonConverterBase<LengthUnit>
    {
        private readonly Dictionary<string, LengthUnit> _predefinedUnits;

        public LengthUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
            TryParseDelegate<LengthUnit> tryReadCustomPredefinedUnit = null)
            : base(serializationFormat, tryReadCustomPredefinedUnit)
        {
            _predefinedUnits = LengthUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(LengthUnit.ValueInMetres);
        protected override ILinearNamedUnitBuilder<LengthUnit> CreateBuilder() =>
            new LengthUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out LengthUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, LengthUnit value, JsonSerializer serializer)
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
