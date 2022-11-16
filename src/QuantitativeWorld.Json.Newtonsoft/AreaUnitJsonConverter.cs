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
    public sealed class AreaUnitJsonConverter : LinearNamedUnitJsonConverterBase<AreaUnit>
    {
        private readonly Dictionary<string, AreaUnit> _predefinedUnits;

        public AreaUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull,
            TryParseDelegate<AreaUnit> tryReadCustomPredefinedUnit = null)
            : base(serializationFormat, tryReadCustomPredefinedUnit)
        {
            _predefinedUnits = AreaUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(AreaUnit.ValueInSquareMetres);
        protected override ILinearNamedUnitBuilder<AreaUnit> CreateBuilder() =>
            new AreaUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out AreaUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, AreaUnit value, JsonSerializer serializer)
        {
            if (_predefinedUnits.TryGetValue(value.Abbreviation, out var _))
            {
                writer.WriteValue(value.Abbreviation);
                return true;
            }

            return base.TryWritePredefinedUnit(writer, value, serializer);
        }
    }
}
