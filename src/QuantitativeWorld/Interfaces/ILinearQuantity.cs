#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Interfaces
{
    using number = System.Double;
#endif

    public interface ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        number BaseValue { get; }
        TUnit BaseUnit { get; }
        number Value { get; }
        TUnit Unit { get; }
    }
}