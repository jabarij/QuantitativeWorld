using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class PowerFactory : ILinearQuantityFactory<Power, PowerUnit>
    {
        public Power Create(double value, PowerUnit unit) =>
            new Power(value, unit);
    }
}