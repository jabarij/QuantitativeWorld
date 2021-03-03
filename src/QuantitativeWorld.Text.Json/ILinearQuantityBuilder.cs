#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json
{
    using QuantitativeWorld.Interfaces;
    using number = System.Double;
#endif

    public interface ILinearQuantityBuilder<TQuantity, TUnit>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : struct, ILinearUnit
    {
        void SetBaseValue(number baseValue);
        void SetUnit(TUnit unit);
        void SetValue(number value);
        bool TryBuild(out TQuantity quantity, TUnit? defaultUnit = null);
    }
}