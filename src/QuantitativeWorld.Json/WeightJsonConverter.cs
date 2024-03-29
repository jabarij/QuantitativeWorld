﻿#if DECIMAL
namespace DecimalQuantitativeWorld.Json;
#else
namespace QuantitativeWorld.Json;
#endif

public sealed class WeightJsonConverter : LinearQuantityJsonConverterBase<Weight, WeightUnit>
{
    public WeightJsonConverter(
        WeightJsonSerializationFormat serializationFormat = WeightJsonSerializationFormat.AsKilogramsWithUnit)
        : base(serializationFormat: (QuantityJsonSerializationFormat) serializationFormat)
    {
    }

    protected override string BaseValuePropertyName
        => nameof(Weight.Kilograms);

    protected override ILinearQuantityBuilder<Weight, WeightUnit> CreateBuilder()
        => new WeightBuilder();
}