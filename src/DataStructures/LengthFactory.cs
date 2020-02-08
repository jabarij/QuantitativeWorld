using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld
{
    public class LengthFactory : ILinearQuantityFactory<Length, LengthUnit>
    {
        public Length Create(decimal value, LengthUnit unit) =>
            new Length(value, unit);
    }
}