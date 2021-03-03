#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Interfaces
{
    using number = System.Double;
#endif

    public interface ILinearUnit
    {
        number ValueInBaseUnit { get; }
    }
}