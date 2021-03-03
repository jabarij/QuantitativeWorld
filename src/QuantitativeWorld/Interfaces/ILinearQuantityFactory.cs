#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Interfaces
{
    using number = System.Double;
#endif

    public interface ILinearQuantityFactory<TQuantity, TUnit>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        TQuantity Create(number value, TUnit unit);
    }
}