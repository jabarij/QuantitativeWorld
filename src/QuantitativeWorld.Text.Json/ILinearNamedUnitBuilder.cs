using QuantitativeWorld.Interfaces;

namespace QuantitativeWorld.Text.Json
{
    public interface ILinearNamedUnitBuilder<TUnit>
        where TUnit : ILinearUnit, INamedUnit
    {
        void SetName(string name);
        void SetAbbreviation(string abbreviation);
        void SetValueInBaseUnit(decimal valueInBaseUnit);
        bool TryBuild(out TUnit unit);
    }
}