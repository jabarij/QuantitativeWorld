namespace DataStructures
{
    public class WeightConverter : IQuantityConverter<Weight, WeightUnit>
    {
        public Weight Convert(Weight weight, WeightUnit targetUnit) =>
            weight.Convert(targetUnit);
    }
}
