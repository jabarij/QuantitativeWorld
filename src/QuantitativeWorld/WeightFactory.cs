#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
#endif

    public class WeightFactory : ILinearQuantityFactory<Weight, WeightUnit>
    {
        public Weight Create(number value, WeightUnit unit) =>
            new Weight(value, unit);
    }
}