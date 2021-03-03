#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
#else
namespace QuantitativeWorld.Interfaces
{
#endif
    public interface ILinearQuantityConverter<TQuantity, TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Convert(TQuantity quantity, TUnit targetUnit);
    }
}