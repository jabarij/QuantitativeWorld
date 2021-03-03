#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
#else
namespace QuantitativeWorld.Text.Parsing
{
#endif
    public interface IParser<T>
    {
        T Parse(string value);
        bool TryParse(string value, out T result);
    }
}
