using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Text.Json
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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