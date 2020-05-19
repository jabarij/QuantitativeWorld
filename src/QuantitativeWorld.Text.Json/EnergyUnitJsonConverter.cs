﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Json
{
    public sealed class EnergyUnitJsonConverter : LinearNamedUnitJsonConverterBase<EnergyUnit>
    {
        private readonly Dictionary<string, EnergyUnit> _predefinedUnits;

        public EnergyUnitJsonConverter(
            LinearUnitJsonSerializationFormat serializationFormat = LinearUnitJsonSerializationFormat.AlwaysFull)
            : base(serializationFormat)
        {
            _predefinedUnits = EnergyUnit.GetPredefinedUnits()
                .ToDictionary(e => e.Abbreviation);
        }

        protected override string ValueInBaseUnitPropertyName =>
            nameof(EnergyUnit.ValueInJoules);
        protected override ILinearNamedUnitBuilder<EnergyUnit> CreateBuilder() =>
            new EnergyUnitBuilder();

        protected override bool TryReadPredefinedUnit(string value, out EnergyUnit predefinedUnit) =>
            _predefinedUnits.TryGetValue(value, out predefinedUnit)
            || base.TryReadPredefinedUnit(value, out predefinedUnit);

        protected override bool TryWritePredefinedUnit(JsonWriter writer, EnergyUnit value, JsonSerializer serializer)
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
