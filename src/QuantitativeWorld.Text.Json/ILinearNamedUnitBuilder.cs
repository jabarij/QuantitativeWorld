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

    public interface ILinearNamedUnitBuilder<TUnit>
        where TUnit : ILinearUnit, INamedUnit
    {
        void SetName(string name);
        void SetAbbreviation(string abbreviation);
        void SetValueInBaseUnit(number valueInBaseUnit);
        bool TryBuild(out TUnit unit);
    }
}