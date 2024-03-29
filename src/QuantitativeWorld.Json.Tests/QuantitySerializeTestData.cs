﻿#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests
{
#else
namespace QuantitativeWorld.Json.Tests
{
#endif
    public class QuantitySerializeTestData<TQuantity>
    {
        public QuantitySerializeTestData(
            TQuantity quantity,
            LinearUnitJsonSerializationFormat unitFormat,
            string expectedJsonPattern)
        {
            Quantity = quantity;
            UnitFormat = unitFormat;
            ExpectedJsonPattern = expectedJsonPattern;
        }

        public TQuantity Quantity { get; set; }
        public LinearUnitJsonSerializationFormat UnitFormat { get; set; }
        public string ExpectedJsonPattern { get; set; }
    }
}
