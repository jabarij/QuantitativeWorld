using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Text.Json
{
    public interface ILinearQuantityBuilder<TQuantity, TUnit>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : struct, ILinearUnit
    {
        void SetBaseValue(double baseValue);
        void SetUnit(TUnit unit);
        void SetValue(double value);
        bool TryBuild(out TQuantity quantity, TUnit? defaultUnit = null);
    }
}