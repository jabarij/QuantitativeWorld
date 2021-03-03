#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
#else
namespace QuantitativeWorld.Interfaces
{
#endif
    public interface INamedUnit
    {
        string Name { get; }
        string Abbreviation { get; }
    }
}
