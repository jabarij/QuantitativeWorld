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

    public class LengthFactory : ILinearQuantityFactory<Length, LengthUnit>
    {
        public Length Create(number value, LengthUnit unit) =>
            new Length(value, unit);
    }
}