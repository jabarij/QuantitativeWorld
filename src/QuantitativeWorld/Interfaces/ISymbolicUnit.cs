#if DECIMAL
namespace DecimalQuantitativeWorld.Interfaces
{
#else
namespace QuantitativeWorld.Interfaces
{
#endif
    public interface ISymbolicUnit
    {
        string Symbol { get; }
    }
}
