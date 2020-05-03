using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class AreaFactory : ILinearQuantityFactory<Area, AreaUnit>
    {
        public Area Create(double value, AreaUnit unit) =>
            new Area(value, unit);
    }
}