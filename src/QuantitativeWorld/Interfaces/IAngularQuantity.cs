#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Interfaces
{
    using number = System.Double;
#endif

    public interface IAngularQuantity<TUnit>
        where TUnit : IAngularUnit
    {
        number Turns { get; }
        number Value { get; }
        TUnit Unit { get; }
    }
}