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

    public class EnergyFactory : ILinearQuantityFactory<Energy, EnergyUnit>
    {
        public Energy Create(number value, EnergyUnit unit) =>
            new Energy(value, unit);
    }
}