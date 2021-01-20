﻿namespace QuantitativeWorld.Text.Json
{
    public sealed class SpeedJsonConverter : LinearQuantityJsonConverterBase<Speed, SpeedUnit>
    {
        public SpeedJsonConverter(
            SpeedJsonSerializationFormat serializationFormat = SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new SpeedUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Speed.MetresPerSecond);
        protected override ILinearQuantityBuilder<Speed, SpeedUnit> CreateBuilder() =>
            new SpeedBuilder();
    }
}
