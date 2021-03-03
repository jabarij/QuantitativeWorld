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

    public class SpeedFactory : ILinearQuantityFactory<Speed, SpeedUnit>
    {
        public Speed Create(number value, SpeedUnit unit) =>
            new Speed(value, unit);
    }
}