using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class EnergyFactory : ILinearQuantityFactory<Energy, EnergyUnit>
    {
        public Energy Create(double value, EnergyUnit unit) =>
            new Energy(value, unit);
    }
}