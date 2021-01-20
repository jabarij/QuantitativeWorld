using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class PowerFactory : ILinearQuantityFactory<Power, PowerUnit>
    {
        public Power Create(number value, PowerUnit unit) =>
            new Power(value, unit);
    }
}