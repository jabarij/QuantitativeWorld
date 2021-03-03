#if DECIMAL
namespace DecimalQuantitativeWorld.Conversion
{
    using DecimalQuantitativeWorld.Interfaces;
#else
namespace QuantitativeWorld.Conversion
{
    using QuantitativeWorld.Interfaces;
#endif
    public class LengthConverter : ILinearQuantityConverter<Length, LengthUnit>
    {
        public Length Convert(Length length, LengthUnit targetUnit) =>
            length.Convert(targetUnit);
    }
}
