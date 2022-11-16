#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using DecimalQuantitativeWorld.Interfaces;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
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