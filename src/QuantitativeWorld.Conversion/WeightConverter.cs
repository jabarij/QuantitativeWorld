using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Conversion
{
    public class WeightConverter : ILinearQuantityConverter<Weight, WeightUnit>
    {
        public Weight Convert(Weight weight, WeightUnit targetUnit) =>
            weight.Convert(targetUnit);
    }
}
