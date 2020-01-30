namespace DataStructures
{
    public interface IUnitConverter<TUnit>
    {
        decimal ConvertValue(decimal value, TUnit sourceUnit, TUnit targetUnit);
    }
}