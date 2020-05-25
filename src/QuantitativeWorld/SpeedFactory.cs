using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class SpeedFactory : ILinearQuantityFactory<Speed, SpeedUnit>
    {
        public Speed Create(double value, SpeedUnit unit) =>
            new Speed(value, unit);
    }
}