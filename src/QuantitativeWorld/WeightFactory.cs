using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class WeightFactory : ILinearQuantityFactory<Weight, WeightUnit>
    {
        public Weight Create(number value, WeightUnit unit) =>
            new Weight(value, unit);
    }
}