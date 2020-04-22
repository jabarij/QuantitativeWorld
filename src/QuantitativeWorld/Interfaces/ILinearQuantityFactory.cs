namespace QuantitativeWorld.Interfaces
{
    public interface ILinearQuantityFactory<TQuantity, TUnit>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Create(double value, TUnit unit);
    }
}