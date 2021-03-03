#if DECIMAL
namespace DecimalQuantitativeWorld.Conversion
{
    using DecimalQuantitativeWorld.Interfaces;
#else
namespace QuantitativeWorld.Conversion
{
    using QuantitativeWorld.Interfaces;
#endif
    public class WeightConverter : ILinearQuantityConverter<Weight, WeightUnit>
    {
        public Weight Convert(Weight weight, WeightUnit targetUnit) =>
            weight.Convert(targetUnit);
    }
}
