using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Angular
{
    public class AngleFactory : ILinearQuantityFactory<Angle, AngleUnit>
    {
        public Angle Create(double value, AngleUnit unit) =>
            new Angle(value, unit);
    }
}