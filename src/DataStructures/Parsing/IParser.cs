namespace QuantitativeWorld.Parsing
{
    public interface IParser<T>
    {
        T Parse(string value);
        bool TryParse(string value, out T result);
    }
}
