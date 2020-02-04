using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.BasicImplementations
{
    public class WeightFactory : IQuantityFactory<Weight, WeightUnit>
    {
        public Weight Create(decimal value, WeightUnit unit) =>
            new Weight(value, unit);
    }
}