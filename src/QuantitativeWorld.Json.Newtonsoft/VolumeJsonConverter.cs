﻿#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
#endif
    public sealed class VolumeJsonConverter : LinearQuantityJsonConverterBase<Volume, VolumeUnit>
    {
        public VolumeJsonConverter(
            VolumeJsonSerializationFormat serializationFormat = VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
            LinearUnitJsonSerializationFormat unitSerializationFormat = LinearUnitJsonSerializationFormat.PredefinedAsString)
            : base(
                  unitConverter: new VolumeUnitJsonConverter(unitSerializationFormat),
                  serializationFormat: (QuantityJsonSerializationFormat)serializationFormat)
        { }

        protected override string BaseValuePropertyName =>
            nameof(Volume.CubicMetres);
        protected override ILinearQuantityBuilder<Volume, VolumeUnit> CreateBuilder() =>
            new VolumeBuilder();
    }
}
