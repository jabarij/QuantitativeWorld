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

    public class PowerFactory : ILinearQuantityFactory<Power, PowerUnit>
    {
        public Power Create(number value, PowerUnit unit) =>
            new Power(value, unit);
    }
}