using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class WeightFactory : ILinearQuantityFactory<Weight, WeightUnit>
    {
        public Weight Create(double value, WeightUnit unit) =>
            new Weight(value, unit);
    }
}