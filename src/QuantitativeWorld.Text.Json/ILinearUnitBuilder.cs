using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Text.Json
{
    public interface ILinearUnitBuilder<TUnit>
        where TUnit : ILinearUnit
    {
        void SetName(string name);
        void SetAbbreviation(string abbreviation);
        void SetValueInBaseUnit(decimal valueInBaseUnit);
        bool TryBuild(out TUnit unit);
    }
}