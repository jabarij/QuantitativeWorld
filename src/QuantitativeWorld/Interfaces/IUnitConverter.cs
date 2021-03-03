#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Interfaces
{
    using number = System.Double;
#endif

    public interface IUnitConverter<TUnit>
    {
        number ConvertValue(number value, TUnit sourceUnit, TUnit targetUnit);
    }
}