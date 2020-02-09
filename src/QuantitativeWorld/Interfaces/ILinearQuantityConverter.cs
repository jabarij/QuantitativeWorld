namespace QuantitativeWorld.Interfaces
{
    public interface ILinearQuantityConverter<TQuantity, TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Convert(TQuantity quantity, TUnit targetUnit);
    }
}