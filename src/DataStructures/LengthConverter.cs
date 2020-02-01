namespace QuantitativeWorld
{
    public class LengthConverter : IQuantityConverter<Length, LengthUnit>
    {
        public Length Convert(Length length, LengthUnit targetUnit) =>
            length.Convert(targetUnit);
    }
}
