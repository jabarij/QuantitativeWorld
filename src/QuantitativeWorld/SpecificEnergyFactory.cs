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

    public class SpecificEnergyFactory : ILinearQuantityFactory<SpecificEnergy, SpecificEnergyUnit>
    {
        public SpecificEnergy Create(number value, SpecificEnergyUnit unit) =>
            new SpecificEnergy(value, unit);
    }
}