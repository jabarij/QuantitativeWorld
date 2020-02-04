using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.BasicImplementations
{
    public class WeightConverter : IQuantityConverter<Weight, WeightUnit>
    {
        public Weight Convert(Weight weight, WeightUnit targetUnit) =>
            weight.Convert(targetUnit);
    }
}
