using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Conversion
{
    public class LengthConverter : ILinearQuantityConverter<Length, LengthUnit>
    {
        public Length Convert(Length length, LengthUnit targetUnit) =>
            length.Convert(targetUnit);
    }
}
