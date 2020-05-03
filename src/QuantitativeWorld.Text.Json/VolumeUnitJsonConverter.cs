using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Json
{
    public sealed class VolumeUnitJsonConverter : LinearNamedUnitJsonConverterBase<VolumeUnit>
    {
        private readonly Dictionary<string, VolumeUnit> _predefinedUnits;

        public VolumeUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull)
            : base(serializationFormat)
        {
            _predefinedUnits = VolumeUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(VolumeUnit.ValueInCubicMetres);
        protected override ILinearNamedUnitBuilder<VolumeUnit> CreateBuilder() =>
            new VolumeUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out VolumeUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, VolumeUnit value, JsonSerializer serializer)
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
