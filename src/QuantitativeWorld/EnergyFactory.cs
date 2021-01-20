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

    public class EnergyFactory : ILinearQuantityFactory<Energy, EnergyUnit>
    {
        public Energy Create(number value, EnergyUnit unit) =>
            new Energy(value, unit);
    }
}